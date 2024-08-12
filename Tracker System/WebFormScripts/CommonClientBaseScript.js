var EnterpriseCode = "";
var IsAdmin = "";
var CW_PayReqModuleType = "SINGLE";

$(document).ready(function () {
    EnterpriseCode = "";
    IsAdmin = "";
    EnterpriseCode = ($('body').data("enterprisecode")).toUpperCase();
    if (($('body').data("isadmin")) != "" && ($('body').data("isadmin")) != null && ($('body').data("isadmin")) != undefined)
        IsAdmin = ($('body').data("isadmin")).toString().toUpperCase();    
    ClientWiseMenu();

    if (EnterpriseCode == "AKTRUST" || EnterpriseCode == "AKTRUST_TEST" || EnterpriseCode == "TEST") { CW_PayReqModuleType = 'MULTI'; }
});

function DisableBatchSerialBatchNo(ObjectCode) {
    if (EnterpriseCode == "CLSL") {
        if ($('section[class=content]').attr('data-configpageid') == "GRPRDORD") {
            $('#tblBatchSerial tbody tr').each(function () {
                $(this).find('td:eq(0) input[type=text]').attr('disabled', 'disabled');
            });
        }
    }
    if (EnterpriseCode == "DISHA" || EnterpriseCode == "DISHA_TEST") {
        if ($('section[class=content]').attr('data-configpageid') == "SR" || $('section[class=content]').attr('data-configpageid') == "PI" || $('section[class=content]').attr('data-configpageid') == "GR" || $('section[class=content]').attr('data-configpageid') == "GRPRDORD" || $('section[class=content]').attr('data-configpageid') == "INVGR" || $('section[class=content]').attr('data-configpageid') == "OS" || ($('section[class=content]').attr('data-configpageid') == "JWR" && ObjectCode != "RTPRDORD")) {
            $('#tblBatchSerial tbody tr').each(function () {
                $(this).find('td:eq(0) input[type=text]').attr('disabled', 'disabled');
            });
        }
    }
}
function DisableSchemeGroupCopyMenu() {
    if (EnterpriseCode == "MOBITECH" || EnterpriseCode == "TEST") {
        $('#lnkhideCreateForBranch').removeClass("DN");
    }
}
function EnablePOSRetailOutletCustomRoundOff() {
    if (EnterpriseCode == "FOURSQUARES") {
        $('#divPOSRetailOutletCustomRoundoffLabel,#divPOSRetailOutletCustomRoundoffLabel1').removeClass("DN");
    }
}
function DisablePONewButton() {
    if (EnterpriseCode == "AMUL") {
        $('#liPONew,#divPONew').addClass("DN");
    }
}
function POSSalesDetailRptDisplayFilter() {
    if (EnterpriseCode == "MOBITECH" || EnterpriseCode == "TEST") {
        $('#POSSalesDetailRptFilter').removeClass("DN");
    }
}
function DisableAutoBatchNoPopupBatchFrom() {
    if (EnterpriseCode == "DISHA" || EnterpriseCode == "DISHA_TEST") {
        if ($('section[class=content]').attr('data-configpageid') == "SR" || $('section[class=content]').attr('data-configpageid') == "PI" || $('section[class=content]').attr('data-configpageid') == "GR" || $('section[class=content]').attr('data-configpageid') == "GRPRDORD" || $('section[class=content]').attr('data-configpageid') == "INVGR" || $('section[class=content]').attr('data-configpageid') == "OS" || $('section[class=content]').attr('data-configpageid') == "JWR") {
            $('input[id=B_BatchNoStartFrom_1]').attr('disabled', 'disabled');
        }
    }
}

function PlayBeepSound() {
    if (EnterpriseCode == "DISHA" || EnterpriseCode == "DISHA_TEST") {
        beep("square", 100, 520, 200);
    }
}

function PlayErrorBeepSound() {
    if (EnterpriseCode == "DISHA" || EnterpriseCode == "DISHA_TEST") {
        beep("square", 100, 100, 300);
    }
}

function beep(type, vol, freq, duration) {
    objAudioContext = new AudioContext() // browsers limit the number of concurrent audio contexts, so you better re-use'em
    v = objAudioContext.createOscillator()
    u = objAudioContext.createGain()
    v.connect(u)
    v.frequency.value = freq
    v.type = type // Types: sine, square, sawtooth, triangle
    u.connect(objAudioContext.destination)
    u.gain.value = vol * 0.01
    v.start(objAudioContext.currentTime)
    v.stop(objAudioContext.currentTime + duration * 0.001)
}

