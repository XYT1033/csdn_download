using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;

namespace FeatureEdit
{
    public partial class Form1 : Form
    {
        //操作类型
        string strOperator = "";
        //当前地图视图
        IActiveView m_activeView = null;
        //当前操作图层
        IFeatureLayer m_FeatureLayer = null;
        //当前操作实体
        IFeature m_Feature = null;
        //当前点移动反馈对象
        IMovePointFeedback m_MovePointFeedback;
        //当前线移动反馈对象
        IMoveLineFeedback m_MoveLineFeedback;
        //当前面移动反馈对象
        IMovePolygonFeedback m_MovePolygonFeedback;

        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.Engine);

            m_MovePointFeedback = new MovePointFeedbackClass();
            m_MoveLineFeedback = new MoveLineFeedbackClass();
            m_MovePolygonFeedback = new MovePolygonFeedbackClass();
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //加载地图文档
            loadMapDocument();
            //将图层名填加到下拉列表框
            for (int i = 0; i < this.axMapControl1.LayerCount; i++)
            {
                ILayer layer = this.axMapControl1.get_Layer(i);
                this.comboBox1.Items.Add(layer.Name);
            }
        }
        //选择操作图层，并让ＭＡＰ选种图层第一个实体，
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.Text != "")
            {
                for (int i = 0; i < this.axMapControl1.LayerCount; i++)
                {
                    ILayer layer = this.axMapControl1.get_Layer(i);
                    if (layer.Name == this.comboBox1.Text.ToString())
                    {
                        m_FeatureLayer = layer as IFeatureLayer;
                        m_Feature = m_FeatureLayer.FeatureClass.GetFeature(0);

                        if (m_Feature != null)
                        {
                            this.axMapControl1.Map.ClearSelection();
                            this.axMapControl1.Map.SelectFeature(m_FeatureLayer, m_Feature);
                            this.axMapControl1.Refresh();
                        }
                        m_activeView = this.axMapControl1.ActiveView;
                        return;
                    }
                }
            }
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
        //初始化反馈对象参数
        private void button3_Click(object sender, EventArgs e)
        {
            strOperator = "move";
            m_MovePointFeedback = new MovePointFeedbackClass();
            m_MoveLineFeedback = new MoveLineFeedbackClass();
            m_MovePolygonFeedback = new MovePolygonFeedbackClass();
        }
        //删除节点
        private void button4_Click(object sender, EventArgs e)
        {
            strOperator = "del";
            IPointCollection pointCollection;
            if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                pointCollection = new PolylineClass();
                IPolyline polyline = m_Feature.Shape as IPolyline;

                pointCollection = polyline as IPointCollection;
                //如果点个数少于两个无法构成线
                if (pointCollection.PointCount > 2)
                {
                    //移除指定的节点
                    pointCollection.RemovePoints(pointCollection.PointCount - 1, 1);
                }
            }
            else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                pointCollection = new PolygonClass();
                IPolygon polygon = m_Feature.Shape as IPolygon;
                pointCollection = polygon as IPointCollection;
                //如果点个数少于三个无法构成面
                if (pointCollection.PointCount > 3)
                {
                    //移除指定的节点
                    pointCollection.RemovePoints(pointCollection.PointCount - 1, 1);
                }
            }
            IWorkspaceEdit workspaceEdit;
            IWorkspace workspace;
            IDataset dataset = m_FeatureLayer.FeatureClass as IDataset;
            workspace = dataset.Workspace;
            workspaceEdit = workspace as IWorkspaceEdit;
            //开始编辑
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();
            //保存数据
            m_Feature.Store();
            //结束编辑
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);
            m_activeView.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            strOperator = "add";
            IPointCollection pointCollection;
            IPoint point = new PointClass();
            IPoint fromPoint = new PointClass();
            IPoint toPoint = new PointClass();
            if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                pointCollection = new PolylineClass();
                IPolyline polyline = m_Feature.Shape as IPolyline;

                object missing1 = Type.Missing;
                object missing2 = Type.Missing;
                pointCollection = polyline as IPointCollection;
                //获取线对象的最后两个点
                fromPoint = pointCollection.get_Point(pointCollection.PointCount - 2);
                toPoint = pointCollection.get_Point(pointCollection.PointCount - 1);
                //根据线最后两个点，创建一个新点
                point.PutCoords((fromPoint.X + toPoint.X) / 2+300, (fromPoint.Y + toPoint.Y) / 2 + 500);
                //将新点添加到线对象的点集合中
                pointCollection.AddPoint(point, ref missing1, ref missing2);

            }
            else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                pointCollection = new PolygonClass();
                IPolygon polygon = m_Feature.Shape as IPolygon;

                object missing1 = Type.Missing;
                object missing2 = Type.Missing;
                pointCollection = polygon as IPointCollection;
                //获取面对象点集最后两个点
                fromPoint = pointCollection.get_Point(pointCollection.PointCount - 2);
                toPoint = pointCollection.get_Point(pointCollection.PointCount - 1);
                //根据线最后两个点，创建一个新点
                point.PutCoords((fromPoint.X + toPoint.X) / 2, (fromPoint.Y + toPoint.Y) / 2 + 50);
                //将新点添加到线对象的点集合中
                pointCollection.AddPoint(point, ref missing1, ref missing2);
            }
            IWorkspaceEdit workspaceEdit;
            IWorkspace workspace;
            IDataset dataset = m_FeatureLayer.FeatureClass as IDataset;
            workspace = dataset.Workspace;
            workspaceEdit = workspace as IWorkspaceEdit;
            //开始编辑
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();
            //保存数据
            m_Feature.Store();
            //结束编辑
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);
            m_activeView.Refresh();
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IPoint point = new PointClass();
            IFeatureClass featureClass = null;
            if (m_Feature == null) return;
            switch (strOperator)
            {
                case "move":
                    //将当前鼠标位置的点转换为地图上的坐标
                    point = m_activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                    if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        //设置显示对象，并启动移动
                        m_MovePointFeedback.Display = m_activeView.ScreenDisplay;
                        m_MovePointFeedback.Start(m_Feature.Shape as IPoint, point);
                    }
                    else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        //设置显示对象，并启动移动
                        m_MoveLineFeedback.Display = m_activeView.ScreenDisplay;
                        m_MoveLineFeedback.Start(m_Feature.Shape as IPolyline, point);
                    }
                    else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        //设置显示对象，并启动移动
                        m_MovePolygonFeedback.Display = m_activeView.ScreenDisplay;
                        m_MovePolygonFeedback.Start(m_Feature.Shape as IPolygon, point);
                    }
                    break;
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            IPoint point = new PointClass();
            switch (strOperator)
            {
                case "move":
                    if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        if (m_MovePointFeedback != null)
                        {
                            //将当前鼠标位置的点转换为地图上的坐标
                            point = m_activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                            //移动对象到当前鼠标位置
                            m_MovePointFeedback.MoveTo(point);
                        }
                    }
                    else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        if (m_MoveLineFeedback != null)
                        {
                            //将当前鼠标位置的点转换为地图上的坐标
                            point = m_activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                            //移动对象到当前鼠标位置
                            m_MoveLineFeedback.MoveTo(point);
                        }
                    }
                    else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        if (m_MovePolygonFeedback != null)
                        {
                            //将当前鼠标位置的点转换为地图上的坐标
                            point = m_activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                            //移动对象到当前鼠标位置
                            m_MovePolygonFeedback.MoveTo(point);
                        }
                    }
                    break;
            }
        }

        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (m_Feature == null) return;
            IGeometry resultGeometry = null;
            switch (strOperator)
            {
                case "move":
                    if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        //停止移动
                        resultGeometry = m_MovePointFeedback.Stop() as IGeometry;
                        m_Feature.Shape = resultGeometry;
                    }
                    else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        //停止移动
                        resultGeometry = m_MoveLineFeedback.Stop() as IGeometry;
                        m_Feature.Shape = resultGeometry;
                    }
                    else if (m_Feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        //停止移动
                        resultGeometry = m_MovePolygonFeedback.Stop() as IGeometry;
                        m_Feature.Shape = resultGeometry;
                    }
                    IWorkspaceEdit workspaceEdit;
                    IWorkspace workspace;
                    IDataset dataset = m_FeatureLayer.FeatureClass as IDataset;
                    workspace = dataset.Workspace;
                    workspaceEdit = workspace as IWorkspaceEdit;
                    //开始编辑
                    workspaceEdit.StartEditing(true);
                    workspaceEdit.StartEditOperation();
                    //保存实体
                    m_Feature.Store();
                    //结束编辑
                    workspaceEdit.StopEditOperation();
                    workspaceEdit.StopEditing(true);
                    m_MovePointFeedback = null;
                    m_MoveLineFeedback = null;
                    m_MovePolygonFeedback = null;
                    break;
            }
            m_activeView.Refresh();
            this.axMapControl1.Map.ClearSelection();
        }

        private void axMapControl1_OnMouseUp_1(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


  

    
    }
}