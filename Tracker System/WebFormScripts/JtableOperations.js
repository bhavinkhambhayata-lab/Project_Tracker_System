
var Title = "";
function RowSelectedOrNot(btn, ControllerName) {

   
    var i = 0;
    var id = "";
    label('lblRequired', 'common', 'Required');
    $('input[type=checkbox][name^=chkGrd]').each(function () {
        if (this.checked) {
            i += 1;
            id += (id == "" ? "" : ",") + (btn == "D" ? "'" : "") + $(this).context.id + (btn == "D" ? "'" : "");
        }
    });

    if (i == 0) {
        $("#dialog-RequiredLayout").dialog({
            title: Title,
            resizable: false,
            height: 170,
            modal: true,
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
        if (btn == "D") {
            label('gblSelect', 'common', 'Please select row.');
        }
        else {
            label('gblSelectSingle', 'common', 'Please select any single row.');
        }
        return false;
    }

    if (btn == 'D') {
        //return confirm('Are you sure to delete?');
    }
    else if (i > 1) {
        $("#dialog-RequiredLayout").dialog({
            title: Title,
            resizable: false,
            height: 170,
            modal: true,
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
        label('gblSelectSingle', 'common', 'Please select any single row.');
        return false;
    }

    if (btn == 'E') {

        $("#lnkEdit").attr("href", "Create" + ControllerName + "/?id=" + id + "&strAction=E");
    }
    if (btn == 'EC') {

        $("#lnkEdit").attr("href", "../../" + ControllerName + "/Create" + ControllerName + "/?id=" + id + "&strAction=E");
    }
    if (btn == 'V') {
        $("#lnkView").attr("href", "View" + ControllerName + "/?id=" + id + "&strAction=V");
    }
    if (btn == 'VC') {
        $("#lnkView").attr("href", "../../" + ControllerName + "/View" + ControllerName + "/?id=" + id + "&strAction=V");
    }
    if (btn == 'D') {
        var TransactionCode = ControllerName;
        if (TransactionCode == "CRMLeads") { TransactionCode = "CRMLead"}
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Common/CheckUserPermissions'),
            data: JSON.stringify({ TransactionName: TransactionCode, TransactionMode: 'D' }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data == "0") {
                    $('<div title="Alert"></div>').dialog({
                        open: function (event, ui) {
                            $(this).html('Access denied. You do not have permission to perform this operation');

                        },
                        buttons: {
                            "OK": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                } else {
                    $.ajax({
                        cache: false,
                        type: "POST",
                        async: false,
                        url: ITX3ResolveUrl(ControllerName + '/CheckForDelete'),
                        data: JSON.stringify({ id: id }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data == "1") {
                                $('<div title="Alert"></div>').dialog({
                                    open: function (event, ui) {
                                        $(this).html('You can not delete selected record, It is used in another transaction');

                                    },
                                    buttons: {
                                        "OK": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            } else {
                                DeleteRecord(id, ControllerName);
                            }

                        }
                    });
                }

            }
        });


        
    }

    return true;
}



function RowSelectedOrNot_OtherRedirectCallFrom(btn, ControllerName,CallFrom) {

  
    var i = 0;
    var id = "";
    label('lblRequired', 'common', 'Required');
    $('input[type=checkbox][name^=chkGrd]').each(function () {
        if (this.checked) {
            i += 1;
            id += (id == "" ? "" : ",") + (btn == "D" ? "'" : "") + $(this).context.id + (btn == "D" ? "'" : "");
        }
    });

    if (i == 0) {
        $("#dialog-RequiredLayout").dialog({
            title: Title,
            resizable: false,
            height: 170,
            modal: true,
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
        if (btn == "D") {
            label('gblSelect', 'common', 'Please select row.');
        }
        else {
            label('gblSelectSingle', 'common', 'Please select any single row.');
        }
        return false;
    }

    if (btn == 'D') {
        //return confirm('Are you sure to delete?');
    }
    else if (i > 1) {
        $("#dialog-RequiredLayout").dialog({
            title: Title,
            resizable: false,
            height: 170,
            modal: true,
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
        label('gblSelectSingle', 'common', 'Please select any single row.');
        return false;
    }
  
    //jayesh
    if (btn == 'E') {

        $("#lnkEdit").attr("href", "Create" + ControllerName + "/?id=" + id + "&strAction=E&CallFrom=" + CallFrom);
    }
    if (btn == 'EC') {

        $("#lnkEdit").attr("href", "../../" + ControllerName + "/Create" + ControllerName + "/?id=" + id + "&strAction=E" + "" + "&CallFrom=" + CallFrom);
    }
    if (btn == 'V') {
        $("#lnkView").attr("href", "View" + ControllerName + "/?id=" + id + "&strAction=V");
    }
    if (btn == 'VC') {
        $("#lnkView").attr("href", "../../" + ControllerName + "/View" + ControllerName + "/?id=" + id + "&strAction=V" + "" + "&CallFrom=" + CallFrom);
    }
    if (btn == 'D') {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Common/CheckUserPermissions'),
            data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: 'D' }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data == "0") {
                    $('<div title="Alert"></div>').dialog({
                        open: function (event, ui) {
                            $(this).html('No permis');

                        },
                        buttons: {
                            "OK": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                } else {
                    $.ajax({
                        cache: false,
                        type: "POST",
                        async: false,
                        url: ITX3ResolveUrl(ControllerName + '/CheckForDelete'),
                        data: JSON.stringify({ id: id }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data == "1") {
                                $('<div title="Alert"></div>').dialog({
                                    open: function (event, ui) {
                                        $(this).html('You can not delete selected record, It is used in another transaction');

                                    },
                                    buttons: {
                                        "OK": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            } else {
                                DeleteRecord(id, ControllerName);
                            }

                        }
                    });
                }

            }
        });



    }

    return true;
}


function DeleteRecord(IDs, ControllerName) {
    label('lblConfirmDelete', 'common', 'Confirm delete');
    $("#dialog-confirmLayout").dialog({
        title: Title,
        resizable: false,
        height: 170,
        modal: true,
        buttons: {
            "Delete": function () {
                var prevQuerystring = "";
                prevQuerystring = window.location.search;
                prevQuerystring = prevQuerystring.replace('?', '');

                $.ajax({
                    cache: false,
                    type: "POST",
                    async: false,
                    url: ITX3ResolveUrl(ControllerName + '/View' + ControllerName + '?test=test' + prevQuerystring),
                    data: JSON.stringify({ id: IDs }),
                    dataType: "html",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                       // alert(data);
                        $("#dialog-confirmLayout").dialog("close");
                         location.reload();
                    },
                    error: function (xhr, ret, e) {
                    
                        $("#dialog-confirmLayout").dialog("close");
                         location.reload();
                    }

                });
               // $('#' + ControllerName + 'TableContainer').jtable('load');
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });
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

//For Edit And Delete With Condition
function RowSelectedOrNot_AndChkEditDelete(btn, ControllerName, Control, ID) {
    var i = 0; var flag = true;
    var id = "";
    if (Control == 'CHECKBOX') {
        $('input[type=checkbox][name^=chkGrd]').each(function () {
            if (this.checked) {
                if (flag == true) {
                    i += 1;
                    id += (id == "" ? "" : ",") + (btn == "D" ? "'" : "") + $(this).context.id + (btn == "D" ? "'" : "");
                    $.ajax({
                        cache: false,
                        type: "POST",
                        async: false,
                        url: ITX3ResolveUrl(ControllerName + '/CheckEditDelete'),
                        data: JSON.stringify({ id: $(this).context.id }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data == "1") {
                                flag = false;
                                $('<div title="Alert" style=padding-left:10px></div>').dialog({
                                    open: function (event, ui) {
                                        $(this).html(' You can not edit/delete auto records');

                                    },
                                    buttons: {
                                        "OK": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                                return false;
                            }
                        }
                    });
                }
                else
                    return false;
            }
        });

        if (flag == true) {
            label('lblRequired', 'common', 'Required');
            if (i == 0) {
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: {
                        "OK": function () {
                            $(this).dialog("close");
                        }
                    }
                });
                if (btn == "D") {
                    label('gblSelect', 'common', 'Please select row.');
                }
                else {
                    label('gblSelectSingle', 'common', 'Please select any single row.');
                }
                return false;
            }

            if (btn == 'D') {
            }
            else if (i > 1) {
                $("#dialog-RequiredLayout").dialog({
                    title: Title,
                    resizable: false,
                    height: 170,
                    modal: true,
                    buttons: {
                        "OK": function () {
                            $(this).dialog("close");
                        }
                    }
                });
                label('gblSelectSingle', 'common', 'Please select any single row.');
                return false;
            }

            if (btn == 'E') {
                $("#lnkEdit").attr("href", "Create" + ControllerName + "/?id=" + id + "&strAction=E");
            }

            if (btn == 'D') {
                $.ajax({
                    cache: false,
                    type: "POST",
                    async: false,
                    url: ITX3ResolveUrl(ControllerName + '/CheckForDelete'),
                    data: JSON.stringify({ id: id }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data == "1") {
                            $('<div title="Alert" style=padding-left:10px></div>').dialog({
                                open: function (event, ui) {
                                    $(this).html('You can not delete selected record, It is used in another transaction');

                                },
                                buttons: {
                                    "OK": function () {
                                        $(this).dialog("close");
                                    }
                                }
                            });
                        }
                        else {
                            DeleteRecord(id, ControllerName);
                        }

                    }
                });
            }
        }
    }
    if (Control == 'LINKBUTTON')
    {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl(ControllerName + '/CheckEditDelete'),
            data: JSON.stringify({ id: ID }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data == "1") {
                    flag = false;
                    $('<div title="Alert" style=padding-left:10px></div>').dialog({
                        open: function (event, ui) {
                            $(this).html(' You can not edit/delete auto records');

                        },
                        buttons: {
                            "OK": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                    return false;
                }
                else {
                    if (btn == 'E') {
                        var url = "Create" + ControllerName + "/?id=" + ID + "&strAction=E";
                        $(location).attr('href', url);
                    }
                    else if (btn == 'D')
                    {
                        $.ajax({
                            cache: false,
                            type: "POST",
                            async: false,
                            url: ITX3ResolveUrl(ControllerName + '/CheckForDelete'),
                            data: JSON.stringify({ id: ID }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data == "1") {
                                    $('<div title="Alert" style=padding-left:10px></div>').dialog({
                                        open: function (event, ui) {
                                            $(this).html('You can not delete selected record, It is used in another transaction');

                                        },
                                        buttons: {
                                            "OK": function () {
                                                $(this).dialog("close");
                                            }
                                        }
                                    });
                                }
                                else {
                                    DeleteRecord(ID, ControllerName);
                                }

                            }
                        });
                    }
                }
            }
        });
    }
 return true;
   
}