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
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;

namespace lesson1
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
        //添加实体对象到地图图层
        private void addFeature(string layerName, IGeometry geometry)
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
            //清除图层原有实体对象
            featureCursor = featureClass.Update(null, true);
            IFeature feature;
            feature = featureCursor.NextFeature();
            while (feature != null)
            {
                featureCursor.DeleteFeature();
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
            System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
        }
        //添加图层
        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开shp文件";
            openFileDialog.Filter = "shp layer(*.shp)|*.shp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog.FileName;
                string filePath, fileName;
                int index = 0;
                index = file.LastIndexOf("\\");
                filePath = file.Substring(0, index);
                fileName = file.Substring(index + 1, file.Length - index - 1);
                IWorkspaceFactory workspaceFactory;
                IFeatureWorkspace featureWorkspace;
                IFeatureLayer featureLayer;
                workspaceFactory = new ShapefileWorkspaceFactoryClass();
                featureWorkspace = workspaceFactory.OpenFromFile(filePath, 0) as IFeatureWorkspace;
                featureLayer = new FeatureLayerClass();
                featureLayer.FeatureClass = featureWorkspace.OpenFeatureClass(fileName);
                featureLayer.Name = featureLayer.FeatureClass.AliasName;
                this.axMapControl1.AddLayer(featureLayer as ILayer);
                CreatePolyline();
                this.axMapControl1.Refresh();
            }
        }
        //创建线对象
        private void CreatePolyline()
        {
            ISegment[] segmentArray = new ISegment[10];
            IPolyline polyline = new PolylineClass();
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
            polyline = segmentCollection as IPolyline;
            addFeature("polyline", polyline as IGeometry);
            this.axMapControl1.Extent = polyline.Envelope;
            this.axMapControl1.Refresh();
        }

        //选择实体
        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IMap pMap;
            IActiveView pActiveView;
            pMap = axMapControl1.Map;
            pActiveView = pMap as IActiveView;
            IEnvelope pEnv;
            pEnv = axMapControl1.TrackRectangle();
            ISelectionEnvironment pSelectionEnv;
            pSelectionEnv = new SelectionEnvironmentClass();
            pSelectionEnv.DefaultColor = getRGB(110, 120, 210);
            pMap.SelectByShape(pEnv, pSelectionEnv, false);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
            null);
        }
        private IRgbColor getRGB(int r, int g, int b)
        {
            IRgbColor pColor;
            pColor = new RgbColorClass();
            pColor.Red = r;
            pColor.Green = g;
            pColor.Blue = b;
            return pColor;
        }
        //添加元素
        private void button2_Click(object sender, EventArgs e)
        {
            IGraphicsContainer graphicsContainer;
            IMap map = this.axMapControl1.Map;
            ILineElement lineElement = new LineElementClass();
            IElement element;
            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 5);
            polyline.FromPoint = point;
            point.PutCoords(80, 5);
            polyline.ToPoint = point;
            element = lineElement as IElement;
            element.Geometry = polyline as IGeometry;
            graphicsContainer = map as IGraphicsContainer;
            graphicsContainer.AddElement(element, 0);
            this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        //更新元素
        private void button3_Click(object sender, EventArgs e)
        {
            IGraphicsContainer graphicsContainer;
            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 5);
            polyline.FromPoint = point;
            point.PutCoords(80, 20);
            polyline.ToPoint = point;
            IElement el;
            graphicsContainer = this.axMapControl1.Map as IGraphicsContainer;
            graphicsContainer.Reset();
            el = graphicsContainer.Next();
            if (el != null)
            {
                el.Geometry = polyline;
                graphicsContainer.UpdateElement(el);
            }
            this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        //删除元素
        private void button4_Click(object sender, EventArgs e)
        {
            IGraphicsContainer graphicsContainer;
            IElement el;
            graphicsContainer = this.axMapControl1.Map as IGraphicsContainer;
            graphicsContainer.Reset();
            el = graphicsContainer.Next();
            while (el != null)
            {
                graphicsContainer.DeleteElement(el);
                el = graphicsContainer.Next();
            }
            this.axMapControl1.ActiveView.Refresh();
        }
    }
}