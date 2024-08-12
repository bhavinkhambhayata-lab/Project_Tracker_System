
var Title = "";

function CheckExistance(Value, ColumnName, TableName, WhereCondition, Message, IsMasterDB, IsEnterpriseLevel) {
    var flag = false;
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckExistance'),
        data: JSON.stringify({ Value: Value, ColumnName: ColumnName, TableName: TableName, WhereCondition: WhereCondition, IsMasterDB: IsMasterDB, IsEnterpriseLevel: IsEnterpriseLevel }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Val >= 1) {
                label('lblAlert', 'common', 'Alert');
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                            flag = true;
                            return true;
                        }
                    }]
                });
                var msg = data.Msg.replace("@param1", Value);

                $("#lblRequiredLayout").text(msg);
                flag = true;
                return true;
            }
        },
        error: function (xhr, ret, e) {
            return false;
        }

    });

    return flag;
}

function CheckExistance_WithoutDialog(Value, ColumnName, TableName, WhereCondition, Message, IsMasterDB, IsEnterpriseLevel, TenantId) {
    var flag = false;
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckExistance'),
        data: JSON.stringify({ Value: Value, ColumnName: ColumnName, TableName: TableName, WhereCondition: WhereCondition, IsMasterDB: IsMasterDB, IsEnterpriseLevel: IsEnterpriseLevel, TenantId: TenantId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Val >= 1) {
                flag = true;
            }

        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
    return flag;
}

function CheckItemNegativeBalance(ItemList, ItemId, ItemName, UOM, Qty, DocDate, InvTypeCode, MastId) {
    var flag; var Json_dt = [];
    if (InvTypeCode == "ST_Dutron") {
        InvTypeCode = "ST";
        ItemList = merge_JSList(ItemList, ItemFGList);
        ItemList = merge_JSList(ItemList, ItemWMList);
    }
    else if (InvTypeCode == "ST") {
        ItemList = merge_JSList(ItemList, ProductionItemList);
    }
    if (InvTypeCode == "TT" || InvTypeCode == "JWI") {
        for (var i = 0; i < ItemList.length; i++) {
            var model = {
                "ItemId": ItemList[i].ItemId,
                "ItemName": ItemList[i].ItemName,
                "UOM": ItemList[i].UOM ? ItemList[i].UOM : "",
                "Qty": parseFloat(ItemList[i].Qty) > 0 ? parseFloat(ItemList[i].Qty).toFixed(4) : 0,
                "StkQty": parseFloat(ItemList[i].Qty) > 0 ? parseFloat(ItemList[i].Qty).toFixed(4) : 0,
                "StoreId": $('#ToStoreId').val() ? $('#ToStoreId').val() : "",
                "MovCode": "I"
            }
            Json_dt.push(model);
        }
    }
    if (InvTypeCode == "SDASO") {
        for (var i = 0; i < ItemList.length; i++) {
            var model = {
                "ItemId": ItemList[i].ItemId,
                "ItemName": ItemList[i].ItemName,
                "UOM": ItemList[i].UOM ? ItemList[i].UOM : "",
                "Qty": parseFloat(ItemList[i].Qty) > 0 ? parseFloat(ItemList[i].Qty).toFixed(4) : 0,
                "StkQty": parseFloat(ItemList[i].Qty) > 0 ? parseFloat(ItemList[i].Qty).toFixed(4) : 0,
                "StoreId": ItemList[i].StoreId ? ItemList[i].StoreId : ($('#StoreId').val() ? $('#StoreId').val() : ""),
                "MovCode": ItemList[i].MovCode ? ItemList[i].MovCode : "",
                "OrderTransId": ItemList[i].OrderTransId,
                "Id": ItemList[i].Id
            }
            Json_dt.push(model);
        }
        InvTypeCode = "SD";
    } else {
        for (var i = 0; i < ItemList.length; i++) {
            var model = {
                "ItemId": ItemList[i].ItemId,
                "ItemName": ItemList[i].ItemName,
                "UOM": ItemList[i].UOM ? ItemList[i].UOM : "",
                "Qty": parseFloat(ItemList[i].Qty) > 0 ? parseFloat(ItemList[i].Qty).toFixed(4) : 0,
                "StkQty": parseFloat(ItemList[i].Qty) > 0 ? parseFloat(ItemList[i].Qty).toFixed(4) : 0,
                "StoreId": ItemList[i].StoreId ? ItemList[i].StoreId : ($('#StoreId').val() ? $('#StoreId').val() : ""),
                "MovCode": ItemList[i].MovCode ? ItemList[i].MovCode : ""
            }
            Json_dt.push(model);
        }
    }
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckItemNegativeBalance'),
        data: JSON.stringify({ Json_dt: JSON.stringify(Json_dt), DocDate: DocDate, InvTypeCode: InvTypeCode, MastId: MastId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Result == "1") {
                flag = false;
                AlertDialog("", data.Message);
                return false;
            }
            else {
                flag = true;
                return true;
            }
        },
        error: function (xhr, ret, e) {
            return false;
        }

    });
    return flag;
}

function CheckItemProjectedNegativeBalance(ItemList, DocDate, MastId, PlantId, AllowNegative, _FunName) {
    var flag; var Json_dt = [];

    for (var i = 0; i < ItemList.length; i++) {
        var model = {
            "ItemId": ItemList[i].ItemId,
            "ItemName": ItemList[i].ItemName,
            "UOM": ItemList[i].UOM ? ItemList[i].UOM : "",
            "Qty": parseFloat(ItemList[i].Qty) > 0 ? parseFloat(ItemList[i].Qty).toFixed(4) : 0,
            "StkQty": parseFloat(ItemList[i].Qty) > 0 ? parseFloat(ItemList[i].Qty).toFixed(4) : 0
        }
        Json_dt.push(model);
    }
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckItemProjectedNegative'),
        data: JSON.stringify({ Json_dt: JSON.stringify(Json_dt), DocDate: DocDate, MastId: MastId, PlantId: PlantId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Result == "1") {
                var str = "";
                for (var count = 0; count < data.lstMessages.length; count++) {
                    str += "<tr>";
                    str += "<td>" + (count + 1) + "</td>";
                    str += "<td>" + data.lstMessages[count][0].Value + "</td>";
                    str += "</tr>";
                }
                $('#tblValidationMessages').html(str);


                if (AllowNegative == "NOTALLOW") {
                    flag = false;
                    label('lblAlert', 'common', 'Alert');
                    $("#divWarningMessage").dialog({
                        resizable: false,
                        height: 400,
                        width: 500,
                        modal: true,
                        show: 'fade',
                        buttons: [{
                            text: "OK",
                            click: function () {
                                $("#divWarningMessage").dialog("close");
                            }
                        }]
                    });
                    $('.ui-dialog .ui-dialog-buttonpane').attr("style", "width:99%");
                    return false;
                }
                else if (AllowNegative == "WARNING") {
                    str = "<tr>";
                    str += "<td></td>";
                    str += "<td>Are you sure want to continue ? </td>";
                    str += "</tr>";
                    $('#tblValidationMessages').append(str);

                    flag = false;
                    label('lblAlert', 'common', 'Alert');
                    $("#divWarningMessage").dialog({
                        resizable: false,
                        height: 400,
                        width: 500,
                        modal: true,
                        show: 'fade',
                        buttons: [
                            {
                                text: "Yes",
                                click: function () {
                                    $("#divWarningMessage").dialog("close");
                                    SubmitFormDataAllowNegative(true);
                                }
                            },
                            {
                                text: "No",
                                click: function () {
                                    $("#divWarningMessage").dialog("close");
                                }
                            }]
                    });
                    $('.ui-dialog .ui-dialog-buttonpane').attr("style", "width:99%");
                    return false;
                }
                else {
                    flag = true;
                    return true;
                }
            }
            else {
                flag = true;
                return true;
            }
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });

    return flag;
}

function CheckCreditLimit(AccId, DocDate, InvTypeCode, MastId, CTotalAmount, Warning, CreditLimitTypeId) {
    var flag = false;
    var returndata = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckAccountCreditLimit'),
        data: JSON.stringify({ AccId: AccId, DocDate: DocDate, InvTypeCode: InvTypeCode, MastId: MastId, CTotalAmount: CTotalAmount, CreditLimitTypeId: CreditLimitTypeId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            returndata = data;
            if (data != null && data != "" && data != undefined) {
                if (Warning) {
                    flag = false;
                    return returndata;
                }
                else {
                    flag = false;
                    if (InvTypeCode == 'SO') {
                        var msg = label("msgCSPExceedCreditLimit", "common", "You can not post order as you have exceed the credit days or amount limit.");
                        AlertDialog("", msg);
                    }
                    else {
                        //var msg = label("msgExceedCreditLimit", "common", "This customer have exceeded the credit days limit or amount limit.");
                        return returndata;
                    }
                }
            }
            else {
                flag = true;
                return returndata;
            }
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
    return returndata;
}

function GetColumnValueGeneralised(getColumn, byColumnName, tableName, byColumnValue, WhereCond, TenantId) {
    var returndata = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/GetColumnValueGeneralised'),
        data: JSON.stringify({ getColumn: getColumn, byColumnName: byColumnName, tableName: tableName, byColumnValue: byColumnValue, WhereCond: WhereCond, TenantId: TenantId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            returndata = data;
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
    return returndata;
}

function funJSSendEmailPopup(Id, Email_ToId, DocType, DefPrint, EmailIds) {
    try {
        $('#DivCommonSendMailLoader').empty();
        var url;
        //url = ITX3ResolveUrl("QuickItemGroup/CreateQuickItemGroup?strAction=C");
        url = ITX3ResolveUrl("common/RenderSendMailPartial?Id=" + Id + "&Email_ToId=" + Email_ToId + "&DocType=" + DocType + "" + "&EmailIds=" + EmailIds);
        $('#DivCommonSendMailLoader').load(url);
        $('#DivCommonSendMailLoader').dialog({
            title: "Send E-Mail",
            resizable: false,
            height: 490,
            width: 510,
            modal: true
        });

    }
    catch (exception) {
        alert(exception);
    }
    return false;
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][\d]\.|1[\d]{2}\.|[\d]{1,2}\.))((25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\.){2}(25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
}
function Readwbdata(event, ControlId) {
    if ((event.which || event.keyCode) == 113) {
        $.get("http://localhost:8121/readwbdata", function (data) {
            var wt = $.trim(data);
            if (wt > 0) {
                $('#' + ControlId).val(parseFloat(wt).toFixed(3));
            }
            else {
                $('#' + ControlId).val(0);
            }
        });
    }
}

jQuery.fn.ForceNumericOnly = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            if (e.shiftKey || e.ctrlKey || e.altKey) {
                return (e.preventDefault());
            }
            var key = e.charCode || e.keyCode || 0;
            if ($(this).val().indexOf('.') !== -1 && (key == 190 || key == 110))
                return (e.preventDefault());

            return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                key == 110 ||
                key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });

}

