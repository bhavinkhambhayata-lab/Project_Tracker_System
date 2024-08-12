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
var RptViewStatus = false, IsRptGenerated = false, flagGridReport = false, IsHeaderAlignLeft = false, RptScrollStatus = false, flagGridReportNoRecord = false;

var ButtonList = [];

// available option in page for grid operation
var AvailableOptionList = [];

// grid menu icons and row level oprations
var OptioniconList = [
    { Text: 'Edit', Icon: 'fa-pencil' },
    { Text: 'View', Icon: 'fa-eye ic' },
    { Text: 'Delete', Icon: 'fa-trash-o' },
    { Text: 'Print', Icon: 'fa-print' },
    { Text: 'Email', Icon: 'fa-envelope' },
    { Text: 'Copy', Icon: 'fa-copy' },
    { Text: 'Send Back', Icon: 'fa-arrow-left' },
    { Text: 'Amendment', Icon: 'fa-edit' },
];

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
    var rCss = '.fixtbldiv {width: 100%;border: 1px black solid;padding: 3px 7px 2px 7px;} .fixtbltheadth {padding: 0px !important;border: none !important;} .tableFixHeaddiv {width: 100%;padding: 3px 7px 2px 7px;border-left: 1px #999 solid;border-top: 1px #999 solid;position: relative;left: -1px;vertical-align: middle;} .fixhedtd{padding:0px !important;border: none !important;}',
        rHead = document.head || document.getElementsByTagName('head')[0],
        rStyle = document.createElement('style');
    if (IsHeaderAlignLeft == true) {
        rCss = '.fixtbldiv {width: 100%;border: 1px black solid;padding: 3px 7px 2px 7px;} .fixtbltheadth {padding: 0px !important;border: none !important;} .tableFixHeaddiv {width: 100%;padding: 3px 7px 2px 7px;border-left: 1px #999 solid;border-top: 1px #999 solid;position: relative;left: -1px;text-align: left;vertical-align: middle;} .fixhedtd{padding:0px !important;border: none !important;}',
            rHead = document.head || document.getElementsByTagName('head')[0],
            rStyle = document.createElement('style');
    }
    rHead.appendChild(rStyle);
    rStyle.type = 'text/css';

    if (rStyle.styleSheet) {
        // This is required for IE8 and below.
        rStyle.styleSheet.cssText = rCss;
    } else {
        rStyle.appendChild(document.createTextNode(rCss));
    }
    setTimeout(function () { $('.fa-exchange').hide(); }, 100);
    /* Fix Report Header CSS & JS End */
    PageTitle();

    if ($('.box-header > .btn-group > .dropdown-menu').length > 0) {
        $(".box-header > .btn-group > .dropdown-menu li").each((id, elem) => {
            $(elem.children).each(function (i, child) {
                let Child;
                if ($(child).prop("tagName").toLowerCase() != 'a') {
                    Child = $(child).siblings("a");
                }
                else {
                    Child = child;
                }

                if ($.trim(Child.innerText) != '') {
                    var opt = new Object();
                    opt.Text = $.trim(Child.innerText);
                    if ($(Child).attr('id') != undefined)
                        opt.id = $(Child).attr('id');
                    AvailableOptionList.push(opt);
                }
            });
        });
    }
    // Club Menu
    if ($('.dvContextMenu > .dropdown-menu > li').length > 0) {
        $('.dvContextMenu > .dropdown-menu > li').each((id, elem) => {
            $(elem.children).each(function (i, child) {
                let Child;
                if ($(child).prop("tagName").toLowerCase() != 'a') {
                    Child = $(child).siblings("a");
                }
                else {
                    Child = child;
                }

                if ($.trim(Child.innerText) != '') {
                    var opt = new Object();
                    opt.Text = $.trim(Child.innerText);
                    if ($(Child).attr('id') != undefined)
                        opt.id = $(Child).attr('id');
                    AvailableOptionList.push(opt);
                }
            });
        });
    }
});

// input reaonly false for user input value in textbox
$(document).on('focus', '.x-box-target input', function () {
    $(".x-box-target input").attr('readonly', false);
});

