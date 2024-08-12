
var Title = "";
var dispGridHelp = "Filter records using =(equals), +(starts with), -(ends with), *(contains), !(doesn't contain)";
selectedExt = new Array();


var selectedExt = new Array();
var selectedExtRow = new Array();


function callEdit(selected) {
    selectedExt = new Array(selected.length); selectedExtRow = new Array();
    for (var i = 0; i < selected.length; i++) {

        if (selected[i].data.Id != undefined) {
            selectedExt[i] = selected[i].data.Id;
        }
        else if (selected[i].data.ID != undefined) {
            selectedExt[i] = selected[i].data.ID;
        }
        else if (selected[i].data.ConsoTenantId != undefined) {
            selectedExt[i] = selected[i].data.ConsoTenantId;
        }
        else if (selected[i].data.TenantId != undefined) {
            selectedExt[i] = selected[i].data.TenantId;
        }


        selectedExtRow[i] = selected[i].data;

    }

}

function OpenHelpMenu(url) {
    window.open(ITX3ResolveUrl('~/Help?strAction=H&Id=' + url));
}

function RowSelectedOrNotEXT_CSP(btn, ControllerName, Action) {
    var i = 0;
    var id = "";
    if (selectedExt.length == 0) {
        label('gblSelect', 'common', 'Please select row.');
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
        return false;
    }
    var i = 0;
    for (var km = 0; km < selectedExt.length; km++) {
        id += (id == "" ? "" : ",") + (btn == "D" ? "'" : "") + selectedExt[km] + (btn == "D" ? "'" : "");
        i += 1;
    }

    if (i == 0) {
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
    var prevQuerystring = "";
    prevQuerystring = window.location.search;
    prevQuerystring = prevQuerystring.replace('?', '');
    DeleteRecord(id, ControllerName, prevQuerystring);
    return true;
}

function RowSelectedOrNotEXT(btn, ControllerName, Action) {
    var i = 0;
    var id = "";

    if (selectedExt.length == 0) {
        label('gblSelect', 'common', 'Please select row.');
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
        return false;
    }
    var i = 0;
    for (var km = 0; km < selectedExt.length; km++) {
        //    alert(selected[i].data.Id);
        //selectedExt[km] = selected[km].data.Id;
        id += (id == "" ? "" : ",") + (btn == "D" ? "'" : "") + selectedExt[km] + (btn == "D" ? "'" : "");
        i += 1;
    }

    if (i == 0) {
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
        label('gblSelectSingle', 'common', 'Please select any single row.');
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
        return false;
    }
    var prevQuerystring = "";
    prevQuerystring = window.location.search;
    prevQuerystring = prevQuerystring.replace('?', '');
    if (btn == 'E') {
        $("#lnkEdit").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=E" + "&" + prevQuerystring);
    }
    if (btn == 'X') {
        $("#lnkExport").attr("href",ControllerName + "?id=" + id);
    }
    if (btn == 'EV') {
        $("#lnkView").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=EV" + "&" + prevQuerystring);
    }
    if (btn == 'T') {
        $("#lnkTest,#lnkTest1").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=T" + "&" + prevQuerystring);
    }
    if (btn == 'SQ') { // Added by Raj
        $("#lnkAddQuestion,#lnkAddQuestion1").attr("href", ITX3ResolveUrl('~/CRM/CRMSurveyQuestion/CreateCRMSurveyQuestion?SurveyDefinitionId=' + id + '&strAction=C'));
    }
    if (btn == 'Def') {
        $("#lnkDefinition, #lnkDefinition1").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=E" + "&Callfrom=definition" + "&" + prevQuerystring);
    }
    if (btn == 'Def') {
        $("#lnkPrjSchedule, #lnkPrjSchedule1").attr("href", "ProjectSchedule?id=" + id + "&strAction=S" + "&Callfrom=definition" + "&" + prevQuerystring);
    }
    if (btn == 'Pack') {
        $("#lnkPackage, #lnkPackage1").attr("href", "../" + ControllerName + "/List" + ControllerName + "?MenuDefinitionId=" + id);
    }
    if (btn == 'PackageEdit') {
        $("#lnkEdit").attr("href", "../" + ControllerName + "/Create" + ControllerName + "?Id=" + id + "&strAction=E&" + prevQuerystring);
    }
    if (btn == 'AssignBOQ') {
        AssignBOQ();
    }
    if (btn == 'ProjectSetup') {
        $("#lnkProjectSetup, #lnkProjectSetup1").attr("href", "ProjectSetup?id=" + id + "&Callfrom=ProjectSetup" + "&" + prevQuerystring);
    }
    if (btn == 'ProjectSetupStatus') {
        $("#lnkProjectSetupStatus").attr("href", "ProjectSetupStatus?id=" + id + "&Callfrom=ProjectSetupStatus" + "&" + prevQuerystring);
    }

    if (btn == 'lnkProjectSetupStatus2') {
        $("#lnkProjectSetupStatus2").attr("href", "ProjectSetupStatus?id=" + id + "&Callfrom=ProjectSetupStatus" + "&" + prevQuerystring);
    }
    if (btn == 'V') {
        if (Action != undefined && Action != "") {
            ControllerName = Action;
            $("#lnkView").attr("href", "View" + ControllerName + "?id=" + id + "&strAction=V" + "&" + prevQuerystring);
        }
        else {
            $("#lnkView").attr("href", "View" + ControllerName + "?id=" + id + "&strAction=V" + "&" + prevQuerystring);
        }

    }
    if (btn == 'SFV') {
        $("#lnkView").attr("href", "View" + ControllerName + "?id=" + id + "&strAction=V" + "&CallFrom=SFV" + "&" + prevQuerystring);
    }
    if (btn == 'SFE') {
        $("#lnkEdit").attr("href", "View" + ControllerName + "?id=" + id + "&strAction=E" + "&CallFrom=SFE" + "&" + prevQuerystring);
    }
    if (btn == 'VSGS') // For Dashboard View (ex. Customer Dashboard)
    {
        $("#lnkView").attr("href", "../" + ControllerName + "/" + Action + "?id=" + id + "&strAction=V" + "&" + prevQuerystring);
    }
    if (btn == 'VD') // For Dashboard View (ex. Customer Dashboard)
    {
        $("#lnkView").attr("href", "../" + ControllerName + "/View" + Action + "?id=" + id + "&strAction=V" + "&" + prevQuerystring);
    }

    if (btn == 'AP') {
        $("#lnkPermission").attr("href", "AssignPermission?id=" + id + "&strAction=AP" + "&" + prevQuerystring);
    }

    if (btn == 'APDT') {
        $("#lnkPermissionDocTypeWise").attr("href", "../" + ControllerName + "/" + Action + "?id=" + id + "&strAction=AP" + "&" + prevQuerystring);
    }

    if (btn == 'AR') {
        $("#lnkRole").attr("href", Action + "?id=" + id + "&strAction=AP" + "&" + prevQuerystring);
    }

    if (btn == 'DADC') {

        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Common/CheckUserPermissions'),
            data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: 'D', prevQuerystring: prevQuerystring }),
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
                                        $(this).html('This Depreciation Calculation can not be deleted before the trailing calculation period');

                                    },
                                    buttons: [{
                                        text: GetLable('lblOK', 'common', 'OK'),
                                        click: function () {
                                            $(this).dialog("close");
                                        }
                                    }]
                                });
                            } else {
                                DeleteRecord(id, ControllerName, "", Action);
                            }

                        }
                    });


                }

            }
        });
    }

    if (btn == 'D') {

        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Common/CheckUserPermissions'),
            data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: 'D', prevQuerystring: prevQuerystring }),
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
                                    buttons: [{
                                        text: GetLable('lblOK', 'common', 'OK'),
                                        click: function () {
                                            $(this).dialog("close");
                                        }
                                    }]
                                });
                            } else {
                                DeleteRecord(id, ControllerName, "", Action);
                            }

                        }
                    });


                }

            }
        });
    }
    if (btn == 'CP') {
        $("#lnkCopy").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=C" + "&CopyQuotation=true" + "&" + prevQuerystring);
    }
    else if (btn == 'CV') {
        $("#lnkCopy").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=C" + "&CopyVoucher=true&VoucherType=" + selectedExtRow[0].VTypeCode + "&" + prevQuerystring);
    }
    else if (btn == 'C') {
        $("#lnkCopy").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=C" + "&Copy=true" + "&" + prevQuerystring);
    }
    else if (btn == 'CONVERT_INTO_BANKRECIPT') {
        window.location.href = ITX3ResolveUrl('AccountVoucher/CreateBankReceipt?strFromCreate=CONVERT_FROM_MEMBER&strAction=C&VoucherType=BR&id=' + id);
    }

    if (btn == 'US') {
        if (Action != undefined && Action != "") {
            ControllerName = Action;
            $("#lnkUpdateStatus").attr("href", "View" + ControllerName + "?id=" + id + "&strAction=US" + "&" + prevQuerystring);
        }
        else {
            $("#lnkUpdateStatus").attr("href", "View" + ControllerName + "?id=" + id + "&strAction=US" + "&" + prevQuerystring);
        }

    }

    return true;
}

