namespace Erp2016.Lib.Report.Academics
{
    partial class RCertification
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.htmlTextBoxBody = new Telerik.Reporting.HtmlTextBox();
            this.pictureBoxSign = new Telerik.Reporting.PictureBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.pagesPageHeaderTextBox = new Telerik.Reporting.TextBox();
            this.htmlTextBoxDate = new Telerik.Reporting.HtmlTextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(22.399997711181641D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.htmlTextBoxBody,
            this.pictureBoxSign,
            this.htmlTextBoxDate});
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Segoe UI";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(16.299999237060547D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3622045516967773D), Telerik.Reporting.Drawing.Unit.Cm(0.59999889135360718D));
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.Font.Name = "Segoe UI";
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Justify;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Authorized Signature";
            // 
            // htmlTextBoxBody
            // 
            this.htmlTextBoxBody.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.99999982118606567D), Telerik.Reporting.Drawing.Unit.Cm(4D));
            this.htmlTextBoxBody.Name = "htmlTextBoxBody";
            this.htmlTextBoxBody.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18D), Telerik.Reporting.Drawing.Unit.Cm(7.90000057220459D));
            this.htmlTextBoxBody.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(20D);
            this.htmlTextBoxBody.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.htmlTextBoxBody.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.htmlTextBoxBody.Value = "";
            // 
            // pictureBoxSign
            // 
            this.pictureBoxSign.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(13.660408020019531D));
            this.pictureBoxSign.Name = "pictureBoxSign";
            this.pictureBoxSign.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3622047901153564D), Telerik.Reporting.Drawing.Unit.Cm(2.6393899917602539D));
            this.pictureBoxSign.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBoxSign.Style.Font.Name = "Segoe UI";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.27559089660644531D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pagesPageHeaderTextBox});
            this.pageFooterSection1.Name = "pageFooterSection1";
            this.pageFooterSection1.Style.Font.Name = "Segoe UI";
            // 
            // pagesPageHeaderTextBox
            // 
            this.pagesPageHeaderTextBox.Docking = Telerik.Reporting.DockingStyle.Fill;
            this.pagesPageHeaderTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010081553045893088D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pagesPageHeaderTextBox.Name = "pagesPageHeaderTextBox";
            this.pagesPageHeaderTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.8740158081054688D), Telerik.Reporting.Drawing.Unit.Inch(0.27559089660644531D));
            this.pagesPageHeaderTextBox.Style.Font.Bold = false;
            this.pagesPageHeaderTextBox.Style.Font.Name = "Segoe UI";
            this.pagesPageHeaderTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.pagesPageHeaderTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.pagesPageHeaderTextBox.Value = "Page {[PageNumber]} of {[PageCount]}";
            // 
            // htmlTextBoxDate
            // 
            this.htmlTextBoxDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(13.09999942779541D));
            this.htmlTextBoxDate.Name = "htmlTextBoxDate";
            this.htmlTextBoxDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.9999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.5602080225944519D));
            this.htmlTextBoxDate.Value = "htmlTextBox1";
            // 
            // RCertification
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.pageFooterSection1});
            this.Name = "RCertification";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.8740158081054688D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox pagesPageHeaderTextBox;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.HtmlTextBox htmlTextBoxBody;
        private Telerik.Reporting.PictureBox pictureBoxSign;
        private Telerik.Reporting.HtmlTextBox htmlTextBoxDate;
    }
}