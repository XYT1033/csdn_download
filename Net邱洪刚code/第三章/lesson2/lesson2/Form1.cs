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
namespace lesson2
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
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                axMapControl1.LoadMxFile(filePath, 0, Type.Missing);
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            else
            {
                MessageBox.Show(filePath + "不是有效的地图文档");
            }
        }
        //获取线实体
        private IFeature getPolylineFeature(string layerName)
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
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "";
            IFeatureCursor featureCursor = featureClass.Search(queryFilter, true);
            IFeature feature = featureCursor.NextFeature();
            if (feature != null)
            {
                return feature;
            }
            return null;
        }
        //获取所有点实体
        private System.Collections.ArrayList getPointFeature(string layerName)
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
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "";
            IFeatureCursor featureCursor = featureClass.Search(queryFilter, true);
            IFeature feature = featureCursor.NextFeature();
            System.Collections.ArrayList features = new System.Collections.ArrayList();
            while (feature != null)
            {
                features.Add(feature as object);
                feature = featureCursor.NextFeature();
            }
            return features;
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
        //沿线创建法
        private void button1_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IFeature polylineFeature;
            polylineFeature = getPolylineFeature("polyline");
            if (polylineFeature != null)
            {
                ICurve curve = polylineFeature.Shape as ICurve;
                IConstructPoint constructPoint = new PointClass();
                constructPoint.ConstructAlong(curve, esriSegmentExtension.esriNoExtension, 10, true);
                IPoint point1 = constructPoint as IPoint;
                constructPoint = new PointClass();
                constructPoint.ConstructAlong(curve, esriSegmentExtension.esriNoExtension, 20, false);
                IPoint point2 = constructPoint as IPoint;
                addFeature("point", point1);
                addFeature("point", point2);
            }
            axMapControl1.Refresh();
        }
        //角平分线创建法
        private void button2_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint point1, point2, point3, point4;
            point1 = new PointClass();
            point2 = new PointClass();
            point3 = new PointClass();
            point1.PutCoords(0, 0);
            point2.PutCoords(0, 20);
            point3.PutCoords(20, 20);
            IConstructPoint constructPoint = new PointClass();
            constructPoint.ConstructAngleBisector(point1, point2, point3, 50, true);
            point4 = constructPoint as IPoint;
            addFeature("point", point1);
            addFeature("point", point2);
            addFeature("point", point3);
            addFeature("point", point4);
            axMapControl1.Refresh();
        }
        //构造角度距离法
        private void button3_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint point1, point2;
            point1 = new PointClass();
            point1.PutCoords(0, 0);
            double distance = 20;
            double angle = 60;
            IConstructPoint constructPoint = new PointClass();
            double angleRad = angle * 2 * Math.PI / 360;
            constructPoint.ConstructAngleDistance(point1, angleRad, distance);
            point2 = constructPoint as IPoint;
            addFeature("point", point1);
            addFeature("point", point2);
            axMapControl1.Refresh();
        }
        //构造角度交点法
        private void button4_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint point1, point2;
            point1 = new PointClass();
            point2 = new PointClass();
            point1.PutCoords(0, 0);
            point2.PutCoords(10, 0);
            double angleRed1 = 45 * 2 * Math.PI / 360;
            double angleRed2 = 60 * 2 * Math.PI / 360;
            IConstructPoint constructPoint = new PointClass();
            constructPoint.ConstructAngleIntersection(point1, angleRed1, point2, angleRed2);

            addFeature("point", point1);
            addFeature("point", point2);
            addFeature("point", constructPoint as IPoint);
            axMapControl1.Refresh();
        }
        //构造偏转角度法
        private void button5_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint fromPoint = new PointClass();
            fromPoint.PutCoords(0, 0);
            IPoint toPoint = new PointClass();
            toPoint.PutCoords(1, 1);
            ILine line = new LineClass();
            line.PutCoords(fromPoint, toPoint);
            double distance = 1.4142135623731;
            double angle = Math.PI / 4;
            IConstructPoint constructionPoint = new PointClass();
            constructionPoint.ConstructDeflection(line, distance, angle);
            IPoint point = constructionPoint as IPoint;
            addFeature("point", fromPoint);
            addFeature("point", toPoint);
            addFeature("point", point);
            axMapControl1.Refresh();
        }
        //构造偏转角交点法
        private void button6_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint fromPoint = new PointClass();
            fromPoint.PutCoords(0, 0);
            IPoint toPoint = new PointClass();
            toPoint.PutCoords(1, 1);
            ILine line = new LineClass();
            line.PutCoords(fromPoint, toPoint);
            double startAngle = Math.PI / 4;
            double endAngle = Math.PI / 4;
            IConstructPoint constructionPoint = new PointClass();
            constructionPoint.ConstructDeflectionIntersection(line, startAngle, endAngle, false);
            IPoint point = constructionPoint as IPoint;
            addFeature("point", fromPoint);
            addFeature("point", toPoint);
            addFeature("point", point);
            axMapControl1.Refresh();
        }
        //构造偏移点法
        private void button7_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint[] points = new IPoint[4];
            for (int i = 0; i < 4; i++)
            {
                points[i] = new PointClass();
            }
            points[0].PutCoords(0, 0);
            points[1].PutCoords(10, 0);
            points[2].PutCoords(20, 0);
            points[3].PutCoords(30, 0);
            IPointCollection polyline = new Polyline();
            IGeometryBridge geometryBride = new GeometryEnvironmentClass();
            geometryBride.AddPoints(polyline as IPointCollection4, ref points);
            IConstructPoint constructionPointRight = new PointClass();
            IConstructPoint constructionPointLeft = new PointClass();
            constructionPointRight.ConstructOffset(polyline as ICurve, esriSegmentExtension.esriNoExtension, 15, false, 5);
            IPoint outPutPoint1 = constructionPointRight as IPoint;
            constructionPointLeft.ConstructOffset(polyline as ICurve, esriSegmentExtension.esriNoExtension, 1, false, -5);
            IPoint outPutPoint2 = constructionPointLeft as IPoint;
            addFeature("point", points[0]);
            addFeature("point", points[1]);
            addFeature("point", points[2]);
            addFeature("point", points[3]);
            addFeature("point", outPutPoint1);
            addFeature("point", outPutPoint2);
            axMapControl1.Refresh();
        }
        //构造平行线上点法
        private void button8_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint[] points = new IPoint[2];
            for (int i = 0; i < 2; i++)
            {
                points[i] = new PointClass();
            }
            points[0].PutCoords(0, 0);
            points[1].PutCoords(20, 0);


            ISegment segment;
            ILine line = new LineClass();
            line.FromPoint = points[0];
            line.ToPoint = points[1];

            segment = line as ISegment;
            IPoint fromPoint = new PointClass();
            fromPoint.X = points[0].X + 10;
            fromPoint.Y = points[0].Y + 5;
            IConstructPoint constructPoint = new PointClass();
            constructPoint.ConstructParallel(segment, esriSegmentExtension.esriNoExtension, fromPoint, segment.Length);
            addFeature("point", points[0]);
            addFeature("point", points[1]);
            addFeature("point", constructPoint as IPoint);
            axMapControl1.Refresh();
        }
        //构造垂直线上点法
        private void button9_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint[] points = new IPoint[2];
            for (int i = 0; i < 2; i++)
            {
                points[i] = new PointClass();
            }
            points[0].PutCoords(0, 0);
            points[1].PutCoords(20, 0);


            ISegment segment;
            ILine line = new LineClass();
            line.FromPoint = points[0];
            line.ToPoint = points[1];

            segment = line as ISegment;
            IPoint fromPoint = new PointClass();
            fromPoint.X = points[0].X + 10;
            fromPoint.Y = points[0].Y + 10;
            IConstructPoint constructPoint = new PointClass();
            constructPoint.ConstructPerpendicular(segment, esriSegmentExtension.esriNoExtension, fromPoint, 3, false);
            addFeature("point", points[0]);
            addFeature("point", points[1]);
            addFeature("point", constructPoint as IPoint);
            axMapControl1.Refresh();
        }
        //后方交会定点法
        private void button10_Click(object sender, EventArgs e)
        {
            delFeature("point");
            IPoint[] points = new IPoint[3];
            for (int i = 0; i < 3; i++)
            {
                points[i] = new PointClass();
            }
            IConstructPoint constructPoint = new PointClass();
            points[0].PutCoords(0, 10);
            points[1].PutCoords(20, 20);
            points[2].PutCoords(0, 0);
            double angle1 = Math.PI / 4;
            double angle2 = Math.PI / 4;
            double angle3 = 0;
            try
            {
                constructPoint.ConstructThreePointResection(points[0], angle1, points[1], angle2, points[2], out angle3);
                addFeature("point", points[0]);
                addFeature("point", points[1]);
                addFeature("point", points[2]);
                addFeature("point", constructPoint as IPoint);
            }
            catch (Exception ex)
            {

            }
            axMapControl1.Refresh();
        }


    }
}