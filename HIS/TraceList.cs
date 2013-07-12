using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maticsoft.Model;
using Infragistics.Win;

namespace HIS
{
    public partial class TraceList : Form
    {
        public TraceList()
        {
            InitializeComponent();
            this.ugBadAction_w.DisplayLayout.LoadFromXml(System.IO.Path.GetFullPath(Application.StartupPath + @"\UltragridStyle.xml"));
            this.ugBadAction_w.DisplayLayout.Bands[0].Columns["DELETE"].AllowRowFiltering = DefaultableBoolean.False;
            this.ugBadAction_w.DisplayLayout.Bands[0].Columns["EDIT"].AllowRowFiltering = DefaultableBoolean.False;

            BindTraceList();
        }

        private void BindTraceList()
        {
            Maticsoft.BLL.Patient bll = new Maticsoft.BLL.Patient();
            DataSet dt = bll.GetTracedPatient();
            this.ugBadAction_w.DataSource = dt;
            this.ugBadAction_w.DataBind();
        }

        private void ugBadAction_w_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            if (e.Cell.Text == "取消提醒")
            {
                DialogResult dr = MessageBox.Show(this, "确定要取消此提醒?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    BloodGas bg = new BloodGas();
                    bg.PID = int.Parse(ugBadAction_w.ActiveRow.Cells["ID"].Text);
                    bg.Period = int.Parse(ugBadAction_w.ActiveRow.Cells["Period"].Text);
                    bg.pH = "-1";
                    bg.PaO2 = "-1";
                    bg.SaO2 = "-1";
                    bg.PaCO2 = "-1";
                    bg.RealHCO3 = "-1";
                    bg.StandHCO3 = "-1";
                    bg.AB = "-1";
                    bg.BE = "-1";
                    bg.AG = "-1";
                    bg.CheckDate = DateTime.MinValue;
                    Maticsoft.BLL.BloodGas bll = new Maticsoft.BLL.BloodGas();
                    bll.SaveOrUpdate(bg);
                     
                    ugBadAction_w.ActiveRow.Delete(false);//删除所选行
                }
            }
            if (e.Cell.Text == "填写数据")
            {
                int pid = int.Parse(ugBadAction_w.ActiveRow.Cells["ID"].Text);
                int period = int.Parse(ugBadAction_w.ActiveRow.Cells["Period"].Text);
                AfterTreatTrace openDialog = new AfterTreatTrace(pid, period);
                openDialog.ShowDialog();
                BindTraceList();
            }
        }

        private void ugBadAction_w_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
           // e.Layout.Bands[0].ColumnFilters["DELETE"].FilterConditions.Clear(); 
        }
    }
}
