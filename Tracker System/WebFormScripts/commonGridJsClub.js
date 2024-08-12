function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}

function deleteClick(id) {
    if ($("#" + id).click()) {

       
        //var Spouse = $("#gridSp").data("kendoGrid");
        //Spouse = Spouse._data;

        //if (Spouse.length > 0) {
        //    $("#btnAdds").addClass("DN");
        //}
        //else {
        //    $("#btnAdds").removeClass("DN");
        //}
    }
}

function deleteSelectedClick(id, name, code) {
    debugger;
    if ($("#" + id).click()) {

        debugger;      
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl('MemberShipManagement/DeleteSelectedMember'),
            data: JSON.stringify({ MembershipCode: code, MemberFrom: name }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data == "-1") {
                    AlertDialog("", "Member Not Deleted");
                }               
            },
            error: function (xhr, ret, e) {
                return false;
            }
        });
    }
}

function GetSelectEditDelete(a) {
    var hiddenid = a.attributes["hiddenid"].value;
    var codeorid = a.attributes["code"].value;
    if (a.checked) {
        $("#" + hiddenid).val($("#" + hiddenid).val() + "'" + codeorid + "',");
    }
    else {
        $("#" + hiddenid).val($("#" + hiddenid).val().replace("'" + codeorid + "',", ""));
    }
}

//function ShowErroBox(Message) {
//    if ($("#divFooterMsg")[0].attributes["current"].value == "hide") {
//        $("#divFooterMsg").slideToggle().slideDown("slow");
//        $("#divFooterMsg").removeClass("DN");
//        $("#divFooterMsg")[0].attributes["current"].value = "show";
//    }
//    else { $("#divFooterMsg").slideToggle().slideToggle(); }
//    $("#divFooterMsg div:first").html('');
//    $("#divFooterMsg div:first").html(Message);
//}

function ShowErroBox(Message) {
    var msgContain = "";
    msgContain = "<div id='tempMsg' title='Error Message'  class='k-animation-container' style='height: 36px; position: fixed;bottom: 25px; z-index: 10005; left: 180px;'>";
    msgContain += "<div class='k-widget k-notification k-notification-error k-notification-button k-popup k-group k-reset k-state-border-up' style='position: absolute;'>";
    msgContain += " <div class='k-notification-wrap'>";
    msgContain += "<span class='k-icon k-i-note'>error</span>" + Message + " <span class='k-icon k-i-close' onclick='hideErrorMessage()' title='Close Message'>Hide</span></div>       </div>       </div>";
    $("#setUpdateMsg").html(msgContain);
}




$(".emc").click(function () { hideErrorMessage(); });

function hideErrorMessage() {

    //var Spouse = $("#gridSp").data("kendoGrid");
    //Spouse = Spouse._data;

    //if (Spouse.length > 1) {
    //    $("#btnAdds").addClass("DN");
    //}
    //else {
    //    $("#btnAdds").removeClass("DN");
    //}

    $("#setUpdateMsg").html("");
    $("#divFooterMsg").fadeOut();
    $("#divFooterMsg")[0].attributes["current"].value = "hide"
}

function editthis(a) {
    var hidenid = a.attributes["hidenid"].value;
    var url = a.attributes["url"].value;

    if (hidenid) {
        if ($("#" + hidenid).val()) {
            var IdOrCode = $("#" + hidenid).val().slice(0, -1).split(",");
            if (IdOrCode.length > 1) {
                ShowErroBox('You can edit only one record.');
            }
            else {
                window.location.href = ITX3ResolveUrl(url + "&Id=" + IdOrCode[0].replace("'", "").replace("'", ""));
            }
        }
        else {
            ShowErroBox('Please select record for edit.');
        }
    }
}

function editthispop(a) {
    var hidenid = a.attributes["hidenid"].value;
    var url = a.attributes["url"].value;

    if (hidenid) {
        if ($("#" + hidenid).val()) {
            var IdOrCode = $("#" + hidenid).val().slice(0, -1).split(",");
            if (IdOrCode.length > 1) {
                ShowErroBox('You can edit only one record.');
            }
            else {
                var lastrowCount = $("#" + IdOrCode[0].replace("'", "").replace("'", ""))[0].parentElement.parentElement.childElementCount;
                var edit = $("#" + IdOrCode[0].replace("'", "").replace("'", ""))[0].parentElement.parentElement;
                var grid = $("#grid").getKendoGrid();
                var item = grid.dataItem(edit);
                EditRow = item;
                edit = edit.children[lastrowCount - 1];

                $(edit)[0].childNodes[0].click();
                $("#" + a.attributes["hidenid"].value).val("");
                hideErrorMessage();
                if ($(".k-grid-update")) {
                    $(".k-grid-update").attr("onclick", "return CheckValidation();");
                    $(".k-grid-update").switchClass("k-grid-update", "changeEdit");
                    $(".changeEdit").removeAttr("Id");
                    $(".changeEdit").attr("Id", "btnPopUpdate");
                    $(".k-grid-cancel").attr("onclick", "hideErrorMessage();");

                }
            }
        }
        else {
            ShowErroBox('Please select record for edit.');
        }
    }
}

