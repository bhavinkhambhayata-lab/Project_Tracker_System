//$IV = IsValid
//i = Id
//err = Display None 
function ShowAutoCompleteValidationMsg() {
    $(".ReqHerrAuto").each(function () { var i = $(this)[0].id; if ($(this).val().trim() == "") { $("#" + i + "_ReqHerrAuto").removeClass("err"); } else { $("#" + i + "_ReqHerrAuto").addClass("err"); } });
    $(".ReqtxtAuto").each(function () { var i = $(this)[0].id; if ($(this).val().trim() == "") { $("#" + i + "_ReqtxtAuto").removeClass("err"); } else { $("#" + i + "_ReqtxtAuto").addClass("err"); } });
    $(".Reqdrp").each(function () { if ($(this)[0].tagName == "SPAN") { var Id = $(this)[0].children[1].id; if ($(this)[0].textContent == "---Select---select") { $("#" + Id + "_Reqdrp").removeClass("err"); } else { $("#" + Id + "_Reqdrp").addClass("err"); } } });
    if ($("#strAction").val() == "FromInquiery") {
        HideMsgTemp();
    }
}

function ShowAllValidationMsg() {
    var $IV = true;
    //    $(".ReqHerrAuto").each(function () {
    //        var i = $(this)[0].id;
    //        if (i) {
    //            if ($(this).val().trim() == "") {
    //                $("#" + i + "_ReqHerrAuto").removeClass("err");
    //                $IV = false;
    //            } else { $("#" + i + "_ReqHerrAuto").addClass("err"); }
    //        }
    //    });
    $(".ReqHdnAuto").each(function () {
        var i = $(this)[0].id;
        if (i) {
            if ($(this).val().trim() == "") {
                $("#" + i)[0].parentElement.attributes["class"].value = $("#" + i)[0].parentElement.attributes["class"].value + " errc";
                $IV = false;
            } else {
                $("#" + i)[0].parentElement.attributes["class"].value = $("#" + i)[0].parentElement.attributes["class"].value.replace("errc", "");
            }
        }
    });
    $(".ReqtxtAuto").each(function () {
        var i = $(this)[0].id; if ($(this).val().trim() == "") {
            $("#" + i + "_ReqtxtAuto").addClass("errc"); $IV = false;
        }
        else { $("#" + i + "_ReqtxtAuto").removeClass("errc"); }
    });
    $(".ReqdrpAuto").each(function () {
        if ($(this)[0].tagName == "SPAN") {
            var i = $(this)[0].children[1].id; if ($(this)[0].textContent == "---Select---select" || $(this)[0].textContent == "Selectselect") {
                $("#" + i).addClass("err"); $IV = false;
            }
            else { $("#" + i).removeClass("err"); }
        }
    });

    $(".Reqtxt").each(function () {
        var i = $(this)[0].id; if ($(this).val().trim() == "") {
            $("#" + i).addClass("err"); $IV = false;
        }
        else { $("#" + i).removeClass("err"); }
    });

    $(".ReqRadio").each(function () {        
        var i = $(this)[0].id; if ($("input[name='IsDeferralPayment']:checked").val() == "" || $("input[name='IsDeferralPayment']:checked").val() == undefined) {
            $("#" + i).addClass("err"); $IV = false;
        }
        else { $("#" + i).removeClass("err"); }
    });

    $(".Reqdrp").each(function () {
        if ($(this)[0].tagName == "SPAN") {
            var i = $(this)[0].children[1].id; if ($(this)[0].textContent == "---Select---select" || $(this)[0].textContent == "Selectselect") {
                $("#" + i + "_Reqdrp").addClass("errb"); $IV = false;
            }
            else { $("#" + i + "_Reqdrp").removeClass("errb"); }
        }
    });

    $(".ReqDate").each(function () {
        if ($(this)[0].tagName == "SPAN") {
            var i = $(this)[0].children[0].children[0].id;
            if ($("#" + i).val().trim() == "") {
                $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value + " errc ";
                $IV = false;
            }
            else {
                $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value.replace("errc", "");
            }
        }
    });

    if (!$IV) {
        ShowErroBox('Required information are missing.');
        return $IV;
    }


    $(".ReqEmailValidation").each(function () {
        var i = $(this)[0].id;
        var x = $(this).val().trim();
        if (x.trim()) {
            if (isValidEmailAddress(x)) {
                $("#" + i).removeClass("err");
            } else {
                $("#" + i).addClass("err");
                $IV = false;
            }
        }
        else {
            $("#" + i).removeClass("err")
        }
    });


    if (!$IV) {
        ShowErroBox('Invalid email information.');
        return $IV;
    }


    $(".ReqDateVali").each(function () {
        if ($(this)[0].tagName == "SPAN") {
            var i = $(this)[0].children[0].children[0].id; if ($("#" + i).val().trim() != "") {
                var IsValid = ValidDateValidation(i);
                if (!IsValid) {
                    $IV = false;
                    $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value + " errc ";
                }
                else {
                    $("#" + i).removeClass("err");
                }
            }
        }
    });

    if (!$IV) {
        ShowErroBox('Invalid date.');
        return $IV;
    }

    $(".InqReqDateCompare").each(function () {
        if ($(this)[0].tagName == "SPAN") {
            var i = $(this)[0].children[0].children[0].id;
            var todaysDate = $(this)[0].children[0].children[0].attributes[7].value;
            var Previousdate = $(this)[0].children[0].children[0].attributes[8].value;

            if ($("#" + i).val().trim() != Previousdate.trim()) {
                if ($("#" + i).val().trim() != todaysDate.trim()) {
                    $("#" + i).addClass("err");
                    $("#" + i).val('');
                    $IV = false;
                }
                else {
                    $("#" + i).removeClass("err");
                }
            }
            else {
                $("#" + i).removeClass("err");
            }
        }
    });

    $(".ReqHerr").each(function () {
        var i = $(this)[0].id; if (i) {
            if ($(this).val().trim() == "") {
                $("#" + i).addClass("err"); $IV = false;
            }
            else { $("#" + i).removeClass("err"); }
        }
    });

    $(".CompToTxt").each(function () { var i = $(this)[0].id; var ci = $(this)[0].attributes.compertto.value; if ($("#" + i).val().trim()) { if ($("#" + i).val().trim() != $("#" + ci).val().trim()) { $("#" + i + "_CompToTxt").removeClass("err"); $IV = false; } else { $("#" + i + "_CompToTxt").addClass("err"); } } });
    if ($IV) {
        hideErrorMessage();
    }

    return $IV;
}
function ShowPopUpValidationMsg() {

    var $IV = true;

    $(".Reqpoptxt").each(function () {
        var i = $(this)[0].id;
        if ($(this).val().trim() == "") {
            $("#" + i).addClass("err");
            $IV = false;
        }
        else {
            $("#" + i).removeClass("err");
        }
    });

    $(".Reqpopdrp").each(function () {
        if ($(this)[0].tagName == "SPAN") {
            var i = $(this)[0].children[1].id;
            if ($(this)[0].textContent == "---Select---select" || $(this)[0].textContent == "Selectselect") {
                $("#" + i + "_Reqpopdrp").addClass("errb");
                $IV = false;
            }
            else {
                $("#" + i + "_Reqpopdrp").removeClass("errb");
            }
        }
    });

    $(".ReqpopDate").each(function () {
        if ($(this)[0].tagName == "SPAN") {
            var i = $(this)[0].children[0].children[0].id;
            if ($("#" + i).val().trim() == "") {
                $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value + " errc ";
                $IV = false;
            }
            else {
                $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value.replace("errc", "");
            }
        }
    });

    $(".ReqpopHerr").each(function () {
        var i = $(this)[0].id; if ($(this).val().trim() == "") {
            $("#" + i + "_ReqpopHerr").addClass("err");
            $IV = false;
        }
        else {
            $("#" + i + "_ReqpopHerr").removeClass("err");
        }
    });


    $(".ReqHdnAutoPop").each(function () {
        var i = $(this)[0].id;
        if (i) {
            if ($(this).val().trim() == "") {
                $("#" + i)[0].parentElement.attributes["class"].value = $("#" + i)[0].parentElement.attributes["class"].value + " errc";
                $IV = false;
            } else {
                $("#" + i)[0].parentElement.attributes["class"].value = $("#" + i)[0].parentElement.attributes["class"].value.replace("errc", "");
            }
        }
    });

    if (!$IV) {
        ShowErroBox('Required information are missing.');
        return $IV;
    }


    $(".ReqEmailValidationPopUp").each(function () {
        var i = $(this)[0].id; var x = $(this).val().trim();
        if (x.trim()) {
            if (isValidEmailAddress(x)) {
                $("#" + i).removeClass("err");
            }
            else {
                $("#" + i).addClass("err");
                $IV = false;
            }
        }
        else {
            $("#" + i).removeClass("err");
        }
    });
    if (!$IV) {
        ShowErroBox('Invalid email information.');
        return $IV;
    }

    //    $(function () {
    //        $(".Reqpoptxt").change(function () {
    //            var i = $(this)[0].id;
    //            if ($(this).val().trim() == "") {
    //                $("#" + i + "_Reqpoptxt").addClass("err");
    //            }
    //            else {
    //                $("#" + i + "_Reqpoptxt").removeClass("err");
    //            }
    //        });
    //    });

    //    $(function () {
    //        $(".Reqpopdrp").change(function () {
    //            var i = $(this)[0].id; if (!$(this).val()) {
    //                $("#" + i + "_Reqpopdrp").removeClass("err");
    //            }
    //            else {
    //                $("#" + i + "_Reqpopdrp").addClass("err");
    //            }
    //        });
    //    });

    //    $(function () {
    //        $(".ReqpopDate").change(function () {
    //            var i = $(this)[0].id;
    //            if ($("#" + i).val() == "") {
    //                $("#" + i + "_ReqpopDate").removeClass("err");
    //            }
    //            else {
    //                $("#" + i + "_ReqpopDate").addClass("err");
    //            }
    //        });
    //    });

    if (!$IV) {
        ShowErroBox('Required information are missing.');
        return $IV;
    }
    $(".ReqDateValidation").each(function () {

        if ($(this)[0].tagName == "SPAN") {
            var i = $(this)[0].children[0].children[0].id;
            if ($("#" + i).val().trim() == "") {
                var IsValid = ValidDateValidation(i);
                if (!IsValid) {
                    $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value + " errc ";
                    $IV = false;
                }
                else {
                    $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value.replace("errc", "");
                }
            }
            else {
                $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value.replace("errc", "");
            }
        }
    })
    if (!$IV) {
        ShowErroBox('Invalid date.');
        return $IV;
    }




    //    $(function () {
    //        $(".ReqDateValidation").change(function () {
    //            var i = $(this)[0].id; if ($("#" + i).val().trim() != "") {
    //                var IsValid = ValidDateValidation(i);
    //                if (!IsValid) {
    //                    $("#" + i + "_ReqDateVali").removeClass("err");
    //                } else { $("#" + i + "_ReqDateVali").addClass("err"); }
    //            }
    //            else {
    //                $("#" + i + "_ReqDateVali").addClass("err");
    //            }
    //        })
    //    });

    if ($IV) {
        hideErrorMessage();
    }
    return $IV;
}
//$(function () {
//    $(".Reqtxt").change(function () {
//        var i =
// $(this)[0].id;
//        if ($(this).val().trim() == "") {
//            $("#" + i).addClass("err");
//        }
//        else {
//            $("#" + i).removeClass("err");
//        }
//    });
//});
//$(function () { $(".CompToTxt").change(function () { var i = $(this)[0].id; var CI = $(this)[0].attributes.compertto.value; if ($("#" + i).val() && $("#" + CI).val()) { if ($("#" + i).val() != $("#" + CI).val()) { $("#" + i + "_CompToTxt").removeClass("err"); } else { $("#" + i + "_CompToTxt").addClass("err"); } } }); });

