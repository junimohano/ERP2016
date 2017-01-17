using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Erp2016.Lib
{
    /// <summary>
    ///     Mail class for sending email in the ERP system
    /// </summary>
    public class CMail
    {
        public bool SendMail(CConstValue.Approval approvalType, CConstValue.MailStatus mailStatus, int index, string indexNumber, int userId)
        {
            try
            {
                var requestedUserId = 0;
                DateTime requestedDate = DateTime.Now;

                var approvedUserId = 0;
                var approvedMemo = string.Empty;
                DateTime approvedDate = DateTime.Now;

                var approvingUserId = 0;
                var approvalStatus = string.Empty;

                var currentApproval = new CApprovalHistory().GetCurrentApproval((int)approvalType, index);
                if (currentApproval != null)
                {
                    var approvalStepDict = new CDict().GetDictByTypeAndValue(217, (int)currentApproval.ApprovalStep);
                    approvalStatus = approvalStepDict?.Name;
                    approvedMemo = currentApproval.ApprovalMemo;
                    approvedDate = currentApproval.ApprovalDate.Value;
                }

                switch (mailStatus)
                {
                    case CConstValue.MailStatus.ToApproveUser:
                        requestedUserId = userId;
                        approvingUserId = new CApproval().GetSupuervisor((int)approvalType, userId);
                        break;
                    case CConstValue.MailStatus.ToApproveUserAndRequestUser:
                        var tempRequestedUser1 = new CApprovalHistory().GetApprovalByRequestedUser((int)approvalType, index);
                        if (tempRequestedUser1 != null)
                        {
                            requestedUserId = tempRequestedUser1.ApprovalUser;
                            requestedDate = tempRequestedUser1.CreatedDate;
                        }
                        approvedUserId = userId;
                        approvingUserId = new CApproval().GetSupuervisor((int)approvalType, userId);

                        break;
                    case CConstValue.MailStatus.ToRequestUser:
                        var tempRequestedUser2 = new CApprovalHistory().GetApprovalByRequestedUser((int)approvalType, index);
                        if (tempRequestedUser2 != null)
                        {
                            requestedUserId = tempRequestedUser2.ApprovalUser;
                            requestedDate = tempRequestedUser2.CreatedDate;
                        }
                        approvedUserId = userId;
                        break;
                }

                var cUser = new CUser();
                var requestedUser = cUser.Get(requestedUserId);
                var approvedUser = cUser.Get(approvedUserId);
                var approvingUser = cUser.Get(approvingUserId);

                if (indexNumber == string.Empty)
                    indexNumber = index.ToString();

                // todo : temp to change mail should be removed before lunching.
                if (requestedUser != null)
                    requestedUser.Email = "jan@loyalistgroup.com";
                if (approvedUser != null)
                    approvedUser.Email = "jan@loyalistgroup.com";
                if (approvingUser != null)
                    approvingUser.Email = "jan@loyalistgroup.com";

                var bodyToApprovingUser = $@"Dear <u><i>{cUser.GetUserName(approvingUser)}</i></u>,
                                            <p></p><br /><br />
                                            A new request is waiting for your approval.
                                            <br /><br />
                                            Please check it out.
                                            <p></p><br /><br />                                            
                                            <b>{approvalType}</b>
                                            <br /><br />
                                            &nbsp;&nbsp;&nbsp;- No : <b>{indexNumber}</b>
                                            <br />
                                            &nbsp;&nbsp;&nbsp;- Status : <b>{approvalStatus}</b>
                                            <br />
                                            &nbsp;&nbsp;&nbsp;- Requested by : <b>{cUser.GetUserName(requestedUser)}</b>
                                            <br />
                                            &nbsp;&nbsp;&nbsp;- Requested Date : <b>{CGlobal.GetDateFormat(requestedDate)}</b>";

                var bodyToRequestedUser = $@"Dear <u><i>{cUser.GetUserName(requestedUser)}</i></u>,
                                            <p></p><br /><br />
                                            Your request has been updated.
                                            <br /><br />
                                            Please check it out.
                                            <p></p><br /><br />
                                            <b>{approvalType}</b>
                                            <br /><br />
                                            &nbsp;&nbsp;&nbsp;- No : <b>{indexNumber}</b>
                                            <br />
                                            &nbsp;&nbsp;&nbsp;- Status : <b>{approvalStatus}</b>
                                            <br />
                                            &nbsp;&nbsp;&nbsp;- Approved by : <b>{cUser.GetUserName(approvedUser)}</b>
                                            <br />
                                            &nbsp;&nbsp;&nbsp;- Approved Date : <b>{CGlobal.GetDateFormat(approvedDate)}</b>
                                            <br />
                                            &nbsp;&nbsp;&nbsp;- Approved Memo : <b>{approvedMemo}</b>";

                switch (mailStatus)
                {
                    case CConstValue.MailStatus.ToApproveUser:
                        SendMailPost(approvalType.ToString(), approvingUser.Email, bodyToApprovingUser);
                        break;
                    case CConstValue.MailStatus.ToApproveUserAndRequestUser:
                        SendMailPost(approvalType.ToString(), requestedUser.Email, bodyToRequestedUser);
                        SendMailPost(approvalType.ToString(), approvingUser.Email, bodyToApprovingUser);
                        break;
                    case CConstValue.MailStatus.ToRequestUser:
                        SendMailPost(approvalType.ToString(), requestedUser.Email, bodyToRequestedUser);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        protected virtual void SendMailPost(string approvalTypeName, string emailAddress, string bodyStrip)
        {
            try
            {
                var msg = new MailMessage { From = new MailAddress("no-reply@loyalistgroup.com") };
                msg.To.Add(new MailAddress(emailAddress));
                msg.Subject = approvalTypeName + " Request";
                msg.IsBodyHtml = true;

                var bodySb = new StringBuilder();
                bodySb.Append(@"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                        <html xmlns='http://www.w3.org/1999/xhtml'>
                        <head>
                        <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                        <body>
                        <style type='text/css'>.ExternalClass {width:100%;}img {display: block;}</style>
                        <style>.table01{border:8px solid #08203e;padding:0px;}body{font-family: 'Montserrat', sans-serif;}</style>");

                bodySb.Append($@"
                        <table cellspacing='0' cellpadding='0' class='table01'>
                        <tr>
                            <td style='height:90px; background-color:#08203e;padding-left:20px;'><img src='{CConstValue.WebSiteUrl}/assets/img/portal_logo.png' alt=''/></td>
                        </tr>
                        <tr>
                            <td style='padding:30px;'>                                
                                {bodyStrip}
                                <br />　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　&nbsp;<br />
                                <footer>
                                    <p align='right'><a href='{CConstValue.WebSiteUrl}'>Click to access ERP</a></p>                                    
                                    <br />
                                    <h4>　　　　　　* This is an automatically generated email - please do not reply. *</h4>
                                </footer>
                            </td>
                        </tr>
                        <tr>
                        <td style='height:90px; text-align:right; background-color:#08203e; color:#fff;padding-right:20px; font-size:13px;'>1255 Bay St, 8th Floor, Toronto Ontario, M5R 2A9, Canada<br /><br />KGIC Inc.</td>
                        </tr>
                        </table>
                        </body>
                        </html>");

                msg.Body = bodySb.ToString();

                var smtpUserInfo = new NetworkCredential("no-reply@loyalistgroup.com", "loy9800408");
                var smtpclient = new SmtpClient("mail.loyalistgroup.com")
                {
                    Port = 587,
                    EnableSsl = false,
                    UseDefaultCredentials = false,
                    Credentials = smtpUserInfo
                };
                smtpclient.Send(msg);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}