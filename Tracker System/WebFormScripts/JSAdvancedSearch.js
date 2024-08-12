var TodayDate,
    ThisWeekStartDate, ThisWeekEndDate,
    ThisMonthStartDate, ThisMonthEndDate,
    ThisFYStartDate, ThisFYEndDate,
    YesterdayDate,
    PreviousWeekStartDate, PreviousWeekEndDate,
    PreviousMonthStartDate, PreviousMonthEndDate,
    PreviousFYStartDate, PreviousFYEndDate,
    NextMonthStartDate, NextMonthEndDate,
    Quarter1, Quarter2, Quarter3, Quarter4,
    ThisQuaterAt = 3, PreviousQuaterAt = 7,
    SortOrdercnt = 0;

var ButtonList = [];

/*
 0 Today
 1 This Week
 2 This Month
 3 This Financial Year
 4 Yesterday
 5 Previous Week
 6 Previous Month
 7 Previous Financial Year
 8 Next Month
 9 Quarter 1 Previous
 10 Quarter 2 This
 11 Quarter 3
 12 Quarter 4
 -1 Custom
 */

$(document).ready(function () {
    var $myDiv = $('.advancebtnbox');
    if ($myDiv.length) { // if advancebtnbox in page
        // take data from server 
        $.ajax({
            cache: false,
            type: "POST",
            async: true,
            url: ITX3ResolveUrl('Login/GetFilterDetails'),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                TodayDate = data.Today;
                ThisWeekStartDate = data.ThisWeekStart;
                ThisWeekEndDate = data.ThisWeekEnd;
                ThisMonthStartDate = data.startOfMonth;
                ThisMonthEndDate = data.endOfMonth;
                ThisFYStartDate = data.FyFrom;
                ThisFYEndDate = data.FyTo;
                YesterdayDate = data.Yesterday;
                PreviousWeekStartDate = data.PreviousWeekStart;
                PreviousWeekEndDate = data.PreviousWeekEnd;
                PreviousMonthStartDate = data.PreviousMonthStart;
                PreviousMonthEndDate = data.PreviousMonthEnd;
                PreviousFYStartDate = data.lFyFrom;
                PreviousFYEndDate = data.lFyTo;
                NextMonthStartDate = data.NextMonthStart;
                NextMonthEndDate = data.NextMonthEnd;
                Quarter1 = data.Quarter1;
                Quarter2 = data.Quarter2;
                Quarter3 = data.Quarter3;
                Quarter4 = data.Quarter4;

                // buttons start

                let withdate = $($myDiv).data('withdate');
                SortOrdercnt = 0;
                var btn = new Object();
                btn.Id = $.trim("Today");
                if ($.trim(TodayDate) != '' && withdate == 1) btn.Lable = $.trim("Today (" + TodayDate + ")");
                else btn.Lable = $.trim("Today");
                btn.SR = 0;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                var btn = new Object();
                btn.Id = $.trim("ThisWeek");
                if ($.trim(ThisWeekStartDate) != '' && $.trim(ThisWeekEndDate) != '' && withdate == 1) btn.Lable = $.trim("This Week (" + ThisWeekStartDate + " - " + ThisWeekEndDate + ")");
                else btn.Lable = $.trim("This Week");
                btn.SR = 1;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                var btn = new Object();
                btn.Id = $.trim("ThisMonth");
                if ($.trim(ThisMonthStartDate) != '' && $.trim(ThisMonthEndDate) != '' && withdate == 1) btn.Lable = $.trim("This Month (" + ThisMonthStartDate + " - " + ThisMonthEndDate + ")");
                else btn.Lable = $.trim("This Month");
                btn.SR = 2;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                var btn = new Object();
                btn.Id = $.trim("ThisFY");
                if ($.trim(ThisFYStartDate) != '' && $.trim(ThisFYEndDate) != '' && withdate == 1) btn.Lable = $.trim("This Financial Year (" + ThisFYStartDate + " - " + ThisFYEndDate + ")");
                else btn.Lable = $.trim("This Financial Year");
                btn.SR = 3;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                var btn = new Object();
                btn.Id = $.trim("Yesterday");
                if ($.trim(YesterdayDate) != '' && withdate == 1) btn.Lable = $.trim("Yesterday (" + YesterdayDate + ")");
                else btn.Lable = $.trim("Yesterday");
                btn.SR = 4;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                var btn = new Object();
                btn.Id = $.trim("PreviousWeek");
                if ($.trim(PreviousWeekStartDate) != '' && $.trim(PreviousWeekEndDate) != '' && withdate == 1) btn.Lable = $.trim("Previous Week (" + PreviousWeekStartDate + " - " + PreviousWeekEndDate + ")");
                else btn.Lable = $.trim("Previous Week");
                btn.SR = 5;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                btn = new Object();
                btn.Id = $.trim("PreviousMonth");
                if ($.trim(PreviousMonthStartDate) != '' && $.trim(PreviousMonthEndDate) != '' && withdate == 1) btn.Lable = $.trim("Previous Month (" + PreviousMonthStartDate + " - " + PreviousMonthEndDate + ")");
                else btn.Lable = $.trim("Previous Month");
                btn.SR = 6;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                btn = new Object();
                btn.Id = $.trim("PreviousFY");
                if ($.trim(PreviousFYStartDate) != '' && $.trim(PreviousFYEndDate) != '' && withdate == 1) btn.Lable = $.trim("Previous Financial Year (" + PreviousFYStartDate + " - " + PreviousFYEndDate + ")");
                else btn.Lable = $.trim("Previous Financial Year");
                btn.SR = 7;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                btn = new Object();
                btn.Id = $.trim("NextMonth");
                if ($.trim(NextMonthStartDate) != '' && $.trim(NextMonthEndDate) != '' && withdate == 1) btn.Lable = $.trim("Next Month (" + NextMonthStartDate + " - " + NextMonthEndDate + ")");
                else btn.Lable = $.trim("Next Month");
                btn.SR = 8;
                btn.DisplayOrder = SortOrdercnt;
                ButtonList.push(btn);
                SortOrdercnt++;

                var arr1 = Quarter1.split('|');
                btn = new Object();
                btn.Id = $.trim("Quarter1");
                if ($.trim(arr1[0]) != '' && $.trim(arr1[1]) != '' && withdate == 1)
                    if ($.trim(arr1[2]) != '')
                        btn.Lable = $.trim("Quarter (" + arr1[0] + " - " + arr1[1] + ") " + arr1[2]);
                    else
                        btn.Lable = $.trim("Quarter (" + arr1[0] + " - " + arr1[1] + ")");
                else {
                    if ($.trim(arr1[2]) != '') btn.Lable = $.trim(arr1[2] + " Quarter");
                    else btn.Lable = $.trim("Quarter 1");
                }
                btn.SR = 9;
                // btn.DisplayOrder = SortOrdercnt;
                if (arr1[2] != '') {
                    if (btn.Lable.toLowerCase().indexOf('this') != -1) { SetDisplayOrderofButton(ThisQuaterAt); btn.DisplayOrder = ThisQuaterAt; }
                    if (btn.Lable.toLowerCase().indexOf('previous') != -1) { SetDisplayOrderofButton(PreviousQuaterAt); btn.DisplayOrder = PreviousQuaterAt; }
                    ButtonList.push(btn);
                    SortOrdercnt++;
                }

                var arr2 = Quarter2.split('|');
                btn = new Object();
                btn.Id = $.trim("Quarter2");
                if ($.trim(arr2[0]) != '' && $.trim(arr2[1]) != '' && withdate == 1)
                    if ($.trim(arr2[2]) != '')
                        btn.Lable = $.trim("Quarter (" + arr2[0] + " - " + arr2[1] + ") " + arr2[2]);
                    else
                        btn.Lable = $.trim("Quarter (" + arr2[0] + " - " + arr2[1] + ")");
                else {
                    if ($.trim(arr2[2]) != '') btn.Lable = $.trim(arr2[2] + " Quarter");
                    else btn.Lable = $.trim("Quarter 2");
                }
                btn.SR = 10;
                //  btn.DisplayOrder = SortOrdercnt;
                if (arr2[2] != '') {
                    if (btn.Lable.toLowerCase().indexOf('this') != -1) { SetDisplayOrderofButton(ThisQuaterAt); btn.DisplayOrder = ThisQuaterAt; }
                    if (btn.Lable.toLowerCase().indexOf('previous') != -1) { SetDisplayOrderofButton(PreviousQuaterAt); btn.DisplayOrder = PreviousQuaterAt; }
                    ButtonList.push(btn);
                    SortOrdercnt++;
                }

                var arr3 = Quarter3.split('|');
                btn = new Object();
                btn.Id = $.trim("Quarter3");
                if ($.trim(arr3[0]) != '' && $.trim(arr3[1]) != '' && withdate == 1)
                    if ($.trim(arr3[2]) != '')
                        btn.Lable = $.trim("Quarter (" + arr3[0] + " - " + arr3[1] + ") " + arr3[2]);
                    else
                        btn.Lable = $.trim("Quarter (" + arr3[0] + " - " + arr3[1] + ")");
                else {
                    if ($.trim(arr3[2]) != '') btn.Lable = $.trim(arr3[2] + " Quarter");
                    else btn.Lable = $.trim("Quarter 3");
                }
                btn.SR = 11;
                //  btn.DisplayOrder = SortOrdercnt;
                if (arr3[2] != '') {
                    if (btn.Lable.toLowerCase().indexOf('this') != -1) { SetDisplayOrderofButton(ThisQuaterAt); btn.DisplayOrder = ThisQuaterAt; }
                    if (btn.Lable.toLowerCase().indexOf('previous') != -1) { SetDisplayOrderofButton(PreviousQuaterAt); btn.DisplayOrder = PreviousQuaterAt; }
                    ButtonList.push(btn);
                    SortOrdercnt++;
                }

                var arr4 = Quarter4.split('|');
                btn = new Object();
                btn.Id = $.trim("Quarter4");
                if ($.trim(arr4[0]) != '' && $.trim(arr4[1]) != '' && withdate == 1)
                    if ($.trim(arr4[2]) != '')
                        btn.Lable = $.trim("Quarter (" + arr4[0] + " - " + arr4[1] + ") " + arr4[2]);
                    else
                        btn.Lable = $.trim("Quarter (" + arr4[0] + " - " + arr4[1] + ")");
                else {
                    if ($.trim(arr4[2]) != '') btn.Lable = $.trim(arr4[2] + " Quarter");
                    else btn.Lable = $.trim("Quarter 4");
                }
                btn.SR = 12;
                //   btn.DisplayOrder = SortOrdercnt;
                if (arr4[2] != '') {
                    if (btn.Lable.toLowerCase().indexOf('this') != -1) { SetDisplayOrderofButton(ThisQuaterAt); btn.DisplayOrder = ThisQuaterAt; }
                    if (btn.Lable.toLowerCase().indexOf('previous') != -1) { SetDisplayOrderofButton(PreviousQuaterAt); btn.DisplayOrder = PreviousQuaterAt; }
                    ButtonList.push(btn);
                    SortOrdercnt++;
                }

                // buttons end


                var excludebtns = $($myDiv).data('exclude').toLowerCase();

                let fromdateid = $($myDiv).data('fromdateid');
                let todateid = $($myDiv).data('todateid');

                var dataelement = '';
                if (fromdateid != undefined) { dataelement += "data-fromdateid=\"" + fromdateid + "\""; }
                if (todateid != undefined) { dataelement += " data-todateid=\"" + todateid + "\""; }

                // Create buttons on page
                let adsbuttons = "<div class=\"btn-group\"><button type='button' class='btn btn-default dropdown-toggle' data-toggle='dropdown'><span id='spselectedOption'>Custom</span><span class='caret' style=\"margin-left:11px;\"></span></button>";
                let dropdown = '<ul class="dropdown-menu">';
                if (ButtonList.length > 0) {
                    if (excludebtns.indexOf("quarter") != -1) {
                        excludebtns = excludebtns.replace("quarter", "quarter1,quarter2,quarter3,quarter4");
                    }
                    let buttons = excludebtns.split(',');
                    ButtonList.sort((a, b) => a.DisplayOrder - b.DisplayOrder);
                    for (var i = 0; i < ButtonList.length; i++) {
                        var btn = $.grep(buttons, function (n, j) {
                            return $.trim(n).toLowerCase() == $.trim(ButtonList[i].Id).toLowerCase();
                        });
                        if (btn.length <= 0)
                            dropdown += "<li><a href='javascript:;' " + dataelement + " class='adslnk' data-value='" + ButtonList[i].SR + "' data-lable='" + ButtonList[i].Lable + "'>" + ButtonList[i].Lable + "</a></li>";
                    }
                }
                dropdown += '</ul>';
                adsbuttons += dropdown;
                adsbuttons += "</div>";
                $(adsbuttons).appendTo($myDiv);

                $(".adslnk").val('-1'); // Set Custom Option
            },
            error: function (xhr, ret, e) {
                alert('Error occured in fetch financial year details');
            }
        });

        $("#FromDate").change(function () {
            $('#spselectedOption').html('Custom'); // Set Custom Option
        });

        $("#ToDate").change(function () {
            $('#spselectedOption').html('Custom'); // Set Custom Option
        });
    }


    /* Fix Report Header CSS & JS Start */
    var rCss = '.fixtbldiv {width: 100%;border: 1px black solid;padding: 3px 7px 2px 7px;} .fixtbltheadth {padding: 0px !important;border: none !important;} .tableFixHeaddiv {width: 100%;padding: 3px 7px 2px 7px;border-left: 1px #000 solid;border-bottom: 1px #000 solid;position: relative;left: -1px;} .fixhedtd{padding:0px !important;border: none !important;}',
        rHead = document.head || document.getElementsByTagName('head')[0],
        rStyle = document.createElement('style');

    rHead.appendChild(rStyle);
    rStyle.type = 'text/css';

    if (rStyle.styleSheet) {
        // This is required for IE8 and below.
        rStyle.styleSheet.cssText = rCss;
    } else {
        rStyle.appendChild(document.createTextNode(rCss));
    }

    /* Fix Report Header CSS & JS End */
});