function approvethis(a) {
    var hidenid = a.attributes["hidenid"].value;
    var url = a.attributes["url"].value;
    var gridId = a.attributes["gridid"].value;
    var IdOrCode = $("#" + hidenid).val().slice(0, -1).split(",");
    if (IdOrCode.length > 1) {
        ShowErroBox('You can approve only one record.');
    }
    else {
        if ($.trim(IdOrCode)) {
            var checkStatus = $("#" + gridId + " input[class=ac]:checked")[0].attributes["status"].value;
            if (checkStatus == "APPROVED") {
                ShowErroBox('You can not approve this record.');
            }
            else {

                window.location.href = ITX3ResolveUrl(url + "&Id=" + IdOrCode[0].replace("'", "").replace("'", ""));
            }
        }
        else {
            ShowErroBox('Please select record for approve.');
        }
    }
}

function rejectThis(a) {
    var hidenid = a.attributes["hidenid"].value;
    var url = a.attributes["url"].value;
    var gridId = a.attributes["gridid"].value;
    var IdOrCode = $("#" + hidenid).val().slice(0, -1).split(",");
    if (IdOrCode.length > 1) {
        ShowErroBox('You can approve only one record.');
    }
    else {
        if ($.trim(IdOrCode)) {
            var checkStatus = $("#" + gridId + " input[class=ac]:checked")[0].attributes["status"].value;
            if (checkStatus == "APPROVED" || checkStatus == "REJECT") {
                ShowErroBox('You can not reject this record.');
            }
            else {

                window.location.href = ITX3ResolveUrl(url + "&Id=" + IdOrCode[0].replace("'", "").replace("'", ""));
            }
        }
        else {
            ShowErroBox('Please select record for approve.');
        }
    }
}





function GetTooltipContent(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /> ";
}

function GetTooltipContentForInquiry(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /><span  title='View' onclick=ViewClick('" + editbtnId + "') class='fa fa-eye ic' ></span> ";
}

function GetTooltipContentForConvertTomember(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    if (item.IsConvertedToMember == 'false' && item.BasicType == 'CloseWon') {
        return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /><span  title='View' onclick=ViewClick('" + editbtnId + "') style='margin-left:5px;' class='fa fa-eye ic' ></span> <span  title='Convert To Member' onclick=convertToMember('" + editbtnId + "') class='fa fa-mail-forward ic CP' ></span> ";
    }
    else {
        return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /><span  title='View' onclick=ViewClick('" + editbtnId + "') style='margin-left:8px;' class='fa fa-eye ic' ></span> ";
    }
}

function GetTooltipContentForApproveMember(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    if (item.Status == "APPROVALPENDING") {
        return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /> <span title='View' onclick=View('" + editbtnId + "')  class='fa fa-eye ic'/>  <span  title='Approve' onclick=ApproveMember('" + editbtnId + "') class='fa fa-mail-forward ic CP' ></span> ";
    }
    else {
        return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /> <span title='View' onclick=View('" + editbtnId + "')  class='fa fa-eye ic'/>";
    }
}

function GetTooltipContentForApproveMemberNot(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;

    return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /> <span title='View' onclick=View('" + editbtnId + "')  class='fa fa-eye ic'/>";

}

function GetTooltipContentInPage(e, ColoumNumber, GridId) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#" + GridId).getKendoGrid();
    var item = grid.dataItem(row);
    var editbtnId = item.uid;
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + editbtnId;
    row[0].cells[ColoumNumber].children[1].id = "delete" + editbtnId;

    //if (GridId == "gridSp") {
    //    var Spouse = $("#" + GridId).data("kendoGrid");
    //    Spouse = Spouse._data;
    //    if (Spouse.length > 0) {
    //        $("#btnAdds").addClass("DN");
    //    }
    //    else {
    //        $("#btnAdds").removeClass("DN");
    //    }
    //}
   
    return " <span onclick=deleteClick('" + "delete" + item.uid + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + "edit" + editbtnId + "') class='k-icon k-edit CP' /> ";
} 

function GetTooltipContentInPageDelete(e, ColoumNumber, GridId) {
    debugger;
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#" + GridId).getKendoGrid();
    var item = grid.dataItem(row);
    var editbtnId = item.uid;
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + editbtnId;
    row[0].cells[ColoumNumber].children[1].id = "delete" + editbtnId;
    
    debugger;
    return " <span onclick=deleteSelectedClick('" + "delete" + item.uid + "','" + GridId + "','" + item.MembershipCode + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + "edit" + editbtnId + "') class='k-icon k-edit CP' /> ";
}

