using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLDSV_TC.report
{
    public partial class rptDiemSinhVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDiemSinhVien(String masv,int type)

        {
           
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = masv;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = type;
            this.sqlDataSource1.Fill();
        }

    }
}
