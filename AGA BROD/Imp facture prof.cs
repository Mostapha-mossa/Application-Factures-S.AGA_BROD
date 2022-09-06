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
    public partial class Imp_facture_prof : Form
    {
        public Imp_facture_prof()
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
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from FACTURE_Proforma", p.con);
            p.dr = p.cmd.ExecuteReader();
            p.dt.Load(p.dr);
            comboBox2.DataSource = p.dt;
            comboBox2.DisplayMember = "code_f";
            comboBox2.ValueMember = "code_f";
            p.dr.Close();
            p.deconnecter();
        }
        private void Imp_facture_prof_Load(object sender, EventArgs e)
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
                p.cmd = new System.Data.SqlClient.SqlCommand("exec p1 '" + comboBox2.SelectedValue + "'", p.con);
                p.dr = p.cmd.ExecuteReader();
                dt1.Load(p.dr);
                CrystalReport2 cr = new CrystalReport2();
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