function PageTitle() {
    let title_el = document.querySelector("title");
    if (title_el) {
        let pHeader = $('.Headertxt h4').html();
        let ptitle = '';
        if (pHeader != "" && pHeader != undefined) {   
            pHeader = $.parseHTML(pHeader)[0].data;
            ptitle = title_el.innerHTML + " - " + pHeader;
            if (ptitle.indexOf('div') != -1) {
                setTimeout(function () { ptitle = $('#CreateHeader').html(); title_el.innerHTML = title_el.innerHTML + " - " + ptitle; }, 500);
            }
            else {
                title_el.innerHTML = ptitle;
                RecentlyVisited(pHeader);
            }
        }
        else {
            RecentlyVisited('');
        }
    }
}

function RecentlyVisited(pHeader) {
    let RecentlyVisitedList = [];
    
    if (localStorage.getItem("RecentlyVisited") != null && localStorage.getItem("RecentlyVisited") != '') {
        RecentlyVisitedList = JSON.parse(localStorage.getItem("RecentlyVisited"));
    }

    if (RecentlyVisitedList.length == 10) {
        RecentlyVisitedList.shift();
        RecentlyVisitedList.pop();
    }

    if (pHeader != '' && pHeader != 'General Settings') {
        var VisitedItem = $.grep(RecentlyVisitedList, function (n, i) {
            return $.trim(n.title) === $.trim(pHeader)
        });
        //&& $.trim(n.Url) === $.trim(document.URL);

        if (VisitedItem.length <= 0) {
            RecentlyVisitedList.push({
                title: pHeader,
                Url: document.URL
            });
        }
    }

    localStorage.setItem("RecentlyVisited", JSON.stringify(RecentlyVisitedList));

    if (RecentlyVisitedList.length > 0) {
        var cList = $("#navbar-collapse > ul");
        var Newli = $('<li/>')
            .addClass('dropdown top-change-company hidden-md hidden-sm')
            .appendTo(cList);
        $('<a/>')
            .addClass('dropdown-toggle zipnav-link')
            .html('Recently Visited <span class="caret"></span>')
            .attr("href", '#')
            .attr('data-toggle', 'dropdown')
            .attr('role', 'button')
            .attr('aria-expanded', 'false')
            .appendTo(Newli);

        var SubM = $('<ul/>').addClass('dropdown-menu').attr('role', 'menu').appendTo(Newli);

        $.each(RecentlyVisitedList, function (i) {
            var li = $('<li/>')
                .appendTo(SubM);
            var aaa = $('<a/>')
                .text(RecentlyVisitedList[i].title)
                .attr("href", RecentlyVisitedList[i].Url)
                .appendTo(li);
        });

    }

}

function settableFixHead() {
    RptScrollStatus = false;
    if ($(".zb_tableFixHead").length > 0) {
        setTimeout(function () {
            var hHeadRHit = 0;
            activeTab('#liReport', '#Report', '#liCriteria', '#Criteria');
            var tableHeight = $('.zb_tableFixHead > table').outerHeight();
            activeTab('#liCriteria', '#Criteria', '#liReport', '#Report');
            var NewtableHeight = ($(window).height() - 135);
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

                var $Scrollbtns = $('<button class="zipbtn btnrptTop" title="Top"><i class="fa fa-arrow-up"></i></button><button class="zipbtn btnrptBot" title="Bottom"><i class="fa fa-arrow-down"></i></button>');
                $('.btnrptTop').remove();
                $('.btnrptBot').remove();
                $('.box-tools').append($Scrollbtns);

                if ($('.zb_tableFixHead > table tr').last().html().toLowerCase().indexOf('sub total') == -1 && $('.zb_tableFixHead > table tr').last().html().toLowerCase().indexOf('total') != -1) {
                    $('.zb_tableFixHead > table tr').last().addClass('zb_fixfooter');
                }
                activeTab('#liReport', '#Report', '#liCriteria', '#Criteria');
                $([document.documentElement, document.body]).animate({
                    scrollTop: ($("#divReportTableData").offset().top - 5)
                }, 100);

                const scroller = document.querySelector(".zb_tableFixHead");

                $('.btnrptTop').attr("disabled", true);
                $('.rptbtn').attr('title', 'Back to Criteria');
                RptViewStatus = false;
                RptScrollStatus = true;
                scroller.addEventListener("scroll", (event) => {
                    if (scroller.scrollTop == 0) {
                        $('.btnrptTop').attr("disabled", true);
                    }
                    else {
                        $('.btnrptTop').attr("disabled", false);
                    }
                    if ((scroller.scrollTop + $('#divReportTableData').prop('scrollHeight')) >= $('.zb_tableFixHead')[0].scrollHeight) {
                        $('.btnrptBot').attr("disabled", true);
                    }
                    else {
                        $('.btnrptBot').attr("disabled", false);
                    }
                });



            }
            else {
                activeTab('#liReport', '#Report', '#liCriteria', '#Criteria');
                RptViewStatus = false;
            }
        }, 50);
    }
    else {
        activeTab('#liReport', '#Report', '#liCriteria', '#Criteria');
        RptViewStatus = false;
    }
    $(".rptbtn").show();
}

