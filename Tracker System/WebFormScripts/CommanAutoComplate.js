
//ControlName = Control Name
//ActionName = Action Name
//GetValueFromControlId = Search value control name
//SetCodeControlId = Control id in which you want to save code or id
//SetTextControlId = Control id in which you want show text(Name)
function AutoComplate(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, ArrParam, SalesExecId) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);
            ArrParam["SalesExecutiveId"] = $(SalesExecId).val() == "" ? null : $(SalesExecId).val();
            $.ajax({
                url: autocompleteUrl,
                //data: ({ ParentName: $('#' + GetValueFromControlId).val() }),                
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {

                    response($.map(json, function (data, id) {

                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId);
}

//this function is only for account voucher page, do not use this function anywhere
function AccountAutoComplete(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetItemCodeControlId, No, TdsApplicable, ArrParam) { // this method is only for account voucher screen   
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);
            $.ajax({
                url: autocompleteUrl,
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            BAid: data.split('ᴌ')[2],
                            Alias: data.split('ᴌ')[3],
                            AccType: data.split('ᴌ')[4]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            if (ui.item.BAid == "") {
                $('#' + SetItemCodeControlId).val("0");
            }
            else {
                $('#' + SetItemCodeControlId).val(ui.item.BAid);
            }

            if ($("#VTypeCode").val() == "BP" && ui.item.AccType == "VN") {
                if (ui.item.Alias != "" && ui.item.Alias != null && ui.item.Alias != undefined)
                    $("#Drawees").val(ui.item.Alias);
                else
                    $("#Drawees").val(ui.item.label);
            }
            if (VouchReference[No]) {
                for (var i = 0; i < VouchReference[No].length; i++) {
                    VouchReference[No].splice(i, 1);
                }
            }
            if (CostVoucherRef[No]) {
                for (var i = 0; i < CostVoucherRef[No].length; i++) {
                    CostVoucherRef[No].splice(i, 1);
                }
            }
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            if (($.trim(TdsApplicable).toUpperCase() == "TRUE") && ($('#VTypeCode').val() == "JV" || $('#VTypeCode').val() == "CN" || $('#VTypeCode').val() == "CR" || $('#VTypeCode').val() == "PV" || $('#VTypeCode').val() == "BP" || $('#VTypeCode').val() == "CP")) {
                BindTDSInfo(ui.item.idValue, No, $('#' + SetTextControlId));
            }
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetItemCodeControlId);
}

function BindTDSInfo(AccId, No, ControlId) {
    var ObjAcc = GetAccountTDSInfo(AccId);
    if (ObjAcc) {
        $(ControlId).attr("TDSApplicable", ObjAcc.TDSApplicable);
        $(ControlId).attr("TDSNatureofPayment", ObjAcc.TDSNatureofPayment);
        $(ControlId).attr("TDSNatureofPaymentName", ObjAcc.TDSNatureofPaymentName);
        $(ControlId).attr("TDSDeductible", ObjAcc.TDSDeductible);
        $(ControlId).attr("IgnoreTDSExcemptionLimit", ObjAcc.IgnoreTDSExcemptionLimit);
        $(ControlId).attr("TDSDeducteeType", ObjAcc.TDSDeducteeType);
        $(ControlId).attr("TDSDeducteeTypePAN", ObjAcc.TaxNo1);

        gblAccTDSInfo[No] = ObjAcc;
        var Count1 = [], Count2 = [];
        Count1 = gblAccTDSInfo.filter(function (gblAccTDSInfo) { return (gblAccTDSInfo.TDSApplicable == 1) });
        Count2 = gblAccTDSInfo.filter(function (gblAccTDSInfo) { return (gblAccTDSInfo.TDSDeductible == 1) });  //&& Count2.length == 1 If 2 Party Selected then dont allow TDS calculation
        if ((Count1.length > 0) && ($('#VTypeCode').val() == "JV" || $('#VTypeCode').val() == "CN" || $('#VTypeCode').val() == "CP" || $('#VTypeCode').val() == "PV")) {
            $('#lnkAddTDS').show();
        }
        else if ($('#VTypeCode').val() == "BP") {
            $('#lnkAddTDS').show();
        } else if ($('#VTypeCode').val() == "CP") {
            $('#lnkAddTDS').show();
        }
        else {
            $('#lnkAddTDS').hide();
        }
    }
}

function AutoComplateItemOrService(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetItemCodeControlId, ArrParam) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);

            $.ajax({
                url: autocompleteUrl,
                //data: ({ ParentName: $('#' + GetValueFromControlId).val() }),                
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[2]                            
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + SetItemCodeControlId).val(ui.item.Code);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetItemCodeControlId);
}

function AutoCompleteItemsWithSetVariant(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetItemCodeControlId, ArrParam) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);

            $.ajax({
                url: autocompleteUrl,
                //data: ({ ParentName: $('#' + GetValueFromControlId).val() }),                
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[2]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + SetItemCodeControlId).val(ui.item.Code);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            try {
                SetVariant(ui.item.idValue);
            } catch (e) { }
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetItemCodeControlId);
}