function settableFixHead() {
    if ($(".zb_tableFixHead").length > 0) {
        setTimeout(function () {
            var hHeadRHit = 0;
            activeTab('#liReport', '#Report', '#liCriteria', '#Criteria');
            var tableHeight = $('.zb_tableFixHead > table').outerHeight();
            activeTab('#liCriteria', '#Criteria', '#liReport', '#Report');
            var NewtableHeight = ($(window).height() - 70);
            //console.log(tableHeight);
            //console.log(NewtableHeight);
            if (tableHeight > NewtableHeight) {
                $(".zb_tableFixHead > table > thead > tr > td, .zb_tableFixHead > table > thead > tr > th").addClass("fixtbltheadth");
                $(".zb_tableFixHead > table > thead > tr > td, .zb_tableFixHead > table > thead > tr > th").each(function () { $(this).html("<div class='tableFixHeaddiv'>" + $(this).html() + "</div>"); });

                setTimeout(function () {

                    $(".zb_tableFixHead > table > thead > tr:nth-child(1) > td, .zb_tableFixHead > table > thead > tr:nth-child(1) > th").each(function () {
                        var $div = $(this);
                        if (hHeadRHit < $div.closest("td").height()) hHeadRHit = $div.closest("td").height();
                        if (hHeadRHit < $div.closest("th").height()) hHeadRHit = $div.closest("th").height();
                    });
                    $(".zb_tableFixHead > table > thead > tr > td > div, .zb_tableFixHead > table > thead > tr > th > div").each(function () { var $div = $(this); $div.parent().addClass('fixhedtd'); });
                    $(".zb_tableFixHead > table > thead > tr:nth-child(1) > td > div, .zb_tableFixHead > table > thead > tr:nth-child(1) > th > div").each(function () { var $div = $(this); $div.height(hHeadRHit); });

                    // second row
                    hHeadRHit = 0;

                    $(".zb_tableFixHead > table > thead > tr:nth-child(2) > td, .zb_tableFixHead > table > thead > tr:nth-child(2) > th").each(function () {
                        var $div = $(this);
                        if (hHeadRHit < $div.closest("td").height()) hHeadRHit = $div.closest("td").height();
                        if (hHeadRHit < $div.closest("th").height()) hHeadRHit = $div.closest("th").height();
                    });
                    $(".zb_tableFixHead > table > thead > tr > td > div, .zb_tableFixHead > table > thead > tr > th > div").each(function () { var $div = $(this); $div.parent().addClass('fixhedtd'); });
                    $(".zb_tableFixHead > table > thead > tr:nth-child(2) > td > div, .zb_tableFixHead > table > thead > tr:nth-child(2) > th > div").each(function () { var $div = $(this); $div.height(hHeadRHit); });

                    // third row
                    hHeadRHit = 0;
                    $(".zb_tableFixHead > table > thead > tr:nth-child(3) > td, .zb_tableFixHead > table > thead > tr:nth-child(3) > th").each(function () {
                        var $div = $(this);
                        if (hHeadRHit < $div.closest("td").height()) hHeadRHit = $div.closest("td").height();
                        if (hHeadRHit < $div.closest("th").height()) hHeadRHit = $div.closest("th").height();
                    });
                    $(".zb_tableFixHead > table > thead > tr:nth-child(3) > td > div, .zb_tableFixHead > table > thead > tr:nth-child(3) > th > div").each(function () { var $div = $(this); $div.height(hHeadRHit); });

                    // forth row
                    hHeadRHit = 0;
                    $(".zb_tableFixHead > table > thead > tr:nth-child(4) > td, .zb_tableFixHead > table > thead > tr:nth-child(4) > th").each(function () {
                        var $div = $(this);
                        if (hHeadRHit < $div.closest("td").height()) hHeadRHit = $div.closest("td").height();
                        if (hHeadRHit < $div.closest("th").height()) hHeadRHit = $div.closest("th").height();
                    });
                    $(".zb_tableFixHead > table > thead > tr:nth-child(4) > td > div, .zb_tableFixHead > table > thead > tr:nth-child(4) > th > div").each(function () { var $div = $(this); $div.height(hHeadRHit); });

                }, 100);

                $('.zb_tableFixHead').find('thead').addClass('zb_fixheader');

                $('.zb_tableFixHead').css('height', NewtableHeight + 'px');
                $('.zb_tableFixHead').addClass('zb_tableborders');

                //var $Scrollbtns = $('<div class="zb_rptscrollbtns"><a href="javascript:;" class="btn btn-primary btnrptTop" title="scroll top"><i class="fa fa-arrow-up"></i></a> <a href="javascript:;" class="btn btn-primary btnrptBot" title="scroll down"><i class="fa fa-arrow-down"></i></a></div>');
                var $Scrollbtns = $('<a href="javascript:;" class="btn btn-primary btnrptTop" title="scroll top"><i class="fa fa-arrow-up"></i></a><a href="javascript:;" class="btn btn-primary btnrptBot" title="scroll down"><i class="fa fa-arrow-down"></i></a>');
                //$Scrollbtns.appendTo($('#divReportTableData').children(':first'));
                $('.btnrptTop').remove();
                $('.btnrptBot').remove();
                $('.box-tools').prepend($Scrollbtns);

                if ($('.zb_tableFixHead > table tr').last().html().toLowerCase().indexOf('sub total') == -1 && $('.zb_tableFixHead > table tr').last().html().toLowerCase().indexOf('total') != -1) {
                    $('.zb_tableFixHead > table tr').last().addClass('zb_fixfooter');
                }
                activeTab('#liReport', '#Report', '#liCriteria', '#Criteria');
                $([document.documentElement, document.body]).animate({
                    scrollTop: ($("#divReportTableData").offset().top - 5)
                }, 100);

            }
            else {
                activeTab('#liReport', '#Report', '#liCriteria', '#Criteria');
            }
        }, 50);
    }
    else {
        activeTab('#liReport', '#Report', '#liCriteria', '#Criteria');
    }
}

