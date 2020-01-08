using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using System.Diagnostics.Contracts;


using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace ControlSample1
{
    public partial class ControlSample1 : Form
    {

        private int flagSelect = 0;
        private int flag = 0;
        public ControlSample1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.Engine); 
            InitializeComponent();
             
        }
        private void ControlSample1_Load(object sender, EventArgs e)
        {
            //loadEagleEyeDocument();
            this.label1.Visible = false;
            this.textBox1.Visible = false;
        }
        IMapDocument mapDocument;
        //*******************************************************************************************************
        private void 加载地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
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
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                if (axMapControl1.CheckMxFile(filePath))
                {
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                    axMapControl1.LoadMxFile(filePath, 0, Type.Missing);
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                    loadEagleEyeDocument(filePath);
                    axMapControl1.Extent = axMapControl1.FullExtent;
                }
                else
                {
                    MessageBox.Show(filePath + "不是有效的地图文档");
                }
            }
        }
        //加载地图文档中的特定地图
        private void loadMapDocument2()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开地图文档";
            openFileDialog.Filter = "map documents(*.mxd)|*.mxd";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                if (axMapControl1.CheckMxFile(filePath))
                {
                    IArray arrayMap = axMapControl1.ReadMxMaps(filePath, Type.Missing);
                    int i;
                    IMap map;
                    for (i = 0; i < arrayMap.Count; i++)
                    {
                        map = arrayMap.get_Element(i) as IMap;
                        if (map.Name == "Layers")
                        {
                            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                            axMapControl1.LoadMxFile(filePath, 0, Type.Missing);
                            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                            loadEagleEyeDocument(filePath);
                            axMapControl1.Extent = axMapControl1.FullExtent;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(filePath + "不是有效的地图文档");
                }
            }
        }
        //加载地图文档
        private void loadMapDoc()
        {
            mapDocument = new ESRI.ArcGIS.Carto.MapDocumentClass();
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog;
                openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "打开地图文档";
                openFileDialog.Filter = "map documents(*.mxd)|*.mxd";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    mapDocument.Open(filePath, "");
                    for (int i = 0; i < mapDocument.MapCount; i++)
                    {
                        axMapControl1.Map = mapDocument.get_Map(i);
                    }
                    axMapControl1.Refresh();
                }
                else
                {
                    mapDocument = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        //保存地图文档
        private void saveDocument()
        {
            if (mapDocument == null)
            {
                MessageBox.Show("地图文档对象为空，请先加载地图文档");

            }
            else
            {
                if (mapDocument.get_IsReadOnly(mapDocument.DocumentFilename) == true)
                {
                    MessageBox.Show("地图文档是只读的无法保存");
                }
                else
                {
                    string fileSavePath = @"E:\World\newworld1.mxd";
                    try
                    {
                        mapDocument.Save(mapDocument.UsesRelativePaths, true);
                        MessageBox.Show("保存地图文档成功");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("保存地图文档失败！！！" + e.ToString());
                    }
                }
            }
        }
        private void saveAsDocument()
        {
            if (mapDocument == null)
            {
                MessageBox.Show("地图文档对象为空，请先加载地图文档");

            }
            else
            {
                if (mapDocument.get_IsReadOnly(mapDocument.DocumentFilename) == true)
                {
                    MessageBox.Show("地图文档是只读的无法保存");
                }
                else
                {
                    string fileSavePath = @"E:\World\newword2.mxd";
                    try
                    {
                        mapDocument.SaveAs(fileSavePath, true, true);
                        MessageBox.Show("另存地图文档成功");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("另存地图文档失败！！！" + e.ToString());
                    }
                }
            }
           
        }


        private void 加载特定地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMapDocument2();
        }

        private void 保存地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMapDoc();
            saveDocument();
        }

        private void 另存地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMapDoc();
            saveAsDocument();
        }
        //*******************************************************************************************************
        //图层操作相关功能
        //添加图层文件
        private void addLayerFile()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开图层文件";
            openFileDialog.Filter = "map documents(*.lyr)|*.lyr";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    axMapControl1.AddLayerFromFile(filePath);
                }
                catch (Exception e)
                {
                    MessageBox.Show("添加图层失败！！！" + e.ToString());
                }
            }
        }
        //添加shape文件
        //10版本中addshapefile方法执行的程序中，在程序启动时候加入如下语句：
        // ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.Engine); 
        //或者在程序界面上拖入一个LicenseControl 控件
        private void addShapeFile()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开图层文件";
            openFileDialog.Filter = "map documents(*.shp)|*.shp";
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                String path= fileInfo.Directory.ToString();
                String fileName = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));
                try
                {
                    axMapControl1.AddShapeFile(path, fileName);
                 }
                catch (Exception e)
                {
                     MessageBox.Show("添加图层失败！！！" + e.ToString());
                }
            }
        }
        //删除图层
        private void deleteLayer()
        {
            try
            {
                //删除地图中所有的图层
                for (int i = axMapControl1.LayerCount - 1; i >= 0; i--)
                {
                    axMapControl1.DeleteLayer(i);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("删除图层失败！！！" + e.ToString());
            }
        }
        //移动图层
        private void moveLayer()
        {
            if (axMapControl1.LayerCount > 0)
            {
                try
                {
                    //将最下层图层文件移动到最上层
                    axMapControl1.MoveLayerTo(axMapControl1.LayerCount - 1, 0);
                }
                catch (Exception e)
                {
                    MessageBox.Show("移动图层失败！！！" + e.ToString());
                }
            }
        }


        private void 添加图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLayerFile();
        }

        private void 添加SHP文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addShapeFile();
        }

        private void 删除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteLayer();
        }

        private void 移动图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveLayer();
        }

        //*******************************************************************************************************
        //图形绘制功能区
 
        private void 画线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 1;
        }

        private void 绘制矩形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 3;
        }

        private void 绘制文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 5;
        }

        private void 绘制圆形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 2;
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            IGeometry geometry = null;
            if (flag != 0)
            {
                //绘制图形
                switch (flag)
                {
                    case 1:
                        geometry = this.axMapControl1.TrackLine();
                        break;
                    case 2:
                        geometry = this.axMapControl1.TrackCircle();
                        break;
                    case 3:
                        geometry = this.axMapControl1.TrackRectangle();
                        break;
                    case 5:
                        IPoint point = new PointClass();
                        point.X = e.mapX;
                        point.Y = e.mapY;
                        geometry = point as IGeometry;
                        break;
                    default:
                        flag = 0;
                        break;
                }

                if (flag == 1 || flag == 2 || flag == 3)
                {
                    drawMapShape(geometry);
                }
                if (flag == 5)
                {
                    drawMapText(geometry);
                }


                //axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
                //this.axMapControl1.Refresh();
            }
            else if (flagSelect != 0)
            {
                //空间查询
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
                IGeometry selectGeo = null;
                ISelection selection;
                switch (flagSelect)
                {
                    case 1:
                        IPoint point = new PointClass();
                        point.X = e.mapX;
                        point.Y = e.mapY;
                        selectGeo = point as IGeometry;
                        break;
                    case 2:
                        selectGeo=axMapControl1.TrackRectangle ();
                        break;
                    case 3:
                        selectGeo = axMapControl1.TrackCircle();
                        break;
                    case 4:
                        selectGeo=axMapControl1.TrackPolygon();
                        break;
                    default :
                        flagSelect=0;
                        break;
                }
                if (null != selectGeo)
                {
                    axMapControl1.Map.SelectByShape(selectGeo, null, false);
                    axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                }

            }
        }
        //绘制文本对象
        private void drawMapText(IGeometry geometry)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = 255;
            color.Blue = 0;
            color.Green = 0;
            ITextSymbol txtSymbol = new TextSymbolClass();
            txtSymbol.Color = color;
            object symbol = txtSymbol;
            this.axMapControl1.DrawText(geometry, "测试DRAW TEXT ", ref symbol);            
        }
        //绘制线、圆、矩形
        private void drawMapShape(IGeometry geometry )
        {
            IRgbColor rgbColor;
            rgbColor = new RgbColorClass();
            rgbColor.Red = 255;
            rgbColor.Green = 255;
            rgbColor.Blue = 0;
            object symbol = null;
            if (geometry.GeometryType == esriGeometryType.esriGeometryPolyline ||
                geometry.GeometryType == esriGeometryType.esriGeometryLine)
            {
                ISimpleLineSymbol simpleLineSymbol;
                simpleLineSymbol = new SimpleLineSymbolClass();
                simpleLineSymbol.Color = rgbColor;
                simpleLineSymbol.Width = 5;
                symbol = simpleLineSymbol;
            }
            else
            {
                ISimpleFillSymbol simpleFillSymbol;
                simpleFillSymbol = new SimpleFillSymbolClass();
                simpleFillSymbol.Color = rgbColor;
                symbol = simpleFillSymbol;
            }
            axMapControl1.DrawShape(geometry, ref symbol);
        } 



        //*******************************************************************************************************
        //鹰眼图功能代码 E:\World\World.mxd
        private void loadEagleEyeDocument(String filePath)
        {
            if (axMapControl2.CheckMxFile(filePath))
            {
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerHourglass;
                axMapControl2.LoadMxFile(filePath, 0, Type.Missing);
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerDefault;
                setLoadEagle();
            }
            else
            {
                MessageBox.Show(filePath + "不是有效的地图文档");
            }
        }
        //设置鹰眼图的显示范围中心点等
        private void setLoadEagle()
        {
            axMapControl2.MapScale = this.axMapControl1.MapScale * 2.0;
            IPoint point = new PointClass();
            point.X = (this.axMapControl1.Extent.XMax + this.axMapControl1.Extent.XMin) / 2;
            point.Y = (this.axMapControl1.Extent.YMax + this.axMapControl1.Extent.YMin) / 2;
            axMapControl2.ShowScrollbars = false;
            axMapControl2.CenterAt(point);
            axMapControl2.Refresh();
        }
        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            setLoadEagle();
        }


        //*******************************************************************************************************
        //选择查询功能代码
       
        private void 点选查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagSelect = 1;
        }

        private void 矩形查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagSelect = 2;
        }

        private void 圆形查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagSelect = 3;
        }

        private void 多边形查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagSelect = 4;
        }

        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //清除选择
            //在这里需要特别注意，在清除选择集前必须做一个刷新，否则在视图上看不到清除的效果
            IActiveView pActiveView = (IActiveView)(axMapControl1.Map);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, axMapControl1.get_Layer(0), null);
            axMapControl1.Map.ClearSelection();
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, axMapControl1.get_Layer(0), null);

        }

        private void 名称查询_Click(object sender, EventArgs e)
        {
            this.label1.Visible = true;
            this.textBox1.Visible = true;
        }
        //名称查询
        private void searchByName()
        {
            
            string searchName = this.textBox1.Text.Trim();
            if (null != searchName && searchName.Length > 1)
            {
                ILayer layer = axMapControl1.Map.get_Layer(1);
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IQueryFilter queryFilter = new QueryFilterClass();
                IFeatureCursor featureCursor;
                IFeature feature = null;
                queryFilter.WhereClause = "continent like '%" + searchName + "%'";
                featureCursor = featureClass.Search(queryFilter, true);
                feature = featureCursor.NextFeature();
                if (feature != null)
                {
                    axMapControl1.Map.SelectFeature(axMapControl1.get_Layer(1), feature);
                    axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            searchByName();
        }






        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
        
        }
        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {

        }


    }

}
