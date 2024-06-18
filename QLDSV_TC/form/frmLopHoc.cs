using DevExpress.XtraEditors;
using System;
using System.Collections;
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
    public partial class frmLopHoc : DevExpress.XtraEditors.XtraForm
    {

        string macn = "";
        int vitri = 0;
        private string _flagOptionLop;
        private string _oldMaLop = "";
        private string _oldTenLop = "";
        Stack undoList = new Stack();
        private bool dangthemmoi = false;
        private bool dangsua = false;
        public frmLopHoc()
        {
            InitializeComponent();
        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsLOP.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmLopHoc_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;
            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.dataSet.LOP);

            this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SINHVIENTableAdapter.Fill(this.dataSet.SINHVIEN);
            macn = ((DataRowView)bdsLOP[0])["MAKHOA"].ToString();
            cbKhoa.DataSource = Program.bds_khoa;
            cbKhoa.DisplayMember = "TENKHOA";
            cbKhoa.ValueMember = "TENSERVER";
            cbKhoa.SelectedIndex = Program.mChinhanh;
            if (Program.mGroup == "KHOA")
            {
                cbKhoa.Enabled = false;
            }
            panel1.Enabled = false;
            btnHuy.Enabled = false;

            txbMaKhoa.Enabled = false;

            
        }

        private bool validatorLopHoc()
        {
            if (btnThem.Enabled = false)
            {
                _flagOptionLop = "ADD";
            }
            else {
                _flagOptionLop = "UPDATE";
            }
            if (txbMaLop.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp không được thiếu!", "", MessageBoxButtons.OK);
                txbMaLop.Focus();
                return false;
            }
            if (txbTenLop.Text.Trim() == "")
            {
                MessageBox.Show("Tên lớp không được thiếu!", "", MessageBoxButtons.OK);
                txbTenLop.Focus();
                return false;
            }
            if (txbKhoaHoc.Text.Trim() == "")
            {
                MessageBox.Show("Khóa học không được thiếu!", "", MessageBoxButtons.OK);
                txbKhoaHoc.Focus();
                return false;
            }
            
            if (_flagOptionLop == "ADD")
            {


                //TODO: Check mã lớp có tồn tại chưa
                string query1 = "DECLARE  @return_value int \n"
                            + "EXEC  @return_value = SP_CHECKID \n"
                            + "@Code = N'" + txbMaLop.Text + "',@Type = N'MALOP' \n"
                            + "SELECT  'Return Value' = @return_value ";

                int resultMa = Program.CheckDataHelper(query1);
                if (resultMa == -1)
                {
                    XtraMessageBox.Show("Lỗi kết nối với database. Mời ban xem lại !", "", MessageBoxButtons.OK);
                    this.Close();
                }
                if (resultMa == 1)
                {
                    XtraMessageBox.Show("Mã lớp đã tồn tại ở Khoa hiên tại !", "", MessageBoxButtons.OK);

                    return false;
                }
                if (resultMa == 2)
                {
                    XtraMessageBox.Show("Mã lớp đã tồn tại ở Khoa khác !", "", MessageBoxButtons.OK);

                    return false;
                }

                // TODO : Check tên lớp có tồn tại chưa
                string query2 = "DECLARE @return_value int \n"
                               + "EXEC @return_value = SP_CHECKNAME \n"
                               + "@Name = N'" + txbTenLop.Text + "', @Type = N'TENLOP' \n"
                               + "SELECT 'Return Value' = @return_value ";
                int resultTen = Program.CheckDataHelper(query2);
                if (resultTen == -1)
                {
                    XtraMessageBox.Show("Lỗi kết nối với Database. Mời bạn xem lại !", "", MessageBoxButtons.OK);
                    this.Close();
                }
                if (resultTen == 1)
                {
                    XtraMessageBox.Show("Tên lớp đã có rồi !", "", MessageBoxButtons.OK);

                    return false;
                }
                if (resultTen == 2)
                {
                    XtraMessageBox.Show("Tên lớp đã tồn tại ở Khoa khác !", "", MessageBoxButtons.OK);
                    return false;
                }
            }

            if (_flagOptionLop == "UPDATE")
            {
                _oldMaLop = txbMaLop.Text.Trim();
                _oldTenLop = txbTenLop.Text.Trim();
                if (!this.txbMaLop.Text.Trim().ToString().Equals(_oldMaLop))// Nếu mã lớp thay đổi so với ban đầu
                {
                    //TODO: Check mã lớp có tồn tại chưa
                    string query1 = "DECLARE  @return_value int \n"
                                + "EXEC  @return_value = SP_CHECKID \n"
                                + "@Code = N'" + txbMaLop.Text + "',@Type = N'MALOP' \n"
                                + "SELECT  'Return Value' = @return_value ";

                    int resultMa = Program.CheckDataHelper(query1);
                    if (resultMa == -1)
                    {
                        XtraMessageBox.Show("Lỗi kết nối với database. Mời ban xem lại !", "", MessageBoxButtons.OK);
                        this.Close();
                    }
                    if (resultMa == 1)
                    {
                        XtraMessageBox.Show("Mã lớp đã tồn tại ở Khoa hiên tại !", "", MessageBoxButtons.OK);
                        return false;
                    }
                    if (resultMa == 2)
                    {
                        XtraMessageBox.Show("Mã lớp đã tồn tại ở Khoa khác !", "", MessageBoxButtons.OK);
                        return false;
                    }
                }
                if (!this.txbTenLop.Text.Trim().ToString().Equals(_oldTenLop))
                {
                    // TODO : Check tên lớp có tồn tại chưa
                    string query2 = "DECLARE @return_value int \n"
                                   + "EXEC @return_value = SP_CHECKNAME \n"
                                   + "@Name = N'" + txbTenLop.Text + "', @Type = N'TENLOP' \n"
                                   + "SELECT 'Return Value' = @return_value ";
                    int resultTen = Program.CheckDataHelper(query2);
                    if (resultTen == -1)
                    {
                        XtraMessageBox.Show("Lỗi kết nối với Database. Mời bạn xem lại !", "", MessageBoxButtons.OK);
                        this.Close();
                    }
                    if (resultTen == 1)
                    {
                        XtraMessageBox.Show("Tên lớp đã có rồi !", "", MessageBoxButtons.OK);
                        return false;
                    }
                    if (resultTen == 2)
                    {
                        XtraMessageBox.Show("Tên lớp đã tồn tại ở Khoa khác !", "", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }

            return true;
        }



        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangthemmoi = true;
            vitri = bdsLOP.Position;
            _flagOptionLop = "ADD";
            panel1.Enabled = true;
            bdsLOP.AddNew();
            txbMaKhoa.Text = macn;
            btnThem.Enabled = btnSua1.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnLamMoi.Enabled = btnSua1.Enabled =  false;
            btnGhi.Enabled = btnHuy.Enabled = true;
            gcLOP.Enabled = cbKhoa.Enabled = false;
            txbMaLop.Enabled = true;
            

        }


        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnPhucHoi.Enabled = false;
                return;
            }


            else {
                String undoQuery = undoList.Pop().ToString();
                Program.ExecSqlNonQuery(undoQuery);
            }

            this.LOPTableAdapter.Fill(this.dataSet.LOP);
        }


        private void btnTHOAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }


        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                this.LOPTableAdapter.Fill(this.dataSet.LOP);
                this.gcLOP.Enabled = true;
                btnPhucHoi.Enabled =btnThem.Enabled=btnXoa.Enabled = true;
                panel1.Enabled = false;
                btnGhi.Enabled = false;
                btnSua1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
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
                    MessageBox.Show("PGV ko đc vào PKT", "", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else if (cbKhoa.SelectedIndex != 2)
            {
                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Fill(this.dataSet.LOP);
                
                if (bdsLOP.Count > 0)
                    macn = ((DataRowView)bdsLOP[0])["MAKHOA"].ToString();
            }
        }


        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string malop = "";
            if (bdsSINHVIEN.Count > 0)
            {
                MessageBox.Show("Không thể xóa lớp học này vì đã có sinh viên", "", MessageBoxButtons.OK);
                return;
            }

   
            if (MessageBox.Show("Bạn có thật sự muốn xóa lớp học này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    string undoQuery =
              string.Format("INSERT INTO DBO.LOP( MALOP,TENLOP,KHOAHOC,MAKHOA)" +
                "VALUES(N'{0}',N'{1}',N'{2}', N'{3}')",txbMaLop.Text.Trim(),txbTenLop.Text.Trim(), txbKhoaHoc.Text.Trim(), txbMaKhoa.Text.Trim());

                    Console.WriteLine(undoQuery);
                    undoList.Push(undoQuery);
                    malop = ((DataRowView)bdsLOP[bdsLOP.Position])["MALOP"].ToString();
                    bdsLOP.RemoveCurrent();
                    this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTableAdapter.Update(this.dataSet.LOP);
                    btnPhucHoi.Enabled = btnLamMoi.Enabled = btnGhi.Enabled = true;
                }
                catch (Exception ex)
                {
                    undoList.Pop();
                    MessageBox.Show("Lỗi xóa lớp học: " + ex.Message, "", MessageBoxButtons.OK);
                    this.LOPTableAdapter.Fill(this.dataSet.LOP);
                    bdsLOP.Position = bdsLOP.Find("MALOP", malop);
                    return;
                }
            }

           
            if (bdsLOP.Count == 0) btnXoa.Enabled = false;
        }



        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            if (validatorLopHoc() == true)
            {

                DialogResult dr = MessageBox.Show("Ghi thông tin vào database?", "Thông báo",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        


                        bdsLOP.EndEdit();

                         bdsLOP.ResetCurrentItem();
                         this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.LOPTableAdapter.Update(this.dataSet.LOP);
                        vitri = bdsLOP.Position;
                        dangsua = false;
                     
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi lớp học: " + ex.Message, "", MessageBoxButtons.OK);
                        return;
                    }
                    this.bdsLOP.Position = vitri;
                    gcLOP.Enabled = true;
                    btnGhi.Enabled = btnThem.Enabled = btnSua1.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = btnPhucHoi.Enabled = true;
                    btnPhucHoi.Enabled = btnHuy.Enabled = false;
                    panel1.Enabled = dangthemmoi = false;
                    btnGhi.Enabled = false;
                }
                    
                
            }
            else
            {
                return;
            }

        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangsua == true)
            {
                undoList.Pop();
                dangsua = false;
            }
            vitri = bdsLOP.Position;
            bdsLOP.CancelEdit();
         //   bdsLOP.RemoveCurrent();
            btnThem.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = gcLOP.Enabled = true;
            btnHuy.Enabled = panel1.Enabled = false;
            btnGhi.Enabled = false;
            btnSua1.Enabled = true;
            bdsLOP.Position = vitri;
            btnPhucHoi.Enabled = true;
        }

        private void tENLOPLabel_Click(object sender, EventArgs e)
        {

        }

        private void txbTenLop_TextChanged(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangsua = true;
          
            vitri = bdsLOP.Position;
            _flagOptionLop = "UPDATE";
            _oldMaLop = this.txbMaLop.Text.Trim();
            _oldTenLop = this.txbTenLop.Text.Trim();


            String undoQuery = "";
            undoQuery = "UPDATE DBO.LOP SET MALOP = N'" + txbMaLop.Text.Trim()
                    + "',TENLOP = N'" + txbTenLop.Text.Trim()
                    + "',KHOAHOC = N'" + txbKhoaHoc.Text.Trim() + "' where malop = N'"
                    + _oldMaLop.Trim() + "'";
            Console.WriteLine("query" + undoQuery);
            undoList.Push(undoQuery);

            txbMaLop.Enabled = false;
            panel1.Enabled = true;
            btnThem.Enabled = btnSua1.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled=btnLamMoi.Enabled=cbKhoa.Enabled = false;
            btnGhi.Enabled = btnHuy.Enabled = true;
            gcLOP.Enabled = false;
        }

        private void gcLOP_Click(object sender, EventArgs e)
        {

        }

        private void cbKhoa_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}