function RowSelectedOrNotEXT_View(Id, btn, ControllerName) {
    var i = 0;
    var id = "";

    id = (btn == "D" ? "'" : "") + Id + (btn == "D" ? "'" : "");

    var prevQuerystring = "";
    prevQuerystring = window.location.search;
    prevQuerystring = prevQuerystring.replace('?', '');
    if (btn == 'E') {
        $("#lnkEdit").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=E");
    }
    if (btn == 'T') {
        $("#lnkTest").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=T");
    }
    if (btn == 'D') {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('Common/CheckUserPermissions'),
            data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: 'D', prevQuerystring: prevQuerystring }),
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
                                    buttons: [{
                                        text: GetLable('lblOK', 'common', 'OK'),
                                        click: function () {
                                            $(this).dialog("close");
                                        }
                                    }]
                                });
                            } else {
                                DeleteRecord(id, ControllerName, "V");
                            }

                        }
                    });


                }

            }
        });
    }

    return true;
}

function DeleteRecord(IDs, ControllerName, CallFrom, Action) {
    var prevQuerystring = "";
    prevQuerystring = window.location.search;
    prevQuerystring = prevQuerystring.replace('?', '');

    if (Action != undefined && Action != "") { //for Admin/Coupons
        var url = ITX3ResolveUrl(ControllerName + '/View' + Action + "?test=test&" + prevQuerystring)
        ControllerName = Action;
    }
    else {
        var url = ITX3ResolveUrl(ControllerName + '/View' + ControllerName + "?test=test&" + prevQuerystring)
    }

    label('lblConfirmDelete', 'common', 'Confirm delete');
    $("#dialog-confirmLayout").dialog({
        title: Title,
        resizable: false,
        height: 170,
        modal: true,
        buttons: [{
            text: GetLable('lblDelete', 'common', 'Delete'),
            click: function () {
                $.ajax({
                    cache: false,
                    type: "POST",
                    async: false,
                    url: url,
                    data: JSON.stringify({ id: IDs }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        alert(data);
                        $("#dialog-confirmLayout").dialog("close");
                        location.reload();
                    },
                    error: function (xhr, ret, e) {
                        $("#dialog-confirmLayout").dialog("close");
                        location.reload();
                    }
                });
                if (CallFrom == "V") {
                    if (ControllerName == "Item")
                        $(location).attr("href", "ListItems");
                    else if (ControllerName == "PRDPlan")
                        location.reload();
                    else
                        $(location).attr("href", "List" + ControllerName);
                }
                $('#' + ControllerName + 'TableContainer').jtable('load');
            }
        },
        {
            text: GetLable('lblCancel', 'common', 'Cancel'),
            click: function () {
                $(this).dialog("close");
            }
        }]
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
function RowSelectedOrNot_AndChkEditDeleteEXT(btn, ControllerName, strMsg) {

    var i = 0; var flag = true;
    var id = "";
    if (strMsg == undefined || strMsg == "") { strMsg = " You can not edit/delete primary records "; }
    for (var km = 0; km < selectedExt.length; km++) {
        //    alert(selected[i].data.Id);
        //selectedExt[km] = selected[km].data.Id;
        id += (id == "" ? "" : ",") + (btn == "D" ? "'" : "") + selectedExt[km] + (btn == "D" ? "'" : "");
        i += 1;
        if (flag == true) {

                $.ajax({
                    cache: false,
                    type: "POST",
                    async: false,
                    url: ITX3ResolveUrl(ControllerName + '/CheckEditDelete'),
                    data: JSON.stringify({ id: selectedExt[km], Action: btn }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var RecordCount = 1;
                        if (data.returnVal >= 0) { RecordCount = data.returnVal; }
                        if (data.MSG) { strMsg = data.MSG; }
                        if (RecordCount >= 1) {
                            flag = false;
                            $('<div title="Alert" style=padding-left:10px></div>').dialog({
                                open: function (event, ui) {
                                    $(this).html(strMsg);
                                },
                                buttons: [{
                                    text: GetLable('lblOK', 'common', 'OK'),
                                    click: function () {
                                        $(this).dialog("close");
                                    }
                                }]
                            });
                            return false;
                        }
                    },
                    error: function (xhr, ret, e) {
                        alert('some error occured');
                        return false;
                    }
                });
            
        }
        else
            return false;
    }
    if (flag == true) {
        if (selectedExt.length == 0) {
            label('gblSelect', 'common', 'Please select row.');
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
            return false;
        }
        var i = 0;
        var id = "";
        for (var km = 0; km < selectedExt.length; km++) {
            //    alert(selected[i].data.Id);
            //selectedExt[km] = selected[km].data.Id;
            id += (id == "" ? "" : ",") + (btn == "D" ? "'" : "") + selectedExt[km] + (btn == "D" ? "'" : "");
            i += 1;
        }

        if (i == 0) {
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
            if (btn == "D") {
                label('gblSelect', 'common', 'Please select row.');
            }
            else {
                label('gblSelectSingle', 'common', 'Please select any single row.');
            }
            return false;

        }

        if (btn == 'D' || btn == 'CONVERT' || btn == 'CONVERTVN' || btn == 'CONVERTAC') {
            //return confirm('Are you sure to delete?');
        }
        else if (i > 1) {
            label('gblSelectSingle', 'common', 'Please select any single row.');
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
            return false;
        }
        var prevQuerystring = "";
        prevQuerystring = window.location.search;
        prevQuerystring = prevQuerystring.replace('?', '');
        if (btn == 'E') {
            $("#lnkEdit").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=E" + "&" + prevQuerystring);
        }

        if (btn == 'V') {
            $("#lnkView").attr("href", "View" + ControllerName + "?id=" + id + "&strAction=V" + "&" + prevQuerystring);
        }

        if (btn == 'D') {

            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('Common/CheckUserPermissions'),
                data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: 'D', prevQuerystring: prevQuerystring }),
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
                                        buttons: [{
                                            text: GetLable('lblOK', 'common', 'OK'),
                                            click: function () {
                                                $(this).dialog("close");
                                            }
                                        }]
                                    });
                                } else {
                                    DeleteRecord(id, ControllerName, "");
                                }

                            }
                        });
                    }
                }
            });

        }

        //jayesh
        if (btn == 'CONVERT' || btn == 'CONVERTVN' || btn == 'CONVERTAC') {
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('Common/CheckUserPermissions'),
                data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: 'E', prevQuerystring: prevQuerystring }),
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
                    } else {

                        //alert('CALL ');
                        //DeleteRecord(id, ControllerName);
                        $('<div title="Convert" ></div>').dialog({
                            height: 400,
                            width: 280,
                            open: function (event, ui) {
                                var strHTML = "";
                                if (btn == 'CONVERT') {
                                    strHTML += '<br/><a href="Convert' + ControllerName + "?id=" + id + "&ConvertTo=CS&strAction=Convert" + "&" + prevQuerystring + '" id="lnkCS"  class="smallsmoothrectange whitebutton"  style="padding:10px 75px 10px 75px;">    Customer    </a> <br/><br/>'
                                    strHTML += '<br/><a href="Convert' + ControllerName + "?id=" + id + "&ConvertTo=VN&strAction=Convert" + "&" + prevQuerystring + '" id="lnkVN"  class="smallsmoothrectange whitebutton"  style="padding:10px 82px 10px 82px;">    Vendor    </a> <br/><br/>'
                                    strHTML += '<br/><a href="Convert' + ControllerName + "?id=" + id + "&ConvertTo=TP&strAction=Convert" + "&" + prevQuerystring + '" id="lnkTp"  class="smallsmoothrectange whitebutton"  style="padding:10px 55px 10px 55px;">    Trading Partner    </a> <br/><br/>'
                                    strHTML += '<br/><a href="Convert' + ControllerName + "?id=" + id + "&ConvertTo=AG&strAction=Convert" + "&" + prevQuerystring + '" id="lnkAG"  class="smallsmoothrectange whitebutton"  style="padding:10px 85px 10px 85px;">    Agent   </a> <br/><br/>'
                                    strHTML += '<br/><a href="Convert' + ControllerName + "?id=" + id + "&ConvertTo=TR&strAction=Convert" + "&" + prevQuerystring + '" id="lnkTR"  class="smallsmoothrectange whitebutton"  style="padding:10px 68px 10px 68px;">    Transporter   </a> <br/>'
                                }
                                if (btn == 'CONVERTVN') {
                                    strHTML += '<br/><a href="Convert' + ControllerName + "?id=" + id + "&ConvertTo=GL&strAction=Convert" + "&" + prevQuerystring + '" id="lnkVN"  class="smallsmoothrectange whitebutton"  style="padding:10px 75px 10px 75px;">General Ledger</a> <br/><br/>'
                                    strHTML += '<br/><a href="Convert' + ControllerName + "?id=" + id + "&ConvertTo=TP&strAction=Convert" + "&" + prevQuerystring + '" id="lnkTP"  class="smallsmoothrectange whitebutton"  style="padding:10px 75px 10px 75px;">Trading Partner</a> <br/><br/>'
                                }
                                if (btn == 'CONVERTAC') {
                                    strHTML += '<br/><a href="Convert' + ControllerName + "?id=" + id + "&ConvertTo=GL&strAction=Convert" + "&" + prevQuerystring + '" id="lnkVN"  class="smallsmoothrectange whitebutton"  style="padding:10px 75px 10px 75px;">General Ledger</a> <br/><br/>'
                                }
                                $(this).html(strHTML);

                            },
                            buttons: [{
                                text: GetLable('lblCancel', 'common', 'Cancel'),
                                click: function () {
                                    $(this).dialog("close");
                                }
                            }]
                        });

                    }

                }
            });
        }
        //end

        if (btn == "CONVERT_TO_INVOICE") {
            if (ControllerName == "SQ") {
                window.location.href = ITX3ResolveUrl('SIASD/CreateDirectInvoice?strAction=C&ConvertFrom=SQ&ConvertFromId=' + id);
            }
            if (ControllerName == "SO") {
                window.location.href = ITX3ResolveUrl('SIASD/CreateDirectInvoice?strAction=C&ConvertFrom=SO&IsAutoBatchScheme=true&ConvertFromId=' + id);
            }
            if (ControllerName == "PO") {
                window.location.href = ITX3ResolveUrl('PIAGR/CreateDirectInvoice?strAction=C&ConvertFrom=PO&ConvertFromId=' + id);
            }
            if (ControllerName == "SD") {
                ConvertDeliveryToInvoice(id);
            }
        }

        if (btn == "CONVERT_TO_DELIVERY") {
            if (ControllerName == "SO") {
                window.location.href = ITX3ResolveUrl('SD/CreateSD_FromSO?strAction=C&ConvertFrom=SO&IsAutoBatchScheme=true&ConvertFromId=' + id);
            }
        }
        if (btn == "CONVERT_TO_PFINVOICE") {
            if (ControllerName == "SO") {
                window.location.href = ITX3ResolveUrl('PF/CreatePF?strAction=C&CallFrom=SO&id=' + id);
            }
            //if (ControllerName == "PO") {
            //    window.location.href = ITX3ResolveUrl('PIAGR/CreateDirectInvoice?strAction=C&ConvertFrom=PO&ConvertFromId=' + id);
            //}
        }
        if (btn == "CONVERT_TO_SO") {
            window.location.href = ITX3ResolveUrl('SO/CreateSO?strAction=C&CallFrom=SQ&id=' + id);
        }
        if (btn == "CONVERT_TO_PO") {
            window.location.href = ITX3ResolveUrl('PO/CreatePO?strAction=C&CallFrom=PQ&id=' + id);
        }
        if (btn == "CONVERTGateIW_TO_GR") {
            ConvertToGoodsReceipts();
        }
        if (btn == "CONVERTGateIW_TO_GoodsReceive") {
            ConvertToGoodsReceive();
        }
        if (btn == "EDIT_ADDITIONAL_DETAILS") {
            window.location.href = ITX3ResolveUrl('SO/UpdateSOAdditionalDetails?Id=' + id);
        }
    }
    return true;

}

