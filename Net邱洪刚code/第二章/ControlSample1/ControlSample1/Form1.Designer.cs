namespace ControlSample1
{
    partial class ControlSample1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlSample1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.地图加载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载地图文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载特定地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存地图文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存地图文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图层管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加SHP文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图形绘制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘制矩形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘制文本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘制圆形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空间查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.点选查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.圆形查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.多边形查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.名称查询 = new System.Windows.Forms.ToolStripMenuItem();
            this.清除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.地图加载ToolStripMenuItem,
            this.图层管理ToolStripMenuItem,
            this.图形绘制ToolStripMenuItem,
            this.空间查询ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(675, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 地图加载ToolStripMenuItem
            // 
            this.地图加载ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载地图文档ToolStripMenuItem,
            this.加载特定地图ToolStripMenuItem,
            this.保存地图文档ToolStripMenuItem,
            this.另存地图文档ToolStripMenuItem});
            this.地图加载ToolStripMenuItem.Name = "地图加载ToolStripMenuItem";
            this.地图加载ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.地图加载ToolStripMenuItem.Text = "地图加载";
            // 
            // 加载地图文档ToolStripMenuItem
            // 
            this.加载地图文档ToolStripMenuItem.Name = "加载地图文档ToolStripMenuItem";
            this.加载地图文档ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.加载地图文档ToolStripMenuItem.Text = "加载地图文档";
            this.加载地图文档ToolStripMenuItem.Click += new System.EventHandler(this.加载地图文档ToolStripMenuItem_Click);
            // 
            // 加载特定地图ToolStripMenuItem
            // 
            this.加载特定地图ToolStripMenuItem.Name = "加载特定地图ToolStripMenuItem";
            this.加载特定地图ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.加载特定地图ToolStripMenuItem.Text = "加载特定地图";
            this.加载特定地图ToolStripMenuItem.Click += new System.EventHandler(this.加载特定地图ToolStripMenuItem_Click);
            // 
            // 保存地图文档ToolStripMenuItem
            // 
            this.保存地图文档ToolStripMenuItem.Name = "保存地图文档ToolStripMenuItem";
            this.保存地图文档ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.保存地图文档ToolStripMenuItem.Text = "保存地图文档";
            this.保存地图文档ToolStripMenuItem.Click += new System.EventHandler(this.保存地图文档ToolStripMenuItem_Click);
            // 
            // 另存地图文档ToolStripMenuItem
            // 
            this.另存地图文档ToolStripMenuItem.Name = "另存地图文档ToolStripMenuItem";
            this.另存地图文档ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.另存地图文档ToolStripMenuItem.Text = "另存地图文档";
            this.另存地图文档ToolStripMenuItem.Click += new System.EventHandler(this.另存地图文档ToolStripMenuItem_Click);
            // 
            // 图层管理ToolStripMenuItem
            // 
            this.图层管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加图层ToolStripMenuItem,
            this.添加SHP文件ToolStripMenuItem,
            this.删除图层ToolStripMenuItem,
            this.移动图层ToolStripMenuItem});
            this.图层管理ToolStripMenuItem.Name = "图层管理ToolStripMenuItem";
            this.图层管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图层管理ToolStripMenuItem.Text = "图层管理";
            // 
            // 添加图层ToolStripMenuItem
            // 
            this.添加图层ToolStripMenuItem.Name = "添加图层ToolStripMenuItem";
            this.添加图层ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.添加图层ToolStripMenuItem.Text = "添加图层";
            this.添加图层ToolStripMenuItem.Click += new System.EventHandler(this.添加图层ToolStripMenuItem_Click);
            // 
            // 添加SHP文件ToolStripMenuItem
            // 
            this.添加SHP文件ToolStripMenuItem.Name = "添加SHP文件ToolStripMenuItem";
            this.添加SHP文件ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.添加SHP文件ToolStripMenuItem.Text = "添加SHP文件";
            this.添加SHP文件ToolStripMenuItem.Click += new System.EventHandler(this.添加SHP文件ToolStripMenuItem_Click);
            // 
            // 删除图层ToolStripMenuItem
            // 
            this.删除图层ToolStripMenuItem.Name = "删除图层ToolStripMenuItem";
            this.删除图层ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.删除图层ToolStripMenuItem.Text = "删除图层";
            this.删除图层ToolStripMenuItem.Click += new System.EventHandler(this.删除图层ToolStripMenuItem_Click);
            // 
            // 移动图层ToolStripMenuItem
            // 
            this.移动图层ToolStripMenuItem.Name = "移动图层ToolStripMenuItem";
            this.移动图层ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.移动图层ToolStripMenuItem.Text = "移动图层";
            this.移动图层ToolStripMenuItem.Click += new System.EventHandler(this.移动图层ToolStripMenuItem_Click);
            // 
            // 图形绘制ToolStripMenuItem
            // 
            this.图形绘制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.画线ToolStripMenuItem,
            this.绘制矩形ToolStripMenuItem,
            this.绘制文本ToolStripMenuItem,
            this.绘制圆形ToolStripMenuItem});
            this.图形绘制ToolStripMenuItem.Name = "图形绘制ToolStripMenuItem";
            this.图形绘制ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图形绘制ToolStripMenuItem.Text = "图形绘制";
            // 
            // 画线ToolStripMenuItem
            // 
            this.画线ToolStripMenuItem.Name = "画线ToolStripMenuItem";
            this.画线ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.画线ToolStripMenuItem.Text = "绘制线条";
            this.画线ToolStripMenuItem.Click += new System.EventHandler(this.画线ToolStripMenuItem_Click);
            // 
            // 绘制矩形ToolStripMenuItem
            // 
            this.绘制矩形ToolStripMenuItem.Name = "绘制矩形ToolStripMenuItem";
            this.绘制矩形ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.绘制矩形ToolStripMenuItem.Text = "绘制矩形";
            this.绘制矩形ToolStripMenuItem.Click += new System.EventHandler(this.绘制矩形ToolStripMenuItem_Click);
            // 
            // 绘制文本ToolStripMenuItem
            // 
            this.绘制文本ToolStripMenuItem.Name = "绘制文本ToolStripMenuItem";
            this.绘制文本ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.绘制文本ToolStripMenuItem.Text = "绘制文本";
            this.绘制文本ToolStripMenuItem.Click += new System.EventHandler(this.绘制文本ToolStripMenuItem_Click);
            // 
            // 绘制圆形ToolStripMenuItem
            // 
            this.绘制圆形ToolStripMenuItem.Name = "绘制圆形ToolStripMenuItem";
            this.绘制圆形ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.绘制圆形ToolStripMenuItem.Text = "绘制圆形";
            this.绘制圆形ToolStripMenuItem.Click += new System.EventHandler(this.绘制圆形ToolStripMenuItem_Click);
            // 
            // 空间查询ToolStripMenuItem
            // 
            this.空间查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.点选查询ToolStripMenuItem,
            this.矩形查询ToolStripMenuItem,
            this.圆形查询ToolStripMenuItem,
            this.多边形查询ToolStripMenuItem,
            this.名称查询,
            this.清除ToolStripMenuItem});
            this.空间查询ToolStripMenuItem.Name = "空间查询ToolStripMenuItem";
            this.空间查询ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.空间查询ToolStripMenuItem.Text = "空间查询";
            // 
            // 点选查询ToolStripMenuItem
            // 
            this.点选查询ToolStripMenuItem.Name = "点选查询ToolStripMenuItem";
            this.点选查询ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.点选查询ToolStripMenuItem.Text = "点选查询";
            this.点选查询ToolStripMenuItem.Click += new System.EventHandler(this.点选查询ToolStripMenuItem_Click);
            // 
            // 矩形查询ToolStripMenuItem
            // 
            this.矩形查询ToolStripMenuItem.Name = "矩形查询ToolStripMenuItem";
            this.矩形查询ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.矩形查询ToolStripMenuItem.Text = "矩形框选";
            this.矩形查询ToolStripMenuItem.Click += new System.EventHandler(this.矩形查询ToolStripMenuItem_Click);
            // 
            // 圆形查询ToolStripMenuItem
            // 
            this.圆形查询ToolStripMenuItem.Name = "圆形查询ToolStripMenuItem";
            this.圆形查询ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.圆形查询ToolStripMenuItem.Text = "圆形查询";
            this.圆形查询ToolStripMenuItem.Click += new System.EventHandler(this.圆形查询ToolStripMenuItem_Click);
            // 
            // 多边形查询ToolStripMenuItem
            // 
            this.多边形查询ToolStripMenuItem.Name = "多边形查询ToolStripMenuItem";
            this.多边形查询ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.多边形查询ToolStripMenuItem.Text = "多边形查询";
            this.多边形查询ToolStripMenuItem.Click += new System.EventHandler(this.多边形查询ToolStripMenuItem_Click);
            // 
            // 名称查询
            // 
            this.名称查询.Name = "名称查询";
            this.名称查询.Size = new System.Drawing.Size(136, 22);
            this.名称查询.Text = "名称查询";
            this.名称查询.Click += new System.EventHandler(this.名称查询_Click);
            // 
            // 清除ToolStripMenuItem
            // 
            this.清除ToolStripMenuItem.Name = "清除ToolStripMenuItem";
            this.清除ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.清除ToolStripMenuItem.Text = "清除选择";
            this.清除ToolStripMenuItem.Click += new System.EventHandler(this.清除ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(395, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "输入查询名称：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(482, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(191, 21);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(219, 246);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 7;
            // 
            // ControlSample1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 413);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ControlSample1";
            this.Text = "ControlSample1";
            this.Load += new System.EventHandler(this.ControlSample1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 地图加载ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载地图文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载特定地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存地图文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存地图文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图层管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加SHP文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图形绘制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绘制矩形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绘制文本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绘制圆形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空间查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 点选查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 矩形查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 圆形查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 多边形查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除ToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.ToolStripMenuItem 名称查询;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    }
}

