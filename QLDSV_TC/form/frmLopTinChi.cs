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
    public partial class frmLopTinChi : DevExpress.XtraEditors.XtraForm
    {

        string macn = "";
        int vitri = 0;
        private string flagOption;
        private string oldMaLTC = "";
        private string oldTenMonHoc = "";
        System.Collections.Stack undoList = new System.Collections.Stack();
        private bool dangthemmoi = false;
        private bool dangsua = false;
        private BindingSource bdsGIANGVIEN = new BindingSource();
        private BindingSource bdsMONHOC = new BindingSource();
        public frmLopTinChi()
        {
            InitializeComponent();
        }

        private void lOPTINCHIBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsLOPTINCHI.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmLopTinChi_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;


            this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTINCHITableAdapter.Fill(this.dataSet.LOPTINCHI);

            this.DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.DANGKYTableAdapter.Fill(this.dataSet.DANGKY);


            cbKhoa.DataSource = Program.bds_khoa;
            cbKhoa.DisplayMember = "TENKHOA";
            cbKhoa.ValueMember = "TENSERVER";
            cbKhoa.SelectedIndex = Program.mChinhanh;
            if (Program.mGroup == "KHOA")
            {
                cbKhoa.Enabled = false;
            }
            string cmd1 = "EXEC DBO.SP_LayDSMH";
            DataTable tableMH = Program.ExecSqlDataTable(cmd1);
            bdsMONHOC.DataSource = tableMH;


            cbTenMonHoc.DataSource = bdsMONHOC;
            cbTenMonHoc.DisplayMember = "TENMH";
            cbTenMonHoc.ValueMember = "MAMH";

            string cmd = "EXEC DBO.SP_LayDSGV";
            DataTable tableGV = Program.ExecSqlDataTable(cmd);
            bdsGIANGVIEN.DataSource = tableGV;



            cbTenGV.DataSource = bdsGIANGVIEN;
            cbTenGV.DisplayMember = "HOTEN";
            cbTenGV.ValueMember = "MAGV";

            btnHuy.Enabled = false;
            panel1.Enabled = false;
            btnGhi.Enabled = false;
        }
        private void txbMaGV_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị MAMH từ TextBox MAGV
            string maGV = txbMaGV.Text.Trim();

            cbTenGV.SelectedValue = maGV;

        }


        private void txbTenGV_TextChanged()
        {
            DataRowView selectedRow = (DataRowView)cbTenGV.SelectedItem;

            string maGV = selectedRow["MAGV"].ToString();
            txbMaGV.Text = maGV;

        }

        private void txbMaMH_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị MAMH từ TextBox MAGV
            string maMH = txbMaMH.Text.Trim();

            cbTenMonHoc.SelectedValue = maMH;

        }


        private void txbTenMH_TextChanged()
        {
          DataRowView selectedRow = (DataRowView)cbTenMonHoc.SelectedItem;

            string maGV = selectedRow["MAMH"].ToString();
            txbMaMH.Text = maGV;

        }



        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKhoa.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cbKhoa.SelectedValue.ToString();
            if (cbKhoa.SelectedIndex != Program.mChinhanh && cbKhoa.SelectedIndex != 2)
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
                if (Program.mGroup == "PGV")
                {
                    XtraMessageBox.Show("PGV ko đc vào PKT", "", MessageBoxButtons.OK);
                }
                else
                    XtraMessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else if (cbKhoa.SelectedIndex != 2)
            {
                this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTINCHITableAdapter.Fill(this.dataSet.LOPTINCHI);

                if (bdsLOPTINCHI.Count > 0)
                    macn = ((DataRowView)bdsLOPTINCHI[0])["MAKHOA"].ToString();
            }
        }


        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (undoList.Count == 0)
            {
                XtraMessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnPhucHoi.Enabled = false;
                return;
            }


            else
            {
                String undoQuery = undoList.Pop().ToString();
                Program.ExecSqlNonQuery(undoQuery);
            }

            this.LOPTINCHITableAdapter.Fill(this.dataSet.LOPTINCHI);
        }



        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            /*String cauTruyVan2 = "EXEC SP_LayMaLTCLonNhat";

            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan2);
                if (Program.myReader == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.myReader.Read();
            int result2 = int.Parse(Program.myReader.GetValue(0).ToString());
            Program.myReader.Close();
            Console.WriteLine(result2);
            result2 += 1;*/
         
            macn = ((DataRowView)bdsLOPTINCHI[0])["MAKHOA"].ToString();
            dangthemmoi = true;
            vitri = bdsLOPTINCHI.Position;
            flagOption = "ADD";
            panel1.Enabled = true;
            bdsLOPTINCHI.AddNew();
            
            txbMaKhoa.Text = macn;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnLamMoi.Enabled = btnSua.Enabled = false;
            btnGhi.Enabled = btnHuy.Enabled = true;
            gcLOPTINCHI.Enabled = cbKhoa.Enabled = txbMaKhoa.Enabled = txbMaGV.Enabled = txbMaLTC.Enabled = txbMaMH.Enabled= false;
        }


        private void btnTHOAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }


        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                this.LOPTINCHITableAdapter.Fill(this.dataSet.LOPTINCHI);
                this.gcLOPTINCHI.Enabled = true;
                btnPhucHoi.Enabled = btnThem.Enabled = btnXoa.Enabled  =   btnSua.Enabled =true;
                panel1.Enabled  = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }


        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string malop = "";
            if (bdsDANGKY.Count > 0)
            {
                XtraMessageBox.Show("Không thể xóa lớp tín chỉ này vì đã có sinh viên đăng ký", "", MessageBoxButtons.OK);
                return;
            }


            if (XtraMessageBox.Show("Bạn có thật sự muốn xóa lớp tín chỉ này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    bool isChecked = txbHuyLop.Checked;
                    int huylop = isChecked ? 1 : 0;
                    string undoQuery =
              string.Format("INSERT INTO DBO.LOPTINCHI(NIENKHOA,HOCKY,MAMH,NHOM,MAGV,MAKHOA,SOSVTOITHIEU,HUYLOP)" +
                "VALUES(N'{0}',N'{1}',N'{2}', N'{3}','{4}','{5}','{6}','{7}')",
                 txbNienKhoa.Text.Trim(), txbHocKy.Value, txbMaMH.Text, txbNhom.Value, txbMaGV.Text, txbMaKhoa.Text, txbSSVTT.Value, huylop);

                    Console.WriteLine(undoQuery);
                    undoList.Push(undoQuery);
                    malop = ((DataRowView)bdsLOPTINCHI[bdsLOPTINCHI.Position])["MALTC"].ToString();
                    bdsLOPTINCHI.RemoveCurrent();
                    this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTINCHITableAdapter.Update(this.dataSet.LOPTINCHI);
                    btnPhucHoi.Enabled = btnLamMoi.Enabled = btnGhi.Enabled = true;
                }
                catch (Exception ex)
                {
                    undoList.Pop();
                    XtraMessageBox.Show("Lỗi xóa lớp học: " + ex.Message, "", MessageBoxButtons.OK);
                    this.LOPTINCHITableAdapter.Fill(this.dataSet.LOPTINCHI);
                    bdsLOPTINCHI.Position = bdsLOPTINCHI.Find("MALTC", malop);
                    return;
                }
            }


            if (bdsLOPTINCHI.Count == 0) btnXoa.Enabled = false;
        }



        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangsua == true)
            {
                undoList.Pop();
                dangsua = false;
            }
            vitri = bdsLOPTINCHI.Position;
            bdsLOPTINCHI.CancelEdit();
            //   bdsLOP.RemoveCurrent();
            btnThem.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = gcLOPTINCHI.Enabled = btnSua.Enabled = btnPhucHoi.Enabled = true;
            btnHuy.Enabled = panel1.Enabled = btnGhi.Enabled = false;
            cbKhoa.Enabled = true;
            bdsLOPTINCHI.Position = vitri;

        }


        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangsua = true;

            vitri = bdsLOPTINCHI.Position;
            flagOption = "UPDATE";
            oldMaLTC = this.txbMaLTC.Text.Trim();





            bool isChecked = txbHuyLop.Checked;
            int huylop = isChecked ? 1 : 0;

            String undoQuery = "";
            undoQuery = "UPDATE DBO.LOPTINCHI SET NIENKHOA ='" + txbNienKhoa.Text.Trim() + "',"
                + "HOCKY =" + txbHocKy.Value + ",MAMH = '" + txbMaMH.Text + "',NHOM =" + txbNhom.Value + ","
                + "MAGV = '" + txbMaGV.Text.Trim() + "',MAKHOA = '" + txbMaKhoa.Text.Trim() + "',SOSVTOITHIEU = " + txbSSVTT.Value
                + ",HUYLOP = " + huylop +" where MALTC = '" + txbMaLTC.Text.Trim()+"'";
            Console.WriteLine("query" + undoQuery);
            undoList.Push(undoQuery);

            txbMaLTC.Enabled = false;
            txbMaKhoa.Enabled = txbMaGV.Enabled = txbMaMH.Enabled = false;
            panel1.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnLamMoi.Enabled = cbKhoa.Enabled = false;
            btnGhi.Enabled = btnHuy.Enabled = true;
            gcLOPTINCHI.Enabled = false;
        }


        private bool validatorLopTinChi()
        {
            if (txbHocKy.Value == 0)
            {
                XtraMessageBox.Show("Học kì không được thiếu!", "", MessageBoxButtons.OK);
                txbHocKy.Focus();
                return false;
            }
            if (txbHocKy.Value < 0)
            {
                XtraMessageBox.Show("Học kì không thể là số âm!", "", MessageBoxButtons.OK);
                txbHocKy.Focus();
                return false;
            }
            if (txbSSVTT.Value == 0)
            {
                XtraMessageBox.Show("Số sinh viên tối thiểu không được thiếu!", "", MessageBoxButtons.OK);
                txbSSVTT.Focus();
                return false;
            }
            if (txbSSVTT.Value < 0)
            {
                XtraMessageBox.Show("Số sinh viên tối thiểu không thể là số âm!", "", MessageBoxButtons.OK);
                txbSSVTT.Focus();
                return false;
            }
            if (txbNhom.Value == 0)
            {
                XtraMessageBox.Show("Nhóm không được thiếu!", "", MessageBoxButtons.OK);
                txbNhom.Focus();
                return false;
            }
            if (txbNhom.Value < 0)
            {
                XtraMessageBox.Show("Số nhóm không thể là số âm!", "", MessageBoxButtons.OK);
                txbNhom.Focus();
                return false;
            }


            if (cbTenMonHoc.SelectedValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn môn học cho lớp tín chỉ", "", MessageBoxButtons.OK);
                cbTenMonHoc.Focus();
                return false;
            }
            if (cbTenGV.SelectedValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn giảng viên 1 cho lớp tín chỉ", "", MessageBoxButtons.OK);
                cbTenGV.Focus();
                return false;
            }
            /*if (cbTenGiangVien.SelectedValue == cbTenGiangVien2.SelectedValue)
            {
                XtraMessageBox.Show("Trùng lập dữ liệu 2 giảng viên lớp tín chỉ", "", MessageBoxButtons.OK);
                cbTenGiangVien2.Focus();
                return;
            }*/


            if (txbNienKhoa.Text.Trim() == "")
            {
                XtraMessageBox.Show("Niên khóa không được thiếu!", "", MessageBoxButtons.OK);
                txbNienKhoa.Focus();
                return false;
            }


            if (flagOption == "ADD")
            {
                string query1 = " DECLARE @return_value INT " +

                            " EXEC @return_value = [dbo].[SP_CHECKID] " +

                            " @Code = N'" + txbMaLTC.Text.Trim() + "',  " +

                            " @Type = N'MALOPTINCHI' " +

                            " SELECT  'Return Value' = @return_value ";

                int resultMa = Program.CheckDataHelper(query1);
                if (resultMa == -1)
                {
                XtraMessageBox.Show("Lỗi kết nối với database. Mời bạn xem lại", "", MessageBoxButtons.OK);
                    this.Close();
                }
                if (resultMa == 1)
                {
                XtraMessageBox.Show("Mã Sinh Viên đã tồn tại. Mời bạn nhập mã khác !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (resultMa == 2)
                {
                  XtraMessageBox.Show("Mã Sinh Viên đã tồn tại ở Khoa khác. Mời bạn nhập lại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }



        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (validatorLopTinChi() == true)
            {

                DialogResult dr = XtraMessageBox.Show("Ghi thông tin vào database?", "Thông báo",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        txbTenGV_TextChanged();
                        txbTenMH_TextChanged();
                  
                        bdsLOPTINCHI.EndEdit();
                        bdsLOPTINCHI.ResetCurrentItem();
                        this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                        this.LOPTINCHITableAdapter.Update(this.dataSet.LOPTINCHI);
                        vitri = bdsLOPTINCHI.Position;
                        dangsua = false;
                        btnPhucHoi.Enabled = true;

                        XtraMessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);


                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Lỗi ghi lớp học: " + ex.Message, "", MessageBoxButtons.OK);
                        Console.WriteLine(ex.Message);
                        return;
                    }

                    gcLOPTINCHI.Enabled = true;
                    btnGhi.Enabled = btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = btnPhucHoi.Enabled =  true;
                     btnHuy.Enabled = false;
                    panel1.Enabled = dangthemmoi = false;
                }


            }
            else
            {
                return;
            }

        }




        private void mALTCTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void mAMHLabel_Click(object sender, EventArgs e)
        {

        }

        private void hUYLOPLabel_Click(object sender, EventArgs e)
        {

        }

        private void gcLOPTINCHI_Click(object sender, EventArgs e)
        {

        }
    }
}