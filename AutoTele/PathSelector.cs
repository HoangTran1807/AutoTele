using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTele
{
    public partial class PathSelector : Form
    {
        public PathSelector()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void PathSelector_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_choose_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.apk*)|*.apk*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txt_path.Text = openFileDialog1.FileName;
            if (!String.IsNullOrEmpty(txt_path.Text.Trim()))
            {
                Form1.TelePath = txt_path.Text;
            }

        }


        private void lb_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
