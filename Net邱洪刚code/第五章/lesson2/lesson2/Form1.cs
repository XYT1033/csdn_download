using System;
using System.IO;
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
            //loadMapDocument();
            IEnvelope env = new EnvelopeClass();
            env.XMax = 15;
            env.XMin = 0;
            env.YMax = 15;
            env.YMin = 0;
            this.axMapControl1.Extent = env;
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
        //SimpleMarkerSymbol
        private void button1_Click(object sender, EventArgs e)
        {
            ISimpleMarkerSymbol iMarkerSymbol;
            ISymbol iSymbol;
            IRgbColor iRgbColor;
            iMarkerSymbol = new SimpleMarkerSymbol();
            iMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            iRgbColor = new RgbColor();
            iRgbColor = getRGB(100, 100, 100);
            iMarkerSymbol.Color = iRgbColor;
            iSymbol = (ISymbol)iMarkerSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            IPoint point1 = new PointClass();
            IPoint point2 = new PointClass();
            point1.PutCoords(5, 5);
            point2.PutCoords(5, 10);
            this.axMapControl1.FlashShape(point1 as IGeometry, 3, 200, iSymbol);
            this.axMapControl1.FlashShape(point2);
        }
        //ArrowMarkerSymbol
        private void button2_Click(object sender, EventArgs e)
        {
            IArrowMarkerSymbol arrowMarkerSymbol = new ArrowMarkerSymbolClass();
            IRgbColor iRgbColor;
            iRgbColor = new RgbColor();
            iRgbColor = getRGB(100, 100, 100);
            arrowMarkerSymbol.Angle = 90;
            arrowMarkerSymbol.Color = iRgbColor;
            arrowMarkerSymbol.Length = 30;
            arrowMarkerSymbol.Width = 20;
            arrowMarkerSymbol.XOffset = 0;
            arrowMarkerSymbol.YOffset = 0;
            arrowMarkerSymbol.Style = esriArrowMarkerStyle.esriAMSPlain;
            IPoint point1 = new PointClass();
            IPoint point2 = new PointClass();
            point1.PutCoords(5, 5);
            point2.PutCoords(5, 10);
            this.axMapControl1.FlashShape(point1 as IGeometry, 3, 500, arrowMarkerSymbol);
            this.axMapControl1.FlashShape(point2 as IGeometry);
        }
        //CharacterMarkerSymbol
        private void button3_Click(object sender, EventArgs e)
        {
            ICharacterMarkerSymbol characterMarkerSymbol = new CharacterMarkerSymbol();
            stdole.IFontDisp fontDisp = (stdole.IFontDisp)(new stdole.StdFontClass());
            IRgbColor rgbColor = new RgbColor();
            rgbColor = getRGB(100, 100, 100);
            fontDisp.Name = "arial";
            fontDisp.Size = 12;
            fontDisp.Italic = true;

            characterMarkerSymbol.Angle = 0;
            characterMarkerSymbol.CharacterIndex = 97;
            characterMarkerSymbol.Color = rgbColor;
            characterMarkerSymbol.Font = fontDisp;
            characterMarkerSymbol.Size = 24;
            characterMarkerSymbol.XOffset = 0;
            characterMarkerSymbol.YOffset = 0;

            IPoint point1 = new PointClass();
            IPoint point2 = new PointClass();
            point1.PutCoords(5, 5);
            point2.PutCoords(5, 10);
            this.axMapControl1.FlashShape(point1 as IGeometry, 3, 500, characterMarkerSymbol);
            this.axMapControl1.FlashShape(point2 as IGeometry);
        }
        //PictureMarkerSymbol
        private void button4_Click(object sender, EventArgs e)
        {
            IRgbColor rgbColor = new RgbColorClass();
            IPictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbolClass();
            //string fileName = @"E:\vs2005\第五章\lesson2\lesson2\data\qq.bmp";

            string path = Directory.GetCurrentDirectory();
            string fileName = path + @"\qq.bmp";
            pictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);
            pictureMarkerSymbol.Angle = 0;
            pictureMarkerSymbol.BitmapTransparencyColor = rgbColor;
            pictureMarkerSymbol.Size = 20;
            pictureMarkerSymbol.XOffset = 0;
            pictureMarkerSymbol.YOffset = 0;
            IPoint point1 = new PointClass();
            IPoint point2 = new PointClass();
            point1.PutCoords(5, 5);
            point2.PutCoords(5, 10);

            this.axMapControl1.FlashShape(point1 as IGeometry, 3, 200, pictureMarkerSymbol);
            this.axMapControl1.FlashShape(point2 as IGeometry);
        }
        //MultiLayerMarkerSymbol
        private void button5_Click(object sender, EventArgs e)
        {
            IMultiLayerMarkerSymbol multiLayerMarkerSymbol = new MultiLayerMarkerSymbolClass();
            IPictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbolClass();
            ICharacterMarkerSymbol characterMarkerSymbol = new CharacterMarkerSymbol();
            stdole.IFontDisp fontDisp = (stdole.IFontDisp)(new stdole.StdFontClass());
            IRgbColor rgbColor = new RgbColor();
            rgbColor = getRGB(0, 0, 0);
            fontDisp.Name = "arial";
            fontDisp.Size = 12;
            fontDisp.Italic = true;
            //创建字符符号
            characterMarkerSymbol.Angle = 0;
            characterMarkerSymbol.CharacterIndex = 97;
            characterMarkerSymbol.Color = rgbColor;
            characterMarkerSymbol.Font = fontDisp;
            characterMarkerSymbol.Size = 24;
            characterMarkerSymbol.XOffset = 0;
            characterMarkerSymbol.YOffset = 0;
            //创建图片符号
            //string fileName = @"E:\vs2005\第五章\lesson2\lesson2\data\qq.bmp";
            string path = Directory.GetCurrentDirectory();
            string fileName = path + @"\qq.bmp";
            pictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);
            pictureMarkerSymbol.Angle = 0;
            pictureMarkerSymbol.BitmapTransparencyColor = rgbColor;
            pictureMarkerSymbol.Size = 30;
            pictureMarkerSymbol.XOffset = 0;
            pictureMarkerSymbol.YOffset = 0;
            //添加图片、字符符号到组合符号中
            multiLayerMarkerSymbol.AddLayer(pictureMarkerSymbol);
            multiLayerMarkerSymbol.AddLayer(characterMarkerSymbol);
            multiLayerMarkerSymbol.Angle = 0;
            multiLayerMarkerSymbol.Size = 30;
            multiLayerMarkerSymbol.XOffset = 0;
            multiLayerMarkerSymbol.YOffset = 0;

            IPoint point1 = new PointClass();
            IPoint point2 = new PointClass();
            point1.PutCoords(5, 5);
            point2.PutCoords(5, 10);
            this.axMapControl1.FlashShape(point1 as IGeometry, 3, 200, multiLayerMarkerSymbol);
            this.axMapControl1.FlashShape(point2 as IGeometry);
        }

        //SimpleLineSymbol
        private void button6_Click(object sender, EventArgs e)
        {
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 1);
            polyline.FromPoint = point;
            point.PutCoords(10, 10);
            polyline.ToPoint = point;
            simpleLineSymbol.Width = 10;
            IRgbColor rgbColor = getRGB(255, 0, 0);
            simpleLineSymbol.Color = rgbColor;
            ISymbol symbol = simpleLineSymbol as ISymbol;
            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            IActiveView activeView = this.axMapControl1.ActiveView;

            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(symbol);
            activeView.ScreenDisplay.DrawPolyline(polyline as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
            activeView.ScreenDisplay.FinishDrawing();

        }
        //CartographicLineSymbol
        private void button7_Click(object sender, EventArgs e)
        {
            ICartographicLineSymbol cartographicLineSymbol = new CartographicLineSymbolClass();
            cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
            cartographicLineSymbol.Join = esriLineJoinStyle.esriLJSBevel;
            cartographicLineSymbol.Width = 10;
            cartographicLineSymbol.MiterLimit = 4;
            ILineProperties lineProperties;
            lineProperties = cartographicLineSymbol as ILineProperties;
            lineProperties.Offset = 0;
            double[] dob = new double[6];
            dob[0] = 0;
            dob[1] = 1;
            dob[2] = 2;
            dob[3] = 3;
            dob[4] = 4;
            dob[5] = 5;
            ITemplate template = new TemplateClass();
            template.Interval = 1;
            for (int i = 0; i < dob.Length; i += 2)
            {
                template.AddPatternElement(dob[i], dob[i + 1]);
            }
            lineProperties.Template = template;

            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 1);
            polyline.FromPoint = point;
            point.PutCoords(10, 10);
            polyline.ToPoint = point;
            IRgbColor rgbColor = getRGB(0, 255, 0);
            cartographicLineSymbol.Color = rgbColor;
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(cartographicLineSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolyline(polyline as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
            activeView.ScreenDisplay.FinishDrawing();

        }
        //MultiLayerLineSymbol
        private void button8_Click(object sender, EventArgs e)
        {
            IMultiLayerLineSymbol multiLayerLineSymbol = new MultiLayerLineSymbolClass();
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            simpleLineSymbol.Width = 10;
            IRgbColor rgbColor = getRGB(255, 0, 0);
            simpleLineSymbol.Color = rgbColor;
            ISymbol symbol = simpleLineSymbol as ISymbol;
            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            ICartographicLineSymbol cartographicLineSymbol = new CartographicLineSymbolClass();
            cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
            cartographicLineSymbol.Join = esriLineJoinStyle.esriLJSBevel;
            cartographicLineSymbol.Width = 10;
            cartographicLineSymbol.MiterLimit = 4;
            ILineProperties lineProperties;
            lineProperties = cartographicLineSymbol as ILineProperties;
            lineProperties.Offset = 0;
            double[] dob = new double[6];
            dob[0] = 0;
            dob[1] = 1;
            dob[2] = 2;
            dob[3] = 3;
            dob[4] = 4;
            dob[5] = 5;
            ITemplate template = new TemplateClass();
            template.Interval = 1;
            for (int i = 0; i < dob.Length; i += 2)
            {
                template.AddPatternElement(dob[i], dob[i + 1]);
            }
            lineProperties.Template = template;

            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 1);
            polyline.FromPoint = point;
            point.PutCoords(10, 10);
            polyline.ToPoint = point;
            rgbColor = getRGB(0, 255, 0);
            cartographicLineSymbol.Color = rgbColor;
            multiLayerLineSymbol.AddLayer(simpleLineSymbol);
            multiLayerLineSymbol.AddLayer(cartographicLineSymbol);

            IActiveView activeView = this.axMapControl1.ActiveView;

            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(multiLayerLineSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolyline(polyline as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
            activeView.ScreenDisplay.FinishDrawing();
        }
        //HashLineSymbol
        private void button9_Click(object sender, EventArgs e)
        {
            IHashLineSymbol hashLineSymbol = new HashLineSymbolClass();
            ILineProperties lineProperties = hashLineSymbol as ILineProperties;
            lineProperties.Offset = 0;
            double[] dob = new double[6];
            dob[0] = 0;
            dob[1] = 1;
            dob[2] = 2;
            dob[3] = 3;
            dob[4] = 4;
            dob[5] = 5;
            ITemplate template = new TemplateClass();
            template.Interval = 1;
            for (int i = 0; i < dob.Length; i += 2)
            {
                template.AddPatternElement(dob[i], dob[i + 1]);
            }
            lineProperties.Template = template;

            hashLineSymbol.Width = 2;
            hashLineSymbol.Angle = 45;
            IRgbColor hashColor = new RgbColor();
            hashColor = getRGB(0, 0, 255);
            hashLineSymbol.Color = hashColor;

            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 1);
            polyline.FromPoint = point;
            point.PutCoords(10, 10);
            polyline.ToPoint = point;
            IActiveView activeView = this.axMapControl1.ActiveView;

            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(hashLineSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolyline(polyline as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
            activeView.ScreenDisplay.FinishDrawing();


        }
        //MarkerLineSymbol
        private void button10_Click(object sender, EventArgs e)
        {
            IArrowMarkerSymbol arrowMarkerSymbol = new ArrowMarkerSymbolClass();
            IRgbColor rgbColor = getRGB(255, 0, 0);
            arrowMarkerSymbol.Color = rgbColor as IColor;
            arrowMarkerSymbol.Length = 10;
            arrowMarkerSymbol.Width = 10;
            arrowMarkerSymbol.Style = esriArrowMarkerStyle.esriAMSPlain;

            IMarkerLineSymbol markerLineSymbol = new MarkerLineSymbolClass();
            markerLineSymbol.MarkerSymbol = arrowMarkerSymbol;
            rgbColor = getRGB(0, 255, 0);
            markerLineSymbol.Color = rgbColor;
            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 1);
            polyline.FromPoint = point;
            point.PutCoords(10, 10);
            polyline.ToPoint = point;
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(markerLineSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolyline(polyline as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
            activeView.ScreenDisplay.FinishDrawing();

        }
        //PictureLineSymbol
        private void button11_Click(object sender, EventArgs e)
        {
            IPictureLineSymbol pictureLineSymbol = new PictureLineSymbolClass();
            //创建图片符号
            //string fileName = @"E:\vs2005\第五章\lesson2\lesson2\data\qq.bmp";
            string path = Directory.GetCurrentDirectory();
            string fileName = path + @"\qq.bmp";
            pictureLineSymbol.CreateLineSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);
            IRgbColor rgbColor = getRGB(0, 255, 0);
            pictureLineSymbol.Color = rgbColor;
            pictureLineSymbol.Offset = 0;
            pictureLineSymbol.Width = 10;
            pictureLineSymbol.Rotate = false;

            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 1);
            polyline.FromPoint = point;
            point.PutCoords(10, 10);
            polyline.ToPoint = point;
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(pictureLineSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolyline(polyline as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
            activeView.ScreenDisplay.FinishDrawing();


        }

        //SimpleFillSymbol 
        private void button12_Click(object sender, EventArgs e)
        {
            //简单填充符号
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            simpleFillSymbol.Color = getRGB(255, 0, 0);
            //创建边线符号
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            simpleLineSymbol.Color = getRGB(0, 255, 0);
            simpleLineSymbol.Width = 10;
            ISymbol symbol = simpleLineSymbol as ISymbol;
            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            simpleFillSymbol.Outline = simpleLineSymbol;
            //创建面对象
            object Missing = Type.Missing;
            IPolygon polygon = new PolygonClass();
            IPointCollection pointCollection = polygon as IPointCollection;
            IPoint point = new PointClass();
            point.PutCoords(5, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(5, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            polygon.SimplifyPreserveFromTo();
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(simpleFillSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolygon(polygon as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
        }
        //LineFillSymbol
        private void button13_Click(object sender, EventArgs e)
        {
            ICartographicLineSymbol cartoLine = new CartographicLineSymbol();
            cartoLine.Cap = esriLineCapStyle.esriLCSButt;
            cartoLine.Join = esriLineJoinStyle.esriLJSMitre;
            cartoLine.Color = getRGB(255, 0, 0);
            cartoLine.Width = 2;
            //Create the LineFillSymbo
            ILineFillSymbol lineFill = new LineFillSymbol();
            lineFill.Angle = 45;
            lineFill.Separation = 10;
            lineFill.Offset = 5;
            lineFill.LineSymbol = cartoLine;
            object Missing = Type.Missing;
            IPolygon polygon = new PolygonClass();
            IPointCollection pointCollection = polygon as IPointCollection;
            IPoint point = new PointClass();
            point.PutCoords(5, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(5, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            polygon.SimplifyPreserveFromTo();
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(lineFill as ISymbol);
            activeView.ScreenDisplay.DrawPolygon(polygon as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
        }
        //MarkerFillSymbol
        private void button14_Click(object sender, EventArgs e)
        {
            IArrowMarkerSymbol arrowMarkerSymbol = new ArrowMarkerSymbolClass();
            IRgbColor rgbColor = getRGB(255, 0, 0);
            arrowMarkerSymbol.Color = rgbColor as IColor;
            arrowMarkerSymbol.Length = 10;
            arrowMarkerSymbol.Width = 10;
            arrowMarkerSymbol.Style = esriArrowMarkerStyle.esriAMSPlain;


            IMarkerFillSymbol markerFillSymbol = new MarkerFillSymbolClass();
            markerFillSymbol.MarkerSymbol = arrowMarkerSymbol;
            rgbColor = getRGB(0, 255, 0);
            markerFillSymbol.Color = rgbColor;
            markerFillSymbol.Style = esriMarkerFillStyle.esriMFSGrid;

            IFillProperties fillProperties = markerFillSymbol as IFillProperties;
            fillProperties.XOffset = 2;
            fillProperties.YOffset = 2;
            fillProperties.XSeparation = 15;
            fillProperties.YSeparation = 20;

            object Missing = Type.Missing;
            IPolygon polygon = new PolygonClass();
            IPointCollection pointCollection = polygon as IPointCollection;
            IPoint point = new PointClass();
            point.PutCoords(5, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(5, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            polygon.SimplifyPreserveFromTo();
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(markerFillSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolygon(polygon as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
        }
        //GradientFillSymbol
        private void button15_Click(object sender, EventArgs e)
        {
            IGradientFillSymbol gradientFillSymbol = new GradientFillSymbolClass();
            IAlgorithmicColorRamp algorithcColorRamp = new AlgorithmicColorRampClass();
            algorithcColorRamp.FromColor = getRGB(255, 0, 0);
            algorithcColorRamp.ToColor = getRGB(0, 255, 0);
            algorithcColorRamp.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
            gradientFillSymbol.ColorRamp = algorithcColorRamp;
            gradientFillSymbol.GradientAngle = 45;
            gradientFillSymbol.GradientPercentage = 0.9;
            gradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;


            object Missing = Type.Missing;
            IPolygon polygon = new PolygonClass();
            IPointCollection pointCollection = polygon as IPointCollection;
            IPoint point = new PointClass();
            point.PutCoords(5, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(5, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            polygon.SimplifyPreserveFromTo();
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(gradientFillSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolygon(polygon as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
        }
        //PictureFillSymbol
        private void button16_Click(object sender, EventArgs e)
        {
            IPictureFillSymbol pictureFillSymbol = new PictureFillSymbolClass();
            //创建图片符号
            //string fileName = @"E:\vs2005\第五章\lesson2\lesson2\data\qq.bmp";
            string path = Directory.GetCurrentDirectory();
            string fileName = path + @"\qq.bmp";
            pictureFillSymbol.CreateFillSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);
            pictureFillSymbol.Color = getRGB(0, 255, 0);

            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            simpleLineSymbol.Color = getRGB(255, 0, 0);
            ISymbol symbol = pictureFillSymbol as ISymbol;
            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            pictureFillSymbol.Outline = simpleLineSymbol;
            pictureFillSymbol.Angle = 45;

            object Missing = Type.Missing;
            IPolygon polygon = new PolygonClass();
            IPointCollection pointCollection = polygon as IPointCollection;
            IPoint point = new PointClass();
            point.PutCoords(5, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(5, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            polygon.SimplifyPreserveFromTo();
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(pictureFillSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolygon(polygon as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();

        }
        //MultilayerFillSymbol
        private void button17_Click(object sender, EventArgs e)
        {
            IMultiLayerFillSymbol multiLayerFillSymbol = new MultiLayerFillSymbolClass();

            IGradientFillSymbol gradientFillSymbol = new GradientFillSymbolClass();
            IAlgorithmicColorRamp algorithcColorRamp = new AlgorithmicColorRampClass();
            algorithcColorRamp.FromColor = getRGB(255, 0, 0);
            algorithcColorRamp.ToColor = getRGB(0, 255, 0);
            algorithcColorRamp.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
            gradientFillSymbol.ColorRamp = algorithcColorRamp;
            gradientFillSymbol.GradientAngle = 45;
            gradientFillSymbol.GradientPercentage = 0.9;
            gradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;

            ICartographicLineSymbol cartoLine = new CartographicLineSymbol();
            cartoLine.Cap = esriLineCapStyle.esriLCSButt;
            cartoLine.Join = esriLineJoinStyle.esriLJSMitre;
            cartoLine.Color = getRGB(255, 0, 0);
            cartoLine.Width = 2;
            //Create the LineFillSymbo
            ILineFillSymbol lineFill = new LineFillSymbol();
            lineFill.Angle = 45;
            lineFill.Separation = 10;
            lineFill.Offset = 5;
            lineFill.LineSymbol = cartoLine;


            multiLayerFillSymbol.AddLayer(gradientFillSymbol);
            multiLayerFillSymbol.AddLayer(lineFill);

            object Missing = Type.Missing;
            IPolygon polygon = new PolygonClass();
            IPointCollection pointCollection = polygon as IPointCollection;
            IPoint point = new PointClass();
            point.PutCoords(5, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(5, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 10);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            point.PutCoords(10, 5);
            pointCollection.AddPoint(point, ref Missing, ref Missing);
            polygon.SimplifyPreserveFromTo();
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(multiLayerFillSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolygon(polygon as IGeometry);
            activeView.ScreenDisplay.FinishDrawing();
        }
        //TextSymbol
        private void button18_Click(object sender, EventArgs e)
        {
            ITextSymbol textSymbol = new TextSymbolClass();
            System.Drawing.Font drawFont = new System.Drawing.Font("宋体", 16, FontStyle.Bold);
            stdole.IFontDisp fontDisp = (stdole.IFontDisp)(new stdole.StdFontClass());
            textSymbol.Font = fontDisp;
            textSymbol.Color = getRGB(0, 255, 0);
            textSymbol.Size = 20;
            IPolyline polyline = new PolylineClass();
            IPoint point = new PointClass();
            point.PutCoords(1, 1);
            polyline.FromPoint = point;
            point.PutCoords(10, 10);
            polyline.ToPoint = point;
            ITextPath textPath = new BezierTextPathClass();
            //创建简单标注 
            ILineSymbol lineSymbol = new SimpleLineSymbolClass();
            lineSymbol.Color = getRGB(255, 0, 0);
            lineSymbol.Width = 5;
            ISimpleTextSymbol simpleTextSymbol = textSymbol as ISimpleTextSymbol;
            simpleTextSymbol.TextPath = textPath;
            object oLineSymbol = lineSymbol;
            object oTextSymbol = textSymbol;
            IActiveView activeView = this.axMapControl1.ActiveView;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(oLineSymbol as ISymbol);
            activeView.ScreenDisplay.DrawPolyline(polyline as IGeometry);
            activeView.ScreenDisplay.SetSymbol(oTextSymbol as ISymbol);
            activeView.ScreenDisplay.DrawText(polyline as IGeometry, "简单标注"); ;
            activeView.ScreenDisplay.FinishDrawing();

            //创建气泡标注（两中风格，一种是有锚点，一种是marker方式）
            //锚点方式
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            simpleFillSymbol.Color = getRGB(0, 255, 0);
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            IBalloonCallout balloonCallout = new BalloonCalloutClass();
            balloonCallout.Style = esriBalloonCalloutStyle.esriBCSRectangle;
            balloonCallout.Symbol = simpleFillSymbol;
            balloonCallout.LeaderTolerance = 10;

            point.PutCoords(5, 5);
            balloonCallout.AnchorPoint = point;

            IGraphicsContainer graphicsContainer = activeView as IGraphicsContainer;
            IFormattedTextSymbol formattedTextSymbol = new TextSymbolClass();
            formattedTextSymbol.Color = getRGB(0, 0, 255);
            point.PutCoords(10, 5);
            ITextBackground textBackground = balloonCallout as ITextBackground;
            formattedTextSymbol.Background = textBackground;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(formattedTextSymbol as ISymbol);
            activeView.ScreenDisplay.DrawText(point as IGeometry, "气泡1");
            activeView.ScreenDisplay.FinishDrawing();


            //marker方式
            textSymbol = new TextSymbolClass();
            textSymbol.Color = getRGB(255, 0, 0);
            textSymbol.Angle = 0;
            textSymbol.RightToLeft = false;
            textSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABaseline;
            textSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull;


            IMarkerTextBackground markerTextBackground = new MarkerTextBackgroundClass();
            markerTextBackground.ScaleToFit = true;
            markerTextBackground.TextSymbol = textSymbol;

            IRgbColor rgbColor = new RgbColorClass();
            IPictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbolClass();
            //string fileName = @"E:\vs2005\第五章\lesson2\lesson2\data\qq.bmp";
            string path = Directory.GetCurrentDirectory();
            string fileName = path + @"\qq.bmp";
            pictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);
            pictureMarkerSymbol.Angle = 0;
            pictureMarkerSymbol.BitmapTransparencyColor = rgbColor;
            pictureMarkerSymbol.Size = 20;
            pictureMarkerSymbol.XOffset = 0;
            pictureMarkerSymbol.YOffset = 0;

            markerTextBackground.Symbol = pictureMarkerSymbol as IMarkerSymbol;

            formattedTextSymbol = new TextSymbolClass();
            formattedTextSymbol.Color = getRGB(255, 0, 0);
            fontDisp.Size = 10;
            fontDisp.Bold = true;
            formattedTextSymbol.Font = fontDisp;

            point.PutCoords(15, 5);

            formattedTextSymbol.Background = markerTextBackground;
            activeView.ScreenDisplay.StartDrawing(activeView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            activeView.ScreenDisplay.SetSymbol(formattedTextSymbol as ISymbol);
            activeView.ScreenDisplay.DrawText(point as IGeometry, "气泡2");
            activeView.ScreenDisplay.FinishDrawing();

        }
    }
}