//$(function () {
//    $(".Reqdrp").change(function () {
//        var i = $(this)[0].id; if (!$(this).val()) {
//            $("#" + i + "_Reqdrp").addClass("errb");
//        }
//        else {
//            $("#" + i + "_Reqdrp").removeClass("errb");
//        }
//    });
//});


//$(".ReqtxtAuto").change(function () {
//    var i = $(this)[0].id; if ($(this).val().trim() == "") {
//        $("#" + i + "_ReqtxtAuto").addClass("errc"); $IV = false;
//    }
//    else { $("#" + i + "_ReqtxtAuto").removeClass("errc"); }
//});

//$(function () {
//    $(".ReqDate").change(function () {
//        var i = $(this)[0].id;
//        if ($("#" + i).val().trim() == "") {
//            $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value + " errc ";
//        } else {
//            $("#" + i)[0].parentNode.attributes["class"].value = $("#" + i)[0].parentNode.attributes["class"].value.replace("errc", "");
//        }
//    });
//});

//$(function () { $(".ReqHerr").change(function () { var i = $(this)[0].id; if (i) { if ($(this).val().trim() == "") { $("#" + i + "_ReqHerr").removeClass("err"); } else { $("#" + i + "_ReqHerr").addClass("err"); } } }); });
//$(function () { $(".ReqDateVali").change(function () { var i = $(this)[0].id; if ($("#" + i).val().trim() != "") { var IsValid = ValidDateValidation(i); if (!IsValid) { $("#" + i + "_ReqDateVali").removeClass("err"); } else { $("#" + i + "_ReqDateVali").addClass("err"); } } else { $("#" + i + "_ReqDateVali").addClass("err"); } }) });
//$(function () { $(".ReqEmailValidation").change(function () { var i = $(this)[0].id; var x = $(this).val().trim(); if (x.trim()) { if (isValidEmailAddress(x)) { $("#" + i + "_ReqEmailValidation").addClass("err"); } else { $("#" + i + "_ReqEmailValidation").removeClass("err"); $IV = false; } } else { $("#" + i + "_ReqEmailValidation").addClass("err"); } }); });

