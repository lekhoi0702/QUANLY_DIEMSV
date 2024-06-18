using DevExpress.XtraBars;
using QLDSV_TC.form;
using QLDSV_TC.reportForm;
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
    public partial class frmChinh : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private SqlConnection conn_publisher = new SqlConnection();
        public frmChinh()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        public void MainForm_Load(object sender, EventArgs e)
        {
            


            this.MAGV.Text = "MÃ GV: " + Program.username;
            this.HOTEN.Text ="HỌ TÊN: " +Program.mHoten;
            this.NHOM.Text ="NHÓM: "  + Program.mGroup;
            Console.WriteLine(this.NHOM.Text);

            if (Program.mGroup == "SV")
            {
                this.MAGV.Text = "MÃ SV: " + Program.username;
                this.rbQuanLy.Visible = false;
                this.rbBaoCao.Visible = false;
                this.btnThemTaiKhoan.Enabled = false;
            }
            if (Program.mGroup == "PGV" || Program.mGroup == "KHOA")
            {
                this.rbSinhVien.Visible = false;
                this.grKeToan.Visible = false;
            }
            if (Program.mGroup == "PKT")
            {
                this.rbSinhVien.Visible = false;
                this.grQuanTri.Visible = false;
                btnBDMH_rp.Enabled = btnDTK_rp.Enabled = btnDSLTC_rp.Enabled = btnDSSVLTC_rp.Enabled = btnDSV_rp.Enabled = false;
            }

        }
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmNhapDiem));
            if (frm != null) frm.Activate();
            else
            {
                frmNhapDiem f = new frmNhapDiem();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDongHocPhi));
            if (frm != null) frm.Activate();
            else
            {
                frmDongHocPhi f = new frmDongHocPhi();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmLopHoc));
            if (frm != null) frm.Activate();
            else
            {
                frmLopHoc f = new frmLopHoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmSinhVien));
            if (frm != null) frm.Activate();
            else
            {
                frmSinhVien f = new frmSinhVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmMonHoc));
            if (frm != null) frm.Activate();
            else
            {
                frmMonHoc f = new frmMonHoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmLopTinChi));
            if (frm != null) frm.Activate();
            else
            {
                frmLopTinChi f = new frmLopTinChi();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDSLTC_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(reportDanhsachLopTinChi));
            if (frm != null) frm.Activate();
            else
            {
                reportDanhsachLopTinChi f = new reportDanhsachLopTinChi();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem5_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(reportDiemMonHoc));
            if (frm != null) frm.Activate();
            else
            {
                reportDiemMonHoc f = new reportDiemMonHoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem7_ItemClick_1(object sender, ItemClickEventArgs e)
        {

            Form frm = this.CheckExists(typeof(reportSinhVienDangKy));
            if (frm != null) frm.Activate();
            else
            {
                reportSinhVienDangKy f = new reportSinhVienDangKy();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(reportDanhSachDongHocPhi));
            if (frm != null) frm.Activate();
            else
            {
                reportDanhSachDongHocPhi f = new reportDanhSachDongHocPhi();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(reportDiemSinhVien));
            if (frm != null) frm.Activate();
            else
            {
                reportDiemSinhVien f = new reportDiemSinhVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDTK_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(reportBangDiemTongKet));
            if (frm != null) frm.Activate();
            else
            {
                reportBangDiemTongKet f = new reportBangDiemTongKet();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDangKyMon_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDangKyMon));
            if (frm != null) frm.Activate();
            else
            {
                frmDangKyMon f = new frmDangKyMon();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnThemTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmTaoTaiKhoan));
            if (frm != null) frm.Activate();
            else
            {
                frmTaoTaiKhoan f = new frmTaoTaiKhoan();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDangXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            Form frmDangNhap = new frmDangNhap();
            frmDangNhap.Show();
            

            
        }

        private void btnXemDiem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDiemSinhVien));
            if (frm != null) frm.Activate();
            else
            {
                frmDiemSinhVien f = new frmDiemSinhVien();
                f.MdiParent = this;
                f.Show();
            }
        }
    }
}