function AutoCompleteComponent(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetComponentControlId, SetCalculationTypeControId, SetIsMandatoryControId, SetFormulaControId, ArrParam) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);

            $.ajax({
                url: autocompleteUrl,

                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[2],
                            CalculationType: data.split('ᴌ')[3],
                            IsMandatory: data.split('ᴌ')[4],
                            Formula: data.split('ᴌ')[5]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + SetComponentControlId).val(ui.item.Code);
            $('#' + SetCalculationTypeControId).val(ui.item.CalculationType);
            $('#' + SetIsMandatoryControId).val(ui.item.IsMandatory);
            $('#' + SetFormulaControId).val(ui.item.Formula);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);

            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetComponentControlId);
}

function ShowSearchComponentPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {

    $("#" + ClickButtonId).click(function () {

        ClearAllPopUpControls(ObjectType);

        $("#tblComponentPopupGrid").hide();
        $('#_tblComponentListPopup').hide();
        $("#tblComponentWithAttrPopupGrid").show();
        $('#_tblComponentListWithAttrPopup').show();

        ComponentGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, "#tblComponentWithAttrPopupGrid", "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });

}

function AutoCompleteGeneralStructure(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetStructureControlId, ArrParam) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);

            $.ajax({
                url: autocompleteUrl,
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[2],
                            value: data.split('ᴌ')[2],
                            idValue: data.split('ᴌ')[1],
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetStructureControlId);
}

function ShowSearchGeneralStructurePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {

    $("#" + ClickButtonId).click(function () {

        ClearAllPopUpControls(ObjectType);

        $("#tblGeneralStructurePopupGrid").hide();
        $('#_tblGeneralStructureListPopup').hide();
        $("#tblGeneralStructureWithAttrPopupGrid").show();
        $('#_tblGeneralStructureListWithAttrPopup').show();

        GeneralStructureGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, "#tblGeneralStructureWithAttrPopupGrid", "");

        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 700,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });

}

function AutoCompleteCity(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetCityCodeControlId, ArrParam) {

    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);

            $.ajax({
                url: autocompleteUrl,
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[2]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetCityCodeControlId);
}

function ShowSearchCityPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {

    $("#" + ClickButtonId).click(function () {

        ClearAllPopUpControls("CityWithAttr");

        $("#tblCityPopupGrid").hide();
        $('#_tblCityListPopup').hide();
        $("#tblCityWithAttrPopupGrid").show();
        $('#_tblCityListWithAttrPopup').show();
        CityGrid(ListAction, Controller, "City", QueryParam, Filters, Sorting, SetFunName, "#tblCityWithAttrPopupGrid", "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();

    });

}

function AutoCompleteCustomerType(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetCustomerTypeCodeControlId, ArrParam) {

    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);

            $.ajax({
                url: autocompleteUrl,
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[2]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetCustomerTypeCodeControlId);
}

function ShowSearchCustomerTypePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {

    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("CustomerTypeWithAttr");

        $("#tblCustomerTypePopupGrid").hide();
        $('#_tblCustomerTypeListPopup').hide();
        $("#tblCustomerTypeWithAttrPopupGrid").show();
        $('#_tblCustomerTypeListWithAttrPopup').show();

        CustomerTypeGrid(ListAction, Controller, "CustomerTypeList", QueryParam, Filters, Sorting, SetFunName, "#tblCustomerTypeWithAttrPopupGrid", "");

        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();

    });

}
function AutoComplateAssets(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetItemCodeControlId, ArrParam) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);

            $.ajax({
                url: autocompleteUrl,
                //data: ({ ParentName: $('#' + GetValueFromControlId).val() }),                
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[2]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + SetItemCodeControlId).val(ui.item.Code);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetItemCodeControlId);
}
function AutoComplateAssetsCode(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetItemCodeControlId, ArrParam) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);
            $.ajax({
                url: autocompleteUrl,
                //data: ({ ParentName: $('#' + GetValueFromControlId).val() }),                
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[2],
                            value: data.split('ᴌ')[2],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[0]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.Code);
            $('#' + SetItemCodeControlId).val(ui.item.label);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetItemCodeControlId);
}

function AutoComplateItemOrService_ForAllowedGroup(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetItemCodeControlId, ArrParam, DocTypId) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);
            ArrParam["ObjectDocTypeId"] = $(DocTypId).val();
            $.ajax({
                url: autocompleteUrl,
                //data: ({ ParentName: $('#' + GetValueFromControlId).val() }),                
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[2]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + SetItemCodeControlId).val(ui.item.Code);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetItemCodeControlId);
}

function CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetItemCodeControlId) {
    $('#' + GetValueFromControlId).change(function () {
        var isFromSelect = $('#' + GetValueFromControlId)[0].attributes["fromselect"].value;
        var LastSelectText = $('#' + GetValueFromControlId)[0].attributes["lasttext"].value;
        var IsInValid = false;
        if (!$("#" + SetCodeControlId).val()) { IsInValid = true; }
        else if (isFromSelect == "") { IsInValid = true; }
        else if (LastSelectText != $('#' + GetValueFromControlId).val()) { IsInValid = true; }
        if (IsInValid) {
            $('#' + GetValueFromControlId).val("");
            $('#' + SetCodeControlId).val("");
            if (SetItemCodeControlId != undefined && SetItemCodeControlId != "") { $('#' + SetItemCodeControlId).val(''); }
            $('#' + GetValueFromControlId)[0].attributes["fromselect"].value = "";
            $('#' + GetValueFromControlId)[0].attributes["lasttext"].value = "";
            $('#' + GetValueFromControlId).focus();
        }
    });
}

function AutoComplateCSPItem_ForAllowedProdGrp(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetItemCodeControlId, ArrParam) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);
            $.ajax({
                url: autocompleteUrl,
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {
                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            Code: data.split('ᴌ')[2]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + SetItemCodeControlId).val(ui.item.Code);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId, SetItemCodeControlId);
}

function AutoComplateAccountByAccountGroupIds(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetAccGroupId, SetAccGroupName, ArrParam, SalesExecId) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);
            ArrParam["SalesExecutiveId"] = $(SalesExecId).val() == "" ? null : $(SalesExecId).val();
            $.ajax({
                url: autocompleteUrl,
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {

                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            AccGroupId: data.split('ᴌ')[2],
                            AccGroupName: data.split('ᴌ')[3]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + SetAccGroupId).val(ui.item.AccGroupId);
            $('#' + SetAccGroupName).val(ui.item.AccGroupName);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId);
}

function AutoCompleteCustomerByTenant(ControlName, ActionName, GetValueFromControlId, SetCodeControlId, SetTextControlId, SetBranchId, SetBranchName, ArrParam, SalesExecId) {
    $('#' + GetValueFromControlId).attr("fromselect", "");
    $('#' + GetValueFromControlId).attr("lasttext", "");
    $('#' + GetValueFromControlId).autocomplete({
        source: function (request, response) {
            var autocompleteUrl = ITX3ResolveUrl(ControlName + "/" + ActionName);
            ArrParam["SalesExecutiveId"] = $(SalesExecId).val() == "" ? null : $(SalesExecId).val();
            $.ajax({
                url: autocompleteUrl,
                data: ($.parseJSON(JSON.stringify(ArrParam).replace(/ᴌ/g, $('#' + GetValueFromControlId).val()))),
                type: 'GET',
                cache: false,
                dataType: 'json',
                success: function (json) {

                    response($.map(json, function (data, id) {
                        return {
                            label: data.split('ᴌ')[0] + ' - ' + data.split('ᴌ')[3],
                            value: data.split('ᴌ')[0],
                            idValue: data.split('ᴌ')[1],
                            BranchId: data.split('ᴌ')[2],
                            BranchName: data.split('ᴌ')[3]
                        };
                    }));
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $('#' + SetCodeControlId).val(ui.item.idValue);
            $('#' + SetTextControlId).val(ui.item.label);
            $('#' + SetBranchId).val(ui.item.BranchId);
            $('#' + SetBranchName).val(ui.item.BranchName);
            $('#' + GetValueFromControlId).attr("fromselect", "1");
            $('#' + GetValueFromControlId).attr("lasttext", ui.item.label);
            return false;
        }
    });
    CheckValiDationAutoComplate(GetValueFromControlId, SetCodeControlId);
}

//ClickButtonId = Button Id on which you want to show popup

//// Grid /////

//ListAction = Action Name 
//Controller = Controller Name
//ObjectType = Object Type
//QueryParam = Querystring Paramiters
//Filters = Filters
//Sorting = Sorting
//SetFunName = Funtion Name
//DivName = Div Id on which you want show records
//ShowQuickCreate

//// Grid Finish ////

//ShowPopUpDivId = div id on which you want to show popup
//PopUpTitle = Popup title
//FocusTableId = Focus id
function ShowSearchItemGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("ItemGroup");
        ItemGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchAssetPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Asset");
        AssetGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchAccountGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("AccountGroup");
        AccGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            //            height: 'auto',
            //            width: 'auto',
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchAccSchedulemasterPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("AccSchedulemaster");
        AccSchedulemasterGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            //            height: 'auto',
            //            width: 'auto',
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}


function ShowSearchSchedulePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Schedule");
        ScheduleGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            //            height: 'auto',
            //            width: 'auto',
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}



function ShowSearchBanquetCategoryPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("BanquetCategory");
        BanquetMenuCategoryGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            //            height: 'auto',
            //            width: 'auto',
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchProductCategoryPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("ProductCategory");
        ProductCategoryGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            //            height: 'auto',
            //            width: 'auto',
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchServiceGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("ServiceGroup");
        ServiceGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchAccountPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {

        ClearAllPopUpControls("Account");
        AccGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

/// for search customer form sales executive
function ShowSearchAccountPopUp_BySalesExec(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, SalesExecId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Account");
        AccGrid_BySalesExec(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, $(SalesExecId).val());
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}


function ShowSearchCostGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("CostGroup");
        CostGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");

        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchBudgetGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("BudgetGroup");
        BudgetGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");

        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchServicePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Service");
        ServiceGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName,PObjectType, $(PObjectDocTypeId).val());
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchItemPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        if ($.trim(_AllowItemAttribute()).toUpperCase() == 'TRUE') {
            ClearAllPopUpControls("ItemWithAttr");
            $("#tblItemPopupGrid").hide();
            $('#_tblItemListPopup').hide();
            $("#tblItemWithAttrPopupGrid").show();
            $('#_tblItemListWithAttrPopup').show();
            ItemGridWithAttr_ForAllowedGroup(ListAction, Controller, "ItemWithAttr", QueryParam, Filters, Sorting, SetFunName, "#tblItemWithAttrPopupGrid", "");
            $("#" + ShowPopUpDivId).dialog({
                title: PopUpTitle,
                resizable: false,
                height: 540,
                width: 1200,
                modal: true
            });
        }
        else {
            ClearAllPopUpControls("Item");
            $("#tblItemWithAttrPopupGrid").hide();
            $('#_tblItemListWithAttrPopup').hide();
            $('#tblItemPopupGrid').show();
            $('#_tblItemListPopup').show();
            ItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
            $("#" + ShowPopUpDivId).dialog({
                title: PopUpTitle,
                resizable: false,
                height: 470,
                width: 900,
                modal: true
            });
            $("#" + FocusTableId).focus();
        }
    });

}