jQuery.fn.ForceDecimalOnly = function (value, precision) {
    return this.each(function () {
        $(this).keydown(function (event) {
            if (event.shiftKey && event.keyCode != 9) {
                event.preventDefault();
            }
            var key = event.charCode || event.keyCode || 0;


            if (
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105) || (key >= 35 && key <= 40) ||
                key == 8 || key == 9 || key == 37 ||
                key == 39 || key == 46 || key == 110 || key == 190 || key == 115) {
            } else {
                event.preventDefault();
            }

            if ($(this).val().indexOf('.') !== -1 && (key == 190 || key == 110))
                event.preventDefault();

            if ($(this).val().length > value + precision + 1) {
                if (key == 8 || key == 9 || key == 37 || key == 39 || key == 46 || key == 115) { }
                else { event.preventDefault(); }
            }
            if ((key >= 48 && key <= 57) || (key >= 96 && key <= 105) || key == 115) {

                var split = ($(this).val().split("."));
                if (split[1] != null && split[1] != "") {
                    //if (split[1].length == precision)
                    //    event.preventDefault();
                }
                else {
                    if ($(this).val().length == value)
                        event.preventDefault();
                }
            }

        });

        $(this).blur(function () {
            if ($(this).val() != '') {
                $(this).val(parseFloat($(this).val()).toFixed(precision));
                //$(this).val(($(this).val()).toFixed(precision));
            }
        });
    });
}

jQuery.fn.CheckValidDate = function (ControlId, format) {
    return this.each(function () {
        $(this).focusout(function () {
            var isvalid = true;
            if ($(this).val() != '') {
                if (format == "dd/MM/yyyy") {
                    var date = $(this).val().split("/");
                    if (date.length == 3) {
                        var $Day = date[0];
                        var $Month = date[1];
                        var $Year = date[2];
                        if ($Month.length == "2") { $Month = $Month.replace("0", ""); }

                        if (parseInt($Month) > 12) { isvalid = false; }
                        if (parseInt($Year.length) != 4) { isvalid = false; }
                        if (parseInt($Day.length) > 2) { isvalid = false; }
                        else {
                            if ($Month == "1" || $Month == "3" || $Month == "5" || $Month == "7" || $Month == "8" || $Month == "10" || $Month == "12") {
                                if (parseInt($Day) > 31) { isvalid = false; }
                            }
                            else if ($Month == "4" || $Month == "6" || $Month == "9" || $Month == "11") {
                                if (parseInt($Day) > 30) { isvalid = false; }
                            }
                            else if ($Month == "2") {
                                if ((parseInt($Year) % 4 == 0 && parseInt($Year) % 100 != 00) || parseInt($Year) % 400 == 0) {
                                    if (parseInt($Day) > 29) { isvalid = false; }
                                }
                                else {
                                    if (parseInt($Day) > 28) { isvalid = false; }
                                }
                            }
                        }
                    }
                    else
                        isvalid = false;
                }

            }
            if (isvalid == false) {
                label('lblAlert', 'common', 'Alert');
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                            $(ControlId).focus();
                        }
                    }]
                });
                $("#lblRequiredLayout").text("Invalid Date");
                return false;
            }
            return true;

        });
    });

}

jQuery.fn.CheckValidTime = function (ControlId, format) {
    return this.each(function () {
        $(this).focusout(function () {
            var isvalid = true;
            if ($(this).val() != '') {
                if (format == "hh:mm tt") {
                    var time = $(this).val().split(" ")[0];
                    if (time.length == 5) {
                        var timeH = time.split(":")[0];
                        var timeM = time.split(":")[1];
                        if (timeH.length == "2") { timeH = timeH.replace("0", ""); } else { isvalid = false; }
                        if (parseInt(timeH) > 12) { isvalid = false; }
                        if (timeM.length > 2) { isvalid = false; }
                        if (parseInt(timeM) > 60) { isvalid = false; }
                    }
                    else
                        isvalid = false;
                }
            }
            if (isvalid == false) {
                label('lblAlert', 'common', 'Alert');
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                            $(ControlId).focus();
                        }
                    }]
                });
                $("#lblRequiredLayout").text("Invalid Time");
                return false;
            }
            return true;
        });
    });
}

function CheckMandatoryField(cntrlID, cntrlType, Msg) {
    if (cntrlType == 'textbox' || cntrlType == 'textarea') {
        if ($.trim($(cntrlID).val()) == '') {
            label('lblRequired', 'common', 'Required');
            $("#lblRequiredLayout").text(Msg);
            $("#dialog-RequiredLayout").dialog({
                title: Title,
                resizable: false,
                height: 170,
                modal: true,
                buttons: [{
                    text: GetLable('lblOK', 'common', 'OK'),
                    click: function () {
                        $(this).dialog("close");
                        $(cntrlID).focus();
                    }
                }]
            });
            return false;
        }
        else {
            return true;
        }
    }
    else if (cntrlType == 'dropdown') {
        if ($.trim($(cntrlID).val()) == '') {
            label('lblRequired', 'common', 'Required');
            $("#lblRequiredLayout").text(Msg);
            $("#dialog-RequiredLayout").dialog({
                title: Title,
                resizable: false,
                height: 170,
                modal: true,
                buttons: [{
                    text: GetLable('lblOK', 'common', 'OK'),
                    click: function () {
                        $(this).dialog("close");
                        $(cntrlID).focus();
                    }
                }]
            });
            return false;
        }
        else {
            return true;
        }
    }
    else if (cntrlType == 'kendodropdown') {
        if ($.trim($(cntrlID).val()) == '') {
            label('lblRequired', 'common', 'Required');
            $("#lblRequiredLayout").text(Msg);
            $("#dialog-RequiredLayout").dialog({
                title: Title,
                resizable: false,
                height: 170,
                modal: true,
                buttons: [{
                    text: GetLable('lblOK', 'common', 'OK'),
                    click: function () {
                        $(this).dialog("close");
                        $(cntrlID).data("kendoDropDownList").focus();
                    }
                }]
            });
            return false;
        }
        else {
            return true;
        }
    }
    else if (cntrlType == 'kendonumerictextbox') {
        if ($.trim($(cntrlID).val()) == '') {
            label('lblRequired', 'common', 'Required');
            $("#lblRequiredLayout").text(Msg);
            $("#dialog-RequiredLayout").dialog({
                title: Title,
                resizable: false,
                height: 170,
                modal: true,
                buttons: [{
                    text: GetLable('lblOK', 'common', 'OK'),
                    click: function () {
                        $(this).dialog("close");
                        $(cntrlID).data("kendoNumericTextBox").focus();
                    }
                }]
            });
            return false;
        }
        else {
            return true;
        }
    }
    return true;
}

