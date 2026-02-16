using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker_System.Models
{
    public class ImpressListModel
    {
        public int RowID { get; set; }
        public int SrNo { get; set; }
        public string Company { get; set; }
        public string EmpName { get; set; }
        public string Month { get; set; }
        public int IYear { get; set; }
        public decimal ClaimedAmount { get; set; }

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

        public DateTime? PaymentDate { get; set; }
        public int TotalDays { get; set; }
    }

    public class ImpressFilterModel
    {
        public List<CompanyFilterModel> GetCompanyFilter = new List<CompanyFilterModel>();
        public List<DivisionFilterModel> GetDivisionFilter = new List<DivisionFilterModel>();
        public List<MonthFilterModel> GetMonthFilter = new List<MonthFilterModel>();
        public List<YearFilterModel> GetYearFilter = new List<YearFilterModel>();
    }

    public class CompanyFilterModel
    {
        public int RowID { get; set; }
        public string Name { get; set; }
    }

    public class MonthFilterModel
    {
        public int RowID { get; set; }
        public string ShortName { get; set; }
    }

    public class DivisionFilterModel
    {
        public int RowId { get; set; }
        public string DivisionName { get; set; }
    }

    public class YearFilterModel
    {
        public int EYear { get; set; }
    }

    public class FYearsFilterModel
    {
        public int RowID { get; set; }
        public string FYear { get; set; }
    }

    public class ExportImpressOtherTabModel
    {
        public int ReportType { get; set; }
        public int DivisionId { get; set; }
        public int CompanyId { get; set; }
        public int FYearId { get; set; }
    }

    public class GetImpressOtherTabModel : ExportImpressOtherTabModel
    {
        public List<DivisionFilterModel> GetDivisionFilter = new List<DivisionFilterModel>();
        public List<CompanyFilterModel> GetCompanyFilter = new List<CompanyFilterModel>();
        public List<FYearsFilterModel> GetFYearFilter = new List<FYearsFilterModel>();
    }




    public class AddEditImpressCompanyFilterModel
    {
        public int RowID { get; set; }
        public string Name { get; set; }
    }

    public class AddEditImpressMonthFilterModel
    {
        public int RowID { get; set; }
        public string ShortName { get; set; }
    }

    public class AddEditImpressDivisionFilterModel
    {
        public int RowId { get; set; }
        public string DivisionName { get; set; }
    }

    public class AddEditImpressYearsFilterModel
    {
        public string EYear { get; set; }
    }

    public class AddEditImpressDropDownListModel
    {
        public List<AddEditImpressDivisionFilterModel> DivisionList { get; set; } = new List<AddEditImpressDivisionFilterModel>();
        public List<AddEditImpressCompanyFilterModel> CompanyList { get; set; } = new List<AddEditImpressCompanyFilterModel>();
        public List<AddEditImpressMonthFilterModel> MonthList { get; set; } = new List<AddEditImpressMonthFilterModel>();
        public List<AddEditImpressYearsFilterModel> YearList { get; set; } = new List<AddEditImpressYearsFilterModel>();

        public int MaxNo { get; set; }
    }


    public class ImpressAddModel
    {
        public int RowID { get; set; }
        public int No { get; set; }

        public int DivisionRowId { get; set; }
        public int CompanyRowID { get; set; }
        public int EmpRowId { get; set; }

        public string EmployeeName { get; set; }

        public string Designation { get; set; }

        public string Zone { get; set; }

        public string Region { get; set; }

        public int IMonth { get; set; }
        public int IYear { get; set; }

        public decimal ClaimedAmount { get; set; }
        public decimal PassedAmount { get; set; }

        public DateTime? TourFrom { get; set; }
        public DateTime? TourTo { get; set; }

        //Commeical
        public bool IsCommercial { get; set; }
        public DateTime? CommercialDate { get; set; }
        public int CommercialNoOfDays { get; set; }

        //CrossCheck
        public bool IsCrossCheck { get; set; }
        public DateTime? CrossCheckingDate { get; set; }
        public int CrossCheckingNoOfDays { get; set; }

        //HOD
        public bool IsHeadApproval { get; set; }
        public DateTime? HeadForApprovalDate { get; set; }
        public int HeadForApprovalNoOfDays { get; set; }

        //IsDirector
        public bool IsDirectorApproval { get; set; }
        public DateTime? DirectorForApprovalDate { get; set; }
        public int DirectorForApprovalNoOfDays { get; set; }

        //Submmited
        public bool IsSubmitted { get; set; }
        public DateTime? SubmittedInAccountsDate { get; set; }
        public int SubmittedInAccountsNoOfDays { get; set; }

        //Payment
        public bool IsPayment { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidAmount { get; set; }

        public string PaymentCycle { get; set; }
        public int TotalDays { get; set; }
        public string Remarks { get; set; }

        // -------- Expenses --------Less Than 10000
        public decimal Less_TollExps { get; set; }
        public decimal Less_CourierExps { get; set; }
        public decimal Less_Stationery_XeroxExps { get; set; }
        public decimal Less_InternetExps { get; set; }
        public decimal Less_FareExps { get; set; }
        public decimal Less_TravelingExps { get; set; }
        public decimal Less_SalesPromotionExps { get; set; }
        public decimal Less_FreightExps { get; set; }
        public decimal Less_OtherExps { get; set; }

        //---------- Expenses ------- Greather Than 10000
        public decimal Greater_OutwardExps { get; set; }
        public decimal Greater_ConveyanceExps { get; set; }
        public decimal Greater_StationeryExps { get; set; }
        public decimal Greater_MaintenanceExps { get; set; }
        public decimal Greater_PostageExps { get; set; }
        public decimal Greater_PrintingExps { get; set; }
        public decimal Greater_SalesProExps { get; set; }
        public decimal Greater_TelephonExps { get; set; }
        public decimal Greater_WelfareExps { get; set; }
        public decimal Greater_AllOtherExps { get; set; }

        //Other Expenses
        public decimal Other_Rent { get; set; }
        public decimal Other_InternetCharges { get; set; }
        public decimal Other_MiscCharges { get; set; }
        public decimal Other_ElectricityBill { get; set; }
        public decimal Other_RoImprest { get; set; }
        public decimal Other_Maintenance { get; set; }

    }

    public class GetImpressAddModel : ImpressAddModel
    {
        public List<AddEditImpressDivisionFilterModel> DivisionList { get; set; } = new List<AddEditImpressDivisionFilterModel>();
        public List<AddEditImpressCompanyFilterModel> CompanyList { get; set; } = new List<AddEditImpressCompanyFilterModel>();
        public List<AddEditImpressMonthFilterModel> MonthList { get; set; } = new List<AddEditImpressMonthFilterModel>();
        public List<AddEditImpressYearsFilterModel> YearList { get; set; } = new List<AddEditImpressYearsFilterModel>();
    }

    public class ImpressEmployeeModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class ImpressSearchEmployeeDetailModel
    {
        public int EmpRowId { get; set; }
        public string EmpName { get; set; }
        public string Zone { get; set; }
        public string Region { get; set; }
        public string Designation { get; set; }
    }


    public class ImpressPaymentCycleModel
    {
        public string DisplayValue { get; set; }
        public string Value { get; set; }
    }

    public class SaveImpressModel
    {
        public int RowID { get; set; }
        public int No { get; set; }

        public int DivisionRowId { get; set; }
        public int CompanyRowID { get; set; }
        public int EmpRowId { get; set; }

        public string EmployeeName { get; set; }

        public string Designation { get; set; }

        public string Zone { get; set; }

        public string Region { get; set; }

        public int IMonth { get; set; }
        public int IYear { get; set; }

        public decimal ClaimedAmount { get; set; }
        public decimal PassedAmount { get; set; }

        public DateTime? TourFrom { get; set; }
        public DateTime? TourTo { get; set; }

        //Commeical
        public bool IsCommercial { get; set; }
        public DateTime? CommercialDate { get; set; }
        public int CommercialNoOfDays { get; set; }

        //CrossCheck
        public bool IsCrossCheck { get; set; }
        public DateTime? CrossCheckingDate { get; set; }
        public int CrossCheckingNoOfDays { get; set; }

        //HOD
        public bool IsHeadApproval { get; set; }
        public DateTime? HeadForApprovalDate { get; set; }
        public int HeadForApprovalNoOfDays { get; set; }

        //IsDirector
        public bool IsDirectorApproval { get; set; }
        public DateTime? DirectorForApprovalDate { get; set; }
        public int DirectorForApprovalNoOfDays { get; set; }

        //Submmited
        public bool IsSubmitted { get; set; }
        public DateTime? SubmittedInAccountsDate { get; set; }
        public int SubmittedInAccountsNoOfDays { get; set; }

        //Payment
        public bool IsPayment { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidAmount { get; set; }

        public int TotalDays { get; set; }
        public string PaymentCycle { get; set; }

        public string Remarks { get; set; }

        // -------- Expenses --------Less Than 10000
        public decimal Less_TollExps { get; set; }
        public decimal Less_CourierExps { get; set; }
        public decimal Less_Stationery_XeroxExps { get; set; }
        public decimal Less_InternetExps { get; set; }
        public decimal Less_FareExps { get; set; }
        public decimal Less_TravelingExps { get; set; }
        public decimal Less_SalesPromotionExps { get; set; }
        public decimal Less_FreightExps { get; set; }
        public decimal Less_OtherExps { get; set; }

        //---------- Expenses ------- Greather Than 10000
        public decimal Greater_OutwardExps { get; set; }
        public decimal Greater_ConveyanceExps { get; set; }
        public decimal Greater_StationeryExps { get; set; }
        public decimal Greater_MaintenanceExps { get; set; }
        public decimal Greater_PostageExps { get; set; }
        public decimal Greater_PrintingExps { get; set; }
        public decimal Greater_SalesProExps { get; set; }
        public decimal Greater_TelephonExps { get; set; }
        public decimal Greater_WelfareExps { get; set; }
        public decimal Greater_AllOtherExps { get; set; }

        //Other Expenses
        public decimal Other_Rent { get; set; }
        public decimal Other_InternetCharges { get; set; }
        public decimal Other_MiscCharges { get; set; }
        public decimal Other_ElectricityBill { get; set; }
        public decimal Other_RoImprest { get; set; }
        public decimal Other_Maintenance { get; set; }

    }


    public class ImpressEditModel
    {

        public int RowID { get; set; }
        public int No { get; set; }

        public int DivisionRowId { get; set; }
        public int CompanyRowID { get; set; }
        public int EmpRowId { get; set; }

        public string EmployeeName { get; set; }

        public string Designation { get; set; }

        public string Zone { get; set; }

        public string Region { get; set; }

        public int IMonth { get; set; }
        public int IYear { get; set; }

        public decimal ClaimedAmount { get; set; }
        public decimal PassedAmount { get; set; }

        public DateTime? TourFrom { get; set; }
        public DateTime? TourTo { get; set; }

        //Commeical
        public bool IsCommercial { get; set; }
        public DateTime? CommercialDate { get; set; }
        public int CommercialNoOfDays { get; set; }

        //CrossCheck
        public bool IsCrossCheck { get; set; }
        public DateTime? CrossCheckingDate { get; set; }
        public int CrossCheckingNoOfDays { get; set; }

        //HOD
        public bool IsHeadApproval { get; set; }
        public DateTime? HeadForApprovalDate { get; set; }
        public int HeadForApprovalNoOfDays { get; set; }

        //IsDirector
        public bool IsDirectorApproval { get; set; }
        public DateTime? DirectorForApprovalDate { get; set; }
        public int DirectorForApprovalNoOfDays { get; set; }

        //Submmited
        public bool IsSubmitted { get; set; }
        public DateTime? SubmittedInAccountsDate { get; set; }
        public int SubmittedInAccountsNoOfDays { get; set; }

        //Payment
        public bool IsPayment { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidAmount { get; set; }

        public int TotalDays { get; set; }
        public string PaymentCycle { get; set; }

        public string Remarks { get; set; }

        // -------- Expenses --------Less Than 10000
        public decimal Less_TollExps { get; set; }
        public decimal Less_CourierExps { get; set; }
        public decimal Less_Stationery_XeroxExps { get; set; }
        public decimal Less_InternetExps { get; set; }
        public decimal Less_FareExps { get; set; }
        public decimal Less_TravelingExps { get; set; }
        public decimal Less_SalesPromotionExps { get; set; }
        public decimal Less_FreightExps { get; set; }
        public decimal Less_OtherExps { get; set; }

        //---------- Expenses ------- Greather Than 10000
        public decimal Greater_OutwardExps { get; set; }
        public decimal Greater_ConveyanceExps { get; set; }
        public decimal Greater_StationeryExps { get; set; }
        public decimal Greater_MaintenanceExps { get; set; }
        public decimal Greater_PostageExps { get; set; }
        public decimal Greater_PrintingExps { get; set; }
        public decimal Greater_SalesProExps { get; set; }
        public decimal Greater_TelephonExps { get; set; }
        public decimal Greater_WelfareExps { get; set; }
        public decimal Greater_AllOtherExps { get; set; }

        //Other Expenses
        public decimal Other_Rent { get; set; }
        public decimal Other_InternetCharges { get; set; }
        public decimal Other_MiscCharges { get; set; }
        public decimal Other_ElectricityBill { get; set; }
        public decimal Other_RoImprest { get; set; }
        public decimal Other_Maintenance { get; set; }
    }
    public class GetImpressEditModel : ImpressEditModel
    {
        public List<AddEditImpressDivisionFilterModel> DivisionList { get; set; } = new List<AddEditImpressDivisionFilterModel>();
        public List<AddEditImpressCompanyFilterModel> CompanyList { get; set; } = new List<AddEditImpressCompanyFilterModel>();
        public List<AddEditImpressMonthFilterModel> MonthList { get; set; } = new List<AddEditImpressMonthFilterModel>();
        public List<AddEditImpressYearsFilterModel> YearList { get; set; } = new List<AddEditImpressYearsFilterModel>();

        public List<ImpressPaymentCycleModel> PaymentCycleList { get; set; } = new List<ImpressPaymentCycleModel>();
    }

    #region Impress Pending Model

    public class ImpressPendingModel
    {
        public int RowID { get; set; }
        public int SrNo { get; set; }
        public string Company { get; set; }
        public string EmpName { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal ClaimedAmount { get; set; }

        public DateTime? CommercialReceiptDate { get; set; }
        public DateTime? SendToCrossCheckingDate { get; set; }
        public DateTime? SendToHeadForApprovalDate { get; set; }
        public DateTime? SendToDirectorForApprovalDate { get; set; }
        public DateTime? SubmittedInAccountsDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? Imp_Bal_Con_SendDate { get; set; }
        public DateTime? Imp_Bal_Con_ReceivedDate { get; set; }

        public decimal PaidAmount { get; set; }
        public decimal PassedAmount { get; set; }
        public string Remarks { get; set; }
    }

    public class ImpressSaveModel
    {
        public int RowID { get; set; }
        public string Date1 { get; set; } // dd/MM/yy
    }

    public class SaveImpressRequest
    {
        public int GroupId { get; set; }
        public List<ImpressSaveModel> Rows { get; set; }
    }

    #endregion
}