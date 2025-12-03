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
            LoadAllInvoices();
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
            else if (tabControl1.SelectedIndex == 2)
            {
                LoadAllInvoices();
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

        // ========== HOÁ ĐƠN (INVOICE) TAB METHODS ==========

        private void LoadAllInvoices()
        {
            try
            {
                using (var conn = new SqlConnection(connectstring))
                using (var cmd = new SqlCommand(
                    @"SELECT 
                        c.MAHD AS [Mã HĐ], 
                        k.TENKH AS [Tên KH], 
                        v.TENVT AS [Vật tư], 
                        c.SL AS [Số lượng], 
                        c.GIABAN AS [Giá bán], 
                        ISNULL(c.KHUYENMAI, 0) AS [Khuyến mãi]
                    FROM CTHD c
                    INNER JOIN HOADON h ON c.MAHD = h.MAHD
                    INNER JOIN KHACHHANG k ON h.MAKH = k.MAKH
                    INNER JOIN VATTU v ON c.MAVT = v.MAVT",
                    conn))
                using (var adt = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adt.Fill(dt);
                    dataGridView3.DataSource = dt;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu hoá đơn: " + ex.Message);
            }
        }

        private void SearchInvoiceByCode(string code)
        {
            try
            {
                using (var conn = new SqlConnection(connectstring))
                using (var cmd = new SqlCommand(
                    @"SELECT 
                        c.MAHD AS [Mã HĐ], 
                        k.TENKH AS [Tên KH], 
                        v.TENVT AS [Vật tư], 
                        c.SL AS [Số lượng], 
                        c.GIABAN AS [Giá bán], 
                        ISNULL(c.KHUYENMAI, 0) AS [Khuyến mãi]
                    FROM CTHD c
                    INNER JOIN HOADON h ON c.MAHD = h.MAHD
                    INNER JOIN KHACHHANG k ON h.MAKH = k.MAKH
                    INNER JOIN VATTU v ON c.MAVT = v.MAVT
                    WHERE c.MAHD LIKE @code",
                    conn))
                {
                    cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = "%" + code.Trim() + "%";

                    using (var adt = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adt.Fill(dt);
                        dataGridView3.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm hoá đơn: " + ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var key = textBox7.Text;
            if (string.IsNullOrWhiteSpace(key))
            {
                LoadAllInvoices();
            }
            else
            {
                SearchInvoiceByCode(key);
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button12.PerformClick();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            LoadAllInvoices();
            textBox7.Focus();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    MessageBox.Show("Mã hoá đơn không được để trống!");
                    textBox11.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox12.Text))
                {
                    MessageBox.Show("Tên khách hàng không được để trống!");
                    textBox12.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    MessageBox.Show("Vật tư không được để trống!");
                    textBox10.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    MessageBox.Show("Số lượng không được để trống!");
                    textBox9.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    MessageBox.Show("Giá bán không được để trống!");
                    textBox8.Focus();
                    return;
                }

                int quantity;
                if (!int.TryParse(textBox9.Text, out quantity) || quantity <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương!");
                    textBox9.Focus();
                    return;
                }

                float salePrice;
                if (!float.TryParse(textBox8.Text, out salePrice) || salePrice <= 0)
                {
                    MessageBox.Show("Giá bán phải là số dương!");
                    textBox8.Focus();
                    return;
                }

                float discount = 0;
                if (!string.IsNullOrWhiteSpace(textBox13.Text))
                {
                    if (!float.TryParse(textBox13.Text, out discount) || discount < 0 || discount > 100)
                    {
                        MessageBox.Show("Khuyến mãi phải từ 0 đến 100!");
                        textBox13.Focus();
                        return;
                    }
                }

                using (var conn = new SqlConnection(connectstring))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Get MAKH from TENKH
                            string makh = "";
                            using (var cmdGetMakh = new SqlCommand(
                                "SELECT TOP 1 MAKH FROM KHACHHANG WHERE TENKH = @tenkh", conn, transaction))
                            {
                                cmdGetMakh.Parameters.AddWithValue("@tenkh", textBox12.Text);
                                var result = cmdGetMakh.ExecuteScalar();
                                if (result == null)
                                {
                                    MessageBox.Show("Tên khách hàng không tồn tại trong hệ thống! Vui lòng thêm khách hàng trước.");
                                    transaction.Rollback();
                                    return;
                                }
                                makh = result.ToString();
                            }

                            // Get MAVT from TENVT
                            string mavt = "";
                            using (var cmdGetMavt = new SqlCommand(
                                "SELECT TOP 1 MAVT FROM VATTU WHERE TENVT = @tenvt", conn, transaction))
                            {
                                cmdGetMavt.Parameters.AddWithValue("@tenvt", textBox10.Text);
                                var result = cmdGetMavt.ExecuteScalar();
                                if (result == null)
                                {
                                    MessageBox.Show("Tên vật tư không tồn tại trong hệ thống! Vui lòng thêm vật tư trước.");
                                    transaction.Rollback();
                                    return;
                                }
                                mavt = result.ToString();
                            }

                            // Check if HOADON exists, if not create it
                            using (var cmdCheckHoadon = new SqlCommand(
                                "SELECT COUNT(*) FROM HOADON WHERE MAHD = @mahd", conn, transaction))
                            {
                                cmdCheckHoadon.Parameters.AddWithValue("@mahd", textBox11.Text);
                                int count = (int)cmdCheckHoadon.ExecuteScalar();
                                if (count == 0)
                                {
                                    // Create new HOADON
                                    using (var cmdInsertHoadon = new SqlCommand(
                                        "INSERT INTO HOADON (MAHD, NGAY, MAKH, TONGTG) VALUES (@mahd, GETDATE(), @makh, 0)",
                                        conn, transaction))
                                    {
                                        cmdInsertHoadon.Parameters.AddWithValue("@mahd", textBox11.Text);
                                        cmdInsertHoadon.Parameters.AddWithValue("@makh", makh);
                                        cmdInsertHoadon.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // Verify HOADON belongs to the same customer
                                    using (var cmdCheckMakh = new SqlCommand(
                                        "SELECT MAKH FROM HOADON WHERE MAHD = @mahd", conn, transaction))
                                    {
                                        cmdCheckMakh.Parameters.AddWithValue("@mahd", textBox11.Text);
                                        var existingMakh = cmdCheckMakh.ExecuteScalar()?.ToString();
                                        if (existingMakh != makh)
                                        {
                                            MessageBox.Show("Mã hoá đơn này đã tồn tại và thuộc về khách hàng khác! Vui lòng sử dụng mã hoá đơn khác.");
                                            transaction.Rollback();
                                            return;
                                        }
                                    }
                                }
                            }

                            // Check if CTHD already exists
                            using (var cmdCheckCthd = new SqlCommand(
                                "SELECT COUNT(*) FROM CTHD WHERE MAHD = @mahd AND MAVT = @mavt", conn, transaction))
                            {
                                cmdCheckCthd.Parameters.AddWithValue("@mahd", textBox11.Text);
                                cmdCheckCthd.Parameters.AddWithValue("@mavt", mavt);
                                int cthdCount = (int)cmdCheckCthd.ExecuteScalar();
                                if (cthdCount > 0)
                                {
                                    MessageBox.Show("Chi tiết hoá đơn này đã tồn tại! Vui lòng sử dụng chức năng Sửa để cập nhật.");
                                    transaction.Rollback();
                                    return;
                                }
                            }

                            // Insert CTHD
                            using (var cmdInsertCthd = new SqlCommand(
                                "INSERT INTO CTHD (MAHD, MAVT, SL, KHUYENMAI, GIABAN) " +
                                "VALUES (@mahd, @mavt, @sl, @khuyenmai, @giaban)",
                                conn, transaction))
                            {
                                cmdInsertCthd.Parameters.AddWithValue("@mahd", textBox11.Text);
                                cmdInsertCthd.Parameters.AddWithValue("@mavt", mavt);
                                cmdInsertCthd.Parameters.AddWithValue("@sl", quantity);
                                cmdInsertCthd.Parameters.AddWithValue("@khuyenmai", discount);
                                cmdInsertCthd.Parameters.AddWithValue("@giaban", salePrice);
                                cmdInsertCthd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Thêm chi tiết hoá đơn thành công!");
                            LoadAllInvoices();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Chi tiết hoá đơn này đã tồn tại!");
                }
                else
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    MessageBox.Show("Mã hoá đơn không được để trống!");
                    textBox11.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    MessageBox.Show("Vật tư không được để trống!");
                    textBox10.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    MessageBox.Show("Số lượng không được để trống!");
                    textBox9.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    MessageBox.Show("Giá bán không được để trống!");
                    textBox8.Focus();
                    return;
                }

                int quantity;
                if (!int.TryParse(textBox9.Text, out quantity) || quantity <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương!");
                    textBox9.Focus();
                    return;
                }

                float salePrice;
                if (!float.TryParse(textBox8.Text, out salePrice) || salePrice <= 0)
                {
                    MessageBox.Show("Giá bán phải là số dương!");
                    textBox8.Focus();
                    return;
                }

                float discount = 0;
                if (!string.IsNullOrWhiteSpace(textBox13.Text))
                {
                    if (!float.TryParse(textBox13.Text, out discount) || discount < 0 || discount > 100)
                    {
                        MessageBox.Show("Khuyến mãi phải từ 0 đến 100!");
                        textBox13.Focus();
                        return;
                    }
                }

                using (var conn = new SqlConnection(connectstring))
                {
                    conn.Open();

                    // Get MAVT from TENVT
                    string mavt = "";
                    using (var cmdGetMavt = new SqlCommand(
                        "SELECT TOP 1 MAVT FROM VATTU WHERE TENVT = @tenvt", conn))
                    {
                        cmdGetMavt.Parameters.AddWithValue("@tenvt", textBox10.Text);
                        var result = cmdGetMavt.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Tên vật tư không tồn tại trong hệ thống!");
                            return;
                        }
                        mavt = result.ToString();
                    }

                    // Update CTHD - only allow updating SL, KHUYENMAI, GIABAN
                    // Cannot change MAHD or MAVT (primary key)
                    string strUpdate = "UPDATE CTHD SET SL = @sl, KHUYENMAI = @khuyenmai, GIABAN = @giaban " +
                        "WHERE MAHD = @mahd AND MAVT = @mavt";

                    using (var comm = new SqlCommand(strUpdate, conn))
                    {
                        comm.Parameters.AddWithValue("@sl", quantity);
                        comm.Parameters.AddWithValue("@khuyenmai", discount);
                        comm.Parameters.AddWithValue("@giaban", salePrice);
                        comm.Parameters.AddWithValue("@mahd", textBox11.Text);
                        comm.Parameters.AddWithValue("@mavt", mavt);

                        int i = comm.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Cập nhật chi tiết hoá đơn thành công!");
                            LoadAllInvoices();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật chi tiết hoá đơn thất bại! Không tìm thấy chi tiết hoá đơn với Mã HĐ và Vật tư này.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoá đơn cần xóa!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    MessageBox.Show("Vui lòng chọn vật tư cần xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa chi tiết hoá đơn này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var conn = new SqlConnection(connectstring))
                    {
                        conn.Open();

                        // Get MAVT from TENVT
                        string mavt = "";
                        using (var cmdGetMavt = new SqlCommand(
                            "SELECT TOP 1 MAVT FROM VATTU WHERE TENVT = @tenvt", conn))
                        {
                            cmdGetMavt.Parameters.AddWithValue("@tenvt", textBox10.Text);
                            var mavtResult = cmdGetMavt.ExecuteScalar();
                            if (mavtResult == null)
                            {
                                MessageBox.Show("Tên vật tư không tồn tại trong hệ thống!");
                                return;
                            }
                            mavt = mavtResult.ToString();
                        }

                        string strDelete = "DELETE FROM CTHD WHERE MAHD = @mahd AND MAVT = @mavt";
                        using (var comm = new SqlCommand(strDelete, conn))
                        {
                            comm.Parameters.AddWithValue("@mahd", textBox11.Text);
                            comm.Parameters.AddWithValue("@mavt", mavt);
                            int i = comm.ExecuteNonQuery();
                            if (i > 0)
                            {
                                MessageBox.Show("Xóa chi tiết hoá đơn thành công!");
                                LoadAllInvoices();
                            }
                            else
                            {
                                MessageBox.Show("Xóa chi tiết hoá đơn thất bại! Không tìm thấy chi tiết hoá đơn với Mã HĐ và Vật tư này.");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Không thể xóa chi tiết hoá đơn vì có liên quan đến bảng khác!");
                }
                else
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        private void dataGridView3_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            textBox11.Text = dataGridView3.Rows[row].Cells["Mã HĐ"].Value?.ToString() ?? "";
            textBox12.Text = dataGridView3.Rows[row].Cells["Tên KH"].Value?.ToString() ?? "";
            textBox10.Text = dataGridView3.Rows[row].Cells["Vật tư"].Value?.ToString() ?? "";
            textBox9.Text = dataGridView3.Rows[row].Cells["Số lượng"].Value?.ToString() ?? "";
            textBox8.Text = dataGridView3.Rows[row].Cells["Giá bán"].Value?.ToString() ?? "";
            textBox13.Text = dataGridView3.Rows[row].Cells["Khuyến mãi"].Value?.ToString() ?? "0";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}