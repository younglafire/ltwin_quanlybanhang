using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace quanlybanhang
{
    public partial class Form1 : Form
    {
        string connectstring = @"Data Source=B402-19B;Initial Catalog=QUANLYBANHANG;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllItems();
        }

        private void LoadAllItems()
        {
            try
            {
                using (var conn = new SqlConnection(connectstring))
                using (var cmd = new SqlCommand(
                    "SELECT MAVT AS [Mã], TENVT AS [Tên], GIAMUA AS [Giá], SLTON AS [Số Lượng], DVT AS [Loại] FROM VATTU",
                    conn))
                using (var adt = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adt.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void SearchByName(string name)
        {
            try
            {
                using (var conn = new SqlConnection(connectstring))
                using (var cmd = new SqlCommand(
                    "SELECT MAVT AS [Mã], TENVT AS [Tên], GIAMUA AS [Giá], SLTON AS [Số Lượng], DVT AS [Loại] " +
                    "FROM VATTU WHERE TENVT LIKE @name", conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = "%" + name.Trim() + "%";

                    using (var adt = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adt.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var key = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(key))
            {
                LoadAllItems();
            }
            else
            {
                SearchByName(key);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadAllItems();
            txtSearch.Focus();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SqlConnection(connectstring)) ;
                string strAdd = "INSERT INTO VATTU (MAVT, TENVT, GIAMUA, SLTON, DVT) " +
                    "VALUES (N'" + txtMAVT.Text + "', N'" + txtTENVT.Text + "', " +
                    txtGIAMUA.Text + ", " + txtSLTON.Text + ", N'" + txtDVT.Text + "')";

                using (var comm = new SqlCommand(strAdd, new SqlConnection(connectstring)))
                {
                    comm.Connection.Open();
                    int i = comm.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Thêm vật tư thành công!");
                        LoadAllItems();
                    }
                    else
                    {
                        MessageBox.Show("Thêm vật tư thất bại!");
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) 
                {
                    MessageBox.Show("Không thể nhập trùng mã VT!");
                }
                else
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            txtMAVT.Text = dataGridView1.Rows[row].Cells["Mã"].Value.ToString();
            txtTENVT.Text = dataGridView1.Rows[row].Cells["Tên"].Value.ToString();
            txtGIAMUA.Text = dataGridView1.Rows[row].Cells["Giá"].Value.ToString();
            txtSLTON.Text = dataGridView1.Rows[row].Cells["Số lượng"].Value.ToString();
            txtDVT.Text = dataGridView1.Rows[row].Cells["Loại"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(connectstring)) ;
            string strUpdate = "UPDATE VATTU SET TENVT = N'" + txtTENVT.Text + "', GIAMUA = " +
                txtGIAMUA.Text + ", SLTON = " + txtSLTON.Text + ", DVT = N'" + txtDVT.Text +
                "' WHERE MAVT = N'" + txtMAVT.Text + "'";
            using (var comm = new SqlCommand(strUpdate, new SqlConnection(connectstring)))
            {
                comm.Connection.Open();
                int i = comm.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cập nhật vật tư thành công!");
                    LoadAllItems();
                }
                else
                {
                    MessageBox.Show("Cập nhật vật tư thất bại! - không thể đổi mã VT! ");


                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SqlConnection(connectstring))
                {
                    string strDelete = "DELETE FROM VATTU WHERE MAVT = @mavt";
                    using (var comm = new SqlCommand(strDelete, conn))
                    {
                        comm.Parameters.AddWithValue("@mavt", txtMAVT.Text);
                        conn.Open();
                        int i = comm.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Xóa vật tư thành công!");
                            LoadAllItems();
                        }
                        else
                        {
                            MessageBox.Show("Xóa vật tư thất bại! Mã VT không tồn tại hoặc đã bị ràng buộc.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Vi phạm ràng buộc khóa ngoại
                {
                    MessageBox.Show("Không thể xóa vật tư vì có liên quan đến bảng khác!");
                }
                else
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }
    }
}