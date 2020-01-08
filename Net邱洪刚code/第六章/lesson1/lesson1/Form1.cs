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
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;

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
            //IAoInitialize pao = new AoInitializeClass();
            //pao.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);

        }
        //　工作空间
        IWorkspace workspace;
        //矢量数据工作空间
        IFeatureWorkspace featureWorkspace;
        //影像数据工作空间
        IRasterWorkspaceEx rasterWorkspace;
        //矢量数据集
        IFeatureDataset featureDataset;
        //影像数据集
        IRasterDataset rasterDataset;
        private void button1_Click(object sender, EventArgs e)
        {
            // ＳＤＥ空间连接属性
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("server", this.textBox1.Text);
            propertySet.SetProperty("instance", this.textBox2.Text);
            propertySet.SetProperty("database", this.textBox3.Text);
            propertySet.SetProperty("user", this.textBox4.Text);
            propertySet.SetProperty("password", this.textBox5.Text);
            propertySet.SetProperty("version", "SDE.DEFAULT");
            IWorkspaceFactory workspaceFactory = new SdeWorkspaceFactory();
            //打开ＳＤＥ工作空间

            workspace = workspaceFactory.Open(propertySet, 0);
            MessageBox.Show("连接ＳＤＥ空间数据库成功");

        }
        //创建数据集（矢量数据集和影像数据集）
        private void button2_Click(object sender, EventArgs e)
        {
            featureWorkspace = workspace as IFeatureWorkspace;
            rasterWorkspace = workspace as IRasterWorkspaceEx;
            //定义空间参考
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954);
            spatialReference.SetDomain(-1000, -1000, 1000, 1000);

            IEnumDatasetName enumDatasetName;
            IDatasetName datasetName;
            string dsName = "";
            enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
            datasetName = enumDatasetName.Next();
            bool isExist = false;
            //创建矢量数据集
            dsName = "SDE." + this.textBox6.Text;
            while (datasetName != null)
            {
                if (datasetName.Name == dsName)
                {
                    isExist = true;
                }
                datasetName = enumDatasetName.Next();
            }
            if (isExist == false)
            {
                featureDataset = featureWorkspace.CreateFeatureDataset(this.textBox6.Text, spatialReference);
            }
            //创建影像数据集
            isExist = false;
            enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
            datasetName = enumDatasetName.Next();

            dsName = "SDE." + this.textBox6.Text;
            while (datasetName != null)
            {
                if (datasetName.Name == dsName)
                {
                    isExist = true;
                }
                datasetName = enumDatasetName.Next();
            }
            if (isExist == false)
            {
                //设置存储参数
                IRasterStorageDef rasterStorageDef = new RasterStorageDefClass();
                rasterStorageDef.CompressionType = esriRasterCompressionType.esriRasterCompressionUncompressed;
                rasterStorageDef.PyramidLevel = 1;
                rasterStorageDef.PyramidResampleType = rstResamplingTypes.RSP_BilinearInterpolation;
                rasterStorageDef.TileHeight = 128;
                rasterStorageDef.TileWidth = 128;
                //设置坐标系统
                IRasterDef rasterDef = new RasterDefClass();
                ISpatialReference rasterDpatialRefrence = new UnknownCoordinateSystemClass();
                rasterDef.SpatialReference = rasterDpatialRefrence;

                IGeometryDef geometryDef = new GeometryDefClass();
                IGeometryDefEdit geometryDefedit = (IGeometryDefEdit)geometryDef;
                geometryDefedit.AvgNumPoints_2 = 5;
                geometryDefedit.GridCount_2 = 1;
                geometryDefedit.set_GridSize(0, 1000);
                geometryDefedit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                ISpatialReference spatialReference2 = new UnknownCoordinateSystemClass();
                geometryDefedit.SpatialReference_2 = spatialReference2;
                rasterDataset = rasterWorkspace.CreateRasterDataset(this.textBox7.Text, 1, rstPixelType.PT_LONG, rasterStorageDef, "DEFAULTS", rasterDef, geometryDef);

            }

        }
        //加载矢量数据到ＳＤＥ数据库
        private void button3_Click(object sender, EventArgs e)
        {
            featureWorkspace = workspace as IFeatureWorkspace;
            this.openFileDialog1.Filter = "shp file (*.shp)|*.shp";
            this.openFileDialog1.Title = "打开矢量数据";
            this.openFileDialog1.Multiselect = false;
            string fileName = "";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                string filepath;
                string file;
                int lastIndex;
                lastIndex = fileName.LastIndexOf(@"\");
                filepath = fileName.Substring(0, lastIndex);
                file = fileName.Substring(lastIndex + 1);
                //读取SHP数据
                IWorkspaceFactory shpwpf = new ShapefileWorkspaceFactoryClass();
                IWorkspace shpwp = shpwpf.OpenFromFile(filepath, 0);
                IFeatureWorkspace shpfwp = shpwp as IFeatureWorkspace;
                IFeatureClass shpfc = shpfwp.OpenFeatureClass(file);

                //导入SDE数据库 
                IFeatureClass sdeFeatureClass = null;
                IFeatureClassDescription featureClassDescription = new FeatureClassDescriptionClass();
                IObjectClassDescription objectClassDescription = featureClassDescription as IObjectClassDescription;
                IFields fields = shpfc.Fields;
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IEnumFieldError enumFieldError = null;
                IFields validateFields = null;
                fieldChecker.ValidateWorkspace = featureWorkspace as IWorkspace;
                fieldChecker.Validate(fields, out enumFieldError, out validateFields);
                featureDataset = featureWorkspace.OpenFeatureDataset(this.textBox6.Text);
                try
                {
                    sdeFeatureClass = featureWorkspace.OpenFeatureClass(shpfc.AliasName);
                }
                catch (Exception ex)
                {
                }
                //在ＳＤＥ数据库中创建矢量数据集
                if (sdeFeatureClass == null)
                {
                    sdeFeatureClass = featureDataset.CreateFeatureClass(shpfc.AliasName, validateFields, objectClassDescription.InstanceCLSID, objectClassDescription.ClassExtensionCLSID, shpfc.FeatureType, shpfc.ShapeFieldName, "");
                }
                IFeatureCursor featureCursor = shpfc.Search(null, true);
                IFeature feature = featureCursor.NextFeature();
                IFeatureCursor sdeFeatureCursor = sdeFeatureClass.Insert(true);
                IFeatureBuffer sdeFeatureBuffer;
                //添加实体对象
                while (feature != null)
                {
                    sdeFeatureBuffer = sdeFeatureClass.CreateFeatureBuffer();
                    IField shpField = new FieldClass();
                    IFields shpFields = feature.Fields;
                    for (int i = 0; i < shpFields.FieldCount; i++)
                    {
                        shpField = shpFields.get_Field(i);
                        int index = sdeFeatureBuffer.Fields.FindField(shpField.Name);
                        if (index != -1)
                        {
                            sdeFeatureBuffer.set_Value(index, feature.get_Value(i));
                        }
                    }
                    sdeFeatureCursor.InsertFeature(sdeFeatureBuffer);
                    sdeFeatureCursor.Flush();
                    feature = featureCursor.NextFeature();
                }
                //加载数据到Mapcontrol
                IFeatureLayer sdeFeatureLayer = new FeatureLayerClass();
                sdeFeatureLayer.FeatureClass = sdeFeatureClass;
                this.axMapControl1.Map.AddLayer(sdeFeatureLayer as ILayer);
                this.axMapControl1.Extent = this.axMapControl1.FullExtent;
                this.axMapControl1.Refresh();
            }

        }
        //加载影像数据到ＳＤＥ数据库
        private void button4_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "TIFF file (*.tif)|*.tif";
            this.openFileDialog1.Title = "打开影像数据";
            this.openFileDialog1.Multiselect = false;
            string fileName = "";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                string filepath;
                string file;
                int lastIndex;
                lastIndex = fileName.LastIndexOf(@"\");
                filepath = fileName.Substring(0, lastIndex);
                file = fileName.Substring(lastIndex + 1);

                //导入SDE数据库 
                rasterWorkspace = workspace as IRasterWorkspaceEx;
                IWorkspaceFactory tifwpf = new RasterWorkspaceFactoryClass();
                IWorkspace tifwp = tifwpf.OpenFromFile(filepath, 0);
                IRasterWorkspace tifrwp = tifwp as IRasterWorkspace;
                IRasterDataset rasterDataset = tifrwp.OpenRasterDataset(file);
                IRasterDataset sdeRasterDataset = null;
                lastIndex = file.LastIndexOf(@".");
                file = file.Substring(0, lastIndex);
                try
                {
                    sdeRasterDataset = rasterWorkspace.OpenRasterDataset(file);
                }
                catch (Exception Ex)
                {
                }
                if (sdeRasterDataset == null)
                {
                    IGeoDataset geoDataset = rasterDataset as IGeoDataset;
                    IRasterSdeServerOperation rasterSdeServeroperation;

                    IBasicRasterSdeConnection sdeCon = new BasicRasterSdeLoader();
                    IPropertySet propertySet = new PropertySetClass();
                    propertySet = workspace.ConnectionProperties;
                    //建立与ＳＤＥ数据库的连接
                    sdeCon.ServerName = propertySet.GetProperty("server").ToString();
                    sdeCon.Instance = propertySet.GetProperty("instance").ToString();
                    sdeCon.UserName = propertySet.GetProperty("user").ToString();
                    sdeCon.Password = "sde";
                    sdeCon.Database = propertySet.GetProperty("database").ToString();
                    sdeCon.SdeRasterName = file;
                    sdeCon.InputRasterName = fileName;
                    rasterSdeServeroperation = sdeCon as IRasterSdeServerOperation;
                    //保存影像数据到ＳＤＥ数据库中
                    rasterSdeServeroperation.Create();
                    rasterSdeServeroperation.Update();
                    rasterSdeServeroperation.ComputeStatistics();
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    sdeRasterDataset = rasterWorkspace.OpenRasterDataset(file);
                    rasterLayer.CreateFromDataset(sdeRasterDataset);
                    this.axMapControl1.Map.AddLayer(rasterLayer as ILayer);
                    this.axMapControl1.Extent = this.axMapControl1.FullExtent;
                    this.axMapControl1.Refresh();
                }
            }
        }

    }
}