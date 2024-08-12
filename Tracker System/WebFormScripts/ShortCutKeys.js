
var ManageGoodsReceipt = 'm g r';
var ManagePurchaseOrder = 'm p o';
var ManagePurchaseReturn = 'm p r';
var ManageSalesDelivery = 'm s d';
var ManageSalesOrder = 'm s o';
var ManageSalesReturn = 'm s r';
var ManageStockTransfer = 'm s t';
var ManageVoucherPosting = 'm v p';


var BankPayment = 'b p';
var BankReceipt = 'b r';
var BankTransfer = 'b t';
var CashDeposit = 'c d';
var CashPayment = 'c p';
var CashReceipt = 'c r';
var CashTransfer = 'c t';
var CashWithdrawal = 'c w';

var BalanceShit = 'b s'; //Reprots
var DayBook = 'd b'; //Reprots

var CreditNote = 'c n';
var DebitNote = 'd n';
var CreditNote = 'c n';
var GoodsReceipt = 'g r';
var JournalVoucher = 'j v';
var Ledger = 'l d'; //Reprots
var OutstandPay = 'o p'; //Reprots
var OutstandRec = 'o r'; //Reprots
var PurchaseInvoice = 'p i';
var ProfitLoss = 'p l'; //Reprots
var PurchaseOrder = 'p o';
var PurchaseReturn = 'p r';
var ReportsAccount = 'r a';   //Reports Main Page
var ReportsInventory = 'r i'; //Reports Main Page
var ReportPurchase = 'r p';   //Reports Main Page
var ReportsSales = 'r s';     //Reports Main Page
var SalesDelivery = 's d';
var SalesInvoice = 's i';
var SalesOrder = 's o';
var SalesReturn = 's r';
var StockSummary = 's s'; //Reprots
var StockTransfer = 's t';
var TrialBalance = 't b'; //Reprots
var VoucherPosting = 'v p';


var DashboardHome = 'ctrl+shift+d';
var HelpReq = 'h e l p';

var hrefStr = window.location.protocol + "//" + window.location.host;