function CheckDateWithFromToRange(FromDate, ToDate, format, MSG) {
    var From = $.trim($(FromDate).val()).split('/');
    var To = $.trim($(ToDate).val()).split('/');
    if (format == "dd/MM/yyyy" && From != "" && To != "") {
        var DtFrom = new Date(Number(From[2].substr(0, 4)), (Number(From[1]) - 1), Number(From[0]));
        var DtTo = new Date(Number(To[2].substr(0, 4)), (Number(To[1]) - 1), Number(To[0]));

        if (DtFrom > DtTo) {
            AlertDialog(ToDate, MSG);
            return false;
        }

    }
    return true;
}
function CheckDateWithFromToRange_Byvalue(FromDate, ToDate, format, MSG) {
    var From = $.trim(FromDate).split('/');
    var To = $.trim(ToDate).split('/');
    if (format == "dd/MM/yyyy" && From != "" && To != "") {
        var DtFrom = new Date(Number(From[2].substr(0, 4)), (Number(From[1]) - 1), Number(From[0]));
        var DtTo = new Date(Number(To[2].substr(0, 4)), (Number(To[1]) - 1), Number(To[0]));

        if (DtFrom > DtTo) {
            AlertDialog(ToDate, MSG);
            return false;
        }

    }
    return true;
}
function CheckEqualValue(Value1, Value2, MSG) {
    var Val1 = $.trim($(Value1).val());
    var Val2 = $.trim($(Value2).val());
    if (Val1 != Val2) {
        AlertDialog('', MSG);
        return false;
    }
    return true;
}
jQuery.fn.CheckMaxLength = function (Length, Msg) {
    return this.each(function () {
        $(this).keypress(function () {
            if ($.trim($(this).val()).length > Length - 1) {
                label('lblAlert', 'common', 'Alert');
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                            $(this).focus();
                        }
                    }]
                });
                $("#lblRequiredLayout").text(Msg);
                return false;
            }
        });
    });
}
function AlertDialog(cntrlID, Msg) {
    label('lblAlert', 'common', 'Alert');
    $("#lblRequiredLayout").html(Msg);
    $("#dialog-RequiredLayout").dialog({
        title: Title,
        resizable: false,
        height: 'auto',
        modal: true,
        buttons: [{
            text: GetLable('lblOK', 'common', 'OK'),
            click: function () {
                $(this).dialog("close");
                $(cntrlID).focus();
            }
        }]
    });
    return false;
}
function WarningDialog(Msg, FunctionName) {
    label('lblWarning', 'common', 'Warning');
    $("#dialog-RequiredLayout").dialog({
        title: Title,
        resizable: false,
        height: 170,
        modal: true,
        buttons: [{
            text: GetLable('lblOK', 'common', 'OK'),
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
    $("#lblRequiredLayout").text(Msg);
    return false;
}
function DialogConfirm(msg, FunctionName) {
    label('lblAlert', 'common', 'Alert');
    $("#dialog-confirmLayout").text(msg);
    $("#dialog-confirmLayout").dialog({
        title: Title,
        resizable: false,
        position: 'center',
        height: 150,
        width: 400,
        modal: true,
        buttons: [{
            text: GetLable('lblYes', 'common', 'Yes'),
            click: function () {
                $(this).dialog("close");
                if (FunctionName != undefined && FunctionName != "" && FunctionName != null)
                    FunctionName(true);
            }
        }, {
            text: GetLable('lblNo', 'common', 'No'),
            click: function () {
                $(this).dialog("close");
                if (FunctionName != undefined && FunctionName != "" && FunctionName != null)
                    FunctionName(false);
            }
        }]
    });
    //$('.ui-dialog').css({ position: "fixed" });
    return false;
}

function DialogConfirmWithoutEvent(msg, FunctionName) {
    label('lblAlert', 'common', 'Alert');
    $("#dialog-confirmLayout").text(msg);
    $("#dialog-confirmLayout").dialog({
        title: Title,
        closeOnEscape: false,
        resizable: false,
        position: 'center',
        height: 150,
        width: 400,
        modal: true,
        closeOnEscape: false,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        },
        buttons: [{
            text: GetLable('lblYes', 'common', 'Yes'),
            click: function () {
                $(this).dialog("close");
                if (FunctionName != undefined && FunctionName != "" && FunctionName != null)
                    FunctionName(true);
            }
        }, {
            text: GetLable('lblNo', 'common', 'No'),
            click: function () {
                $(this).dialog("close");
                if (FunctionName != undefined && FunctionName != "" && FunctionName != null)
                    FunctionName(false);
            }
        }]
    });
    //$('.ui-dialog').css({ position: "fixed" });
    return false;
}
function label(id, form, msg) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/getResourceLable'),
        data: JSON.stringify({ strLableId: id, strResourceFileName: form, DefaultLabelId: msg }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblRequiredLayout").text(data);
            Title = data;
        },
        error: function (xhr, ret, e) {
            $("#lblRequiredLayout").text(e);
            Title = e;
        }
    });
    return Title;
}
jQuery.fn.ForceDecimalWithMinus = function (value, precision) {
    return this.each(function () {
        $(this).keydown(function (event) {
            if (event.shiftKey) {
                event.preventDefault();
            }
            var key = event.charCode || event.keyCode || 0;
            if ((key == 189 || key == 109 || key == 173) && $(this).val() != '') { event.preventDefault(); }

            if (
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105) || (key >= 35 && key <= 40) ||
                key == 8 || key == 9 || key == 37 ||
                key == 39 || key == 46 || key == 110 || key == 190 || key == 189 || key == 109 || key == 173) {
            } else {
                event.preventDefault();
            }

            if ($(this).val().indexOf('.') !== -1 && (key == 190 || key == 110))
                event.preventDefault();

            if ($(this).val().length > value + precision + 1) {
                if (key == 8 || key == 9 || key == 37 || key == 39 || key == 46) { }
                else { event.preventDefault(); }
            }
            if ((key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105)) {

                var split = ($(this).val().split("."));
                if (split[1] != null && split[1] != "") {
                    //if (split[1].length == precision)
                    //    event.preventDefault();
                }
                else {
                    if ($(this).val().length == value)
                        event.preventDefault();
                }
            }
        });

        $(this).blur(function () {
            if ($(this).val() != '') {
                $(this).val(parseFloat($(this).val()).toFixed(precision));
                //$(this).val(($(this).val()).toFixed(precision));
            }
        });
    });
}

jQuery.fn.ForceNumericWithMinus = function () {
    return this.each(function () {
        $(this).keydown(function (event) {
            if (event.shiftKey) {
                event.preventDefault();
            }
            var key = event.charCode || event.keyCode || 0;
            if ((key == 189 || key == 109 || key == 173) && $(this).val() != '') { event.preventDefault(); }

            if (
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105) || (key >= 35 && key <= 40) ||
                key == 8 || key == 9 || key == 37 ||
                key == 39 || key == 46 || key == 110 || key == 190 || key == 189 || key == 109 || key == 173) {
            } else {
                event.preventDefault();
            }

            if ($(this).val().indexOf('.') == -1 && (key == 190 || key == 110))
                event.preventDefault();
        });

    });
}

jQuery.fn.DateValidationWithDuration = function (FYFrom, FYTo, Msg, AllowFutureDate, FutureDateMsg) {
    $(this).change(function () {
        var FYeardate = (FYFrom).split('/');
        var FYeartodate = (FYTo).split('/');
        var DocDate = $(this).val().split('/');
        var DtDocDate = new Date(Number(DocDate[2]), (Number(DocDate[1]) - 1), Number(DocDate[0]));
        var DtFyFrom = new Date(Number(FYeardate[2].substr(0, 4)), (Number(FYeardate[1]) - 1), Number(FYeardate[0]));
        var DtFyTo = new Date(Number(FYeartodate[2].substr(0, 4)), (Number(FYeartodate[1]) - 1), Number(FYeartodate[0]));
        var CurrDate = new Date();

        if (DtDocDate < DtFyFrom) {
            AlertDialog(this, Msg);
        }
        if (DtDocDate > DtFyTo) {
            AlertDialog(this, Msg);
        }

        if (AllowFutureDate == false) {
            if (DtDocDate > CurrDate) {
                AlertDialog(this, FutureDateMsg);
                $(this).val(CurrDate.getDate() + '/' + (CurrDate.getMonth() + 1) + '/' + CurrDate.getFullYear());
            }
        }
    });
}

function CheckFinancialDuration(ControlId, DocDate, FYFrom, FYTo, Message) {
    var FYeardate = (FYFrom).split('/');
    var FYeartodate = (FYTo).split('/');
    var DocDate = (DocDate).split('/');
    var DtDocDate = new Date(Number(DocDate[2]), (Number(DocDate[1]) - 1), Number(DocDate[0]));
    var DtFyFrom = new Date(Number(FYeardate[2].substr(0, 4)), (Number(FYeardate[1]) - 1), Number(FYeardate[0]));
    var DtFyTo = new Date(Number(FYeartodate[2].substr(0, 4)), (Number(FYeartodate[1]) - 1), Number(FYeartodate[0]));


    if (DtDocDate < DtFyFrom) {
        AlertDialog(ControlId, Message);
        return false;
    }
    if (DtDocDate > DtFyTo) {
        AlertDialog(ControlId, Message);
        return false;
    }
    return true;
}

function JSNumberFormat(Amount) {
    if (isNaN(Amount)) { return ""; }
    if ($.trim(jsIndianGST()).toUpperCase() == 'TRUE')
        return parseFloat(Amount).toFixed(GetNoOfDecimal()).replace(/\B(?=(\d{3})(((\d{2})+(?!\d)|(?!\d))))/g, ',');
    else
        return parseFloat(Amount).toFixed(GetNoOfDecimal()).replace(/\d(?=(\d{3})+\.)/g, '$&,');
}
function JSNumberFormatWithDecimalParam(Amount, DecimalPlaces) {
    if (isNaN(Amount)) { return ""; }
    if ($.trim(jsIndianGST()).toUpperCase() == 'TRUE') {
        //return parseFloat(parseFloat(Amount).toFixed(DecimalPlaces)).toString().replace(/\B(?=(\d{3})(((\d{2})+(?!\d)|(?!\d))))/g, ',');
        var abc = parseFloat(parseFloat(Amount).toFixed(4)).toString().split('.');
        if (abc.length > 1)
            return abc[0].replace(/\B(?=(\d{3})(((\d{2})+(?!\d)|(?!\d))))/g, ',') + '.' + abc[1];
        else
            return abc[0].replace(/\B(?=(\d{3})(((\d{2})+(?!\d)|(?!\d))))/g, ',');
    }
    else
        return parseFloat(parseFloat(Amount).toFixed(DecimalPlaces)).toString().replace(/\d(?=(\d{3})+\.)/g, '$&,');
}

