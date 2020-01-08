using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;

namespace GeometrySample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.Engine);
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadMapDocument();
        }

        //加载地图文档
        private void loadMapDocument()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开地图文档";
            openFileDialog.Filter = "map documents(*.mxd)|*.mxd";
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            if (axMapControl1.CheckMxFile(filePath))
            {
                axMapControl1.MousePointer = esriControlsMousePointer. esriPointerHourglass;
                axMapControl1.LoadMxFile(filePath, 0, Type.Missing);
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                MessageBox.Show(filePath + "不是有效的地图文档");
            }
        }
        //添加实体对象到地图图层
        private void addFeature(string layerName,IGeometry geometry)
        {
            int i = 0;
            ILayer layer = null;
            for (i = 0; i < axMapControl1.LayerCount; i++)
            {
                layer = axMapControl1.Map.get_Layer(i);
                if (layer.Name.ToLower() == layerName)
                {
                    break;
                }
            }
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IDataset dataset = (IDataset)featureClass;
            IWorkspace workspace = dataset.Workspace;
            //开始空间编辑
            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();
            IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
            IFeatureCursor featureCursor;
            //清除图层原有的实体对象
            featureCursor = featureClass.Search(null, true);
            IFeature feature;
            feature = featureCursor.NextFeature();
            while (feature!=null)
            {
                feature.Delete();
                feature = featureCursor.NextFeature();
            }
            //开始插入新的实体对象
            featureCursor = featureClass.Insert(true);
            featureBuffer.Shape = geometry;
            object featureOID = featureCursor.InsertFeature(featureBuffer);
            //保存实体
            featureCursor.Flush();
            //结束空间编辑
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject (featureCursor);
        }

        //添加实体对象
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IGeometryCollection geometryCollection = new MultipointClass();
            IMultipoint multipoint;
            object missing = Type.Missing;
            IPoint point;
            for (int i = 0; i < 10; i++)
            {
                point = new PointClass();
                point.PutCoords(i * 2, i * 2);

                geometryCollection.AddGeometry(point as IGeometry, ref missing, ref missing);
            }
            multipoint = geometryCollection as IMultipoint;
            addFeature("multipoint", multipoint as IGeometry);
            this.axMapControl1.Extent = multipoint.Envelope;
            this.axMapControl1.Refresh();    
        }
        //添加实体对象集
        private void addGeometryCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeometryCollection geometryCollection1 = new MultipointClass();
            IGeometryCollection geometryCollection2 = new MultipointClass();
            IMultipoint multipoint;
            object missing = Type.Missing;
            IPoint point;
            for (int i = 0; i < 10; i++)
            {
                point = new PointClass();
                point.PutCoords(i * 2, i * 2);
                geometryCollection1.AddGeometry(point as IGeometry, ref missing, ref missing);

            }
            geometryCollection2.AddGeometryCollection(geometryCollection1);
            multipoint = geometryCollection2 as IMultipoint;
            addFeature("multipoint", multipoint as IGeometry);
            this.axMapControl1.Extent = multipoint.Envelope;
            this.axMapControl1.Refresh();  
        }
        //插入实体对象集
        private void insertGeometriesCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeometryCollection geometryCollection1 = new MultipointClass();
            IGeometryCollection geometryCollection2 = new MultipointClass();
            IGeometryCollection geometryCollection3 = new MultipointClass();
            IGeometryCollection geometryCollection4 = new MultipointClass();
            IMultipoint multipoint;
            object missing = Type.Missing;
            IPoint point;
            //创建3个实体对象集
            for (int i = 0; i < 10; i++)
            {
                point = new PointClass();
                point.PutCoords(i * 2, i);
                geometryCollection1.AddGeometry(point as IGeometry, ref missing, ref missing);
            }
            for (int i = 0; i < 10; i++)
            {
                point = new PointClass();
                point.PutCoords(i, i);
                geometryCollection2.AddGeometry(point as IGeometry, ref missing, ref missing);
            }
            for (int i = 0; i < 10; i++)
            {
                point = new PointClass();
                point.PutCoords(i, i * 2);
                geometryCollection3.AddGeometry(point as IGeometry, ref missing, ref missing);
            }
            geometryCollection1.InsertGeometryCollection(1, geometryCollection2);
            geometryCollection1.InsertGeometryCollection(1, geometryCollection3);

            multipoint = geometryCollection1 as IMultipoint;
            addFeature("multipoint", multipoint as IGeometry);
            this.axMapControl1.Extent = multipoint.Envelope;
            this.axMapControl1.Refresh(); 
        }
        //用实体数组取代实体集中的实体对象
        private void setGeometriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeometryCollection geometryCollection1 = new MultipointClass();
            IGeometryBridge geometryBridge = new GeometryEnvironmentClass();
            IGeometry[] geometryArray = new IGeometry[10];
            IMultipoint multipoint;
            object missing = Type.Missing;
            IPoint point;
            for (int i = 0; i < 10; i++)
            {
                point = new PointClass();
                point.PutCoords(i * 2, i * 2);
                geometryArray[i] = point as IGeometry;
            }
            geometryBridge.SetGeometries(geometryCollection1, ref geometryArray);
            multipoint = geometryCollection1 as IMultipoint;
            addFeature("multipoint", multipoint as IGeometry);
            this.axMapControl1.Extent = multipoint.Envelope;
            this.axMapControl1.Refresh();
        }
        //添加SEGMENT
        private void addSegmentColllectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ISegment[] segmentArray = new ISegment[10];
            IPolyline polyline = new PolylineClass();
            ISegmentCollection segmentCollection = new PolylineClass();
            for (int i = 0; i < 10; i++)
            {
                ILine line = new LineClass();
                IPoint fromPoint = new PointClass();
                fromPoint.PutCoords(i * 10, i * 10);
                IPoint toPoint = new PointClass();
                toPoint.PutCoords(i * 15, i * 15);
                line.PutCoords(fromPoint, toPoint);
                segmentArray[i] = line as ISegment;
                segmentCollection.AddSegment(line as ISegment,Type.Missing,Type.Missing);
            }
            //也可通过IGeometryBridge对象的AddSegments方法进行整个segment数据的添加
            //IGeometryBridge geometryBridge = new GeometryEnvironmentClass();
            //geometryBridge.AddSegments(segmentCollection, ref segmentArray);
            polyline = segmentCollection as IPolyline;
            addFeature("polyline", polyline as IGeometry);
            this.axMapControl1.Extent = polyline.Envelope;
            this.axMapControl1.Refresh();  

        }
        //查询Segment
        private void querySegmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ISegment[] segmentArray = new ISegment[10];
            for (int i = 0; i < 10; i++)
            {
                ILine line = new LineClass();
                IPoint fromPoint = new PointClass();
                fromPoint.PutCoords(i * 10, i * 10);
                IPoint toPoint = new PointClass();
                toPoint.PutCoords(i * 15, i * 15);
                line.PutCoords(fromPoint, toPoint);
                segmentArray[i] = line as ISegment;
            }
            ISegmentCollection segmentCollection = new PolylineClass();            
            IGeometryBridge geometryBridge = new GeometryEnvironmentClass();
            geometryBridge.AddSegments(segmentCollection, ref segmentArray);
           
            int index = 0;
            ISegment[] outputSegmentArray = new ISegment[segmentCollection.SegmentCount - index];
            for (int i = 0; i < outputSegmentArray.Length; i++)
            {
                outputSegmentArray[i] = new LineClass();
            }
            //查询Segment
            geometryBridge.QuerySegments(segmentCollection, index, ref outputSegmentArray);
            String report = "";
            for (int i = 0; i < outputSegmentArray.Length; i++)
            {
                ISegment currentSegment = outputSegmentArray[i];
                ILine currentLine = currentSegment as ILine;
                report = report + "index = " + i + " , FromPoint X = " + currentLine.FromPoint.X +
                    " , FromPoint Y = " + currentLine. FromPoint.X;
                report = report + " , ToPoint X = " + currentLine.ToPoint.X + " , ToPoint Y = " +
                    currentLine.ToPoint.X + "\n";
            }
            System.Windows.Forms.MessageBox.Show(report);

        }

        private void iSegmentCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void setSegmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPolyline polyline = new PolylineClass();
            ISegmentCollection segmentCollection = new PolylineClass();
            IGeometryBridge geometryBridge = new GeometryEnvironmentClass();
            ISegment[] insertSegmentArray = new ISegment[5];
            for (int i = 0; i < 5; i++)
            {
                ILine insertLine = new LineClass();
                IPoint insertFromPoint = new PointClass();
                insertFromPoint.PutCoords(i, 1);
                IPoint insertToPoint = new PointClass();
                insertToPoint.PutCoords(i * 10, 1);
                insertLine.PutCoords(insertFromPoint, insertToPoint);
                insertSegmentArray[i] = insertLine as ISegment;
            }
            geometryBridge.SetSegments(segmentCollection, ref insertSegmentArray);
            polyline = segmentCollection as IPolyline;
            addFeature("polyline", polyline as IGeometry);
            this.axMapControl1.Extent = polyline.Envelope;
            this.axMapControl1.Refresh(); 

        }

        private void addPointCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPointCollection4 pointCollection = new MultipointClass();
            IPointCollection pointCollection2 = new MultipointClass();
            IGeometryBridge geometryBridge = new GeometryEnvironmentClass();
            IPoint[] points = new PointClass[10];
            IMultipoint multipoint;
            object missing = Type.Missing;
            IPoint point;
            for (int i = 0; i < 10; i++)
            {
                point = new PointClass();
                point.PutCoords(i * 5, i);
                points[i] = point;
            }
            geometryBridge.SetPoints(pointCollection, ref points);
            pointCollection2.AddPointCollection(pointCollection);
            multipoint = pointCollection2 as IMultipoint;
            addFeature("multipoint", multipoint as IGeometry);
            this.axMapControl1.Extent = multipoint.Envelope;
            this.axMapControl1.Refresh(); 

        }

        private void queryPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPoint point1 = new PointClass();
            point1.PutCoords(10, 10);
            IPoint point2 = new PointClass();
            point2.PutCoords(20, 20);
            IPoint[] inputPointArray = new IPoint[2];
            inputPointArray[0] = point1;
            inputPointArray[1] = point2;
            IPointCollection4 pointCollection = new MultipointClass();
            IGeometryBridge geometryBridge = new GeometryEnvironmentClass();
            geometryBridge.AddPoints(pointCollection, ref inputPointArray);

            int index = 0;
            IPoint[] outputPointArray = new IPoint[2];
            for (int i = 0; i < outputPointArray.Length; i++)
            {
                outputPointArray[i] = new PointClass();
            }
            pointCollection.QueryPoint(0, outputPointArray[0]);
            //geometryBridge.QueryPoints(pointCollection, index, ref outputPointArray);
            for (int i = 0; i < outputPointArray.Length; i++)
            {
                IPoint currentPoint = outputPointArray[i];
                if (currentPoint.IsEmpty==true)
                {
                    System.Windows.Forms.MessageBox.Show("Current point = null");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("X = " + currentPoint.X + ", Y = " + currentPoint.Y);
                }
            }

        }

        private void updatePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMultipoint multipoint;
            object missing = Type.Missing;
            IPoint point1 = new PointClass();
            point1.PutCoords(10, 10);
            IPoint point2 = new PointClass();
            point2.PutCoords(20, 20);
            IPointCollection pointCollection = new MultipointClass();
            pointCollection.AddPoint(point1, ref missing, ref missing);
            pointCollection.AddPoint(point2, ref missing, ref missing);
            point1 = new PointClass();
            point1.PutCoords(40, 10);
            pointCollection.UpdatePoint(1, point1);
            multipoint = pointCollection as IMultipoint;
            addFeature("multipoint", multipoint as IGeometry);
            System.Windows.Forms.MessageBox.Show("X = " + pointCollection.get_Point(1).X +
                ", Y = " + pointCollection.get_Point(1).Y);
            this.axMapControl1.Extent = multipoint.Envelope;
            this.axMapControl1.Refresh();

        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {

        }












    }
}
