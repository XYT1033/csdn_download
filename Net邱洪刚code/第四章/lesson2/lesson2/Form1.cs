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

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            IActiveView pActiveView = axMapControl1.ActiveView;
            IScreenDisplay screenDisplay = pActiveView.ScreenDisplay;
            ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = 255;
            lineSymbol.Color = rgbColor;
            // IRubberBand rubberLine = new RubberLineClass();
            // IPolyline pLine = (IPolyline)rubberLine.TrackNew(screenDisplay,            (ISymbol)lineSymbol);
            IPolyline pLine = axMapControl1.TrackLine() as IPolyline;
            screenDisplay.StartDrawing(screenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            screenDisplay.SetSymbol((ISymbol)lineSymbol);
            screenDisplay.DrawPolyline(pLine);
            screenDisplay.FinishDrawing();
        }

        private void axMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            IPoint point = new PointClass();
            //Set the coordinates of current mouse location
            point.PutCoords(e.mapX, e.mapY);
            //Rotate the display based upon the current mouse location
            axMapControl1.ActiveView.ScreenDisplay.RotateMoveTo(point);
            //Draw the rotated display
            axMapControl1.ActiveView.ScreenDisplay.RotateTimer();
        }

        private void axMapControl1_OnMouseUp(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseUpEvent e)
        {
            double dRotationAngle = axMapControl1.ActiveView.ScreenDisplay.RotateStop();
            //Rotate the MapControl's display
            axMapControl1.Rotation = dRotationAngle;
            //Refresh the display
            axMapControl1.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, null, null);

        }


    }
}