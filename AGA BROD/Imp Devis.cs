using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGA_BROD
{
    public partial class Imp_Devis : Form
    {
        public Imp_Devis()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SQLconnecter p = new SQLconnecter();
        static DataTable dt1 = new DataTable();
        public void combo1()
        {
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from Devis", p.con);
            p.dr = p.cmd.ExecuteReader();
            p.dt.Load(p.dr);
            comboBox2.DataSource = p.dt;
            comboBox2.DisplayMember = "code_D";
            comboBox2.ValueMember = "code_D";
            p.dr.Close();
            p.deconnecter();
        }
        private void Imp_Devis_Load(object sender, EventArgs e)
        {
            try
            {
                combo1();

            }
            catch { MessageBox.Show(""); }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                dt1.Clear();
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("exec p3 '" + comboBox2.SelectedValue + "'", p.con);
                p.dr = p.cmd.ExecuteReader();
                dt1.Load(p.dr);
                CrystalReport3 cr = new CrystalReport3();
                cr.SetDataSource(dt1);
                crystalReportViewer1.ReportSource = cr;
                p.deconnecter();
            }
            catch
            {

            }
        }
    }
}
