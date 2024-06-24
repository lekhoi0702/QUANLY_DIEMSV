using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC
{
    public partial class frmDangKyMon : DevExpress.XtraEditors.XtraForm
    {

        private BindingSource bdsSinhVien = new BindingSource();
        private BindingSource bdsHUYDANGKY = new BindingSource();
        private BindingSource bdsLopTinchi = new BindingSource();


        



       
        public frmDangKyMon()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sINHVIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsSINHVIEN.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmDangKyMon_Load(object sender, EventArgs e)
        {
            loadcbNienkhoa();
            LoadSinhVienInfo();
            Console.WriteLine("this is load " + cbNienKhoa.Text);
            cbNienKhoa.SelectedIndex = 0;
            this.btnDangKy.Visible = false;
      

        }


       /* private void btnSearchSinhVien_Click(object sender, EventArgs e)
        {
            if (txbMaSV.Text.Trim() == "")
            {
                XtraMessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                txbMaSV.Focus();
                return;
            }
            if (txbMaSV.Text != Program.username)
            {
                XtraMessageBox.Show("Bạn không phải là tài khoản sinh viên này!", "", MessageBoxButtons.OK);
                txbMaSV.Focus();
                return;
            }
           
            //     string cmd = "EXEC dbo.SP_getInfoSVDKI '" + txbMaSV.Text + "'";
            string cmd1 = "EXEC dbo.SP_LIST_SVHUYDANGKY '" + txbMaSV.Text + "'";
            //   DataTable tableSV = Program.ExecSqlDataTable(cmd);
            DataTable tableDSLTC_HUY = Program.ExecSqlDataTable(cmd1);

            //   this.bdsSinhVien.DataSource = tableSV;
            this.bdsHUYDANGKY.DataSource = tableDSLTC_HUY;
            //    this.SINHVIENgridControl.DataSource = this.bdsSinhVien;
            this.gcHUYDANGKY.DataSource = this.bdsHUYDANGKY;
           
        }*/


        private void LoadSinhVienInfo()
        {
    

            string cmd1 = "EXEC dbo.SP_LIST_SVHUYDANGKY '" + Program.username + "'";
            DataTable tableDSLTC_HUY = Program.ExecSqlDataTable(cmd1);
            this.bdsHUYDANGKY.DataSource = tableDSLTC_HUY;
            this.gcHUYDANGKY.DataSource = this.bdsHUYDANGKY;


        }
        void loadcbNienkhoa()
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC SP_LIST_NIENKHOA";
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsNienKhoa = new BindingSource();
            bdsNienKhoa.DataSource = dt;


            cbNienKhoa.DataSource = bdsNienKhoa;
            cbNienKhoa.DisplayMember = "NIENKHOA";
            cbNienKhoa.ValueMember = "NIENKHOA";
            Console.WriteLine("this is load func " + cbNienKhoa.Text);
        }


        private void btnSearchLopTinChi_Click(object sender, EventArgs e)
        {
            /*string query = "EXEC [dbo].[GetMaKhoaSinhVien] '" + Program.username + "'";
            Program.myReader = Program.ExecSqlDataReader(query);
            if (Program.myReader == null)
            {
                return;
            }
            Program.myReader.Read();
            Program.maKhoa = Program.myReader.GetValue(0).ToString();
            Program.myReader.Close();
            Console.WriteLine("ma khoa = " + Program.maKhoa);*/


            string cmd = "EXEC [dbo].[SP_InDanhSachLopTinChi] '" + cbNienKhoa.Text + "', '" + cbHocKi.Text + "'";
            DataTable tableLopTC = Program.ExecSqlDataTable(cmd);
            this.bdsLopTinchi.DataSource = tableLopTC;
            this.gcLOPTINCHI.DataSource = this.bdsLopTinchi;
            this.btnDangKy.Visible = true;
        }



        void loadcbHocKi(string nienkhoa)
        {
            Console.WriteLine(cbNienKhoa.Text);
            Console.WriteLine(nienkhoa);
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.SP_LIST_HOCKY '" + nienkhoa + "'";
            Console.WriteLine(cmd);
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsHocKi = new BindingSource();
            bdsHocKi.DataSource = dt;

            cbHocKi.DataSource = bdsHocKi;
            cbHocKi.DisplayMember = "HOCKY";
            cbHocKi.ValueMember = "HOCKY";
            // cbHocKi.SelectedIndex = 0;
        }
        private void cbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*    loadcbNienkhoa();   
              cbNienKhoa.SelectedIndex = 0;

          
                cbHocKi.Items[1]= */
            loadcbHocKi(cbNienKhoa.Text);
            Console.WriteLine("this is indexchange " + cbNienKhoa.Text);
        }



        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Program.username.Trim() == "")
            {
                XtraMessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                return;
            }
            if (bdsHUYDANGKY.Position < 0)
            {
                XtraMessageBox.Show("Bạn chưa chọn lớp tín chỉ để hủy");
                gcHUYDANGKY.Focus();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn hủy đăng kí lớp học này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string maltc = "";
                if (((DataRowView)bdsHUYDANGKY[bdsHUYDANGKY.Position])["MALTC"] != null)
                {
                    maltc = ((DataRowView)bdsHUYDANGKY[bdsHUYDANGKY.Position])["MALTC"].ToString();
                }

                string cmd = "EXEC [dbo].[SP_XULY_LTC] '" + Program.username + "' , '" + maltc + "', " + 2;
                if (Program.ExecSqlNonQuery(cmd) == 0)
                {
                    XtraMessageBox.Show("Hủy đăng kí thành công!");
                    string cmd1 = "EXEC dbo.SP_LIST_SVHUYDANGKY '" + Program.username + "'";
                    DataTable tableDSLTC_HUY = Program.ExecSqlDataTable(cmd1);
                    this.bdsHUYDANGKY.DataSource = tableDSLTC_HUY;

                    this.gcHUYDANGKY.DataSource = this.bdsHUYDANGKY;
                }
                else
                {
                    XtraMessageBox.Show("Hủy đăng kí thất bại");
                }
            }
            else
            {
                return;
            }
        }




        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataRowView drv = ((DataRowView)bdsLopTinchi[bdsLopTinchi.Position]);
            String maLTC = drv["MALTC"].ToString().Trim();
            Console.WriteLine("ma lop tin chi: " + maLTC);
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn đăng kí lớp học này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string cmd = "EXEC [dbo].[SP_XULY_LTC] '" + Program.username + "' , '" + maLTC + "', " + 1;
                Console.WriteLine("query:" + cmd);
                if (Program.ExecSqlNonQuery(cmd) == 0)
                {
                    XtraMessageBox.Show("Đăng kí thành công!");
                    string cmd1 = "EXEC dbo.SP_LIST_SVHUYDANGKY '" + Program.username + "'";
                    DataTable tableDSLTC_HUY = Program.ExecSqlDataTable(cmd1);
                    this.bdsHUYDANGKY.DataSource = tableDSLTC_HUY;
                    this.gcHUYDANGKY.DataSource = this.bdsHUYDANGKY;
                    this.gcLOPTINCHI.DataSource = this.bdsLopTinchi;
                }
                else
                {
                    XtraMessageBox.Show("Đăng kí thất bại");
                }
            }
            else
            {
                return;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void gcLOPTINCHI_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
    }


}