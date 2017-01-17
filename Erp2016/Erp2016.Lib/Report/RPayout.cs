
namespace Erp2016.Lib.Report
{
    using System;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RPayout.
    /// </summary>
    public partial class RPayout : Telerik.Reporting.Report
    {
        public RPayout()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            
            //sqlDataSourcePayout.Parameters.Add("InvoiceId", DbType.Int32, "= Parameters.InvoiceId.Value");
            


        }
    }
}