function GetJSONListfromString(str) {
    str = str.replace(/&lt;/g, '<');                //Pattern in between /  /g-for global search
    str = str.replace(/&gt;/g, '>');

    str = str.replace(/{&quot;/g, '{"');
    str = str.replace(/&quot;}/g, '"}');
    str = str.replace(/&quot;:/g, '":');
    str = str.replace(/,&quot;/g, ',"');
    str = str.replace(/:&quot;/g, ':"');
    str = str.replace(/&quot;,/g, '",');
    str = str.replace(/&quot;/g, ' ');              //" DQ between Key|Value 
    str = str.replace(/&#201;/g, 'É');              //É, é (e-acute) 
    str = str.replace(/&amp;/g, '&');
    str = str.replace(/&amp;nbsp;/g, '&nbsp;');
    str = str.replace(/&#39;/g, '');
    str = str.replace(/&#177;/g, '±');
    str = str.replace(/&#195;/g, 'Ã');              //Ã, é (e-acute)
    str = str.replace(/&#160;/g, ' ');

    str = str.replace(/\\n/g, ' ');             //For Esacape Character \n to \\n
    str = str.replace(/\\r/g, ' ');
    str = str.replace(/\\t/g, ' ');
    str = str.replace(/\\/g, '\\\\');               //Character \ with \\  i.e. \n \\n, \r \\r, \c \\c
    str = JSON.stringify(str);                      //Append \ before ", CRLF to \n, \n to \\n, Any \\ to \\\\
    str = str.replace(/\\\\n/g, '\\n');             //For Esacape Character \n (User Input)
    str = str.replace(/\\\\r/g, '\\r');             //For Esacape Character \r (User Input)
    str = str.replace(/\\\\t/g, '\\t');             //For Esacape Character \t (User Input)
    str = str.replace(/\\n/g, '\\\\n');             //For Esacape Character \n to \\n
    str = str.replace(/\\r/g, '\\\\r');
    str = str.replace(/\\t/g, '\\\\t');
    str = $.parseJSON($.trim(str));                 // Remove \ before ",\\n to \n,\\\\C to \\C

    var JsonList = $.parseJSON($.trim(str));        //\\C to \C,\\n to \n    
    return JsonList;
}
function GetJSONListfromModelstring(str) {
    str = str.replace(/&lt;/g, '<');
    str = str.replace(/&gt;/g, '>');
    str = str.replace(/&quot;/g, '"');
    str = str.replace(/&amp;nbsp;/g, '&nbsp;');
    return str;
}

function AddDaystoDate(Date1, Days, format) {
    var isvalid = true;
    var NewDate = "";
    if (Date1 != '') {
        if (format == "dd/MM/yyyy") {
            var date = Date1.split("/");
            if (date.length == 3) {
                var $Day = date[0];
                var $Month = date[1];
                var $Year = date[2];
                if ($Month.length == "2") { $Month = $Month.replace("0", ""); }

                if (parseInt($Month) > 12) { isvalid = false; }
                if (parseInt($Year.length) != 4) { isvalid = false; }
                if (parseInt($Day.length) > 2) { isvalid = false; }
                else {
                    if ($Month == "1" || $Month == "3" || $Month == "5" || $Month == "7" || $Month == "8" || $Month == "10" || $Month == "12") {
                        if (parseInt($Day) > 31) { isvalid = false; }
                    }
                    else if ($Month == "4" || $Month == "6" || $Month == "9" || $Month == "11") {
                        if (parseInt($Day) > 30) { isvalid = false; }
                    }
                    else if ($Month == "2") {
                        if ((parseInt($Year) % 4 == 0 && parseInt($Year) % 100 != 00) || parseInt($Year) % 400 == 0) {
                            if (parseInt($Day) > 29) { isvalid = false; }
                        }
                        else {
                            if (parseInt($Day) > 28) { isvalid = false; }
                        }
                    }
                }
            }
            else
                isvalid = false;
        }

    }
    if (isvalid == false) {
        return ""
        return false;
    }
    else {

        var SplitDate = (Date1).split('/');
        var dateformatdt = Number(SplitDate[1].substr(0, 2)) + '/' + Number(SplitDate[0].substr(0, 2)) + '/' + Number(SplitDate[2].substr(0, 4));
        var newdate = new Date(dateformatdt);
        newdate.setDate(newdate.getDate() + parseInt(Days));
        var dd = newdate.getDate();
        var mm = newdate.getMonth() + 1;
        var yy = newdate.getFullYear();
        var newdateobtained = (dd.toString().length == 2 ? dd : '0' + dd) + '/' + (mm.toString().length == 2 ? mm : '0' + mm) + '/' + yy;
        return newdateobtained;
    }
}

function GetCurrentDate(me) {
    var fullDate = new Date();
    var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : '0' + (fullDate.getMonth() + 1);
    var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
    if (!$(me).val()) { // if textbox value is blank or empty
        $(me).datepicker("setDate", currentDate);
    }
}

function CheckUserPermission(ControllerName, TransactionMode, prevQuerystring, TenantId) {
    var IsValid = false;
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckUserPermissions'),
        data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: TransactionMode, prevQuerystring: prevQuerystring, TenantId: TenantId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data == "0") {
                $('<div title="Alert"></div>').dialog({
                    open: function (event, ui) {
                        $(this).html('Access denied. You do not have permission to perform this operation');

                    },
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                        }
                    }]
                });
                IsValid = false;
            }
            else {
                IsValid = true;
            }
        }
    });
    return IsValid;
}

function CheckUserPermission_WithoutDialog(ControllerName, TransactionMode, prevQuerystring, TenantId) {
    var flag = false;
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckUserPermissions'),
        data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: TransactionMode, prevQuerystring: prevQuerystring, TenantId: TenantId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data == "1") {
                flag = true;
            }

        },
        error: function (xhr, ret, e) {
            return false;
        }

    });
    return flag;
}

function CheckUserPermissions_BackDateEntry(TransactionName, DocDate) {
    var IsValid = false;
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckUserPermissions_BackDateEntry'),
        data: JSON.stringify({ TransactionName: TransactionName, DocDate: DocDate }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == "1") {
                IsValid = true;
            }
            else {
                IsValid = false;
            }
        }
    });
    return IsValid;
}

function JSCheckCostCenterAllocation(AccountId) {
    var IsValid = false;
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Account/CheckCostCenterReference'),
        data: JSON.stringify({ AccId: AccountId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            IsValid = data;
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
    return IsValid;
}

function AutoCopyToAnotherControl(FromControlId, ToControlId, ControlType) {
    $(FromControlId).keyup(function (event) {
        var stt = $(this).val();
        if (ControlType == "textbox") {
            $(ToControlId).val(stt);
            $(ToControlId).change();
        }
        else if (ControlType == "label") {
            $(ToControlId).val(stt);
        }
    });
}

jQuery.fn.DoNotAllowSpecialCharacter = function () {

    return this.each(function () {
        $(this).keydown(function (e) {

            if (e.shiftKey || e.ctrlKey || e.altKey) {
                return (e.preventDefault());

            } else {
                var key = e.keyCode;
                if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                    return (e.preventDefault());
                }
            }

        });
    });
}
jQuery.fn.TimeFormatAllowNumeric = function () {
    return this.each(function () {
        $(this).keyup(function (e) {
            var $eval = $("#" + e.currentTarget.id).val();
            var len = parseInt($eval.length);
            if (len == 2) {
                $("#" + e.currentTarget.id).val($("#" + e.currentTarget.id).val() + ":")
            }

        });

        $(this).keydown(function (e) {

            if (e.shiftKey || e.ctrlKey || e.altKey) {
                return (e.preventDefault());

            } else {
                var key = e.charCode || e.keyCode || 0;
                return (
                    key == 8 ||
                    key == 9 ||
                    key == 46 ||
                    key == 110 ||
                    key == 190 ||
                    (key >= 35 && key <= 40) ||
                    (key >= 48 && key <= 57) ||
                    (key >= 96 && key <= 105));
            }
        });
    });
}

jQuery.fn.DateAddSlac = function () {

    return this.each(function () {
        $(this).keypress(function (e) {
            var $eval = $("#" + e.currentTarget.id).val();
            var len = parseInt($eval.length);
            if (len == 2 || len == 5) {
                $("#" + e.currentTarget.id).val($("#" + e.currentTarget.id).val() + "/")
            }
        });
        $(this).keydown(function (e) {
            if (e.shiftKey || e.ctrlKey || e.altKey) {
                return (e.preventDefault());
            }
            else {
                var key = e.charCode || e.keyCode || 0;
                return (
                    key == 8 ||
                    key == 9 ||
                    key == 46 ||
                    key == 110 ||
                    key == 190 ||
                    (key >= 35 && key <= 40) ||
                    (key >= 48 && key <= 57) ||
                    (key >= 96 && key <= 105));
            }
        });
    });
}

jQuery.fn.ForceDecimalOnlyWithPrecision = function (value, precision) {
    return this.each(function () {
        $(this).keydown(function (event) {

            if (event.shiftKey) {
                event.preventDefault();
            }
            var key = event.charCode || event.keyCode || 0;


            if (
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105) || (key >= 35 && key <= 40) ||
                key == 8 || key == 9 || key == 37 ||
                key == 39 || key == 46 || key == 110 || key == 190) {
            } else {
                event.preventDefault();
            }

            if ($(this).val().indexOf('.') !== -1 && (key == 190 || key == 110))
                event.preventDefault();

            if ($(this).val().length > value + precision + 1) {
                if (key == 8 || key == 9 || key == 37 || key == 39 || key == 46) { }
                else { event.preventDefault(); }
            }
            if ((key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105)) {

                var split = ($(this).val().split("."));
                if (split[1] != null && split[1] != "") {
                    if (split[1].length == precision && split[0].length >= value)
                        event.preventDefault();
                }
                else {
                    if (split[0].length >= value && $(this).val().indexOf('.') == -1)
                        event.preventDefault();
                }
            }

        });

        //$(this).blur(function () {
        //    if ($(this).val() != '') {
        //        // $(this).val(parseFloat($(this).val()).toFixed(precision));
        //        $(this).val(($(this).val()).toFixed(precision));
        //    }
        //});
    });
}

//To allow only alphabetic characters
$('.alphaonly').bind('keyup blur', function () {
    var node = $(this);
    node.val(node.val().replace(/[^a-zA-Z]/g, ''));
});

$('.alphanumericonly').bind('keyup blur', function () {
    var node = $(this);
    node.val(node.val().replace(/[^a-zA-Z0-9]/g, ''));
});

// user cant add more then 100 %
$(".pertext").change(function () {

    if (parseFloat($(this).val()) > 100) {
        $(this).val("");
        AlertDialog("", "Percentage can't add more then 100.");
    }

});

function merge_JSList(obj1, obj2) {
    var finalObj = obj1.concat(obj2);
    return finalObj;
}

jQuery.fn.CheckNumberRange = function (FromValue, ToValue, Msg) {
    return this.each(function () {
        $(this).focusout(function () {
            if (parseFloat($.trim($(this).val())) < FromValue || parseFloat($.trim($(this).val())) > ToValue) {
                var ControlId = this.id;
                label('lblAlert', 'common', 'Alert');
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                            $("#" + ControlId).focus();
                        }
                    }]
                });
                $("#lblRequiredLayout").text(Msg);
                return false;
            }
        });
    });
}

function DocumentApprove(DocTypeCode, DocId, DocNo, redirectToList, TenantId) {
    var IsValid = false;

    if (CheckExistance_DocumentApproval(DocTypeCode, DocId, DocNo, TenantId)) {
        $("#DivApproveDiscription").dialog({
            title: 'Remarks',
            resizable: false,
            height: 230,
            width: 400,
            modal: true,
            buttons: [{
                text: GetLable('lblYes', 'common', 'Yes'),
                click: function () {
                    $(this).dialog("close");
                    $.ajax({
                        cache: false,
                        type: "POST",
                        async: false,
                        url: ITX3ResolveUrl('Common/DocumentApprove'),
                        data: JSON.stringify({ DocTypeCode: DocTypeCode, DocId: DocId, Remarks: $("#ApproveDiscription").val(), TenantId: TenantId }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data == "0") {
                                $("#dialog-MsgBox").delay(10).fadeIn();
                                $("#lblMsgBox").text(DocNo + " Not Approved.");
                                $("#dialog-MsgBox").delay(4000).fadeOut();
                                IsValid = false;
                            }
                            else {
                                $("#dialog-MsgBox").delay(10).fadeIn();
                                $("#lblMsgBox").text(DocNo + " Approved Successfully.");
                                $("#dialog-MsgBox").delay(4000).fadeOut();
                                IsValid = true;
                                if (redirectToList != undefined && redirectToList != "") {
                                    redirectToLists(DocTypeCode, 'Approved');
                                }
                            }
                        }
                    });
                }
            }, {
                text: GetLable('lblNo', 'common', 'No'),
                click: function () {
                    $(this).dialog("close");
                    return false;
                }
            }]
        });
    }
    else {
        AlertDialog("", GetLable('lblCanNotApproveDoc', 'common', 'You can approve only Unapproved document.'))
    }
    return IsValid;
}


