//Fill Sales Region Dropdown in Sales Area Dropdown Changes Event
function FillSalesRegionsListBySalesArea(SalesRegionId, SalesAreaId) {

    $(SalesAreaId).change(function () {
        if ($.trim($(SalesAreaId).val()) != "") {
            var selectedArea = $(SalesAreaId).val();
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('SalesRegions/GetAllSalesRegionsList'),
                data: JSON.stringify({ SalesAreaId: selectedArea }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    $(SalesRegionId).html('');
                    var _tempstr = "<option value=''>-- Select --</option>";
                    $.each(data, function (id, option) {
                        _tempstr += ' <option value="' + this.Id + '" > ' + this.Name + ' </option> ';
                    });
                    $(SalesRegionId).append(_tempstr);
                    $(SalesRegionId).change();
                },
                error: function (xhr, ajaxOptins, throwError) {
                    alert("Error occured");
                }
            });
        }
        else {
            $(SalesRegionId).html('');
            $(SalesRegionId).append("<option value=''>-- Select --</option>");
            $(SalesRegionId).change();
        }
    });
}

//Fill Sales Territories Dropdown in Sales Region Dropdown Changes Event
function FillSalesTerritoriesListBySalesRegion(SalesTerritoriesId, SalesRegionId) {

    $(SalesRegionId).change(function () {
        if ($.trim($(SalesRegionId).val()) != "") {
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('SalesTerritories/GetAllSalesTerritoriesList'),
                data: JSON.stringify({ SalesRegionId: $(SalesRegionId).val() }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    $(SalesTerritoriesId).html('');
                    var _tempstr = "<option value=''>-- Select --</option>";
                    $.each(data, function (id, option) {
                        _tempstr += ' <option value="' + this.Id + '" > ' + this.Name + ' </option> ';
                    });
                    $(SalesTerritoriesId).append(_tempstr);
                },
                error: function (xhr, ajaxOptins, throwError) {
                    alert("Error occured");
                }
            });
        }
        else {
            $(SalesTerritoriesId).html('');
            $(SalesTerritoriesId).append("<option value=''>-- Select --</option>");
        }
    });
}

//Fill Terms and Condition value Textbox in Terms and Condition Id Dropdown Changes Event
function FillTermsAndConditionValueByTermsAndConditionId(TermsConditionId, TermsConditionValue) {
    $(TermsConditionId).change(function () {
        if ($.trim($(TermsConditionId).val()) != "") {
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('TermsAndCondition/GetTermsCondition'),
                data: JSON.stringify({ ID: $(TermsConditionId).val() }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $(TermsConditionValue).val(data);
                },
                error: function (xhr, ajaxOptins, throwError) {
                    alert("Error occured");
                }
            });
        }
        else {
            $(TermsConditionValue).val('');
        }
    });
}

//Fill Item ID & Name and Description based on Code
function FillItemNameandIdbyCode(ItemName, ItemId, Description, ItemCode, ShowInactive) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Item/GetItemNameByItemCode'),
        data: JSON.stringify({ ItemCode: $(ItemCode).val(), ShowInactive: ShowInactive }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != '') {
                $(ItemCode).val(data.Code);
                $(ItemName).val(data.Name);
                $(ItemId).val(data.Id);
                if (Description != "")
                    $(Description).val(data.Description);
            }
            else {
                $(ItemCode).val('');
                $(ItemName).val('');
                $(ItemId).val('');
                if (Description != "")
                    $(Description).val('');
                AlertDialog(ItemCode, "Invalid item code");
                return false;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
}

//Fill Item ID & Name and Description based on Code (For Allowed Groups)
function FillItemNameandIdbyCode_ForAllowedGroup(ItemName, ItemId, Description, ItemCode, ObjectType, ObjectDocTypeId, ObjectTransType, ObjectDocTypeIdSub) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Item/GetItemNameByItemCode_AllowedGroups'),
        data: JSON.stringify({ ItemCode: $(ItemCode).val(), ObjectType: ObjectType, ObjectDocTypeId: $(ObjectDocTypeId).val(), ObjectTransType: ObjectTransType, ObjectDocTypeIdSub: $(ObjectDocTypeIdSub).val() }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != '') {
                $(ItemCode).val(data.Code);
                $(ItemName).val(data.Name);
                $(ItemId).val(data.Id);
                if (Description != "")
                    $(Description).val(data.Description);
            }
            else {
                $(ItemCode).val('');
                $(ItemName).val('');
                $(ItemId).val('');
                if (Description != "")
                    $(Description).val('');
                AlertDialog(ItemCode, "Invalid item code");
                return false;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
}

