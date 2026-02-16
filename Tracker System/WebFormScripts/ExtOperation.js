
var Title = "";
var dispGridHelp = "Filter records using =(equals), +(starts with), -(ends with), *(contains), !(doesn't contain)";
selectedExt = new Array();


var selectedExt = new Array();
var selectedExtRow = new Array();
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
            Title = data;
        },
        error: function (xhr, ret, e) {
            $("#lblRequiredLayout").text(e);
            Title = e;
        }
    });
    return Title;
}