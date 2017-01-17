namespace Erp2016.Lib.Report
{
    partial class RInvoiceLumpSum
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RInvoiceLumpSum));
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.sqlDataSourceInvoiceDetail = new Telerik.Reporting.SqlDataSource();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.pagesPageHeaderTextBox = new Telerik.Reporting.TextBox();
            this.pageHeaderSection = new Telerik.Reporting.PageHeaderSection();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBoxInvoiceIssuer = new Telerik.Reporting.TextBox();
            this.reportHeaderSection = new Telerik.Reporting.ReportHeaderSection();
            this.CompanyInfoPanel = new Telerik.Reporting.Panel();
            this.companyLogoPictureBox = new Telerik.Reporting.PictureBox();
            this.textBoxInvoiceTitle = new Telerik.Reporting.TextBox();
            this.panel1 = new Telerik.Reporting.Panel();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBoxStudentPriceSum1 = new Telerik.Reporting.TextBox();
            this.tableTotal = new Telerik.Reporting.Table();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(15.599998474121094D);
            this.detailSection1.KeepTogether = false;
            this.detailSection1.Name = "detailSection1";
            this.detailSection1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            // 
            // sqlDataSourceInvoiceDetail
            // 
            this.sqlDataSourceInvoiceDetail.ConnectionString = "Erp2016.Lib.Properties.Settings.ERP2016ConnectionString";
            this.sqlDataSourceInvoiceDetail.Name = "sqlDataSourceInvoiceDetail";
            this.sqlDataSourceInvoiceDetail.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("InvoiceId", System.Data.DbType.Int32, null)});
            this.sqlDataSourceInvoiceDetail.SelectCommand = "SELECT        *\r\nFROM            vwReportInvoiceDetail";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.27559080719947815D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pagesPageHeaderTextBox});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // pagesPageHeaderTextBox
            // 
            this.pagesPageHeaderTextBox.Docking = Telerik.Reporting.DockingStyle.Right;
            this.pagesPageHeaderTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.179798126220703D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pagesPageHeaderTextBox.Name = "pagesPageHeaderTextBox";
            this.pagesPageHeaderTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8201007843017578D), Telerik.Reporting.Drawing.Unit.Inch(0.27559080719947815D));
            this.pagesPageHeaderTextBox.Style.Font.Bold = false;
            this.pagesPageHeaderTextBox.Style.Font.Name = "Segoe UI";
            this.pagesPageHeaderTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.pagesPageHeaderTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pagesPageHeaderTextBox.Value = "Page {[PageNumber]} of {[PageCount]}";
            // 
            // pageHeaderSection
            // 
            this.pageHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D);
            this.pageHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox17,
            this.textBox13,
            this.textBoxInvoiceIssuer});
            this.pageHeaderSection.Name = "pageHeaderSection";
            // 
            // textBox17
            // 
            this.textBox17.Docking = Telerik.Reporting.DockingStyle.Right;
            this.textBox17.Format = "{0:d}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.399799346923828D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.60010027885437D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Name = "Segoe UI";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "= Now()";
            // 
            // textBox13
            // 
            this.textBox13.Docking = Telerik.Reporting.DockingStyle.Right;
            this.textBox13.Format = "{0:d}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.399800300598145D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9999985694885254D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Segoe UI";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Printed Date";
            // 
            // textBoxInvoiceIssuer
            // 
            this.textBoxInvoiceIssuer.Docking = Telerik.Reporting.DockingStyle.Left;
            this.textBoxInvoiceIssuer.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBoxInvoiceIssuer.Name = "textBoxInvoiceIssuer";
            this.textBoxInvoiceIssuer.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.1837773323059082D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBoxInvoiceIssuer.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(47)))), ((int)(((byte)(11)))));
            this.textBoxInvoiceIssuer.Style.Font.Bold = true;
            this.textBoxInvoiceIssuer.Style.Font.Name = "Segoe UI";
            this.textBoxInvoiceIssuer.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(15D);
            this.textBoxInvoiceIssuer.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBoxInvoiceIssuer.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBoxInvoiceIssuer.Value = "";
            // 
            // reportHeaderSection
            // 
            this.reportHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(6.4380006790161133D);
            this.reportHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.CompanyInfoPanel});
            this.reportHeaderSection.Name = "reportHeaderSection";
            this.reportHeaderSection.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Cm(0.5D);
            // 
            // CompanyInfoPanel
            // 
            this.CompanyInfoPanel.Docking = Telerik.Reporting.DockingStyle.Top;
            this.CompanyInfoPanel.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.companyLogoPictureBox,
            this.textBoxInvoiceTitle,
            this.panel1});
            this.CompanyInfoPanel.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.CompanyInfoPanel.Name = "CompanyInfoPanel";
            this.CompanyInfoPanel.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.999898910522461D), Telerik.Reporting.Drawing.Unit.Cm(2.5997993946075439D));
            // 
            // companyLogoPictureBox
            // 
            this.companyLogoPictureBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.399999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.companyLogoPictureBox.MimeType = "image/jpeg";
            this.companyLogoPictureBox.Name = "companyLogoPictureBox";
            this.companyLogoPictureBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6000988483428955D), Telerik.Reporting.Drawing.Unit.Cm(2.5997993946075439D));
            this.companyLogoPictureBox.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.companyLogoPictureBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.companyLogoPictureBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.companyLogoPictureBox.Value = ((object)(resources.GetObject("companyLogoPictureBox.Value")));
            // 
            // textBoxInvoiceTitle
            // 
            this.textBoxInvoiceTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBoxInvoiceTitle.Name = "textBoxInvoiceTitle";
            this.textBoxInvoiceTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.999799728393555D), Telerik.Reporting.Drawing.Unit.Cm(1.6997992992401123D));
            this.textBoxInvoiceTitle.Style.Font.Bold = true;
            this.textBoxInvoiceTitle.Style.Font.Name = "Segoe UI Light";
            this.textBoxInvoiceTitle.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(30D);
            this.textBoxInvoiceTitle.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(34D);
            this.textBoxInvoiceTitle.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBoxInvoiceTitle.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBoxInvoiceTitle.Value = "Invoice Lump Sum";
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox43,
            this.tableTotal});
            this.panel1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.7000000476837158D));
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.3599996566772461D), Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D));
            // 
            // textBox43
            // 
            this.textBox43.Docking = Telerik.Reporting.DockingStyle.Left;
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.559999942779541D), Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D));
            this.textBox43.Style.Font.Bold = true;
            this.textBox43.Style.Font.Name = "Segoe UI";
            this.textBox43.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(15D);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox43.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox43.Value = "Total";
            // 
            // textBoxStudentPriceSum1
            // 
            this.textBoxStudentPriceSum1.Culture = new System.Globalization.CultureInfo("en-CA");
            this.textBoxStudentPriceSum1.Format = "{0:C2}";
            this.textBoxStudentPriceSum1.Name = "textBoxStudentPriceSum1";
            this.textBoxStudentPriceSum1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.7999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D));
            this.textBoxStudentPriceSum1.Style.Font.Bold = true;
            this.textBoxStudentPriceSum1.Style.Font.Name = "Segoe UI Light";
            this.textBoxStudentPriceSum1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(15D);
            this.textBoxStudentPriceSum1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBoxStudentPriceSum1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBoxStudentPriceSum1.StyleName = "";
            this.textBoxStudentPriceSum1.Value = "=Sum(Fields.StudentPrice)";
            // 
            // tableTotal
            // 
            this.tableTotal.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(6.7999997138977051D)));
            this.tableTotal.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D)));
            this.tableTotal.Body.SetCellContent(0, 0, this.textBoxStudentPriceSum1);
            tableGroup1.Name = "tableGroup";
            this.tableTotal.ColumnGroups.Add(tableGroup1);
            this.tableTotal.DataSource = this.sqlDataSourceInvoiceDetail;
            this.tableTotal.Docking = Telerik.Reporting.DockingStyle.Left;
            this.tableTotal.Filters.Add(new Telerik.Reporting.Filter("= Fields.InvoiceId", Telerik.Reporting.FilterOperator.In, "= Split(\",\", \"26427, 26429\")"));
            this.tableTotal.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBoxStudentPriceSum1});
            this.tableTotal.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.559999942779541D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.tableTotal.Name = "tableTotal";
            tableGroup2.Name = "group";
            this.tableTotal.RowGroups.Add(tableGroup2);
            this.tableTotal.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.7999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D));
            // 
            // RInvoiceLumpSum
            // 
            this.Culture = new System.Globalization.CultureInfo("en-US");
            this.Filters.Add(new Telerik.Reporting.Filter("= Fields.InvoiceId", Telerik.Reporting.FilterOperator.Equal, "= Parameters.InvoiceId.Value"));
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection,
            this.detailSection1,
            this.pageFooterSection1,
            this.reportHeaderSection});
            this.Name = "RInvoice";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "InvoiceId";
            reportParameter1.Text = "InvoiceId";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Value = "0";
            this.ReportParameters.Add(reportParameter1);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            descendantSelector1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableHeader")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule3.Style.BackgroundColor = System.Drawing.Color.Gainsboro;
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Silver;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule3.Style.Color = System.Drawing.Color.DimGray;
            styleRule3.Style.Font.Bold = true;
            styleRule3.Style.Font.Name = "Arial";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableBody")});
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule4.Style.BorderColor.Default = System.Drawing.Color.Silver;
            styleRule4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Font.Name = "Arial";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            styleRule4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Normal.TableTotal")});
            styleRule5.Style.BackgroundColor = System.Drawing.Color.Silver;
            styleRule5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            styleRule5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            styleRule5.Style.Font.Bold = true;
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            styleRule5.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896D);
            styleRule5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(19.999898910522461D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion
        private Telerik.Reporting.DetailSection detailSection1;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.SqlDataSource sqlDataSourceInvoiceDetail;
        private Telerik.Reporting.TextBox pagesPageHeaderTextBox;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBoxInvoiceIssuer;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection;
        private Telerik.Reporting.Panel CompanyInfoPanel;
        private Telerik.Reporting.PictureBox companyLogoPictureBox;
        private Telerik.Reporting.TextBox textBoxInvoiceTitle;
        private Telerik.Reporting.Panel panel1;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.Table tableTotal;
        private Telerik.Reporting.TextBox textBoxStudentPriceSum1;
    }
}