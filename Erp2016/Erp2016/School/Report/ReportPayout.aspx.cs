﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Erp2016.Lib.Report;
using Telerik.Reporting;


public partial class School_Report_ReportPayout :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InstanceReportSource RS = new InstanceReportSource();
        var payout = new RPayout();

      //  payout.ReportParameters.Add("CreditMemoType", ReportParameterType.String, "CP");

        //payout.ReportParameters.Add("AgingStart", ReportParameterType.Integer, 1);

        //payout.ReportParameters.Add("AgingEnd", ReportParameterType.Integer, 30);

        RS.ReportDocument = payout;
        
        RV_Payout.ReportSource = RS;

    }
}