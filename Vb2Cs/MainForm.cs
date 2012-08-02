using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vb2Cs
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CodeTransformForm ctForm = new CodeTransformForm();
            ctForm.MdiParent = this;
            ctForm.Show();

            StatForm sForm = new StatForm();
            sForm.MdiParent = this;
            sForm.Show();

            this.LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