//$i= Text Box Id// $ND= New Date// $D = Day ,$M = Month ,$Y=Year $("#" + Id).ForceNumericOnly();
function ValidDateValidation(Id) {
    try {
        $i = $("#" + Id).val().substr(0, 10);
        if ($i) {
            var $ND = $i.split('/');
            var $D = $ND[0];
            var $M = $ND[1];
            var $Y = $ND[2];

            if ($D == "0" || $D == "00") {
                return false;
            }
            if ($M == "0" || $M == "00") {
                return false;
            }
            if ($Y == "0" || $Y == "00" || $Y == "000" || $Y == "0000") {
                return false;
            }
            if ($M.length == "2") {
                $M = $M.replace("0", "");
            }
            if ($ND.length != 3) {
                return false;
            }
            if (parseInt($M) > 12) {
                return false;
            }
            if (parseInt($Y.length) != 4) {
                return false;
            }
            if (parseInt($D.length) > 2) {
                return false;
            }
            else {
                if ($M == "1" || $M == "3" || $M == "5" || $M == "7" || $M == "8" || $M == "10" || $M == "12") {
                    if (parseInt($ND[0]) > 31) {
                        return false;
                    }
                }
                else if ($M == "4" || $M == "6" || $M == "9" || $M == "11") {
                    if (parseInt($ND[0]) > 30) {
                        return false;
                    }
                }
                else if ($M == "2") {
                    if ((parseInt($Y) % 4 == 0 && parseInt($Y) % 100 != 00) || parseInt($Y) % 400 == 0) {
                        if (parseInt($D) > 29) {
                            return false;
                        }
                    }
                    else {
                        if (parseInt($D) > 28) {
                            return false;
                        }
                    }
                }
            }
        }
        return true;

    }
    catch (e) {
        return false;
    }
}

