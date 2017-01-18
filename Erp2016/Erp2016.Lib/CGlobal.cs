using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Data.Linq;

namespace Erp2016.Lib
{
    public class CGlobal
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public static void Copy(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            var propMap = GetMatchingProperties(sourceType, targetType);

            for (var i = 0; i < propMap.Count; i++)
            {
                var prop = propMap[i];
                var sourceValue = prop.SourceProperty.GetValue(source, null);
                prop.TargetProperty.SetValue(target, sourceValue, null);
            }
        }

        public static IList<CPropertyMapModel> GetMatchingProperties(Type sourceType, Type targetType)
        {
            var sourceProperties = sourceType.GetProperties();
            var targetProperties = targetType.GetProperties();

            var properties = (from s in sourceProperties
                              from t in targetProperties
                              where s.Name == t.Name &&
                                    s.CanRead &&
                                    t.CanWrite &&
                                    s.PropertyType.IsPublic &&
                                    t.PropertyType.IsPublic &&
                            s.PropertyType == t.PropertyType &&
                            (
                              (s.PropertyType.IsValueType && t.PropertyType.IsValueType
                              ) ||
                              (s.PropertyType == typeof(string) &&
                               t.PropertyType == typeof(string)
                              )
                            )
                              select new CPropertyMapModel()
                              {
                                  SourceProperty = s,
                                  TargetProperty = t
                              }).ToList();
            return properties;
        }

        public static string Serialize(object o)
        {
            var ser = new XmlSerializer(o.GetType());
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            ser.Serialize(writer, o);
            return sb.ToString();
        }

