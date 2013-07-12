using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HIS.common
{
    public class OpenDialogBox
    {
        string filter = "";

        public string _filter
        {
            get { return filter; }
            set { filter = value; }
        }
        string selectedFile = "";

        public string _selectedFile
        {
            get { return selectedFile; }
            set { selectedFile = value; }
        }
        DialogResult dialog = DialogResult.OK;

        public DialogResult _dialog
        {
            get { return dialog; }
            set { dialog = value; }
        }

        //public string _filter { get; set; }
        //public string _selectedFile { get; set; }
        //public System.Windows.Forms.DialogResult _dialog { get; set; }

        public void OpenDig()
        {
            try
            {
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Folder));
                th.IsBackground = true;
                th.Start();
                System.Threading.Thread.Sleep(1000);
                if (th.IsAlive)
                {
                    th.Abort();
                }
            }
            catch (System.Threading.ThreadAbortException ex)
            {

            }
            //Folder();
            System.Windows.Forms.OpenFileDialog OF = new System.Windows.Forms.OpenFileDialog();
            OF.Filter = _filter;
            OF.AutoUpgradeEnabled = false;
            OF.RestoreDirectory = true;
            //OF.InitialDirectory = "C:\\";
            _dialog = OF.ShowDialog();
            _selectedFile = OF.FileName;
            //System.Windows.Forms.MessageBox.Show(_selectedFile);
            //System.Windows.Forms.MessageBox.Show(OF.FileName.ToString());
        }

        void Folder()
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
                fd.ShowDialog();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