function activeTab(AddLi, AddContent, RemoveLi, RemoveContent) {
    $(AddLi).addClass("active");
    $(AddContent).addClass("active");
    $(RemoveLi).removeClass("active");
    $(RemoveContent).removeClass("active");
}

$(document).on('click', '.btnrptTop', function () {
    $('.btnrptBot').attr("disabled", false);
    $(".zb_tableFixHead").animate({ scrollTop: 0 }, "slow");
});
$(document).on('click', '.btnrptBot', function () {
    $('.btnrptBot').attr("disabled", true);
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

$(document).on('click', '.rptbtn', function () {
    if (!RptViewStatus) {
        $("#ReportDiv").hide();
        $("#CriteriaDiv").show();
        $(".btnrptTop, .btnrptBot").hide();
        $(".lnkEmailbtn, #lnkExportToExcel, #lnkExportToPdf,#lnkEInvoice, #lnkForeClose,#lnkRemoveForeClose,#lnkEmail, #lnkExportLetterToPdf, #lnkAGExportToExcel,#lnkUpdateReceivedDateAmount,#lnkAutoPostVoucher,#lnkOpenForeClose,#lnkPost,#lnkImport,#lnkExportToSpecificFormat,#lnkExportToPdfDepositSlip,#lnkApprove,#lnkUnApprove,#lnkExportToPdfLetter,#lnkWhatsapp,#lnkAutoPostVoucher,#lnkForeClosePR,#lnkUnreserved,#lnkChangeStatus,#lnkSubmitForAproval,#lnkBillWiseLedger").hide();
        RptViewStatus = true;
        $('.rptbtn').attr('title', 'Go to Report');
        if (flagGridReport == true && RptViewStatus == false) {
            $("#lnkExportToExcel,#lnkExportToPdf,#lnkEInvoice,#lnkForeClose,#lnkRemoveForeClose,#lnkUpdateReceivedDateAmount,#lnkAutoPostVoucher,#lnkOpenForeClose,#lnkBillWiseLedger").hide();
            $("#lnkAGExportToExcel").show();
        }
        if (flagGridReport == true && RptViewStatus == false && flagGridReportNoRecord == true) {
            $("#lnkAGExportToExcel").hide();
        }
    }
    else {
        $("#CriteriaDiv").hide();
        $("#ReportDiv").show();
        $(".btnrptTop, .btnrptBot").show();
        if ($(".zb_tableFixHead").length) {
            $(".lnkEmailbtn, #lnkExportToExcel, #lnkExportToPdf,#lnkEInvoice, #lnkForeClose,#lnkRemoveForeClose,#lnkEmail, #lnkExportLetterToPdf, #lnkUpdateReceivedDateAmount,#lnkAutoPostVoucher,#lnkOpenForeClose,#lnkPost,#lnkImport,#lnkExportToSpecificFormat,#lnkExportToPdfDepositSlip,#lnkApprove,#lnkUnApprove,#lnkExportToPdfLetter,#lnkWhatsapp,#lnkAutoPostVoucher,#lnkForeClosePR,#lnkUnreserved,#lnkChangeStatus,#lnkSubmitForAproval,#lnkBillWiseLedger").show();
            if (RptScrollStatus == false)
                $(".btnrptTop, .btnrptBot").hide();
        }
        else
            $(".lnkEmailbtn, #lnkExportToExcel, #lnkExportToPdf,#lnkEInvoice,#lnkForeClose,#lnkRemoveForeClose,#lnkEmail,#lnkExportLetterToPdf,#lnkUpdateReceivedDateAmount,#lnkAutoPostVoucher,#lnkOpenForeClose,#lnkPost,#lnkImport,#lnkExportToSpecificFormat,#lnkExportToPdfDepositSlip,#lnkApprove,#lnkUnApprove,#lnkExportToPdfLetter,#lnkWhatsapp,#lnkAutoPostVoucher,#lnkForeClosePR,#lnkUnreserved,#lnkChangeStatus,.btnrptTop, .btnrptBot,#lnkSubmitForAproval,#lnkBillWiseLedger").hide();
        RptViewStatus = false;
        $('.rptbtn').attr('title', 'Back to Criteria');
        if (flagGridReport == true && RptViewStatus == false) {
            $("#lnkExportToExcel,#lnkExportToPdf,#lnkEInvoice,#lnkForeClose,#lnkRemoveForeClose,#lnkUpdateReceivedDateAmount,#lnkAutoPostVoucher,#lnkOpenForeClose,#lnkBillWiseLedger").hide();
            $("#lnkAGExportToExcel").show();
        }
        if (flagGridReport == true && RptViewStatus == false && flagGridReportNoRecord == true) {
            $("#lnkAGExportToExcel").hide();
        }
    }
});
$(document).on('click', '.lnkCancel', function () {
    if (IsRptGenerated) {
        $('.rptbtn').trigger('click');
    }
    else {
        window.location.href = "/DashBoard";
    }
});



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


function MenuDisplay() {
    $('.fa-exchange').trigger('click');
}

function MessageNotification(msgType, Msg) {
    //ZipMessage(msgType, Msg);

    Lobibox.notify(msgType.toLowerCase(), {
        msg: Msg,
        delay: 25000,  //In milliseconds
        sound: false
    });
}


function ZipMessage(msgType, Msg, cntrlID = "", IsSticky = false) {
    if (IsSticky) {
        Lobibox.notify(msgType.toLowerCase(), {
            content: "html",
            delay: false,
            //delay: 30000,
            msg: Msg,
            size: 'mini',
            width: 'auto',
            position: 'top right',
            // width: $(window).width(),
            sound: false
        });
    }
    else {
        Lobibox.notify(msgType.toLowerCase(), {
            content: "html",
            msg: Msg,
            size: 'mini',
            width: 'auto',
            position: 'top right',
            // width: $(window).width(),
            delay: 30000,  //In milliseconds
            sound: false
        });
    }

    if (cntrlID != "")
        $(cntrlID).focus();
}

/* Create dropdown menu in grid Table opration */

//$(document).on('click', '.x-btn-toolbar', function () { 
//    setTimeout(function () { let Grid = $('.x-grid-table'); SubmenuAppend(Grid); }, 500);
//});

$(document).on('mouseover', '.x-grid-table', function () {
    if ($(this).attr('id').indexOf('gridview') !== -1) {
        SubmenuAppend(this);
    }
});

function SubmenuAppend(SelectedGrid) {
    if (!$(SelectedGrid).hasClass('nosubmenu')) {
        let menucount = 1;
        var table = $(SelectedGrid);
        let ddMenu = '<div class="tbldropdown"><button class="dropbtn" ><i class="fa fa-fw fa-ellipsis-v tbldropdown-toggle"></i></button><div class="tbldropdown-content">';

        if (AvailableOptionList.length > 0) {
            for (var i = 0; i < AvailableOptionList.length; i++) {
                var Id = '';
                if (AvailableOptionList[i].id != undefined) {
                    if (AvailableOptionList[i].id != '')
                        Id = AvailableOptionList[i].id;
                }

                let AnchorText = '';
                OptioniconList.forEach(function (e) {
                    if (AvailableOptionList[i].Text == e.Text) AnchorText = '<i class="fa fa-fw ' + e.Icon + '" ></i> ' + AvailableOptionList[i].Text;
                });
                if (AnchorText != '') {
                    if ($('#' + $.trim(Id)).length > 0) {
                        if ($('#' + $.trim(Id)).css('display') != 'none') {
                            ddMenu += '<a href="javascript:;"  data-id=\'' + $.trim(Id) + '\'  >' + AnchorText + '</a>';
                            //  $('#' + $.trim(Id)).parent().addClass('DN'); // hide row menu from top
                            menucount++;
                        }
                    }
                }
            }
        }

        ddMenu += '</div></div>';

        if (menucount > 1) {
            if (table != null && table != undefined) {
                let tbody = table.children('tbody');
                // add class to avoid duplication process
                if (!$(SelectedGrid).find('tr:first').hasClass('actionbtn')) {
                    $(SelectedGrid).find('tr:first').addClass('actionbtn');

                    // increase first col
                    let FirstColgroupWidth = $(table).find('colgroup:first').children('col').css('width');
                    if (FirstColgroupWidth != '') {
                        FirstColgroupWidth = FirstColgroupWidth.replace('px', '');
                        let NewFirstColgroupWidth = $.trim(FirstColgroupWidth);
                        NewFirstColgroupWidth = parseInt(NewFirstColgroupWidth) + 10;
                        $(table).find('colgroup:first').children('col').css("width", NewFirstColgroupWidth + "px");
                    }

                    // decrease last col
                    let LastColgroupWidth = $(table).find('colgroup:last').children('col').css('width');
                    if (LastColgroupWidth != '') {
                        LastColgroupWidth = LastColgroupWidth.replace('px', '');
                        let NewLastColgroupWidth = $.trim(LastColgroupWidth);
                        NewLastColgroupWidth = parseInt(NewLastColgroupWidth) - 10;
                        $(table).find('colgroup:last').children('col').css("width", NewLastColgroupWidth + "px");
                    }
                    //$('.x-grid-cell-inner').css('line-height','23px');

                    if (tbody.children('tr').length > 0) {
                        //let TotalRowsinGrid = tbody.children('tr').length;
                        //let RowIndex = 0;

                        tbody.children('tr').each(function () {
                            let Menulist = '';
                            Menulist = ddMenu;
                            //if (RowIndex > (TotalRowsinGrid - menucount) && ((TotalRowsinGrid - menucount)) > 0 ) {
                            //    Menulist = Menulist.replace('tbldropdown-content', 'tbldropdown-content tbldropdownaboverow');
                            //    // display menu position base on no of menu
                            //    let toppos = (25 * menucount);// * -1;
                            //    setTimeout(function () {
                            //        let CurrentRow = 0;
                            //        $(".tbldropdownaboverow").each(function () {
                            //            let newTop = toppos + CurrentRow;
                            //            $(this).css('top', newTop);
                            //            CurrentRow += 28;
                            //        });
                            //    }, 500);
                            //}
                            var td = $(this).find('td:first');
                            tdHTML = $(td).find('div:first').html();
                            $(td).find('div:first').html(Menulist + tdHTML);
                            //RowIndex++;
                        });
                    }
                }
            }
        }
    }
}


//string = string.replace(new RegExp(escapeRegExp(xColumnHeaderFirstWidth), 'g'), xNewxColumnHeaderFirstWidth);
function escapeRegExp(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // $& means the whole matched string
}

$(document).on('click', '.tbldropdown-content a', function () {
    var Id = $(this).data("id")
    var linkref = $("#" + Id).attr('href');
    if (linkref == '#' || linkref == 'javascript:void(0);') {
        $("#" + Id).trigger('click');
        if (linkref != $("#" + Id).attr('href')) {
            window.location.href = $("#" + Id).attr('href');
        }
    }
});
function RemoveAutoPassword()
{
    $('.box-body .x-panel .x-box-inner .x-box-target div.x-column-header:last table input[type=text]').val('');
}
function ZipNotificationMessage(str) {
    // modify message

    //var MsgText = $('#lblSuccessMessage').html();

    //MsgText = MsgText.replace("class='fa fa-print'", "class='btn-msg'");
    //MsgText = MsgText.replace("class='glyphicon glyphicon-print'", "class='btn-msg'");
    //MsgText = MsgText.replace("class='fa fa-envelope'", "class='btn-msg'");

    //MsgText = MsgText.replace("data-original-title='Print'>", "data-original-title='Print'><i class='fa fa-print'></i>Print");
    //MsgText = MsgText.replace(" title='Print'>", "title='Print'><i class='fa fa-print'></i>Print");
    //MsgText = MsgText.replace(" title='Print Cheque'>", "title='Print Cheque'><i class='glyphicon glyphicon-print'></i>Print Cheque");
    //MsgText = MsgText.replace(" title='Email'>", "title='Email'><i class='fa fa-envelope'></i>Email");

    //$('#lblSuccessMessage').html(MsgText);


    // remove old message
    //$('#div_ResultMessage').html('');


    // old changes
    //$('#div_ResultMessage').hide();

    //var IsSticky = false;
    //var ZipMessageType = 'success';
    //var MsgText = "";

    //if (str.indexOf('lblSuccessMessage') != -1) {
    //    ZipMessageType = 'success';
    //    MsgText = str.replace("id='lblSuccessMessage' class='alert alert-success alert-dismissable'", '');
    //    MsgText = MsgText.replace("<div class='fa fa-check-square-o'></div>", '');
    //    MsgText = MsgText.replace("<div class='fa fa-fw fa-exclamation-triangle'></div>", '');
    //    MsgText = MsgText.replace("×</button>", '');

    //    MsgText = MsgText.replace("class='fa fa-print'", "class='btn-msg'");
    //    MsgText = MsgText.replace("class='glyphicon glyphicon-print'", "class='btn-msg'");
    //    MsgText = MsgText.replace("class='fa fa-envelope'", "class='btn-msg'");

    //    MsgText = MsgText.replace("data-original-title='Print'>", "data-original-title='Print'><i class='fa fa-print'></i>Print");
    //    MsgText = MsgText.replace(" title='Print'>", "title='Print'><i class='fa fa-print'></i>Print");
    //    MsgText = MsgText.replace(" title='Print Cheque'>", "title='Print Cheque'><i class='glyphicon glyphicon-print'></i>Print Cheque");
    //    MsgText = MsgText.replace(" title='Email'>", "title='Email'><i class='fa fa-envelope'></i>Email");

    //    MsgText = MsgText.replace("<button type='button' class='close' data-toggle='tooltip' title='Close' data-dismiss='alert' aria-hidden='true'>", '');

    //}

    //if (str.indexOf('alert-danger') != -1) {
    //    ZipMessageType = 'error';
    //    MsgText = str.replace("class='alert alert-danger alert-dismissable'", '');
    //    MsgText = MsgText.replace("<div class='fa fa-check-square-o'></div>", '');
    //    MsgText = MsgText.replace("<div class='fa fa-fw fa-exclamation-triangle'></div>", '');
    //    MsgText = MsgText.replace("×</button>", '');

    //    MsgText = MsgText.replace("class='fa fa-print'", "class='btn-msg'");
    //    MsgText = MsgText.replace("class='glyphicon glyphicon-print'", "class='btn-msg'");
    //    MsgText = MsgText.replace("class='fa fa-envelope'", "class='btn-msg'");


    //    MsgText = MsgText.replace("data-original-title='Print'>", "data-original-title='Print'><i class='fa fa-print'></i>Print");
    //    MsgText = MsgText.replace(" title='Print'>", "title='Print'><i class='fa fa-print'></i>Print");

    //    MsgText = MsgText.replace(" title='Print Cheque'>", "title='Print Cheque'><i class='glyphicon glyphicon-print'></i>Print Cheque");

    //    MsgText = MsgText.replace(" title='Email'>", "title='Email'><i class='fa fa-envelope'></i>Email");

    //    MsgText = MsgText.replace("<button type='button' class='close' data-toggle='tooltip' title='Close' data-dismiss='alert' aria-hidden='true'>", '');

    //}

    //ZipMessage(ZipMessageType, MsgText, "", IsSticky);

    //setTimeout(function () {

    //    var msgText = document.getElementById("lblSuccessMessage").innerText;
    //    msgText = msgText.replace('×\n', '');
    //    msgText = msgText.replace('×', '');
    //    msgText = msgText.replace('Updated Successfully', '');
    //    msgText = msgText.replace('posted Successfully', '');

    //    //msgText 

    //    var btns = '';

    //    if ($(".mid-box") != undefined && $(".mid-box").html() != undefined)
    //        btns += $.trim($(".mid-box").html());

    //    if ($(".lobibox-notify-msg > div > .DivPrint") != undefined && $(".lobibox-notify-msg > div > .DivPrint").html() != undefined)
    //        btns += $.trim($(".lobibox-notify-msg > div > .DivPrint").html());

    //    if ($(".lobibox-notify-msg > div > .DivPrint2") != undefined && $(".lobibox-notify-msg > div > .DivPrint2").html() != undefined)
    //        btns += $.trim($(".lobibox-notify-msg > div > .DivPrint2").html());

    //    if ($(".lobibox-notify-msg > div > .DivEmail") != undefined && $(".lobibox-notify-msg > div > .DivEmail").html() != undefined)
    //        btns += $.trim($(".lobibox-notify-msg > div > .DivEmail").html());


    //    $(".mid-box").html(btns);

    //    $('.mid-box').find('a').each(function () {
    //        if ($(this).html() != undefined) {
    //            var btnText = $(this).html();
    //            btnText = btnText.replace('<i class="fa fa-print"></i>Print', '<i class="fa fa-print"></i>' + ' Print ' + msgText);
    //            btnText = btnText.replace('<i class="glyphicon glyphicon-print"></i>Print Cheque ', '<i class="glyphicon glyphicon-print"></i>' + ' Print Cheque ' + msgText);
    //            $(this).html(btnText);
    //        }
    //    });

    //    $(".lobibox-notify-msg > div > .DivPrint").hide();
    //    $(".lobibox-notify-msg > div > .DivPrint2").hide();

    //}, 500)
}

//$(document).on('click', '.filterbox i', function () {
//    var selectId = $(this).prev().attr('id');
//    //$('#' + selectId).openSelect();
//    $('#' + selectId).trigger('open');
//});
/* end Create dropdown menu in grid Table opration */

/*  AJAX Call PROGRESS BAR ON TOP */
    /*var ProgressBarHeightInPX = 5;
var ProgressBoxColor = "#000";
var ProgressBarColor = "#56C444";

$.ajaxPrefilter(function (options, originalOptions, jqXHR) {
    // Modify options, control originalOptions, store jqXHR, etc
    options.async = true;
    options.beforeSend = function () { if (!(options.url.indexOf("CheckSessionExpire") !== -1)) { TopProgressbar(true); } };
    options.complete = function () { if (!(options.url.indexOf("CheckSessionExpire") !== -1)) { TopProgressbar(false); ButtonSpinner(false); } };
    options.error = function () { if (!(options.url.indexOf("CheckSessionExpire") !== -1)) { TopProgressbar(false); ButtonSpinner(false); } };
});

function TopProgressbar(IsShow) {
    if (IsShow) {
        var ProgressboxStyle = "style='position: fixed; top: 0; z-index: 9999; width: 100%; height: " + ProgressBarHeightInPX + "px;background: " + ProgressBoxColor +"!important;'";
        var ProgressbarStyle = "style='background: " + ProgressBarColor +"!important; height: " + ProgressBarHeightInPX + "px;width:0px;'";
        let ZipProgressbar = "<div id='zipprogressbox' " + ProgressboxStyle + " ><div class='zipprogress-bar' role='progressbar' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100' " + ProgressbarStyle +"  class='progress-bar progress-bar-striped rounded-pill'></div></div>";

        // Add progressbar on top
        $('.wrapper').append(ZipProgressbar);
        var ZIPprogressBar = $('.zipprogress-bar');
        var ProgressPercent = 0;
        window.setInterval(function () {
            ProgressPercent += 2;
            ZIPprogressBar.css("width", ProgressPercent + '%').attr("aria-valuenow", ProgressPercent + '%').html('&nbsp;');
            if (ProgressPercent == 100) {ProgressPercent = 0;}
        }, 500);
    }
    else {
         // remove progressbar from top
        $('#zipprogressbox').remove();
    }
}*/

/*  AJAX Call PROGRESS BAR ON TOP */
/*  Progress in  BUTTON Start */
    /*var ButtonArray = ['fa fa-search-plus', 'fas fa-robot', 'fas fa-bolt','fa fa-fw fa-save'];
var SelectedButtonIndex = -1;
var SelectedButton = null;
$(document).on('click', '.btn-social', function (button) {
    var HtmlOfButton = $(this).html();
    for (var i = 0; i < ButtonArray.length; i++) {
     var found = HtmlOfButton.includes(ButtonArray[i]);
        if (found) {
            if ($(this).find('i').hasClass(ButtonArray[i])) {
                // select index and button in globle variable from remove spinner
                SelectedButtonIndex = i;
                SelectedButton = this;
                ButtonSpinner(true);
            }
        }
    }
});

function ButtonSpinner(IsShow) {
    if (IsShow) {
        // add Spinner in button
        if (SelectedButtonIndex >= 0 && SelectedButton != null) {
            $($(SelectedButton).find('i')).removeClass(ButtonArray[SelectedButtonIndex]);
            $($(SelectedButton).find('i')).addClass('fa fa-circle-notch fa-spin');
        }
    }
    else {
        // remove Spinner from button
        $($(SelectedButton).find('i')).removeClass('fa fa-circle-notch fa-spin');
        $($(SelectedButton).find('i')).addClass(ButtonArray[SelectedButtonIndex]);
        SelectedButton = null;
        SelectedButtonIndex = -1;
    }
}*/

/*  Progress in  BUTTON end */