//Fill Machine ID & Name based on Code
function FillMachineNameandIdbyCode(MachineName, MachineId, MachineCode) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Machine/GetMachineNameByMachineCode'),
        data: JSON.stringify({ MachineCode: $(MachineCode).val() }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != '') {
                $(MachineCode).val(data.Code);
                $(MachineName).val(data.Name);
                $(MachineId).val(data.Id);
            }
            else {
                $(MachineCode).val('');
                $(MachineName).val('');
                $(MachineId).val('');
                AlertDialog(MachineCode, "Invalid Machine code");
                return false;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
}

//Fill Production Process ID & Name based on Code
function FillPRDProcessNameandIdbyCode(ProcessName, ProcessId, ProcessCode) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Process/GetProcessNameByProcessCode'),
        data: JSON.stringify({ ProcessCode: $(ProcessCode).val() }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != '') {
                $(ProcessCode).val(data.Code);
                $(ProcessName).val(data.Name);
                $(ProcessId).val(data.Id);
            }
            else {
                $(ProcessCode).val('');
                $(ProcessName).val('');
                $(ProcessId).val('');
                AlertDialog(ProcessCode, "Invalid Process code");
                return false;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
}

//Fill Production Overhead ID & Name based on Code
function FillPRDOverheadNameandIdbyCode(OverheadName, OverheadId, OverheadCode, OverheadRate) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Overhead/GetOverheadNameByOverheadCode'),
        data: JSON.stringify({ OverheadCode: $(OverheadCode).val() }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != '') {
                $(OverheadCode).val(data.Code);
                $(OverheadName).val(data.Name);
                $(OverheadId).val(data.Id);
                $(OverheadRate).val(data.Rate);
            }
            else {
                $(OverheadCode).val('');
                $(OverheadName).val('');
                $(OverheadId).val('');
                $(OverheadRate).val('');
                AlertDialog(OverheadCode, "Invalid Overhead code");
                return false;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
}

//Fill Store Dropdown by Given PlantId
function FillStoreByPlant(StoreId, PlantId, ShowInActiveStore, ShowSelectOption = false ) {

    $(PlantId).change(function () {
        if ($.trim($(PlantId).val()) != "") {

            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('Store/GetStoreByPlant'),
                data: JSON.stringify({ PlantId: $(PlantId).val(), ShowInActiveStore: ShowInActiveStore }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    $(StoreId).html('');
                    var _tempstr = "";
                    if (data.length != 1 || ShowSelectOption ) { _tempstr = "<option value=''>-- Select --</option>"; }
                    if (data.length == 1) {
                        _tempstr += ' <option selected value="' + data[0].Id + '" > ' + data[0].Name + ' </option> ';
                    } else {
                        $.each(data, function (id, option) {
                            _tempstr += ' <option value="' + this.Id + '" > ' + this.Name + ' </option> ';
                        });
                    }
                    $(StoreId).append(_tempstr);
                },
                error: function (xhr, ret, e) {
                    console.log('some error occured', ret, e);
                    return false;
                }
            });
        }
        else {
            $(StoreId).html('');
            $(StoreId).append("<option value=''>-- Select --</option>");
        }
    });
}

//Get Account Closing Balance
function FindAccClosingBalance(AccId, Date) {
    var CLBl = 0;
    if (AccId != "" && Date != "") {
        $.ajax
       ({
           cache: false,
           type: "POST",
           async: false,
           url: ITX3ResolveUrl('Common/FindAccClBalance'),
           data: JSON.stringify({ AccId: AccId, Date: Date }),
           dataType: "json",
           contentType: "application/json; charset=utf-8",
           success: function (data) {
               CLBl = data;
           },
           error: function (xhr, ret, e) {
               return false;
           }
       });
    }
    return CLBl;
}


