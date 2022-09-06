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
    public partial class Devis : Form
    {
        public Devis()
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
            p.cmd = new System.Data.SqlClient.SqlCommand("select count(code_d) from Devis where code_d='" + maskedTextBox1.Text + "'", p.con);
            cpt = (int)p.cmd.ExecuteScalar();
            return cpt;
        }
        //
        public int count1()
        {
            int cpt;
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select count(code_d) from Devis where code_cl='" + comboBox2.SelectedValue + "'", p.con);
            cpt = (int)p.cmd.ExecuteScalar();
            return cpt;
        }
        //
        public int count2()
        {
            int cpt;
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select count(code_d) from Devis where date_Facture between  '" + maskedTextBox2.Text + "' and '" + maskedTextBox3.Text + "'", p.con);
            cpt = (int)p.cmd.ExecuteScalar();
            return cpt;
        }
        //
        public bool ajouter()
        {

            if (count() == 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("insert into Devis values ('" + maskedTextBox1.Text + "','" + comboBox2.SelectedValue + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + comboBox1.Text + "','','','')", p.con);
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
                p.cmd.CommandText = "update Devis set date_échéance='" + maskedTextBox3.Text + "',date_Facture='" + maskedTextBox2.Text + "',code_cl='" + comboBox2.SelectedValue + "',Réglement='" + comboBox1.Text + "' where code_d='" + maskedTextBox1.Text + "'";
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
                p.cmd = new System.Data.SqlClient.SqlCommand("delete from Devis where code_d='" + maskedTextBox1.Text + "'", p.con);
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
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from CLIENT", p.con);
            p.dr = p.cmd.ExecuteReader();
            p.dt.Load(p.dr);
            comboBox2.DataSource = p.dt;
            comboBox2.DisplayMember = "nom_prenom_or_raison_sociale";
            comboBox2.ValueMember = "code_cl";
            p.dr.Close();
            p.deconnecter();
        }

        public bool rechercher()
        {
            if (count() != 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("select * from Devis where code_d = '" + maskedTextBox1.Text + "' ", p.con);
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
        public bool rechercher1()
        {
            if (count1() != 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("select * from Devis where code_cl = '" + comboBox2.SelectedValue + "' ", p.con);
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
        public bool rechercher2()
        {
            if (count2() != 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("select * from Devis where date_Facture between  '"+ maskedTextBox2.Text + "' and '"+ maskedTextBox3.Text + "' ", p.con);
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
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from Devis", p.con);
            p.dr = p.cmd.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(p.dr);
            dataGridView1.DataSource = dt1;
            p.dr.Close();
            p.deconnecter();
        }
        public void clear()
        {
            //maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox3.Text = "";
            comboBox2.Text = "";
        }
        private void Devis_Load(object sender, EventArgs e)
        {
            combo1();
            chagedgv();
            comboBox1.Text = "Espèce";
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
                    clear();
                }
                else
                {
                    MessageBox.Show("N'existe pas!");
                }
            }
            catch { MessageBox.Show("Ce Devis a détail Devis !\n Vous devez supprimer ces détails Devis."); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (modifier() == true)
                {
                    MessageBox.Show("Bien Modifier!");
                    chagedgv();
                    clear();
                }
                else
                {
                    MessageBox.Show("N'existe pas!");
                }
            }
            catch { }
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (rechercher1() == true)
            {

            }
            else
            {
                MessageBox.Show("N'existe pas!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (rechercher2() == true)
                {

                }
                else
                {
                    MessageBox.Show("N'existe pas!\n Ou vous devez écrire à la date 1 qui est plus petite que la date 2 .");
                }
            }
            catch { MessageBox.Show("Veuillez choisir la date !"); }
            
        }
      
    }
}