function activeTab(AddLi, AddContent, RemoveLi, RemoveContent) {
    $(AddLi).addClass("active");
    $(AddContent).addClass("active");
    $(RemoveLi).removeClass("active");
    $(RemoveContent).removeClass("active");
}

$(document).on('click', '.btnrptTop', function () {
    $(".zb_tableFixHead").animate({ scrollTop: 0 }, "slow");
});
$(document).on('click', '.btnrptBot', function () {
    $(".zb_tableFixHead").animate({ scrollTop: $('.zb_tableFixHead')[0].scrollHeight }, "slow");
});

function SetDisplayOrderofButton(DisplayOrder) {
    for (var b = 0; b < ButtonList.length; b++) {
        if (ButtonList[b].DisplayOrder >= DisplayOrder) {
            ButtonList[b].DisplayOrder = ButtonList[b].DisplayOrder + 1;
        }
    }
}

function GoBackPage() { window.location.href = document.referrer; }
$(document).on('click', '.adslnk', function () {
    var FromDateid = $(this).data('fromdateid'); // .find(':selected')
    var ToDateid = $(this).data('todateid'); // .find(':selected')
    var linksr = $(this).data('value');
    var FromDate = '', ToDate = '';
    $('#spselectedOption').html($(this).data('lable'));
    switch (linksr) {
        case 0: FromDate = TodayDate; ToDate = TodayDate; break; // Today
        case 1: FromDate = ThisWeekStartDate; ToDate = ThisWeekEndDate; break; // This Week
        case 2: FromDate = ThisMonthStartDate; ToDate = ThisMonthEndDate; break; // This Month
        case 3: FromDate = ThisFYStartDate; ToDate = ThisFYEndDate; break; // This Financial Year
        case 4: FromDate = YesterdayDate; ToDate = YesterdayDate; break; // Yesterday
        case 5: FromDate = PreviousWeekStartDate; ToDate = PreviousWeekEndDate; break; // Previous Week
        case 6: FromDate = PreviousMonthStartDate; ToDate = PreviousMonthEndDate; break; // Previous Month
        case 7: FromDate = PreviousFYStartDate; ToDate = PreviousFYEndDate; break; // Previous Financial Year
        case 8: FromDate = NextMonthStartDate; ToDate = NextMonthEndDate; break; // Next Month
        case 9: var arr = Quarter1.split('|'); FromDate = arr[0]; ToDate = arr[1]; break; // Quarter 1
        case 10: var arr = Quarter2.split('|'); FromDate = arr[0]; ToDate = arr[1]; break; // Quarter 2
        case 11: var arr = Quarter3.split('|'); FromDate = arr[0]; ToDate = arr[1]; break; // Quarter 3
        case 12: var arr = Quarter4.split('|'); FromDate = arr[0]; ToDate = arr[1]; break; // Quarter 4
        default: FromDate = GetTodayDate(); ToDate = GetTodayDate(); $('#spselectedOption').html('Custom'); break; // Custom - Set Today Date
    }

    if ($.trim(FromDate) == '') FromDate = GetTodayDate();
    if ($.trim(ToDate) == '') ToDate = GetTodayDate();

    if (FromDateid != undefined && $('#' + FromDateid).length == 1)
        $('#' + FromDateid).val(FromDate);
    else
        if ($('#FromDate').length == 1) $('#FromDate').val(FromDate);

    if (ToDateid != undefined && $('#' + ToDateid).length == 1)
        $('#' + ToDateid).val(ToDate);
    else
        if ($('#ToDate').length == 1) $('#ToDate').val(ToDate);
});

function GetTodayDate() {
    const today = new Date();
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    const formattedToday = dd + '/' + mm + '/' + yyyy;
    return formattedToday;
}
//#region Fix Report Table Header
