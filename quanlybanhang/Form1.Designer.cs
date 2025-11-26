namespace quanlybanhang
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDVT = new System.Windows.Forms.TextBox();
            this.txtSLTON = new System.Windows.Forms.TextBox();
            this.txtGIAMUA = new System.Windows.Forms.TextBox();
            this.txtTENVT = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtMAVT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 176);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(609, 358);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(384, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 47);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quản Lý Vật Tư";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDVT);
            this.groupBox1.Controls.Add(this.txtSLTON);
            this.groupBox1.Controls.Add(this.txtGIAMUA);
            this.groupBox1.Controls.Add(this.txtMAVT);
            this.groupBox1.Controls.Add(this.txtTENVT);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(658, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 428);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin vật tư";
            // 
            // txtDVT
            // 
            this.txtDVT.Location = new System.Drawing.Point(318, 169);
            this.txtDVT.Name = "txtDVT";
            this.txtDVT.Size = new System.Drawing.Size(119, 29);
            this.txtDVT.TabIndex = 1;
            // 
            // txtSLTON
            // 
            this.txtSLTON.Location = new System.Drawing.Point(90, 169);
            this.txtSLTON.Name = "txtSLTON";
            this.txtSLTON.Size = new System.Drawing.Size(119, 29);
            this.txtSLTON.TabIndex = 1;
            // 
            // txtGIAMUA
            // 
            this.txtGIAMUA.Location = new System.Drawing.Point(318, 97);
            this.txtGIAMUA.Name = "txtGIAMUA";
            this.txtGIAMUA.Size = new System.Drawing.Size(119, 29);
            this.txtGIAMUA.TabIndex = 1;
            // 
            // txtTENVT
            // 
            this.txtTENVT.Location = new System.Drawing.Point(90, 97);
            this.txtTENVT.Name = "txtTENVT";
            this.txtTENVT.Size = new System.Drawing.Size(119, 29);
            this.txtTENVT.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(108, 262);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 33);
            this.button4.TabIndex = 0;
            this.button4.Text = "Xoá";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(298, 262);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 0;
            this.button2.Text = "Sửa";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 262);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Thêm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.txtSearch);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 60);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tìm vật tư";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(482, 25);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 29);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Xoá";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(401, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 29);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(75, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(311, 29);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số lượng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Loại:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(279, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "Giá:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(362, 378);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 33);
            this.button3.TabIndex = 0;
            this.button3.Text = "Thoát";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtMAVT
            // 
            this.txtMAVT.Location = new System.Drawing.Point(193, 31);
            this.txtMAVT.Name = "txtMAVT";
            this.txtMAVT.Size = new System.Drawing.Size(119, 29);
            this.txtMAVT.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(127, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = "Mã:";
            this.label6.Click += new System.EventHandler(this.label2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtDVT;
        private System.Windows.Forms.TextBox txtSLTON;
        private System.Windows.Forms.TextBox txtGIAMUA;
        private System.Windows.Forms.TextBox txtTENVT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtMAVT;
        private System.Windows.Forms.Label label6;
    }
}

