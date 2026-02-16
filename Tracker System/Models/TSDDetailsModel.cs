using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker_System.Models
{

    public class TSDDetailsModel
    {
        public int RowID { get; set; }
        public int SrNo { get; set; }
        public string Company { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal TSDAmount { get; set; }

        public DateTime? CommercialReceiptDate { get; set; }
        public int CommercialNoOfDays { get; set; }

        public DateTime? SendToCrossCheckingDate { get; set; }
        public int SendToCrossCheckingNoOfDays { get; set; }

        public DateTime? SendToHeadForApprovalDate { get; set; }
        public int SendToHeadForApprovalNoOfDays { get; set; }

        public DateTime? SendToDirectorForApprovalDate { get; set; }
        public int SendToDirectorForApprovalNoOfDays { get; set; }

        public DateTime? SubmittedInAccountsDate { get; set; }
        public int SubmittedInAccountsNoOfDays { get; set; }

        public int TotalDays { get; set; }

        public DateTime? PaymentDate { get; set; }
        public string ChequeNumber { get; set; }
        public decimal PaymentAmount { get; set; }

        public DateTime? CurieredOnSentDate { get; set; }
        public int CurieredOnNoOfDays { get; set; }
        public DateTime? CurieredOnConfirmationDate { get; set; }

        public string Remarks { get; set; }
    }

    public class CompanyListModel
    {
        public string ID { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
    }

    public class BrandModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class TSDPendingModel
    {
        public int RowID { get; set; }
        public int SrNo { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public decimal TSDAmount { get; set; }

        public DateTime? CommercialReceiptDate { get; set; }
        public DateTime? SendToCrossCheckingDate { get; set; }
        public DateTime? SendToHeadForApprovalDate { get; set; }
        public DateTime? SendToDirectorForApprovalDate { get; set; }
        public DateTime? SubmittedInAccountsDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? CurieredOnSentDate { get; set; }
        public DateTime? CurieredOnConfirmationDate { get; set; }

        public string Remarks { get; set; }
    }

    public class SaveTSDRequest
    {
        public int GroupId { get; set; }
        public List<TSDSaveModel> Rows { get; set; }
    }


    public class TSDSaveModel
    {
        public int RowID { get; set; }
        public string Date1 { get; set; } // dd/MM/yy
    }

    public class GetTSDOtherReportModel : ExportTSDOtherReportModel
    {
        public List<CompanyListModel> GetCompanyList { get; set; } = new List<CompanyListModel>();

        public List<BrandModel> GetBrandList { get; set; } = new List<BrandModel>();

    }

    public class ExportTSDOtherReportModel
    {
        public int ReportType { get; set; }
        public string CompanyId { get; set; }
        public string BrandId { get; set; }
        public string Name { get; set; }
    }

    public class RegisterTSDReportModel
    {
        // Basic Details
        public string Company { get; set; }
        public string BrandName { get; set; }
        public string Region { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Regarding { get; set; }

        // Amount
        public decimal TSDAmount { get; set; }

        // Commercial
        public DateTime? CommercialReceiptDate { get; set; }
        public int CommercialNoOfDays { get; set; }

        // Cross Checking
        public DateTime? SendToCrossCheckingDate { get; set; }
        public int SendToCrossCheckingNoOfDays { get; set; }

        // HOD Approval
        public DateTime? SendToHeadForApprovalDate { get; set; }
        public int SendToHeadForApprovalNoOfDays { get; set; }

        // Director Approval
        public DateTime? SendToDirectorForApprovalDate { get; set; }
        public int SendToDirectorForApprovalNoOfDays { get; set; }

        // Refund / Accounts
        public decimal TotalRefundAmount { get; set; }
        public DateTime? SubmittedInAccountsDate { get; set; }
        public int SubmittedInAccountsNoOfDays { get; set; }

        // Payment
        public DateTime? PaymentDate { get; set; }
        public string ChequeNumber { get; set; }
        public decimal PaymentAmount { get; set; }

        // Total & Courier
        public int TotalDays { get; set; }
        public DateTime? CurieredOnSentDate { get; set; }
        public int CurieredOnNoOfDays { get; set; }
        public DateTime? CurieredOnConfirmationDate { get; set; }

        // Remarks
        public string Remarks { get; set; }
    }


    public class TSDSubmittedAccountPendingModel
    {
        public int RowId { get; set; }
        public string Company { get; set; }
        public string Brand { get; set; }
        public string BrandName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Regarding { get; set; }

        public decimal TSDAmount { get; set; }

        public DateTime? CommercialReceiptDate { get; set; }
        public int CommercialNoOfDays { get; set; }

        public DateTime? SendToCrossCheckingDate { get; set; }
        public int SendToCrossCheckingNoOfDays { get; set; }

        public DateTime? SendToHeadForApprovalDate { get; set; }
        public int SendToHeadForApprovalNoOfDays { get; set; }

        public DateTime? SendToDirectorForApprovalDate { get; set; }
        public int SendToDirectorForApprovalNoOfDays { get; set; }

        public decimal TotalRefundAmount { get; set; }

        public DateTime? SubmittedInAccountsDate { get; set; }
        public int SubmittedInAccountsNoOfDays { get; set; }

        public DateTime? PaymentDate { get; set; }
        public string ChequeNumber { get; set; }
        public decimal PaymentAmount { get; set; }

        public int TotalDays { get; set; }

        public DateTime? CurieredOnSentDate { get; set; }
        public int CurieredOnNoOfDays { get; set; }
        public DateTime? CurieredOnConfirmationDate { get; set; }

        public string Remarks { get; set; }
    }


    public class TSDDetailsViewModel
    {
        /* =======================
           1. DETAILS SECTION
           ======================= */
        public int RowID { get; set; }
        public int CompanyID { get; set; }
        public int BrandID { get; set; }
        public int No { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string TableName { get; set; } = "";
        public string MasterCode { get; set; } = "";
        public string City { get; set; }
        public string Region { get; set; }
        public string Regarding { get; set; }
        public decimal? TSDAmount { get; set; }

        /* =======================
           2. COMMERCIAL
           ======================= */

        public DateTime? CommercialReceiptDate { get; set; }
        public int? CommercialNoOfDays { get; set; }


        /* =======================
           3. HOD APPROVAL
           ======================= */

        public DateTime? HODReceiptDate { get; set; }
        public int? HODNoOfDays { get; set; }


        /* =======================
           4. MARKETING HEAD
           ======================= */

        public DateTime? MktReceiptDate { get; set; }
        public int? MktNoOfDays { get; set; }


        /* =======================
           5. DIRECTOR APPROVAL
           ======================= */

        public DateTime? DirectorReceiptDate { get; set; }
        public int? DirectorNoOfDays { get; set; }
        public decimal? TotalRefundAmount { get; set; }
        /* =======================
           6. SUBMITTED IN ACCOUNT
           ======================= */
        public DateTime? SubmittedDate { get; set; }
        public int? SubmittedNoOfDays { get; set; }

        public int? TotalDays { get; set; }
        /* =======================
           7. PAYMENT
           ======================= */

        public DateTime? PaymentDate { get; set; }
        public string ChequeNumber { get; set; }
        public decimal? PaymentAmount { get; set; }
        /* =======================
           8. COURIERED ON
           ======================= */

        public DateTime? CourierSentDate { get; set; }
        public int? CourierNoOfDays { get; set; }

        public DateTime? ConfirmationReceiptDate { get; set; }


        /* =======================
           OTHER
           ======================= */

        public string Remarks { get; set; }


        /* =======================
           DROPDOWN / LIST DATA
           ======================= */

        public List<CompanyListModel> CompanyList { get; set; } = new List<CompanyListModel>();
        public List<BrandModel> BrandList { get; set; } = new List<BrandModel>();
        public List<EmployeeModel> EmployeeList { get; set; } = new List<EmployeeModel>();

        public bool IsCommercial { get; set; }
        public bool IsHOD { get; set; }
        public bool IsMKT { get; set; }
        public bool IsDirector { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsPayment { get; set; }
        public bool IsCourier { get; set; }

        public bool IsConfm { get; set; }

    }


    public class TSDNameSearchModel
    {
        public string MasterCode { get; set; }
        public string Name { get; set; }
    }

    public class TSDNameDetailsModel
    {
        public string MasterCode { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
    }

    public class TSDNameSearchOnlyCustomerModel
    {
        public string MasterCode { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
    }

    public class TSDNameDetailsOnlyCustomerModel
    {
        public string MasterCode { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string TableName { get; set; }
        public decimal TSDAmount { get; set; }
    }


    public class SaveTSDDetailModel
    {
        public int RowID { get; set; }

        // ================= Basic =================
        public int No { get; set; }
        public int? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int? BrandID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Regarding { get; set; }

        public string TableName { get; set; }

        public string MasterCode { get; set; }

        public decimal TSDAmount { get; set; }

        // ================= FLAGS (VB Checked equivalent) =================
        public bool IsCommercial { get; set; }
        public bool IsHOD { get; set; }
        public bool IsMkt { get; set; }
        public bool IsDirector { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsPayment { get; set; }
        public bool IsCourier { get; set; }
        public bool IsConfirmation { get; set; }

        // ================= Dates + Days =================
        public DateTime? CommercialReceiptDate { get; set; }
        public int CommercialNoOfDays { get; set; }

        public DateTime? HODReceiptDate { get; set; }
        public int HODNoOfDays { get; set; }

        public DateTime? MktReceiptDate { get; set; }
        public int MktNoOfDays { get; set; }

        public DateTime? DirectorReceiptDate { get; set; }
        public int DirectorNoOfDays { get; set; }

        public decimal? TotalRefundAmount { get; set; }

        public DateTime? SubmittedDate { get; set; }
        public int SubmittedNoOfDays { get; set; }

        public DateTime? PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string ChequeNumber { get; set; }

        public DateTime? CourierSentDate { get; set; }
        public int CourierNoOfDays { get; set; }

        public DateTime? ConfirmationReceiptDate { get; set; }

        // ================= Calculated =================
        public int TotalDays { get; set; }
        public string Remarks { get; set; }
    }

    public class GetEditTSDDetailsViewModel
    {
        public int RowID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int BrandID { get; set; }
        public int No { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string TableName { get; set; } = "";
        public string MasterCode { get; set; } = "";
        public string City { get; set; }
        public string Region { get; set; }
        public string Regarding { get; set; }
        public decimal TSDAmount { get; set; }

        //Commecial
        public bool IsCommercial { get; set; } = false;
        public DateTime? CommercialReceiptDate { get; set; }
        public int? CommercialNoOfDays { get; set; }

        //HOD
        public bool IsHOD { get; set; } = false;
        public DateTime? HODReceiptDate { get; set; }
        public int? HODNoOfDays { get; set; }

        //MKT
        public bool IsMKT { get; set; } = false;
        public DateTime? MktReceiptDate { get; set; }
        public int? MktNoOfDays { get; set; }

        //Directors
        public bool IsDirector { get; set; } = false;
        public DateTime? DirectorReceiptDate { get; set; }
        public int? DirectorNoOfDays { get; set; }
        public decimal? TotalRefundAmount { get; set; }

        //Submmited
        public bool IsSubmitted { get; set; } = false;
        public DateTime? SubmittedDate { get; set; }
        public int? SubmittedNoOfDays { get; set; }
        public int? TotalDays { get; set; }

        //Payment
        public bool IsPayment { get; set; } = false;
        public DateTime? PaymentDate { get; set; }
        public string ChequeNumber { get; set; }
        public decimal? PaymentAmount { get; set; }

        //Courier
        public bool IsCourier { get; set; } = false;
        public DateTime? CourierSentDate { get; set; }
        public int? CourierNoOfDays { get; set; }

        //Confimation
        public bool IsConfm { get; set; } = false;
        public DateTime? ConfirmationReceiptDate { get; set; }
        public string Remarks { get; set; }

        public List<CompanyListModel> CompanyList { get; set; } = new List<CompanyListModel>();
        public List<BrandModel> BrandList { get; set; } = new List<BrandModel>();
        public List<EmployeeModel> EmployeeList { get; set; } = new List<EmployeeModel>();

    }

    public class EmployeeModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}