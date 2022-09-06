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
    public partial class Ville : Form
    {
        public Ville()
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
            p.cmd = new System.Data.SqlClient.SqlCommand("select count(CODE_v) from ville where code_v='" + maskedTextBox1.Text + "'", p.con);
            cpt = (int)p.cmd.ExecuteScalar();
            return cpt;
        }
        //
        public int count2()
        {
            int cpt;
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select count(CODE_v) from ville where nom_ville='" + textBox2.Text + "'", p.con);
            cpt = (int)p.cmd.ExecuteScalar();
            return cpt;
        }
        //
        public bool ajouter()
        {
            if (count() == 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("insert into ville values ('" + maskedTextBox1.Text + "','" + textBox2.Text + "','"+ textBox3.Text+ "')", p.con);
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
                p.cmd = new System.Data.SqlClient.SqlCommand("update ville set nom_ville='" + textBox2.Text + "',c_postal='" + textBox3.Text + "' where CODE_V='" + maskedTextBox1.Text + "'", p.con);
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
                p.cmd = new System.Data.SqlClient.SqlCommand("delete from ville where code_v='" + maskedTextBox1.Text + "'", p.con);
                p.cmd.ExecuteNonQuery();
                p.deconnecter();
                return true;
            }
            p.deconnecter();
            return false;
        }




        public bool rechercher()
        {
            if (count2() != 0)
            {
                p.connecter();
                p.cmd = new System.Data.SqlClient.SqlCommand("select * from ville where nom_ville = '" + textBox2.Text + "' ", p.con);
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
        public void clear()
        {
            maskedTextBox1.Text = textBox2.Text = textBox3.Text = "";
        }


        public void chagedgv()
        {
            p.connecter();
            p.cmd = new System.Data.SqlClient.SqlCommand("select * from ville", p.con);
            p.dr = p.cmd.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(p.dr);
            dataGridView1.DataSource = dt1;
            p.dr.Close();
            p.deconnecter();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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
        
        private void button2_Click(object sender, EventArgs e)
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

        private void Ville_Load(object sender, EventArgs e)
        {
            chagedgv();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}