// Fill Service Id & Service Name and Description based on Service Code
function FillServiceNameandIdbyCode(ServiceName, ServiceId, Description, ServiceCode, ObjectType, ObjectDocTypeId) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Services/GetServiceNameByServiceCode'),
        data: JSON.stringify({ ServiceCode: $(ServiceCode).val(), ObjectType: ObjectType, ObjectDocTypeId: $(ObjectDocTypeId).val()}),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != '') {
                $(ServiceCode).val(data.Code);
                $(ServiceName).val(data.Name);
                $(ServiceId).val(data.Id);
                if (Description != "")
                    $(Description).val(data.Description);
            }
            else {
                $(ServiceCode).val('');
                $(ServiceName).val('');
                $(ServiceId).val('');
                if (Description != "")
                    $(Description).val('');
                AlertDialog(ServiceCode, "Invalid Service code");
                return false;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
}

//Get Payment Terms Days By Payment Terms Id
function GetPaymentTermsDaysByPaymentTermsId(PaymentTermsId) {
    var Days = 0;
    if (PaymentTermsId) {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Account/GetPaymentTermsData'),
            data: JSON.stringify({ PaymentTermsId: PaymentTermsId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                Days = data.Days;
            },
            error: function (xhr, ret, e) {

            }

        });
    }
    return Days;
}

function GetPaymentTermsDetailAndSetDueDate(PaymentTermId, DocDateCntrl) {
    if ($('#PaymentTermsId').val() != "") {
        $.ajax
        ({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('PaymentTerm/GetFixDatePaymentTermDetail'),
            data: JSON.stringify({ PaymentTermId: PaymentTermId, DocDate: $(DocDateCntrl).val() }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.IsFixDatePaymentTerm == true) {
                    $('#DueDate').datepicker('setDate', data.FixDateDueDate);
                }
                else {
                    var Days = parseInt(data.Days);
                    if (Days > 0 && $(DocDateCntrl).val() != "") { $('#DueDate').datepicker('setDate', AddDaystoDate($(DocDateCntrl).val(), Days, "dd/MM/yyyy")) }
                    else if (parseInt(Days) == 0 && $(DocDateCntrl).val() != "") { $('#DueDate').datepicker('setDate', $(DocDateCntrl).val()) }
                }
            },
            error: function (xhr, ret, e) {
                return false;
            }
        });
    }
}

function GetAllSalesTaxList() {
    var locSalesTaxList = [];
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Tax/GetAllSalesTaxList'),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != '') {
                locSalesTaxList = data;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
    return locSalesTaxList;
}

function GetAllPurchaseTaxList() {
    var locPurchaseTaxList = [];
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Tax/GetAllPurchaseTaxList'),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != '') {
                locPurchaseTaxList = data;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
    return locPurchaseTaxList;
}

function GetStoreListByPlant(PlantId, ShowInActiveStore) {
    var Store = [];
    if ($.trim($(PlantId).val()) != "") {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Store/GetStoreByPlant'),
            data: JSON.stringify({ PlantId: $(PlantId).val(), ShowInActiveStore: ShowInActiveStore }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                Store = data;
            },
            error: function (xhr, ret, e) {
                console.log('some error occured', ret, e);
                return false;
            }
        });
    }
    return Store;
}

function GetPlantDataByPlantId(PlantId) {
    var ObjPlant = [];
    if ($.trim($(PlantId).val()) != "") {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Plant/GetPlantDataObject'),
            data: JSON.stringify({ PlantId: $(PlantId).val() }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "") {
                    ObjPlant = data;
                }
            },
            error: function (xhr, ret, e) {
                console.log('some error occured', ret, e);
                return false;
            }
        });
    }
    return ObjPlant;
}