function ShowSearchItemPopUpWithAttribute(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        if ($.trim(_AllowItemAttribute()).toUpperCase() == 'TRUE') {
            ClearAllPopUpControls("ItemWithAttr");
            $("#tblItemPopupGrid").hide();
            $('#_tblItemListPopup').hide();
            $("#tblItemWithAttrPopupGrid").show();
            $('#_tblItemListWithAttrPopup').show();
            ItemGridWithAttribute(ListAction, Controller, "ItemWithAttr", QueryParam, Filters, Sorting, SetFunName, "#tblItemWithAttrPopupGrid", "");
            $("#" + ShowPopUpDivId).dialog({
                title: PopUpTitle,
                resizable: false,
                height: 540,
                width: 1200,
                modal: true
            });
        }
        else {
            ClearAllPopUpControls("Item");
            $("#tblItemWithAttrPopupGrid").hide();
            $('#_tblItemListWithAttrPopup').hide();
            $('#tblItemPopupGrid').show();
            $('#_tblItemListPopup').show();
            ItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
            $("#" + ShowPopUpDivId).dialog({
                title: PopUpTitle,
                resizable: false,
                height: 470,
                width: 900,
                modal: true
            });
            $("#" + FocusTableId).focus();
        }
    });

}

function ShowSearchBatchNoPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("BatchNo");
        BatchGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}


function ShowSearchItemPopUp_ForAllowedGroup(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType, ObjectDocTypeIdSub) {
    // Item Popup To Show Only Allowed item as per doc type master allowed group
    // here ObjectType and ObjectDocTypeId is passed    
    $("#" + ClickButtonId).click(function () {
        if ($.trim(_AllowItemListWithStock()).toUpperCase() == 'TRUE' && PObjectType == 'SO') {
            var VendorCustomer = $("#Vendor_Customer_Name").val().replace(/ /g, "ǒ");
            var VendorCustomerId = $("#Vendor_Customer_Id").val();
            var ItemCode = $("#ItemCode_1").val().replace(/ /g, "ǒ");
            var ItemName = $("#ItemName_1").val().replace(/ /g, "ǒ");

            VendorCustomer = VendorCustomer.replace("&", "¤");
            ItemCode = ItemCode.replace("&", "¤");
            ItemName = ItemName.replace("&", "¤");

            var AttributeGroupId = 1;

            $("#" + ShowPopUpDivId).dialog({
                title: PopUpTitle,
                resizable: false,
                height: 540,
                width: 1200,
                modal: true,
                open: function (event, ui) {
                    $(this).load(ITX3ResolveUrl("WebGrid/ItemList?SetMethodName=" + SetFunName + "&IsCalledFirstTime=" + true + "&VendorCustomer=" + VendorCustomer + "&VendorCustomerId=" + VendorCustomerId + "&ItemCode=" + ItemCode + "&ItemName=" + ItemName + "&AttributeGroupId=" + AttributeGroupId));
                }
            });
        }
        else if ($.trim(_AllowItemAttribute()).toUpperCase() == 'TRUE') {
            $("#tblItemPopupGrid").hide();
            $('#_tblItemListPopup').hide();
            $("#tblItemWithAttrPopupGrid").show();
            $('#_tblItemListWithAttrPopup').show();
            ItemGridWithAttr_ForAllowedGroup(ListAction, Controller, "ItemWithAttr", QueryParam, Filters, Sorting, SetFunName, "#tblItemWithAttrPopupGrid", PObjectType, $(PObjectDocTypeId).val(), PObjectTransType, $(ObjectDocTypeIdSub).val());
            $("#" + ShowPopUpDivId).dialog({
                title: PopUpTitle,
                resizable: false,
                height: 540,
                width: 1200,
                modal: true
            });
        }
        else {
            ClearAllPopUpControls("Item");
            $("#tblItemWithAttrPopupGrid").hide();
            $('#_tblItemListWithAttrPopup').hide();
            $('#tblItemPopupGrid').show();
            $('#_tblItemListPopup').show();
            ItemGrid_ForAllowedGroup(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType, $(ObjectDocTypeIdSub).val());
            $("#" + ShowPopUpDivId).dialog({
                title: PopUpTitle,
                resizable: false,
                height: 470,
                width: 900,
                modal: true
            });
            $("#" + FocusTableId).focus();
        }
    });


}

function ShowSearchItemAttributrPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("ItemAttribute");
        ItemAttribute(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchAssetsGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("AssetsGroup");
        AssetsGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchInvoicePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#" + ClickButtonId).click(function () {
        InvoiceGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        ClearAllPopUpControls("InvoiceData");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPendingInvoicePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#" + ClickButtonId).click(function () {
        PendingInvoiceGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        ClearAllPopUpControls("InvoiceData");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchInvoiceWithItemPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#" + ClickButtonId).click(function () {
        InvoiceGridWithItem(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPRDProcessPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("PRDProcess");
        PRDProcessGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}
function ShowSearchPRDOverheadPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        PRDOverheadGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchCostCenterPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("CostCenter");
        CostCenterGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "", "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchBudgetPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Budget");
        BudgetGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchBinMasterPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("BinMaster");
        BinMasterGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPrjSkillPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        PrjSkillGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}
function ShowSearchPrjOtherChargesPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        PrjOtherChargesGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPrjOverheadPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        PrjOverheadGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchUserPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("User");
        UserGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchGEMStagePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("GEMStage");
        GEMStageGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchTenderEnquiryNoPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("TenderForm");
        TenderFormGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchGEMProductPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("GEMProduct");
        GEMProductGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPrjActivityPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        PrjActivityGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPrjBOQGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, ZoneId) {

    $("#" + ClickButtonId).click(function () {
        PrjBOQGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "", $(ZoneId).val());
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchTasksPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, ZoneId) {

    $("#" + ClickButtonId).click(function () {
        TasksGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "", $(ZoneId).val());
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchGenericResourcesPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {

    $("#" + ClickButtonId).click(function () {
        GenericResourceGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchProductGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("ProductGroup");
        ProductGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchAssetsPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Assets");
        AssetsGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchBOQPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        BOQGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchRetailCustPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, Vendor_Cust_Id) {
    $("#" + ClickButtonId).click(function () {
        RetailGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "", $(Vendor_Cust_Id).val());
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchVendor_Cust_Wise_RetailCustTransDetailPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, Vendor_Cust_Id, DocType) {
    $("#" + ClickButtonId).click(function () {
        Vendor_Cust_Wise_RetailCustTransDetail(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "", $(Vendor_Cust_Id).val(), DocType);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchAttributeGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        AttributeGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchInsurancePolicyPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        InsurancePolicyGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchRoutePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        RoutesGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}
function ShowSearchPlantPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Plant");
        PlantGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchStorePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Store");
        StoreGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchMachinePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Machine");
        MachineGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchMachineGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("MachineGroup");
        MachineGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchQcTestTempPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        QcTestTempGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchSubTerritoryPopUp_ByTerritory(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectDocTypeId) {
    $("#" + ClickButtonId).click(function () {
        SubTerritoryGrid_ByTerritory(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, $(PObjectDocTypeId).val());
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchAdditionalChargePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        prjAdditionalCharge(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchContactGridPopUP_ForShipToOfCustomer(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectDocTypeId) {
    $("#" + ClickButtonId).click(function () {
        ContactGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, $(PObjectDocTypeId).val());
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchProjectPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ProjectGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchContactGridPopUP_ForBOQActivity(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, ProjectId, ZoneId) {
    $("#" + ClickButtonId).click(function () {
        BOQActivityNameGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, $(ProjectId).val(), $(ZoneId).val());
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSLocationPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSLocationGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSDepartmentPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSDepartmentGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSDesignationPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSDesignationGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSGradePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSGradeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSEmployeeTypePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSEmployeeTypeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSPresentCityPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSPresentCityGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSPermanentCityPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSPermanentCityGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSStructureCodePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSStructureCodeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSAssetPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSAssetCodeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSIndentPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSIndentCodeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}


function ShowSearchMemberPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        MemberGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSEmployeeForSeparation(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSEmployeeForSeparation(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHRMSExistingUserPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSUserGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchSOPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("SO");
        OrderMaster(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchOrderItemsPopUp_Chk(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#filtOrderNo_Chk, #filtOdrerDate_Chk, #filtCustomerName_Chk,#filtOrdItem_Chk").val('');
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("OrderItem_Chk");
        OrderItemGrid_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 515,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPRDPOPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("PRDPO");
        PRDPO(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPRDPOPopUp_Chk(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#filtPrdOrdDate_Chk, #filtPrdOrdNo_Chk, #filtPrdOrdType_Chk, #filtPrdOrdItemCode_Chk,#filtPrdOrdItem_Chk, #filtPrdOrdQty_Chk").val('');
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("PRDPOItem_Chk");
        PRDPO_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        $("#dialog-ProdOrderList").dialog({
            title: "Production Order",
            resizable: false,
            height: 490,
            width: 1050,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPendingAdPRDPOPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("PendingAdPRDPO");
        PendingAdPRDPO(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchHSNCodePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("HSNCode");
        HSNCode(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPendingStockTransferReqItemsPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("PendingStockTransferReqItemGrid");
        StockTransferReqItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPendingOrderItemsPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("OrderItemGrid");
        OrderItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPendingIssueItemsPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("IssueItemGrid");
        IssueItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchClaimReasonPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("MasterNameList");
        ClaimReasonGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ClearAllPopUpControls(DivObjectName) {
    if (DivObjectName == "Account")
        $("#filtAccName,#filtAccParentName,#filtAccCity,#filtAccPhone,#filtAccMobile,#filtAccCode").val('');
    if (DivObjectName == "AccSchedulemaster")
        $("#filtAccName,#filtAccParentName,#filtAccCity,#filtAccPhone,#filtAccMobile,#filtAccCode").val('');
    if (DivObjectName == "Item")
        $("#filtItemCode,#filtItemName,#filtDescription,#filtItemUnit,#filtItemGroup").val('');
    if (DivObjectName == "Component")
        $("#filtComponentCode,#filtComponentName").val('');
    if (DivObjectName == "ItemWithAttr")
        $("#filtItemCode_ItemWithAttr,#filtItemName_ItemWithAttr,#filtDescription_ItemWithAttr,#filtItemUnit_ItemWithAttr,#filtItemGroup_ItemWithAttr,#filtAttribute1_ItemWithAttr,#filtAttribute2_ItemWithAttr,#filtAttribute3_ItemWithAttr,#filtAttribute4_ItemWithAttr,#filtAttribute5_ItemWithAttr,#filtAttribute6_ItemWithAttr,#filtAttribute7_ItemWithAttr").val('');
    if (DivObjectName == "BatchNo")
        $("#filtBBatchNo,#filtBItemName,#filtBStoreName,#filtBClosing,#filtBMfgDate").val('');
    if (DivObjectName == "AccountGroup")
        $("#filtAccGrpName,#filtAccGrpParentName,#filtAccGrpCode").val('');
    if (DivObjectName == "Schedule")
        $("#filtScheduleName,#filtScheduleParentName,#filtScheduleCode").val('');
    if (DivObjectName == "BanquetCategory")
        $("#filtBanquetCatName,#filtBanquetCatParentName").val('');
    if (DivObjectName == "ProductCategory")
        $("#filtProductCategoryName,#filtProductCategoryParentName").val('');
    if (DivObjectName == "ItemGroup")
        $("#filtItemGrpName,#filtItemGrpParentName").val('');
    if (DivObjectName == "Asset")
        $("#filtAssetName,#filtAssetGrpName").val('');
    if (DivObjectName == "AssetsGroup")
        $("#filtAssetsGrpName,#filtAssetsGrpParentName").val('');
    if (DivObjectName == "Contact")
        $("#filtContactName,#filtContactAddress,#filtContactCity,#filtContactState,#filtContactMobile,#filtContactPhone").val('');
    if (DivObjectName == "ServiceGroup")
        $("#filtServiceGrpName,#filtServiceGrpParentName").val('');
    if (DivObjectName == "Service")
        $("#filtServiceCode,#filtServiceName,#filtServiceUnit,#filtServiceGroup").val('');
    if (DivObjectName == "CostGroup")
        $("#filtCostGrpName,#filtCostGrpParentName").val('');
    if (DivObjectName == "BudgetGroup")
        $("#filtBudgetGrpName,#filtBudgetGrpParentName").val('');
    if (DivObjectName == "PRDProcess")
        $("#filtPRDProcessCode, #filtPRDProcessName").val('');
    if (DivObjectName == "PRDOverhead")
        $("#filtPRDOverheadCode, #filtPRDOverheadName").val('');
    if (DivObjectName == "CostCenter")
        $("#filtCostCenterName, #filtCostCenterParentName").val('');
    if (DivObjectName == "Budget")
        $("#filtBudgetName").val('');
    if (DivObjectName == "User")
        $("#filtUserId,#filtUserName").val('');
    if (DivObjectName == "ProductGroup")
        $("#filtProductGrpName,#filtProductGrpParentName").val('');
    if (DivObjectName == "AttributeGroup")
        $("#filtAttributeGrpName").val('');
    if (DivObjectName == "Store")
        $("#filtStoreName, #filtPlantName").val('');
    if (DivObjectName == "Machine")
        $("#filtMachineCode, #filtMachineName, #filtMachineType,#filtMachineGroup").val('');
    if (DivObjectName == "MachineGroup")
        $("#filtMachineGroupName").val('');
    if (DivObjectName == "User")
        $("#filtHRUserName").val('');
    if (DivObjectName == "SO")
        $("#filtSODate,#filtSONo,#filtSOType,#filtSOCustomer,#filtSOAmount").val('');
    if (DivObjectName == "PRDPO")
        $("#filtPrdOrdDate,#filtPrdOrdNo,#filtPrdOrdType,#filtPrdOrdItem,#filtPrdOrdQty,#filtPrdOrdBatchNo").val('');
    if (DivObjectName == "HSNCode")
        $("#filtHSNCode,#filtHSNDesc").val('');
    if (DivObjectName == "PendingStockTransferReqItemGrid")
        $("#filtSTRTypeName, #filtSTRNo, #filtSTRDate, #filtSTRItemName, #filtSTRPendingQty").val('');
    if (DivObjectName == "OrderItemGrid")
        $("#filtOrderNo, #filtOdrerDate, #filtCustomerName").val('');
    if (DivObjectName == "POSPackages")
        $('#filtPackName,#filtPackEffDate,#filtPackValidity,#filtPackAmount').val('');
    if (DivObjectName == "ClubEvents")
        $('#filtEveTitle,#filtEveCategory,#filtEveType,#filtEveTopic,#filtEveSDAte,#filtEveEDate').val('');
    if (DivObjectName == "MasterNameList")
        $("#filtMasterName").val('');
    if (DivObjectName == "LogInventoryTransList")
        $("#filtLogDocDate, #filtLogDocType, #filtLogDocNo, #filtLogCustomer, #filtLogPlant, #filtLogDepartment, #filtLogAmount").val('');
    if (DivObjectName == "CRMLeads")
        $("#filtLeadNo, #filtLeadTitle, #filtLeadDate, #filtLeadCustomer, #filtLeadContact").val('');
    if (DivObjectName == "PRDPOItem_Chk")
        $("#filtPrdOrdDate_Chk, #filtPrdOrdNo_Chk, #filtPrdOrdType_Chk, #filtPrdOrdItem_Chk, #filtPrdOrdQty_Chk").val('');
    if (DivObjectName == "OrderItem_Chk")
        $("#filtOrderNo_Chk, #filtOdrerDate_Chk, #filtCustomerName_Chk").val('');
    if (DivObjectName == "Assets")
        $("#filtAssetCode,#filtAssetName,#filtAssetDescription,#filtAssetSerialNumber").val('');
    if (DivObjectName == "VoucherNarration")
        $("#filtVoucherNarration").val('');
    if (DivObjectName == "PurchaseInquiry")
        $("#filtPurInquiryDate, #filtPurInquiryNo, #filtPurInquiryType").val('');
    if (DivObjectName == "Quotation")
        $("#filtQuotationDate, #filtQuotationNo, #filtQuotationType, #filtQuotationParty, #filtQuotationAmount").val('');
    if (DivObjectName == "PendingAdPRDPO")
        $("#filtAdPrdOrdDate, #filtAdPrdOrdNo, #filtAdPrdOrdItem, #filtAdPrdOrdQty, #filtAdPrdOrdPrcess").val('');
    if (DivObjectName == "ZBGLSalesGeneralStructure")
        $("#filtStructureName,#filtEntryDate").val('');
    if (DivObjectName == "AccountVoucher")
        $("#filtVchTypeName, #filtVchDate, #filtVchNo, #filtVchAccName, #filtVchAmount").val('');
    if (DivObjectName == "BinMaster")
        $("#filtBinName,#filtBinPlantName,#filtBinStoreName").val('');
    if (DivObjectName == "PRDAdPOItem_Chk")
        $("#filtAdPrdOrdDate_Chk, #filtAdPrdOrdNo_Chk, #filtAdPrdOrdType_Chk, #filtAdPrdOrdItem_Chk, #filtAdPrdOrdQty_Chk").val('');
    if (DivObjectName == "BOMItem_Chk")
        $("#filtBOMType_Chk, #filtBOMVariant_Chk, #filtBOMItemCode_Chk, #filtBOMItemName_Chk, #filtBOMPlant_Chk").val('');
    if (DivObjectName == "PRDPOItemSerialMappingPending_Chk")
        $("#filtGRNo_Chk, #filtGRDate_Chk, #filtPrdOrdNo_Chk, #filtGRItemCode_Chk,#filtGRItemName_Chk,#filtGRQty_Chk").val('');
    if (DivObjectName == "InvoiceData")
        $("#PRfiltInvDocDate,#PRfiltInvDocType, #PRfiltInvParty, #PRfiltInvDocNo, #PRfiltInvRefNo,#PRfiltInvTotalAmount,#filtInvDocDate,#filtInvDocType, #filtInvParty, #filtInvDocNo,#filtInvTotalAmount").val('');
    if (DivObjectName == "Taluka")
        $("#filtCountryName,#filtStateName,#filtCityName,#filtTalukaName").val('');
    if (DivObjectName == "GEMStage")
        $("#filtGEMStageName").val('');
    if (DivObjectName == "GEMProduct")
        $("#filtGEMProductName").val(''); 
    if (DivObjectName == "TenderForm")
        $("#filtTenderEnquiryNo").val('');
    if (DivObjectName == "ModeofAcknowledgement")
        $("#filtModeofAcknowledgementName").val('');
    if (DivObjectName == "PendingReasonGroup")
        $("#filtPendingReasonGroupName").val('');
    if (DivObjectName == "TechnicianMaster")
        $("#filtTechnicianMasterName").val('');
    if (DivObjectName == "ApprovedSOList") {
        $("#filtSONo").val('');
        $("#filtSODate").val('');
        $("#filtSOCustomer").val('');
    }
    if (DivObjectName == "Vertical")
        $("#filtVerticalName").val('');
}

function ShowSearchHRMSEmployeeForDayArrear(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        HRMSEmployeeForDayArrear(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchTenantPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        TenantPopUp(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPackagePopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls('POSPackages');
        PackageGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchEventsPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {

    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls('ClubEvents');
        EventsGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchLogInventoryTransListPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("LogInventoryTransList");
        LogInventoryTransGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 515,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPOSCustomerMasterPopup(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        POSRetailCustomerGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 515,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchCRMLeadsPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("CRMLeads");
        CRMLeadsGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");

        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchApprovedSOListPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("ApprovedSOList");
        ApprovedSOListGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");

        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchVocuherPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("AccountVoucher");
        VoucherGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}


function ShowSearchVoucherNarrationPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("VoucherNarration");
        VoucherNarrationGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchPurchaseInquiryPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("PurchaseInquiry");
        PurchaseInquiryGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}

function ShowSearchQuotationPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Quotation");
        QuotationGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName);
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}
function ShowSearchAdPRDPOPopUp_Chk(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#filtAdPrdOrdDate_Chk, #filtAdPrdOrdNo_Chk, #filtAdPrdOrdType_Chk, #filtAdPrdOrdItem_Chk, #filtAdPrdOrdQty_Chk").val('');
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("PRDAdPOItem_Chk");
        PRDAdPO_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        $("#dialog-AdProdOrderList").dialog({
            title: "Advance Production Order",
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}
function ShowSearchBOMPopUp_Chk(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#filtBOMType_Chk, #filtBOMVariant_Chk, #filtBOMItemCode_Chk, #filtBOMItemName_Chk, #filtBOMPlant_Chk").val('');
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("BOMItem_Chk");
        BOMVarientItem_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        $("#dialog-BOMList").dialog({
            title: "Select BOM",
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}
function ShowCustSearchPRDPOPopUp_Chk(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId, PObjectType, PObjectDocTypeId, PObjectTransType) {
    $("#filtGRNo_Chk,#filtGRDate_Chk,#filtPrdOrdNo_Chk,#filtGRItemCode_Chk, #filtGRItemName_Chk,#filtGRQty_Chk").val('');
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("PRDPOItemSerialMappingPending_Chk");
        CustPRDPOItemSerialMappingPendingList_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, $(PObjectDocTypeId).val(), PObjectTransType);
        $("#dialog-CustProdOrderList").dialog({
            title: "GR No",
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}
function ShowSearchTalukaPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {

    $("#" + ClickButtonId).click(function () {

        ClearAllPopUpControls("Taluka");

        $("#tblTalukaGrid").show();
        TalukaGrid(ListAction, Controller, "Taluka", QueryParam, Filters, Sorting, SetFunName, "#tblTalukaGrid", "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();

    });

}
function ShowSearchModeofAcknowledgementPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {

        ClearAllPopUpControls("ModeofAcknowledgement");

        $("#tblModeofAcknowledgementGrid").show();
        ModeofAcknowledgementGrid(ListAction, Controller, "ModeofAcknowledgement", QueryParam, Filters, Sorting, SetFunName, "#tblModeofAcknowledgementGrid", "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();

    });

}
function ShowSearchPendingReasonGroupPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {

        ClearAllPopUpControls("PendingReasonGroup");

        $("#tblPendingReasonGroupGrid").show();
        PendingReasonGroupGrid(ListAction, Controller, "PendingReasonGroup", QueryParam, Filters, Sorting, SetFunName, "#tblPendingReasonGroupGrid", "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();

    });
}
function ShowSearchTechnicianMasterPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {

        ClearAllPopUpControls("TechnicianMaster");

        $("#tblTechnicianMasterGrid").show();
        TechnicianMasterGrid(ListAction, Controller, "TechnicianMaster", QueryParam, Filters, Sorting, SetFunName, "#tblTechnicianMasterGrid", "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();

    });
}
function ShowSearchVerticalPopUp(ClickButtonId, ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ShowPopUpDivId, PopUpTitle, FocusTableId) {
    $("#" + ClickButtonId).click(function () {
        ClearAllPopUpControls("Vertical");
        VerticalGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, "");
        $("#" + ShowPopUpDivId).dialog({
            title: PopUpTitle,
            resizable: false,
            height: 470,
            width: 900,
            modal: true
        });
        $("#" + FocusTableId).focus();
    });
}