function _CheckItemDetailsTaxExist(ItemControlType) {
    if ((EnterpriseCode == "DISHA" || EnterpriseCode == "DISHA_TEST") && ($("#ItemTAX1" + ItemControlType).val() == undefined || $("#ItemTAX1" + ItemControlType).val() == "" || $("#ItemTAX1" + ItemControlType).val() == null) && ($("#ItemTAX2" + ItemControlType).val() == undefined || $("#ItemTAX2" + ItemControlType).val() == "" || $("#ItemTAX2" + ItemControlType).val() == null) && ($("#ItemTAX3" + ItemControlType).val() == undefined || $("#ItemTAX3" + ItemControlType).val() == "" || $("#ItemTAX3" + ItemControlType).val() == null) && ($("#ItemTAX4" + ItemControlType).val() == undefined || $("#ItemTAX4" + ItemControlType).val() == "" || $("#ItemTAX4" + ItemControlType).val() == null) && ($("#ItemTAX5" + ItemControlType).val() == undefined || $("#ItemTAX5" + ItemControlType).val() == "" || $("#ItemTAX5" + ItemControlType).val() == null) && ($("#ItemTAX6" + ItemControlType).val() == undefined || $("#ItemTAX6" + ItemControlType).val() == "" || $("#ItemTAX6" + ItemControlType).val() == null)) {
        var LogMsg = " ---NO TAX ITEM---";
        LogMsg += "\n ItemCode: " + $('#ItemCode' + ItemControlType).val() + " |ItemName: " + $('#ItemName' + ItemControlType).val() + " |DocDate: " + $("#DocDate").val() + " |VoucherType: " + $("#DocTypeId :selected").text();
        LogMsg += "\n  - - - - - - - - - - - ";

        _SetLogOnWebFile(LogMsg);
        AlertDialog('#ItemCode' + ItemControlType, "Tax detail not available for item :" + $('#ItemCode' + ItemControlType).val() + "-" + $('#ItemName' + ItemControlType).val());
        $('#ItemName' + ItemControlType).val('');
        $('#ItemId' + ItemControlType).val('');
        return false;
    }
    return true;
}

function _SetLogOnWebFile(LogMsg) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/SetLogOnWeb'),
        data: JSON.stringify({ LogMsg: LogMsg }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
    });
}

function LinkShowHideOfPaymentRequest() {
    if (CW_PayReqModuleType == "MULTI") {
        $("#DivlnkEdit").hide();
        $("#DivlnkEdit1").show();
        $("#lnkCreateNew, #lnkEdit, #lnkCreateNew2, #DivlnkEdit").hide();
        $("#lnkCreateNew1, #lnkEdit1, #lnkCreateNew3, #DivlnkEdit1").show();
    }
    else {
        $("#DivlnkEdit").show();
        $("#DivlnkEdit1").hide();
    }
      
}

function LinkShowHideOfBankSelection() {
    if (EnterpriseCode == "AEROLAM" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "ZIPERP" || EnterpriseCode == "TEST" || EnterpriseCode == "AMUL") {
        $("#lnkExportToSpecificFormat,#lnkExportToSpecificFormat1").removeClass("DN");
    }
}

function LinkShowHideOfRetailerContact() {
    if (EnterpriseCode == "RUDI" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "ZIPERP" || EnterpriseCode == "TEST") {
        $("#lnkRetailerContact").show();
        $("#IsRetailerContact").val(true);
    }
    else {
        $("#lnkRetailerContact").hide();
        $("#IsRetailerContact").val(false);
    }
}

function DeliveryChargeWaiver() {
    if (EnterpriseCode == "RUDI" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "ZIPERP") {
        $("#divDeliveryChargeWaiver").show();
    }
    else {
        $("#divDeliveryChargeWaiver").hide();
    }
}