        public static T Deserialize<T>(string s)
        {
            var xdoc = new XmlDocument();

            try
            {
                xdoc.LoadXml(s);
                var reader = new XmlNodeReader(xdoc.DocumentElement);
                var ser = new XmlSerializer(typeof(T));
                var obj = ser.Deserialize(reader);

                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }

        public List<CListModel> GetSiteLocationBySiteId(int id)
        {
            var result = new List<CListModel>();
            var siteLocationList = _db.SiteLocations.Where(q => q.SiteId == id);

            foreach (var d in siteLocationList.OrderBy(q => q.Name))
                result.Add(new CListModel { Name = d.Name, Value = d.SiteLocationId.ToString() });

            return result;
        }

        public List<CListModel> LoadRoomList(int HostId)
        {
            var result = new List<CListModel>();
            result.Add(new CListModel { Name = "Please Select Room", Value = "0" });
            var RoomLocationList = _db.HomestayHostRooms.Where(q => q.HostId == HostId);
            string floor_location = "";
            foreach (var r in RoomLocationList.OrderBy(q => q.HostRoomName))
            {
                switch (r.HostRoomFloor)
                {
                    case 5:
                        floor_location = "Basemnet";
                        break;
                    case 1:
                        floor_location = "First Floor";
                        break;
                    case 2:
                        floor_location = "Second Floor";
                        break;
                    case 3:
                        floor_location = "Third Floor";
                        break;
                    case 4:
                        floor_location = "Other Floor";
                        break;
                    default:
                        break;
                }
                result.Add(new CListModel { Name = r.HostRoomName + ": " + floor_location, Value = r.HostRoomId.ToString() });

            }
            return result;
        }

        public List<CListModel> LoadDormatoryRoomList(int HostId)
        {
            var result = new List<CListModel>();
            result.Add(new CListModel { Name = "Please Select Room", Value = "0" });
            var RoomLocationList = _db.DormitoryHostRooms.Where(q => q.HostId == HostId);
            foreach (var r in RoomLocationList.OrderBy(q => q.HostRoomName))
            {

                result.Add(new CListModel { Name = r.HostRoomName, Value = r.HostRoomId.ToString() });

            }
            return result;
        }
        public List<CListModel> LoadSchoolList(int userid)
        {
            var result = new List<CListModel>();
            result.Add(new CListModel { Name = "Please Select School", Value = "0" });

            var SchoolList = HomestayHostSchoolList(userid);//db.Sites;
            foreach (var s in SchoolList.OrderBy(q => q.Abbreviation))
            {
                result.Add(new CListModel { Name = s.Abbreviation + ": " + s.City + "(" + s.Address + ")", Value = s.SiteLocationId.ToString() });
            }
            return result;

        }

        public List<CListModel> LoadStudentList(int SiteLocationId)
        {
            var result = new List<CListModel>();
            //  result.Add(new CListModel { Name = "Please Select Student Name", Value = "0" });

            var StudentList = _db.Students.Where(q => q.SiteLocationId == SiteLocationId);
            foreach (var s in StudentList.OrderBy(q => q.FirstName))
            {
                result.Add(new CListModel { Name = new CStudent().GetStudentName(s) + " (Student No.: " + s.StudentNo + ")", Value = s.StudentId.ToString() });
            }
            return result;

        }

        public ISingleResult<spGetHomestayHostSchoolListResult> HomestayHostSchoolList(int userid)
        {
            return _dbView.spGetHomestayHostSchoolList(userid);
        }
        public ISingleResult<spGetHomestayHostSchoolListResult> HomestayStudentList(int userid)
        {
            return _dbView.spGetHomestayHostSchoolList(userid);
        }

        public List<CListModel> LoadSchooContactlList(int SiteLoctionId)
        {
            var result = new List<CListModel>();
            result.Add(new CListModel { Name = "Please Select School Contact Person", Value = "0" });

            var SchoolList = HomestayHostSchoolContactList(SiteLoctionId);//db.Sites;
            foreach (var s in SchoolList.OrderBy(q => q.UserName))
            {
                result.Add(new CListModel { Name = "Name: " + s.UserName + ", Position: " + s.Name + ", Phone: " + s.Phone, Value = s.UserId.ToString() });
            }
            return result;

        }

        public ISingleResult<spGetHomestayHostSchoolContactlListResult> HomestayHostSchoolContactList(int SiteLocationId)
        {
            return _dbView.spGetHomestayHostSchoolContactlList(SiteLocationId);
        }

        public int GetSiteBySiteLocationId(int id)
        {
            var result = 0;

            var site = _db.SiteLocations.Where(q => q.SiteLocationId == id).FirstOrDefault();

            if (site != null)
            {
                result = site.SiteId;
            }

            return result;
        }

        public List<CListModel> GetSiteLocation(int id)
        {
            var result = new List<CListModel>();

            var siteLocationList = _db.SiteLocations.Where(q => q.SiteLocationId == id);

            foreach (var d in siteLocationList.OrderBy(q => q.Name))
            {
                result.Add(new CListModel { Name = d.Name, Value = d.SiteLocationId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetSiteId(int id)
        {
            var result = new List<CListModel>();

            var sites = _db.Sites.Where(q => id == 0 || q.SiteId == id);

            foreach (var d in sites.OrderBy(q => q.Name))
            {
                result.Add(new CListModel { Name = d.Abbreviation, Value = d.SiteId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetSiteId()
        {
            var result = new List<CListModel>();

            var sites = _db.Sites;

            foreach (var d in sites.OrderBy(q => q.Name))
            {
                result.Add(new CListModel { Name = d.Abbreviation, Value = d.SiteId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetCountry()
        {
            var result = new List<CListModel>();

            var countries = _db.Countries;

            foreach (var d in countries.Where(q => q.RegionId != null).OrderBy(q => q.CountryId))
            {
                result.Add(new CListModel { Name = d.Name, Value = d.CountryId.ToString() });
            }

            return result;
        }

        public string GetCountry(int id)
        {
            string CountryName = null;
            var country = _db.Countries.Where(q => q.CountryId == id).FirstOrDefault();
            if (country != null)
                CountryName = country.Name;
            return CountryName;
        }

        public List<CListModel> GetLanguage()
        {
            var result = new List<CListModel>();

            var lan = _db.Languages;

            foreach (var d in lan.Where(q => q.Name != null).OrderBy(q => q.Name))
            {
                result.Add(new CListModel { Name = d.Name, Value = d.LanguageId.ToString() });
            }

            return result;
        }

        public string GetLanguage(int id)
        {
            string Language = null;
            var lan = _db.Languages.Where(q => q.LanguageId == id).FirstOrDefault();
            if (lan != null)
                Language = lan.Name;
            return Language;
        }

        public List<CListModel> GetDictionary(int dictype)
        {
            var result = new List<CListModel>();

            var dics = _db.Dicts.Where(q => q.DictType == dictype);

            foreach (var d in dics.OrderBy(q => q.Value))
            {
                result.Add(new CListModel { Name = d.Name, Value = d.Value.ToString() });
            }

            return result;
        }

        public double GetDistanceByAddress(string address1, string address2)
        {
            double distance = 0;

            var response = XDocument.Load("http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + address1 + "&destinations=" + address2 + "&mode=driving&sensor=false");

            var qry = (from q in response.Element("DistanceMatrixResponse").Elements("row").Elements("element") select q);

            foreach (var q in qry)
            {
                try
                {
                    var d = Convert.ToDouble(q.Element("distance").Element("value").Value);

                    if (distance > 0 && d <= distance || distance == 0)
                    {
                        distance = d;
                    }
                }
                catch (Exception)
                {
                }
            }

            return distance;
        }

        public int CheckApprovalEnable(int type, int id)
        {
            var supervisorId = 0;

            if (type == (int)CConstValue.Approval.Refund)
            {
                var qry = _dbView.vwRefundApprovalLists.FirstOrDefault(q => q.RefundId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.BusinessTrip)
            {
                var qry = _dbView.vwBusinessTripApprovalLists.FirstOrDefault(q => q.BusinessTripId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.Expense)
            {
                var qry = _dbView.vwExpenseApprovalLists.FirstOrDefault(q => q.ExpenseId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.Hire)
            {
                var qry = _dbView.vwHireApprovalLists.FirstOrDefault(q => q.No == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.Vacation)
            {
                var qry = _dbView.vwVacationApprovalLists.FirstOrDefault(q => q.VacationId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.Package)
            {
                var qry = _dbView.vwPackageProgramApprovalLists.FirstOrDefault(q => q.PackageProgramId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.Scholarship)
            {
                var qry = _dbView.vwScholarshipApprovalLists.FirstOrDefault(q => q.ScholarshipId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.Promotion)
            {
                var qry = _dbView.vwPromotionApprovalLists.FirstOrDefault(q => q.PromotionId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.CreditMemoPayout)
            {
                var qry = _dbView.vwCreditMemoPayoutApprovalLists.FirstOrDefault(q => q.CreditMemoPayoutId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.Agency)
            {
                var qry = _dbView.vwAgencyApprovalLists.FirstOrDefault(q => q.AgencyId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            else if (type == (int)CConstValue.Approval.PurchaseOrder)
            {
                var qry = _dbView.vwPurchaseOrderApprovalLists.FirstOrDefault(q => q.PurchaseOrderId == id && q.IsApprovalRequest == true && q.ApprovalStep == null);
                if (qry != null)
                    supervisorId = Convert.ToInt32(qry.ApprovalUser);
            }
            return supervisorId;
        }

        public int GetApprovalValue(int approveType)
        {
            // approved
            int approvalStatus = (int)CConstValue.ApprovalStatus.Approved;
            switch (approveType)
            {
                case (int)CConstValue.Approval.Expense:
                case (int)CConstValue.Approval.BusinessTrip:
                case (int)CConstValue.Approval.PurchaseOrder:
                case (int)CConstValue.Approval.CorporateCreditCard:
                    approvalStatus = (int)CConstValue.ApprovalStatus.WaitingForPreviewFromHq;
                    break;
            }
            return approvalStatus;
        }

        public string GetIPAddress()
        {
            var GetLan = false;

            var visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }

            if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                var stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                var ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                var arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }
            }


            return visitorIPAddress;
        }

        //public static DataTable ConvertToDataTable<T>(IEnumerable<T> source)
        //{
        //    var properties = typeof(T).GetProperties();

        //    var output = new DataTable();

        //    foreach (var prop in properties)
        //    {
        //        // prop.PropertyType;
        //        output.Columns.Add(prop.Name, typeof(string));
        //    }

        //    foreach (var item in source)
        //    {
        //        var row = output.NewRow();

        //        foreach (var prop in properties)
        //        {
        //            row[prop.Name] = prop.GetValue(item, null);
        //        }

        //        output.Rows.Add(row);
        //    }
        //    return output;
        //}

        public static DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
        {
            var props = typeof(TSource).GetProperties();

            var dt = new DataTable();

            dt.Columns.AddRange(
              props.Select(p => new DataColumn(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType)).ToArray()
            );

            source.ToList().ForEach(
              i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
            );

            return dt;
        }

        public static string GetDateFormat(DateTime date)
        {
            return date.ToString("MM-dd-yyyy") + " " + date.ToShortTimeString();
        }

        // <summary>
        // Get the name of a static or instance property from a property access lambda.
        // </summary>
        // <typeparam name="T">Type of the property</typeparam>
        // <param name="propertyLambda">lambda expression of the form: '() => Class.Property' or '() => object.Property'</param>
        // <returns>The name of the property</returns>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        public static string GetTableName(string info)
        {
            var temp = info.Replace("Table(", string.Empty);
            if (temp.EndsWith("y)"))
                temp = temp.Replace("y)", "ies");
            else
                temp = temp.Replace(")", "s");
            return temp;
        }

        public string GetLogoImagePath(int siteLocationId, CConstValue.ImageType imageType)
        {
            // location
            var siteLocation = new CSiteLocation().Get(siteLocationId);
            var site = new CSite().Get(Convert.ToInt32(siteLocation.SiteId));

            var fileSb = new StringBuilder();
            fileSb.Append(site.Abbreviation);

            switch (imageType)
            {
                case CConstValue.ImageType.Basic:
                    // nothing
                    break;
                case CConstValue.ImageType.Small:
                    fileSb.Append("_small");
                    break;
                case CConstValue.ImageType.Sign:
                    fileSb.Append("_sign");
                    break;
                case CConstValue.ImageType.Logo:
                    fileSb.Append("_logo");
                    break;
                case CConstValue.ImageType.LogoSide:
                    fileSb.Append("_side");
                    break;
            }


            fileSb.Append(".png");

            return HttpContext.Current.Server.MapPath("~/assets/img/" + fileSb);
        }

    }
}

