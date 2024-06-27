using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC
{
    public partial class frmDangNhap : Form
    {
        private SqlConnection conn_publisher = new SqlConnection();

        private bool isSinhVien = false;

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void LayDSPM(String cmd)
        {
            DataTable dt = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            Program.test.DataSource = dt;
            Program.bds_dspm.DataSource = dt;
            cbChiNhanh.DataSource = Program.bds_dspm;
            cbChiNhanh.DisplayMember = "TENKHOA";
            cbChiNhanh.ValueMember = "TENSERVER";
        }


        private void LayDSKHOA(String cmd)
        {
            DataTable dt = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            Program.test.DataSource = dt;
            Program.bds_khoa.DataSource = dt;
           
        }

        private int KetNoi_CSDLGOC()
        {
            if (conn_publisher != null && conn_publisher.State == ConnectionState.Open)
                conn_publisher.Close();
            try
            {
                conn_publisher.ConnectionString = Program.connstr_publisher;
                conn_publisher.Open();
                return 1;
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Lỗi kết nối tới CSDL gốc!!!" + e.Message);
                return 0;
            }
        }


        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //    Console.WriteLine(txbTaiKhoan.Text);
            //    Console.WriteLine(txbMatKhau.Text);
            if (isSinhVien == false)
            {
                if (txbTaiKhoan.Text.Trim() == "" || txbMatKhau.Text.Trim() == "")
                {
                    XtraMessageBox.Show("Login name và mật khẩu không được trống", "", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    Program.mlogin = txbTaiKhoan.Text; Program.password = txbMatKhau.Text;
                    if (Program.KetNoi() == 0) return;

                    Program.mChinhanh = cbChiNhanh.SelectedIndex;
                    Program.mloginDN = Program.mlogin;
                    Program.passwordDN = Program.password;

                    string strLenh = "EXEC dbo.SP_DANGNHAP '" + Program.mlogin + "'";
                    Console.WriteLine(cbChiNhanh.SelectedValue);

                    Program.myReader = Program.ExecSqlDataReader(strLenh);
                    if (Program.myReader == null) return;
                    Program.myReader.Read(); // Đọc 1 dòng nếu dữ liệu có nhiều dùng thì dùng for lặp nếu null thì break

                    Program.mGroup = Program.myReader.GetString(2);
                    Program.mHoten = Program.myReader.GetString(1);
                    Program.username = Program.myReader.GetString(0);
                    Program.myReader.Close();

                }
            }
            else
            {
                if (txbTaiKhoan.Text.Trim() == "" || txbMatKhau.Text.Trim() == "")
                {
                    XtraMessageBox.Show("Login name và mật khẩu không được trống", "", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    Console.WriteLine("eewqqw");
                    Program.mlogin = "SVKN";
                    Program.password = "123456";
                  
                    if (Program.KetNoi() == 0) return;


                    string strlenh1 = "EXEC [dbo].[SP_SinhVienDangNhap] '" + txbTaiKhoan.Text + "', '" + txbMatKhau.Text + "'";
                    Console.WriteLine("THIS IS " + strlenh1);
                    SqlDataReader reader = Program.ExecSqlDataReader(strlenh1);

                    if (reader.HasRows == false)
                    {
                        Console.WriteLine("true");
                        XtraMessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!!!");
                        return;
                    }

                    reader.Read();

                    if (Convert.IsDBNull(Program.username))
                    {
                        XtraMessageBox.Show("Tài khoản không có quyền truy cập dữ liệu", "", MessageBoxButtons.OK);
                        return;
                    }
                    Program.mGroup = "SV";
                    Program.mHoten = reader.GetString(1);
                    Program.username = reader.GetString(0);
                    reader.Close();


                }
            }
            Program.conn.Close();
            XtraMessageBox.Show("Đăng nhập thành công !!!");
            frmChinh FormChinh = new frmChinh();
            FormChinh.Show();
            this.Hide();

        }


        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txbTaiKhoan.Text = "pgv";
            txbMatKhau.Text = "123456";
            if (KetNoi_CSDLGOC() == 0) return;
            LayDSPM("SELECT * FROM V_DSPM");
            LayDSKHOA("SELECT * FROM V_DSKHOA");
            // cbChiNhanh.SelectedIndex = 1;
            cbChiNhanh.SelectedIndex = 0;
            Program.servername = cbChiNhanh.SelectedValue.ToString();

        }


        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.servername = cbChiNhanh.SelectedValue.ToString();
              //  Console.WriteLine(cmbCHINHANH.SelectedValue.ToString());
            }
            catch (Exception)
            {

            }
        }






        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isSinhVien = !isSinhVien;
        }
    }
}