function ClientWiseMenu() {
    $('#liLogisticMenu').hide();
    //$('.RUDILOGO').hide();
    //$('.UNNATILOGO').hide();
    $('#LogisticMB').hide();
    $("#lnkVocuherAdditionalDetail").hide();
    $('#divLogisticsReport').hide();
    $("#lnkCustomProductionEntry, #lnkCustomProductionEntryDutron").hide();
    $("#DivlnkItemAttributeMatrix,#DivlnkItemAttributeMatrixJWR").addClass("DN");
    $("#lnkQuickAddItemDetail").addClass("DN");
    $("#lnkQuickAddItemDetailJWI").addClass("DN");
    $('#liSalesStockReservation').addClass("DN");
    $("#liSalesOrderExportToTxt").addClass("DN");
    $("#divImportItemPRDPO").addClass("DN");
    $("#divImportItemSD").addClass("DN");
    $("#divSDFindPendingOrder").addClass("DN");
    $("#thSDItemCode").hide();
    $("#thSDSOItemCode").hide();
    $(".tdSDItemCode").hide();
    $(".tdSDSOItemCode").hide();
    $('#dvSchemeMenu').hide();
    $("#liRudiWiseTargetEntry").hide();
    $("#liPlantWiseTargetEntry").hide();
    $("#DivClubRewardPointFlag").hide();
    $("#divLoyaltyProgramReq").hide();
    $("#divPRAgstSOForAutoPOSO").hide();
    $("#liItemSerialNumberMappingTypeMaster").hide();
    $("#hrItemSerialNumberMappingTypeMaster").hide();
    $("#liCustItemSerialNoMapping").hide();
    $("#liCustRMItemSerialNoReplace,#PRAgstCRMLead").hide();
    $(".DivlnkItemBifurcation").addClass("DN");
    if (EnterpriseCode != "DILIPCO") {
        $("#thSDItemCode").show();
        $("#thSDSOItemCode").show();
        $(".tdSDItemCode").show();
        $(".tdSDSOItemCode").show();
    }

    if (EnterpriseCode != "DILIPCO" || IsAdmin == "TRUE") {
        $("#divSDFindPendingOrder").removeClass("DN");
    }

    if (EnterpriseCode == "ZIPERP" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "TEST" || EnterpriseCode == "DEMO") {
        //$('.RUDILOGO').removeClass("DN");
        $("#DivlnkItemAttributeMatrix,#DivlnkItemAttributeMatrixJWR").removeClass("DN");
        $("#lnkQuickAddItemDetail").removeClass("DN");
        $("#lnkCustomProductionEntryDutron").show();
        $("#liSalesStockReservation").show();
        $("#liSalesOrderExportToTxt").removeClass("DN");
        $("#divImportItemPRDPO").removeClass("DN");
        $("#divImportItemSD").removeClass("DN");
        $("#liSalesStockReservation").show();
        $("#liClosingStockAfterStockReservation").show();
        $("#lnkQuickAddItemDetailJWI").removeClass("DN");
        $("#liRudiDashboard").show();
        $(".IssueSlipRqstMD").show();
        $("#lnkBOQForm").removeClass("DN");
        $("#DashBAWiseSalesCollectionOutstanding").show();
        $(".lnkBINMAPPING").removeClass('DN');
        $("#divLoyaltyProgramReq").show();
        $("#divPRAgstSOForAutoPOSO").show();
        $("#liItemSerialNumberMappingTypeMaster").show();
        $("#hrItemSerialNumberMappingTypeMaster").show();
        $("#liCustItemSerialNoMapping").show();
        $("#liCustRMItemSerialNoReplace").show();
        $("#VoucherAddField").show();
        $(".liProjectMaster").removeClass('DN');
        $("#liPendingReasonGroup,#hrPendingReasonGroup,#liModeofAcknowledgement,#hrModeofAcknowledgement,#liTechnicianMaster,#hrTechnicianMaster,#lnkAdditionalDetails").show();
        $("#liCSPMobileApplicationAlerts,#hrCSPMobileApplicationAlerts,#CSPAccountVoucher,#CSPAccountVoucherIndex,#CSPrptPendingSO,#liConvertBulkSI,#liServiceForm,#TransRequestForm,#liUpdateSOAdditionalDetails,#PRAgstCRMLead").show();
    }
    else if (EnterpriseCode == "VVAAN") {
        $("#DashBAWiseSalesCollectionOutstanding").show();
    }
    else if (EnterpriseCode == "BROWN" || EnterpriseCode == "AIMS") {
        $(".lnkBINMAPPING").removeClass('DN');
    }
    else if (EnterpriseCode == "SAR") {
        $("#divLoyaltyProgramReq").show();
        $("#divPRAgstSOForAutoPOSO").show();
    }
    else if (EnterpriseCode == "FUSION") {
        $("#divPRAgstSOForAutoPOSO").show();
    }
    else if (EnterpriseCode == "APL" || EnterpriseCode == "MNBKGI" || EnterpriseCode == "AMCLEAN") {
        $("#liSalesStockReservation").show();
        $("#liClosingStockAfterStockReservation").show();
    }
    else if (EnterpriseCode == "AEROLAM" || EnterpriseCode == "ANDAVAR") {
        $("#liSalesStockReservation").show();
        $("#liClosingStockAfterStockReservation").show();
    }
    else if (EnterpriseCode == "GAHIRAGRO" || EnterpriseCode == "TRITON") {
        $(".IssueSlipRqstMD").show();
        $("#liSalesStockReservation").show();
        $("#liClosingStockAfterStockReservation").show();
    }
    else if (EnterpriseCode == "MACO") {
        $("#DivlnkItemAttributeMatrix").removeClass("DN");
        $("#liSalesStockReservation").show();
        $("#liClosingStockAfterStockReservation").show();
        $(".lnkBINMAPPING").removeClass('DN');
        $(".IssueSlipRqstMD").show();
    }
    else if (EnterpriseCode == "GENUS") {
        if (window.location.pathname.includes('PO/CreatePO') == true || window.location.pathname.includes('PO/POAmendment') == true || window.location.pathname.includes('PurRequisition/CreatePurRequisition') == true) {
            $("#SendForApproval").attr("disabled", true);
        }
    }
    else if (EnterpriseCode == "AHPLCONNECT_0001" || EnterpriseCode == "AHPLCONNECT_TEST") {
        $('#liLogisticMenu').show();
        $('#LogisticMB').show();
        $('#divLogisticsReport').show();
        $('#divInvoiceDateWiseRadioButton').hide();
    }
    else if (EnterpriseCode == "GENTEX") {
        $("#lnkCustomProductionEntry").show();
    }
    else if (EnterpriseCode == "DISHA" || EnterpriseCode == "DISHA_TEST") {
        $("#DivlnkItemAttributeMatrix,#DivlnkItemAttributeMatrixJWR").removeClass("DN");
        $("#lnkQuickAddItemDetail").removeClass("DN");
    }
    else if (EnterpriseCode == "HARSHENTERPRISE") {
        $("#lnkQuickAddItemDetail").removeClass("DN");
        $("#liSalesStockReservation").show();
        $("#liClosingStockAfterStockReservation").show();
        $("#drpAll,.btnhide").hide();
        $("#drpNotification,#liCSPMobileApplicationAlerts,#hrCSPMobileApplicationAlerts,#CSPAccountVoucher,#CSPAccountVoucherIndex,#CSPrptPendingSO").show();
        
        
    }
    else if (EnterpriseCode == "PEARL") {
        $("#DivlnkItemAttributeMatrix").removeClass("DN");
    }
    else if (EnterpriseCode == "DUTRON") {
        $("#lnkCustomProductionEntryDutron").show();
        $("#liSalesOrderExportToTxt").removeClass("DN");
        $("#divImportItemPRDPO").removeClass("DN");
        $("#divImportItemSD").removeClass("DN");
    }
    else if (EnterpriseCode == "BAGHEL") {
        $(".lnkBINMAPPING").removeClass('DN');
    }
    else if (EnterpriseCode == "ICONICS") {
        $(".lnkBINMAPPING").removeClass('DN');
    }
    else if (EnterpriseCode == "VASACOSMETICS") {
        $(".lnkBINMAPPING").removeClass('DN');
    }
    else if (EnterpriseCode == "HKELECTRONICS") {
        $(".lnkBINMAPPING").removeClass('DN');
    }
    else if (EnterpriseCode == "PITZOINDIA" || EnterpriseCode == "PRATIBHA") {
        if (EnterpriseCode == "PITZOINDIA") {
            $("#liSalesStockReservation").show();
            $("#liClosingStockAfterStockReservation").show();
            $(".lnkBINMAPPING").removeClass('DN');
        }
    }
    else if (EnterpriseCode == "AFFIZIENT") {
        $(".lnkBINMAPPING").removeClass('DN');
    }
    else if (EnterpriseCode == "ASKAEQUIPMENTS") {
        $(".lnkBINMAPPING").removeClass('DN');
        $("#liPendingReasonGroup,#hrPendingReasonGroup,#liModeofAcknowledgement,#hrModeofAcknowledgement,#liTechnicianMaster,#hrTechnicianMaster,#lnkAdditionalDetails,#liServiceForm,#liUpdateSOAdditionalDetails").show();
    }
    else if (EnterpriseCode == "AKTRUST" || EnterpriseCode == "AKTRUST_TEST") {
        $("#lnkVocuherAdditionalDetail").show();
        var AKT_Url = "http://aktrustmodule.ziperp.net?AccessToken=" + ($('body').data("accesstoken")) + "&EnterpriseCode=" + EnterpriseCode;
        $("#lnkAKTHeaderMenu").show().html("<a href='" + AKT_Url + "' target=_blank class='zipnav-link'>MODULE</a>");
        $("#MMODULE").show();
    }
    else if (EnterpriseCode == "SAKARIYA") {
        $("#DivlnkItemAttributeMatrix").removeClass("DN");
        $("#lnkQuickAddItemDetailJWI").removeClass("DN");
    }
    else if (EnterpriseCode == "RAGHVENDRA") {
        $("#DivlnkItemAttributeMatrix").removeClass("DN");
    }
    else if (EnterpriseCode == "SHREERENGA") {
        $("#divWeightscale").show();
        $("#divAgstProdOrderItemInfoWeightscale").show();
    }
    else if (EnterpriseCode == "RKTEX") {
        $("#DivlnkItemAttributeMatrix").removeClass("DN");
        $("#lnkQuickAddItemDetail").removeClass("DN");
    }
    else if (EnterpriseCode == "RAHEJA") {
        $("#DivlnkItemAttributeMatrix").removeClass("DN");
    }
    else if (EnterpriseCode == "KARNAVATI") {
        $("#liSalesStockReservation").show();
        $("#liClosingStockAfterStockReservation").show();
        $(".lnkBINMAPPING").removeClass('DN');
    }
    else if (EnterpriseCode == "HARSHECOMMERCE") {
        $("#lnkQuickAddItemDetail").removeClass("DN");
    }
    else if (EnterpriseCode == "WHITEPOLYCHEM") {
        $("#lnkQuickAddItemDetail").removeClass("DN");
    }
    else if (EnterpriseCode == "PPSL") {
        $("#liSalesStockReservation").show();
        $("#liClosingStockAfterStockReservation").show();
    }
    else if (EnterpriseCode == "RUDI") {
        $("#liRudiDashboard").show();
        $("#liRudiWiseTargetEntry").show();
        $("#liPlantWiseTargetEntry").show();
        $(".hrRudiTargetEntry").css("display", "block");
        $('.RUDILOGO').removeClass("DN");
        $("#liDeliveryDetails").show();
    }
    else if (EnterpriseCode == "UNNATI") {
        $('.RUDILOGO').removeClass("DN");
        $('.UNNATILOGO').removeClass("DN");
        $("#liRudiDashboard").show();
        $("#liRudiWiseTargetEntry").show();
        $("#liPlantWiseTargetEntry").show();
        $(".hrRudiTargetEntry").css("display", "block");
        $("#lnkQuickAddItemDetail").removeClass("DN");
        $("#liDeliveryDetails").show();
    }
    else if (EnterpriseCode.toLowerCase() == "gtd") {
        $('#dvSchemeMenu').show();
    }
    else if (EnterpriseCode == "AMBITIOUS") {
        $("#lnkBOQForm").removeClass("DN");
    }
    else if (EnterpriseCode == "ARTCLUB") {
        $("#DivClubRewardPointFlag").show();
    }
    else if (EnterpriseCode == "MULTIFAB") {
        $("#lnkQuickAddItemDetail").removeClass("DN");
    }
    else if (EnterpriseCode == "AMRUTAM") {
        $("#lnkQuickAddItemDetail").removeClass("DN");
    }
    else if (EnterpriseCode == "AKSHAR") {
        $(".lnkBINMAPPING").removeClass('DN');
        $("#liSalesStockReservation").show();
    }
    else if (EnterpriseCode == "PLUS") {
        $(".lnkBINMAPPING").removeClass('DN');
    }
    else if (EnterpriseCode == "MOBITECHINDUSTRIES") {
        $("#liSalesStockReservation").show();
    }
    else if (EnterpriseCode == "PROMPT") {
        $("#liItemSerialNumberMappingTypeMaster").show();
        $("#hrItemSerialNumberMappingTypeMaster").show();
        $("#liCustItemSerialNoMapping").show();
        $("#liCustRMItemSerialNoReplace").show();
    }
    else if (EnterpriseCode == "GALLOPS") {                                                                                                           
        $("#VoucherAddField").show();
    }
    else if (EnterpriseCode == "RAKSHA") {
        $(".liProjectMaster").removeClass('DN');
        $(".liProjectMaster").show();
    }
    else if (EnterpriseCode == "JEEVAN") {
        $("#liDeliveryDetails").show();
    }
    else if (EnterpriseCode == "GRAMEENEXT") {
        $("#liConvertBulkSI").show();
    }
    else if (EnterpriseCode == "GEOCLEANER") {
        $("#TransRequestForm").show();
    }
    else if (EnterpriseCode == "ANDSLITE") {
        $("#CSPAccountVoucher,#CSPrptPendingSO").show();
        $("#CSPAccountVoucherIndex").hide();
    }
    else if (EnterpriseCode == "SFPL") {
        $("#liSFPLDashboard").show();
    }
    else if (EnterpriseCode == "BLUEPEARL") {
        $("#PRAgstCRMLead").show();
    }
    else if (EnterpriseCode == "USGPL") {
        $(".DivlnkItemBifurcation").removeClass("DN");
    }
}
function PendingSalesOrderHideShowExcelDumpButton() {
    if (EnterpriseCode == "AEROLAM") {
        $("#lnkExeclDump").hide();
    }
    else {
        $("#lnkExeclDump").show();
    }
}

