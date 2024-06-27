using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmNhapDiem : DevExpress.XtraEditors.XtraForm
    {

        int vitri = 0;
        string macn = "";
        private BindingSource bdsDiem = new BindingSource();
        public frmNhapDiem()
        {
            InitializeComponent();
        }

        private void mONHOCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsMONHOC.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmNhapDiem_Load(object sender, EventArgs e)
        {
            /*dataSet.EnforceConstraints = false;
            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.dataSet.MONHOC);*/

            cbKhoa.DataSource = Program.bds_khoa;
            cbKhoa.DisplayMember = "TENKHOA";
            cbKhoa.ValueMember = "TENSERVER";
            cbKhoa.SelectedIndex = Program.mChinhanh;

            /* cbTenMH.DataSource = bdsMONHOC;
             cbTenMH.ValueMember = "TENMH";
             cbTenMH.DisplayMember = "TENMH";*/
            loadTenMH();

            if (Program.mGroup == "KHOA")
            {
                cbKhoa.Enabled = false;
            }
            loadcbNienkhoa();
            cbNienKhoa.SelectedIndex = 0;
            loadcbHocKi(cbNienKhoa.Text);
            cbHocKi.SelectedIndex = 0;
            loadNhom(cbNienKhoa.Text, cbHocKi.Text);

        }
        private void loadTenMH()
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.SP_LAYTENMH";
            dt = Program.ExecSqlDataTable(cmd);
            BindingSource bdsMONHOC = new BindingSource();
            bdsMONHOC.DataSource = dt;
            cbTenMH.DataSource = bdsMONHOC;
            cbTenMH.DisplayMember = "TENMH";
            cbTenMH.ValueMember = "TENMH";
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


        void loadNhom(string nienkhoa, string hocki)
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.SP_LIST_NHOM '" + nienkhoa + "', " + hocki;
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsNhom = new BindingSource();
            bdsNhom.DataSource = dt;
            cbNhom.DataSource = bdsNhom;
            cbNhom.DisplayMember = "NHOM";
            cbNhom.ValueMember = "NHOM";
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
                XtraMessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {
                this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.MONHOCTableAdapter.Fill(this.dataSet.MONHOC);
                loadcbNienkhoa();
                cbNienKhoa.SelectedIndex = 0;
             

            }
        }


        void loadBDMH()
        {
            string cmd = "EXEC [dbo].[SP_BANGDIEM] '" + cbNienKhoa.Text + "', " + cbHocKi.Text + ", " + cbNhom.Text + ", N'" + cbTenMH.SelectedValue.ToString() + "'";
            DataTable diemTable = Program.ExecSqlDataTable(cmd);
            this.bdsDiem.DataSource = diemTable;
            this.gcNhapDiem.DataSource = this.bdsDiem;
            Console.WriteLine(cmd);
        }
        private void btnBatDau_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadBDMH();
        }


        private void btnCapNhat_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BindingSource bdsTemp = (BindingSource)this.gcNhapDiem.DataSource;
            if (bdsTemp == null)
            {
                XtraMessageBox.Show("Chưa có thông tin để ghi điểm!", "", MessageBoxButtons.OK);
                return;
            }
            GridView gridView = (GridView)this.gcNhapDiem.MainView;

            // Force the current edit to end and commit changes
            gridView.CloseEditor();
            gridView.UpdateCurrentRow();
            bdsTemp.EndEdit();
            SqlConnection conn = new SqlConnection(Program.connstr);
            // bắt đầu transaction
            SqlTransaction tran;

            conn.Open();
            /* tran = conn.BeginTransaction();
             try
             {
                 DataTable dtDiemThi = new DataTable();
                 dtDiemThi.Columns.Add("MALTC", typeof(int));
                 dtDiemThi.Columns.Add("MASV", typeof(string));
                 dtDiemThi.Columns.Add("DIEM_CC", typeof(float));
                 dtDiemThi.Columns.Add("DIEM_GK", typeof(float));
                 dtDiemThi.Columns.Add("DIEM_CK", typeof(float));

                 for (int i = 0; i < bdsTemp.Count; i++)
                 {

                     DataRowView rowView = (DataRowView)bdsTemp[i];
                     DataRow row = dtDiemThi.NewRow();
                     row["MALTC"] = int.Parse(rowView["MALTC"].ToString());
                     row["MASV"] = rowView["MASV"];
                     row["DIEM_CC"] = rowView["DIEM_CC"] != DBNull.Value ? float.Parse(rowView["DIEM_CC"].ToString()) : 0;
                     row["DIEM_GK"] = rowView["DIEM_GK"] != DBNull.Value ? float.Parse(rowView["DIEM_GK"].ToString()) : 0;
                     row["DIEM_CK"] = rowView["DIEM_CK"] != DBNull.Value ? float.Parse(rowView["DIEM_CK"].ToString()) : 0;
                     float cc = float.Parse(rowView["DIEM_CC"].ToString());
                     float gk = float.Parse(rowView["DIEM_GK"].ToString());
                     float ck = float.Parse(rowView["DIEM_CK"].ToString());
                     Console.Clear();
                     Console.WriteLine("CC " + cc + " GK " + gk + " CK " + ck);
                     if ((float)row["DIEM_CC"] < 0 || (float)row["DIEM_CC"] > 10 ||
                         (float)row["DIEM_GK"] < 0 || (float)row["DIEM_GK"] > 10 ||
                         (float)row["DIEM_CK"] < 0 || (float)row["DIEM_CK"] > 10)
                     {
                         tran.Rollback();
                    XtraMessageBox.Show("Điểm số chỉ được nhập từ 0 đến 10! Xin vui lòng nhập lại");
                         conn.Close();
                         loadBDMH();
                         return;
                     }
                     dtDiemThi.Rows.Add(row);


                 }
                 SqlCommand cmd = new SqlCommand("SP_XULY_DIEM", conn, tran);
                 cmd.CommandType = CommandType.StoredProcedure;
                 SqlParameter tvpParam = cmd.Parameters.AddWithValue("@DIEMTHI", dtDiemThi);
                 tvpParam.SqlDbType = SqlDbType.Structured;
                 cmd.ExecuteNonQuery();

                 tran.Commit();
             }
             catch (SqlException sqlex)
             {
                 try
                 {

                     tran.Rollback();
                    XtraMessageBox.Show("Lỗi ghi toàn bộ điểm vào Database. Bạn hãy xem lại ! " + sqlex.Message, "", MessageBoxButtons.OK);
                     loadBDMH();
                 }
                 catch (Exception ex2)
                 {
                     Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                     Console.WriteLine("  Message: {0}", ex2.Message);
                 }
                 conn.Close();
                 return;
             }
             finally
             {
                 conn.Close();
             }*/
            try
            {
                DataTable dtDiemThi = new DataTable();
                dtDiemThi.Columns.Add("MALTC", typeof(int));
                dtDiemThi.Columns.Add("MASV", typeof(string));
                dtDiemThi.Columns.Add("DIEM_CC", typeof(float));
                dtDiemThi.Columns.Add("DIEM_GK", typeof(float));
                dtDiemThi.Columns.Add("DIEM_CK", typeof(float));

                for (int i = 0; i < bdsTemp.Count; i++)
                {
                    DataRowView rowView = (DataRowView)bdsTemp[i];
                    DataRow row = dtDiemThi.NewRow();
                    row["MALTC"] = int.Parse(rowView["MALTC"].ToString());
                    row["MASV"] = rowView["MASV"];
                    row["DIEM_CC"] = rowView["DIEM_CC"] != DBNull.Value ? float.Parse(rowView["DIEM_CC"].ToString()) : 0;
                    row["DIEM_GK"] = rowView["DIEM_GK"] != DBNull.Value ? float.Parse(rowView["DIEM_GK"].ToString()) : 0;
                    row["DIEM_CK"] = rowView["DIEM_CK"] != DBNull.Value ? float.Parse(rowView["DIEM_CK"].ToString()) : 0;

                    if ((float)row["DIEM_CC"] < 0 || (float)row["DIEM_CC"] > 10 ||
                        (float)row["DIEM_GK"] < 0 || (float)row["DIEM_GK"] > 10 ||
                        (float)row["DIEM_CK"] < 0 || (float)row["DIEM_CK"] > 10)
                    {
                        XtraMessageBox.Show("Điểm số chỉ được nhập từ 0 đến 10! Xin vui lòng nhập lại");
                        loadBDMH();
                        return;
                    }
                    dtDiemThi.Rows.Add(row);
                }

                using (SqlConnection conn2 = new SqlConnection(Program.connstr))
                {
                    conn2.Open();
                    SqlCommand cmd = new SqlCommand("SP_XULY_DIEM", conn2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@DIEMTHI", dtDiemThi);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                XtraMessageBox.Show("Lỗi ghi toàn bộ điểm vào Database. Bạn hãy xem lại! " + sqlex.Message, "", MessageBoxButtons.OK);
                loadBDMH();
            }

            XtraMessageBox.Show("Thao tác thành công!", "", MessageBoxButtons.OK);
            string cmd1 = "EXEC [dbo].[SP_BANGDIEM] '" + cbNienKhoa.Text + "', " + cbHocKi.Text + ", " + cbNhom.Text + ", N'" + cbTenMH.SelectedValue.ToString() + "'";
            DataTable diemTable = Program.ExecSqlDataTable(cmd1);
            this.bdsDiem.DataSource = diemTable;
            this.gcNhapDiem.DataSource = this.bdsDiem;

        }


        private void btnThoat_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void cbHocKi_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadNhom(cbNienKhoa.Text, cbHocKi.Text);
            // cbNhom.SelectedIndex = 0;
        }
        private void cbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadcbHocKi(cbNienKhoa.Text);
            //cbHocKi.SelectedIndex = 0;
        }
    }
}