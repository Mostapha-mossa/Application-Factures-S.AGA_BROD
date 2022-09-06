using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace AGA_BROD
{
    class SQLconnecter
    {
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;
        public DataTable dt = new DataTable();
        public SqlDataAdapter dap = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        private string Code_c;
        public string code_c
        {
            get { return Code_c; }
            set { Code_c = value; }
        }

        public void connecter()
        {
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Agafacture;Integrated Security=True";
                con.Open();
            }
        }
        public void deconnecter()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