$.getScript('../Scripts/mousetrap.min.js', function () {

    Mousetrap.bind(ManageGoodsReceipt, function (e) {
        $(location).attr('href', ITX3ResolveUrl('GR/ListGR'));
    });

    Mousetrap.bind(ManagePurchaseOrder, function (e) {
        $(location).attr('href', ITX3ResolveUrl('PO/ListPO'));
    });

    Mousetrap.bind(ManagePurchaseReturn, function (e) {
        $(location).attr('href', ITX3ResolveUrl('PurchaseReturn/ListPR'));
    });

    Mousetrap.bind(ManageSalesDelivery, function (e) {
        $(location).attr('href', ITX3ResolveUrl('SD/ListSD'));
    });

    Mousetrap.bind(ManageSalesOrder, function (e) {
        $(location).attr('href', ITX3ResolveUrl('SO/ListSO'));
    });

    Mousetrap.bind(ManageSalesReturn, function (e) {
        $(location).attr('href', ITX3ResolveUrl('SalesReturn/ListSR'));
    });

    Mousetrap.bind(ManageStockTransfer, function (e) {
        $(location).attr('href', ITX3ResolveUrl('StockTransfer/ListStockTransfer'));
    });

    Mousetrap.bind(ManageVoucherPosting, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/ListAccountVoucher'));
    });


    //-----------------

    Mousetrap.bind(BankPayment, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateBankReceipt?strAction=C&strFromCreate=Y&VoucherType=BP'));
    });
    
    Mousetrap.bind(BankReceipt, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateBankReceipt?strAction=C&strFromCreate=Y&VoucherType=BR'));
    });

    Mousetrap.bind(BankTransfer, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateAccountVoucher?strAction=C&strFromCreate=Y&VoucherType=BT'));
    });

    Mousetrap.bind(CashDeposit, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateAccountVoucher?strAction=C&strFromCreate=Y&VoucherType=CD'));
    });

    Mousetrap.bind(CashPayment, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateBankReceipt?strAction=C&strFromCreate=Y&VoucherType=CP'));
    });

    Mousetrap.bind(CashReceipt, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateBankReceipt?strAction=C&strFromCreate=Y&VoucherType=CR'));
    });

    Mousetrap.bind(CashTransfer, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateAccountVoucher?strAction=C&strFromCreate=Y&VoucherType=CT'));
    });

    Mousetrap.bind(CashWithdrawal, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateAccountVoucher?strAction=C&strFromCreate=Y&VoucherType=CW'));
    });
    Mousetrap.bind(CreditNote, function (e) {
        $(location).attr('href', ITX3ResolveUrl ('AccountVoucher/CreateAccountVoucher?strAction=C&strFromCreate=Y&VoucherType=CN'));
    });

    Mousetrap.bind(CreditNote, function (e) {
        $(location).attr('href', hrefStr + '/AccountVoucher/CreateAccountVoucher?strAction=C&strFromCreate=Y&VoucherType=CN');
    });

    Mousetrap.bind(DebitNote, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateAccountVoucher?strAction=C&strFromCreate=Y&VoucherType=DN'));
    });

    Mousetrap.bind(JournalVoucher, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateAccountVoucher?strAction=C&strFromCreate=Y&VoucherType=JV'));
    });

    Mousetrap.bind(PurchaseInvoice, function (e) {
        $(location).attr('href', ITX3ResolveUrl('PIAGR/CreateDirectInvoice?strAction=C'));
    });

    Mousetrap.bind(SalesInvoice, function (e) {
        $(location).attr('href', ITX3ResolveUrl('SIASD/CreateDirectInvoice?strAction=C'));
    });
    
    //--------------------
    Mousetrap.bind(BalanceShit, function (e) {
        $(location).attr('href', ITX3ResolveUrl('rptBalanceSheet/rptBalanceSheet'));
    });

    Mousetrap.bind(DayBook, function (e) {
        $(location).attr('href', ITX3ResolveUrl('rptDayBook/rptDayBook'));
    });

    Mousetrap.bind(GoodsReceipt, function (e) {
        $(location).attr('href', ITX3ResolveUrl('GR/CreateGR_0?strAction=C'));
    });

    Mousetrap.bind(Ledger, function (e) {
        $(location).attr('href', ITX3ResolveUrl('rptLedgerReport/rptLedgerReport'));
    });

    Mousetrap.bind(OutstandPay, function (e) {
        $(location).attr('href', ITX3ResolveUrl('rptOutstandingPayable/rptOutstandingPayable'));
    });

    Mousetrap.bind(OutstandRec, function (e) {
        $(location).attr('href', ITX3ResolveUrl('rptOutstandingReceivable/rptOutstandingReceivable'));
    });

    Mousetrap.bind(ProfitLoss, function (e) {
        $(location).attr('href', ITX3ResolveUrl('rptProfitLoss/rptProfitLoss'));
    });

    Mousetrap.bind(PurchaseOrder, function (e) {
        $(location).attr('href', ITX3ResolveUrl('PO/CreatePO?strAction=C'));
    });

    Mousetrap.bind(PurchaseReturn, function (e) {
        $(location).attr('href', ITX3ResolveUrl('PurchaseReturn/CreatePR?strAction=C'));
    });

    Mousetrap.bind(ReportsAccount, function (e) {
        $(location).attr('href', ITX3ResolveUrl('ReportList/ReportList?Type=AC'));
    });

    Mousetrap.bind(ReportsInventory, function (e) {
        $(location).attr('href', ITX3ResolveUrl('ReportList/ReportList?Type=IV'));
    });

    Mousetrap.bind(ReportPurchase, function (e) {
        $(location).attr('href', ITX3ResolveUrl('ReportList/ReportList?Type=PL'));
    });

    Mousetrap.bind(ReportsSales, function (e) {
        $(location).attr('href', ITX3ResolveUrl('ReportList/ReportList?Type=SL'));
    });

    Mousetrap.bind(SalesDelivery, function (e) {
        $(location).attr('href', ITX3ResolveUrl('SD/CreateSD_0?strAction=C'));
    });

    Mousetrap.bind(SalesOrder, function (e) {
        $(location).attr('href', ITX3ResolveUrl('SO/CreateSO?strAction=C'));
    });

    Mousetrap.bind(SalesReturn, function (e) {
        $(location).attr('href', ITX3ResolveUrl('SalesReturn/CreateSR?strAction=C'));
    });

    Mousetrap.bind(StockSummary, function (e) {
        $(location).attr('href', ITX3ResolveUrl('rptStockSummary/rptStockSummary'));
    });

    Mousetrap.bind(StockTransfer, function (e) {
        $(location).attr('href', ITX3ResolveUrl('StockTransfer/CreateStockTransfer?strAction=C'));
    });

    Mousetrap.bind(TrialBalance, function (e) {
        $(location).attr('href', ITX3ResolveUrl('rptTrialBalance/rptTrialBalance'));
    });

    Mousetrap.bind(VoucherPosting, function (e) {
        $(location).attr('href', ITX3ResolveUrl('AccountVoucher/CreateAccountVoucher?strAction=C'));
    });

    Mousetrap.bind('ctrl+shift+d', function (e) {
        //  $(location).attr('href', hrefStr);
        // alert('hi');
    });

    Mousetrap.bind(HelpReq, function (e) {
        // $(location).attr('href', hrefStr+'/Help/Index');
        window.open(ITX3ResolveUrl('Help/Index'));
    });


});