function GetLogInventoryTypeMasterData(TypeCode, Id) {
    var LogInventoryTypeData = [];
    if (TypeCode != "" && Id != "") {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('LogInventoryType/GetLogInventoryDataObject'),
            data: JSON.stringify({ TypeCode: TypeCode, Id: Id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "0") {
                    LogInventoryTypeData = data;
                }
            },
            error: function (xhr, ret, e) {
                console.log('some error occured', ret, e);
                return false;
            }
        });
    }
    return LogInventoryTypeData;
}

function GetAccountTDSInfo(AccId) {
    var AccountTDSInfo = [];
    if (AccId) {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Account/GetAccountTDSData'),
            data: JSON.stringify({ AccId: AccId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "") {
                    AccountTDSInfo = data;
                }
            },
            error: function (xhr, ret, e) {
                console.log('some error occured', ret, e);
                return false;
            }
        });
    }
    return AccountTDSInfo;
}

function GetTDSDutyLedger(AccId) {
    var TDSDutyLedger = [];
    if (AccId) {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Account/GetTDSDutyLedger'),
            data: JSON.stringify({ AccId: AccId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "") {
                    TDSDutyLedger = data;
                }
            },
            error: function (xhr, ret, e) {
                console.log('some error occured', ret, e);
                return false;
            }
        });
    }
    return TDSDutyLedger;
}

function GetAllTDSDutyLedger() {
    var TDSDutyLedger = [];
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Account/GetAllTDSDutyLedger'),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != "") {
                TDSDutyLedger = data;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
    return TDSDutyLedger;
}

function GetAllNatureofPaymentList(ShowTDSus194QApplicableOnly) {
    var NatureofPaymentList = [];
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('TDSNatureofPayment/GetAllTDSNatureofPayment'),
        data: JSON.stringify({ "TDSus194QApplicable": ShowTDSus194QApplicableOnly }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != "") {
                NatureofPaymentList = data;
            }
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
    return NatureofPaymentList;
}

function GetItemAltUnitWiseQty(No) {
    if ($("#ItemUOM_" + No).val() != $("#ItemAltPriceUOM_" + No).val()) {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            data: JSON.stringify({ ItemId: $("#ItemId_" + No).val(), Qty: $("#ItemQty_" + No).val(), FromUnit: $("#ItemUOM_" + No).val(), ToUnit: $("#ItemAltPriceUOM_" + No).val() }),
            url: ITX3ResolveUrl('Item/GetItemAltUnitWiseQty'),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#ItemAltUnitQty_" + No).val(parseFloat(data).toFixed(GetUnitDecimalPlaces($("#ItemAltPriceUOM_" + No).val())));
                ShowAltPriceFileds(No);
            },
            error: function (xhr, ret, e) {
                alert('some error occured');
                return false;
            }
        });
    }
    else {
        $("#ItemAltUnitQty_" + No).val($("#ItemQty_" + No).val());
        ShowAltPriceFileds(No);
    }
}

function RenderBatchSerialPopup(ItemId, Qty, UOM, StoreId, PlantId, LineItemNo, ObjectId, ObjectCode, strAction, StockTransType, Attributes, IsChangeLineQty, SchemeItemNo = 0) {
    if (Qty == "" || Qty == null) { Qty = 0; }
    var width = $(document).width();
    var popupbatchwidth = 1100;
    if (width < 900) {
        popupbatchwidth = 700;
    }
    if (ItemId != "" && ItemId != undefined && ((StoreId != "" && StoreId != undefined) || ObjectCode== "PRDPO") && (parseFloat(Qty) > 0 || IsChangeLineQty == true) && UOM != "" && UOM != undefined) {
        $('#dialog-RenderBatchSerial').empty();
        $('#dialog-RenderBatchSerial').load(ITX3ResolveUrl(encodeURI('Item/RenderBatchSerial' + "?ItemId=" + ItemId + "&UOM=" + UOM + "&Qty=" + Qty + "&StoreId=" + StoreId + "&PlantId=" + PlantId + "&LineItemNo=" + LineItemNo + "&ObjectId=" + ObjectId + "&ObjectCode=" + ObjectCode + "&strAction=" + strAction + "&StockTransType=" + StockTransType + "&Attributes=" + encodeURIComponent(GetDecodedStringfromString(Attributes)) + "&IsChangeLineQty=" + IsChangeLineQty + "&SchemeItemNo=" + SchemeItemNo)));
        $('#dialog-RenderBatchSerial').dialog({
            title: "Batch-Serial",
            resizable: false,
            height: 500,
            width: popupbatchwidth,
            modal: true
        });
    }
}