function CustomPurchaseOrderValidation() {
    if (EnterpriseCode == "ZIPERP" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "SAL") {
        if (CheckMandatoryField($("#TransportPaymentMode"), "dropdown", "Please Select Transport Mode") == false)
            return false;
    }
    return true;
}

function CustomSIASDValidation() {
    if (EnterpriseCode == "ZIPERP" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "SFPL") {
        for (var i = 0; i < ItemList.length; i++) {
            if (ItemList[i].Rate == "" || ItemList[i].Rate == null || parseFloat(ItemList[i].Rate) == 0) {
                AlertDialog("", "Please enter rate for item : " + ItemList[i].ItemName);
                return false;
            }
        }
    }
    return true;
}


function CustomSalesOrderValidation(YearKey) {
    if (EnterpriseCode == "ZIPERP" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "PITZOINDIA") {
        if (CheckMandatoryField($("#CPONo"), "textbox", "Please Enter the PO No") == false)
            return false;
        if (CheckMandatoryField($("#CPODate"), "textbox", "Please Enter the PO Date") == false)
            return false;
    }
    if (EnterpriseCode == "ZIPERP" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "SAL") {
        if (CheckMandatoryField($("#TransportPaymentMode"), "dropdown", "Please Select Transport Mode") == false)
            return false;
    }
    if (EnterpriseCode == "ZIPERP" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "AKSHAR"|| EnterpriseCode == "TEST") {
        if ($("#CPONo").val() != "" && $("#CPONo").val() != null) {
            if ($('#strAction').val() == 'E') {
                if (CheckExistance($("#CPONo").val(), "CPONo", "OrderMaster", " AND OrderTypeCode='SO' AND YearKey =" + YearKey + " AND Id <> '" + $("#Id").val() + "' AND Vendor_Customer_Id='" + $("#Vendor_Customer_Id").val() + "'", "Po.No. " + $("#CPONo").val() + " already exist For " + $("#Vendor_Customer_Name").val() + ".", false, false)) {
                    return false;
                }
            }
            else {
                if (CheckExistance($("#CPONo").val(), "CPONo", "OrderMaster", " AND OrderTypeCode='SO' AND YearKey =" + YearKey + "  AND Vendor_Customer_Id='" + $("#Vendor_Customer_Id").val() + "'", "Po.No. " + $("#CPONo").val() + " already exist For " + $("#Vendor_Customer_Name").val() + ".", false, false)) {
                    return false;
                }
            }
        }
    }

    return true;
}