function DocumentSentBack(DocTypeCode, DocId, DocNo, redirectToList, TenantId) {
    var IsValid = false;

    if (CheckExistance_DocumentApproval(DocTypeCode, DocId, DocNo, TenantId)) {
        $("#DivSentBackDiscription").dialog({
            title: 'Remarks',
            resizable: false,
            height: 230,
            width: 400,
            modal: true,
            buttons: [{
                text: GetLable('lblYes', 'common', 'Yes'),
                click: function () {
                    if ($("#SentBackDiscription").val() != "") {
                        $(this).dialog("close");
                        $.ajax({
                            cache: false,
                            type: "POST",
                            async: false,
                            url: ITX3ResolveUrl('Common/DocumentSentBack'),
                            data: JSON.stringify({ DocTypeCode: DocTypeCode, DocId: DocId, Remarks: $("#SentBackDiscription").val(), TenantId: TenantId }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data == "0") {
                                    $("#dialog-MsgBox").delay(10).fadeIn();
                                    $("#lblMsgBox").text(DocNo + " Not Sent Back.");
                                    $("#dialog-MsgBox").delay(4000).fadeOut();
                                    IsValid = false;
                                }
                                else {
                                    $("#dialog-MsgBox").delay(10).fadeIn();
                                    $("#lblMsgBox").text(DocNo + " Sent Back Successfully.");
                                    $("#dialog-MsgBox").delay(4000).fadeOut();
                                    IsValid = true;
                                    if (redirectToList != undefined && redirectToList != "") {
                                        redirectToLists(DocTypeCode, 'SendBack');
                                    }
                                }
                            }
                        });
                    }
                    else {
                        AlertDialog("#SentBackDiscription", GetLable('lblEnterRemarks', 'common', 'Please Enter Remarks.'))
                    }
                }
            }, {
                text: GetLable('lblNo', 'common', 'No'),
                click: function () {
                    $("#SentBackDiscription").val("");
                    $('#SendBackItemTrans input:checkbox').prop("checked", false);
                    $(this).dialog("close");
                    return false;
                }
            }]
        });
    }
    else {
        AlertDialog("", GetLable('lblYoucannotsentbackdoc', 'common', 'You can sent back only Unapproved document.'))
    }
    return IsValid;
}


function DocumentReject(DocTypeCode, DocId, DocNo, redirectToList) {
    var IsValid = false;

    if (CheckExistance_DocumentApproval(DocTypeCode, DocId, DocNo)) {
        $("#DivRejectDiscription").dialog({
            title: 'Remarks',
            resizable: false,
            height: 230,
            width: 400,
            modal: true,
            buttons: [{
                text: GetLable('lblYes', 'common', 'Yes'),
                click: function () {
                    if ($("#RejectDiscription").val() != "") {
                        $(this).dialog("close");
                        $.ajax({
                            cache: false,
                            type: "POST",
                            async: false,
                            url: ITX3ResolveUrl('Common/DocumentReject'),
                            data: JSON.stringify({ DocTypeCode: DocTypeCode, DocId: DocId, Remarks: $("#RejectDiscription").val() }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data == "0") {
                                    $("#dialog-MsgBox").delay(10).fadeIn();
                                    $("#lblMsgBox").text(DocNo + " Not Rejected.");
                                    $("#dialog-MsgBox").delay(4000).fadeOut();
                                    IsValid = false;
                                }
                                else {
                                    $("#dialog-MsgBox").delay(10).fadeIn();
                                    $("#lblMsgBox").text(DocNo + " Rejected Successfully.");
                                    $("#dialog-MsgBox").delay(4000).fadeOut();
                                    IsValid = true;
                                    if (redirectToList != undefined && redirectToList != "") {
                                        redirectToLists(DocTypeCode, 'Rejected');
                                    }
                                }
                            }
                        });
                    }
                    else {
                        AlertDialog("#RejectDiscription", GetLable('lblEnterRemarks', 'common', 'Please Enter Remarks.'))
                    }
                }
            }, {
                text: GetLable('lblNo', 'common', 'No'),
                click: function () {
                    $(this).dialog("close");
                    return false;
                }
            }]


        });
    }
    else {
        AlertDialog("", GetLable('lblYoucannotRejectdoc', 'common', 'You can reject only Unapproved document.'))
    }
    return IsValid;
}


