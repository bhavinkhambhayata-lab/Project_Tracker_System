using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Tracker_System.Models
{
    public class Exporttracker
    {
        public string SrNo { get; set; }
        public int rowid { get; set; }
        public string Division { get; set; }
        public string Mfr_Out { get; set; }
        public string Invoice_No { get; set; }
        public string InvDate { get; set; }
        public string Bill_to_Name { get; set; }
        public string Ship_to_Name { get; set; }
        public string Shipping_Bill_No { get; set; }
        public string Shipping_Bill_Date { get; set; }
        public string EPCG_Licence_No { get; set; }
        public string Currency { get; set; }
        public string Basic_Amt_Charges { get; set; }
        public string Sea_Freight_Air_Freight { get; set; }
        public string Insurarnce_charged { get; set; }
        public string Invoice_Value { get; set; }
        public string AirWay_BillNo_Bill_of_LodingNo { get; set; }
        public string AirWay_BillDate_Bill_of_LodingDate { get; set; }
        public string Other_Charges { get; set; }
        public string Mode { get; set; }
        public string Country { get; set; }
        public string Clearing_Point { get; set; }
        public List<ClearingPointModel> lstCleringPoint { get; set; }
        public List<FCLModel> lstFCL{ get; set; }
        public string Port_of_Discharge { get; set; }
        public string Port_of_Loading { get; set; }
        public string Inco_Terms { get; set; }
        public List<Inco_TermsModel> lstInco_Terms { get; set; }

        public string Payment_Terms { get; set; }
        public string Due_Date { get; set; }
        public string Remarks { get; set; }
        public string CCClearance_Agent_Name { get; set; }
        public string CCInvoice_No { get; set; }
        public string CCInvDate { get; set; }

        public string CCEDI_Xeam_charges { get; set; }
        public string CCEdi_Xeam_ChargesGST { get; set; }
        public string CCEdi_Xeam_ChargesGSTAmount { get; set; }
        public string CCEdi_Xeam_Charges_Total { get; set; }
        public string CCClearance_agency_charges { get; set; }
        public string CCClearance_Agency_chargesGST { get; set; }
        public string CCClearance_Agency_chargesGSTAmount { get; set; }
        public string CCClearance_Agency_charges_Total { get; set; }
        public string CCVGM { get; set; }
        public string CCVGMGST { get; set; }
        public string CCVGMGSTAmount { get; set; }
        public string CCVGM_Total { get; set; }
        public string CCGSEC { get; set; }
        public string CCGSECGST { get; set; }
        public string CCGSECGSTAmount { get; set; }
        public string CCGSEC_Total { get; set; }
        public string CCCOO { get; set; }
        public string CCCOOGST { get; set; }
        public string CCCOOGSTAmount { get; set; }
        public string CCCOO_Total { get; set; }
        public string CCExamination_CFS { get; set; }
        public string CCExamination_CFSGST { get; set; }
        public string CCExamination_CFSGSTAmount { get; set; }
        public string CCExamination_CFS_Total { get; set; }
        public string CCLift_on_Lift_off { get; set; }
        public string CCLift_on_Lift_offGST { get; set; }
        public string CCLift_on_Lift_offGSTAmount { get; set; }
        public string CCLift_on_Lift_off_Total { get; set; }
        public string CCCFS { get; set; }
        public string CCCFSGST { get; set; }
        public string CCCFSGSTAmount { get; set; }
        public string CCCFSTotal { get; set; }
        public string CCOthers { get; set; }
        public string CCOthersGST { get; set; }
        public string CCOthersGSTAmount { get; set; }
        public string CCOthers_Total { get; set; }

        public string CCAmt_Before_GST { get; set; }
        public string CCGST { get; set; }
        public string CC_total { get; set; }
        public string FCForwarder { get; set; }
        public string FCInvoice_No { get; set; }
        public string FCInvDate { get; set; }
        public string FCSFreight { get; set; }
        public string FCSFreight_GST { get; set; }
        public string FCSFreight_GSTAmount { get; set; }
        public string FCSFreight_Total { get; set; }
        public string FCSTHC { get; set; }
        public string FCSTHC_GST { get; set; }
        public string FCSTHC_GSTAmount { get; set; }
        public string FCSTHC_Total { get; set; }
        public string FCSBL { get; set; }
        public string FCSBL_GST { get; set; }
        public string FCSBL_GSTAmount { get; set; }
        public string FCSBL_Total { get; set; }
        public string FCSSeal { get; set; }
        public string FCSSEAL_GST { get; set; }
        public string FCSSEAL_GSTAmount { get; set; }
        public string FCSSEAL_Total { get; set; }
        public string FCSVGM { get; set; }
        public string FCSVGM_GST { get; set; }
        public string FCSVGM_GSTAmount { get; set; }
        public string FCSVGM_Total { get; set; }
        public string FCSMUC { get; set; }
        public string FCSMUC_GST { get; set; }
        public string FCSMUC_GSTAmount { get; set; }
        public string FCSMUC_Total { get; set; }
        public string FCSITHC { get; set; }
        public string FCSITHC_GST { get; set; }
        public string FCSITHC_GSTAmount { get; set; }
        public string FCSITHC_Total { get; set; }

        public string FCSDry_Port_charges { get; set; }
        public string FCSDry_Port_charges_GST { get; set; }
        public string FCSDry_Port_charges_GSTAmount { get; set; }
        public string FCSDry_Port_charges_Total { get; set; }

        public string FCSAdministrative_charges { get; set; }
        public string FCSAdministrative_charges_GST { get; set; }
        public string FCSAdministrative_charges_GSTAmount { get; set; }
        public string FCSAdministrative_charges_Total { get; set; }

        public string FCSSecurity_filling_fees { get; set; }
        public string FCSSecurity_filling_fees_GST { get; set; }
        public string FCSSecurity_filling_fees_GSTAmount { get; set; }
        public string FCSSecurity_filling_fees_Total { get; set; }

        public string FCSOther { get; set; }
        public string FCSOther_GST { get; set; }
        public string FCSOther_GSTAmount { get; set; }
        public string FCSOther_Total { get; set; }
        public string FCSAmt_Before_GST { get; set; }
        public string FCSGST { get; set; }
        public string FCSTotal { get; set; }
        public string FCAAir_Freight { get; set; }
        public string FCAAir_Freight_GST { get; set; }
        public string FCAAir_Freight_GSTAmount { get; set; }
        public string FCAAir_Freight_Total { get; set; }
        public string FCAMCC { get; set; }
        public string FCAMCC_GST { get; set; }
        public string FCAMCC_GSTAmount { get; set; }
        public string FCAMCC_Total { get; set; }
        public string FCAX_Ray { get; set; }
        public string FCAX_Ray_GST { get; set; }
        public string FCAX_Ray_GSTAmount { get; set; }
        public string FCAX_Ray_Total { get; set; }
        public string FCAMYC_Fuel { get; set; }
        public string FCAMYC_Fuel_GST { get; set; }
        public string FCAMYC_Fuel_GSTAmount { get; set; }
        public string FCAMYC_Fuel_Total { get; set; }
        public string FCAAMS { get; set; }
        public string FCAAMS_GST { get; set; }
        public string FCAAMS_GSTAmount { get; set; }
        public string FCAAMS_Total { get; set; }
        public string FCAAWB { get; set; }
        public string FCAAWB_GST { get; set; }
        public string FCAAWB_GSTAmount { get; set; }
        public string FCAAWB_Total { get; set; }
        public string FCAPCA { get; set; }
        public string FCAPCA_GST { get; set; }
        public string FCAPCA_GSTAmount { get; set; }
        public string FCAPCA_Total { get; set; }
        public string FCAOthers { get; set; }
        public string FCAOthers_GST { get; set; }
        public string FCAOthers_GSTAmount { get; set; }
        public string FCAOthers_Total { get; set; }
        public string FCAAMT_before_GST { get; set; }
        public string FCAGST { get; set; }
        public string FCATotal { get; set; }
        public string CFS_Vendor { get; set; }
        public string CFS_Invoice_No { get; set; }
        public string CFS_InvDate { get; set; }
        public string CFS_Particulars { get; set; }
        public string CFS_Amt_Before_GST { get; set; }
        public string CFS_GST { get; set; }
        public string CFS_Total { get; set; }
        public string TC_Transporter { get; set; }
        public string TC_Invoice_No { get; set; }
        public string TC_InvDate { get; set; }
        public string TC_Charges { get; set; }
        public string TCChargesGST { get; set; }
        public string TCChargesGSTAmount { get; set; }
        public string TCCharges_Total { get; set; }
        public string TC_VGM { get; set; }
        public string TCVGMGST { get; set; }
        public string TCVGMGSTAmount { get; set; }
        public string TCVGM_Total { get; set; }
        public string TC_Other { get; set; }
        public string TCOtherGST { get; set; }
        public string TCOtherGSTAmount { get; set; }
        public string TCOther_Total { get; set; }
        public string TC_AMT_Before_GST { get; set; }
        public string TC_GST { get; set; }
        public string TC_Total { get; set; }
        public string AddTC_Transporter { get; set; }
        public string AddTC_Invoice_No { get; set; }
        public string AddTC_InvDate { get; set; }
        public string AddTCAdvPaymentOn { get; set; }
        public string AddTCPaymentDate { get; set; }
        public string AddTC_Charges { get; set; }
        public string AddTCChargesGST { get; set; }
        public string AddTCChargesGSTAmount { get; set; }
        public string AddTCCharges_Total { get; set; }
        public string AddTC_VGM { get; set; }
        public string AddTCVGMGST { get; set; }
        public string AddTCVGMGSTAmount { get; set; }
        public string AddTCVGM_Total { get; set; }
        public string AddTC_Other { get; set; }
        public string AddTCOtherGST { get; set; }
        public string AddTCOtherGSTAmount { get; set; }
        public string AddTCOther_Total { get; set; }
        public string AddTC_AMT_Before_GST { get; set; }
        public string AddTC_GST { get; set; }
        public string AddTC_Total { get; set; }
        public string COO_Vendor { get; set; }
        public string COO_Invoice_No { get; set; }
        public string COO_InvDate { get; set; }
        public string COO_Particulars { get; set; }
        public string COO_Amt_Before_GST { get; set; }
        public string COO_GST { get; set; }
        public string COOGSTPercentage { get; set; }
        public string COO_Total { get; set; }
        public string EIA_Vendor { get; set; }
        public string EIA_Invoice_No { get; set; }
        public string EIA_InvDate { get; set; }
        public string EIA_Particulars { get; set; }
        public string EIA_Amt_Before_GST { get; set; }
        public string EIA_GST { get; set; }
        public string EIAGSTPercentage { get; set; }
        public string EIA_Total { get; set; }

        public string TotalExcluded_GST { get; set; }
        public string Forwarder_Details { get; set; }
        public string OC_Vendor { get; set; }
        public string OC_Invoice_No { get; set; }
        public string OC_InvDate { get; set; }
        public string OC_Particulars { get; set; }
        public string OC_Amt_Before_GST { get; set; }
        public string OC_GST { get; set; }
        public string OC_Total { get; set; }
        public string Doc_ForwardedNo { get; set; }
        public string Doc_Sent_Through { get; set; }
        public string Doc_Date { get; set; }
        public string Doc_Submitted_Account_On { get; set; }
        public List<Exporttracker_OtherCharges> OtherCharges { get; set; }
        public List<ExportCFSChargeDetails> CFSOtherCharges { get; set; }
   
        public List<InvoiceLogDetail> ExporttrackerLogDetails { get; set; } = new List<InvoiceLogDetail>();

        public string UserName { get; set; }
        public string Password { get; set; }
        public Boolean finalsave { get; set; }
        public int runfor { get; set; }
        public int EmployeeRowId { get; set; }
        public string SearchType { get; set; }
        public string FinalTotal { get; set; }
        public string FinalAmtbeforegst { get; set; }
        public string Finalgstamount { get; set; }
        public string Weight { get; set; }
        public string Quntity_SQM { get; set; }
        public string Type { get; set; }
        public string No_of_FCL { get; set; }
        public string AvgCostPerSQM { get; set; }
        public string AvgCostPerFCL { get; set; }
        public string AdvPaymenton { get; set; }
        public string PaymentDate { get; set; }

    }
    public class AdvanceTracker
    {
        public string SrNo { get; set; }
        public string Division { get; set; }
        public string Invoice_No { get; set; }
   
        public string FCForwarder { get; set; }
        public string FCInvoiceNo { get; set; }
        public string Inco_Terms { get; set; }
        public string FCInvDate { get; set; }
        public string FCSTOTAL { get; set; }
        public string FCATOTAL { get; set; }
        public string AdvPaymenton { get; set; }
        public string PaymentDate { get; set; }
        public string SourceType { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class Shipmenttracker
    {
        public string SrNo { get; set; }
        public string Invoice_No { get; set; }
        public string InvDate { get; set; }
        public string No_of_FCL { get; set; }
        public string Bill_to_Name { get; set; }
        public string Ship_to_Name { get; set; }
        public string Mode { get; set; }
        public string Port_of_Loading { get; set; }
        public string Port_of_Discharge { get; set; }
        public string Vessel_Airlines { get; set; }
        public string ETD { get; set; }
        public string ETA { get; set; }
        public string Transit_Time { get; set; }
        public string BL_Express { get; set; }
        public string Doc_Submitted_Account_On { get; set; }
        public string Doc_Date { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EmployeeRowId { get; set; }
        public string SearchType { get; set; }
        public int runfor { get; set; }
        public bool SelectPreMarketing { get; set; }
    }

   
    public class InvoiceLogDetail
    {
        public string SectionName { get; set; }
        public DateTime? LogDate { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class Inco_TermsModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class ClearingPointModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class FCLModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class Exporttracker_OtherCharges
    {
        public int? OC_RowId { get; set; }
        public int? OC_ETMasterId { get; set; }
        public string OC_Vendor { get; set; }
        public string OC_Invoice_No { get; set; }
        public string OC_InvDate { get; set; }
        public string OC_Particulars { get; set; }
        public string OC_Amt_Before_GST { get; set; }
        public string OC_GSTPercentage { get; set; }
        public string OC_GSTAmount { get; set; }
        public string OC_Total { get; set; }
        public string OCAdvPaymenton { get; set; }
        public string OCPaymentDate { get; set; }
        public string OC_BLAdvPaymenton { get; set; }
        public string OC_BLPaymentDate { get; set; }
    }
    public class ExportCFSChargeDetails
    {
        public int? Cfs_RowId { get; set; }
        public int? Cfs_MasterId { get; set; }
        public string Nature_of_Service { get; set; }
        public string AmtBeforeGST { get; set; }
        public string GstPercentage { get; set; }
        public string GstAmount { get; set; }
        public string Total { get; set; }
    
    }

}