function CustomCSPUserValidation() {
    if (EnterpriseCode != "SAR") {
        if ($('#strAction').val() == 'C') {
            if (CheckExistance($("#CSPUserId").val(), "CSPUserId", "AccAccountMaster", "", "message")) {
                return false;
            }
            if (CheckExistance($("#CSPUserId").val(), "UserId", "CSPUsers", "", "message")) {
                return false;
            }
        }
        else {
            if (CheckExistance($("#CSPUserId").val(), "CSPUserId", "AccAccountMaster", " and Id !='" + $("#Id").val() + "'", "message")) {
                return false;
            }
            if (CheckExistance($("#CSPUserId").val(), "UserId", "CSPUsers", "", "message")) {
                return false;
            }
        }
    }
    return true;
}

function CustomSOItemCodeChangeValidation(_ItemCode) {
    if ($(_ItemCode).val() != "" && $(_ItemCode).val() != null) {
        if (EnterpriseCode == "ZIPERP" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "TEST" || EnterpriseCode == "DISHA" || EnterpriseCode == "DISHA_TEST") {
            if (($(_ItemCode).val()).split('.').length > 1) {
                var _removeCharacterLength = (-1)*((($(_ItemCode).val()).split('.')[($(_ItemCode).val()).split('.').length - 1]).length+1);
                var _tmpItemCode = ($(_ItemCode).val()).slice(0, _removeCharacterLength);
                $($(_ItemCode).val(_tmpItemCode));
            }
        }
    }
    return true;
}

