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
        ShowAllMsg();
    }
}
//function ShowHide() {
//    var divCon = document.getElementById('divAddContent');
//    if (divCon.className == "DB") {
//        $("#divAddContent").removeClass("DB");
//        $("#divAddContent").addClass("DN");
//        $("#btnAdd").removeClass("DN");
//        $("#btnAdd").addClass("DB");
//        $("#divAddContent").slideToggle(600);
//        $("#divAddContent").fadeTo(500, 0.5).fadeTo(500, 1.0);
//    }
//    else {
//        $("#divAddContent").slideToggle(800);
//        $("#divAddContent").removeClass("DN");
//        $("#divAddContent").addClass("DB");
//        $("#btnAdd").removeClass("DB");
//        $("#btnAdd").addClass("DN");
//        $("#divAddContent").fadeTo(500, 0.5).fadeTo(200, 1.0);
//        $("#Code").focus();
//    }
//}
function GetTooltipContent(e,ColoumNumber) {
    EditRow = "";
    var row = e.target.closest("tr");
    var grid = $("#grid").getKendoGrid();
    var item = grid.dataItem(row);
    EditRow = item;
    row[0].cells[ColoumNumber].children[0].id = "edit" + item.id;
    row[0].cells[ColoumNumber].children[1].id = "delete" + item.id;
    var editbtnId = row[0].cells[ColoumNumber].children[0].id;
    return " <span onclick=deleteClick('" + "delete" + item.id + "')  class='k-icon k-delete CP' /> <a type='button' onclick=editClick('" + editbtnId + "') class='k-icon k-edit CP' /> ";
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
    return " <span  title='Assign Permission' onclick=AssignPermissionClick('" + editbtnId + "') class='fa fa-lock ic AssignIconShow' /> ";
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
function setPopupDimensions(width) {
    $(".k-edit-form-container").parent().width(width).data("kendoWindow").center();
    $(".k-edit-form-container").addClass("W100P");
    $("#UpdatePopUp").addClass("W90");
    var ad = $(".k-edit-form-container");
    ad[0].children[1].children[0].id = "btnPopUpdate";
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
    else {return false;}
}
function ShowHide() {
    var divCon = document.getElementById('divAddContent');
    if (divCon.className == "DB") {
        $("#divAddContent").removeClass("DB");
        $("#divAddContent").addClass("DN");
        $("#btnAdd").removeClass("DN");
        $("#divAddContent").slideToggle(300);
    }
    else {
        $("#divAddContent").slideToggle(300);
        $("#divAddContent").removeClass("DN");
        $("#divAddContent").addClass("DB");
        $("#btnAdd").removeClass("DB");
        $("#btnAdd").addClass("DN");
        $("#Id").focus();
        //$("#divAddContent").fadeTo(500, 0.5).fadeTo(200, 1.0);
    }
}
function HideToggel(){
    ShowHide();
    Clear();
}
function CheckSubmit() {
    if (IsValidInput()) {
        $('form').submit();
    }
    else {
        return false;
    }
}