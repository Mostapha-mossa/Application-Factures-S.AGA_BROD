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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            hideSubMenu();
        }
        private void hideSubMenu()
        {
            panelMediaSubMenu.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            openChildForm(new Ville());

        }

        private void btnContra_Click(object sender, EventArgs e)
        {
            
        }

        private void btnville_Click(object sender, EventArgs e)
        {
            openChildForm(new Client());
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            openChildForm(new Produit());
        }

        private void btnProduit_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFacture_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new Facture());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Détail_Facture());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new IMP_FACTURE());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new Facture_Proforma()); 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            showSubMenu(panel2);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            showSubMenu(panel3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new Détail_Facture_prof());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new Imp_facture_prof()); 
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openChildForm(new Devis());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openChildForm(new Détail_Devis());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new Imp_Devis());
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
