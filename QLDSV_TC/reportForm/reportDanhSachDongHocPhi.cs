using DevExpress.XtraReports.UI;
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

namespace QLDSV_TC.reportForm
{
    public partial class reportDanhSachDongHocPhi : Form
    {
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static String database = "QLDSV_TC";
        public int type;
        public reportDanhSachDongHocPhi()
        {
            InitializeComponent();
        }

        public static string NumberToText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            return "(" + result + (suffix ? " đồng chẵn)" : ")");
        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
           

        }

        private void reportDanhSachDongHocPhi_Load(object sender, EventArgs e)
        {
            if (Program.mGroup.Equals("PKT"))
            {
                label4.Hide();
                cbKhoa.Hide();
            }
            else 
            {
                cbKhoa.DataSource = Program.bds_khoa;
                cbKhoa.DisplayMember = "TENKHOA";
                cbKhoa.ValueMember = "TENSERVER";
                cbKhoa.SelectedIndex = Program.mChinhanh;
                if (Program.mGroup == "KHOA")
                {
                    cbKhoa.Enabled = false;
                }

                if (Program.mGroup.Equals("PGV") || Program.mGroup.Equals("KHOA"))
                {
                    if (KetNoiSql("ADMIN\\MSSQLSERVER03", Program.remotelogin, Program.remotepassword) == 0)
                    {
                        MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
                    }
                }
                if (Program.mGroup.Equals("PGV")) type = 0;
                else type = 1;
            }

            
            loadLOPcombobox();
            loadcbNienkhoa();
            cbNienKhoa.SelectedIndex = 0;

        }


        void loadcbNienkhoa()
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.SP_LIST_NIENKHOA";
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsNienKhoa = new BindingSource();
            bdsNienKhoa.DataSource = dt;
            cbNienKhoa.DataSource = bdsNienKhoa;
            cbNienKhoa.DisplayMember = "NIENKHOA";
            cbNienKhoa.ValueMember = "NIENKHOA";
        }

        void loadcbHocKi(string nienkhoa)
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.SP_LIST_HOCKY '" + nienkhoa + "'";
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsHocKi = new BindingSource();
            bdsHocKi.DataSource = dt;
            cbHocKi.DataSource = bdsHocKi;
            cbHocKi.DisplayMember = "HOCKY";
            cbHocKi.ValueMember = "HOCKY";
        }

        public static int KetNoiSql(string severname, string mlogin, string password)
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
            try
            {
                connstr = "Data Source=" + severname + ";Initial Catalog=" +
                    database + ";User ID=" +
                    mlogin + ";password=" + password;
                Console.WriteLine("this is " + connstr);
                conn.ConnectionString = connstr;
                conn.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu. \nBạn xem lại username và password.");
                return 0;
            }

        }

        void loadLOPcombobox()
        {
            DataTable dt = new DataTable();
            //  string cmd = "EXEC [dbo].[getAllLopByRole] "+type;
            string cmd = "select MALOP,TENLOP from LOP";
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdslh = new BindingSource();
            bdslh.DataSource = dt;
            cbLop.DataSource = bdslh;
            cbLop.DisplayMember = "MALOP";
            cbLop.ValueMember = "TENLOP";
        }
        private void btnExport_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

          /*  if (    cbNienKhoa.Text.Trim() == "")
            {
                MessageBox.Show("Niên khóa không được để trống", "", MessageBoxButtons.OK);
                cbNienKhoa.Focus();
                return;
            }
            if (cbHocKi.Value == 0)
            {
                MessageBox.Show("Học Kỳ không được để trống", "", MessageBoxButtons.OK);
                .Focus();
                return;
            }*/
            string nienkhoa = cbNienKhoa.Text;
            string hocky = cbHocKi.Text;
            string malop = cbLop.Text;
            string tongtien = "";
            string cmd = "SELECT TENKHOA FROM dbo.LOP,dbo.KHOA WHERE MALOP = '" + malop + "' AND KHOA.MAKHOA = LOP.MAKHOA";
            SqlDataReader reader = ExecSqlDataReader(cmd);
            reader.Read();
            string tenkhoa = reader.GetString(0);
            reader.Close();

            string cmd1 = "EXEC [dbo].[SP_SUM_HP_THEOLOP] '" + malop + "', '" + nienkhoa + "', " + hocky;
            SqlDataReader reader1 = ExecSqlDataReader(cmd1);
            reader1.Read();
            tongtien = reader1.GetInt32(0).ToString();
            reader1.Close();



            if (tongtien != "0")
            {
                tongtien = NumberToText(double.Parse(tongtien));
            }


            rptDanhSachDongHocPhi rpt = new rptDanhSachDongHocPhi(malop, nienkhoa, hocky);
            rpt.lbMaLop.Text = malop;
            rpt.lbKhoa.Text = tenkhoa;
            rpt.lbTienChu.Text = tongtien;


            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();



        }

        public static SqlDataReader ExecSqlDataReader(string strlenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strlenh, conn);
            sqlcmd.CommandType = CommandType.Text;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        private void btnThoat_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKhoa.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cbKhoa.SelectedValue.ToString();
            if (cbKhoa.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {
                loadLOPcombobox();
                loadcbNienkhoa();
                cbNienKhoa.SelectedIndex = 0;
            }
        }

        private void cbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadcbHocKi(cbNienKhoa.Text);
        }
    }
}
