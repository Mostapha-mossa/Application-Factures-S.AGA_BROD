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
    public partial class Détail_Facture_prof : Form
    {
        public Détail_Facture_prof()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SQLconnecter p = new SQLconnecter();
        static DataTable dt1 = new DataTable();
        //
        public int count()
        {
            int cpt;
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select count(code_f) from detal_FACTUREp where code_f='" + comboBox2.SelectedValue + "' AND code_p='" + comboBox1.SelectedValue + "'", p.con);
            cpt = (int)p.cmd.ExecuteScalar();
            return cpt;
        }
        //
        public bool ajouter()
        {

            if (count() == 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("insert into detal_FACTUREp values ('" + comboBox2.SelectedValue + "','" + comboBox1.SelectedValue + "','" + maskedTextBox1.Text + "')", p.con);
                p.cmd.ExecuteNonQuery();
                p.deconnecter();
                return true;
            }
            p.deconnecter();
            return false;
        }
        //
        public bool modifier()
        {
            if (count() != 0)
            {
                p.connecter();
                p.cmd.CommandText = "update detal_FACTUREp set qté='" + maskedTextBox1.Text + "' where code_f='" + comboBox2.SelectedValue + "' AND code_p='" + comboBox1.SelectedValue + "'";
                p.cmd.ExecuteNonQuery();
                p.deconnecter();
                return true;
            }
            p.deconnecter();
            return false;
        }
        //
        public bool supprimer()
        {

            if (count() != 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("delete from detal_FACTUREp where code_f='" + comboBox2.SelectedValue + "' AND code_p='" + comboBox1.SelectedValue + "'", p.con);
                p.cmd.ExecuteNonQuery();
                p.deconnecter();
                return true;
            }
            p.deconnecter();
            return false;


        }


        public void combo1()
        {
            p.connecter();
            dt1.Clear();
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from FACTURE_Proforma", p.con);
            p.dr = p.cmd.ExecuteReader();
            dt1.Load(p.dr);
            comboBox2.DataSource = dt1;
            comboBox2.DisplayMember = "code_f";
            comboBox2.ValueMember = "code_f";
            p.dr.Close();
            p.deconnecter();
        }
        public void combo2()
        {
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from PRODUIE", p.con);
            p.dr = p.cmd.ExecuteReader();
            p.dt.Load(p.dr);
            comboBox1.DataSource = p.dt;
            comboBox1.DisplayMember = "Description_produie";
            comboBox1.ValueMember = "code_p";
            p.dr.Close();
            p.deconnecter();
        }
        public bool rechercher()
        {
            if (count() != 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("select * from detal_FACTUREp where code_f='" + comboBox2.SelectedValue + "'", p.con);
                p.dr = p.cmd.ExecuteReader();
                DataTable dt1 = new DataTable();
                dt1.Load(p.dr);
                dataGridView1.DataSource = dt1;
                p.dr.Close();
                p.deconnecter();
                return true;
            }
            p.deconnecter();
            return false;
        }


        public void chagedgv()
        {
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from detal_FACTUREp", p.con);
            p.dr = p.cmd.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(p.dr);
            dataGridView1.DataSource = dt1;
            p.dr.Close();
            p.deconnecter();
        }
        public void clear()
        {
            maskedTextBox1.Text = "";
        }
        private void Détail_Facture_prof_Load(object sender, EventArgs e)
        {
            try
            {
                combo1();
                combo2();
                chagedgv();
            }
            catch { MessageBox.Show(""); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool vide = false;
                foreach (Control c in this.Controls)
                {
                    if (c is TextBox && ((TextBox)c).Text == "")
                    {
                        vide = true;
                    }
                }
                if (vide)
                {
                    MessageBox.Show("Merci de Remplir Tous les champs");
                }
                else
                {
                    if (ajouter() == true)
                    {
                        MessageBox.Show("Bien Ajouter!");
                        chagedgv();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Existe Deja!");
                    }
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (supprimer() == true)
                {
                    MessageBox.Show("Bien Supprimer!");
                    chagedgv();

                }
                else
                {
                    MessageBox.Show("N'existe pas!");
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (modifier() == true)
            {
                MessageBox.Show("Bien Modifier!");
                chagedgv();
            }
            else
            {
                MessageBox.Show("N'existe pas!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (rechercher() == true)
            {

            }
            else
            {
                MessageBox.Show("N'existe pas!");
            }
        }
    }
}