function GetDecodedStringfromString(str) {
    if (str != undefined && str != "" && str != null) {
        str = str.replace(/&lt;/g, '<');
        str = str.replace(/&gt;/g, '>');
        str = str.replace(/&quot;/g, '"');
        str = str.replace(/&amp;/g, '&');
        str = str.replace(/&amp;nbsp;/g, '&nbsp;');
        str = str.replace(/&#39;/g, '');
        str = str.replace(/\\n/g, ' ');
        str = str.replace(/\\t/g, ' ');
        str = str.replace(/\\/g, '\\\\');
    }
    else {
        str = "";
    }
    return str;
}

function GetBatchSerialCode(ItemId) {
    var tempBatchSerailCode = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Item/GET_BatchSerial_Code'),
        data: JSON.stringify({ ItemId: ItemId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            tempBatchSerailCode = data;
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
    return tempBatchSerailCode;
}

function GetBatchSerialAttributeRequired(ItemId) {
    var tempBatchSerail_AttributeReq = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Item/GET_BatchSerial_AttributeReq'),
        data: JSON.stringify({ ItemId: ItemId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            tempBatchSerail_AttributeReq = data;
        },
        error: function (xhr, ret, e) {
            console.log('some error occured', ret, e);
            return false;
        }
    });
    return tempBatchSerail_AttributeReq;
}

function RenderItemAttributesDetailPopup(ItemId, Qty, UOM, LineItemNo, ObjectId, ObjectCode, strAction) {
    if (ItemId != "" && ItemId != undefined && parseFloat(Qty) > 0 && UOM != "" && UOM != undefined) {
        $('#dialog-RenderItemAttributesDetail').empty();
        $('#dialog-RenderItemAttributesDetail').load(ITX3ResolveUrl('Item/RenderItemAttributes' + "?ItemId=" + ItemId + "&UOM=" + UOM + "&Qty=" + Qty + "&LineItemNo=" + LineItemNo + "&ObjectId=" + ObjectId + "&ObjectCode=" + ObjectCode + "&strAction=" + strAction));
        $('#dialog-RenderItemAttributesDetail').dialog({
            title: "Item Attributes",
            resizable: false,
            height: 320,
            width: 500,
            modal: true
        });
    }
}

function RenderItemAttributesDetailByItemIdPopup(ItemId) {
    if (ItemId != "" && ItemId != undefined) {
        $('#dialog-RenderItemAttributesDetail').empty();
        $('#dialog-RenderItemAttributesDetail').load(ITX3ResolveUrl('Item/RenderItemAttributesByItemId' + "?ItemId=" + ItemId));
        $('#dialog-RenderItemAttributesDetail').dialog({
            title: "Item Attributes",
            resizable: false,
            height: 320,
            width: 500,
            modal: true
        });
    }
}

function JSGetAllUnitList() {
    var JSUnitList = [];
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Unit/GetAllUnitList'),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            JSUnitList = data
        },
        error: function (xhr, ret, e) {
            return false;
        }

    });
    return JSUnitList;
}

var gblJSUnitList = [];
function GetUnitDecimalPlaces(UnitId) {
    try {
        if (sessionStorage.getItem("ZIPERP_Unit") != null) { gblJSUnitList = $.parseJSON(sessionStorage.getItem("ZIPERP_Unit")); }
        if ((gblJSUnitList.length == 0 || gblJSUnitList == undefined) && (UnitId != '')) {
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('Unit/GetAllUnitList'),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    gblJSUnitList = data;
                    sessionStorage.setItem("ZIPERP_Unit", JSON.stringify(gblJSUnitList));
                },
                error: function (xhr, ret, e) {
                    return false;
                }
            });
        }
        var decimalplaces = 2;
        if (gblJSUnitList.length > 0 && UnitId.length > 0 && gblJSUnitList != undefined && UnitId != undefined) {
            $.grep(gblJSUnitList, function (V, I) {
                if ($.trim(V["Id"]) == UnitId) {
                    decimalplaces = $.trim(V["DecimalPlaces"])
                }
            });
        }
        return decimalplaces;
    }
    catch (e) {
        alert("Error:-" + e);
    }
}
function SetUOMDecimalPlaces(UnitId, QtyControl) {
    $(QtyControl).unbind("blur");
    $(QtyControl).ForceDecimalOnly(14, GetUnitDecimalPlaces(UnitId));
    $(QtyControl).blur();
}