function CustomGoodReceiptValidation(strAction, GRId) {
    if ( EnterpriseCode == "SAL") {
        if ($("#ItemTotalWeight").val() != "" && $("#ItemTotalWeight").val() != null && parseFloat($("#ItemTotalWeight").val()) > 0) {
            if (parseFloat($('#ItemTotalQty').val()) == 0 && ItemList.length == 0 && ServiceList.length > 0) {
                if (parseFloat($("#ItemTotalWeight").val()) != parseFloat($('#ServiceTotalQty').val())) {
                    AlertDialog("#_Weight1", "Service qty and total lr weight qty should be same.");
                    return false;
                }
            }
            else if (parseFloat($("#ItemTotalWeight").val()) != parseFloat($('#ItemTotalQty').val())) {
                AlertDialog("#_Weight1", "Item qty and total lr weight qty should be same.");
                return false;
            }
        }
    }
    else if (EnterpriseCode == "ZIPERP1" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "TEST1" || EnterpriseCode == "VIOLIN") {
        if ($("#ChallanNo").val() != "") {
            if (strAction == "C") {
                if (CheckExistance($("#ChallanNo").val(), "ChallanNo", "LogInventoryOtherDetails", "", "message", false, false)) {
                    return false;
                }
            }
            else if (strAction == "E") {
                if (CheckExistance($("#ChallanNo").val(), "ChallanNo", "LogInventoryOtherDetails", "and MastId !='" + GRId + "'", "message", false, false))
                    return false;
            }
        }
    }
    else if (EnterpriseCode == "ZIPERP" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "TEST" || EnterpriseCode == "AKSHAR") {
        if ($('#Vendor_Customer_Id').val() != "" && $('#BillNo').val() != "") {
            var exists = 0;
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('GR/CHECKEXISTBILLNO'),
                data: JSON.stringify({ VendorId: $('#Vendor_Customer_Id').val(), BillNo: $('#BillNo').val(), Id: $('#Id').val() }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    exists = data;
                },
                error: function (xhr, ret, e) {
                    return true;
                }
            });

            if (exists == 1) {
                AlertDialog("", "Bill No. already exists");
                return false;
            }
        }
    }
    return true;
}
function CustomSalesReturnValidation() {
    if (EnterpriseCode == "SATVAM") {
        if (CheckMandatoryField($("#SRReasonId"), "dropdown", "Please Select Sales Reason") == false)
            return false;
    }
    return true;
}

