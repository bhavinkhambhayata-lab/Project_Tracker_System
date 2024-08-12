
var Title = "";

function CheckExistance(Value, ColumnName, TableName, WhereCondition, Message, IsMasterDB) {
    var flag = false;

    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckExistance'),
        data: JSON.stringify({ Value: Value, ColumnName: ColumnName, TableName: TableName, WhereCondition: WhereCondition, IsMasterDB: IsMasterDB }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Val == "1") {
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
                $("#lblRequiredLayout").text(Value + " " + data.Msg);
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

function AlertDialog(Msg) {
    label('lblAlert', 'common', 'Alert');
    $("#lblRequiredLayout").text(Msg);
    $("#dialog-RequiredLayout").dialog({
        title: Title,
        resizable: false,
        height: 'auto',
        modal: true,
        buttons: [{
            text: GetLable('lblOK', 'common', 'OK'),
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
    return false;
}

function GetColumnValueGeneralised(getColumn, byColumnName, tableName, byColumnValue)
{
    var returndata = "";
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/GetColumnValueGeneralised'),
        data: JSON.stringify({ getColumn: getColumn, byColumnName: byColumnName, tableName: tableName, byColumnValue: byColumnValue }),
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

function JSGetAllUnitList() {
    var JSUnitList = [];
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Unit/GetAllUnitList'),       
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data)
        {
            JSUnitList = data
        },
        error: function (xhr, ret, e) {
            return false;
        }

    });
    return JSUnitList;
}

function JSGetUnitDecimalPlaces(lstUnit, UnitId) {
    var decimalplaces = 2;
    if (lstUnit.length > 0 && UnitId.length > 0 && lstUnit != undefined  && UnitId != undefined) {
        $.grep(lstUnit, function (V, I) {
            if ($.trim(V["Id"]) == UnitId) {
                decimalplaces = $.trim(V["DecimalPlaces"])
            }
        });
    }
    return decimalplaces;
}

function funJSSendEmailPopup(Id, Email_ToId, DocType, DefPrint) {
    try {
        $('#DivCommonSendMailLoader').empty();
        var url;        
        //url = ITX3ResolveUrl("QuickItemGroup/CreateQuickItemGroup?strAction=C");
        url = ITX3ResolveUrl("common/RenderSendMailPartial?Id=" + Id + "&Email_ToId=" + Email_ToId + "&DocType=" + DocType + "");
        $('#DivCommonSendMailLoader').load(url);        
        $('#DivCommonSendMailLoader').dialog({
            title: "Send E-Mail",
            resizable: false,
            height: 500,           
            width: 540,
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

jQuery.fn.ForceNumericOnly = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
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
        });
    });

}

jQuery.fn.ForceDecimalOnly = function (value, precision) {    
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
    return true;
}

function CheckDateWithFromToRange(FromDate, ToDate, format, MSG) {
    var From = $.trim($(FromDate).val()).split('/');
    var To = $.trim($(ToDate).val()).split('/');
    if (format == "dd/MM/yyyy" && From != "" && To != "") {
        var DtFrom = new Date(Number(From[2].substr(0, 4)), (Number(From[1]) - 1), Number(From[0]));
        var DtTo = new Date(Number(To[2].substr(0, 4)), (Number(To[1]) - 1), Number(To[0]));

        if (DtFrom > DtTo) {
            AlertDialog(this, MSG);
            return false;
        }

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
function AlertDialogs(Msg) {
    label('lblAlert', 'common', 'Alert');
    $("#lblRequiredLayout").text(Msg);
    $("#dialog-RequiredLayout").dialog({
        title: "Alert",
        resizable: false,
        height: 'auto',
        modal: true,
        buttons: [{
            text: GetLable('lblOK', 'common', 'OK'),
            click: function () {
                $(this).dialog("close");
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

            if ((key == 189 || key == 109) && $(this).val() != '') { event.preventDefault(); }

            if (
            (key >= 48 && key <= 57) ||
            (key >= 96 && key <= 105) || (key >= 35 && key <= 40) ||
            key == 8 || key == 9 || key == 37 ||
            key == 39 || key == 46 || key == 110 || key == 190 || key == 189 || key == 109) {
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
    return Amount.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
}
jQuery.fn.DateNumeric = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;
            var retnval = (
                        key == 8 ||
                        key == 9 ||
                        key == 46 ||
                        key == 110 ||
                        key == 190 ||
                        (key >= 35 && key <= 40) ||
                        (key >= 48 && key <= 57) ||
                        (key >= 96 && key <= 105));
            if (retnval) {
                var Len = $("#" + e.currentTarget.id).val();
            }
            return retnval;
        });
    });

}
jQuery.fn.DateAddSlac = function () {
    return this.each(function () {
        $(this).keyup(function (e) {
            var $eval = $("#" + e.currentTarget.id).val();
            var len = parseInt($eval.length);
            if (len == 2 || len == 5) {
                $("#" + e.currentTarget.id).val($("#" + e.currentTarget.id).val() + "/")
            }

        });
    });
}
function CheckDateTimeWithFromToRange(FromDateId, FromTimeId, ToDateId, ToTimeId, format, MSG, isCheckfutureDate, withDate) {
    try {

        if ($(FromTimeId).val() == "")
        {
            $(FromTimeId).val('00:00');
        }
        if ($(ToTimeId).val() == "") {
            $(ToTimeId).val('00:00');
        }
        var FromDate = $.trim($(FromDateId).val()).split('/');
        var FromTime = $.trim($(FromTimeId).val()).split(':');
        var ToDate = $.trim($(ToDateId).val()).split('/');
        var ToTime = $.trim($(ToTimeId).val()).split(':');
        var CurrDate = new Date();
        if (format == "dd/MM/yyyy" && FromDate != "" && FromTime != "" && FromTime != "" && ToTime != "") {
            var DtFrom = new Date(Number(FromDate[2].substr(0, 4)), (Number(FromDate[1]) - 1), Number(FromDate[0]), FromTime[0], FromTime[1], "00");
            var DtTo = new Date(Number(ToDate[2].substr(0, 4)), (Number(ToDate[1]) - 1), Number(ToDate[0]), ToTime[0], ToTime[1], "00");

            if (DtFrom > DtTo) {
                AlertDialog(FromDateId, MSG);
                return false;
            }
            if (isCheckfutureDate) {
                if (withDate == "FromDate") {
                    if (DtFrom > CurrDate) {
                        AlertDialog(FromDateId, "Entered date can not be future date.");
                        return false;
                    }
                }
                if (withDate == "ToDate") {
                    if (DtTo > CurrDate) {
                        AlertDialog(ToDateId, "Entered date can not be future date.");
                        return false;
                    }
                }
            }

        }
        else {
            AlertDialog(FromDateId, "Please Enter Valid From date or To date.");
            return false;
        }
        return true;
    }
    catch (e) {
        alert(e);
        return false;
    }
}