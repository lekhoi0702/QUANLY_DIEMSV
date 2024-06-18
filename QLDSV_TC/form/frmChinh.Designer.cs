
namespace QLDSV_TC
{
    partial class frmChinh
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
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.btnDiem = new DevExpress.XtraBars.BarButtonItem();
            this.btnDSLTC_rp = new DevExpress.XtraBars.BarButtonItem();
            this.btnBDMH_rp = new DevExpress.XtraBars.BarButtonItem();
            this.btnDSSVLTC_rp = new DevExpress.XtraBars.BarButtonItem();
            this.btnHP = new DevExpress.XtraBars.BarButtonItem();
            this.btnDSV_rp = new DevExpress.XtraBars.BarButtonItem();
            this.btnDTK_rp = new DevExpress.XtraBars.BarButtonItem();
            this.btnDangKyMon = new DevExpress.XtraBars.BarButtonItem();
            this.btnThemTaiKhoan = new DevExpress.XtraBars.BarButtonItem();
            this.btnDangXuat = new DevExpress.XtraBars.BarButtonItem();
            this.btnXemDiem = new DevExpress.XtraBars.BarButtonItem();
            this.rbQuanLy = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grQuanTri = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grKeToan = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rbSinhVien = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rbBaoCao = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MAGV = new System.Windows.Forms.ToolStripStatusLabel();
            this.HOTEN = new System.Windows.Forms.ToolStripStatusLabel();
            this.NHOM = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem6,
            this.btnDiem,
            this.btnDSLTC_rp,
            this.btnBDMH_rp,
            this.btnDSSVLTC_rp,
            this.btnHP,
            this.btnDSV_rp,
            this.btnDTK_rp,
            this.btnDangKyMon,
            this.btnThemTaiKhoan,
            this.btnDangXuat,
            this.btnXemDiem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Margin = new System.Windows.Forms.Padding(12);
            this.ribbon.MaxItemId = 20;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rbQuanLy,
            this.rbSinhVien,
            this.rbBaoCao,
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1057, 197);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "SINH VIÊN";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.ImageOptions.Image = global::QLDSV_TC.Properties.Resources.graduated;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "LỚP HỌC";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.ImageOptions.Image = global::QLDSV_TC.Properties.Resources.classroom;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "MÔN HỌC";
            this.barButtonItem3.Id = 3;
            this.barButtonItem3.ImageOptions.Image = global::QLDSV_TC.Properties.Resources.math_book;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "LỚP TÍN CHỈ";
            this.barButtonItem4.Id = 4;
            this.barButtonItem4.ImageOptions.Image = global::QLDSV_TC.Properties.Resources.classroom__1_;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "HỌC PHÍ";
            this.barButtonItem6.Id = 6;
            this.barButtonItem6.ImageOptions.Image = global::QLDSV_TC.Properties.Resources.tuition;
            this.barButtonItem6.ItemAppearance.Disabled.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonItem6.ItemAppearance.Disabled.Options.UseFont = true;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem6_ItemClick);
            // 
            // btnDiem
            // 
            this.btnDiem.Caption = "ĐIỂM";
            this.btnDiem.Id = 9;
            this.btnDiem.ImageOptions.Image = global::QLDSV_TC.Properties.Resources.test;
            this.btnDiem.Name = "btnDiem";
            this.btnDiem.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnDiem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // btnDSLTC_rp
            // 
            this.btnDSLTC_rp.Caption = "DANH SÁCH LỚP TÍN CHỈ";
            this.btnDSLTC_rp.Id = 10;
            this.btnDSLTC_rp.ImageOptions.SvgImage = global::QLDSV_TC.Properties.Resources.reportlayoutpivottable;
            this.btnDSLTC_rp.Name = "btnDSLTC_rp";
            this.btnDSLTC_rp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDSLTC_ItemClick);
            // 
            // btnBDMH_rp
            // 
            this.btnBDMH_rp.Caption = "BẢNG ĐIỂM MÔN HỌC";
            this.btnBDMH_rp.Id = 11;
            this.btnBDMH_rp.ImageOptions.SvgImage = global::QLDSV_TC.Properties.Resources.reportlayoutpivottable1;
            this.btnBDMH_rp.Name = "btnBDMH_rp";
            this.btnBDMH_rp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick_1);
            // 
            // btnDSSVLTC_rp
            // 
            this.btnDSSVLTC_rp.Caption = "DANH SÁCH SV ĐĂNG KÝ LTC";
            this.btnDSSVLTC_rp.Id = 12;
            this.btnDSSVLTC_rp.ImageOptions.SvgImage = global::QLDSV_TC.Properties.Resources.reportlayoutpivottable2;
            this.btnDSSVLTC_rp.Name = "btnDSSVLTC_rp";
            this.btnDSSVLTC_rp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem7_ItemClick_1);
            // 
            // btnHP
            // 
            this.btnHP.Caption = "DANH SÁCH ĐÓNG HỌC PHÍ";
            this.btnHP.Id = 13;
            this.btnHP.ImageOptions.SvgImage = global::QLDSV_TC.Properties.Resources.reportlayoutpivottable3;
            this.btnHP.Name = "btnHP";
            this.btnHP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem8_ItemClick);
            // 
            // btnDSV_rp
            // 
            this.btnDSV_rp.Caption = "BẢNG ĐIỂM SINH VIÊN";
            this.btnDSV_rp.Id = 14;
            this.btnDSV_rp.ImageOptions.SvgImage = global::QLDSV_TC.Properties.Resources.reportlayoutpivottable4;
            this.btnDSV_rp.Name = "btnDSV_rp";
            this.btnDSV_rp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem9_ItemClick);
            // 
            // btnDTK_rp
            // 
            this.btnDTK_rp.Caption = "BẢNG ĐIỂM TỔNG KẾT";
            this.btnDTK_rp.Id = 15;
            this.btnDTK_rp.ImageOptions.SvgImage = global::QLDSV_TC.Properties.Resources.reportlayoutpivottable5;
            this.btnDTK_rp.Name = "btnDTK_rp";
            this.btnDTK_rp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDTK_ItemClick);
            // 
            // btnDangKyMon
            // 
            this.btnDangKyMon.Caption = "ĐĂNG KÝ MÔN HỌC";
            this.btnDangKyMon.Id = 16;
            this.btnDangKyMon.ImageOptions.Image = global::QLDSV_TC.Properties.Resources.customer_service;
            this.btnDangKyMon.Name = "btnDangKyMon";
            this.btnDangKyMon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnDangKyMon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDangKyMon_ItemClick);
            // 
            // btnThemTaiKhoan
            // 
            this.btnThemTaiKhoan.Caption = "THÊM TÀI KHOẢN";
            this.btnThemTaiKhoan.Id = 17;
            this.btnThemTaiKhoan.ImageOptions.SvgImage = global::QLDSV_TC.Properties.Resources.actions_user;
            this.btnThemTaiKhoan.Name = "btnThemTaiKhoan";
            this.btnThemTaiKhoan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThemTaiKhoan_ItemClick);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Caption = "ĐĂNG XUẤT";
            this.btnDangXuat.Id = 18;
            this.btnDangXuat.ImageOptions.SvgImage = global::QLDSV_TC.Properties.Resources.close;
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDangXuat_ItemClick);
            // 
            // btnXemDiem
            // 
            this.btnXemDiem.Caption = "XEM ĐIỂM";
            this.btnXemDiem.Id = 19;
            this.btnXemDiem.ImageOptions.Image = global::QLDSV_TC.Properties.Resources.test;
            this.btnXemDiem.Name = "btnXemDiem";
            this.btnXemDiem.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.btnXemDiem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXemDiem_ItemClick);
            // 
            // rbQuanLy
            // 
            this.rbQuanLy.Appearance.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbQuanLy.Appearance.Options.UseFont = true;
            this.rbQuanLy.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grQuanTri,
            this.grKeToan});
            this.rbQuanLy.Name = "rbQuanLy";
            this.rbQuanLy.Text = "QUẢN LÝ";
            // 
            // grQuanTri
            // 
            this.grQuanTri.ItemLinks.Add(this.barButtonItem1);
            this.grQuanTri.ItemLinks.Add(this.barButtonItem2);
            this.grQuanTri.ItemLinks.Add(this.barButtonItem3);
            this.grQuanTri.ItemLinks.Add(this.barButtonItem4);
            this.grQuanTri.ItemLinks.Add(this.btnDiem);
            this.grQuanTri.Name = "grQuanTri";
            this.grQuanTri.Text = "QUẢN TRỊ PGV - KHOA";
            // 
            // grKeToan
            // 
            this.grKeToan.ItemLinks.Add(this.barButtonItem6);
            this.grKeToan.Name = "grKeToan";
            this.grKeToan.Text = "KẾ TOÁN";
            // 
            // rbSinhVien
            // 
            this.rbSinhVien.Appearance.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSinhVien.Appearance.Options.UseFont = true;
            this.rbSinhVien.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3});
            this.rbSinhVien.Name = "rbSinhVien";
            this.rbSinhVien.Text = "SINH VIÊN";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnDangKyMon);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnXemDiem);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "SINH VIÊN";
            // 
            // rbBaoCao
            // 
            this.rbBaoCao.Appearance.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBaoCao.Appearance.Options.UseFont = true;
            this.rbBaoCao.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4});
            this.rbBaoCao.Name = "rbBaoCao";
            this.rbBaoCao.Text = "BÁO CÁO";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.btnDSLTC_rp);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnBDMH_rp);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnDSSVLTC_rp);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnHP);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnDSV_rp);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnDTK_rp);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "QUẢN LÝ BÁO CÁO";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonPage1.Appearance.Options.UseFont = true;
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "TÀI KHOẢN";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnThemTaiKhoan);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDangXuat);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "QUẢN LÝ TÀI KHOẢN";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MAGV,
            this.HOTEN,
            this.NHOM});
            this.statusStrip1.Location = new System.Drawing.Point(0, 543);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1057, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MAGV
            // 
            this.MAGV.Name = "MAGV";
            this.MAGV.Size = new System.Drawing.Size(112, 20);
            this.MAGV.Text = "MÃ GV/ MÃ SV:";
            // 
            // HOTEN
            // 
            this.HOTEN.Name = "HOTEN";
            this.HOTEN.Size = new System.Drawing.Size(69, 20);
            this.HOTEN.Text = "HỌ TÊN: ";
            // 
            // NHOM
            // 
            this.NHOM.Name = "NHOM";
            this.NHOM.Size = new System.Drawing.Size(58, 20);
            this.NHOM.Text = "NHÓM:";
            // 
            // frmChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 569);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ribbon);
            this.IconOptions.Image = global::QLDSV_TC.Properties.Resources.Logo_Học_Viện_Công_Nghệ_Bưu_Chính_Viễn_Thông___PTIT_Simple___3_;
            this.Name = "frmChinh";
            this.Ribbon = this.ribbon;
            this.Text = "FORM CHÍNH";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage rbQuanLy;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grQuanTri;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grKeToan;
        private DevExpress.XtraBars.Ribbon.RibbonPage rbSinhVien;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.RibbonPage rbBaoCao;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel MAGV;
        private System.Windows.Forms.ToolStripStatusLabel HOTEN;
        private System.Windows.Forms.ToolStripStatusLabel NHOM;
        private DevExpress.XtraBars.BarButtonItem btnDiem;
        private DevExpress.XtraBars.BarButtonItem btnDSLTC_rp;
        private DevExpress.XtraBars.BarButtonItem btnBDMH_rp;
        private DevExpress.XtraBars.BarButtonItem btnDSSVLTC_rp;
        private DevExpress.XtraBars.BarButtonItem btnHP;
        private DevExpress.XtraBars.BarButtonItem btnDSV_rp;
        private DevExpress.XtraBars.BarButtonItem btnDTK_rp;
        private DevExpress.XtraBars.BarButtonItem btnDangKyMon;
        private DevExpress.XtraBars.BarButtonItem btnThemTaiKhoan;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnDangXuat;
        private DevExpress.XtraBars.BarButtonItem btnXemDiem;
    }
}