function CustomValidationForIndent() {
    if (EnterpriseCode == "AKSHAR" || EnterpriseCode == "PRATIBHA") {
        if (($("#SOId").val() != null && $("#SOId").val() != '' && $("#SOId").val() != "") && ItemList.length >= 0) {
            if (ValidateItemSalesOrderQty(ItemList, $("#SOId").val(), $("#Id").val()) == false)
                return false;
        }
    }
    return true;
}

function CheckValidationForDefInvAccount() {
    if (EnterpriseCode == "GENTEX") {
        if ($("#DefaultInvAccountId").val() == "" || $("#DefaultInvAccountId").val() == null || $("#DefaultInvAccountId").val() == undefined) {
            $("#DocTypeId").val('');
            alert('Def. Inventory account not loaded in item details. cancel this transaction.');
            return false;
        }
    }
}

function CheckSameItemCodeInItemList(_SrN) {
    if (EnterpriseCode == "NEELKANTH") {
        var _ItemId = $("#ItemId_" + _SrN).val();
        var _ItemsId = ItemList.filter(function (obj) { return obj.ItemId == _ItemId });
        if (_ItemsId != "" && _ItemsId != null && _ItemsId != [] && _ItemsId.length > 0) {
            AlertDialog("#ItemCode_" + _SrN, "Item code " + $("#ItemCode_" + _SrN).val() + " already added in list.");
            return false;
        }
    }
    return true;
}

