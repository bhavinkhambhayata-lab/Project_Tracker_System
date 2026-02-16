using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker_System.Models
{
    public class ExportCommissiontracker
    {
        public string SrNo { get; set; }
        public string PI_No { get; set; }
        public string OCF_No { get; set; }
        public string INV_No { get; set; }
        public string Invoice_No { get; set; }
        public string SearchType { get; set; }
        public string Division { get; set; }

        public string PI_Date { get; set; }

        public string INV_Dates { get; set; }
        public bool SelectPreMarketing { get; set; }
        public bool emty { get; set; }
        public bool SelectCommercial { get; set; }
        public bool SelectPostMarketing { get; set; }
        public bool SelectLogistics { get; set; }

        public bool SelectAccounts { get; set; }
        public string CustomerName { get; set; }
        public string Commission_Vendor_Name { get; set; }
        public string VendorTab { get; set; }
        public string CommissionType { get; set; }
        public decimal CommissionRate { get; set; }
        public string SalesPerson_Name { get; set; }
        public string TentativeCommBasic_Amt { get; set; }
        public string ActualCommission { get; set; }
        public string FreightBillDate { get; set; }
        public string FreightAmt { get; set; }
        public string FreightBillNo { get; set; }
        public string Total { get; set; }
        public List<ExportCommissiontracker> lstFyear { get; set; }
        public List<ExportCommissionInvoiceLogDetail> ExportCommissionLogDetails { get; set; } = new List<ExportCommissionInvoiceLogDetail>();
        public bool TaggedOnPI { get; set; }
        public bool TaggedOnOCF { get; set; }
        public bool TaggedOnINV { get; set; }
        public bool TaggedAfterInvoice { get; set; }

        public string Claimform_Received { get; set; }

        public string CommissionworkingsenttoVendor_Employee { get; set; }

        public string CommissionInvoice_Received { get; set; }

        public string Credit_Advice_Prepared { get; set; }
        public string Credit_Advice_Details { get; set; }

        public string Credit_Note_Prepared { get; set; }
        public string CreditNotePreparedNo { get; set; }
        public string Vendor_Code { get; set; }
        public string FyearId { get; set; }
        public string EYear { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Fromtable { get; set; }
        public string FromNo { get; set; }
        public string CustomerNo { get; set; }
        public string sales_person { get; set; }
        public string Remarks { get; set; }
    }
    public class ExportCommissionInvoiceLogDetail
    {
        public string SectionName { get; set; }
        public DateTime? LogDate { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
    }
}