using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace quanlybanhang
{
    public partial class Form1 : Form
    {
        string connectstring = @"Data Source=B402-19;Initial Catalog=QUANLYBANHANG;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllItems();
            LoadAllCustomers();
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                LoadAllItems();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                LoadAllCustomers();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ========== KHÁCH HÀNG (CUSTOMERS) TAB METHODS ==========

        private bool ValidatePhoneNumber(string phoneNumber, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return true; // Phone is optional
            }

            if (phoneNumber.Length != 10 && phoneNumber.Length != 11)
            {
                errorMessage = "Số điện thoại phải có 10 hoặc 11 chữ số!";
                return false;
            }

            // Check if all characters are digits
            foreach (char c in phoneNumber)
            {
                if (!char.IsDigit(c))
                {
                    errorMessage = "Số điện thoại chỉ được chứa chữ số!";
                    return false;
                }
            }

            return true;
        }

        private void LoadAllCustomers()
        {
            try
            {
                using (var conn = new SqlConnection(connectstring))
                using (var cmd = new SqlCommand(
                    "SELECT MAKH AS [Mã KH], TENKH AS [Tên], DIACHI AS [Địa Chỉ], DT AS [SĐT], EMAIL AS [Email] FROM KHACHHANG",
                    conn))
                using (var adt = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adt.Fill(dt);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message);
            }
        }

        private void SearchCustomerByName(string name)
        {
            try
            {
                using (var conn = new SqlConnection(connectstring))
                using (var cmd = new SqlCommand(
                    "SELECT MAKH AS [Mã KH], TENKH AS [Tên], DIACHI AS [Địa Chỉ], DT AS [SĐT], EMAIL AS [Email] " +
                    "FROM KHACHHANG WHERE TENKH LIKE @name", conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = "%" + name.Trim() + "%";

                    using (var adt = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adt.Fill(dt);
                        dataGridView2.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm khách hàng: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var key = textBox1.Text;
            if (string.IsNullOrWhiteSpace(key))
            {
                LoadAllCustomers();
            }
            else
            {
                SearchCustomerByName(key);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6.PerformClick();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            LoadAllCustomers();
            textBox1.Focus();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Mã khách hàng không được để trống!");
                    textBox5.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    MessageBox.Show("Tên khách hàng không được để trống!");
                    textBox6.Focus();
                    return;
                }

                // Validate phone number (10 or 11 digits or empty)
                string phoneNumber = textBox3.Text.Trim();
                string errorMessage;
                if (!ValidatePhoneNumber(phoneNumber, out errorMessage))
                {
                    MessageBox.Show(errorMessage);
                    textBox3.Focus();
                    return;
                }

                using (var conn = new SqlConnection(connectstring))
                {
                    string strAdd = "INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, DT, EMAIL) " +
                        "VALUES (@makh, @tenkh, @diachi, @dt, @email)";

                    using (var comm = new SqlCommand(strAdd, conn))
                    {
                        comm.Parameters.AddWithValue("@makh", textBox5.Text);
                        comm.Parameters.AddWithValue("@tenkh", textBox6.Text);
                        comm.Parameters.AddWithValue("@diachi", string.IsNullOrWhiteSpace(textBox4.Text) ? (object)DBNull.Value : textBox4.Text);
                        comm.Parameters.AddWithValue("@dt", string.IsNullOrWhiteSpace(textBox3.Text) ? (object)DBNull.Value : textBox3.Text);
                        comm.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(textBox2.Text) ? (object)DBNull.Value : textBox2.Text);

                        conn.Open();
                        int i = comm.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Thêm khách hàng thành công!");
                            LoadAllCustomers();
                        }
                        else
                        {
                            MessageBox.Show("Thêm khách hàng thất bại!");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) 
                {
                    MessageBox.Show("Không thể nhập trùng mã khách hàng!");
                }
                else
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Mã khách hàng không được để trống!");
                    textBox5.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    MessageBox.Show("Tên khách hàng không được để trống!");
                    textBox6.Focus();
                    return;
                }

                // Validate phone number (10 or 11 digits or empty)
                string phoneNumber = textBox3.Text.Trim();
                string errorMessage;
                if (!ValidatePhoneNumber(phoneNumber, out errorMessage))
                {
                    MessageBox.Show(errorMessage);
                    textBox3.Focus();
                    return;
                }

                using (var conn = new SqlConnection(connectstring))
                {
                    string strUpdate = "UPDATE KHACHHANG SET TENKH = @tenkh, DIACHI = @diachi, " +
                        "DT = @dt, EMAIL = @email WHERE MAKH = @makh";

                    using (var comm = new SqlCommand(strUpdate, conn))
                    {
                        comm.Parameters.AddWithValue("@tenkh", textBox6.Text);
                        comm.Parameters.AddWithValue("@diachi", string.IsNullOrWhiteSpace(textBox4.Text) ? (object)DBNull.Value : textBox4.Text);
                        comm.Parameters.AddWithValue("@dt", string.IsNullOrWhiteSpace(textBox3.Text) ? (object)DBNull.Value : textBox3.Text);
                        comm.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(textBox2.Text) ? (object)DBNull.Value : textBox2.Text);
                        comm.Parameters.AddWithValue("@makh", textBox5.Text);

                        conn.Open();
                        int i = comm.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Cập nhật khách hàng thành công!");
                            LoadAllCustomers();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật khách hàng thất bại! - không thể đổi mã khách hàng! ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SqlConnection(connectstring))
                {
                    string strDelete = "DELETE FROM KHACHHANG WHERE MAKH = @makh";
                    using (var comm = new SqlCommand(strDelete, conn))
                    {
                        comm.Parameters.AddWithValue("@makh", textBox5.Text);
                        conn.Open();
                        int i = comm.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Xóa khách hàng thành công!");
                            LoadAllCustomers();
                        }
                        else
                        {
                            MessageBox.Show("Xóa khách hàng thất bại! Mã khách hàng không tồn tại hoặc đã bị ràng buộc.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Vi phạm ràng buộc khóa ngoại
                {
                    MessageBox.Show("Không thể xóa khách hàng vì có liên quan đến bảng khác!");
                }
                else
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            textBox5.Text = dataGridView2.Rows[row].Cells["Mã KH"].Value?.ToString() ?? "";
            textBox6.Text = dataGridView2.Rows[row].Cells["Tên"].Value?.ToString() ?? "";
            textBox4.Text = dataGridView2.Rows[row].Cells["Địa Chỉ"].Value?.ToString() ?? "";
            textBox3.Text = dataGridView2.Rows[row].Cells["SĐT"].Value?.ToString() ?? "";
            textBox2.Text = dataGridView2.Rows[row].Cells["Email"].Value?.ToString() ?? "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }
    }
}