function PopUpDateValidation(i) {
    if ($("#" + i).val().trim() != "") {
        var IsValid = ValidDateValidation(i);
        if (!IsValid) {
            $("#" + i + "_ReqDateValidation").removeClass("err");
        }
        else {
            $("#" + i + "_ReqDateValidation").addClass("err");
        }
    }
    else { $("#" + i + "_ReqDateValidation").addClass("err"); }
}
function PopUpEmailValidation(i) { var x = $("#" + i).val().trim(); if (x.trim()) { if (isValidEmailAddress(x)) { $("#" + i + "_ReqEmailValidationPopUp").addClass("err"); } else { $("#" + i + "_ReqEmailValidationPopUp").removeClass("err"); } } else { $("#" + i + "_ReqEmailValidationPopUp").addClass("err"); } }
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        return true;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function isMobilevalid(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 43 || charCode == 45) {
        return true;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function onlyAlphabets(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else {
            return true;
        }
        if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32 || charCode == 0 || charCode == 8) {
            return true;
        }
        else {
            return false;
        }
    } catch (err) {
        alert(err.Description);
    }
}
function myfunction(FromYearId, FromMonthId, ToYearId, ToMonthId) {

    var FromYear = $("#" + FromYearId).val();
    var FromMonth = $("#" + FromMonthId).val();
    var ToYear = $("#" + ToYearId).val();
    var ToMonth = $("#" + ToMonthId).val();
}

function MonthName(Monthno) {
    if (Monthno == "1") {
        return "Jan";
    }
    else if (Monthno == "2") {
        return "Feb";
    }
    else if (Monthno == "3") {
        return "Mar"
    }
    else if (Monthno == "4") {
        return "Apr";
    }
    else if (Monthno == "5") {
        return "May";
    }
    else if (Monthno == "6") {
        return "Jun";
    }
    else if (Monthno == "7") {
        return "Jul";
    }
    else if (Monthno == "8") {
        return "Aug";
    }
    else if (Monthno == "9") {
        return "Sep";
    }
    else if (Monthno == "10") {
        return "Oct";
    }
    else if (Monthno == "11") {
        return "Nov";
    }
    else if (Monthno == "12") {
        return "Dec";
    }
}


function MonthNo(Monthno) {
    if (Monthno == "Jan") {
        return "01";
    }
    else if (Monthno == "Feb") {
        return "02";
    }
    else if (Monthno == "Mar") {
        return "03"
    }
    else if (Monthno == "Apr") {
        return "04";
    }
    else if (Monthno == "May") {
        return "05";
    }
    else if (Monthno == "Jun") {
        return "06";
    }
    else if (Monthno == "Jul") {
        return "07";
    }
    else if (Monthno == "Aug") {
        return "08";
    }
    else if (Monthno == "Sep") {
        return "09";
    }
    else if (Monthno == "Oct") {
        return "10";
    }
    else if (Monthno == "Nov") {
        return "11";
    }
    else if (Monthno == "Dec") {
        return "12";
    }
}
function OnlyNumberValue(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else {
            return true;
        }
        if (charCode >= 48 && charCode <= 57) {
            return true;
        }
        else {
            return false;
        }
    } catch (err) {
        console.log(err.Description);
    }
}