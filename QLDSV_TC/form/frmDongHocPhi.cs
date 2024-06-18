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
    public partial class frmDongHocPhi : DevExpress.XtraEditors.XtraForm
    {

        BindingSource bdsHocPhi = new BindingSource();
        BindingSource bdsCTHP = new BindingSource();
        int vitri = 0;
        int vitri1 = 0;
        bool dangthemmoi = false;
        public frmDongHocPhi()
        {
            InitializeComponent();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }


        void loadHP()
        {
            string cmd1 = "EXEC [dbo].[SP_GetInfoSV_HP] '" + txbMaSV.Text + "'";
            SqlDataReader reader = Program.ExecSqlDataReader(cmd1);
            if (reader.HasRows == false)
            {
                MessageBox.Show("Mã sinh viên không tồn tại");
                reader.Close();
                return;
            }
            reader.Read();
            txbTenSinhVien.Text = reader.GetString(0);
            txbMaLop.Text = reader.GetString(1);
            reader.Close();
            Program.conn.Close();
            string cmd2 = "EXEC dbo.SP_GetDSHP_SV '" + txbMaSV.Text + "'";
            DataTable tableHocPhi = Program.ExecSqlDataTable(cmd2);
            this.bdsHocPhi.DataSource = tableHocPhi;
            this.gcHocPhi.DataSource = this.bdsHocPhi;
            btnThem.Enabled = btnGhi.Enabled = btnHuy.Enabled = btnPhucHoi.Enabled = gcHocPhi.Enabled = btnMenu.Enabled = true;
            btnHuy.Enabled = false;
            btnLamMoi.Enabled = true;
            gridColumn6.OptionsColumn.ReadOnly = true;
            gridColumn7.OptionsColumn.ReadOnly = true;



        }


        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txbMaSV.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được bỏ trống");
                txbMaSV.Focus();
                return;
            }
            loadHP();
            btnMenu.Links[0].Caption = "Học phí";
          
        }

        private void HOCPHIGRIDCONTROL_MouseClick(object sender, MouseEventArgs e)
        {
            if (bdsHocPhi.Count > 0)
            {
                string nienkhoa = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["NIENKHOA"].ToString();
                string hocki = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString();
                string msv = txbMaSV.Text;

                string cmd = "EXEC dbo.SP_GetCTHP_SV '" + msv + "', '" + nienkhoa + "', '" + hocki + "'";
                DataTable tableCTHP = Program.ExecSqlDataTable(cmd);
                this.bdsCTHP.DataSource = tableCTHP;
                this.gcCTHP.DataSource = this.bdsCTHP;
                Console.WriteLine("test");
            }

        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnMenu.Links[0].Caption == "Học phí")
            {
                bdsHocPhi.AddNew();
               
            }
            else
            {
                bdsCTHP.AddNew();
                Console.WriteLine("CTDHP");
            }
            dangthemmoi = true;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnThoat.Enabled = btnLamMoi.Enabled = btnMenu.Enabled = btnThem.Enabled =  false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnMenu.Links[0].Caption == "Học phí")
            {
                try
                {
                    if (txbMaSV.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn chưa nhập mã sinh viên");
                        txbMaSV.Focus();
                        return;
                    }
                    if (float.Parse(((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCPHI"].ToString()) <= 0)
                    {
                        MessageBox.Show("Số tiền không được nhỏ hơn 0đ");
                        return;
                    }
                    if (((DataRowView)bdsHocPhi[bdsHocPhi.Position])["NIENKHOA"].ToString() == "")
                    {
                        MessageBox.Show("Niên khóa chưa nhập!");
                        return;
                    }
                    if (((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString() == "")
                    {
                        MessageBox.Show("Học kỳ chưa nhập!");
                        return;
                    }
                    if (((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCPHI"].ToString() == "")
                    {
                        MessageBox.Show("Học phí chưa nhập!");
                        return;
                    }
                    if (float.Parse(((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString()) <= 0)
                    {
                        MessageBox.Show("Học kì không được nhỏ hơn 1");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                string msv = txbMaSV.Text.Trim();
                string nienkhoa = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["NIENKHOA"].ToString();
                string hocki = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString();
                string hocphi = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCPHI"].ToString();
                string type = "";

                try
                {
                    if (dangthemmoi == true)
                    {
                        type = "ADD";


                    }
                    else
                    {
                        type = "UPDATE";
                    }
              
                    string query = "DECLARE @RETURN_VALUE INT " +
                        "EXEC @RETURN_VALUE = DBO.TAO_THONGTINHOCPHI " +
                        "@MASV='"+msv+"'," +
                        "@NIENKHOA ='"+nienkhoa+"'," +
                        "@HOCKY ='"+hocki+"'," +
                        "@HOCPHI='"+hocphi+"'," +
                        "@TYPE='"+type+"'" +
                        "SELECT  'Return Value' = @RETURN_VALUE";
                    Console.WriteLine(query);

                    int result = Program.CheckDataHelper(query);
                    Console.WriteLine(result);
                    bdsHocPhi.EndEdit();
                    bdsHocPhi.ResetCurrentItem();
                    dangthemmoi = false;                  
                    btnThem.Enabled = true;
                    btnHuy.Enabled = false;
                    if (result == 1)
                    {
                        MessageBox.Show("Thêm thành công!", "", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại!", "", MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm học phí: " + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
            if (btnMenu.Links[0].Caption == "Chi tiết học phí")
            {
                try
                {
                    if (((DataRowView)bdsCTHP[bdsCTHP.Position])["SOTIENDONG"].ToString() == "")
                    {
                        MessageBox.Show("Số tiền không được bỏ trống");
                        return;
                    }
                    if (float.Parse(((DataRowView)bdsCTHP[bdsCTHP.Position])["SOTIENDONG"].ToString()) <= 0)
                    {
                        MessageBox.Show("Số tiền không được nhỏ hơn 0đ");
                        return;
                    }

                    if (float.Parse(((DataRowView)bdsCTHP[bdsCTHP.Position])["SOTIENDONG"].ToString()) > float.Parse(((DataRowView)bdsHocPhi[bdsHocPhi.Position])["SOTIENCANDONG"].ToString()))
                    {
                        MessageBox.Show("Số tiền đóng không được lớn hơn số tiền cần đóng!");
                        return;
                    }

                    string nienkhoa = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["NIENKHOA"].ToString();
                    string hocki = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString();
                    string msv = txbMaSV.Text;
                    string sotiendong = ((DataRowView)bdsCTHP[bdsCTHP.Position])["SOTIENDONG"].ToString();

                    string query = "DECLARE	@return_value int " +
                        "EXEC	@return_value = [dbo].[SP_DONGHOCPHI] " +
                        "@MASV='"+msv+"'," +
                        "@NIENKHOA='"+nienkhoa+"'," +
                        "@HOCKY='"+hocki+"'," +
                        "@SOTIENDONG='"+sotiendong+"'" +
                        " SELECT	'Return Value' = @return_value";
                    Console.WriteLine(query);
                    int result = Program.CheckDataHelper(query);
                    Console.WriteLine(result);
                    bdsCTHP.EndEdit();
                    bdsCTHP.ResetCurrentItem();
                    btnThem.Enabled = btnThoat.Enabled = btnLamMoi.Enabled = btnMenu.Enabled = true;
                    btnGhi.Enabled = false;
                    btnHuy.Enabled = false;
                    if (result == 1)
                    {
                        MessageBox.Show("Đóng học phí thành công", "", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thất bại", "", MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


              
            }
        }

        private void btnCTHP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMenu.Links[0].Caption = "Chi tiết học phí";
            gcHocPhi.Enabled = false;
            groupBox1.Enabled = false;
            btnGhi.Enabled = false;
            gridColumn6.OptionsColumn.ReadOnly = false;
            gridColumn7.OptionsColumn.ReadOnly = false;

        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnMenu.Links[0].Caption == "Học phí")
            {
                
                bdsHocPhi.CancelEdit();
                btnThem.Enabled = btnGhi.Enabled = btnThoat.Enabled = btnMenu.Enabled = btnLamMoi.Enabled = true;
                btnHuy.Enabled = false;
            }
            else
            {
                bdsCTHP.CancelEdit();
                btnGhi.Enabled = btnHuy.Enabled = false;
                btnThoat.Enabled = btnLamMoi.Enabled = btnMenu.Enabled = true;
            }
            
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try

            {
             
                if (btnMenu.Links[0].Caption == "Học phí")
                {
                    vitri = bdsHocPhi.Position;
                    btnThem.Enabled = btnGhi.Enabled = btnLamMoi.Enabled = btnMenu.Enabled = gcHocPhi.Enabled = true;              
                }
                else
                {
                    vitri = bdsCTHP.Position;
                    btnThem.Enabled =  btnLamMoi.Enabled = btnMenu.Enabled = gcCTHP.Enabled = true;
                    gcHocPhi.Enabled = btnGhi.Enabled = false;

                }
                loadHP();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnHP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMenu.Links[0].Caption = "Học phí";
            loadHP();
            groupBox1.Enabled = true;
            gridColumn6.OptionsColumn.ReadOnly = true;
            gridColumn7.OptionsColumn.ReadOnly = true;
        }

        private void gcHocPhi_Click(object sender, EventArgs e)
        {

        }

        private void gcCTHP_Click(object sender, EventArgs e)
        {

        }
    }
}