using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HIS.common
{
    public delegate void PagingEventHandler();
    public partial class Pagination : UserControl
    {
        public Pagination()
        {
            InitializeComponent();
        }
        [Description("绑定数据事件"), Category("自定义")]
        public event PagingEventHandler Paging;

        private int _currentPage = 1;// 当前页号
        private int _pageCount = 0;// 页数=总记录数/每页显示记录数
        private int _pageSize = 20;// 每页显示记录数
        private int _recordCount = 0;// 总记录数
        private bool _customPageSize = false;

        /// <summary>
        /// 是否使用自定义页的大小
        /// </summary>
        [Description("是否使用自定义每页显示记录数"), Category("自定义")]
        public bool CustomPageSize
        {
            get { return _customPageSize; }
            set { _customPageSize = value; }
        }

        /// <summary>
        /// 当前页号
        /// </summary>
        [Description("当前页号"), Category("自定义")]
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value; ;
            }
        }

        /// <summary>
        /// 页数=总记录数/每页显示记录数
        /// </summary>
        [Description("页数=总记录数/每页显示记录数"), Category("自定义")]
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        [Description("每页显示记录数"), Category("自定义")]
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (_customPageSize == true)
                {
                    _pageSize = value;
                }
                //getPageCount();
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        [Description("总记录数"), Category("自定义")]
        public int RecordCount
        {
            get { return _recordCount; }
            set
            {
                //页数发生改变
                if (_recordCount != value || value == 0)
                {
                    _recordCount = value;
                    setPageCount();
                }
            }
        }

        /// <summary>
        /// 显示统计信息
        /// </summary>
        public string StaticInfo
        {
            get { return this.lblStatistics.Text; }
            set { this.lblStatistics.Text = value; }
        }


        /// <summary>
        /// 设置页数,及索引框中设置页数项
        /// </summary>
        private void setPageCount()
        {
            if (this._recordCount > 0)
            {
                this._pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this._recordCount) / Convert.ToDouble(this._pageSize)));
                //移除下拉选择页方法
                this.cmbCurrentPage.SelectedIndexChanged -= new System.EventHandler(this.cmbCurrentPage_SelectedIndexChanged);

                //往索引页中填充数据
                this.cmbCurrentPage.Items.Clear();
                for (int i = 1; i <= this._pageCount; i++)
                {
                    this.cmbCurrentPage.Items.Add(i);
                }
                this.cmbCurrentPage.SelectedIndexChanged += new System.EventHandler(this.cmbCurrentPage_SelectedIndexChanged);
            }
            else
            {
                this._pageCount = 0;
            }
            //设置显示状态
            setPaginationViewState();
        }


        /// <summary>
        /// 翻页控件数据绑定的方法
        /// </summary>
        private void bind()
        {
            if (this.Paging != null)
            {
                Paging();
            }
            //设置显示状态
            setPaginationViewState();
        }


        /// <summary>
        /// 设置显示状态.
        /// </summary>
        private void setPaginationViewState()
        {
            this.cmbCurrentPage.SelectedIndexChanged -= new System.EventHandler(this.cmbCurrentPage_SelectedIndexChanged);
            this.cmbCurrentPage.Text = this._currentPage.ToString();
            this.cmbCurrentPage.SelectedIndexChanged += new System.EventHandler(this.cmbCurrentPage_SelectedIndexChanged);

            this.lblPagingInfo.Text = "页次：" + this._currentPage.ToString() + "/" + this._pageCount.ToString() + "，每页" + this._pageSize.ToString() + "条，共" + this._recordCount.ToString() + "条";

            //如果当前页是第一页，首页、上一页不能点击，下一页、尾页可点击
            if (this._currentPage == 1)
            {
                this.btnPrevPage.Enabled = false;
                this.btnFirstPage.Enabled = false;
            }
            else
            {
                this.btnPrevPage.Enabled = true;
                this.btnFirstPage.Enabled = true;
            }
            //如果当前页是最后一页，首页、上一页可点击，下一页、尾页不可点击
            if (this._currentPage == this._pageCount)
            {
                this.btnNextPage.Enabled = false;
                this.btnLastPage.Enabled = false;
            }
            else
            {
                this.btnNextPage.Enabled = true;
                this.btnLastPage.Enabled = true;
            }
            //页数为0时,首页、上一页、下一页、尾页都不能点击
            if (this._recordCount == 0)
            {
                this.btnPrevPage.Enabled = false;
                this.btnFirstPage.Enabled = false;
                this.btnNextPage.Enabled = false;
                this.btnLastPage.Enabled = false;
            }
        }


        #region 翻页事件(包括点击首页、上一页、下一页、尾页,索引框下拉择页数,填写页数,点击跳转)
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            this.bind();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            //当前页大于1页,当前页减一页,否则不做操作
            if (_currentPage > 1)
            {
                _currentPage -= 1;
                this.bind();
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            //当前页不是最后一页,当前页加一页,否则不做操作
            if (_currentPage < _pageCount)
            {
                _currentPage += 1;
                this.bind();
            }
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLastPage_Click(object sender, EventArgs e)
        {
            _currentPage = _pageCount;
            this.bind();
        }

        /// <summary>
        /// 点击跳转按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntGo_Click(object sender, EventArgs e)
        {
            //只有页数改成非当前页才触发.
            if (Convert.ToInt32(this.cmbCurrentPage.Text) != this._currentPage)
            {
                if (Convert.ToInt32(this.cmbCurrentPage.Text) <= this.cmbCurrentPage.Items.Count && Convert.ToInt32(this.cmbCurrentPage.Text) != 0)
                {
                    cmbCurrentPage_SelectedIndexChanged(null, null);
                    this.cmbCurrentPage.SelectAll();
                    this.cmbCurrentPage.Focus();
                }
                else
                {
                    MessageBox.Show("页的大小超出索引,请重新输入", "提示");
                }
            }
        }

        /// <summary>
        /// 页数索引改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._currentPage = Convert.ToInt32(this.cmbCurrentPage.Text);
            bind();
        }


        /// <summary>
        /// 索引框内只能填写数字和回车键有效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)13)
            {
                bntGo_Click(null, null);
            }
        }
        #endregion


        /// <summary>
        /// 设置分页控件大小(宽度)的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pagination_SizeChanged(object sender, EventArgs e)
        {
            this.lblStatistics.Width = this.Width - 488;
        }
    }
}
