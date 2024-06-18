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
    public partial class frmMonHoc : DevExpress.XtraEditors.XtraForm
    {

        string macn = "";
        int vitri = 0;
        private string flagOption;
        private string oldMaMonHoc = "";
        private string oldTenMonHoc = "";
        System.Collections.Stack undoList = new System.Collections.Stack();
        private bool dangthemmoi = false;
        private bool dangsua = false;
        public frmMonHoc()
        {
            InitializeComponent();
        }

        private void mONHOCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsMONHOC.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;
            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.dataSet.MONHOC);

            this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTINCHITableAdapter.Fill(this.dataSet.LOPTINCHI);
            
            panel1.Enabled = false;
            btnHuy.Enabled = false;
            btnGhi.Enabled = false;

        }

        private bool validatorMonHoc()
        {

            if (txbMaMonHoc.Text.Trim() == "")
            {
                MessageBox.Show("Mã môn học không được thiếu!", "", MessageBoxButtons.OK);
                txbMaMonHoc.Focus();
                return false;
            }
            if (txbTenMonHoc.Text.Trim() == "")
            {
                MessageBox.Show("Tên môn học không được thiếu!", "", MessageBoxButtons.OK);
                txbTenMonHoc.Focus();
                return false;
            }
            if ((txbSTLT.Value + txbSTTH.Value) % 15 != 0)
            {
                MessageBox.Show("Số tiết lý thuyết và thực hành phải là bội của 15!", "", MessageBoxButtons.OK);
                txbSTLT.Focus();
                return false;
            }
            if (flagOption == "ADD")
            {

                string query1 = "DECLARE  @return_value int \n"
                            + "EXEC @return_value = SP_CHECKID \n"
                            + "@Code = N'" + txbMaMonHoc.Text + "',@Type = N'MAMONHOC' \n"
                            + "SELECT 'Return Value' = @return_value";

                int resultMa = Program.CheckDataHelper(query1);
                if (resultMa == -1)
                {
                    XtraMessageBox.Show("Lỗi kết nối với database. Mời bạn xem lại", "", MessageBoxButtons.OK);
                    this.Close();
                }
                if (resultMa == 1)
                {
                    XtraMessageBox.Show("Mã môn học đã tồn tại!", "", MessageBoxButtons.OK);
                    return false;
                }

                // TODO : Check tên môn học có tồn tại chưa
                string query2 = "DECLARE  @return_value int \n"
                            + "EXEC @return_value = SP_CHECKNAME \n"
                            + "@Name = N'" + txbTenMonHoc.Text + "',@Type = N'TENMONHOC' \n"
                            + "SELECT 'Return Value' = @return_value";

                int resultTen = Program.CheckDataHelper(query2);
                if (resultTen == -1)
                {
                    XtraMessageBox.Show("Lỗi kết nối với database. Mời bạn xem lại", "", MessageBoxButtons.OK);
                    this.Close();
                }
                if (resultTen == 1)
                {
                    XtraMessageBox.Show("Tên môn học đã tồn tại!", "", MessageBoxButtons.OK);

                    return false;
                }
            }

            if (flagOption == "UPDATE")
            {
                if (!this.txbMaMonHoc.Text.Trim().ToString().Equals(oldMaMonHoc))// Nếu mã môn học thay đổi so với ban đầu
                {
                    //TODO: Check mã môn học có tồn tại chưa
                    string query1 = "DECLARE  @return_value int \n"
                                + "EXEC @return_value = SP_CHECKID \n"
                                + "@Code = N'" + txbMaMonHoc.Text + "',@Type = N'MAMONHOC' \n"
                                + "SELECT 'Return Value' = @return_value";

                    int resultMa = Program.CheckDataHelper(query1);
                    if (resultMa == -1)
                    {
                        XtraMessageBox.Show("Lỗi kết nối với database. Mời bạn xem lại", "", MessageBoxButtons.OK);
                        this.Close();
                    }
                    if (resultMa == 1)
                    {
                        XtraMessageBox.Show("Mã môn học đã tồn tại!", "", MessageBoxButtons.OK);

                        return false;
                    }
                }
                if (!this.txbTenMonHoc.Text.Trim().ToString().Equals(oldTenMonHoc))// Nếu tên môn học thay đổi so với ban đầu
                {
                    // TODO : Check tên môn học có tồn tại chưa
                    string query2 = "DECLARE  @return_value int \n"
                                + "EXEC @return_value = SP_CHECKNAME \n"
                                + "@Name = N'" + txbTenMonHoc.Text + "',@Type = N'TENMONHOC' \n"
                                + "SELECT 'Return Value' = @return_value";

                    int resultTen = Program.CheckDataHelper(query2);
                    if (resultTen == -1)
                    {
                        XtraMessageBox.Show("Lỗi kết nối với database. Mời bạn xem lại", "", MessageBoxButtons.OK);
                        this.Close();
                    }
                    if (resultTen == 1)
                    {
                        XtraMessageBox.Show("Tên môn học đã tồn tại!", "", MessageBoxButtons.OK);

                        return false;
                    }
                }
            }

            return true;


        }


        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            dangthemmoi = true;
            vitri = bdsMONHOC.Position;
            flagOption = "ADD";
            panel1.Enabled = true;
            bdsMONHOC.AddNew();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnLamMoi.Enabled = btnSua.Enabled = false;
            btnGhi.Enabled = btnHuy.Enabled = true;
            gcMONHOC.Enabled =false;
            txbMaMonHoc.Enabled = true;

        }
        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnPhucHoi.Enabled = false;
                return;
            }


            else
            {
                String undoQuery = undoList.Pop().ToString();
                Program.ExecSqlNonQuery(undoQuery);
            }

            this.MONHOCTableAdapter.Fill(this.dataSet.MONHOC);
            this.btnSua.Enabled = true;
            btnGhi.Enabled = false;
        }



        private void btnTHOAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                this.MONHOCTableAdapter.Fill(this.dataSet.MONHOC);
                gcMONHOC.Enabled = true;
                btnPhucHoi.Enabled = btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = true;
                panel1.Enabled = btnGhi.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }


        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string mamh = "";
            if (bdsLOPTINCHI.Count > 0)
            {
                MessageBox.Show("Không thể xóa lớp học này vì đã đăng ký lớp tín chỉ", "", MessageBoxButtons.OK);
                return;
            }


            if (MessageBox.Show("Bạn có thật sự muốn xóa môn học này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                   
                    string undoQuery =
              string.Format("INSERT INTO DBO.MONHOC(MAMH,TENMH,SOTIET_LT,SOTIET_TH)" +
                "VALUES(N'{0}',N'{1}','{2}', '{3}')",txbMaMonHoc.Text.Trim(),txbTenMonHoc.Text.Trim(),txbSTLT.Value,txbSTTH.Value);

                    Console.WriteLine(undoQuery);
                    undoList.Push(undoQuery);
                   
                    bdsMONHOC.RemoveCurrent();
                    mamh = ((DataRowView)bdsMONHOC[bdsMONHOC.Position])["MAMH"].ToString();
                    this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.MONHOCTableAdapter.Update(this.dataSet.MONHOC);
                    btnPhucHoi.Enabled = btnLamMoi.Enabled = btnGhi.Enabled = true;
                }
                catch (Exception ex)
                {
                    undoList.Pop();
                    MessageBox.Show("Lỗi xóa sinh viên: " + ex.Message, "", MessageBoxButtons.OK);
                    this.MONHOCTableAdapter.Fill(this.dataSet.MONHOC);
                    bdsMONHOC.Position = bdsMONHOC.Find("MAMH", mamh);
                    return;
                }
            }


            if (bdsMONHOC.Count == 0) btnXoa.Enabled = false;
        }


        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (validatorMonHoc() == true)
            {

                DialogResult dr = MessageBox.Show("Ghi thông tin vào database?", "Thông báo",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {

                        bdsMONHOC.EndEdit();
                        bdsMONHOC.ResetCurrentItem();
                        this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.MONHOCTableAdapter.Update(this.dataSet.MONHOC);
                        vitri = bdsMONHOC.Position;
                        dangsua = false;
                        btnPhucHoi.Enabled = true;

                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi ghi lớp học: " + ex.Message, "", MessageBoxButtons.OK);
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    this.bdsMONHOC.Position = vitri;
                    gcMONHOC.Enabled  = true;
                    btnGhi.Enabled = btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = btnPhucHoi.Enabled = true;
                    btnPhucHoi.Enabled = btnHuy.Enabled = false;
                    panel1.Enabled = dangthemmoi = false;
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
            vitri = bdsMONHOC.Position;
            bdsMONHOC.CancelEdit();
            //   bdsLOP.RemoveCurrent();
            btnThem.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = gcMONHOC.Enabled = btnSua.Enabled = btnPhucHoi.Enabled = true;
            btnHuy.Enabled = panel1.Enabled = btnGhi.Enabled = false;

            bdsMONHOC.Position = vitri;

        }


        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangsua = true;

            vitri = bdsMONHOC.Position;
            flagOption = "UPDATE";
            oldMaMonHoc = this.txbMaMonHoc.Text.Trim();
         
            String undoQuery = "";
            undoQuery = "UPDATE DBO.MONHOC SET TENMH =N'"+txbTenMonHoc.Text.Trim()
                +"',SOTIET_LT = "+txbSTLT.Value+",SOTIET_TH="+txbSTTH.Value+" WHERE MAMH ='"+txbMaMonHoc.Text.Trim()+"'";
            Console.WriteLine("query" + undoQuery);
            undoList.Push(undoQuery);

            txbMaMonHoc.Enabled = false;
            panel1.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnLamMoi.Enabled  = false;
            btnGhi.Enabled = btnHuy.Enabled = true;
            gcMONHOC.Enabled= false;
        }

        private void txbMaMonHoc_TextChanged(object sender, EventArgs e)
        {

        }
    }

}