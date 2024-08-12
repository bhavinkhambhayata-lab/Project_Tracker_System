function GetReportPreferenceData(ReportName) {
    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: ITX3ResolveUrl("Common/GetCommonReportPreferences"),
        data: JSON.stringify({ ReportName: ReportName }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.IsSuccess) {
                _CommonReportPreference = data.CommonReportPreferences;
                _gblCommonReportCritriaPreferences = data.lstCommonReportCriteriaPreferences;
            } else {
                _CommonReportPreference = [];
                _gblCommonReportCritriaPreferences = [];
            }
        },
        error: function (xhr, ret, e) {
            //AlertDialog("", e);
            _CommonReportPreference = [];
            _gblCommonReportCritriaPreferences = [];
        }
    });
}

function SaveReportPreference() {
    if (_CommonReportPreference != null) {
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: ITX3ResolveUrl("Common/SaveCommonReportPreferences"),
            data: JSON.stringify(_CommonReportPreference),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.IsSuccess) {
                    if (_CommonReportPreference.Id == undefined || _CommonReportPreference.Id == null || _CommonReportPreference.Id == "") {
                        _CommonReportPreference.Id = data.Id;
                    }
                    console.log(data.ResponseMessage);
                } else {
                    console.log(data.ResponseMessage);
                }
            },
            error: function (xhr, ret, e) {
                console.log(e);
            }
        });
    }
}




function fnGridColumnPinned_Event(event) {
    if (event.pinned != null) {
        if (_CRPGridColumnPinned.filter(m=>m.colId === event.column.colId).length == 0) {
            _CRPGridColumnPinned.push({ colId: event.column.colId, pinned: event.pinned });
            _CommonReportGridJSON.CRPGridColumnPinned = _CRPGridColumnPinned;
        }
    } else {
        if (_CommonReportGridJSON != null) {
            if (_CommonReportGridJSON.CRPGridColumnPinned != undefined && _CommonReportGridJSON.CRPGridColumnPinned != null && _CommonReportGridJSON.CRPGridColumnPinned.length > 0) {
                for (var count = 0; count < _CommonReportGridJSON.CRPGridColumnPinned.length; count++) {
                    if (_CommonReportGridJSON.CRPGridColumnPinned[count].colId === event.column.colId) {
                        _CommonReportGridJSON.CRPGridColumnPinned.splice(count, 1);
                        break;
                    }
                }
            }
        }
    }
    _CommonReportPreference.ReportGridJSON = JSON.stringify(_CommonReportGridJSON);
    SaveReportPreference(_CommonReportPreference);
}

function fnGridColumnVisible_Event(event) {
    if (event != null && event.visible != undefined && event.visible != null)
        if (!event.visible) {
            if (!_CRPGridColumnVisible.includes(event.column.colId)) {
                _CRPGridColumnVisible.push(event.column.colId);
                _CommonReportGridJSON.CRPGridColumnVisible = _CRPGridColumnVisible;
            }
        } else {
            if (_CommonReportGridJSON != null) {
                if (_CommonReportGridJSON.CRPGridColumnVisible != undefined && _CommonReportGridJSON.CRPGridColumnVisible != null && _CommonReportGridJSON.CRPGridColumnVisible.length > 0) {
                    for (var count = 0; count < _CommonReportGridJSON.CRPGridColumnVisible.length; count++) {
                        if (_CommonReportGridJSON.CRPGridColumnVisible[count] === event.column.colId) {
                            _CommonReportGridJSON.CRPGridColumnVisible.splice(count, 1);
                            break;
                        }
                    }
                }
            }
        }
    _CommonReportPreference.ReportGridJSON = JSON.stringify(_CommonReportGridJSON);
    SaveReportPreference(_CommonReportPreference);
}

function fnGridColumnFilterChanged_Event(event) {
    if (event != null) {
        _CRPGridColumnFilter = gridOptions.api.getFilterModel();
        _CommonReportGridJSON.CRPGridColumnFilter = _CRPGridColumnFilter;
    }
    _CommonReportPreference.ReportGridJSON = JSON.stringify(_CommonReportGridJSON);
    SaveReportPreference(_CommonReportPreference);
}

function fnGridColumnSortChanged_Event(event) {
    if (event != null) {
        if (event.api.getSortModel().length > 0) {
            _CRPGridSorting = [];
            _CRPGridSorting.push({ colId: event.api.getSortModel()[0].colId, sort: event.api.getSortModel()[0].sort });
        }
        _CommonReportGridJSON.CRPGridSorting = _CRPGridSorting;
    }
    _CommonReportPreference.ReportGridFilterJSON = JSON.stringify(_CommonReportGridJSON);
    SaveReportPreference(_CommonReportPreference);
}


function SetReportGridPreferences() {
    if (_CRPGridColumnFilter != null) {
        gridOptions.api.filterManager.setFilterModel(_CRPGridColumnFilter);
    }
    if (_CRPGridColumnPinned != null && _CRPGridColumnPinned.length > 0) {
        for (var count = 0 ; count < _CRPGridColumnPinned.length ; count++) {
            gridOptions.columnApi.setColumnPinned(_CRPGridColumnPinned[count].colId, _CRPGridColumnPinned[count].pinned);
        }
    }
    if (_CRPGridColumnVisible != null && _CRPGridColumnVisible.length > 0) {
        for (var count = 0 ; count < _CRPGridColumnVisible.length ; count++) {
            gridOptions.columnApi.setColumnVisible(_CRPGridColumnVisible[count], false);
        }
    }
    if (_CRPGridSorting != null && _CRPGridSorting.length > 0) {
        gridOptions.api.setSortModel(_CRPGridSorting);
    }
}