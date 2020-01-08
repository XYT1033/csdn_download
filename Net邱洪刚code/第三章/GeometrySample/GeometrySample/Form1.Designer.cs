namespace GeometrySample
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.iGeometryCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addGeometryCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertGeometriesCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setGeometriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iSegmentCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSegmentColllectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.querySegmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSegmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPointCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPointCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iGeometryCollectionToolStripMenuItem,
            this.iSegmentCollectionToolStripMenuItem,
            this.iPointCollectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(675, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // iGeometryCollectionToolStripMenuItem
            // 
            this.iGeometryCollectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.addGeometryCollectionToolStripMenuItem,
            this.insertGeometriesCollectionToolStripMenuItem,
            this.setGeometriesToolStripMenuItem});
            this.iGeometryCollectionToolStripMenuItem.Name = "iGeometryCollectionToolStripMenuItem";
            this.iGeometryCollectionToolStripMenuItem.Size = new System.Drawing.Size(138, 21);
            this.iGeometryCollectionToolStripMenuItem.Text = "IGeometryCollection";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(233, 22);
            this.toolStripMenuItem1.Text = "AddGeometry";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // addGeometryCollectionToolStripMenuItem
            // 
            this.addGeometryCollectionToolStripMenuItem.Name = "addGeometryCollectionToolStripMenuItem";
            this.addGeometryCollectionToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.addGeometryCollectionToolStripMenuItem.Text = "AddGeometryCollection";
            this.addGeometryCollectionToolStripMenuItem.Click += new System.EventHandler(this.addGeometryCollectionToolStripMenuItem_Click);
            // 
            // insertGeometriesCollectionToolStripMenuItem
            // 
            this.insertGeometriesCollectionToolStripMenuItem.Name = "insertGeometriesCollectionToolStripMenuItem";
            this.insertGeometriesCollectionToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.insertGeometriesCollectionToolStripMenuItem.Text = "InsertGeometriesCollection";
            this.insertGeometriesCollectionToolStripMenuItem.Click += new System.EventHandler(this.insertGeometriesCollectionToolStripMenuItem_Click);
            // 
            // setGeometriesToolStripMenuItem
            // 
            this.setGeometriesToolStripMenuItem.Name = "setGeometriesToolStripMenuItem";
            this.setGeometriesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.setGeometriesToolStripMenuItem.Text = "SetGeometries";
            this.setGeometriesToolStripMenuItem.Click += new System.EventHandler(this.setGeometriesToolStripMenuItem_Click);
            // 
            // iSegmentCollectionToolStripMenuItem
            // 
            this.iSegmentCollectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSegmentColllectionToolStripMenuItem,
            this.querySegmentsToolStripMenuItem,
            this.setSegmentsToolStripMenuItem});
            this.iSegmentCollectionToolStripMenuItem.Name = "iSegmentCollectionToolStripMenuItem";
            this.iSegmentCollectionToolStripMenuItem.Size = new System.Drawing.Size(131, 21);
            this.iSegmentCollectionToolStripMenuItem.Text = "iSegmentCollection";
            this.iSegmentCollectionToolStripMenuItem.Click += new System.EventHandler(this.iSegmentCollectionToolStripMenuItem_Click);
            // 
            // addSegmentColllectionToolStripMenuItem
            // 
            this.addSegmentColllectionToolStripMenuItem.Name = "addSegmentColllectionToolStripMenuItem";
            this.addSegmentColllectionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addSegmentColllectionToolStripMenuItem.Text = "AddSegments";
            this.addSegmentColllectionToolStripMenuItem.Click += new System.EventHandler(this.addSegmentColllectionToolStripMenuItem_Click);
            // 
            // querySegmentsToolStripMenuItem
            // 
            this.querySegmentsToolStripMenuItem.Name = "querySegmentsToolStripMenuItem";
            this.querySegmentsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.querySegmentsToolStripMenuItem.Text = "QuerySegments";
            this.querySegmentsToolStripMenuItem.Click += new System.EventHandler(this.querySegmentsToolStripMenuItem_Click);
            // 
            // setSegmentsToolStripMenuItem
            // 
            this.setSegmentsToolStripMenuItem.Name = "setSegmentsToolStripMenuItem";
            this.setSegmentsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.setSegmentsToolStripMenuItem.Text = "SetSegments";
            this.setSegmentsToolStripMenuItem.Click += new System.EventHandler(this.setSegmentsToolStripMenuItem_Click);
            // 
            // iPointCollectionToolStripMenuItem
            // 
            this.iPointCollectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPointCollectionToolStripMenuItem,
            this.queryPointsToolStripMenuItem,
            this.updatePointToolStripMenuItem});
            this.iPointCollectionToolStripMenuItem.Name = "iPointCollectionToolStripMenuItem";
            this.iPointCollectionToolStripMenuItem.Size = new System.Drawing.Size(110, 21);
            this.iPointCollectionToolStripMenuItem.Text = "IPointCollection";
            // 
            // addPointCollectionToolStripMenuItem
            // 
            this.addPointCollectionToolStripMenuItem.Name = "addPointCollectionToolStripMenuItem";
            this.addPointCollectionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addPointCollectionToolStripMenuItem.Text = "AddPointCollection";
            this.addPointCollectionToolStripMenuItem.Click += new System.EventHandler(this.addPointCollectionToolStripMenuItem_Click);
            // 
            // queryPointsToolStripMenuItem
            // 
            this.queryPointsToolStripMenuItem.Name = "queryPointsToolStripMenuItem";
            this.queryPointsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.queryPointsToolStripMenuItem.Text = "QueryPoints";
            this.queryPointsToolStripMenuItem.Click += new System.EventHandler(this.queryPointsToolStripMenuItem_Click);
            // 
            // updatePointToolStripMenuItem
            // 
            this.updatePointToolStripMenuItem.Name = "updatePointToolStripMenuItem";
            this.updatePointToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.updatePointToolStripMenuItem.Text = "UpdatePoint";
            this.updatePointToolStripMenuItem.Click += new System.EventHandler(this.updatePointToolStripMenuItem_Click);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(0, 64);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(675, 346);
            this.axMapControl1.TabIndex = 2;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 29);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(675, 28);
            this.axToolbarControl1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 409);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem iGeometryCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addGeometryCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertGeometriesCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setGeometriesToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iSegmentCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSegmentColllectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem querySegmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSegmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPointCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPointCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatePointToolStripMenuItem;
    }
}

