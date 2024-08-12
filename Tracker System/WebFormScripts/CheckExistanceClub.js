
function CheckExistance(Value, ColumnName, TableName, WhereCondition, Message, IsIsEntCode, IsCompCode, ExistMessage) {
    var flag = false;

    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckExistance'),
        data: JSON.stringify({ Value: Value, ColumnName: ColumnName, TableName: TableName, WhereCondition: WhereCondition, IsIsEntCode: IsIsEntCode, IsCompCode: IsCompCode, ExistMessage: ExistMessage }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (parseInt(data) > 0) {
                ShowAllMsg();
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

function CheckExistanceMsg(Value, ColumnName, TableName, WhereCondition, Message, IsMasterDB, IsEnterpriseLevel) {
    var flag = false;
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: 'Common/CheckExistance',
        data: JSON.stringify({ Value: Value, ColumnName: ColumnName, TableName: TableName, WhereCondition: WhereCondition, IsMasterDB: IsMasterDB, IsEnterpriseLevel: IsEnterpriseLevel }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.Val == "1") {
                
                Alert('hii');
                flag = true;
                return data.Msg;
            }
        },
        error: function (xhr, ret, e) {
            return false;
        }

    });

    return flag;
}

function CheckExistanceNotShowMsg(Value, ColumnName, TableName, WhereCondition, Message, IsIsEntCode, IsCompCode, ExistMessage) {
    var flag = false;

    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/CheckExistance'),
        data: JSON.stringify({ Value: Value, ColumnName: ColumnName, TableName: TableName, WhereCondition: WhereCondition, IsIsEntCode: IsIsEntCode, IsCompCode: IsCompCode, ExistMessage: ExistMessage }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (parseInt(data) > 0) {
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

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][\d]\.|1[\d]{2}\.|[\d]{1,2}\.))((25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\.){2}(25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
}


jQuery.fn.ForceNumericOnlyNew = function (value) {

    return this.each(function () {
        $(this).keydown(function (event) {
            var key = event.charCode || event.keyCode || 0;

            if (key == 9 && event.shiftKey) {
            }
            else if (event.shiftKey) {
                event.preventDefault();
            }
            else if ((key != 110 || key != 190) && (key == 8 || key == 9 || key == 46 || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {

            }
            else {
                event.preventDefault();
            }


            if ($(this).val().length > value + 1) {
                if (key == 8 || key == 9 || key == 37 || key == 39 || key == 46) {
                }
                else {
                    event.preventDefault();
                }
            }
            if ((key >= 48 && key <= 57) || (key >= 96 && key <= 105)) {

                if ($(this).val().length == value)
                    event.preventDefault();

            }
        });
    });
}

jQuery.fn.ForceDecimalOnlyNew = function (value, precision) {

    return this.each(function () {
        $(this).keydown(function (event) {
            var key = event.charCode || event.keyCode || 0;
            if (key == 9 && event.shiftKey) {
            }
            else if (event.shiftKey) {
                event.preventDefault();
            }
            else if ((key >= 48 && key <= 57) || (key >= 96 && key <= 105) || (key >= 35 && key <= 40) || key == 8 || key == 9 || key == 37 || key == 39 || key == 46 || key == 110 || key == 190) {
            }
            else {
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
                        if (parseInt(date[0]) > 31 || parseInt(date[0]) < 0 || parseInt(date[1]) > 12 || parseInt(date[1]) < 0 || date[2].length != 4 || parseInt(date[2]) < 0) {
                            isvalid = false;
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
                    buttons: {
                        "OK": function () {
                            $(this).dialog("close");
                            $(ControlId).focus();
                        }
                    }
                });
                $("#lblRequiredLayout").text("Invalid Date");
                return false;
            }
            return true;

        });
    });

}

function SetMandatoryField(cntrlID, cntrlType, Class) {

    if (cntrlType == 'textbox') {
        $(cntrlID).keyup(function () {
            if ($(cntrlID).val() == "") {
                $(cntrlID).closest('div').prop("class", Class + " has-error");
            } else {
                $(cntrlID).closest('div').prop("class", Class);
            }
        });

        $(cntrlID).blur(function () {
            if ($(cntrlID).val() == "") {
                $(cntrlID).closest('div').prop("class", Class + " has-error");
            } else {
                $(cntrlID).closest('div').prop("class", Class);
            }
        });
    }

    if (cntrlType == 'dropdown') {
        $(cntrlID).change(function () {
            if ($(cntrlID).val() == "0" || $(cntrlID).val() == "") {
                $(cntrlID).closest('div').prop("class", Class + " has-error");
            } else {
                $(cntrlID).closest('div').prop("class", Class);
            }
        });

        $(cntrlID).blur(function () {
            if ($(cntrlID).val() == "0" || $(cntrlID).val() == "") {
                $(cntrlID).closest('div').prop("class", Class + " has-error");
            } else {
                $(cntrlID).closest('div').prop("class", Class);
            }
        });
    }

}

jQuery.fn.AllowNumbersWithDecimal = function () {
    //46,110,190 for Decimal, 8 for backspace,9 for tab,35 to 40 arrow keys, 

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

function toFixedDecimal(obj) {
    if ($(obj).val() != '') {
        $(obj).val(parseFloat($(obj).val()).toFixed(2));

        if ($(obj).val() == 'NaN') {
            $(obj).val('0.00');
        }
    }
    else {
        $(obj).val('0.00');
    }
}

function SetMandatoryFieldRegistration(cntrlID, cntrlType) {

    if (cntrlType == 'textbox') {
    
        if ($(cntrlID).val() == "") {
            $(cntrlID).closest('div').prop("class", "form-group has-feedback has-warning");
        } else {
            $(cntrlID).closest('div').prop("class", "form-group has-feedback");
        }

        $(cntrlID).keyup(function () {
            if ($(cntrlID).val() == "") {
                $(cntrlID).closest('div').prop("class", "form-group has-feedback has-error");
            } else {
                $(cntrlID).closest('div').prop("class", "form-group has-feedback");
            }
        });

        $(cntrlID).blur(function () {
            if ($(cntrlID).val() == "") {
                $(cntrlID).closest('div').prop("class", "form-group has-feedback has-error");
            } else {
                $(cntrlID).closest('div').prop("class", "form-group has-feedback");
            }
        });
    }

    if (cntrlType == 'dropdown') {
        $(cntrlID).change(function () {
            if ($(cntrlID).val() == "0" || $(cntrlID).val() == "") {
                $(cntrlID).closest('div').prop("class", "form-group has-feedback has-error");
            } else {
                $(cntrlID).closest('div').prop("class", "form-group has-feedback");
            }
        });

        $(cntrlID).blur(function () {
            if ($(cntrlID).val() == "0" || $(cntrlID).val() == "") {
                $(cntrlID).closest('div').prop("class", "form-group has-feedback has-error");
            } else {
                $(cntrlID).closest('div').prop("class", "form-group has-feedback");
            }
        });
    }

    if (cntrlType == 'checkbox') {
        $(cntrlID).keyup(function () {
            
            if ($(cntrlID).prop('checked') == false) {
                $(cntrlID).prop("class", "has-error");
            } else {
                $(cntrlID).prop("class", "");
            }
        });

        $(cntrlID).blur(function () {
            if ($(cntrlID).prop('checked') == false) {
                $(cntrlID).prop("class", "has-error");
            } else {
                $(cntrlID).prop("class", "");
            }
        });
    }

}
   