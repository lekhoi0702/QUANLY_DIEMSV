using DevExpress.XtraReports.UI;
using QLDSV_TC.report;
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
    public partial class reportBangDiemTongKet : Form
    {
        public reportBangDiemTongKet()
        {
            InitializeComponent();
        }

        private void reportBangDiemTongKet_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;
            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.dataSet.LOP);

            cbLop.DataSource = bdsLOP;
            cbLop.DisplayMember = "MALOP";
            cbLop.ValueMember = "TENLOP";


            cbKhoa.DataSource = Program.bds_khoa;
            cbKhoa.DisplayMember = "TENKHOA";
            cbKhoa.ValueMember = "TENSERVER";
            cbKhoa.SelectedIndex = Program.mChinhanh;
            if (Program.mGroup == "KHOA")
            {
                cbKhoa.Enabled = false;
            }
        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsLOP.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

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
                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Fill(this.dataSet.LOP);
            }
        }

        private void BtnThoat_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

      

        private void btnExport_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string malop = cbLop.Text;
            Console.WriteLine(malop);
            string cmd = "SELECT TENKHOA,KHOAHOC FROM dbo.LOP,dbo.KHOA WHERE MALOP = '" + malop + "' AND KHOA.MAKHOA = LOP.MAKHOA";
            SqlDataReader reader = Program.ExecSqlDataReader(cmd);
            reader.Read();
            string tenkhoa = reader.GetString(0);
            string khoahoc = reader.GetString(1);
            reader.Close();
            rptBangDiemTongKet rpt = new rptBangDiemTongKet(malop);
            rpt.lbKhoa.Text = tenkhoa;
            rpt.lbKhoaHoc.Text = khoahoc;
            rpt.lbLop.Text = malop;

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();

        }

    }
}
