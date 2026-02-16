using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tracker_System.Models
{
    public class FreightTracker
    {
        public string SrNo { get; set; }
        public string Total { get; set; }
        public string Doc_No { get; set; }
        [DataType(DataType.Date)]
        public DateTime Dates { get; set; }
        public string Transporter { get; set; }
        public string TypeOfSales { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string SearchType { get; set; }
        public bool ShowAllHODApproval { get; set; }
        public bool ShowAllPostSubmission { get; set; }

        public bool ShowAllFT { get; set; }
        public string FyearId { get; set; }
        public string EYear { get; set; }
        public List<FreightTracker> lstFyear { get; set; }
        [DataType(DataType.Date)]
        public string InvoiceDate { get; set; }
        public string Doc_Date { get; set; }
        public string CustCode { get; set; }
        public string Cust_Name { get; set; }
        public string Cust_Type { get; set; }
        public string Cust_Category_Code { get; set; }
        public string Salesperson_Name { get; set; }
        public string Brand { get; set; }
        public string Promotional_Sample_Sale { get; set; }
        public string Freight_Basis { get; set; }
        public string Freight_Paid_By { get; set; }
        public string Qnty_SQM { get; set; }
        public string Invoice_Amt { get; set; }
        public string Net_Rlsn_Per_SQM { get; set; }      
        public string LRRRDate { get; set; }
        public string LR_RR_Date { get; set; }

        public string LR_RR_No { get; set; }
        public string Transporter_Name { get; set; }
        public string Freight_Amt { get; set; }
        public bool SelectAll { get; set; }
        public bool SelectCommercial { get; set; }
        public bool SelectHO { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:y/MM/dd}")]

        // public DateTime FreightBillRecdONDate { get; set; }
        public string FreightBillRecdONDate { get; set; }
        //[DataType(DataType.Date)]
        public string FOFreightBillRecdONDate { get; set; }
        public string HOFreightBillRecdONDate { get; set; }
        public string FreightBillRecdON { get; set; }
        public string FreightBillNo { get; set; }
        public string FOFreightBillNo { get; set; }

        public string HOFreightBillNo { get; set; }
        public string HOBill_Date { get; set; }
        public string Bill_Date { get; set; }
        public string FOBill_Date { get; set; }
        [DataType(DataType.Date)]
        public string BillDate { get; set; }
       // [DataType(DataType.Date)]
        public string FreightBillForwardedDate { get; set; }
      //  [DataType(DataType.Date)]
        public string FOFreightBillForwardedDate { get; set; }
        public string HOFreightBillForwardedDate { get; set; }
        public string FreightBillForwarded { get; set; }
        public string ShownSepONInvoice { get; set; }
        public bool ISShownSepONInvoiceNo { get; set; }
        public string ISShownSepONInvoice { get; set; }
        public bool ISShownSepONInvoiceYes { get; set; }
        public bool ISDebitNoteToBeRaisedYes { get; set; }
        public bool ISDebitNoteToBeRaisedNo { get; set; }
        public string DebitNoteToBeRaised { get; set; }
        public string DebitAdviseNo { get; set; }
        public string ApprovalForwardedON { get; set; }
        public string Remarks { get; set; }
        public string FORemarks { get; set; }
        public string HORemarks { get; set; }
        public string ApprovalForwardedONDate { get; set; }
        public string HOApprovalRemarks { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentChecqueRecdONDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentChecqueRecdONDatePostSub { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentAdviseSentONDatePostSub { get; set; }

        public string PaymentChecqueRecdON { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentAdviseSentONDate { get; set; }
        public string PaymentAdviseSentON { get; set; }

        public string CommRemarks { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentSentONDate { get; set; }
        public string PaymentSentON { get; set; }

        public string TotalWeight { get; set; }
        public string EstimatedFreightAmt { get; set; }
        public string NoOfBoxes { get; set; }
        public string ModeOfTransport { get; set; }
        public string RegionName { get; set; }
        public string FreightBorneBy { get; set; }
       // [DataType(DataType.Date)]
        public string ForwardedONDate { get; set; }        
        public string ForwardedON { get; set; }
       /// [DataType(DataType.Date)]
        public string SentForHODApprovalDate { get; set; }    
        public string SentForHODApprovalComDate { get; set; }

        public string SentForHODApproval { get; set; }

        public int RecordCount { get; set; }
        public string ActualFreightAmt { get; set; }
        public string FOActualFreightAmt { get; set; }
        public string HOActualFreightAmt { get; set; }
        public string ShipToCode { get; set; }
        public string Region { get; set; }
        public string checkApproval { get; set; }
        public string PendingName { get; set; }
        public string VPendingdate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
     
        public bool IsActive { get; set; }
        public bool CheckedHODApproval { get; set; }
    }
}