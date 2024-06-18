using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC.form
{
    public partial class frmDiemSinhVien : Form
    {
        private BindingSource bdsDiem = new BindingSource();
        public frmDiemSinhVien()
        {
            InitializeComponent();
        }
        private void ReportDiemMonHoc_Load(object sender, EventArgs e)
        {
            
            loadcbNienkhoa();
            cbNienKhoa.SelectedIndex = 0;

        }
        private void cbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadcbHocKi(cbNienKhoa.Text);
            //cbHocKi.SelectedIndex = 0;
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

        private void cbHocKi_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gcDiem_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string cmd = "EXEC [dbo].[SP_DIEMSV] " +"'" +Program.username+"',"+"'"+cbNienKhoa.Text + "', '" + cbHocKi.Text + "'";
            Console.WriteLine(cmd);
            DataTable tableDiem = Program.ExecSqlDataTable(cmd);
            this.bdsDiem.DataSource = tableDiem;
            this.gcDiem.DataSource = this.bdsDiem;
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
    }
}
