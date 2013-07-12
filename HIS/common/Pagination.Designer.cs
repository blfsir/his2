namespace HIS.common
{
    partial class Pagination
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pagination));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblStatistics = new System.Windows.Forms.ToolStripLabel();
            this.lblPagingInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFirstPage = new System.Windows.Forms.ToolStripButton();
            this.btnPrevPage = new System.Windows.Forms.ToolStripButton();
            this.btnNextPage = new System.Windows.Forms.ToolStripButton();
            this.btnLastPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbCurrentPage = new System.Windows.Forms.ToolStripComboBox();
            this.bntGo = new System.Windows.Forms.ToolStripButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatistics,
            this.lblPagingInfo,
            this.toolStripSeparator1,
            this.btnFirstPage,
            this.btnPrevPage,
            this.btnNextPage,
            this.btnLastPage,
            this.toolStripSeparator3,
            this.cmbCurrentPage,
            this.bntGo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(632, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblStatistics
            // 
            this.lblStatistics.AutoSize = false;
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(100, 22);
            this.lblStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPagingInfo
            // 
            this.lblPagingInfo.AutoSize = false;
            this.lblPagingInfo.Name = "lblPagingInfo";
            this.lblPagingInfo.Size = new System.Drawing.Size(222, 22);
            this.lblPagingInfo.Text = "PagingInfo";
            this.lblPagingInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.AutoToolTip = false;
            this.btnFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFirstPage.Image =  HIS.Properties.Resources.FirstPage;
            this.btnFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(36, 22);
            this.btnFirstPage.Text = "首页";
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.AutoToolTip = false;
            this.btnPrevPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPrevPage.Image =  HIS.Properties.Resources.PrevPage;
            this.btnPrevPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(48, 22);
            this.btnPrevPage.Text = "上一页";
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.AutoToolTip = false;
            this.btnNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNextPage.Image =  HIS.Properties.Resources.NextPage;
            this.btnNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(48, 22);
            this.btnNextPage.Text = "下一页";
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.AutoToolTip = false;
            this.btnLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLastPage.Image = HIS.Properties.Resources.LastPage;
            this.btnLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(36, 22);
            this.btnLastPage.Text = "尾页";
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // cmbCurrentPage
            // 
            this.cmbCurrentPage.AutoSize = false;
            this.cmbCurrentPage.IntegralHeight = false;
            this.cmbCurrentPage.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.cmbCurrentPage.MaxDropDownItems = 20;
            this.cmbCurrentPage.Name = "cmbCurrentPage";
            this.cmbCurrentPage.Size = new System.Drawing.Size(50, 25);
            this.cmbCurrentPage.SelectedIndexChanged += new System.EventHandler(this.cmbCurrentPage_SelectedIndexChanged);
            this.cmbCurrentPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCurrentPage_KeyPress);
            // 
            // bntGo
            // 
            this.bntGo.AutoToolTip = false;
            this.bntGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bntGo.Image = ((System.Drawing.Image)(resources.GetObject("bntGo.Image")));
            this.bntGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntGo.Name = "bntGo";
            this.bntGo.Size = new System.Drawing.Size(36, 22);
            this.bntGo.Text = "跳转";
            this.bntGo.Click += new System.EventHandler(this.bntGo_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.toolStrip1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(632, 25);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // Pagination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Pagination";
            this.Size = new System.Drawing.Size(635, 28);
            this.SizeChanged += new System.EventHandler(this.Pagination_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFirstPage;
        private System.Windows.Forms.ToolStripButton btnPrevPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cmbCurrentPage;
        private System.Windows.Forms.ToolStripLabel lblPagingInfo;
        private System.Windows.Forms.ToolStripButton btnNextPage;
        private System.Windows.Forms.ToolStripButton btnLastPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton bntGo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripLabel lblStatistics;
    }
}
