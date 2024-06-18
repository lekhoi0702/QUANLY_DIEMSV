using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLDSV_TC.report
{
    public partial class rptBangDiemTongKet : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangDiemTongKet(string malop)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = malop;
        }

    }
}