function RowSelectedOrNot_AndChkEditDeleteEXT_View(Id, btn, ControllerName, strMsg) {
    var i = 0; var flag = true;
    var id = "";
    if (strMsg == undefined || strMsg == "") { strMsg = " You can not edit/delete primary records "; }

    id = (btn == "D" ? "'" : "") + Id + (btn == "D" ? "'" : "");

    if (flag == true) {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl(ControllerName + '/CheckEditDelete'),
            data: JSON.stringify({ id: Id, Action: btn }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var RecordCount = 1;
                if (data.returnVal >= 0) { RecordCount = data.returnVal; }
                if (data.MSG) { strMsg = data.MSG; }
                if (RecordCount >= 1) {
                    flag = false;
                    $('<div title="Alert" style=padding-left:10px></div>').dialog({
                        open: function (event, ui) {
                            $(this).html(strMsg);
                        },
                        buttons: [{
                            text: GetLable('lblOK', 'common', 'OK'),
                            click: function () {
                                $(this).dialog("close");
                            }
                        }]
                    });
                    return false;
                }
            },
            error: function (xhr, ret, e) {
                alert('some error occured');
                return false;
            }
        });
    }
    else
        return false;


    if (flag == true) {
        var prevQuerystring = "";
        prevQuerystring = window.location.search;
        prevQuerystring = prevQuerystring.replace('?', '');
        if (btn == 'E') {
            $("#lnkEdit").attr("href", "Create" + ControllerName + "?id=" + id + "&strAction=E&" + prevQuerystring);
        }

        if (btn == 'D') {
            $.ajax({
                cache: false,
                type: "POST",
                async: false,
                url: ITX3ResolveUrl('Common/CheckUserPermissions'),
                data: JSON.stringify({ TransactionName: ControllerName, TransactionMode: 'D', prevQuerystring: prevQuerystring }),
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
                                        buttons: [{
                                            text: GetLable('lblOK', 'common', 'OK'),
                                            click: function () {
                                                $(this).dialog("close");
                                            }
                                        }]
                                    });
                                } else {
                                    DeleteRecord(id, ControllerName, "V");
                                }

                            }
                        });


                    }

                }
            });

        }
        if (btn == "CONVERT_TO_INVOICE") {
            if (ControllerName == "SQ") {
                window.location.href = ITX3ResolveUrl('SIASD/CreateDirectInvoice?strAction=C&ConvertFrom=SQ&ConvertFromId=' + id);
            }
            if (ControllerName == "SO") {
                window.location.href = ITX3ResolveUrl('SIASD/CreateDirectInvoice?strAction=C&ConvertFrom=SO&ConvertFromId=' + id);
            }
            if (ControllerName == "PO") {
                window.location.href = ITX3ResolveUrl('PIAGR/CreateDirectInvoice?strAction=C&ConvertFrom=PO&ConvertFromId=' + id);
            }
        }
    }
    return true;
}

function ClearAllControls() {
    $(':input')
           .not(':button', ':submit', ':reset', ':hidden')
            .not(':checkbox')
            .not(':radio')
           .val('')
           .val('').trigger("change")
           .removeAttr('checked')
           .removeAttr('selected');


    $(':input[type=checkbox]')
        .prop('checked', false);

}

function GetLable(id, form, msg) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl('Common/getResourceLable'),
        data: JSON.stringify({ strLableId: id, strResourceFileName: form, DefaultLabelId: msg }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //$("#lblRequiredLayout").text(data);
            Title = data;
        },
        error: function (xhr, ret, e) {
            $("#lblRequiredLayout").text(e);
            Title = e;
        }
    });
    return Title;
}