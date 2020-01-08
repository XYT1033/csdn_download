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
        //获取颜色对象
        private IRgbColor getRGB(int r, int g, int b)
        {
            IRgbColor pColor;
            pColor = new RgbColorClass();
            pColor.Red = r;
            pColor.Green = g;
            pColor.Blue = b;
            return pColor;
        }
        private IGeoFeatureLayer getGeoLayer(string layerName)
        {
            ILayer layer;
            IGeoFeatureLayer geoFeatureLayer;
            for (int i = 0; i < this.axMapControl1.LayerCount; i++)
            {
                layer = this.axMapControl1.get_Layer(i);
                if (layer != null && layer.Name == layerName)
                {
                    geoFeatureLayer = layer as IGeoFeatureLayer;
                    return geoFeatureLayer;
                }
            }
            return null;
        }
        //简单渲染专题图
        private void button1_Click(object sender, EventArgs e)
        {
            //简单填充符号
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            simpleFillSymbol.Color = getRGB(255, 0, 0);
            //创建边线符号
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            simpleLineSymbol.Color = getRGB(0, 255, 0);
            ISymbol symbol = simpleLineSymbol as ISymbol;
            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            simpleFillSymbol.Outline = simpleLineSymbol;

            ISimpleRenderer simpleRender = new SimpleRendererClass();
            simpleRender.Symbol = simpleFillSymbol as ISymbol;
            simpleRender.Label = "continent";
            simpleRender.Description = "简单渲染";

            IGeoFeatureLayer geoFeatureLayer;
            geoFeatureLayer = getGeoLayer("Continents");
            if (geoFeatureLayer != null)
            {
                geoFeatureLayer.Renderer = simpleRender as IFeatureRenderer;
            }
            this.axMapControl1.Refresh();
        }
        //创建颜色带
        private IColorRamp CreateAlgorithmicColorRamp(int count)
        {
            //创建一个新AlgorithmicColorRampClass对象
            IAlgorithmicColorRamp algColorRamp = new AlgorithmicColorRampClass();
            IRgbColor fromColor = new RgbColorClass();
            IRgbColor toColor = new RgbColorClass();
            //创建起始颜色对象
            fromColor.Red = 255;
            fromColor.Green = 0;
            fromColor.Blue = 0;
            //创建终止颜色对象           
            toColor.Red = 0;
            toColor.Green = 0;
            toColor.Blue = 255;
            //设置AlgorithmicColorRampClass的起止颜色属性
            algColorRamp.ToColor = fromColor;
            algColorRamp.FromColor = toColor;
            //设置梯度类型
            algColorRamp.Algorithm = esriColorRampAlgorithm.esriCIELabAlgorithm;
            //设置颜色带颜色数量
            algColorRamp.Size = count;
            //创建颜色带
            bool bture = true;
            algColorRamp.CreateRamp(out bture);
            return algColorRamp;

        }
        //分级专题图
        private void button2_Click(object sender, EventArgs e)
        {
            int classCount = 6;
            ITableHistogram tableHistogram;
            IBasicHistogram basicHistogram;
            ITable table;
            IGeoFeatureLayer geoFeatureLayer;
            geoFeatureLayer = getGeoLayer("Continents");
            ILayer layer = geoFeatureLayer as ILayer;
            table = layer as ITable;
            tableHistogram = new BasicTableHistogramClass();
            //按照 数值字段分级
            tableHistogram.Table = table;
            tableHistogram.Field = "sqmi";
            basicHistogram = tableHistogram as IBasicHistogram;
            object values;
            object frequencys;
            //先统计每个值和各个值出现的次数
            basicHistogram.GetHistogram(out values, out frequencys);
            //创建平均分级对象
            IClassifyGEN classifyGEN = new QuantileClass();
            //用统计结果进行分级 ，级别数目为classCount
            classifyGEN.Classify(values, frequencys, ref classCount);
            //获得分级结果,是个 双精度类型数组 
            double[] classes;
            classes = classifyGEN.ClassBreaks as double[];

            IEnumColors enumColors = CreateAlgorithmicColorRamp(classes.Length).Colors;
            IColor color;

            IClassBreaksRenderer classBreaksRenderer = new ClassBreaksRendererClass();
            classBreaksRenderer.Field = "sqmi";
            classBreaksRenderer.BreakCount = classCount;
            classBreaksRenderer.SortClassesAscending = true;

            ISimpleFillSymbol simpleFillSymbol;
            for (int i = 0; i < classes.Length - 1; i++)
            {
                color = enumColors.Next();
                simpleFillSymbol = new SimpleFillSymbolClass();
                simpleFillSymbol.Color = color;
                simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;

                classBreaksRenderer.set_Symbol(i, simpleFillSymbol as ISymbol);
                classBreaksRenderer.set_Break(i, classes[i]);

            }

            if (geoFeatureLayer != null)
            {
                geoFeatureLayer.Renderer = classBreaksRenderer as IFeatureRenderer;
            }

            this.axMapControl1.ActiveView.Refresh();
        }
        //单一值专题图
        private void button3_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer geoFeatureLayer;
            geoFeatureLayer = getGeoLayer("Continents");
            IUniqueValueRenderer uniqueValueRenderer = new UniqueValueRendererClass();
            uniqueValueRenderer.FieldCount = 1;
            uniqueValueRenderer.set_Field(0, "continent");

            //简单填充符号
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;

            IFeatureCursor featureCursor = geoFeatureLayer.FeatureClass.Search(null, false);
            IFeature feature;

            if (featureCursor != null)
            {
                IEnumColors enumColors = CreateAlgorithmicColorRamp(8).Colors;
                int fieldIndex = geoFeatureLayer.FeatureClass.Fields.FindField("continent");
                for (int i = 0; i < 8; i++)
                {
                    feature = featureCursor.NextFeature();
                    string nameValue = feature.get_Value(fieldIndex).ToString();
                    simpleFillSymbol = new SimpleFillSymbolClass();
                    simpleFillSymbol.Color = enumColors.Next();
                    uniqueValueRenderer.AddValue(nameValue, "continent", simpleFillSymbol as ISymbol);
                }
            }

            geoFeatureLayer.Renderer = uniqueValueRenderer as IFeatureRenderer;
            this.axMapControl1.Refresh();
        }
        //ProportionalSymbolRenderer
        private void button4_Click(object sender, EventArgs e)
        {

            IGeoFeatureLayer geoFeatureLayer;
            IFeatureLayer featureLayer;
            IProportionalSymbolRenderer proportionalSymbolRenderer;
            ITable table;
            ICursor cursor;
            IDataStatistics dataStatistics;
            IStatisticsResults statisticsResult;
            stdole.IFontDisp fontDisp;

            geoFeatureLayer = getGeoLayer("Continents");
            featureLayer = geoFeatureLayer as IFeatureLayer;
            table = geoFeatureLayer as ITable;
            cursor = table.Search(null, true);
            dataStatistics = new DataStatisticsClass();
            dataStatistics.Cursor = cursor;
            dataStatistics.Field = "sqmi";
            statisticsResult = dataStatistics.Statistics;
            if (statisticsResult != null)
            {
                IFillSymbol fillSymbol = new SimpleFillSymbolClass();
                fillSymbol.Color = getRGB(0, 255, 0);
                ICharacterMarkerSymbol characterMarkerSymbol = new CharacterMarkerSymbolClass();
                fontDisp = new stdole.StdFontClass() as stdole.IFontDisp;
                fontDisp.Name = "arial";
                fontDisp.Size = 20;
                characterMarkerSymbol.Font = fontDisp;
                characterMarkerSymbol.CharacterIndex = 90;
                characterMarkerSymbol.Color = getRGB(255, 0, 0);
                characterMarkerSymbol.Size = 8;
                proportionalSymbolRenderer = new ProportionalSymbolRendererClass();
                proportionalSymbolRenderer.ValueUnit = esriUnits.esriUnknownUnits;
                proportionalSymbolRenderer.Field = "sqmi";
                proportionalSymbolRenderer.FlanneryCompensation = false;
                proportionalSymbolRenderer.MinDataValue = statisticsResult.Minimum;
                proportionalSymbolRenderer.MaxDataValue = statisticsResult.Maximum;
                proportionalSymbolRenderer.BackgroundSymbol = fillSymbol;
                proportionalSymbolRenderer.MinSymbol = characterMarkerSymbol as ISymbol;
                proportionalSymbolRenderer.LegendSymbolCount = 10;
                proportionalSymbolRenderer.CreateLegendSymbols();
                geoFeatureLayer.Renderer = proportionalSymbolRenderer as IFeatureRenderer;
            }
            this.axMapControl1.Refresh();
        }
        //BarChartSymbol
        private void button5_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer geoFeatureLayer;
            IFeatureLayer featureLayer;
            ITable table;
            ICursor cursor;
            IRowBuffer rowBuffer;
            //设置渲染要素
            string field1 = "sqmi";
            string field2 = "sqkm";
            //获取渲染图层
            geoFeatureLayer = getGeoLayer("Continents");
            featureLayer = geoFeatureLayer as IFeatureLayer;
            table = featureLayer as ITable;
            geoFeatureLayer.ScaleSymbols = true;
            IChartRenderer chartRenderer = new ChartRendererClass();
            IRendererFields rendererFields = chartRenderer as IRendererFields;
            rendererFields.AddField(field1, field1);
            rendererFields.AddField(field2, field2);
            int[] fieldIndexs = new int[2];
            fieldIndexs[0] = table.FindField(field1);
            fieldIndexs[1] = table.FindField(field2);
            //获取要素最大值
            double fieldValue = 0.0, maxValue = 0.0;
            cursor = table.Search(null, true);
            rowBuffer = cursor.NextRow();
            while (rowBuffer != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    fieldValue = double.Parse(rowBuffer.get_Value(fieldIndexs[i]).ToString());
                    if (fieldValue > maxValue)
                    {
                        maxValue = fieldValue;
                    }
                }
                rowBuffer = cursor.NextRow();
            }
            //创建水平排列符号
            IBarChartSymbol barChartSymbol = new BarChartSymbolClass();
            barChartSymbol.Width = 10;
            IMarkerSymbol markerSymbol = barChartSymbol as IMarkerSymbol;
            markerSymbol.Size = 50;
            IChartSymbol chartSymbol = barChartSymbol as IChartSymbol;
            chartSymbol.MaxValue = maxValue;
            //添加渲染符号
            ISymbolArray symbolArray = barChartSymbol as ISymbolArray;
            IFillSymbol fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(255, 0, 0);
            symbolArray.AddSymbol(fillSymbol as ISymbol);
            fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(0, 255, 0);
            symbolArray.AddSymbol(fillSymbol as ISymbol);
            //设置柱状图符号
            chartRenderer.ChartSymbol = barChartSymbol as IChartSymbol;
            fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(0, 0, 255);
            chartRenderer.BaseSymbol = fillSymbol as ISymbol;
            chartRenderer.UseOverposter = false;
            //创建图例
            chartRenderer.CreateLegend();
            geoFeatureLayer.Renderer = chartRenderer as IFeatureRenderer;
            this.axMapControl1.Refresh();
        }
        //StackedChartSymbol
        private void button6_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer geoFeatureLayer;
            IFeatureLayer featureLayer;
            ITable table;
            ICursor cursor;
            IRowBuffer rowBuffer;
            //设置渲染要素
            string field1 = "sqmi";
            string field2 = "sqkm";
            //获取渲染图层
            geoFeatureLayer = getGeoLayer("Continents");
            featureLayer = geoFeatureLayer as IFeatureLayer;
            table = featureLayer as ITable;
            geoFeatureLayer.ScaleSymbols = true;
            IChartRenderer chartRenderer = new ChartRendererClass();
            IRendererFields rendererFields = chartRenderer as IRendererFields;
            rendererFields.AddField(field1, field1);
            rendererFields.AddField(field2, field2);
            int[] fieldIndexs = new int[2];
            fieldIndexs[0] = table.FindField(field1);
            fieldIndexs[1] = table.FindField(field2);
            //获取要素最大值
            double fieldValue = 0.0, maxValue = 0.0;
            cursor = table.Search(null, true);
            rowBuffer = cursor.NextRow();
            while (rowBuffer != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    fieldValue = double.Parse(rowBuffer.get_Value(fieldIndexs[i]).ToString());
                    if (fieldValue > maxValue)
                    {
                        maxValue = fieldValue;
                    }
                }
                rowBuffer = cursor.NextRow();
            }
            //创建累积排列符号
            IStackedChartSymbol stackedChartSymbol = new StackedChartSymbolClass();

            stackedChartSymbol.Width = 10;
            IMarkerSymbol markerSymbol = stackedChartSymbol as IMarkerSymbol;
            markerSymbol.Size = 50;
            IChartSymbol chartSymbol = stackedChartSymbol as IChartSymbol;
            chartSymbol.MaxValue = maxValue;
            //添加渲染符号
            ISymbolArray symbolArray = stackedChartSymbol as ISymbolArray;
            IFillSymbol fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(255, 0, 0);
            symbolArray.AddSymbol(fillSymbol as ISymbol);
            fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(0, 255, 0);
            symbolArray.AddSymbol(fillSymbol as ISymbol);
            //设置柱状图符号
            chartRenderer.ChartSymbol = stackedChartSymbol as IChartSymbol;
            fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(0, 0, 255);
            chartRenderer.BaseSymbol = fillSymbol as ISymbol;
            chartRenderer.UseOverposter = false;
            //创建图例
            chartRenderer.CreateLegend();
            geoFeatureLayer.Renderer = chartRenderer as IFeatureRenderer;
            this.axMapControl1.Refresh();
        }
        //PieChartRenderer
        private void button7_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer geoFeatureLayer;
            IFeatureLayer featureLayer;
            ITable table;
            ICursor cursor;
            IRowBuffer rowBuffer;
            //设置饼图的要素
            string field1 = "sqmi";
            string field2 = "sqkm";
            //获取渲染图层
            geoFeatureLayer = getGeoLayer("Continents");
            featureLayer = geoFeatureLayer as IFeatureLayer;
            table = featureLayer as ITable;
            geoFeatureLayer.ScaleSymbols = true;
            IChartRenderer chartRenderer = new ChartRendererClass();
            IPieChartRenderer pieChartRenderer = chartRenderer as IPieChartRenderer;
            IRendererFields rendererFields = chartRenderer as IRendererFields;
            rendererFields.AddField(field1, field1);
            rendererFields.AddField(field2, field2);
            int[] fieldIndexs = new int[2];
            fieldIndexs[0] = table.FindField(field1);
            fieldIndexs[1] = table.FindField(field2);
            //获取渲染要素的最大值
            double fieldValue = 0.0, maxValue = 0.0;
            cursor = table.Search(null, true);
            rowBuffer = cursor.NextRow();
            while (rowBuffer != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    fieldValue = double.Parse(rowBuffer.get_Value(fieldIndexs[i]).ToString());
                    if (fieldValue > maxValue)
                    {
                        maxValue = fieldValue;
                    }
                }
                rowBuffer = cursor.NextRow();
            }
            //设置饼图符号
            IPieChartSymbol pieChartSymbol = new PieChartSymbolClass();
            pieChartSymbol.Clockwise = true;
            pieChartSymbol.UseOutline = true;
            IChartSymbol chartSymbol = pieChartSymbol as IChartSymbol;
            chartSymbol.MaxValue = maxValue;
            ILineSymbol lineSymbol = new SimpleLineSymbolClass();
            lineSymbol.Color = getRGB(255, 0, 0);
            lineSymbol.Width = 2;
            pieChartSymbol.Outline = lineSymbol;
            IMarkerSymbol markerSymbol = pieChartSymbol as IMarkerSymbol;
            markerSymbol.Size = 30;
            //添加渲染符号
            ISymbolArray symbolArray = pieChartSymbol as ISymbolArray;
            IFillSymbol fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(0, 255, 0);
            symbolArray.AddSymbol(fillSymbol as ISymbol);
            fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(0, 0, 255);
            symbolArray.AddSymbol(fillSymbol as ISymbol);
            chartRenderer.ChartSymbol = pieChartSymbol as IChartSymbol;
            fillSymbol = new SimpleFillSymbolClass();
            fillSymbol.Color = getRGB(100, 100, 100);
            chartRenderer.BaseSymbol = fillSymbol as ISymbol;
            chartRenderer.UseOverposter = false;
            //创建图例
            chartRenderer.CreateLegend();
            geoFeatureLayer.Renderer = chartRenderer as IFeatureRenderer;
            this.axMapControl1.Refresh();

        }
        //DotDensityFillSymbol
        private void button8_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer geoFeatureLayer;
            IDotDensityFillSymbol dotDensityFillSymbol;
            IDotDensityRenderer dotDensityRenderer;
            //获取渲染图层
            geoFeatureLayer = getGeoLayer("Continents");
            dotDensityRenderer = new DotDensityRendererClass();
            IRendererFields rendererFields = dotDensityRenderer as IRendererFields;
            //设置渲染字段
            string field1 = "sqmi";
            rendererFields.AddField(field1, field1);
            //设置填充颜色和背景色
            dotDensityFillSymbol = new DotDensityFillSymbolClass();
            dotDensityFillSymbol.DotSize = 3;
            dotDensityFillSymbol.Color = getRGB(255, 0, 0);
            dotDensityFillSymbol.BackgroundColor = getRGB(0, 255, 0);
            //设置渲染符号
            ISymbolArray symbolArray = dotDensityFillSymbol as ISymbolArray;
            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbolClass();
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            simpleMarkerSymbol.Size = 2;
            simpleMarkerSymbol.Color = getRGB(0, 0, 255);
            symbolArray.AddSymbol(simpleMarkerSymbol as ISymbol);
            dotDensityRenderer.DotDensitySymbol = dotDensityFillSymbol;
            //设置渲染密度
            dotDensityRenderer.DotValue = 50000;
            //创建图例
            dotDensityRenderer.CreateLegend();
            geoFeatureLayer.Renderer = dotDensityRenderer as IFeatureRenderer;
            this.axMapControl1.Refresh();
        }
    }
}