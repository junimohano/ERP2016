using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Erp2016.Lib.Extensions;
using Erp2016.Lib.Properties;

namespace Erp2016.Lib
{
    public class CInvoice
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CInvoice()
        {
        }

        public CInvoice(string id)
        {
            var qry = _dbView.vwStudentContracts.FirstOrDefault(q => q.InvoiceId == Convert.ToInt32(id));

            if (qry != null)
            {
                StartDate = qry.StartDate;
                EndDate = qry.EndDate;
                AgencyNet = Convert.ToDouble(qry.AgencyPriceSum);
                Balance = Convert.ToDouble(qry.Balance);
                Agency = qry.AgencyName;
                ProgramName = qry.ProgramName;
            }
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double AgencyNet { get; set; }
        public double Balance { get; set; }
        public string Agency { get; set; }
        public string ProgramName { get; set; }

        public Invoice Get(int invoiceId)
        {
            return _db.Invoices.FirstOrDefault(x => x.InvoiceId == invoiceId);
        }

        public vwInvoice GetVwInvoice(int invoiceId)
        {
            return _dbView.vwInvoices.FirstOrDefault(x => x.InvoiceId == invoiceId);
        }

        public int GetInvoiceIdbyHomestayStudentId(int HomestayStudentId)
        {
            int InvoiceId = 0;
            var Invoice = _db.Invoices.Where(q => q.HomestayRegistrationId == HomestayStudentId).FirstOrDefault();
            if (Invoice != null)
            {
                InvoiceId = Invoice.InvoiceId;
            }
            return InvoiceId;
        }

        public int Add(Invoice obj)
        {
            try
            {
                var invoiceType = _db.Dicts.Where(q => q.DictType == 30 && q.Value == obj.InvoiceType).FirstOrDefault();

                var nowYear = DateTime.Now.ToString("yy");

                // partialIndex
                if (obj.OriginalInvoiceId == null)
                {
                    var last = (from q in _db.Invoices
                                    //where q.InvoiceType == obj.InvoiceType && q.InvoiceNumber.Substring(2, 2) == nowYear
                                where q.InvoiceNumber.Substring(2, 2) == nowYear
                                orderby q.InvoiceIndex descending
                                select q).FirstOrDefault();

                    if (last == null)
                        obj.InvoiceIndex = 1;
                    else
                        obj.InvoiceIndex = last.InvoiceIndex + 1;

                    obj.InvoicePartialIndex = 1;
                }
                else
                {
                    var originalInvoice = _db.Invoices.FirstOrDefault(x => x.InvoiceId == obj.OriginalInvoiceId);
                    nowYear = originalInvoice.InvoiceNumber.Substring(2, 2);
                    obj.InvoiceIndex = originalInvoice.InvoiceIndex;
                    obj.InvoicePartialIndex = originalInvoice.InvoicePartialIndex + 1;
                }

                obj.InvoiceNumber = invoiceType.Abbreviation + nowYear + obj.InvoiceIndex.ToString("D6") + "_" + obj.InvoicePartialIndex.ToString("D2");
                obj.IsActive = true;
                obj.IsGross = false;

                _db.Invoices.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Invoices.Max(x => x.InvoiceId);
        }

        public bool Update(Invoice obj)
        {
            try
            {
                obj.UpdatedDate = DateTime.Now;

                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(Invoice obj)
        {
            try
            {
                _db.Invoices.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public int GetInvoiceStatus(int id)
        {
            var qry = _db.Invoices.Where(q => q.InvoiceId == id).FirstOrDefault();
            if (qry != null)
            {
                return Convert.ToInt32(qry.Status);
            }
            return -1;
        }

        public int GetInvoiceIdHomestay(int id)
        {
            var qry = _db.Invoices.FirstOrDefault(q => q.HomestayRegistrationId == id);

            if (qry != null)
            {
                return qry.InvoiceId;
            }

            return -1;
        }

        public List<CFilterListModel> GetInvoiceTypeList()
        {
            return _db.Dicts.Where(x => x.DictType == 30).OrderBy(q => q.Value).Select(p => new CFilterListModel { InvoiceType = p.Name }).Distinct().ToList();
        }

        public List<CFilterListModel> GetInvoiceStatusList()
        {
            return _db.Dicts.Where(x => x.DictType == 34).OrderBy(q => q.Value).Select(p => new CFilterListModel { InvoiceStatus = p.Name }).Distinct().ToList();
        }

        public DataTable GetVwInvoiceExcel(StringBuilder filterExpressionSb)
        {
            var tempDt = _dbView.ExecuteQuery<vwInvoiceExcel>("SELECT * FROM " + nameof(vwInvoiceExcel) + (filterExpressionSb.Length == 0 ? string.Empty : " WHERE " + filterExpressionSb));
            return CGlobal.ConvertToDataTable(tempDt);
        }

        public DataTable GetInvoiceDetailExcel(StringBuilder filterExpressionSb)
        {
            var whereExpression = (filterExpressionSb.Length == 0 ? string.Empty : " WHERE " + filterExpressionSb);

            var results = _dbView.ExecuteQueryToDictionary($@"
            -- Dynamic PIVOT
            DECLARE @T AS TABLE(y INT NOT NULL PRIMARY KEY);

            DECLARE
            @cols AS NVARCHAR(MAX),
            @y    AS INT,
            @sql  AS NVARCHAR(MAX);

            -- Construct the column list for the IN clause
            -- e.g., 2002,2003,2004
            SET @cols = STUFF(
            (SELECT N',' + QUOTENAME(y) AS [text()]
            FROM (SELECT DISTINCT ItemName AS y FROM InvoiceCoaItem) AS Y
            ORDER BY y
            FOR XML PATH('')),
            1, 1, N'');

            -- Construct the full T-SQL statement
            -- and execute dynamically
            SET @sql = N'SELECT *
            FROM (
            	SELECT *
            	FROM (
            			SELECT V100.InvoiceId, ''StandardPrice'' AS Name, V200.ItemName, V200.StandardPrice AS Price,
            			V100.OriginalInvoiceId, V100.InvoiceNumber, V100.StatusId, V100.InvoiceStatus, V100.StudentId, V100.StudentMasterNo, V100.StudentNo, V100.StudentName, V100.CountryId, V100.CountryName, V100.SiteLocationId, V100.SiteLocationName, V100.ProgramRegistrationId, V100.HomestayRegistrationId, V100.AgencyName, V100.InvoiceName, V100.StartDate, V100.EndDate, V100.CreatedId, V100.CreatedDate, V100.UpdatedId, V100.UpdatedDate, V100.CreatedUsername, V100.StandardPriceSum, V100.StudentPriceSum, V100.AgencyPriceSum, V100.IsFinancialGurantee, V100.IsActive, V100.InvoiceType, V100.SiteId, V100.SiteName, V100.InvoicePartialIndex, V100.ScholarshipId, V100.ScholarshipAmount, V100.ScholarshipWeeks, V100.PromotionId, V100.AgencyId, V100.Gender,
            			V200.InvoiceItemId, V200.InvoiceCoaItemId, V200.Remark
            			FROM vwInvoice AS V100 LEFT OUTER JOIN
            			vwInvoiceItem AS V200 ON V100.InvoiceId = V200.InvoiceId
                        {whereExpression}

            			UNION ALL

            			SELECT V100.InvoiceId, ''StudentPrice'' AS Name, V200.ItemName, V200.StudentPrice AS Price,
            			V100.OriginalInvoiceId, V100.InvoiceNumber, V100.StatusId, V100.InvoiceStatus, V100.StudentId, V100.StudentMasterNo, V100.StudentNo, V100.StudentName, V100.CountryId, V100.CountryName, V100.SiteLocationId, V100.SiteLocationName, V100.ProgramRegistrationId, V100.HomestayRegistrationId, V100.AgencyName, V100.InvoiceName, V100.StartDate, V100.EndDate, V100.CreatedId, V100.CreatedDate, V100.UpdatedId, V100.UpdatedDate, V100.CreatedUsername, V100.StandardPriceSum, V100.StudentPriceSum, V100.AgencyPriceSum, V100.IsFinancialGurantee, V100.IsActive, V100.InvoiceType, V100.SiteId, V100.SiteName, V100.InvoicePartialIndex, V100.ScholarshipId, V100.ScholarshipAmount, V100.ScholarshipWeeks, V100.PromotionId, V100.AgencyId, V100.Gender,
            			V200.InvoiceItemId, V200.InvoiceCoaItemId, V200.Remark
            			FROM vwInvoice AS V100 LEFT OUTER JOIN
            			vwInvoiceItem AS V200 ON V100.InvoiceId = V200.InvoiceId
                        {whereExpression}

            			UNION ALL

            			SELECT V100.InvoiceId, ''AgencyPrice'' AS Name, V200.ItemName, V200.AgencyPrice AS Price,
            			V100.OriginalInvoiceId, V100.InvoiceNumber, V100.StatusId, V100.InvoiceStatus, V100.StudentId, V100.StudentMasterNo, V100.StudentNo, V100.StudentName, V100.CountryId, V100.CountryName, V100.SiteLocationId, V100.SiteLocationName, V100.ProgramRegistrationId, V100.HomestayRegistrationId, V100.AgencyName, V100.InvoiceName, V100.StartDate, V100.EndDate, V100.CreatedId, V100.CreatedDate, V100.UpdatedId, V100.UpdatedDate, V100.CreatedUsername, V100.StandardPriceSum, V100.StudentPriceSum, V100.AgencyPriceSum, V100.IsFinancialGurantee, V100.IsActive, V100.InvoiceType, V100.SiteId, V100.SiteName, V100.InvoicePartialIndex, V100.ScholarshipId, V100.ScholarshipAmount, V100.ScholarshipWeeks, V100.PromotionId, V100.AgencyId, V100.Gender,
            			V200.InvoiceItemId, V200.InvoiceCoaItemId, V200.Remark
            			FROM vwInvoice AS V100 LEFT OUTER JOIN
            			vwInvoiceItem AS V200 ON V100.InvoiceId = V200.InvoiceId
                        {whereExpression}
            	) AS T100
            ) AS D
            PIVOT(MAX(Price) FOR ItemName IN(' + @cols + N')) AS P
            ORDER BY P.InvoiceId, 	
                CASE WHEN P.Name = ''StandardPrice'' THEN 1
            	WHEN P.Name = ''StudentPrice'' THEN 2
            	WHEN P.Name = ''AgencyPrice'' THEN 3 END;';
            EXEC sp_executesql @sql;");

            return ToDataTable(results);
        }

        private DataTable ToDataTable(IEnumerable<Dictionary<string, object>> list)
        {
            DataTable result = new DataTable();
            if (!list.Any())
                return result;

            var columnNames = list.SelectMany(dict => dict.Keys).Distinct();
            result.Columns.AddRange(columnNames.Select(c => new DataColumn(c)).ToArray());
            foreach (Dictionary<string, object> item in list)
            {
                var row = result.NewRow();
                foreach (var key in item.Keys)
                {
                    row[key] = item[key];
                }

                result.Rows.Add(row);
            }

            return result;
        }

    }

}