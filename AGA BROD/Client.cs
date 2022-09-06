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
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }
        SQLconnecter p = new SQLconnecter();
        static DataTable dt1 = new DataTable();
        //
        public int count()
        {
            int cpt;
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select count(code_cl) from CLIENT where code_cl='" + maskedTextBox1.Text + "'", p.con);
            cpt = (int)p.cmd.ExecuteScalar();
            return cpt;
        }
        //
        public bool ajouter()
        {

            if (count() == 0)
            {
                p.connecter();
            
                p.cmd = new System.Data.SqlClient.SqlCommand("insert into CLIENT values ('" + maskedTextBox1.Text + "','" + textBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + comboBox1.SelectedValue + "','" + textBox3.Text + "','" + textBox4.Text + "','" + maskedTextBox4.Text + "','" +textBox5.Text +" ')", p.con);
                p.cmd.ExecuteNonQuery();
                p.deconnecter();
                return true;
            }
            p.deconnecter();
            return false;
        }
        public bool ajouter1()
        {

            if (count() == 0)
            {
                p.connecter();

                p.cmd = new System.Data.SqlClient.SqlCommand("insert into CLIENT values ('" + maskedTextBox1.Text + "','" + textBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + comboBox1.SelectedValue + "','" + textBox3.Text + "','" + textBox4.Text + "','" + maskedTextBox5.Text + "','" + textBox5.Text + " ')", p.con);
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
                p.cmd.CommandText = "update CLIENT set nom_prenom_or_raison_sociale='" + textBox1.Text + "',Type_client='" + comboBox2.Text + "',Adresse='" + textBox2.Text + "',N_Telephone='" + maskedTextBox2.Text + "',N_Portable='" + maskedTextBox3.Text + "',code_v='" + comboBox1.SelectedValue + "',Site_internet='" + textBox3.Text + "',E_mail='" + textBox4.Text + "',ICE='" + maskedTextBox4.Text + "' ,Remarques='" + textBox5.Text + "' where code_cl='" + maskedTextBox1.Text + "'";
                p.cmd.ExecuteNonQuery();
                p.deconnecter();
                return true;
            }
            p.deconnecter();
            return false;
        }
        public bool modifier1()
        {
            if (count() != 0)
            {
                p.connecter();
                p.cmd.CommandText = "update CLIENT set nom_prenom_or_raison_sociale='" + textBox1.Text + "',Type_client='" + comboBox2.Text + "',Adresse='" + textBox2.Text + "',N_Telephone='" + maskedTextBox2.Text + "',N_Portable='" + maskedTextBox3.Text + "',code_v='" + comboBox1.SelectedValue + "',Site_internet='" + textBox3.Text + "',E_mail='" + textBox4.Text + "',ICE='" + maskedTextBox5.Text + "' ,Remarques='" + textBox5.Text + "' where code_cl='" + maskedTextBox1.Text + "'";
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
                p.cmd = new System.Data.SqlClient.SqlCommand("delete from CLIENT where code_cl='" + maskedTextBox1.Text + "'", p.con);
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
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from ville", p.con);
            p.dr = p.cmd.ExecuteReader();
            p.dt.Load(p.dr);
            comboBox1.DataSource = p.dt;
            comboBox1.DisplayMember = "nom_ville";
            comboBox1.ValueMember = "code_v";
            p.dr.Close();
            p.deconnecter();
        }

        public bool rechercher()
        {
            if (count() != 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("select CLIENT.*,ville.nom_ville,ville.c_postal from CLIENT,ville where ville.code_v=CLIENT.code_v AND code_cl = '" + maskedTextBox1.Text + "' ", p.con);
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
            p.cmd = new System.Data.SqlClient.SqlCommand("select CLIENT.*,ville.nom_ville,ville.c_postal from CLIENT,ville where ville.code_v=CLIENT.code_v ", p.con);
            p.dr = p.cmd.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(p.dr);
            dataGridView1.DataSource = dt1;
            p.dr.Close();
            p.deconnecter();
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void clear()
        {
             maskedTextBox2.Text = maskedTextBox3.Text = maskedTextBox4.Text = maskedTextBox5.Text = "";
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            combo1();
            chagedgv();
            comboBox2.Text = "Pressionnel";
            maskedTextBox5.Visible = false;
            maskedTextBox4.Visible = true;
            label13.Visible = false;
            label12.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bool vide = false;
            //foreach (Control c in this.Controls)
            //{
            //    if (c is TextBox && ((TextBox)c).Text == "")
            //    {
            //        vide = true;
            //    }
            //}
            //if (vide)
            //{
            //    MessageBox.Show("Merci de Remplir Tous les champs");
            //}
            //else
            //{
            if (comboBox2.Text == "Pressionnel")
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
            else
            {
                if (comboBox2.Text == "Particulier")
                {
                    if (ajouter1() == true)
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
                else
                {
                    MessageBox.Show("VOUS AVEZ SILICTIONE LE TYPE CLIENT!");
                }
            }
            
            //}
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
            catch { MessageBox.Show("Ce client a les factures !\n Vous devez supprimer ces factures."); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Pressionnel")
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
            else
            {
                if (comboBox2.Text == "Particulier")
                {
                    if (modifier1() == true)
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
                else
                {
                    MessageBox.Show("VOUS AVEZ SILICTIONE LE TYPE CLIENT!");
                }
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text== "Particulier")
            {
                maskedTextBox4.Visible = false;
                maskedTextBox5.Visible = true;
                label12.Visible = false;
                label13.Visible = true;
            }
            if (comboBox2.Text == "Pressionnel")
            {
                maskedTextBox5.Visible = false;
                maskedTextBox4.Visible = true;
                label13.Visible = false;
                label12.Visible = true;
            }
        }
    }
}
