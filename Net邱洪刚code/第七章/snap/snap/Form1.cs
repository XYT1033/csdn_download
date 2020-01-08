using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Snap
{
    public partial class Form1 : Form
    {
        bool bCreateElement = true;
        int internalTime = 5;//时间间隔
        int snapTime = 10;//初始值
        IElement m_element = null;  //界面绘制点元素
        IPoint currentPoint; //当前鼠标点
        IPoint snapPoint = null;  //捕捉到的点
        IMovePointFeedback movePointFeedback;
        string snapLayer = "";
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.Engine);
            currentPoint = new PointClass();
            movePointFeedback = new MovePointFeedbackClass();
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //加载地图文档
            loadMapDocument();
            for (int i = 0; i < this.axMapControl1.LayerCount; i++)
            {
                cbLayerName.Items.Add(this.axMapControl1.get_Layer(i).Name);
            }
            cbLayerName.Text = cbLayerName.Items[0].ToString();
            snapLayer = cbLayerName.Text;
        }

        //鼠标移动事件
        private void axMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            currentPoint.PutCoords(e.mapX, e.mapY);

            snapTime++;
            snapTime = snapTime % internalTime;
            ILayer layer = GetLayerByName(snapLayer, axMapControl1);
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            if (bCreateElement)
            {
                CreateMarkerElement(currentPoint);
                bCreateElement = false;
            }
            if (snapPoint == null)
                ElementMoveTo(currentPoint);
            //鼠标自动扑获顶点  
            if (snapTime == 0)
                snapPoint = Snapping(e.mapX, e.mapY, featureLayer);
            if (snapPoint != null && snapTime == 0)
                ElementMoveTo(snapPoint);

        }
        //捕捉
        public IPoint Snapping(double x, double y, IFeatureLayer featureLayer)
        {
            IMap map = this.axMapControl1.Map;
            IActiveView activeView = this.axMapControl1.ActiveView;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IPoint point = new PointClass();
            point.PutCoords(x, y);

            IFeature feature = featureClass.GetFeature(0);
            IPoint hitPoint1 = new PointClass();
            IPoint hitPoint2 = new PointClass();
            IHitTest hitTest = feature.Shape as IHitTest;
            double hitDist = 0;
            int partIndex = 0;
            int vertexIndex = 0;
            bool bVertexHit = false;

            double tol = ConvertPixelsToMapUnits(activeView, 8);
            if (hitTest.HitTest(point, tol, esriGeometryHitPartType.esriGeometryPartBoundary,
                hitPoint2, ref hitDist, ref partIndex, ref vertexIndex, ref bVertexHit))
            {
                hitPoint1 = hitPoint2;
            }
            axMapControl1.ActiveView.Refresh();
            return hitPoint1;
        }
        public void CreateMarkerElement(IPoint point)
        {
            IActiveView activeView = this.axMapControl1.ActiveView;
            IGraphicsContainer graphicsContainer = axMapControl1.Map as IGraphicsContainer;
            //建立一个marker元素
            IMarkerElement markerElement = new MarkerElement() as IMarkerElement;
            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
            //符号化元素
            IRgbColor rgbColor1 = new RgbColor();
            rgbColor1.Red = 255;
            rgbColor1.Blue = 0;
            rgbColor1.Green = 0;
            simpleMarkerSymbol.Color = rgbColor1;
            IRgbColor rgbColor2 = new RgbColor();
            rgbColor2.Red = 0;
            rgbColor2.Blue = 255;
            rgbColor2.Green = 0;
            simpleMarkerSymbol.Outline = true;
            simpleMarkerSymbol.OutlineColor = rgbColor2 as IColor;
            simpleMarkerSymbol.OutlineSize = 1;
            simpleMarkerSymbol.Size = 5;
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            ISymbol symbol = simpleMarkerSymbol as ISymbol;
            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            markerElement.Symbol = simpleMarkerSymbol;
            m_element = markerElement as IElement;
            m_element.Geometry = point as IGeometry;
            graphicsContainer.AddElement(m_element, 0);
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, m_element, null);
            IGeometry geometry = m_element.Geometry;
            movePointFeedback.Display = activeView.ScreenDisplay;
            movePointFeedback.Symbol = simpleMarkerSymbol as ISymbol;
            movePointFeedback.Start(geometry as IPoint, point);
        }
        //移动元素到新的位置
        public void ElementMoveTo(IPoint point)
        {
            //移动元素
            movePointFeedback.MoveTo(point);
            IGeometry geometry1 = null;
            IGeometry geometry2 = null;
            if (m_element != null)
            {
                geometry1 = m_element.Geometry;
                geometry2 = movePointFeedback.Stop();
                m_element.Geometry = geometry2;
                //更新该元素的位置
                this.axMapControl1.ActiveView.GraphicsContainer.UpdateElement(m_element);
                //重新移动元素
                movePointFeedback.Stop();//(geometry1 as IPoint, point);
                this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }
        //通过名称获取图层
        public ILayer GetLayerByName(string layerName, AxMapControl axMapControl1)
        {
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                if (axMapControl1.get_Layer(i).Name.Equals(layerName))
                    return axMapControl1.get_Layer(i);
            }
            return null;
        }
        //转换像素到地图单位
        public double ConvertPixelsToMapUnits(IActiveView activeView, double pixelUnits)
        {
            double realDisplayExtent;
            int pixelExtent;
            double sizeOfOnePixel;
            pixelExtent = activeView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right - activeView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;
            realDisplayExtent = activeView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            sizeOfOnePixel = realDisplayExtent / pixelExtent;
            return pixelUnits * sizeOfOnePixel;
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

        //更改当前捕捉图层名
        private void cbLayerName_TextChanged(object sender, EventArgs e)
        {
            snapLayer = cbLayerName.Text;
        }

    }
}