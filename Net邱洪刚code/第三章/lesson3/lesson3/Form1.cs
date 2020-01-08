using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
namespace lesson3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.Engine);
            InitializeComponent();
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
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                axMapControl1.LoadMxFile(filePath, 0, Type.Missing);
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                MessageBox.Show(filePath + "不是有效的地图文档");
            }
        }
        private void delFeature(string layerName)
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
            IDataset pDataset = featureClass as IDataset;
            IFeatureCursor featureCursor;
            //清除图层原有实体对象
            featureCursor = featureClass.Update(null, true);
            IFeature feature;
            feature = featureCursor.NextFeature();
            while (feature != null)
            {
                featureCursor.DeleteFeature();
                feature = featureCursor.NextFeature();
            }



        }
        //添加点实体
        private void addFeature(string layerName, IPoint point)
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
            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();
            IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
            IFeatureCursor featureCursor = featureClass.Insert(true);

            featureBuffer.Shape = point;
            object featureOID = featureCursor.InsertFeature(featureBuffer);
            featureCursor.Flush();
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadMapDocument();
        }
        //构造圆弧点
        private void button1_Click(object sender, EventArgs e)
        {
            delFeature("point");
            //构造一段圆弧
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(10, 0);
            IPoint fromPoint = new PointClass();
            fromPoint.PutCoords(0, 0);
            IPoint toPoint = new PointClass();
            toPoint.PutCoords(0, 20);
            IConstructCircularArc circularArcConstruction = new CircularArcClass();
            circularArcConstruction.ConstructThreePoints(fromPoint, centerPoint, toPoint, false);
            //构造圆弧点
            IConstructMultipoint constructMultipoint = new MultipointClass();
            constructMultipoint.ConstructArcPoints(circularArcConstruction as ICircularArc);
            IPointCollection pointCollection = constructMultipoint as IPointCollection;
            for (int i = 0; i < pointCollection.PointCount; i++)
            {
                addFeature("point", pointCollection.get_Point(i));
            }

            axMapControl1.Refresh();

        }
        //构造等分点
        private void button2_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(10, 10);
            IPoint fromPoint = new PointClass();
            fromPoint.PutCoords(0, 0);
            IPoint toPoint = new PointClass();
            toPoint.PutCoords(10, 20);
            ICircularArc circularArcConstruction = new CircularArcClass();
            circularArcConstruction.PutCoords(centerPoint, fromPoint, toPoint, esriArcOrientation.esriArcClockwise);
            IConstructMultipoint constructMultipoint = new MultipointClass();
            constructMultipoint.ConstructDivideEqual(circularArcConstruction as ICurve, (int)circularArcConstruction.Length / 5);
            IPointCollection pointCollection = constructMultipoint as IPointCollection;
            for (int i = 0; i < pointCollection.PointCount; i++)
            {
                addFeature("point", pointCollection.get_Point(i));
            }

            axMapControl1.Refresh();
        }
        //构造等长点
        private void button3_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(10, 0);
            IPoint fromPoint = new PointClass();
            fromPoint.PutCoords(0, 0);
            IPoint toPoint = new PointClass();
            toPoint.PutCoords(0, 20);
            ICircularArc circularArcConstruction = new CircularArcClass();
            circularArcConstruction.PutCoords(centerPoint, fromPoint, toPoint, esriArcOrientation.esriArcClockwise);
            IConstructMultipoint constructMultipoint = new MultipointClass();
            constructMultipoint.ConstructDivideLength(circularArcConstruction as ICurve, 10);
            IPointCollection pointCollection = constructMultipoint as IPointCollection;
            for (int i = 0; i < pointCollection.PointCount; i++)
            {
                addFeature("point", pointCollection.get_Point(i));
            }

            axMapControl1.Refresh();

        }
        //构造交点
        private void button4_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint[] points = new IPoint[4];
            for (int i = 0; i < 4; i++)
            {
                points[i] = new PointClass();
            }

            points[0].PutCoords(15, 10);
            points[1].PutCoords(20, 60);
            points[2].PutCoords(40, 60);
            points[3].PutCoords(45, 10);
            addFeature("point", points[0]);
            addFeature("point", points[1]);
            addFeature("point", points[2]);
            addFeature("point", points[3]);
            //构造Bezier曲线
            IBezierCurveGEN bezierCurve = new BezierCurveClass();
            bezierCurve.PutCoords(ref points);
            IPoint centerPoint = new PointClass();
            centerPoint.PutCoords(30, 30);
            IPoint fromPoint = new PointClass();
            fromPoint.PutCoords(10, 10);
            IPoint toPoint = new PointClass();
            toPoint.PutCoords(50, 10);
            //构造圆弧
            IConstructCircularArc circularArcConstruction = new CircularArcClass();
            circularArcConstruction.ConstructThreePoints(fromPoint, centerPoint, toPoint, false);


            object param0;
            object param1;
            object isTangentPoint;
            IConstructMultipoint constructMultipoint = new MultipointClass();
            constructMultipoint.ConstructIntersection(circularArcConstruction as ISegment, esriSegmentExtension.esriNoExtension, bezierCurve as ISegment, esriSegmentExtension.esriNoExtension, out param0, out param1, out isTangentPoint);
            IMultipoint multipoint = constructMultipoint as IMultipoint;
            IPointCollection pointCollection = multipoint as IPointCollection;
            for (int i = 0; i < pointCollection.PointCount; i++)
            {
                addFeature("point", pointCollection.get_Point(i));
            }

            axMapControl1.Extent = multipoint.Envelope;
            axMapControl1.Refresh();

        }
        //构造切线点
        private void button5_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint[] points = new IPoint[4];
            for (int i = 0; i < 4; i++)
            {
                points[i] = new PointClass();
            }

            points[0].PutCoords(15, 10);
            points[1].PutCoords(20, 60);
            points[2].PutCoords(40, 60);
            points[3].PutCoords(45, 10);
            addFeature("point", points[0]);
            addFeature("point", points[1]);
            addFeature("point", points[2]);
            addFeature("point", points[3]);

            IBezierCurveGEN bezierCurve = new BezierCurveClass();
            bezierCurve.PutCoords(ref points);
            IConstructMultipoint constructMultipoint = new MultipointClass();
            constructMultipoint.ConstructTangent(bezierCurve as ICurve, points[1]);
            IMultipoint multipoint = constructMultipoint as IMultipoint;
            IPointCollection pointCollection = multipoint as IPointCollection;
            for (int i = 0; i < pointCollection.PointCount; i++)
            {
                addFeature("point", pointCollection.get_Point(i));
            }
            axMapControl1.Refresh();
        }
    }
}