function CheckExistance_DocumentApproval(DocTypeCode, DocId, DocNo, TenantId) {
    var IsExist = false;

    if (DocTypeCode == "PO" || DocTypeCode == "SO" || DocTypeCode == "JO") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "OrderMaster", " AND Id='" + DocId + "' AND OrderTypeCode='" + DocTypeCode + "'", "", false, false, TenantId) == 1) {
            IsExist = true;
            DocNo = DocTypeCode + " No. " + DocNo;
        }
    }
    else if (DocTypeCode == "PI") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "InventoryVoucherMaster", " AND Id='" + DocId + "' AND InvTypeCode='" + DocTypeCode + "'", "", false, false, TenantId) == 1) {
            IsExist = true;
            DocNo = DocTypeCode + " No. " + DocNo;
        }
    }
    else if (DocTypeCode == "SQ" || DocTypeCode == "PQ") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "QuotationMaster", " AND Id='" + DocId + "' AND QuotationTypeCode='" + DocTypeCode + "' ") == 1) {
            IsExist = true;
            DocNo = DocTypeCode + " No. " + DocNo;
        }
    }
    else if (DocTypeCode == "SR") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "LogInventoryMaster", " AND Id='" + DocId + "' AND InvTypeCode='" + DocTypeCode + "' ") == 1) {
            IsExist = true;
            DocNo = DocTypeCode + " No. " + DocNo;
        }
    }
    else if (DocTypeCode == "PRDIndent") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "PRDIndentMaster", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Indent No. " + DocNo;
        }
    }
    else if (DocTypeCode == "TTR") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "StockTransferRequestMaster", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Stock Transfer Req. No. " + DocNo;
        }
    }
    else if (DocTypeCode == "PurRequisition") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "PurRequisitionMaster", " AND Id='" + DocId + "'", "", false, false, TenantId) == 1) {
            IsExist = true;
            DocNo = "Pur. Requisition No. " + DocNo;
        }
    }
    else if (DocTypeCode == "PRDPlanningRequest") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "PRDPlanningRequestMaster", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Production Planning Request No. " + DocNo;
        }
    }
    else if (DocTypeCode == "PRDPO") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "PRDPOMaster", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Production Order No. " + DocNo;
        }
    }
    else if (DocTypeCode == "BOM") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "PRDBOMMaster", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "BOM No. " + DocNo;
        }
    }
    else if (DocTypeCode == "AdBOM") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "AdBOMMaster", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Adv. BOM " + DocNo;
        }
    }
    else if (DocTypeCode == "AdPRDPO") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "AdPRDPOMaster", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Adv. Production Order No. " + DocNo;
        }
    }
    else if (DocTypeCode == "SALESPLAN") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "SalesExecutivePlan", " AND RequestId='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Sales Plan ";
        }
    }
    else if (DocTypeCode == "CreLim") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "CreditLimitPeriodRequest", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Credit Limit";
        }
    }
    else if (DocTypeCode == "PAYREQ") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "PaymentRequestMaster", " AND Id='" + DocId + "'", "", false, false, TenantId) == 1) {
            IsExist = true;
            DocNo = "Payment Request No. " + DocNo;
        }
    }
    else if (DocTypeCode == "PHYINV") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "PhysicalInventoryMaster", " AND Id='" + DocId + "'", "", false, false, TenantId) == 1) {
            IsExist = true;
            DocNo = "Physical Inventory No. " + DocNo;
        }
    }
    else if (DocTypeCode == "ESSAR") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatus", "ESSAttendanceRegularization", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSLR") {
        if (CheckExistance_WithoutDialog("05", "Status", "ESSLeaveRequest", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSATR") {
        if (CheckExistance_WithoutDialog("05", "Status", "HREmpAdvanceRequest", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSTR") {
        if (CheckExistance_WithoutDialog("05", "RequestStatus", "HRTravelRequestMaster", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSCR" || DocTypeCode == "HRCR") {
        if (CheckExistance_WithoutDialog("05", "Status", "HRClaimRequestMaster", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSAST") {
        if (CheckExistance_WithoutDialog("05", "Status", "HREmpAssetAssign", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSRENTR") {
        if (CheckExistance_WithoutDialog("05", "Status", "ESSRentReceipt", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSASRT") {
        if (CheckExistance_WithoutDialog("05", "Status", "HREmpReturnAsset", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSIndent") {
        if (CheckExistance_WithoutDialog("05", "Status", "HREmployeeIndent", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSTDS") {
        if (CheckExistance_WithoutDialog("05", "Status", "ESSTDSDeclarationMast", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "Appraisal") {
        if (CheckExistance_WithoutDialog("05", "Status", "HRAppraisal", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSELR") {
        if (CheckExistance_WithoutDialog("05", "Status", "HREmpEarlyLeaveRequest", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSCOMPOFF") {
        if (CheckExistance_WithoutDialog("05", "Status", "HRCompOffCredit", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ASSETCOMPLAINT") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "AssetComplaintsMaster", " AND Id='" + DocId + "'") == 1) {
            IsExist = true;
            DocNo = "Asset Complaints " + DocNo;
        }
    }
    else if (DocTypeCode == "ESSPITEXE") {
        if (CheckExistance_WithoutDialog("05", "Status", "ESSPITExemptionMaster", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSOHR") {
        if (CheckExistance_WithoutDialog("05", "Status", "HROptionalHolidayRequest", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "HRPFLR") {
        if (CheckExistance_WithoutDialog("05", "Status", "HRPFLoanRequestMaster", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSLCR") {
        if (CheckExistance_WithoutDialog("05", "Status", "HRLeaveCancellationRequest", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSRR") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatus", "HREmpSeparation", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "HREMR") {
        if (CheckExistance_WithoutDialog("05", "Status", "HREmployeeMovement", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSFV") {
        if (CheckExistance_WithoutDialog("05", "Status", "ESSFieldVisit", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSAFVP") {
        if (CheckExistance_WithoutDialog("05", "Status", "ESSAnnualFieldVisitPlanMaster", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSMFVP") {
        if (CheckExistance_WithoutDialog("05", "Status", "ESSMonthlyFieldVisitPlanMaster", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "ESSUPDATEPROFILE") {
        if (CheckExistance_WithoutDialog("05", "Status", "ESSEmployeeMaster", " AND Id='" + DocId + "'", "", false, true) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "LPClub") {
        if (CheckExistance_WithoutDialog("05", "ApprovalStatusCode", "ClubMemberRewardPoints", " AND Id='" + DocId + "'", "", false, false, TenantId) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "HRCONFIRREQ") {
        if (CheckExistance_WithoutDialog("05", "Status", "HREmployeeCofirmationRequest", " AND Id='" + DocId + "'", "", false, false, TenantId) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "SOPTemplate") {
        if (CheckExistance_WithoutDialog("05", "Status", "HRSOPTemplateMaster", " AND Id='" + DocId + "'", "", false, false, TenantId) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "AssignPermissions") {
        if (CheckExistance_WithoutDialog("05", "Status", "TmpRolePermissions", " AND Id='" + DocId + "'", "", false, true, TenantId) == 1) {
            IsExist = true;
        }
    }
    else if (DocTypeCode == "AssignRoles") {
        if (CheckExistance_WithoutDialog("05", "Status", "TmpUserRoles", " AND Id='" + DocId + "'", "", false, true, TenantId) == 1) {
            IsExist = true;
        }
    }
    return IsExist;
}

function redirectToLists(DocTypeCode, Status) {

    if (DocTypeCode == "PO" || DocTypeCode == "SO" || DocTypeCode == "PI" || DocTypeCode == "SR" || DocTypeCode == "PRDIndent" || DocTypeCode == "PurRequisition" || DocTypeCode == "PRDPlanningRequest" || DocTypeCode == "PRDPO" || DocTypeCode == "AdBOM" || DocTypeCode == "AdPRDPO" || DocTypeCode == "SQ" || DocTypeCode == "PQ" || DocTypeCode == "TTR" || DocTypeCode == "SALESPLAN" || DocTypeCode == "CreLim" || DocTypeCode == "JO" || DocTypeCode == "PAYREQ" || DocTypeCode == "PHYINV" || DocTypeCode == "ASSETCOMPLAINT" || DocTypeCode == "LPClub") {
        location.href = ITX3ResolveUrl("rptPendingApprovalDetails?DocTypeCode=" + DocTypeCode + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSLR") {
        location.href = ITX3ResolveUrl("ESS/LeaveRequest/ListLeaveApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSAR") {
        location.href = ITX3ResolveUrl("ESS/AttendanceRegularization/ListAttendanceApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSATR") {
        location.href = ITX3ResolveUrl("ESS/ESSHRAdvanceRequest/ESSAdvanceRequestApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSTR") {
        location.href = ITX3ResolveUrl("ESS/ESSTravelRequest/ListESSTravelApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSCR" || DocTypeCode == "HRCR") {
        location.href = ITX3ResolveUrl("ESS/ESSHREmpClaimRequest/ESSHREmpClaimRequestApproval?CallFrom=" + Status + "&StatusCode=05&DocTypeCode=" + DocTypeCode);
    }
    else if (DocTypeCode == "ESSAST") {
        location.href = ITX3ResolveUrl("ESS/ESSHREmpAssetAssign/ListESSAssetApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSRENTR") {
        location.href = ITX3ResolveUrl("ESS/ESSRentReceipt/ListApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSASRT") {
        location.href = ITX3ResolveUrl("ESS/ESSHREmpReturnAsset/ListESSReturnAssetApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSIndent") {
        location.href = ITX3ResolveUrl("ESS/ESSHREmployeeIndent/ESSMyIndentApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSTDS") {
        location.href = ITX3ResolveUrl("ESS/ESSTDSDeclaration/TDSApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "Appraisal") {
        location.href = ITX3ResolveUrl("ESS/ESSAppraisalApproval/ESSAppraisalApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSELR") {
        location.href = ITX3ResolveUrl("ESS/ESSEarlyLeave/ListEarlyLeaveApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSCOMPOFF") {
        location.href = ITX3ResolveUrl("ESS/ESSHRCompOffCredit/ListCompOffCreditApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSPITEXE") {
        location.href = ITX3ResolveUrl("ESS/ESSPITExemption/PITApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSOHR") {
        location.href = ITX3ResolveUrl("ESS/ESSOptionalHolidayRequest/ListHolidayApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "HRPFLR") {
        location.href = ITX3ResolveUrl("HRMS/HRPFLoanRequest/ListPFLoanApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSLCR") {
        location.href = ITX3ResolveUrl("ESS/ESSLeaveCancellationRequest/ListLeaveCancellationApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSRR") {
        location.href = ITX3ResolveUrl("ESS/ESSResignationRequest/ListResignationApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "HREMR") {
        location.href = ITX3ResolveUrl("HRMS/HREmployeeMovement/ListEmployeeMovementApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSFV") {
        location.href = ITX3ResolveUrl("ESS/ESSFieldVisit/ListFieldVisitApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSAFVP") {
        location.href = ITX3ResolveUrl("ESS/ESSAnnualFieldVisitPlan/ListFieldVisitApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSMFVP") {
        location.href = ITX3ResolveUrl("ESS/ESSMonthlyFieldVisitPlan/ListFieldVisitApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "ESSUPDATEPROFILE") {
        location.href = ITX3ResolveUrl("ESS/ESSHRUpdateMyProfile/ListUpdateMyProfileApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "HRCONFIRREQ") {
        location.href = ITX3ResolveUrl("HRMS/EmployeeConfirmationRequest/ListConfirmationApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "SOPTemplate") {
        location.href = ITX3ResolveUrl("HRMS/HRSOPTemplate/ListSOPTemplateApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "AssignPermissions") {
        location.href = ITX3ResolveUrl("Roles/ListAssignPermissionApproval?CallFrom=" + Status + "&StatusCode=05");
    }
    else if (DocTypeCode == "AssignRoles") {
        location.href = ITX3ResolveUrl("User/ListAssignRolesApproval?CallFrom=" + Status + "&StatusCode=05");
    }
}

function GetDateRangeAsPerFilterType(ControlId, FromDateId, ToDateId) {
    var FromDate = new Date();
    var ToDate = new Date();
    var CurrentDate = new Date();
    var DateFilterType = $(ControlId).val();
    if (DateFilterType != "") {
        if (DateFilterType == "TW") {
            FromDate.setDate(CurrentDate.getDate() - CurrentDate.getDay() + 1);
            ToDate.setDate(CurrentDate.getDate() + (7 - CurrentDate.getDay()));
        }
        if (DateFilterType == "YD") {
            FromDate.setDate(CurrentDate.getDate() - 1);
            ToDate.setDate(CurrentDate.getDate() - 1);
        }
        else if (DateFilterType == "TM") {
            FromDate = new Date(CurrentDate.getFullYear(), CurrentDate.getMonth(), 01);
            ToDate = new Date(CurrentDate.getFullYear(), CurrentDate.getMonth() + 1, 01);
            ToDate.setDate(ToDate.getDate() - 1);
        }
        else if (DateFilterType == "TY") {
            FromDate = new Date(CurrentDate.getFullYear(), 0, 01);
            ToDate = new Date(CurrentDate.getFullYear(), 11, 31);
        }
        else if (DateFilterType == "FY") {
            var FiscalFromYear, FiscalToYear;
            if (FromDate.getMonth() < 4) {
                FiscalFromYear = FromDate.getFullYear() - 1;
                FiscalToYear = FromDate.getFullYear();
            }
            else {
                FiscalFromYear = FromDate.getFullYear();
                FiscalToYear = FromDate.getFullYear() + 1;
            }
            FromDate = new Date(FiscalFromYear, 03, 01);
            ToDate = new Date(FiscalToYear, 02, 31);
        }
    }
    $(FromDateId).val(('0' + FromDate.getDate()).slice(-2) + '/' + ('0' + (FromDate.getMonth() + 1)).slice(-2) + '/' + FromDate.getFullYear());
    $(ToDateId).val(('0' + ToDate.getDate()).slice(-2) + '/' + ('0' + (ToDate.getMonth() + 1)).slice(-2) + '/' + ToDate.getFullYear());
}

function AddGoBack() {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/PushGoBackCriteria'),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.result == "1") {
                return true;
            }
            else {
                return false;
            }
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
}

function GetGoBackUrl() {
    var url = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/GetGoBackURL'),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.result == "1") {
                url = data.url;
            }
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
    return url;
}

function lnkGoBackClick() {
    url = GetGoBackUrl();
    if (url != "") {
        url = url.replace(/amp;/g, '');
        $(location).attr('href', url + '?CallFrom=GB');
    }
}
function IsSessionExpire() {
    var flag = false;
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckSessionExpire'),
        data: "",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.IsSessionTimeOut) {
                alert('Session is expired.')
                window.location.href = ITX3ResolveUrl('Login/Index');
                flag = true;
            } else {
                flag = false;
            }
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
    return flag;
}

function CheckDuplicatesBatchNoInSameItems(BatchSerialTrans, Temp_BatchSerialTrans) {
    var _BSDuplicateflag = true;
    //**** Code For Same items having duplicates batch No with diffrent Attributes
    for (var a = 0; a < BatchSerialTrans.length; a++) {

        if (BatchSerialTrans[a].ItemId == Temp_BatchSerialTrans.ItemId && BatchSerialTrans[a].BatchNo == Temp_BatchSerialTrans.BatchNo) {
            var TMP_AttrList = Temp_BatchSerialTrans.lstItemBSAttribute;
            var EXT_AttrList = BatchSerialTrans[a].lstItemBSAttribute;
            if (EXT_AttrList != [] && EXT_AttrList != null && EXT_AttrList != undefined && EXT_AttrList.length > 0 && TMP_AttrList != [] && TMP_AttrList != null && TMP_AttrList != undefined && TMP_AttrList.length > 0) {
                for (var e = 0; e < TMP_AttrList.length; e++) {
                    var TMP_AttrId = TMP_AttrList[e].AttributeId;
                    var TMP_AttrVal = TMP_AttrList[e].Value;
                    for (var t = 0; t < EXT_AttrList.length; t++) {
                        var EXT_AttrId = EXT_AttrList[t].AttributeId;
                        var EXT_AttrVal = EXT_AttrList[t].Value;
                        if (TMP_AttrId == EXT_AttrId && TMP_AttrVal != EXT_AttrVal) {
                            AlertDialog("", "Found same items with same batch No. " + Temp_BatchSerialTrans.BatchNo + " having different attributes details.");
                            _BSDuplicateflag = false;
                        }
                    }
                }
            }
        }
    }
    //*************
    return _BSDuplicateflag;
}

function GetAttributesDet(LineNo) {
    var attr = "";
    if (BatchSerialMast[0][LineNo] != undefined && BatchSerialMast[0][LineNo] != [] && BatchSerialMast[0][LineNo] != null && BatchSerialMast[0][LineNo][0] != undefined && BatchSerialMast[0][LineNo].length > 0) {
        if (BatchSerialMast[0][LineNo][0].AttributeDet != undefined && BatchSerialMast[0][LineNo][0].AttributeDet != null && BatchSerialMast[0][LineNo][0].AttributeDet != "")
            attr = "<br /><b>Attr : </b>" + BatchSerialMast[0][LineNo][0].AttributeDet;
    }
    return attr;
}
function SetAttrDetInGrid(LineNo) {
    if (BatchSerialMast[0][LineNo][0] != undefined && BatchSerialMast[0][LineNo][0].AttributeDet != undefined && BatchSerialMast[0][LineNo][0].AttributeDet != null && BatchSerialMast[0][LineNo][0].AttributeDet != "")
        $("#ItemAttr" + LineNo).html("<br /><b>Attr : </b>" + BatchSerialMast[0][LineNo][0].AttributeDet);
}
function GetItemAttributesDet(LineNo) {
    var attr = "";
    if (ItemAttributesList[LineNo] != undefined && ItemAttributesList[LineNo] != [] && ItemAttributesList[LineNo] != null && ItemAttributesList[LineNo] != "" && ItemAttributesList[LineNo].length > 0) {
        attr = "<br /><b>Attr : </b>";
        for (var i = 0; i < ItemAttributesList[LineNo].length; i++) {
            attr = attr + ItemAttributesList[LineNo][i].Value + ", ";
        }
        attr = attr.substring(0, attr.length - 2);
    }
    return attr;
}
function SetItemAttributesDet(LineNo) {
    $("#ItemAttr" + LineNo).html(GetItemAttributesDet(LineNo));
}
function GetItemAttributesDetailsFromList(AttributeList, LineNo) {
    var attr = "";
    if (AttributeList[LineNo] != undefined && AttributeList[LineNo] != [] && AttributeList[LineNo] != null && AttributeList[LineNo] != "" && AttributeList[LineNo].length > 0) {
        attr = "<br /><b>Attr : </b>";
        for (var i = 0; i < AttributeList[LineNo].length; i++) {
            attr = attr + AttributeList[LineNo][i].Value + ", ";
        }
        attr = attr.substring(0, attr.length - 2);
    }
    return attr;
}

function GetItemAttributesDetails(AttributeDetails) {
    var attr = "";
    if (AttributeDetails != undefined && AttributeDetails != null && AttributeDetails != "") {
        attr = "<br /><b>Attr : </b>";
        attr = attr + AttributeDetails;
    }
    return attr;
}

function GetServerDate() {
    var Date = '';
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/GetServerDate'),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.result == "1") {
                Date = data.Date;
            }
            else {
                Date = '';
            }
        },
        error: function (xhr, ret, e) {
            Date = '';
        }
    });
    return Date;
}

function DocumentApproveByAdmin(DocTypeCode, DocId, DocNo, redirectToList, ApprovalQueueId, TenantId) {

    var IsValid = false;

    if (CheckExistance_DocumentApproval(DocTypeCode, DocId, DocNo, TenantId)) {
        $("#DivApproveDiscription").dialog({
            title: 'Remarks',
            resizable: false,
            height: 230,
            width: 400,
            modal: true,
            buttons: [{
                text: GetLable('lblYes', 'common', 'Yes'),
                click: function () {
                    $(this).dialog("close");
                    $.ajax({
                        cache: false,
                        type: "POST",
                        async: false,
                        url: ITX3ResolveUrl('HRApprovalQueueESS/ApproveByAdmin'),
                        data: JSON.stringify({ DocTypeCode: DocTypeCode, DocId: DocId, Remarks: $("#ApproveDiscription").val(), TenantId: TenantId, ApprovalQueueId: ApprovalQueueId }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data == "0") {
                                $("#dialog-MsgBox").delay(10).fadeIn();
                                $("#lblMsgBox").text(DocNo + " Not Approved.");
                                $("#dialog-MsgBox").delay(4000).fadeOut();
                                IsValid = false;
                            }
                            else {
                                $("#dialog-MsgBox").delay(10).fadeIn();
                                $("#lblMsgBox").text(DocNo + " Approved Successfully.");
                                $("#dialog-MsgBox").delay(4000).fadeOut();
                                IsValid = true;
                                $("#ApproveDiscription").val('');
                                $("#lnkGo").click();
                                //if (redirectToList != undefined && redirectToList != "") {
                                //    redirectToLists(DocTypeCode, 'Approved');
                                //}
                            }
                        }
                    });
                }
            }, {
                text: GetLable('lblNo', 'common', 'No'),
                click: function () {
                    $(this).dialog("close");
                    return false;
                }
            }]
        });
    }
    else {
        AlertDialog("", GetLable('lblCanNotApproveDoc', 'common', 'You can approve only Unapproved document.'))
    }
    return IsValid;
}

function DocumentRejectByAdmin(DocTypeCode, DocId, DocNo, redirectToList, ApprovalQueueId) {
    var IsValid = false;

    if (CheckExistance_DocumentApproval(DocTypeCode, DocId, DocNo)) {
        $("#DivRejectDiscription").dialog({
            title: 'Remarks',
            resizable: false,
            height: 230,
            width: 400,
            modal: true,
            buttons: [{
                text: GetLable('lblYes', 'common', 'Yes'),
                click: function () {
                    if ($("#RejectDiscription").val() != "") {
                        $(this).dialog("close");
                        $.ajax({
                            cache: false,
                            type: "POST",
                            async: false,
                            url: ITX3ResolveUrl('HRApprovalQueueESS/RejectByAdmin'),
                            data: JSON.stringify({ DocTypeCode: DocTypeCode, DocId: DocId, Remarks: $("#RejectDiscription").val(), ApprovalQueueId: ApprovalQueueId }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data == "0") {
                                    $("#dialog-MsgBox").delay(10).fadeIn();
                                    $("#lblMsgBox").text(DocNo + " Not Rejected.");
                                    $("#dialog-MsgBox").delay(4000).fadeOut();
                                    IsValid = false;
                                }
                                else {
                                    $("#dialog-MsgBox").delay(10).fadeIn();
                                    $("#lblMsgBox").text(DocNo + " Rejected Successfully.");
                                    $("#dialog-MsgBox").delay(4000).fadeOut();
                                    IsValid = true;
                                    $("#RejectDiscription").val('');
                                    $("#lnkGo").click();
                                    //if (redirectToList != undefined && redirectToList != "") {
                                    //    redirectToLists(DocTypeCode, 'Rejected');
                                    //}
                                }
                            }
                        });
                    }
                    else {
                        AlertDialog("#RejectDiscription", GetLable('lblEnterRemarks', 'common', 'Please Enter Remarks.'))
                    }
                }
            }, {
                text: GetLable('lblNo', 'common', 'No'),
                click: function () {
                    $(this).dialog("close");
                    return false;
                }
            }]


        });
    }
    else {
        AlertDialog("", GetLable('lblYoucannotRejectdoc', 'common', 'You can reject only Unapproved document.'))
    }
    return IsValid;
}

function GetNewGuidStr() {
    var Id = "";
    $.ajax
        ({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Common/GetNewGuidStr'),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                Id = data;
            },
            error: function (xhr, ret, e) {
                return "";
            }
        });
    return Id;
}
function funJSSendWhatsAppPopup(Id, Email_ToId, DocType, DefPrint) {
    try {
        $('#DivCommonSendMailLoader').empty();
        var url;
        //url = ITX3ResolveUrl("QuickItemGroup/CreateQuickItemGroup?strAction=C");
        url = ITX3ResolveUrl("common/RenderSendWhatsAppPartial?Id=" + Id + "&Email_ToId=" + Email_ToId + "&DocType=" + DocType + "");
        $('#DivCommonSendMailLoader').load(url);
        $('#DivCommonSendMailLoader').dialog({
            title: "Send WhatsApp",
            resizable: false,
            height: 490,
            width: 510,
            modal: true
        });

    }
    catch (exception) {
        alert(exception);
    }
    return false;
}

function CheckDuplicateTaxInItem() {
    var taxArr = [], taxValid = [];
    if ($('#ItemTAX1_2').val() != "" && $('#ItemTAX1_2').val() != undefined && $('#ItemTAX1_2').val() != null) {
        taxArr.push($('#ItemTAX1_2').val());
        taxValid.push($('#ItemTAX1_2 option:selected').text());
    }
    if ($('#ItemTAX2_2').val() != "" && $('#ItemTAX2_2').val() != undefined && $('#ItemTAX2_2').val() != null) {
        taxArr.push($('#ItemTAX2_2').val());
        taxValid.push($('#ItemTAX2_2 option:selected').text());
    }
    if ($('#ItemTAX3_2').val() != "" && $('#ItemTAX3_2').val() != undefined && $('#ItemTAX3_2').val() != null) {
        taxArr.push($('#ItemTAX3_2').val());
        taxValid.push($('#ItemTAX3_2 option:selected').text());
    }
    if ($('#ItemTAX4_2').val() != "" && $('#ItemTAX4_2').val() != undefined && $('#ItemTAX4_2').val() != null) {
        taxArr.push($('#ItemTAX4_2').val());
        taxValid.push($('#ItemTAX4_2 option:selected').text());
    }
    if ($('#ItemTAX5_2').val() != "" && $('#ItemTAX5_2').val() != undefined && $('#ItemTAX5_2').val() != null) {
        taxArr.push($('#ItemTAX5_2').val());
        taxValid.push($('#ItemTAX5_2 option:selected').text());
    }
    if ($('#ItemTAX6_2').val() != "" && $('#ItemTAX6_2').val() != undefined && $('#ItemTAX6_2').val() != null) {
        taxArr.push($('#ItemTAX6_2').val());
        taxValid.push($('#ItemTAX6_2 option:selected').text());
    }
    if (taxValid.filter(item => item.toUpperCase().indexOf('CGST') > -1).length > 0 || taxValid.filter(item => item.toUpperCase().indexOf('SGST') > -1).length > 0) {
        if (taxValid.filter(item => item.toUpperCase().indexOf('IGST') > -1).length > 0) {
            return false;
        }
    }
    else if (taxValid.filter(item => item.toUpperCase().indexOf('IGST') > -1).length > 0) {
        if (taxValid.filter(item => item.toUpperCase().indexOf('CGST') > -1).length > 0 || taxValid.filter(item => item.toUpperCase().indexOf('SGST') > -1).length > 0) {
            return false;
        }
    }

    var IsDuplicateTax = false;
    IsDuplicateTax = taxArr.some((element, index) => {
        return taxArr.indexOf(element) !== index
    });
    if (IsDuplicateTax) { return false; } else { return true; }


}

function CheckDuplicateTaxInService() {
    var taxArr = [], taxValid = [];
    if ($('#ServiceTAX1_2').val() != "" && $('#ServiceTAX1_2').val() != undefined && $('#ServiceTAX1_2').val() != null) {
        taxArr.push($('#ServiceTAX1_2').val());
        taxValid.push($('#ServiceTAX1_2 option:selected').text());
    }
    if ($('#ServiceTAX2_2').val() != "" && $('#ServiceTAX2_2').val() != undefined && $('#ServiceTAX2_2').val() != null) {
        taxArr.push($('#ServiceTAX2_2').val());
        taxValid.push($('#ServiceTAX2_2 option:selected').text());
    }
    if ($('#ServiceTAX3_2').val() != "" && $('#ServiceTAX3_2').val() != undefined && $('#ServiceTAX3_2').val() != null) {
        taxArr.push($('#ServiceTAX3_2').val());
        taxValid.push($('#ServiceTAX3_2 option:selected').text());
    }
    if ($('#ServiceTAX4_2').val() != "" && $('#ServiceTAX4_2').val() != undefined && $('#ServiceTAX4_2').val() != null) {
        taxArr.push($('#ServiceTAX4_2').val());
        taxValid.push($('#ServiceTAX4_2 option:selected').text());
    }
    if ($('#ServiceTAX5_2').val() != "" && $('#ServiceTAX5_2').val() != undefined && $('#ServiceTAX5_2').val() != null) {
        taxArr.push($('#ServiceTAX5_2').val());
        taxValid.push($('#ServiceTAX5_2 option:selected').text());
    }
    if ($('#ServiceTAX6_2').val() != "" && $('#ServiceTAX6_2').val() != undefined && $('#ServiceTAX6_2').val() != null) {
        taxArr.push($('#ServiceTAX6_2').val());
        taxValid.push($('#ServiceTAX6_2 option:selected').text());
    }
    if (taxValid.filter(item => item.toUpperCase().indexOf('CGST') > -1).length > 0 || taxValid.filter(item => item.toUpperCase().indexOf('SGST') > -1).length > 0) {
        if (taxValid.filter(item => item.toUpperCase().indexOf('IGST') > -1).length > 0) {
            return false;
        }
    }
    else if (taxValid.filter(item => item.toUpperCase().indexOf('IGST') > -1).length > 0) {
        if (taxValid.filter(item => item.toUpperCase().indexOf('CGST') > -1).length > 0 || taxValid.filter(item => item.toUpperCase().indexOf('SGST') > -1).length > 0) {
            return false;
        }
    }

    var IsDuplicateTax = false;
    IsDuplicateTax = taxArr.some((element, index) => {
        return taxArr.indexOf(element) !== index
    });
    if (IsDuplicateTax) { return false; } else { return true; }
}

function CheckValidEmail(cntrlID, cntrlType, Msg) {
    if (cntrlType == 'textbox') {
        var pattern = /^([\w-\.]+\u0040([\w-]+\.)+[\w-]{2,4})?$/;
        if ($(cntrlID).val() != "" && $(cntrlID).val() != undefined && $(cntrlID).val() != null) {
            if (!pattern.test($(cntrlID).val())) {
                label('lblRequired', 'common', 'Required');
                $("#lblRequiredLayout").text(Msg);
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                            $(cntrlID).focus();
                        }
                    }]
                });
                return false;
            }
            else {
                return true;
            }
        }
    }
    return true;
}
function CheckValidWebSite(cntrlID, cntrlType, Msg) {
    if (cntrlType == 'textbox') {
        var pattern = /^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9-\.]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$/;
        if ($(cntrlID).val() != "" && $(cntrlID).val() != undefined && $(cntrlID).val() != null) {
            if (!pattern.test($(cntrlID).val())) {
                label('lblRequired', 'common', 'Required');
                $("#lblRequiredLayout").text(Msg);
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                            $(cntrlID).focus();
                        }
                    }]
                });
                return false;
            }
            else {
                return true;
            }
        }
    }
    return true;
}


$(".PIN,.Phone,.Time").on('input', function (e) {
    this.value = this.value.replace(/[^0-9]/g, '');
});

function CheckPasswordWithPolicy(UserId, Password) {
    var Message = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckPasswordWithPolicy'),
        data: JSON.stringify({}),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.result == 1) {
                if (data.lstDynamic.length > 0) {
                    for (var i = 0; i < data.lstDynamic[0].length; i++) {
                        if (data.lstDynamic[0][i]["Key"] == "PasswordLength") {
                            if (data.lstDynamic[0][i]["Value"] > Password.length)
                                Message += "Length of Password must be greater than or equal to " + data.lstDynamic[0][i]["Value"] + " character.";
                        }
                        else if (data.lstDynamic[0][i]["Key"] == "IsLowercase") {
                            if (data.lstDynamic[0][i]["Value"] == "True") {
                                if (!Password.match(/([a-z])/)) {
                                    if (Message != "") Message += "<br>";
                                    Message += "Password must contain atleast one Lowercase character.";
                                }
                            }
                        }
                        else if (data.lstDynamic[0][i]["Key"] == "IsUppercase") {
                            if (data.lstDynamic[0][i]["Value"] == "True") {
                                if (!Password.match(/([A-Z])/)) {
                                    if (Message != "") Message += "<br>";
                                    Message += "Password must contain atleast one Uppercase character.";
                                }
                            }
                        }
                        else if (data.lstDynamic[0][i]["Key"] == "IsNumeric") {
                            if (data.lstDynamic[0][i]["Value"] == "True") {
                                if (!Password.match(/([0-9])/)) {
                                    if (Message != "") Message += "<br>";
                                    Message += "Password must contain atleast one Number.";
                                }
                            }
                        }
                        else if (data.lstDynamic[0][i]["Key"] == "IsSpecialCharacter") {
                            if (data.lstDynamic[0][i]["Value"] == "True") {
                                if (!Password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) {
                                    if (Message != "") Message += "<br>";
                                    Message += "Password must contain atleast one Special character.";
                                }
                            }
                        }
                    }
                }
            }
            else { Message += data.RetMSG; }
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
    return Message;
}

function SetTooltipForPassword() {
    var Message = "";

    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckPasswordWithPolicy'),
        data: JSON.stringify({}),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.result == 1) {
                var K = 0;
                if (data.lstDynamic.length > 0) {
                    for (var i = 0; i < data.lstDynamic[0].length; i++) {
                        if (data.lstDynamic[0][i]["Key"] == "PasswordLength") {
                            K += 1;
                            Message += K + ". Length of Password must be greater than or equal to " + data.lstDynamic[0][i]["Value"] + " character.\n";
                        }
                        else if (data.lstDynamic[0][i]["Key"] == "IsLowercase") {
                            if (data.lstDynamic[0][i]["Value"] == "True") {
                                K += 1;
                                Message += K + ". Password must contain atleast one Lowercase character.\n";
                            }
                        }
                        else if (data.lstDynamic[0][i]["Key"] == "IsUppercase") {
                            if (data.lstDynamic[0][i]["Value"] == "True") {
                                K += 1;
                                Message += K + ". Password must contain atleast one Uppercase character.\n";
                            }
                        }
                        else if (data.lstDynamic[0][i]["Key"] == "IsNumeric") {
                            if (data.lstDynamic[0][i]["Value"] == "True") {
                                K += 1;
                                Message += K + ". Password must contain atleast one Number.\n";
                            }
                        }
                        else if (data.lstDynamic[0][i]["Key"] == "IsSpecialCharacter") {
                            if (data.lstDynamic[0][i]["Value"] == "True") {
                                K += 1;
                                Message += K + ". Password must contain atleast one Special character.\n";
                            }
                        }
                    }
                }
            }
            else { Message += data.RetMSG; }
        },
        error: function (xhr, ret, e) {
            return false;
        }
    });
    return Message;
}

function funJSSendWhatsAppFacebookPopup(Id, DocType) {
    try {
        $('#DivCommonSendMailLoader').empty();
        var url;
        //url = ITX3ResolveUrl("QuickItemGroup/CreateQuickItemGroup?strAction=C");
        url = ITX3ResolveUrl("common/RenderSendWhatsFacebookAppPartial?Id=" + Id + "&DocType=" + DocType);
        $('#DivCommonSendMailLoader').load(url);
        $('#DivCommonSendMailLoader').dialog({
            title: "Send WhatsApp",
            resizable: false,
            height: 390,
            width: 670,
            modal: true
        });

    }
    catch (exception) {
        alert(exception);
    }
    return false;
}
function CheckValidCharacter(cntrlID, cntrlType, Msg) {
    if (cntrlType == 'textbox') {
        var pattern = /^([a-zA-Z0-9\@\%\&\?\#\ \!\-\_\$\()\+\|[\]\;\=\°\Ω\.\:\;\,\/\|\'\"\±\{}\\\n\~]+)$/;  
        if ($(cntrlID).val() != "" && $(cntrlID).val() != undefined && $(cntrlID).val() != null) {
            if (!pattern.test($(cntrlID).val())) {
                label('lblRequired', 'common', 'Required');
                $("#lblRequiredLayout").text(Msg);
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: [{
                        text: GetLable('lblOK', 'common', 'OK'),
                        click: function () {
                            $(this).dialog("close");
                            $(cntrlID).focus();
                        }
                    }]
                });
                return false;
            }
            else {
                return true;
            }
        }
    }
    return true;
}