function ChangeUDFLableofLead()
{
    if (EnterpriseCode == "TROTHINSURANCE" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "ZIPERP") {
        $("#lblUDF1").text("Referred By");
        $("#lblUDF2").text("Cluster Manager");
        $("#lblUDF3").text("PRN");
        $("#lblUDF4").text("Prospective PoSP");
        $("#lblUDF5").text("Lead Stage");
    }
}

function ChangeSendForApproval()
{
    if (EnterpriseCode == "ASKAEQUIPMENTS" || EnterpriseCode == "ZIPERP_0001" || EnterpriseCode == "ZIPERP")
    {
        $("#divSendForApproval").closest('div').find(':checkbox').prop('checked', false);
    }
}

function CustomSalesOrderTypeValidation() {
    if (EnterpriseCode == "DISHA" || EnterpriseCode == "TEST" || EnterpriseCode == "ZipERP") {
        $('#divReverificationofPriceList').removeClass("DN");
    }
}

function LoadBatchDetailInSDScan() {
    if (EnterpriseCode == "DISHA" || EnterpriseCode == "DISHA_TEST") {
        $("#IsLoadBatchDetail").val(true);
    }
    else { $("#IsLoadBatchDetail").val(false); }
}

function CSPAccountVoucher() {
    if (EnterpriseCode == "HARSHENTERPRISE" || EnterpriseCode == "ZIPERP" || EnterpriseCode == "ANDSLITE") {
        return true;
    }
    else {
        return false;
    }
}

function CustomerPrompt() {
    if (EnterpriseCode == "PROMPT" || EnterpriseCode == "TEST"|| EnterpriseCode == "ZIPERP" || EnterpriseCode == "PROMPTRND" || EnterpriseCode == "PIPL" || EnterpriseCode == "INDIFOSS" || EnterpriseCode == "INDIFOSSOLD") {
        $("#LstAddionalDetails").show();
        $(".LiTalukaVillage").show();
        $(".LiTalukaVillage").css('visibility', 'visible');
        return true;
    }
    else {
        return false;
    }
}

(function() {
    var name = $(location).attr("host").split('.')[0];
    $('head').append('<link rel="icon" type="image/x-icon" href="">');
    if (name != undefined && name != null && name != "" && name != '' && (name =="eastmade")) {
        $("title").html(name);
        $('link[rel="icon"]').attr('href', '/eastmade.ico');
    }
    if (name != undefined && name != null && name != "" && name != '' && (name =="eartylife")) {
        $("title").html(name);
        $('link[rel="icon"]').attr('href', '/eartylife.ico');
    }
    else {
        $("title").html("ZipERP");
        $('link[rel="icon"]').attr('href', '/favicon.ico');
    }
})();

function ShowAdditionalDetailsTab() {
    if (EnterpriseCode == "CLSL" || EnterpriseCode == "ZIPERP") {
        $(".clsAdditionalDetails").show();
    }
    else {
        $('.clsAdditionalDetails').hide();
    }
}