var gblJSSalesTAXList = [];
function JSGetSalesTaxData(TaxId) {
    var tmpSalesTaxData;
    try {
        if (sessionStorage.getItem("ZIPERP_SalesTAX") != null) { gblJSSalesTAXList = $.parseJSON(sessionStorage.getItem("ZIPERP_SalesTAX")); }
        if ((gblJSSalesTAXList.length == 0 || gblJSSalesTAXList == undefined) && (TaxId != '')) {
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('Tax/GetAllSalesTaxList'),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    gblJSSalesTAXList = data;
                    sessionStorage.setItem("ZIPERP_SalesTAX", JSON.stringify(gblJSSalesTAXList));
                },
                error: function (xhr, ret, e) {
                    return false;
                }
            });
        }
        if (gblJSSalesTAXList.length > 0 && TaxId.length > 0 && gblJSSalesTAXList != undefined && TaxId != undefined) {
            $.grep(gblJSSalesTAXList, function (V, I) {
                if ($.trim(V["Id"]) == TaxId) {
                    tmpSalesTaxData = V;
                }
            });
        }
        return tmpSalesTaxData;
    }
    catch (e) {
        alert("Error:-" + e);
    }
}

var gblJSPurchaseTAXList = [];
function JSGetPurchaseTaxData(TaxId) {
    var tmpPurchaseTaxData;
    try {
        if (sessionStorage.getItem("ZIPERP_PurchaseTAX") != null) { gblJSPurchaseTAXList = $.parseJSON(sessionStorage.getItem("ZIPERP_PurchaseTAX")); }
        if ((gblJSPurchaseTAXList.length == 0 || gblJSPurchaseTAXList == undefined) && (TaxId != '')) {
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('Tax/GetAllPurchaseTaxList'),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    gblJSPurchaseTAXList = data;
                    sessionStorage.setItem("ZIPERP_PurchaseTAX", JSON.stringify(gblJSPurchaseTAXList));
                },
                error: function (xhr, ret, e) {
                    return false;
                }
            });
        }
        if (gblJSPurchaseTAXList.length > 0 && TaxId.length > 0 && gblJSPurchaseTAXList != undefined && TaxId != undefined) {
            $.grep(gblJSPurchaseTAXList, function (V, I) {
                if ($.trim(V["Id"]) == TaxId) {
                    tmpPurchaseTaxData = V;
                }
            });
        }
        return tmpPurchaseTaxData;
    }
    catch (e) {
        alert("Error:-" + e);
    }
}

function GetCurrencyConversion(EffDate, ForeignCurrencyId) {
    var CurrencyConversionVal = [];
    if (ForeignCurrencyId != "" && ForeignCurrencyId != undefined) {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('CurrencyConversion/GetCurrencyConversion'),
            data: JSON.stringify({ EffDate: EffDate, ForeignCurrencyId: ForeignCurrencyId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "" && data.length > 0) {
                    CurrencyConversionVal = data[0];
                }
            },
            error: function (xhr, ret, e) {
                console.log('some error occured', ret, e);
                return false;
            }
        });
    }
    return CurrencyConversionVal;
}

//Company wise or Plant Wise Auto batch Generate
function SetAutoBatchGenerateDataForItem(SrNo, ItemId, PlantId, StoreId, Qty) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('BatchMaster/FillAutoBatchGenerate'),
        data: JSON.stringify({ ItemId: ItemId, PlantId: PlantId, StoreId: StoreId, Qty: Qty, LineItemNo: SrNo }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            BatchSerialMast[0][SrNo] = data;
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
}