function GetTooltipUpdate(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    return " <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /> ";
}

function GetTooltipWithView(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    return " <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /> ";
}

function setPopupDimensions(width) {
    $(".k-edit-form-container").parent().width(width).data("kendoWindow").center();
    $(".k-edit-form-container").addClass("W100P");
    $("#UpdatePopUp").addClass("W90");
    var ad = $(".k-edit-form-container");
    ad[0].children[1].children[0].id = "btnPopUpdate";
    $("#btnPopUpdate").attr("onclick", "javascript: return CheckValidation();");
    $("#btnPopUpdate").removeClass("k-grid-update");

}

function setPopupDimensionsPageGrid(width) {
    $(".k-edit-form-container").parent().width(width).data("kendoWindow").center();
    $(".k-edit-form-container").addClass("W100P");
    $("#UpdatePopUp").addClass("W90");
    var ad = $(".k-edit-form-container");
    ad[0].lastElementChild.children[0].id = "btnPopUpdate";
    $("#btnPopUpdate").attr("onclick", "javascript: return CheckValidation();");
    $("#btnPopUpdate").removeClass("k-grid-update");

}

function CheckValidation() {
    if (IsValidInputEdit()) {
        $("#btnPopUpdate").addClass("k-grid-update");
        $("#btnPopUpdate").attr("onclick", "javascript: return false");
        $("#btnPopUpdate").click();
        ShowAllMsg();
        return true;
    }
    else { return false; }
}

function ShowHide() {
    var divCon = document.getElementById('divAddContent');
    if (divCon.className == "DB") {
        $("#divAddContent").slideToggle("slow");
        $("#divAddContent").removeClass("DB");
        $("#divAddContent").addClass("DN");
        $("#divShowHide").removeClass("DN");
        // $("#btnAdd").addClass("DB");
    }
    else {
        $("#divAddContent").slideToggle(1);
        $("#divAddContent").slideToggle(0);
        $("#divAddContent").slideToggle(800);
        $("#divAddContent").removeClass("DN");
        $("#divAddContent").addClass("DB");
        $("#divShowHide").removeClass("DB");
        $("#divShowHide").addClass("DN");
        $("#Id").focus();
    }
}

function HideToggel() {
    ShowHide();
    Clear();
}
function labels(id, form, msg) {
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
function CheckSubmit() {

    if (IsValidInput()) {
        if ($("#MembershipCode").val() != "" && $("#strAction").val() == "E") {
            labels('MailConfirmsEmailsMember', 'common', 'Member details modified,do you want to notify by email');
            $("#dialog-RequiredLayout").dialog({
                title: "Confirm",
                resizable: false,
                height: 170,
                modal: true,
                buttons: {
                    "Yes": function () {
                        $(this).dialog("close");
                        $("#Ismail").val(true);
                        $('form').submit();
                        return true;
                    },
                    "No": function () {
                        $(this).dialog("close");
                        $("#Ismail").val(false);
                        $('form').submit();
                        return true;
                    }

                }
            });
        }
        else {
             $('form').submit();
        }
    }
    else {
        return false;
    }
}

function Submit() {

    if (IsValidInputSubmit()) {
        $('form').submit();
    }
    else {
        return false;
    }
}

function CheckSubmitApprove() {
    if (IsValidInput()) {
        $("#strAction").val("APPROVE");
        $('form').submit();
    }
    else {
        return false;
    }
}

function CheckSubmitApproveUpdate() {
    if (IsValidInput()) {
        $("#strAction").val("E");
        $('form').submit();
    }
    else {
        return false;
    }
}

function GetTooltipContentForUserAdmin(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /><span  title='Assign Permission' onclick=AssignPermissionClick('" + editbtnId + "') class='fa fa-lock ic AssignIconShow' /> ";
}

function GetTooltipContentForUser(e, ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' title='Delete' /> <a type='button' title='Edit' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' />";
}

function AddCheckAll(gridId, hiddenid) {
    $("#" + gridId + " th:first").html("<input type='checkbox' class='gridac' style='margin-left:-4px;' hiddenid=" + hiddenid + " gridid=" + gridId + " onchange=CheckUnCheckAll(this); />");
}

function CheckUnCheckAll(a) {
    var gridid = a.attributes["gridid"].value;
    var hiddenid = a.attributes["hiddenid"].value;
    var isChecked = a.checked;
    $("#" + gridid + " .ac").each(function () {
        $(this).prop("checked", isChecked);
        GetSelectEditDelete(this);
    });
}