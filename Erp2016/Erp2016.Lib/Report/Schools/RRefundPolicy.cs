using System;
using System.Diagnostics;
using System.Drawing;

namespace Erp2016.Lib.Report.Schools
{
    /// <summary>
    /// Summary description for RTFTblLetterAcceptance.
    /// </summary>
    public partial class RRefundPolicy : Telerik.Reporting.Report
    {
        public RRefundPolicy(int invoiceId)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var invoice = new CInvoice().Get(invoiceId);
            if (invoice?.ProgramRegistrationId == null) return;

            htmlTextBoxPolicy.Value = $@"1. Registration fee and Accommodation placement fee are non-refundable.<br>
2. A tuition fee is refunded when Immigration Canada refuses to issue a student or visitor visa. You must
send Cornerstone the letter of rejection together with Cornerstone's letter of acceptance for a full
refund.<br>
3. Students who come to Canada with a Cornerstone study permit forfeit the right to all refunds.<br>
4. Cancellation before the start of course:<br>
When cancellations are made in writing more than 7 days before the initial start date on written
notification of a visa rejection and receipt of relevant supporting documentation, 100% of the tuition
and accommodation fees will be refunded. In all cases, however, the courier fee, the accommodation
placement fee (if applicable), the registration fee and any other service charges are non-refundable.
For cancellations made less than 7 days before the initial start date, including 'no shows', tuition and
accommodation fees will be refunded less than one week's accommodation fee and one week's tuition
fee. In all cases, the courier fee, registration fee, accommodation placement fee and any other service
charges are non-refundable.<br>
5. Cancellation after the start of course:<br>
Tuition refunds are paid within 30 days of a written request, and are calculated as follows:<br>
- Between 0 - 10% of program completion, a 50% refund of tuition fees will be issued.<br>
- Between 11 - 25% of program completion, a 30% refund of tuition fees will be issued.<br>
- After 25% or more of program completion: no tuition fees will be issued.<br>
6. Changing Homestay:<br>
Homestay placement fee is non-refundable. Students must give 2 weeks notice in writing to the
Homestay Coordinator if they wish to change their homestay. A refund will be made of 100% of the
unused homestay fee. A residence can be canceled for a full refund 4 weeks prior to arrival. Four
weeks post arrival, no refund will be given for any residence.<br>
7. In case of postponement, and / or cancellation, the school refund policy will apply, with the original
start date as the official start date.<br>
8. Registration fee and Accommodation placement fee are non-refundable. $400 is non refundable for
Summer Camp program.";

            htmlTextBoxDescription.Value = $@"** These Terms / Conditions are subjected to change and you will be notified at the time of booking of these changes.<br>
    Any disputes or claims arising from Cornerstone's refund policy will be subject to Canadian laws.<br>
** Students who have applied through an agent must contact the agent for a refund.<br>
<font style='color: red'>** Please note if a student starts the program and cancels (or is dismissed), no refund will be given. (SPECIAL PACKAGE)</font><br>
** All fees are due before program start date<br>
I understand that Cornerstone may be required to share my enrollment and/or reporting information with CIC as necessary for the purposes of
the ISP (International Student Program). By signing this contract below, I confirm that I have read and am in agreement with the Letter of
Acceptance and Refund Policy.";

            try
            {
                var logoPath = new CGlobal().GetLogoImagePath((int)invoice.SiteLocationId, CConstValue.ImageType.Logo);
                if (logoPath != string.Empty)
                    pictureBoxCompanyLogo.Value = Image.FromFile(logoPath);

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            try
            {
                var sideLogoPath = new CGlobal().GetLogoImagePath((int)invoice.SiteLocationId, CConstValue.ImageType.LogoSide);
                if (sideLogoPath != string.Empty)
                    pictureBoxSideLogo.Value = Image.FromFile(sideLogoPath);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}