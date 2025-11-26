using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace quanlybanhang
{
    public partial class Form1 : Form
    {

        string connectstring = @"Data Source=B402-19B;Initial Catalog=QUANLYBANHANG;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectstring);

            try
            {
                conn.Open();
                cmd = new SqlCommand(
                    "select TENVT as N'Tên', " +
                    "GIAMUA AS N'Giá' ,"+
                    "SLTON as N'Số Lượng'," +
                    "DVT as N'Loại'"+
                    "from VATTU"
                    , conn);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


