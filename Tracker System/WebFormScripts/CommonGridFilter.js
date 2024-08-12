var gblSetFunName = "";
var gblQueryParam = "";
var tmpObjectType = "";   //ObjectType and ObjectDocTypeId added for Item Grid IF Showing Only Allowed Group List
var tmpObjectDocTypeId = "", tmpObjectDocTypeIdSub = "";
var tmpObjectTransType = "";

function AccGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = ObjectType;

    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        pageSizes: [10, 25, 50, 100, 250],
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + tmpObjectType + "&PObjectTransType=" + Filters)
        },
        fields: {
            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '15%'
            },

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '25%'
            },
            ParentName: {
                title: label('lblParentGroup', 'common', 'Parent Group'),
                width: '19%'
            },
            City: {
                title: label('lblCity', 'common', 'City'),
                width: '18%'
            },
            Phone: {
                title: label('lblPhone', 'common', 'Phone'),
                width: '14%'
            },
            Mobile: {
                title: label('lblMobile', 'common', 'Mobile'),
                width: '14%'
            }
        },
        selectionChanged: function () {
            //Get all selected rows           
            var $selectedRows = $(DivName).jtable('selectedRows');

            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetOAccountName")
                        SetOAccountName(record.ID.toString(), record.Name.toString());
                    else if (gblSetFunName == "SelectPerticular") //For AccountVoucher
                        SelectPerticular(record.Name.toString(), record.ID.toString(), record.BusinessAreaId.toString(), parseFloat(gblQueryParam), record.AccountType.toString(), record.Alias.toString());
                    else if (gblSetFunName == "SelectPerticular_1") //For OP Vch. Bank Reco.
                        SelectPerticular_1(record.Name.toString(), record.ID.toString(), record.BusinessAreaId.toString(), parseFloat(gblQueryParam));
                    else if (gblSetFunName == "SetAgent")
                        SetAgent(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetCashCustomer")
                        SetCashCustomer(record.Name.toString(), record.ID.toString(), record.Phone);
                    else if (gblSetFunName == "SetAccount_1")
                        SetAccount_1(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_2")
                        SetAccount_2(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_3")
                        SetAccount_3(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_4")
                        SetAccount_4(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_5")
                        SetAccount_5(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_6")
                        SetAccount_6(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_7")
                        SetAccount_7(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_8")
                        SetAccount_8(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_9")
                        SetAccount_9(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_10")
                        SetAccount_10(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_11")
                        SetAccount_11(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_12")
                        SetAccount_12(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_13")
                        SetAccount_13(record.Name.toString(), record.ID.toString());
                    else if (gblSetFunName == "SetAccount_14")
                        SetAccount_14(record.Name.toString(), record.ID.toString());

                    else if (gblSetFunName == "SetAccount_15")// customer
                        SetAccount_15(record.Name.toString(), record.ID.toString(), record.Address.toString(), record.Email.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetAccount_16") //transporter
                        SetAccount_16(record.Name.toString(), record.ID.toString(), record.Address.toString());
                    else
                        SetAccount(record.Name.toString(), record.ID.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam,
        ObjectType: tmpObjectType
    });
}

function AccGrid_BySalesExec(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, SalesExexId) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = ObjectType;
    tmpObjectDocTypeId = SalesExexId;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + tmpObjectType + "&PObjectTransType=" + Filters)
        },
        fields: {
            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '15%'
            },

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '25%'
            },
            ParentName: {
                title: label('lblParentGroup', 'common', 'Parent Group'),
                width: '19%'
            },
            City: {
                title: label('lblCity', 'common', 'City'),
                width: '18%'
            },
            Phone: {
                title: label('lblPhone', 'common', 'Phone'),
                width: '14%'
            },
            Mobile: {
                title: label('lblMobile', 'common', 'Mobile'),
                width: '14%'
            }
        },
        selectionChanged: function () {
            //Get all selected rows           
            var $selectedRows = $(DivName).jtable('selectedRows');

            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetAccountExecutiveWise")
                        SetAccountExecutiveWise(record.ID.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        ObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        QueryParam: gblQueryParam,
    });
}

function ItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblItemCode', 'common', 'Item Code'),
                width: '15%',

            },
            Name: {
                title: label('lblItemName', 'common', 'Item Name'),
                width: '15%'
            },
            Description: {
                title: label('lblDescription', 'common', 'Description'),
                width: '20%'
            },
            UnitName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '13%'
            },
            ItemGroupName: {
                title: label('lblItemGroup', 'common', 'Item Group'),
                width: '18%'
            },
            BrandName: {
                title: label('lblBrand', 'common', 'Brand'),
                width: '18%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetItemAndCodeValue")
                        SetItemAndCodeValue(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_1")
                        SetItemAndCodeValue_1(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_2")
                        SetItemAndCodeValue_2(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_3")
                        SetItemAndCodeValue_3(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_4")
                        SetItemAndCodeValue_4(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else
                        SetItemValue(record.Id.toString(), record.Name.toString(), record.Code.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function BatchGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'MfgDate ASC,BatchNo',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            BatchNo: {
                title: label('lblBatchNo', 'common', 'Batch No'),
                width: '15%',

            },
            ItemName: {
                title: label('lblItemName', 'common', 'Item Name'),
                width: '25%'
            },
            StoreName: {
                title: label('lblStoreName', 'common', 'Store Name'),
                width: '25%'
            },
            Closing: {
                title: label('lblClosing', 'common', 'Closing'),
                width: '15%'
            },

            MfgDate: {
                title: label('lblMfgDate', 'common', 'Mfg Date'),
                width: '20%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetBatchValue")
                        SetBatchValue(record.BatchNo.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function ItemGrid_ForAllowedGroup(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType, ObjectDocTypeIdSub) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectDocTypeIdSub = ObjectDocTypeIdSub;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '15%',

            },
            Name: {
                title: label('lblItemName', 'common', 'Item Name'),
                width: '15%'
            },
            Description: {
                title: label('lblDescription', 'common', 'Description'),
                width: '20%'
            },
            UnitName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '13%'
            },
            ItemGroupName: {
                title: label('lblItemGroup', 'common', 'Item Group'),
                width: '18%'
            },
            BrandName: {
                title: label('lblBrand', 'common', 'Brand'),
                width: '18%'
            },
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');

                    if (gblSetFunName == "SetItemAndCodeValue")
                        SetItemAndCodeValue(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_1")
                        SetItemAndCodeValue_1(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_2")
                        SetItemAndCodeValue_2(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_3")
                        SetItemAndCodeValue_3(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_4")
                        SetItemAndCodeValue_4(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else
                        SetItemValue(record.Id.toString(), record.Name.toString(), record.Code.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        ObjectDocTypeIdSub: tmpObjectDocTypeIdSub,
        PObjectTransType: tmpObjectTransType
    });
}

function ComponentGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {

    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'ComponentName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            ComponentCode: {
                title: label('lblComponentCode', 'common', 'Component Code'),
                width: '8%',

            },
            ComponentName: {
                title: label('lblComponentName', 'common', 'Component Name'),
                width: '10%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');

                    $('.jtable-row-selected').removeClass('jtable-row-selected');

                    if (gblSetFunName == "SetComponentAndCodeValue")
                        SetComponentAndCodeValue(record.Id.toString(), record.ComponentName.toString(), record.ComponentCode.toString(), record.CalculationTypeCode.toString(), record.CalculationTypeName.toString(), record.Formula.toString(), record.IsMandatory.toString(), record.IsManualChangeAllowed.toString());
                    else if (gblSetFunName == "SetComponentAndCodeValue_1")
                        SetComponentAndCodeValue(record.Id.toString(), record.ComponentName.toString(), record.ComponentCode.toString(), record.CalculationTypeCode.toString(), record.CalculationTypeName.toString(), record.Formula.toString(), record.IsMandatory.toString(), record.IsManualChangeAllowed.toString());
                    else if (gblSetFunName == "SetComponentAndCodeValue_2")
                        SetComponentAndCodeValue(record.Id.toString(), record.ComponentName.toString(), record.ComponentCode.toString(), record.CalculationTypeCode.toString(), record.CalculationTypeName.toString(), record.Formula.toString(), record.IsMandatory.toString(), record.IsManualChangeAllowed.toString());
                    else if (gblSetFunName == "SetComponentAndCodeValue_3")
                        SetComponentAndCodeValue(record.Id.toString(), record.ComponentName.toString(), record.ComponentCode.toString(), record.CalculationTypeCode.toString(), record.CalculationTypeName.toString(), record.Formula.toString(), record.IsMandatory.toString(), record.IsManualChangeAllowed.toString());
                    else if (gblSetFunName == "SetComponentAndCodeValue_4")
                        SetComponentAndCodeValue(record.Id.toString(), record.ComponentName.toString(), record.ComponentCode.toString(), record.CalculationTypeCode.toString(), record.CalculationTypeName.toString(), record.Formula.toString(), record.IsMandatory.toString(), record.IsManualChangeAllowed.toString());
                    else
                        SetComponentAndCodeValue(record.Id.toString(), record.ComponentName.toString(), record.ComponentCode.toString(), record.CalculationTypeCode.toString(), record.CalculationTypeName.toString(), record.Formula.toString(), record.IsMandatory.toString(), record.IsManualChangeAllowed.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        QueryParam: gblQueryParam,
        Filters: "ComponentNameᴌ" + $('#filtComponentName_ComponentWithAttr').val() + "|ComponentCodeᴌ" + $('#filtComponentCode_ComponentWithAttr').val()
    });
}

function GeneralStructureGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {

    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'TemplateName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            TemplateName: {
                title: label('lblStructureName', 'common', 'Structure'),
                width: '70%',
            },
            EntryDate: {
                title: label('lblEntryDate', 'common', 'Entry Date'),
                width: '30%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');

                    $('.jtable-row-selected').removeClass('jtable-row-selected');

                    if (gblSetFunName == "SetStructureValue")
                        SetStructureValue(record.Id.toString(), record.TemplateName.toString());
                    else if (gblSetFunName == "SetStructureValue_1")
                        SetStructureValue(record.Id.toString(), record.TemplateName.toString());
                    else if (gblSetFunName == "SetStructureValue_2")
                        SetStructureValue(record.Id.toString(), record.TemplateName.toString());
                    else if (gblSetFunName == "SetStructureValue_3")
                        SetStructureValue(record.Id.toString(), record.TemplateName.toString());
                    else if (gblSetFunName == "SetStructureValue_4")
                        SetStructureValue(record.Id.toString(), record.TemplateName.toString());
                    else
                        SetStructureValue(record.Id.toString(), record.TemplateName.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        QueryParam: gblQueryParam,
        Filters: "TemplateNameᴌ" + $('#filtStructureName_ComponentWithAttr').val() + "|EntryDateᴌ" + $('#filtEntryDate_ComponentWithAttr').val()
    });
}

function CityGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    //debugger;
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;


    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblCityName', 'common', 'City Name'),
                width: '10%'
            },
            StateName: {
                title: label('lblStateName', 'common', 'State Name'),
                width: '10%'
            },
            CountryName: {
                title: label('lblCountryName', 'common', 'Country Name'),
                width: '10%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');


            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    //debugger;
                    var record = $(this).data('record');

                    $('.jtable-row-selected').removeClass('jtable-row-selected');

                    if (gblSetFunName == "SetCityAndCodeValue")
                        SetCityAndCodeValue(record.Name.toString(), record.Id.toString());
                    else if (gblSetFunName == "SetCityAndCodeValue_1")
                        SetCityAndCodeValue(record.Name.toString(), record.Id.toString());
                    else if (gblSetFunName == "SetCityAndCodeValue_2")
                        SetCityAndCodeValue(record.Name.toString(), record.Id.toString());
                    else if (gblSetFunName == "SetCityAndCodeValue_3")
                        SetCityAndCodeValue(record.Name.toString(), record.Id.toString());
                    else if (gblSetFunName == "SetCityAndCodeValue_4")
                        SetCityAndCodeValue(record.Name.toString(), record.Id.toString());
                    else
                        SetCityAndCodeValue(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        QueryParam: gblQueryParam,
        Filters: "Nameᴌ" + $('#filtCityName_CityWithAttr').val() + "|StateNameᴌ" + $('#filtStateName_StateWithAttr').val() + "|CountryNameᴌ" + $('#filtCountryName_CountryWithAttr').val()
    });
}

function CustomerTypeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;


    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'CustomerTypeName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            CustomerTypeName: {
                title: label('lblCustomerTypeName', 'common', 'Customer Type Name'),
                width: '10%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');



            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');

                    if (gblSetFunName == "SetCustomerTypeAndCodeValue")
                        SetCustomerTypeAndCodeValue(record.CustomerTypeName.toString(), record.CustomerTypeCode.toString());
                    else if (gblSetFunName == "SetCustomerTypeAndCodeValue_1")
                        SetCustomerTypeAndCodeValue(record.CustomerTypeName.toString(), record.CustomerTypeCode.toString());
                    else if (gblSetFunName == "SetCustomerTypeAndCodeValue_2")
                        SetCustomerTypeAndCodeValue(record.CustomerTypeName.toString(), record.CustomerTypeCode.toString());
                    else if (gblSetFunName == "SetCustomerTypeAndCodeValue_3")
                        SetCustomerTypeAndCodeValue(record.CustomerTypeName.toString(), record.CustomerTypeCode.toString());
                    else if (gblSetFunName == "SetCustomerTypeAndCodeValue_4")
                        SetCustomerTypeAndCodeValue(record.CustomerTypeName.toString(), record.CustomerTypeCode.toString());
                    else
                        SetCustomerTypeAndCodeValue(record.CustomerTypeName.toString(), record.CustomerTypeCode.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        QueryParam: gblQueryParam,
        Filters: "CustomerTypeNameᴌ" + $('#filtCustomerTypeName_CustomerTypeWithAttr').val()
    });
}

function ItemGridWithAttr_ForAllowedGroup(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType, ObjectDocTypeIdSub) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectDocTypeIdSub = ObjectDocTypeIdSub;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '8%',

            },
            Name: {
                title: label('lblItemName', 'common', 'Item Name'),
                width: '10%'
            },
            Description: {
                title: label('lblDescription', 'common', 'Description'),
                width: '10%'
            },
            UnitName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '7%'
            },
            ItemGroupName: {
                title: label('lblItemGroup', 'common', 'Item Group'),
                width: '10%'
            },
            Attribute1: {
                title: "Attribute1",
                width: '7%'
            },
            Attribute2: {
                title: "Attribute2",
                width: '7%'
            },
            Attribute3: {
                title: "Attribute3",
                width: '7%'
            },
            Attribute4: {
                title: "Attribute4",
                width: '7%'
            },
            Attribute5: {
                title: "Attribute5",
                width: '7%'
            },
            Attribute6: {
                title: "Attribute6",
                width: '7%'
            },
            Attribute7: {
                title: "Attribute7",
                width: '7%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');

                    if (gblSetFunName == "SetItemAndCodeValue")
                        SetItemAndCodeValue(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_1")
                        SetItemAndCodeValue_1(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_2")
                        SetItemAndCodeValue_2(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_3")
                        SetItemAndCodeValue_3(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_4")
                        SetItemAndCodeValue_4(record.Id.toString(), record.Name.toString(), record.Code.toString());
                    else
                        SetItemValue(record.Id.toString(), record.Name.toString(), record.Code.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        ObjectDocTypeIdSub: tmpObjectDocTypeIdSub,
        QueryParam: gblQueryParam,
        Filters: "IM.Nameᴌ" + $('#filtItemName_ItemWithAttr').val() + "|IM.Descriptionᴌ" + $('#filtDescription_ItemWithAttr').val() + "|IM.Codeᴌ" + $('#filtItemCode_ItemWithAttr').val() + "|UnitMaster.Symbolᴌ" + $('#filtItemUnit_ItemWithAttr').val() + "|IGM.Nameᴌ" + $('#filtItemGroup_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute1_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute2_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute3_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute4_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute5_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute6_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute7_ItemWithAttr').val()
    });
}

function ItemGridWithAttribute(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType, ObjectDocTypeIdSub) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectDocTypeIdSub = ObjectDocTypeIdSub;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '8%',

            },
            Name: {
                title: label('lblItemName', 'common', 'Item Name'),
                width: '10%'
            },
            Description: {
                title: label('lblDescription', 'common', 'Description'),
                width: '10%'
            },
            UnitName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '7%'
            },
            ItemGroupName: {
                title: label('lblItemGroup', 'common', 'Item Group'),
                width: '10%'
            },
            Attribute1: {
                title: "Attribute1",
                width: '7%'
            },
            Attribute2: {
                title: "Attribute2",
                width: '7%'
            },
            Attribute3: {
                title: "Attribute3",
                width: '7%'
            },
            Attribute4: {
                title: "Attribute4",
                width: '7%'
            },
            Attribute5: {
                title: "Attribute5",
                width: '7%'
            },
            Attribute6: {
                title: "Attribute6",
                width: '7%'
            },
            Attribute7: {
                title: "Attribute7",
                width: '7%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');

                    if (record.Attribute1 == null)
                        record.Attribute1 = "-";
                    if (record.Attribute2 == null)
                        record.Attribute2 = "-";
                    if (record.Attribute3 == null)
                        record.Attribute3 = "-";
                    if (gblSetFunName == "SetItemAndCodeValue")
                        SetItemAndCodeValue(record.Id.toString(), record.Name.toString(), record.Code.toString(), record.Attribute1.toString(), record.Attribute2.toString(), record.Attribute3.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_1")
                        SetItemAndCodeValue_1(record.Id.toString(), record.Name.toString(), record.Code.toString(), record.Attribute1.toString(), record.Attribute2.toString(), record.Attribute3.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_2")
                        SetItemAndCodeValue_2(record.Id.toString(), record.Name.toString(), record.Code.toString(), record.Attribute1.toString(), record.Attribute2.toString(), record.Attribute3.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_3")
                        SetItemAndCodeValue_3(record.Id.toString(), record.Name.toString(), record.Code.toString(), record.Attribute1.toString(), record.Attribute2.toString(), record.Attribute3.toString());
                    else if (gblSetFunName == "SetItemAndCodeValue_4")
                        SetItemAndCodeValue_4(record.Id.toString(), record.Name.toString(), record.Code.toString(), record.Attribute1.toString(), record.Attribute2.toString(), record.Attribute3.toString());
                    else
                        SetItemValue(record.Id.toString(), record.Name.toString(), record.Code.toString(), record.Attribute1.toString(), record.Attribute2.toString(), record.Attribute3.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        ObjectDocTypeIdSub: tmpObjectDocTypeIdSub,
        QueryParam: gblQueryParam,
        Filters: "IM.Nameᴌ" + $('#filtItemName_ItemWithAttr').val() + "|IM.Descriptionᴌ" + $('#filtDescription_ItemWithAttr').val() + "|IM.Codeᴌ" + $('#filtItemCode_ItemWithAttr').val() + "|UnitMaster.Symbolᴌ" + $('#filtItemUnit_ItemWithAttr').val() + "|IGM.Nameᴌ" + $('#filtItemGroup_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute1_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute2_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute3_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute4_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute5_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute6_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute7_ItemWithAttr').val()
    });
}

function AccGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',

        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '30%'
            },

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '35%'
            },
            ParentName: {
                title: label('lblParentName', 'common', 'Parent Name'),
                width: '35%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "Parent")
                        Parent(record.Name.toString(), record.ID.toString(), record.CrDr.toString());
                    else if (gblSetFunName == "SetGroupAccount_1")
                        SetGroupAccount_1(record.ID.toString(), record.Name.toString());
                    else if (gblSetFunName == "SetGroupAccount_2")
                        SetGroupAccount_2(record.ID.toString(), record.Name.toString());
                    else if (gblSetFunName == "SetGroupAccount_3")
                        SetGroupAccount_3(record.ID.toString(), record.Name.toString());
                    else if (gblSetFunName == "SetGroupAccount_4")
                        SetGroupAccount_4(record.ID.toString(), record.Name.toString());
                    else
                        SetGroupValue(record.ID.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: gblQueryParam
    });
}


function AccSchedulemasterGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',

        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '30%'
            },

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '35%'
            },
            ParentName: {
                title: label('lblParentName', 'common', 'Parent Name'),
                width: '35%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetCrScheduleValue")
                        SetAccScheduleGroupValue(record.Name.toString(), record.ID.toString(), "Cr");
                    else if (gblSetFunName == "SetDrScheduleValue")
                        SetAccScheduleGroupValue(record.Name.toString(), record.ID.toString(), "Dr");
                    else
                        SetAccScheduleGroupValue(record.ID.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: QueryParam });
}

function ScheduleGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',

        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '30%'
            },

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '35%'
            },
            ParentName: {
                title: label('lblParentName', 'common', 'Parent Name'),
                width: '35%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetGroupValue(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}


function ProductCategoryGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',

        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '35%'
            },
            ParentName: {
                title: label('lblParentName', 'common', 'Parent Name'),
                width: '35%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetProductCategory")
                        SetProductCategory(record.Name.toString(), record.Id.toString());
                    else
                        SetProductCategory(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function BanquetMenuCategoryGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',

        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '35%'
            },
            ParentName: {
                title: label('lblParentName', 'common', 'Parent Name'),
                width: '35%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetGroupValue")
                        SetGroupValue(record.Name.toString(), record.ID.toString());
                    else
                        SetGroupValue(record.ID.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function ItemGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    if (ShowQuickCreate == true)
        $("#DivlnkQuickItemGroup").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblItemGroup', 'common', 'Item Group'),
                width: '50%'
            },
            ParentName: {
                title: label('lblParentGroup', 'common', 'Parent Group'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetItemtGroup")
                        SetItemtGroup(record.Name.toString(), record.Id.toString(), record.StoreId.toString());
                    else if (gblSetFunName == "SetItemGroup_1")
                        SetItemGroup_1(record.Name.toString(), record.Id.toString());
                    else
                        ParentGroup(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function AssetGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblAsset', 'common', 'Asset'),
                width: '50%'
            },
            AssetsGroupName: {
                title: label('lblAssetGroup', 'common', 'Asset Group'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetAsset")
                        SetAsset(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function ContactGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectDocTypeId) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpPObjectDocTypeId = PObjectDocTypeId;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true,
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'c_Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType)
        },
        fields: {

            c_Name: {
                title: label('lblName', 'common', 'Name'),
                width: '19%'
            },
            c_Address: {
                title: label('lblAddress', 'common', 'Address'),
                width: '30%'
            },
            c_CityName: {
                title: label('lblCity', 'common', 'City'),
                width: '15%'
            },
            c_StateName: {
                title: label('lblState', 'common', 'State'),
                width: '14%'
            },
            c_Mobile1: {
                title: label('lblMobile', 'common', 'Mobile'),
                width: '11%'
            },
            c_Phone1: {
                title: label('lblPhone', 'common', 'Phone'),
                width: '11%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SET_CONTACT_FULLDETAILS")
                        SET_CONTACT_FULLDETAILS(record.c_Id.toString(), record.c_Name.toString(), record.c_Address, record.c_Designation, record.c_Phone1.toString(), record.c_Mobile1.toString(), record.c_Email.toString(), record.c_WebSite.toString(), record.c_CityId.toString(), record.c_CityName.toString(), record.c_StateId.toString(), record.c_CountryId.toString());
                    else if (gblSetFunName == "SET_CONTACT_BASICDETAILS")
                        SET_CONTACT_BASICDETAILS(record.c_Name.toString(), record.c_Address.toString(), record.c_CityName.toString(), record.c_PIN.toString(), record.c_StateName.toString(), record.c_StateId.toString(), record.c_Mobile1.toString(), record.c_Phone1.toString(), record.c_Email.toString());
                    else if (gblSetFunName == "SET_CONTACT_ADDRESS")
                        SET_CONTACT_ADDRESS(record.c_Address.toString(), record.c_CityName.toString(), record.c_PIN.toString(), record.c_StateName.toString(), record.c_StateId.toString());
                    else if (gblSetFunName == "SET_CONTACT_MOBILE_PHONE")
                        SET_CONTACT_MOBILE_PHONE(record.c_Id.toString(), record.c_Name.toString(), record.c_Phone1.toString(), record.c_Mobile1.toString());
                    else if (gblSetFunName == "SET_CONTACT_MOBILE_PHONE1")
                        SET_CONTACT_MOBILE_PHONE1(record.c_Id.toString(), record.c_Name.toString(), record.c_Phone1.toString(), record.c_Mobile1.toString());
                    else
                        SetContact(record.c_Id.toString(), record.c_Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: QueryParam, PObjectDocTypeId: tmpPObjectDocTypeId });
}

function BOQActivityNameGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ProjectId, ZoneId) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpPObjectDocTypeId = ProjectId;
    tmpObjectTransType = ZoneId;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true,
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'ActivityName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType)
        },
        fields: {
            BOQName: {
                title: label('lblBOQName', 'common', 'BOQ Name'),
                width: '30%'
            },
            ActivityName: {
                title: label('lblName', 'common', 'Activity Name'),
                width: '40%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    SetBOQActivityName(record.Id.toString(), record.ActivityName.toString());
                    $("#dialog-BOQActivityNameList").dialog("close");
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: QueryParam, PObjectDocTypeId: tmpPObjectDocTypeId, PObjectTransType: tmpObjectTransType });
}

function ServiceGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblServiceGroup', 'common', 'Service Group'),
                width: '50%'
            },
            ParentName: {
                title: label('lblParentGroup', 'common', 'Parent Group'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetServiceParentGroup(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}
function ServiceGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId) {
    gblSetFunName = SetFunName;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '15%'
            },
            Name: {
                title: label('lblServiceName', 'common', 'Service Name'),
                width: '35%'
            },
            UnitName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '15%'
            },
            ServiceGroupName: {
                title: label('lblServiceGroup', 'common', 'Service Group'),
                width: '35%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetServiceValue(record.Id.toString(), record.Name.toString(), record.Code.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId
    });
}
function CostGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblCostGroup', 'common', 'Cost Group'),
                width: '50%'
            },
            ParentName: {
                title: label('lblParentName', 'common', 'Parent Name'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    ParentGroup(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function BudgetGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblBudgetGroup', 'common', 'Budget Group'),
                width: '50%'
            },
            ParentName: {
                title: label('lblParentName', 'common', 'Parent Name'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    ParentGroup(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function ItemAttribute(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblItemAttribute', 'common', 'Item Attribute'),
                width: '50%'
            }

        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetItemAttribute(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}
function AssetsGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    if (ShowQuickCreate == true)
        $("#DivlnkQuickAssetsGroup").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblAssetsGroup', 'common', 'Assets Group'),
                width: '50%'
            },
            ParentName: {
                title: label('lblParentGroup', 'common', 'Parent Group'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    ParentGroup(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function VoucherGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: '',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType)
        },
        fields: {
            VtypeId: {
                title: label('lblVchType', 'common', 'Vch. Type'),
                width: '20%'
            },
            VDate: {
                title: label('lblVchDate', 'common', 'Vch. Date'),
                width: '15%'
            },
            VNo: {
                title: label('lblVchNo', 'common', 'Vch. No.'),
                width: '20%'
            },
            AccName: {
                title: label('lblParty', 'common', 'Party'),
                width: '30%'
            },
            Amount: {
                title: label('lblAmount', 'common', 'Amount'),
                width: '20%',
                display: function (data) { return "<div class=''><table width='80%'><tr><td style='text-align: right'>" + data.record.Amount + "</td></tr></div>"; }
            }
        },
        selectionChanged: function () {
            //Get all selected rows           
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetVocuherData_Amount")
                        SetVocuherData_Amount(record.VNo.toString(), record.ID.toString(), record.Amount.toString());
                    else
                        SetVocuherData(record.VNo.toString(), record.ID.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam, PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function InvoiceGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: '',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType)
        },
        fields: {
            DocDate: {
                title: label('lblDocDate', 'common', 'Doc. Date'),
                width: '15%'
            },
            DocTypeName: {
                title: label('lblDocType', 'common', 'Doc. Type'),
                width: '20%'
            },
            Vendor_Customer_Name: {
                title: label('lblParty', 'common', 'Party'),
                width: '20%'
            },
            DocNo: {
                title: label('lblDocNo', 'common', 'Doc. No.'),
                width: '15%'
            },
            VInvoiceNo: {
                title: label('lblRefNo', 'common', 'Ref. No.'),
                width: '15%'
            },
            InvoiceTotalAmount: {
                title: label('lblAmount', 'common', 'Amount'),
                width: '15%',
                display: function (data) { return "<div class=''><table width='80%'><tr><td style='text-align: right'>" + data.record.InvoiceTotalAmount + "</td></tr></div>"; }
            }
        },
        selectionChanged: function () {
            //Get all selected rows           
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetInvoiceData_Amount")
                        SetInvoiceData_Amount(record.VInvoiceNo.toString(), record.Id.toString(), record.InvoiceTotalAmount.toString(),record.DocNo.toString());
                    else
                        SetInvoiceData(record.DocNo.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam, PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function PendingInvoiceGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: '',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType)
        },
        fields: {
            DocDate: {
                title: label('lblDocDate', 'common', 'Doc. Date'),
                width: '15%'
            },
            DocTypeName: {
                title: label('lblDocType', 'common', 'Doc. Type'),
                width: '20%'
            },
            Vendor_Customer_Name: {
                title: label('lblParty', 'common', 'Party'),
                width: '25%'
            },
            DocNo: {
                title: label('lblDocNo', 'common', 'Doc. No.'),
                width: '18%'
            },
            InvoiceTotalAmount: {
                title: label('lblAmount', 'common', 'Amount'),
                width: '20%',
                display: function (data) { return "<div class=''><table width='80%'><tr><td style='text-align: right'>" + data.record.InvoiceTotalAmount + "</td></tr></div>"; }
            }
        },
        selectionChanged: function () {
            //Get all selected rows           
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetInvoiceData_Amount")
                        SetInvoiceData_Amount(record.DocNo.toString(), record.Id.toString(), record.TotalAmount.toString());
                    else
                        SetInvoiceData(record.DocNo.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam, PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function InvoiceGridWithItem(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: '',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType)
        },
        fields: {
            DocDate: {
                title: label('lblDocDate', 'common', 'Doc. Date'),
                width: '15%'
            },
            DocTypeName: {
                title: label('lblDocType', 'common', 'Doc. Type'),
                width: '25%'
            },
            DocNo: {
                title: label('lblDocNo', 'common', 'Doc. No.'),
                width: '20%'
            },
            Item: {
                title: label('lblItem', 'common', 'Item'),
                width: '20%'
            },
            InvoiceTotalAmount: {
                title: label('lblAmount', 'common', 'Amount'),
                width: '18%',
                display: function (data) { return "<div class=''><table width='80%'><tr><td style='text-align: right'>" + data.record.InvoiceTotalAmount + "</td></tr></div>"; }
            }
        },
        selectionChanged: function () {
            //Get all selected rows           
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                //Show selected rows
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetInvoiceData(record.DocNo.toString(), record.Id.toString(), record.DocDate.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam, PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function PRDProcessGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    if (ShowQuickCreate == true)
        $("#DivlnkQuickProcess").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblProcessCode', 'common', 'Process Code'),
                width: '50%'
            },
            Name: {
                title: label('lblProcessName', 'common', 'Process Name'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetProcessNameAndCode(record.Id.toString(), record.Code.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}
function PRDOverheadGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    if (ShowQuickCreate == true)
        $("#DivlnkQuickOverhead").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblOverheadCode', 'common', 'Overhead Code'),
                width: '50%'
            },
            Name: {
                title: label('lblOverheadName', 'common', 'Overhead Name'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetOverheadNameAndCode(record.Id.toString(), record.Code.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function CostCenterGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, StrWhereCondition) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    if (ShowQuickCreate == true)
        $("#DivlnkQuickCostCenter").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + (StrWhereCondition == "" ? QueryParam : StrWhereCondition))
        },
        fields: {

            Name: {
                title: label('lblCostCenterName', 'common', 'Cost Center Name'),
                width: '50%'
            },
            ParentName: {
                title: label('lblCostCenterGroup', 'common', 'Cost Center Group'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetCostCenterNameAndId")
                        SetCostCenterNameAndId(record.Id.toString(), record.Name.toString(), parseFloat(gblQueryParam));
                    else if (gblSetFunName == "SetCostCenterNameAndId_1")
                        SetCostCenterNameAndId_1(record.Id.toString(), record.Name.toString(), parseFloat(gblQueryParam));
                    else if (gblSetFunName == "SetCostCenterNameAndId_2")
                        SetCostCenterNameAndId_2(record.Id.toString(), record.Name.toString(), parseFloat(gblQueryParam));
                    else if (gblSetFunName == "SetCostCenterNameAndId_3")
                        SetCostCenterNameAndId_3(record.Id.toString(), record.Name.toString(), parseFloat(gblQueryParam));
                    else
                        SetCostCenterNameAndGroup(record.Id.toString(), record.Name.toString(), record.ParentName.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: (StrWhereCondition == "" ? QueryParam : StrWhereCondition)
    });
}

function BudgetGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    if (ShowQuickCreate == true)
        $("#DivlnkQuickBudget").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblBudgetName', 'common', 'Budget Name'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetBudgetNameAndId")
                        SetBudgetNameAndId(record.Id.toString(), record.Name.toString(), parseFloat(gblQueryParam));
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function BinMasterGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    if (ShowQuickCreate == true)
        $("#DivlnkQuickBudget").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblBin', 'common', 'Bin'),
                width: '40%'
            },
            PlantName: {
                title: label('lblPlant', 'common', 'Plant'),
                width: '30%'
            },
            StoreName: {
                title: label('lblStore', 'common', 'Store'),
                width: '30%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetBinMasterNameAndId(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function PrjSkillGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    //if (ShowQuickCreate == true)
    //    $("#DivlnkQuickCostCenter").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '50%'
            },
            Unit: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetSkillAndId(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function PrjActivityGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetActivityAndId(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function PrjBOQGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ZoneId) {
    gblSetFunName = SetFunName;
    tmpObjectDocTypeId = ZoneId;

    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '50%'
            },
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetBOQGroupNameAndId_1") {
                        SetBOQGroupNameAndId_1(record.Id.toString(), record.Name.toString());
                        gblSetFunName = "";
                    }
                    if (gblSetFunName == "SetBOQGroupNameAndId_2") {
                        SetBOQGroupNameAndId_2(record.Id.toString(), record.Name.toString());
                        gblSetFunName = "";
                    }
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectDocTypeId: tmpObjectDocTypeId
    });
}

function PrjOtherChargesGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    //if (ShowQuickCreate == true)
    //    $("#DivlnkQuickCostCenter").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '40%'
            },
            Type: {
                title: label('lblType', 'common', 'Type'),
                width: '30%'
            },
            Unit: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '30%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetOtherChargesNameAndId(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function PrjOverheadGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '40%'
            },
            Type: {
                title: label('lblType', 'common', 'Type'),
                width: '30%'
            },
            Unit: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '30%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetOverheadNameAndId(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function prjAdditionalCharge(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblAdditionalCharge', 'common', 'Additional Charge'),
                width: '50%'
            }

        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetItemAttribute(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function UserGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'UserName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            UserName: {
                title: label('lblName', 'common', 'Name'),
                width: '40%'
            },

            UserId: {
                title: label('lblUserId', 'common', 'User Id'),
                width: '40%'
            },

        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetUser")
                        SetUser(record.UserName.toString(), record.Id.toString());
                    else if (gblSetFunName == "SetUser_1")
                        SetUser_1(record.UserName.toString(), record.Id.toString());
                    else if (gblSetFunName == "SetUser_2") // To get UserId from users table                        
                        SetUser_2(record.UserName.toString(), record.UserId.toString());
                    else if (gblSetFunName == "SetUser_3")
                        SetUser_3(record.UserName.toString(), record.Id.toString());
                    else if (gblSetFunName == "SetUser_4")
                        SetUser_4(record.UserName.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function GEMStageGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblGEMStage', 'common', 'GEM Stage'),
                width: '40%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetGEMStage")
                        SetGEMStage(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function TenderFormGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'EnquiryNo ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            EnquiryNo: {
                title: label('lblTenderEnquiryNo', 'common', 'Tender Enquiry No'),
                width: '40%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetTenderEnquiryNo")
                        SetTenderEnquiryNo(record.EnquiryNo.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function GEMProductGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblGEMProduct', 'common', 'GEM Product'),
                width: '40%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetGEMProduct")
                        SetGEMProduct(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function TasksGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, ZoneId) {

    //gblQueryParam = QueryParam;
    tmpObjectType = ObjectType;
    gblSetFunName = SetFunName;
    tmpObjectDocTypeId = ZoneId;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblTask', 'common', 'Task'),
                width: '40%'
            },
            TaskTypeName: {
                title: label('lblTaskType', 'common', 'Task Type'),
                width: '30%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetParentTaskIdandName_1") {
                        SetParentTaskIdandName_1(record.Id.toString(), record.Name.toString(), record.ZoneId.toString());
                        gblSetFunName = "";
                    }
                    if (gblSetFunName == "SetParentTaskIdandName_2") {
                        SetParentTaskIdandName_2(record.Id.toString(), record.Name.toString());
                        gblSetFunName = "";
                    }
                    else if (gblSetFunName == "SetTaskNameAndId") {
                        SetTaskNameAndId(record.Id.toString(), record.Name.toString());
                        SetTaskDates(record.StartDate, record.EndDate);
                        gblSetFunName = "";
                    }
                });
            }
        }
    });
    $(DivName).jtable('load',
        {
            ObjectType: tmpObjectType,
            QueryParam: QueryParam,
            PObjectDocTypeId: tmpObjectDocTypeId
        });
}

function GenericResourceGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '30%'
            },
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '40%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetGenericResourceNameAndId(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function ProductGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    if (ShowQuickCreate == true)
        $("#DivlnkQuickProductGroup").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblProductGroup', 'common', 'Product Group'),
                width: '50%'
            },
            ParentName: {
                title: label('lblParentGroup', 'common', 'Parent Group'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetProductGroup1")
                        SetProductGroup1(record.Name.toString(), record.Id.toString());
                    else
                        SetProductGroup(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function AssetsGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    if (ShowQuickCreate == true)
        $("#DivlnkQuickAssetsList").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam + "&Filters=" + Filters)
        },
        fields: {
            Code: {
                title: label("lblAssetsCode", "Assets", "Assets Code"),
                width: '15%',
            },
            Name: {
                title: label("lblAssetsName", "Assets", "Assets Name"),
                width: '25%'
            },
            Description: {
                title: label("lblDescription", "common", "Description"),
                width: '40%'
            },
            SerialNumber: {
                title: label("lblSerialNumber", "common", "Serial Number"),
                width: '15%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetAssetCode")
                        SetAssetCode(record.Code.toString(), record.Name.toString(), record.Id.toString(), record.SerialNumber.toString());
                    else
                        SetAsset(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function BOQGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblBOQCode', 'common', 'BOQ Code'),
                width: '50%'
            },
            Name: {
                title: label('lblBOQName', 'common', 'BOQ Name'),
                width: '50%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetBOQId") {
                        SetBOQId(record.Id.toString());
                        gblSetFunName = "";
                    }

                    if (gblSetFunName == "SetBOQIdWithName") {
                        SetBOQIdWithName(record.Id.toString(), record.Name.toString());
                        gblSetFunName = "";
                    }

                });
            }
        }
    });
    $(DivName).jtable('load');
}

function RetailGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, Vendor_Cust_Id) {

    tmpObjectType = ObjectType;
    gblSetFunName = SetFunName;
    tmpObjectDocTypeId = Vendor_Cust_Id;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '30%'
            },
            Address: {
                title: label('lblAddress', 'common', 'Address'),
                width: '30%'
            },
            Mobile: {
                title: label('lblMobile', 'common', 'Mobile'),
                width: '15%'
            },
            Email: {
                title: label('lblEmail', 'common', 'Email'),
                width: '25%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetRetailNameAndId") {
                        SetRetailNameAndId(record.Id.toString(), record.Name.toString());
                    }
                    else if (gblSetFunName == "SetRetailMobileNo") {
                        SetRetailMobileNo(record.Mobile.toString(), record.Id.toString());
                    }
                    else if (gblSetFunName == "SetCashCustDetail") {
                        SetCashCustDetail(record.Name.toString(), record.Phone.toString(), record.PANNumber.toString(), record.AadharNumber.toString(), record.GSTIN.toString(), record.Address.toString());
                    }
                    gblSetFunName = "";
                });
            }
        }
    });
    $(DivName).jtable('load',
        {
            ObjectType: tmpObjectType,
            QueryParam: QueryParam,
            PObjectDocTypeId: tmpObjectDocTypeId
        });
}

function Vendor_Cust_Wise_RetailCustTransDetail(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate, Vendor_Cust_Id, DocType) {
    tmpObjectTransType = DocType;
    tmpObjectType = ObjectType;
    gblSetFunName = SetFunName;
    tmpObjectDocTypeId = Vendor_Cust_Id;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            DocDate: {
                title: label('lblDate', 'common', 'Date'),
                width: '10%'
            },
            DocNo: {
                title: label('lblNumber', 'common', 'Number'),
                width: '10%'
            },
            RetailCust_Name: {
                title: label('lblRetailCustomer', 'common', 'Retail Customer'),
                width: '25%'
            },
            RetailCust_Address: {
                title: label('lblRetailAddress', 'common', 'Retail Address'),
                width: '25%'
            },
            RetailCust_MobileNo: {
                title: label('lblMobileNo', 'common', 'Mobile No.'),
                width: '10%'
            },
            ItemName: {
                title: label('lblItem', 'common', 'Item'),
                width: '20%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetRetailCustMobileNo") {
                        SetRetailCustMobileNo(record.RetailCust_MobileNo.toString(), record.Id.toString());
                    }
                    gblSetFunName = "";
                });
            }
        }
    });
    $(DivName).jtable('load',
        {
            ObjectType: tmpObjectType,
            QueryParam: QueryParam,
            PObjectDocTypeId: tmpObjectDocTypeId,
            PObjectTransType: tmpObjectTransType
        });
}


function AttributeGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetAttributeGroup") {
                        SetAttributeGroup(record.Name.toString(), record.Id.toString());
                        gblSetFunName = "";
                    }
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function InsurancePolicyGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'PolicyNo ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            PolicyNo: {
                title: label('lblPolicyNo', 'common', 'Policy No.'),
                width: '25%'
            },
            Issuer: {
                title: label('lblIssuer', 'common', 'Issuer'),
                width: '25%'
            },
            Description: {
                title: label('lblDescription', 'common', 'Description'),
                width: '45%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetInsurancePolicyNoAndId") {
                        SetInsurancePolicyNoAndId(record.Id.toString(), record.PolicyNo.toString());
                        gblSetFunName = "";
                    }
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function RoutesGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblRoutes', 'common', 'Routes'),
                width: '98%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetRouteIdAndName") {
                        SetRouteIdAndName(record.Id.toString(), record.Name.toString());
                        gblSetFunName = "";
                    }
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function StoreGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblQueryParam = QueryParam;
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblStore', 'common', 'Store'),
                width: '50%'
            },
            PlantName: {
                title: label('lblPlant', 'common', 'Plant'),
                width: '50%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetStoreIdAndName(record.Id.toString(), record.Name.toString());

                });
            }
        }
    });
    $(DivName).jtable('load');
}

function PlantGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblQueryParam = QueryParam;
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblPlant', 'common', 'Plant'),
                width: '50%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetPlantIdAndName(record.Id.toString(), record.Name.toString());

                });
            }
        }
    });
    $(DivName).jtable('load');
}

function MachineGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Code: {
                title: label('lblCode', 'common', 'Code'),
                width: '30%'
            },
            Name: {
                title: label('lblMachine', 'common', 'Machine'),
                width: '30%'
            },
            MachineTypeName: {
                title: label('lblType', 'common', 'Type'),
                width: '20%'
            },
            MachineGroup: {
                title: label('lblMachineGroup', 'common', 'Machine Group'),
                width: '20%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetMachineNameAndId(record.Id.toString(), record.Name.toString(), record.Code.toString());

                });
            }
        }
    });
    $(DivName).jtable('load');
}

function MachineGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblMachineGroup', 'common', 'Machine Group'),
                width: '100%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetMachineGroupNameAndId(record.Id.toString(), record.Name.toString());

                });
            }
        }
    });
    $(DivName).jtable('load');
}


function QcTestTempGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblTestTemplate', 'common', 'Test Template'),
                width: '100%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetQcTestTemplateIdAndName_1")
                        SetQcTestTemplateIdAndName_1(record.Id.toString(), record.Name.toString());
                    else if (gblSetFunName == "SetQcTestTemplateIdAndName_2")
                        SetQcTestTemplateIdAndName_2(record.Id.toString(), record.Name.toString());
                    else if (gblSetFunName == "SetQcTestTemplateIdAndName_3")
                        SetQcTestTemplateIdAndName_3(record.Id.toString(), record.Name.toString());
                    else
                        SetQcTestTemplateIdAndName(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSLocationGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetLocation(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSDepartmentGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetDepartment(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSDesignationGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetDesignation(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSGradeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetGrade(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSEmployeeTypeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetEmployeeType(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSPresentCityGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetPresentCity(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSPermanentCityGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetPermanentCity(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSStructureCodeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetStructureCode(record.Name.toString(), record.Code.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function SubTerritoryGrid_ByTerritory(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectDocTypeId) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpPObjectDocTypeId = PObjectDocTypeId;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblSubTerritory', 'common', 'Sub Territory'),
                width: '40%'
            },
            SalesTerritoriesName: {
                title: label('lblTerritories', 'common', 'Territories'),
                width: '30%'
            },
            RouteName: {
                title: label('lblRoute', 'common', 'Route'),
                width: '30%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetSubTerritoryNameAndId(record.Id.toString(), record.Name.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectDocTypeId: tmpPObjectDocTypeId
    });
}

function HRMSAssetCodeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '50%'
            },
            IsReturnable: {
                title: label('lblIsReturnable', 'common', 'IsReturnable'),
                width: '25%'
            },
            AssetTypeName: {
                title: label('lblAssetTypeName', 'common', 'Asset Type Name'),
                width: '25%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetAsset(record.Name.toString(), record.Id.toString(), record.IsReturnable.toString(), record.AssetTypeName.toString(), record.IsNonStockableAsset, record.Location);
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSIndentCodeGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Indents ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            RequestId: {
                title: label('lblRequestId', 'common', 'Request Id'),
                width: '50%'
            },
            Indents: {
                title: label('lblIndents', 'common', 'Indents'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetIndent(record.Indents.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function MemberGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {

    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'FirstName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            MembershipCode: {
                title: label('lblMembershipNo', 'common', 'Membership No.'),
                width: '30%'
            },

            FirstName: {
                title: label('lblName', 'common', 'Name'),
                width: '50%'
            },

            ConatctMobile: {
                title: label('lblMobile', 'common', 'Mobile'),
                width: '20%'
            },
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetMember")
                        SetMember(record.MembershipCode.toString(), record.FirstName.toString(), record.ConatctMobile.toString(), record.Address, record.Email);

                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSEmployeeForSeparation(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?Filters=")
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblEmployee', 'common', 'Employee'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetEmployeeForSeparation(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSUserGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'UserName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?UserName=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            },
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetUser(record.UserName.toString(), record.UserId.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function HRMSEmployeeForDayArrear(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?Filters=")
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblEmployee', 'common', 'Employee'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetEmployeeForDayArrear(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function TenantPopUp(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?Filters=")
        },
        fields: {
            Name: {
                title: label('lblCompany/Branch', 'common', 'Company/Branch'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetTenantId(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function OrderMaster(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblQueryParam = QueryParam;
    gblSetFunName = SetFunName;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'DocDate ASC,DocNo',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            DocDate: {
                title: label('lblOrderDate', 'common', 'Order Date'),
                width: '20%'
            },

            DocNo: {
                title: label('lblOrderNo', 'common', 'Order No'),
                width: '25%',

            },
            DocTypeName: {
                title: label('lblOrderType', 'common', 'Order Type'),
                width: '15%'
            },
            Vendor_Customer_Name: {
                title: label('lblCustomer', 'common', 'Customer'),
                width: '25%'
            },

            OrderTotalAmount: {
                title: label('lblAmount', 'common', 'Amount'),
                width: '15%'
            }


        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetOrderDataWithAmount")
                        SetOrderDataWithAmount(record.Id.toString(), record.DocNo.toString(), record.OrderTotalAmount.toString().replace(",", ""));
                    else if (gblSetFunName == "SetSOValue")
                        SetSOValue(record.Id.toString(), record.DocNo.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function PRDPO(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'DocDate ASC,DocNo',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            DocDate: {
                title: label('lblPrdOrdDate', 'common', 'Prd. Ord. Date'),
                width: '15%'
            },

            DocNo: {
                title: label('lblPrdOrdNo', 'common', 'Prd. Ord. No'),
                width: '15%',

            },

            POTypeName: {
                title: label('lblPrdOrdType', 'common', 'Prd. Ord. Type'),
                width: '25%'
            },
            ItemName: {
                title: label('lblItem', 'common', 'Item'),
                width: '25%'
            },

            Qty: {
                title: label('lblQty', 'common', 'Qty'),
                width: '10%'
            },

            BatchNo: {
                title: label('lblBatchNo', 'common', 'BatchNo'),
                width: '10%'
            }


        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetPrdOrdValue")
                        SetPrdOrdValue(record.DocNo.toString(), record.Id.toString());
                    else if (gblSetFunName == "SetPrdOrdValueWithItem")
                        SetPrdOrdValueWithItem(record.DocNo.toString(), record.Id.toString(), record.ItemName.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function PRDPO_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'DocDate ASC,DocNo',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Selection: {
                title: '<input type="checkbox" id="chkSelectDeselectAll" ></input>',
                width: '1%',
                sorting: false,
                position: 'center',
                display: function (data) { return "<input type='checkbox' name='checkGrdPRDPO' id='" + data.record.Id.toString() + "ᴌ" + data.record.DocNo + "' style='padding-left:15px;' onclick='DeselectMain();' ></input>"; },
                columnResizable: false,
                visibility: 'fixed'

            },
            DocDate: {
                title: label('lblPrdOrdDate', 'common', 'Prd. Ord. Date'),
                width: '19%'
            },

            DocNo: {
                title: label('lblPrdOrdNo', 'common', 'Prd. Ord. No'),
                width: '15%',

            },

            POTypeName: {
                title: label('lblPrdOrdType', 'common', 'Prd. Ord. Type'),
                width: '18%'
            },
            ItemCode: {
                title: label('lblItemCode', 'common', 'ItemCode'),
                width: '15%'
            },
            ItemName: {
                title: label('lblItem', 'common', 'Item'),
                width: '18%'
            },

            Qty: {
                title: label('lblQty', 'common', 'Qty'),
                width: '15%'
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: gblQueryParam
    });
    $('#chkSelectDeselectAll').on('click', function () {
        $(this).closest('table').find(':checkbox').prop('checked', this.checked);
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function PendingAdPRDPO(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            DocDate: {
                title: label('lblDocDate', 'common', 'Doc Date'),
                width: '15%'
            },
            DocNo: {
                title: label('lblDocNo', 'common', 'Doc No.'),
                width: '15%',
            },
            BaseItemName: {
                title: label('lblItem', 'common', 'Item'),
                width: '20%'
            },
            BaseItemQty: {
                title: label('lblQty', 'common', 'Qty'),
                width: '10%'
            },
            BaseItemUOMName: {
                title: label('lblUnit', 'common', 'Unit'),
                width: '10%'
            },
            ProcessName: {
                title: label('lblProcess', 'common', 'Process'),
                width: '20%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetAdPrdOrdValue")
                        SetAdPrdOrdValue(record.DocNo.toString(), record.Id.toString(), record.ProcessName.toString(), record.ProcessTransId.toString(), record.BaseItemQty.toString(), record.BaseItemUOMName.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function HSNCode(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Code',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Code: {
                title: label('lblHSNCode', 'common', 'HSN Code'),
                width: '50%'
            },
            Description: {
                title: label('lblHSNDescription', 'common', 'Description'),
                width: '50%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetHSNCodeValue")
                        SetHSNCodeValue(record.Code.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function ProjectGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    if (ShowQuickCreate == true)
        $("#DivlnkQuickItemGroup").show();
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'ProjectName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            ProjectCode: {
                title: label('lblCode', 'common', 'Code'),
                width: '20%'
            },
            ProjectName: {
                title: label('lblName', 'common', 'Name'),
                width: '30%'
            },
            ProjectType: {
                title: label('lblType', 'common', 'Type'),
                width: '30%'
            },
            BOQName: {
                title: label('lblBOQ', 'common', 'BOQ'),
                width: '20%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetProjectCodeNameANDBOQ")
                        SetProjectCodeNameANDBOQ(record.ProjectName.toString(), record.Id.toString(), record.BOQId.toString());

                });
            }
        }
    });
    $(DivName).jtable('load');
}

function StockTransferReqItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblQueryParam = QueryParam;
    gblSetFunName = SetFunName;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'DocNo DESC',

        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            DocTypeName: {
                title: label('lblRequestType', 'common', 'Request Type'),
                width: '25%',
            },
            DocNo: {
                title: label('lblRequestNo', 'common', 'Request No.'),
                width: '15%',

            },
            DocDate: {
                title: label('lblRequestDate', 'common', 'Request Date'),
                width: '15%'
            },
            ItemName: {
                title: label('lblItem', 'common', 'Item'),
                width: '30%'
            },
            ItemId: {
                title: label('lblItemId', 'common', 'Item Id'),
                visibility: 'hidden'
            },
            PendingQty: {
                title: label('lblQty', 'common', 'Qty'),
                width: '10%'
            },
            UOMName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '5%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetReqIdAndItemIdName")
                        SetReqIdAndItemIdName(record.MastId.toString(), record.Id.toString(), record.DocNo.toString(), record.ItemId.toString(), record.ItemName.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function OrderItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblQueryParam = QueryParam;
    gblSetFunName = SetFunName;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'DocNo DESC',

        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            DocNo: {
                title: label('lblOrderNo', 'common', 'Order No.'),
                width: '11%',

            },
            DocDate: {
                title: label('lblOrderDate', 'common', 'Order Date'),
                width: '11%'
            },
            CustomerName: {
                title: label('lblCustomer', 'common', 'Customer'),
                width: '25%'
            },
            ItemId: {
                title: label('lblItemId', 'common', 'Item Id'),
                visibility: 'hidden'
            },
            ItemCode: {
                title: label('lblItemCode', 'common', 'Item Code'),
                width: '12%'
            },
            ItemName: {
                title: label('lblItemName', 'common', 'Item Name'),
                width: '25%'
            },
            SOQty: {
                title: label('lblQty', 'common', 'Qty'),
                width: '10%'
            },
            UOMName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '5%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetSOItemValue")
                        SetSOItemValue(record.OrderId.toString(), record.Id.toString(), record.DocNo.toString(), record.ItemId.toString(), record.ItemName.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function OrderItemGrid_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: false, //Enable selecting
        multiselect: true,
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Selection: {
                title: '<input type="checkbox" id="chkSelectDeselectAll" ></input>',
                width: '1%',
                sorting: false,
                position: 'center',
                display: function (data) { return "<input type='checkbox' name='chkGrd' id='" + data.record.Id.toString() + "ᴌ" + data.record.DocNo + "ᴌ" + data.record.ItemName.toString() + "ᴌ" + data.record.OrderId.toString() + "' style='padding-left:15px;' onclick='DeselectMain();' ></input>"; },
                columnResizable: false,
                visibility: 'fixed'

            },
            DocNo: {
                title: label('lblOrderNo', 'common', 'Order No.'),
                width: '11%',

            },
            DocDate: {
                title: label('lblOrderDate', 'common', 'Order Date'),
                width: '11%'
            },
            CustomerName: {
                title: label('lblCustomer', 'common', 'Customer'),
                width: '25%'
            },
            ItemCode: {
                title: label('lblItemCode', 'common', 'Item Code'),
                width: '12%'
            },
            ItemName: {
                title: label('lblItemName', 'common', 'Item Name'),
                width: '25%'
            },
            Qty: {
                title: label('lblQty', 'common', 'Qty'),
                width: '10%'
            },
            UOMName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '5%'
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
    $('#chkSelectDeselectAll').on('click', function () {
        $(this).closest('table').find(':checkbox').prop('checked', this.checked);
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function IssueItemGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblQueryParam = QueryParam;
    gblSetFunName = SetFunName;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'DocNo DESC',

        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            DocNo: {
                title: label('lblIssueNo', 'common', 'Issue No.'),
                width: '11%',

            },
            DocDate: {
                title: label('lblIssueDate', 'common', 'Issue Date'),
                width: '11%'
            },
            VehicleNo: {
                title: label('lblVehicleNo', 'common', 'Vehicle No'),
                width: '25%'
            },
            ItemId: {
                title: label('lblItemId', 'common', 'Item Id'),
                visibility: 'hidden'
            },
            ItemCode: {
                title: label('lblItemCode', 'common', 'Item Code'),
                width: '12%'
            },
            ItemName: {
                title: label('lblItemName', 'common', 'Item Name'),
                width: '25%'
            },
            PendingQty: {
                title: label('lblQty', 'common', 'Qty'),
                width: '10%'
            },
            UOMName: {
                title: label('lblUOM', 'common', 'UOM'),
                width: '5%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetIssueItemValue")
                        SetIssueItemValue(record.Id.toString(), record.OrderTransId.toString(), record.DocNo.toString(), record.ItemId.toString(), record.ItemName.toString(), record.VehicleNo.toString()
                            , record.LRNo.toString(), record.LRDate.toString(), record.TransporterId.toString(), record.PlantId.toString())
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function PackageGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '29%'
            },

            EffectiveDate: {
                title: label('lblPackageEffDate', 'common', 'Effective Date'),
                width: '30%'
            },
            Validity: {
                title: label('lblPackageValidity', 'common', 'Validity'),
                width: '20%'
            },
            Amount: {
                title: label('lblAmount', 'common', 'Amount'),
                width: '20%'
            }
        },
        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    //Show selected rows
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetPackages(record.Name.toString(), record.Id.toString());
                });

            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function EventsGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Title ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Title: {
                title: label('lblTitle', 'common', 'Title'),
                width: '23%'
            },

            EventCategoryName: {
                title: label('lblCategory', 'common', 'Category Name'),
                width: '16%'
            },
            EventTypeName: {
                title: label('lblEventType', 'common', 'Type'),
                width: '17%'
            },
            EventTopicName: {
                title: label('lblEventTopic', 'common', 'Topic'),
                width: '16%'
            },
            StartDate: {
                title: label('lblEventStartDate', 'common', 'Start Date'),
                width: '14%'
            },
            EndDate: {
                title: label('lblEventEndDate', 'common', 'End Date'),
                width: '20%'
            }
        },
        selectionChanged: function () {
            //Get all selected rows
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    //Show selected rows
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetEvents(record.Title.toString(), record.Id.toString());
                });

            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function ClaimReasonGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    tmpObjectType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            //Code: {
            //    title: label('lblCode', 'common', 'Code'),
            //    width: '50%'
            //},
            Name: {
                title: label('lblName', 'common', 'Name'),
                width: '100%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetClaimReason(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function LogInventoryTransGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'DocNo ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            DocDate: {
                title: label('lblDocDate', 'common', 'Doc.Date'),
                width: '10%'
            },
            DocTypeName: {
                title: label('lblDocNo', 'common', 'Doc.No.'),
                width: '14%'
            },
            DocNo: {
                title: label('lblDocNo', 'common', 'Doc.No.'),
                width: '12%'
            },
            GICustomerName: {
                title: label('lblCustomer', 'common', 'Customer'),
                width: '22%'
            },
            PlantName: {
                title: label('lblPlant', 'common', 'Plant'),
                width: '14%'
            },
            DepartmentName: {
                title: label('lblDepartment', 'common', 'Department'),
                width: '14%'
            },
            TotalAmount: {
                title: label('lblAmount', 'common', 'Amount'),
                width: '14%'
            },

        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetLogInventoryTransData(record.Id.toString(), record.DocNo.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}

function POSRetailCustomerGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: 'Name',
                width: '30%'
            },
            MobileNo: {
                title: 'Mobile No.',
                width: '30%'
            },
            StateName: {
                title: 'State',
                width: '20%'
            },
            CityName: {
                title: 'City',
                width: '20%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetPOSRetailCustomer")
                        SetPOSRetailCustomer(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function POSFranchiseMenuDefinitionItems(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'ItemName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            ItemCode: {
                title: 'Item Code',
                width: '15%'
            },
            ItemName: {
                title: 'Item Name',
                width: '20%'
            },
            Description: {
                title: 'Description',
                width: '20%'
            },
            Unit: {
                title: 'Unit',
                width: '15%'
            },
            ItemGroup: {
                title: 'Item Group',
                width: '15%'
            },
            MenuDefinition: {
                title: 'Menu Definition',
                width: '15%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetItemAndCodeValue")
                        SetItemAndCodeValue(record.ItemId.toString(), record.ItemName.toString(), record.ItemCode.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam
    });
}

function CRMLeadsGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'ItemName ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            LeadNo: {
                title: 'Lead No',
                width: '20%'
            },
            LeadTitle: {
                title: 'Lead Title',
                width: '25%'
            },
            LeadDate: {
                title: 'Lead Date',
                width: '10%'
            },
            CustomerName: {
                title: 'Customer',
                width: '25%'
            },
            ContactName: {
                title: 'Contact Name',
                width: '20%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetCRMLeadNO")
                        SetCRMLeadNO(record.LeadNo.toString(), record.Id.toString(), record.CustomerId.toString(), record.CustomerName.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam
    });
}

function ApprovedSOListGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'SalesOrderDate DESC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            SalesOrderNo: {
                title: 'SO No.',
                width: '20%'
            },
            SalesOrderDate: {
                title: 'SO Date',
                width: '10%'
            },
            Customer: {
                title: 'Customer',
                width: '25%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetApprovedSOListData(record.Id.toString(), record.SalesOrderNo.toString(), record.SalesOrderDate.toString(), record.CustomerId.toString(), record.Customer.toString(), record.PONo.toString(), record.PODate.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam
    });
}

function VoucherNarrationGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Narration ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Narration: {
                title: 'Narration',
                width: '100%'
            }
        },
        selectionChanged: function () {
            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetVoucherNarration")
                        SetVoucherNarration(record.Narration.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: QueryParam
    });
}

function PurchaseInquiryGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            DocDate: {
                title: label('lblInquiryDate', 'common', 'Inquiry Date'),
                width: '20%'
            },
            DocNo: {
                title: label('lblInquiryNo', 'common', 'Inquiry No.'),
                width: '30%'
            },
            DocTypeName: {
                title: label('lblInquiryType', 'common', 'Inquiry Type'),
                width: '50%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetPurchaseInquiryDet")
                        SetPurchaseInquiryDet(record.DocNo.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}

function QuotationGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            DocDate: {
                title: label('lblQuotationDate', 'common', 'Quotation Date'),
                width: '10%'
            },
            DocNo: {
                title: label('lblQuotationNo', 'common', 'Quotation No.'),
                width: '10%'
            },
            DocTypeName: {
                title: label('lblQuotationType', 'common', 'Quotation Type'),
                width: '20%'
            },
            Vendor_Customer_Name: {
                title: label('lblParty', 'common', 'Party'),
                width: '30%'
            },
            QuotationTotalAmount: {
                title: label('lblAmount', 'common', 'Amount'),
                width: '10%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetQuotationDet")
                        SetQuotationDet(record.DocNo.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}
function PRDAdPO_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'DocDate ASC,DocNo',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Selection: {
                title: '<input type="checkbox" id="chkSelectDeselectAll" ></input>',
                width: '1%',
                sorting: false,
                position: 'center',
                display: function (data) { return "<input type='checkbox' name='checkGrdAdPRDPO' id='" + data.record.Id.toString() + "ᴌ" + data.record.DocNo + "' style='padding-left:15px;' onclick='DeselectMain();' ></input>"; },
                columnResizable: false,
                visibility: 'fixed'

            },
            DocDate: {
                title: label('lblAdPrdOrdDate', 'common', 'Ad. Prd. Ord. Date'),
                width: '20%'
            },

            DocNo: {
                title: label('lblAdPrdOrdNo', 'common', 'Ad. Prd. Ord. No'),
                width: '15%',

            },

            POTypeName: {
                title: label('lblAdPrdOrdType', 'common', 'Ad. Prd. Ord. Type'),
                width: '25%'
            },
            ItemName: {
                title: label('lblItem', 'common', 'Item'),
                width: '25%'
            },

            Qty: {
                title: label('lblQty', 'common', 'Qty'),
                width: '15%'
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: gblQueryParam
    });
    $('#chkSelectDeselectAll').on('click', function () {
        $(this).closest('table').find(':checkbox').prop('checked', this.checked);
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}
function BOMVarientItem_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'BOMVariant ASC,ItemCode',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Selection: {
                title: '<input type="checkbox" id="chkSelectDeselectAll" ></input>',
                width: '1%',
                sorting: false,
                position: 'center',
                display: function (data){
                    return "<input type='checkbox' name='checkGrdBOM' id='" + data.record.Id.toString() + "ᴌ" + data.record.BOMVarient.toString() + "ᴌ" + data.record.ItemCode.toString() + "' style='padding-left:15px;' onclick='DeselectMain();' ></input>";
                },
                columnResizable: false,
                visibility: 'fixed'

            },
            BOMTypeName: {
                title: label('lblBOMType', 'common', 'BOM Type'),
                width: '20%'
            },

            BOMVarient: {
                title: label('lblBOMVariant', 'common', 'BOM Variant'),
                width: '15%',

            },

            ItemCode: {
                title: label('lblBOMItemCode', 'common', 'Item Code'),
                width: '25%'
            },
            ItemName: {
                title: label('lblItem', 'common', 'Item'),
                width: '25%'
            },

            PlantName: {
                title: label('lblBOMPlant', 'common', 'Plant'),
                width: '15%'
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: gblQueryParam
    });
    $('#chkSelectDeselectAll').on('click', function () {
        $(this).closest('table').find(':checkbox').prop('checked', this.checked);
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}
function CustPRDPOItemSerialMappingPendingList_Chk(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = "";
    tmpObjectDocTypeId = "";
    tmpObjectTransType = "";
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'GRDate ASC,GRNo',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Selection: {
                title: '<input type="checkbox" id="chkCustSelectDeselectAll" ></input>',
                width: '1%',
                sorting: false,
                position: 'center',
                display: function (data) { return "<input type='checkbox' name='checkCustGrdPRDPO' id='" + data.record.GRId.toString() + "ᴌ" + data.record.GRNo + "' style='padding-left:15px;' onclick='DeselectMain();' ></input>"; },
                columnResizable: false,
                visibility: 'fixed'

            },
            GRNo: {
                title: label('lblGRNo', 'common', 'GR No.'),
                width: '15%'
            },
            GRDate: {
                title: label('lblGRDate', 'common', 'GR Date'),
                width: '15%'
            },
            PrdOrdNo: {
                title: label('lblPrdOrdNo', 'common', 'Prd. Ord. No'),
                width: '15%',

            },
            GRItemCode: {
                title: label('lblGRItemCode', 'common', 'GR Item Code'),
                width: '15%'
            },
            GRItemName: {
                title: label('lblGRItemName', 'common', 'GR Item Name'),
                width: '25%'
            },
            GRItemQty: {
                title: label('lblGRQty', 'common', 'GR Qty.'),
                width: '15%'
            }
        }
    });
    $(DivName).jtable('load', {
        QueryParam: gblQueryParam
    });
    $('#chkCustSelectDeselectAll').on('click', function () {
        $(this).closest('table').find(':checkbox').prop('checked', this.checked);
    });
    $(DivName).jtable('load', { QueryParam: gblQueryParam });
}
$.fn.onTypeFinished = function (func) {
    var T = undefined, S = 0, D = 1000;
    $(this).bind("keyup", onKeyPress);//.bind("focusout", onTimeOut);
    function onKeyPress() {
        clearTimeout(T);
        if (S == 0) { S = new Date().getTime(); D = 1000; T = setTimeout(onTimeOut, 1000); return; }
        var t = new Date().getTime();
        D = (D + (t - S)) / 2; S = t; T = setTimeout(onTimeOut, D * 2);
    }

    function onTimeOut() {
        func.apply(); S = 0;
    }
    return this;
};
$("#filtAccName,#filtAccParentName,#filtAccCity,#filtAccPhone,#filtAccMobile,#filtAccCode").onTypeFinished(GetFilterData);
$("#filtItemCode,#filtItemName,#filtDescription,#filtItemUnit,#filtItemGroup,#filtBrand").onTypeFinished(GetFilterDataItem);
$("#filtItemCode_ItemWithAttr,#filtItemName_ItemWithAttr,#filtDescription_ItemWithAttr,#filtItemUnit_ItemWithAttr,#filtItemGroup_ItemWithAttr,#filtAttribute1_ItemWithAttr,#filtAttribute2_ItemWithAttr,#filtAttribute3_ItemWithAttr,#filtAttribute4_ItemWithAttr,#filtAttribute5_ItemWithAttr,#filtAttribute6_ItemWithAttr,#filtAttribute7_ItemWithAttr").onTypeFinished(GetFilterDataItemWithAttr);
$("#filtComponentCode_ComponentWithAttr,#filtComponentName_ComponentWithAttr").onTypeFinished(GetFilterDataComponentWithAttr);
$("#filtStructureName_ComponentWithAttr,#filtEntryDate_ComponentWithAttr").onTypeFinished(GetFilterDataSalesGeneralStructure);
$("#filtCityCode_CityWithAttr,#filtCityName_CityWithAttr,#filtStateName_StateWithAttr,#filtCountryName_CountryWithAttr").onTypeFinished(GetFilterDataCityWithAttr);
$("#filtCountryName,#filtStateName,#filtCityName,#filtTalukaName").val('').onTypeFinished(GetFilterDataTaluka);
$("#filtModeofAcknowledgementName").val('').onTypeFinished(GetFilterDataModeofAcknowledgement);
$("#filtPendingReasonGroupName").val('').onTypeFinished(GetFilterDataPendingReasonGroup);
$("#filtTechnicianMasterName").val('').onTypeFinished(GetFilterDataTechnicianMaster);
$("#filtCustomerTypeCode_CustomerTypeWithAttr,#filtCustomerTypeName_CustomerTypeWithAttr").onTypeFinished(GetFilterDataCustomerTypeWithAttr);
$("#filtBBatchNo,#filtBItemName,#filtBStoreName,#filtBClosing,#filtBMfgDate").onTypeFinished(GetFilterDataBatch)
$("#filtAccGrpName,#filtAccGrpParentName,#filtAccGrpCode").onTypeFinished(GetFilterDataAccGroup);
$("#filtScheduleName,#filtScheduleParentName,#filtScheduleCode").onTypeFinished(GetFilterDataSchedule);
$("#filtBanquetCatName,#filtBanquetCatParentName").onTypeFinished(GetFilterDataBanquetCategory);
$("#filtProductCategoryName,#filtProductCategoryParentName").onTypeFinished(GetFilterDataProductCategory);
$("#filtItemGrpName,#filtItemGrpParentName").onTypeFinished(GetFilterDataItemGroup);
$("#filtAssetsGrpName,#filtAssetsGrpParentName").onTypeFinished(GetFilterDataAssetsGroup);
$("#filtInvDocDate,#filtInvDocType, #filtInvParty, #filtInvDocNo,#filtInvTotalAmount").onTypeFinished(GetFilterDataInvoice);
$("#PRfiltInvDocDate,#PRfiltInvDocType, #PRfiltInvParty, #PRfiltInvDocNo, #PRfiltInvRefNo,#PRfiltInvTotalAmount").onTypeFinished(GetFilterPaymentRequestDataInvoice);
$("#filtContactName,#filtContactAddress,#filtContactCity,#filtContactState,#filtContactMobile,#filtContactPhone").onTypeFinished(GetFilterDataContact);
$("#filtServiceGrpName,#filtServiceGrpParentName").onTypeFinished(GetFilterDataServiceGroup);
$("#filtServiceCode,#filtServiceName,#filtServiceUnit,#filtServiceGroup").onTypeFinished(GetFilterDataService);
$("#filtCostGrpName,#filtCostGrpParentName").onTypeFinished(GetFilterDataCostGroup);
$("#filtAttributeDefinitionName").onTypeFinished(GetFilterDataAttributeDefinition);
$("#filtPRDProcessCode, #filtPRDProcessName").onTypeFinished(GetFilterDataPRDProcess);
$("#filtPRDOverheadCode, #filtPRDOverheadName").onTypeFinished(GetFilterDataPRDOverhead);
$("#filtCostCenterName, #filtCostCenterParentName").onTypeFinished(GetFilterDataCostCenter);
$("#filtPrjSkillName, #filtPrjSkillUnit").onTypeFinished(GetFilterDataPrjSkill);
$("#filtPrjOtherChargesName, #filtPrjOtherChargesType, #filtPrjOtherChargesUnit").onTypeFinished(GetFilterDataPrjOtherCharges);
$("#filtPrjOverheadName,#filtPrjOverheadType,#filtPrjOverheadUnit").onTypeFinished(GetFilterDataPrjOverhead);
$("#filtUserId,#filtUserName").onTypeFinished(GetFilterDataUser);
$("#filtPrjActivityName").onTypeFinished(GetFilterPrjActivity);
$("#filtPrjBOQGroupCode, #filtPrjBOQGroupName").onTypeFinished(GetFilterPrjBOQGroup);
$("#filtTaskTitle, #filtTaskType").onTypeFinished(GetFilterTasks);
$("#filtProductGrpName,#filtProductGrpParentName").onTypeFinished(GetFilterDataProductGroup);
$("#filtBOQCode,#filtBOQName").onTypeFinished(GetFilterDataBOQ);
$("#filtRetailName,#filtRetailAddress,#filtRetailMobile,#filtRetailEmail").onTypeFinished(GetFilterDataRetailCustomer);
$("#filtInsurancePolicyNo,#filtInsurancePolicyIssuer,#filtInsurancePolicyDescription").onTypeFinished(GetFilterDataInsurancePolicy);
$("#filtAttributeGrpName").onTypeFinished(GetFilterDataAttributeGroup);
$("#filtRouteName").onTypeFinished(GetFilterDataRoutes);
$("#filtRoute, #filtSubTerritory, #filtTerritory").onTypeFinished(GetFilterDataSubTerritory);
$("#filtStoreName, #filtPlantName").onTypeFinished(GetFilterDataStore);
$("#filterPlantName").onTypeFinished(GetFilterDataPlant);
$("#filtMachineCode,#filtMachineName, #filtMachineType,#filtMachineGroup").onTypeFinished(GetFilterDataMachine);
$("#filtMachineGroupName").onTypeFinished(GetFilterDataMachineGroup);
$("#filtTestTempName").onTypeFinished(GetFilterDataQCTestTempName);
$("#filtLocationName").onTypeFinished(GetFilterDataHRMSLocationData);
$("#filtDepartmentName").onTypeFinished(GetFilterDataHRMSDepartmentData);
$("#filtDesignationName").onTypeFinished(GetFilterDataHRMSDesignationData);
$("#filtGradeName").onTypeFinished(GetFilterDataHRMSGradeData);
$("#filtEmployeeTypeName").onTypeFinished(GetFilterDataHRMSEmployeeTypeData);
$("#filtTenantName").onTypeFinished(GetFilterDataTenantData);
$("#filtPresentCityName").onTypeFinished(GetFilterDataHRMSPresentCityData);
$("#filtPermanentCityName").onTypeFinished(GetFilterDataHRMSPermanentCityData);
$("#filtStructureCodeName").onTypeFinished(GetFilterDataHRMSStructureCodeData);
$("#filtAssetName").onTypeFinished(GetFilterDataHRMSAssetCodeGrid);
$("#filtIndentName").onTypeFinished(GetFilterDataHRMSIndentCodeGrid);
$("#filtMembershipNo,#filtMemberName,#filtMemberMobile").onTypeFinished(GetFilterDataMemberGrid);
$("#filtHRUserName").onTypeFinished(GetFilterDataHRMSUserData);
$("#filtPrdOrdDate,#filtPrdOrdNo,#filtPrdOrdType,#filtPrdOrdItem,#filtPrdOrdQty,#filtPrdOrdBatchNo").onTypeFinished(GetFilterDataPrdOrd);
$("#filtPrdOrdDate_Chk, #filtPrdOrdNo_Chk, #filtPrdOrdType_Chk,#filtPrdOrdItemCode_Chk, #filtPrdOrdItem_Chk, #filtPrdOrdQty_Chk").onTypeFinished(GetFilterDataPRDPOItemDetails_Chk);
$("#filtSODate,#filtSONo,#filtSOType,#filtSOCustomer,#filtSOAmount").onTypeFinished(GetFilterDataSO);
$("#filtHSNCode,#filtHSNDesc").onTypeFinished(GetFilterDataHSNCode);
$("#filtProjectCode,#filtProjectName,#filtProjectType,#filtBOQName").onTypeFinished(GetFilterDataProject);
$("#filtSTRTypeName, #filtSTRNo, #filtSTRDate, #filtSTRItemName, #filtSTRPendingQty").onTypeFinished(GetFilterDataPendingStockTransferReqItemDetails);
$("#filtOrderNo, #filtOdrerDate, #filtCustomerName").onTypeFinished(GetFilterDataOrderItemDetails);
$("#filtOrderNo_Chk, #filtOdrerDate_Chk, #filtCustomerName_Chk, #filtOrdItem_Chk").onTypeFinished(GetFilterDataOrderItemDetails_Chk);
$('#filtPackName,#filtPackEffDate,#filtPackValidity,#filtPackAmount').onTypeFinished(GetFilterDataPOSPackages);
$('#filtEveTitle,#filtEveCategory,#filtEveType,#filtEveTopic,#filtEveSDAte,#filtEveEDate').onTypeFinished(GetFilterDataClubEvents);
$("#filtGIDocNo, #filtGIDocDate, #filtGIVehicleNo").onTypeFinished(GetFilterDataIssueItemTrans);
$("#filtMasterName").onTypeFinished(GetFilterDataMasterName);
$("#filtInvItemDocDate,#filtInvItemDocType,#filtInvItemDocNo,#filtInvItem,#filtInvItemTotalAmount").onTypeFinished(GetFilterDataInvoiceWithItem);
$("#filtLogDocDate,#filtLogDocType,#filtLogDocNo,#filtLogCustomer,#filtLogPlant, #filtLogDepartment, #filtLogAmount").onTypeFinished(GetFilterDataLogInventory);
$("#filtRetailCustomerName,#filtRetailCustomerMobile,#filtRetailCustomerState,#filtRetailCustomerCity").onTypeFinished(GetFilterPOSRetailCustomerMasterData);
$("#filtPOSFranchiseeItemCode,#filtPOSFranchiseeItemName,#filtPOSFranchiseeDescription,#filtPOSFranchiseeUnit,#filtPOSFranchiseeItemGroup,#filtPOSFranchiseeMenuDefinition").onTypeFinished(GetFilterPOSFranchiseMenuDefinitionItems);
$("#filtRetDocDate,#filtRetDocNo,#filtRetRetailCustName,#filtRetRetailCustAddress,#filtRetRetailCustMobile,#filtRetRetailItemName").onTypeFinished(GetFilterVendor_Cust_Wise_RetailCustTransDetailData);
$("#filtLeadNo,#filtLeadTitle,#filtLeadDate,#filtLeadCustomer,#filtLeadContact").onTypeFinished(GetFilterCRMLeadsData);
$("#filtAssetCode,#filtAssetName,#filtAssetDescription,#filtAssetSerialNumber").onTypeFinished(GetFilterDataAssets);
$("#filtVoucherNarration").onTypeFinished(GetFilterDataVoucherNarration);
$("#filtPOSInvoiceDate,#filtPOSInvoiceNumber,#filtPOSTotalAmount,#filtPOSRetailCustomerName,#filtPOSMobileNumber,#filtPOSTableName,#filtPOSOutlet").onTypeFinished(GetFilterDataForPOSInvoice);
$("#filtAccSchCode,#filtAccSchName,#filtAccSchParentName").onTypeFinished(GetFilterDataForAccSchedule);
$("#filtPurInquiryDate,#filtPurInquiryNo, #filtPurInquiryType").onTypeFinished(GetFilterDataPurchaseInquiry);
$("#filtQuotationDate,#filtQuotationNo, #filtQuotationType, #filtQuotationParty, #filtQuotationAmount").onTypeFinished(GetFilterDataQuotation);
$("#filtAdPrdOrdDate,#filtAdPrdOrdNo, #filtAdPrdOrdItem, #filtAdPrdOrdQty, #filtAdPrdOrdPrcess").onTypeFinished(GetFilterDataPendingAdPRDPO);
$("#filtVchTypeName, #filtVchDate, #filtVchNo, #filtVchAccName, #filtVchAmount").onTypeFinished(GetFilterDataAccountVoucher);
$("#filtBinName,#filtBinPlantName, #filtBinStoreName").onTypeFinished(GetFilterDataBinMaster);
$("#filtAdPrdOrdDate_Chk, #filtAdPrdOrdNo_Chk, #filtAdPrdOrdType_Chk, #filtAdPrdOrdItem_Chk, #filtAdPrdOrdQty_Chk").onTypeFinished(GetFilterDataAdPRDPOItemDetails_Chk);
$("#filtBOMType_Chk, #filtBOMVariant_Chk, #filtBOMItemCode_Chk, #filtBOMItemName_Chk, #filtBOMPlant_Chk").onTypeFinished(GetFilterDataBOMVarientItemDetails_Chk);
$("#filtGRNo_Chk, #filtGRDate_Chk, #filtPrdOrdNo_Chk, #filtGRItemCode_Chk, #filtGRItemName_Chk,#filtGRQty_Chk").onTypeFinished(GetFilterDataPRDPOItemMappingPendingDetails_Chk);
$("#filtGEMStageName").onTypeFinished(GetFilterDataGEMStage);
$("#filtGEMProductName").onTypeFinished(GetFilterDataGEMProduct);
$("#filtOPItemName").onTypeFinished(GetFilterDataOPItem);
$("#filtTenderEnquiryNo").onTypeFinished(GetFilterDataTenderEnquiryNo);
$("#filtSONo, #filtSODate, #filtSOCustomer").onTypeFinished(GetFilterDataApprovedSONoList);
$("#filtVerticalName").onTypeFinished(GetFilterDataVertical);

function GetFilterDataOPItem() {
    var Filter = "ItemNameᴌ" + $('#filtOPItemName').val();
    $('#tblOPItemPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataTenderEnquiryNo() {
    var Filter = "EnquiryNoᴌ" + $('#filtTenderEnquiryNo').val();
    $('#tblTenderPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataApprovedSONoList() {
    var Filter = "SalesOrderNoᴌ" + $('#filtSONo').val() + "|SalesOrderDateᴌ" + $('#filtSODate').val() + "|Customerᴌ" + $('#filtSOCustomer').val();
    $('#tblApprovedSOListPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataForPOSInvoice() {
    var Filter = "InvoiceDateᴌ" + $('#filtPOSInvoiceDate').val() + "|InvoiceNumberᴌ" + $('#filtPOSInvoiceNumber').val() + "|TotalAmountᴌ" + $('#filtPOSTotalAmount').val() + "|RetailCustomerNameᴌ" + $('#filtPOSRetailCustomerName').val() + "|MobileNumberᴌ" + $('#filtPOSMobileNumber').val() + "|TableNameᴌ" + $('#filtPOSTableName').val() + "|Outletᴌ" + $('#filtPOSOutlet').val();
    $('#tblPOSInvoicePopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
    });
}

function GetFilterCRMLeadsData() {
    var Filter = "LeadNoᴌ" + $('#filtLeadNo').val() + "|LeadTitleᴌ" + $('#filtLeadTitle').val() + "|LeadDateᴌ" + $('#filtLeadDate').val() + "|CustomerNameᴌ" + $('#filtLeadCustomer').val() + "|ContactNameᴌ" + $('#filtLeadContact').val();
    $('#tblCRMLeadPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetFilterData() {
    var Filter = "Codeᴌ" + $('#filtAccCode').val() + "|Nameᴌ" + $('#filtAccName').val() + "|ParentNameᴌ" + $('#filtAccParentName').val() + "|Cityᴌ" + $('#filtAccCity').val() + "|Phoneᴌ" + $('#filtAccPhone').val() + "|Mobileᴌ" + $('#filtAccMobile').val();
    $('#tblAccPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        ObjectType: tmpObjectType
    });
}
function GetFilterDataItem() {
    var Filter = "Nameᴌ" + $('#filtItemName').val() + "|Descriptionᴌ" + $('#filtDescription').val() + "|Codeᴌ" + $('#filtItemCode').val() + "|UnitNameᴌ" + $('#filtItemUnit').val() + "|ItemGroupNameᴌ" + $('#filtItemGroup').val() + "|BrandNameᴌ" + $('#filtBrand').val();
    $('#tblItemPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        ObjectDocTypeIdSub: tmpObjectDocTypeIdSub,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterDataItemWithAttr() {
    var Filter = "IM.Nameᴌ" + $('#filtItemName_ItemWithAttr').val() + "|IM.Descriptionᴌ" + $('#filtDescription_ItemWithAttr').val() + "|IM.Codeᴌ" + $('#filtItemCode_ItemWithAttr').val() + "|UnitMaster.Symbolᴌ" + $('#filtItemUnit_ItemWithAttr').val() + "|IGM.Nameᴌ" + $('#filtItemGroup_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute1_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute2_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute3_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute4_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute5_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute6_ItemWithAttr').val() + "|IA.Valueᴌ" + $('#filtAttribute7_ItemWithAttr').val();
    $('#tblItemWithAttrPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        ObjectDocTypeIdSub: tmpObjectDocTypeIdSub,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterDataComponentWithAttr() {
    var Filter = "ComponentNameᴌ" + $('#filtComponentName_ComponentWithAttr').val() + "|ComponentCodeᴌ" + $('#filtComponentCode_ComponentWithAttr').val();
    $('#tblComponentWithAttrPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterDataSalesGeneralStructure() {
    var Filter = "TemplateNameᴌ" + $('#filtStructureName_ComponentWithAttr').val() + "|EntryDateᴌ" + $('#filtEntryDate_ComponentWithAttr').val();
    $('#tblGeneralStructureWithAttrPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterDataCityWithAttr() {
    var Filter = "Nameᴌ" + $('#filtCityName_CityWithAttr').val() + "|StateNameᴌ" + $('#filtStateName_StateWithAttr').val() + "|CountryNameᴌ" + $('#filtCountryName_CountryWithAttr').val();
    $('#tblCityWithAttrPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function GetFilterDataTaluka() {
    var Filter = "Nameᴌ" + $('#filtTalukaName').val() + "|CityNameᴌ" + $('#filtCityName').val()+ "|StateNameᴌ" + $('#filtStateName').val() + "|CountryNameᴌ" + $('#filtCountryName').val();
    $('#tblTalukaGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterDataModeofAcknowledgement() {
    var Filter = "Nameᴌ" + $('#filtModeofAcknowledgementName').val();
    $('#tblModeofAcknowledgementGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterDataPendingReasonGroup() {
    var Filter = "Nameᴌ" + $('#filtPendingReasonGroupName').val();
    $('#tblPendingReasonGroupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterDataTechnicianMaster() {
    var Filter = "Nameᴌ" + $('#filtTechnicianMasterName').val();
    $('#tblTechnicianMasterGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function GetFilterDataCustomerTypeWithAttr() {
    var Filter = "CustomerTypeNameᴌ" + $('#filtCustomerTypeName_CustomerTypeWithAttr').val();
    $('#tblCustomerTypeWithAttrPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function GetFilterDataBatch() {
    var Filter = "BatchNoᴌ" + $('#filtBBatchNo').val() + "|ItemNameᴌ" + $('#filtBItemName').val() + "|StoreNameᴌ" + $('#filtBStoreName').val() + "|Closingᴌ" + $('#filtBClosing').val() + "|MfgDateᴌ" + $('#filtBMfgDate').val();
    $('#tblBatchPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam

    });
}
function GetFilterDataAccGroup() {
    var Filter = "Codeᴌ" + $('#filtAccGrpCode').val() + "|Nameᴌ" + $('#filtAccGrpName').val() + "|ParentNameᴌ" + $('#filtAccGrpParentName').val();
    $('#tblAccGroupPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetFilterDataSchedule() {
    var Filter = "Codeᴌ" + $('#filtScheduleCode').val() + "|Nameᴌ" + $('#filtScheduleName').val() + "|ParentNameᴌ" + $('#filtScheduleParentName').val();
    $('#tblSchedulePopupGrid').jtable('load', {
        Filters: Filter,
    });
}


function GetFilterDataBanquetCategory() {
    var Filter = "|Nameᴌ" + $('#filtBanquetCatName').val() + "|ParentNameᴌ" + $('#filtBanquetCatParentName').val();
    $('#tblBanquetCategoryPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataProductCategory() {
    var Filter = "Nameᴌ" + $('#filtProductCategoryName').val() + "|ParentNameᴌ" + $('#filtProductCategoryParentName').val();
    $('#tblProductCategoryPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataItemGroup() {
    var Filter = "Nameᴌ" + $('#filtItemGrpName').val() + "|ParentNameᴌ" + $('#filtItemGrpParentName').val();
    $('#tblItemGroupPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataAssetsGroup() {
    var Filter = "Nameᴌ" + $('#filtAssetsGrpName').val() + "|ParentNameᴌ" + $('#filtAssetsGrpParentName').val();
    $('#tblAssetsGroupPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataInvoice() {
    var Filter = "DocDateᴌ" + $('#filtInvDocDate').val() + "|DocTypeNameᴌ" + $('#filtInvDocType').val() + "|Vendor_Customer_Nameᴌ" + $('#filtInvParty').val() + "|DocNoᴌ" + $('#filtInvDocNo').val() + "|InvoiceTotalAmountᴌ" + $('#filtInvTotalAmount').val();
    $('#tblInvoicePopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterPaymentRequestDataInvoice() {
    var Filter = "DocDateᴌ" + $('#PRfiltInvDocDate').val() + "|DocTypeNameᴌ" + $('#PRfiltInvDocType').val() + "|Vendor_Customer_Nameᴌ" + $('#PRfiltInvParty').val() + "|DocNoᴌ" + $('#PRfiltInvDocNo').val() + "|VInvoiceNoᴌ" + $('#PRfiltInvRefNo').val() + "|InvoiceTotalAmountᴌ" + $('#PRfiltInvTotalAmount').val();
    $('#tblPaymentRequestPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}
function GetFilterDataContact() {
    var Filter = "C_Nameᴌ" + $('#filtContactName').val() + "|C_Addressᴌ" + $('#filtContactAddress').val() + "|C_CityNameᴌ" + $('#filtContactCity').val() + "|C_StateNameᴌ" + $('#filtContactState').val() + "|C_Mobile1ᴌ" + $('#filtContactMobile').val() + "|C_Phone1ᴌ" + $('#filtContactPhone').val();
    $('#tblContactPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectDocTypeId: tmpPObjectDocTypeId

    });
}
function GetFilterDataServiceGroup() {
    var Filter = "Nameᴌ" + $('#filtServiceGrpName').val() + "|ParentNameᴌ" + $('#filtServiceGrpParentName').val();
    $('#tblServiceGroupPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataService() {
    var Filter = "Nameᴌ" + $('#filtServiceName').val() + "|Codeᴌ" + $('#filtServiceCode').val() + "|UnitNameᴌ" + $('#filtServiceUnit').val() + "|ServiceGroupNameᴌ" + $('#filtServiceGroup').val();
    $('#tblServicePopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataCostGroup() {
    var Filter = "Nameᴌ" + $('#filtCostGrpName').val() + "|ParentNameᴌ" + $('#filtCostGrpParentName').val();
    $('#tblCostGroupPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataAttributeDefinition() {
    var Filter = "Nameᴌ" + $('#filtAttributeDefinitionName').val()
    $('#tblAttributeDefinitionPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataPRDProcess() {
    var Filter = "Codeᴌ" + $('#filtPRDProcessCode').val() + "|Nameᴌ" + $('#filtPRDProcessName').val();
    $('#tblPRDProcessPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataPRDOverhead() {
    var Filter = "Codeᴌ" + $('#filtPRDOverheadCode').val() + "|Nameᴌ" + $('#filtPRDOverheadName').val();
    $('#tblPRDOverheadPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataCostCenter() {
    var Filter = "Nameᴌ" + $('#filtCostCenterName').val() + "|ParentNameᴌ" + $('#filtCostCenterParentName').val();
    $('#tblCostCenterPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataPrjSkill() {
    var Filter = "Nameᴌ" + $('#filtPrjSkillName').val() + "|Unitᴌ" + $('#filtPrjSkillUnit').val();
    $('#tblPrjSkillPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataPrjOtherCharges() {

    var Filter = "Nameᴌ" + $('#filtPrjOtherChargesName').val() + "|Typeᴌ" + $('#filtPrjOtherChargesType').val() + "|Unitᴌ" + $('#filtPrjOtherChargesUnit').val();
    $('#tblPrjOtherChargesPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataPrjOverhead() {
    var Filter = "Nameᴌ" + $('#filtPrjOverheadName').val() + "|Typeᴌ" + $('#filtPrjOverheadType').val() + "|Unitᴌ" + $('#filtPrjOverheadUnit').val();
    $('#tblPrjOverheadPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterPrjActivity() {
    var Filter = "Nameᴌ" + $('#filtPrjActivityName').val();
    $('#tblPrjActivityPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterPrjBOQGroup() {
    var Filter = "Nameᴌ" + $('#filtPrjBOQGroupName').val() + "|Codeᴌ" + $('#filtPrjBOQGroupCode').val();
    $('#tblPrjBOQGroupPopupGrid').jtable('load', {
        Filters: Filter,
        PObjectDocTypeId: tmpPObjectDocTypeId,
    });
}
function GetFilterDataUser() {
    var Filter = "UserNameᴌ" + $('#filtUserName').val() + "|UserIdᴌ" + $('#filtUserId').val();
    $('#tblUserPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataGEMProduct() {
    var Filter = "Nameᴌ" + $('#filtGEMProductName').val();
    $('#tblGEMProductPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataGEMStage() {
    var Filter = "Nameᴌ" + $('#filtGEMStageName').val();
    $('#tblGEMStagePopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataVertical() {
    var Filter = "Nameᴌ" + $('#filtVerticalName').val();
    $('#tblVerticalPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterTasks() {
    var Filter = "Nameᴌ" + $('#filtTaskTitle').val() + "|TaskTypeNameᴌ" + $('#filtTaskType').val();
    $('#tblTasksPopupGrid').jtable('load', {
        Filters: Filter,
        ObjectType: tmpObjectType,
        PObjectDocTypeId: tmpPObjectDocTypeId,

    });
}
function GetFilterDataProductGroup() {
    var Filter = "Nameᴌ" + $('#filtProductGrpName').val() + "|ParentNameᴌ" + $('#filtProductGrpParentName').val();
    $('#tblProductGroupPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataAssets() {
    var Filter = "Codeᴌ" + $('#filtAssetCode').val() + "|Nameᴌ" + $('#filtAssetName').val() + "|Descriptionᴌ" + $('#filtAssetDescription').val() + "|SerialNumberᴌ" + $('#filtAssetSerialNumber').val();
    $('#tblAssetsListGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataBOQ() {
    var Filter = "Codeᴌ" + $('#filtBOQCode').val() + "|Nameᴌ" + $('#filtBOQName').val();
    $('#tblBOQPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataRetailCustomer() {
    var Filter = "Nameᴌ" + $('#filtRetailName').val() + "|Addressᴌ" + $('#filtRetailAddress').val() + "|Mobileᴌ" + $('#filtRetailMobile').val() + "|Emailᴌ" + $('#filtRetailEmail').val();
    $('#tblRetailPopupGrid').jtable('load', {
        Filters: Filter,
        ObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
    });
}
function GetFilterDataInsurancePolicy() {
    var Filter = "PolicyNoᴌ" + $('#filtInsurancePolicyNo').val() + "|Issuerᴌ" + $('#filtInsurancePolicyIssuer').val() + "|Descriptionᴌ" + $('#filtInsurancePolicyDescription').val();
    $('#tblInsurancePolicyPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataAttributeGroup() {
    var Filter = "Nameᴌ" + $('#filtAttributeGrpName').val();
    $('#tblAttributeGroupPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataRoutes() {
    var Filter = "Nameᴌ" + $('#filtRouteName').val();
    $('#tblRoutePopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataSubTerritory() {
    var Filter = "Nameᴌ" + $('#filtSubTerritory').val() + "|RouteNameᴌ" + $('#filtRoute').val() + "|SalesTerritoriesNameᴌ" + $('#filtTerritory').val();
    $('#tblSubTerritoryPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectDocTypeId: tmpPObjectDocTypeId
    });
}

function GetFilterDataStore() {
    var Filter = "Nameᴌ" + $('#filtStoreName').val() + "|PlantNameᴌ" + $('#filtPlantName').val();
    $('#tblStorePopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}


function GetFilterDataPlant() {
    var Filter = "Nameᴌ" + $('#filterPlantName').val();
    $('#tblPlantPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetFilterDataMachine() {
    var Filter = "Codeᴌ" + $('#filtMachineCode').val() + "|Nameᴌ" + $('#filtMachineName').val() + "|Typeᴌ" + $('#filtMachineType').val() + "|MachineGroupᴌ" + $('#filtMachineGroup').val();
    $('#tblMachinePopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetFilterDataMachineGroup() {
    var Filter = "Nameᴌ" + $('#filtMachineGroupName').val();
    $('#tblMachineGroupPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetFilterDataQCTestTempName() {
    var Filter = "Nameᴌ" + $('#filtTestTempName').val();
    $('#tblTestTempPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetFilterDataHRMSLocationData() {
    var Filter = "Nameᴌ" + $('#filtLocationName').val();
    $('#tblLocationPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSDepartmentData() {
    var Filter = "Nameᴌ" + $('#filtDepartmentName').val();
    $('#tblDepartmentPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSDesignationData() {
    var Filter = "Nameᴌ" + $('#filtDesignationName').val();
    $('#tblDesignationPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSGradeData() {
    var Filter = "Nameᴌ" + $('#filtGradeName').val();
    $('#tblGradePopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSEmployeeTypeData() {
    var Filter = "Nameᴌ" + $('#filtEmployeeTypeName').val();
    $('#tblEmployeeTypePopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataTenantData() {
    var Filter = "Nameᴌ" + $('#filtTenantName').val();
    $('#tblTenantPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSPresentCityData() {
    var Filter = "Nameᴌ" + $('#filtPresentCityName').val();
    $('#tblPresentCityPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataHRMSPermanentCityData() {
    var Filter = "Nameᴌ" + $('#filtPermanentCityName').val();
    $('#tblPermanentCityPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSStructureCodeData() {
    var Filter = "Nameᴌ" + $('#filtStructureCodeName').val();
    $('#tblStructureCodePopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSAssetCodeGrid() {
    var Filter = "Nameᴌ" + $('#filtAssetName').val();
    $('#tblAssetPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSIndentCodeGrid() {
    var Filter = "Nameᴌ" + $('#filtIndentName').val();
    $('#tblIndentPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataMemberGrid() {
    var Filter = "FirstNameᴌ" + $('#filtMemberName').val() + "|ConatctMobileᴌ" + $('#filtMemberMobile').val() + "|MembershipCodeᴌ" + $('#filtMembershipNo').val();
    $('#tblMemberPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataHRMSUserData() {
    var Filter = "" + $('#filtHRUserName').val();
    $('#tblUserPopupGrid').jtable('load', {
        UserName: Filter,
    });
}

function GetFilterDataSO() {
    var Filter = "DocDateᴌ" + $('#filtSODate').val() + "|DocNoᴌ" + $('#filtSONo').val() + "|DocTypeNameᴌ" + $('#filtSOType').val() + "|Vendor_Customer_Nameᴌ" + $('#filtSOCustomer').val() + "|OrderTotalAmountᴌ" + $('#filtSOAmount').val();
    $('#tblSOPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType

    });
}

function GetFilterDataPrdOrd() {
    var Filter = "DocDateᴌ" + $('#filtPrdOrdDate').val() + "|DocNoᴌ" + $('#filtPrdOrdNo').val() + "|POTypeNameᴌ" + $('#filtPrdOrdType').val() + "|ItemNameᴌ" + $('#filtPrdOrdItem').val() + "|Qtyᴌ" + $('#filtPrdOrdQty').val() + "|BatchNoᴌ" + $('#filtPrdOrdBatchNo').val();
    $('#tblProdOrderPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam

    });
}

function GetFilterDataPRDPOItemDetails_Chk() {
    var Filter = "DocNoᴌ" + $('#filtPrdOrdNo_Chk').val() + "|DocDateᴌ" + $('#filtPrdOrdDate_Chk').val() + "|POTypeNameᴌ" + $('#filtPrdOrdType_Chk').val() + "|ItemCodeᴌ" + $('#filtPrdOrdItemCode_Chk').val() + "|ItemNameᴌ" + $('#filtPrdOrdItem_Chk').val() + "|Qtyᴌ" + $('#filtPrdOrdQty_Chk').val();
    $('#tblProdOrderPopupGrid_Chk').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetFilterDataHSNCode() {
    var Filter = "Codeᴌ" + $('#filtHSNCode').val() + "|Descriptionᴌ" + $('#filtHSNDesc').val();
    $('#tblHSNCode').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam

    });
}

function GetFilterDataProject() {
    var Filter = "ProjectCodeᴌ" + $('#filtProjectCode').val() + "|ProjectNameᴌ" + $('#filtProjectName').val() + "|ProjectTypeᴌ" + $('#filtProjectType').val() + "|BOQNameᴌ" + $('#filtBOQName').val();
    $('#tblProjectPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataPendingStockTransferReqItemDetails() {
    var Filter = "DocTypeNameᴌ" + $('#filtSTRTypeName').val() + "|DocNoᴌ" + $('#filtSTRNo').val() + "|DocDateᴌ" + $('#filtSTRDate').val() + "|ItemNameᴌ" + $('#filtSTRItemName').val() + "|PendingQtyᴌ" + $('#filtSTRPendingQty').val();
    $('#tblPendingStockTransferReqItemPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function GetFilterDataOrderItemDetails() {
    var Filter = "DocNoᴌ" + $('#filtOrderNo').val() + "|DocDateᴌ" + $('#filtOdrerDate').val() + "|CustomerNameᴌ" + $('#filtCustomerName').val();
    $('#tblOrderItemPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function GetFilterDataOrderItemDetails_Chk() {
    var Filter = "DocNoᴌ" + $('#filtOrderNo_Chk').val() + "|DocDateᴌ" + $('#filtOdrerDate_Chk').val() + "|CustomerNameᴌ" + $('#filtCustomerName_Chk').val() + "|ItemNameᴌ" + $('#filtOrdItem_Chk').val();
    $('#tblOrderItemPopupGrid_Chk').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetFilterDataPOSPackages() {
    var Filter = "Nameᴌ" + $('#filtPackName').val() + "|EffectiveDateᴌ" + $('#filtPackEffDate').val() + "|Validityᴌ" + $('#filtPackValidity').val() + "|Amountᴌ" + $('#filtPackAmount').val();
    $('#tblPackagesPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
    });
}

function GetFilterDataClubEvents() {
    var Filter = "Titleᴌ" + $('#filtEveTitle').val() + "|EventCategoryNameᴌ" + $('#filtEveCategory').val() + "|EventTypeNameᴌ" + $('#filtEveType').val() + "|EventTopicNameᴌ" + $('#filtEveTopic').val() + "|StartDateᴌ" + $('#filtEveSDAte').val() + "|EndDateᴌ" + $('#filtEveEDate').val();
    $('#tblEventPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
    });
}

function GetFilterDataIssueItemTrans() {
    var Filter = "DocNoᴌ" + $('#filtGIDocNo').val() + "|DocDateᴌ" + $('#filtGIDocDate').val() + "|VehicleNoᴌ" + $('#filtGIVehicleNo').val();
    $('#tblIssueItemPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}
function GetFilterDataMasterName() {
    var Filter = "Nameᴌ" + $('#filtMasterName').val()
    $('#tblMasterNamePopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataInvoiceWithItem() {
    var Filter = "DocDate?" + $('#filtInvItemDocDate').val() + "|DocTypeName?" + $('#filtInvItemDocType').val() + "|DocNo?" + $('#filtInvItemDocNo').val() + "|Item?" + $('#filtInvItem').val() + "|InvoiceTotalAmount?" + $('#filtInvItemTotalAmount').val();
    $('#tblInvoiceWithItemPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function GetFilterDataLogInventory() {
    var Filter = "DocDateᴌ" + $('#filtLogDocDate').val() + "|DocTypeNameᴌ" + $('#filtLogDocType').val() + "|DocNoᴌ" + $('#filtLogDocNo').val() + "|GICustomerNameᴌ" + $('#filtLogCustomer').val() + "|PlantNameᴌ" + $('#filtLogPlant').val() + "|DepartmentNameᴌ" + $('#filtLogDepartment').val() + "|TotalAmountᴌ" + $('#filtLogAmount').val();
    $('#tblLogInventoryTransPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}

function GetWebGridItemFilterData(Pages) {
    $("#divLoading1").show();

    var VendorCustomer = $('#WGCustomer').val().replace(/ /g, "ǒ");
    var VendorCustomerId = $('#WGCustomerId').val();

    var Filter = "";
    var ItemCode = $('#FiltWG_ItemCode').val().replace(/ /g, "ǒ");
    var ItemName = $("#FiltWG_ItemName").val().replace(/ /g, "ǒ");

    var ItemGroup = "";
    if ($("#FiltWG_ItemGroup option:selected").text() != "-- Select Item Group --")
        ItemGroup = $("#FiltWG_ItemGroup option:selected").text().replace(/ /g, "ǒ");

    var SubItemGroup = "";
    if ($("#FiltWG_SubItemGroup option:selected").text() != "-- Select Sub Item Group --")
        SubItemGroup = $("#FiltWG_SubItemGroup option:selected").text().replace(/ /g, "ǒ");

    var SubSubItemGroup = "";
    if ($("#FiltWG_SubSubItemGroup option:selected").text() != "-- Select Sub Sub Item Group --")
        SubSubItemGroup = $("#FiltWG_SubSubItemGroup option:selected").text().replace(/ /g, "ǒ");

    var Brand = "";
    if ($("#FiltWG_Brand option:selected").text() != "-- Select Brand --")
        Brand = $("#FiltWG_Brand option:selected").text().replace(/ /g, "ǒ");
    var AttributeGroup = "";
    if ($("#FiltWG_AttributeGroup option:selected").text() != "-- Select Attribute Group --")
        AttributeGroup = $("#FiltWG_AttributeGroup option:selected").text().replace(/ /g, "ǒ");

    var SKU = $("#FiltWG_SKU").val().replace(/ /g, "ǒ");
    var Remarks = $("#FiltWG_Remarks").val().replace(/ /g, "ǒ");
    var Sorting = $("#WGSorting").val();
    var SortDir = $("#WGSortDir").val();

    VendorCustomer = VendorCustomer.replace("&", "¤");
    ItemCode = ItemCode.replace("&", "¤");
    ItemName = ItemName.replace("&", "¤");
    ItemGroup = ItemGroup.replace("&", "¤");
    SubItemGroup = SubItemGroup.replace("&", "¤");
    SubSubItemGroup = SubSubItemGroup.replace("&", "¤");
    Brand = Brand.replace("&", "¤");
    AttributeGroup = AttributeGroup.replace("&", "¤");
    SKU = SKU.replace("&", "¤");
    Remarks = Remarks.replace("&", "¤");

    var AttributeGroupId = 1;

    Filter += "IM.Codeᴌ" + ItemCode + "|IM.Nameᴌ" + ItemName + "|ItemGroupNameᴌ" + ItemGroup + "|SubItemGroupNameᴌ" + SubItemGroup + "|SubSubItemGroupNameᴌ" + SubSubItemGroup + "|IM.SKUᴌ" + SKU + "|IA.Valueᴌ" + AttributeGroup + "|BR.Nameᴌ" + Brand + "|IM.Remarksᴌ" + Remarks;
    var Link = ITX3ResolveUrl("WebGrid/ItemList?Page=" + Pages + "&Filters=" + Filter + "&sort=" + Sorting + "&sortdir=" + SortDir + "&VendorCustomer=" + VendorCustomer + "&VendorCustomerId=" + VendorCustomerId + "&AttributeGroupId=" + AttributeGroupId);
    $("#dialog-ItemList").dialog('close');
    $("#dialog-ItemList").dialog({
        title: "Select Item",
        resizable: false,
        height: 540,
        width: 1200,
        modal: true,
        open: function (event, ui) {
            $(this).load(Link);
        }
    });
}

function GetFilterPOSRetailCustomerMasterData() {
    var Filter = "Nameᴌ" + $('#filtRetailCustomerName').val() + "|MobileNoᴌ" + $('#filtRetailCustomerMobile').val() + "|StateNameᴌ" + $('#filtRetailCustomerState').val() + "|CityNameᴌ" + $('#filtRetailCustomerState').val();
    $('#tblPOSRetailCustomerPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterPOSFranchiseMenuDefinitionItems() {
    var Filter = "ItemNameᴌ" + $('#filtPOSFranchiseeItemName').val() + "|ItemCodeᴌ" + $('#filtPOSFranchiseeItemCode').val() + "|Descriptionᴌ" + $('#filtPOSFranchiseeDescription').val() + "|Unitᴌ" + $('#filtPOSFranchiseeUnit').val() + "|ItemGroupᴌ" + $('#filtPOSFranchiseeItemGroup').val() + "|MenuDefinitionᴌ" + $('#filtPOSFranchiseeMenuDefinition').val();
    $('#POSFranchiseMenuDefinitionItemsGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterVendor_Cust_Wise_RetailCustTransDetailData() {
    var Filter = "DocDateᴌ" + $('#filtRetDocDate').val() + "|DocNoᴌ" + $('#filtRetDocNo').val() + "|RetailCust_Nameᴌ" + $('#filtRetRetailCustName').val() + "|RetailCust_Addressᴌ" + $('#filtRetRetailCustAddress').val() + "|RetailCust_MobileNoᴌ" + $('#filtRetRetailCustMobile').val() + "|ItemNameᴌ" + $('#filtRetRetailItemName').val();
    $('#tblVCWiseRetailCustTransDetailPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function GetFilterDataVoucherNarration() {
    var Filter = "Narrationᴌ" + $('#filtVoucherNarration').val();
    $('#tblVoucherNarrationPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataForAccSchedule() {
    var Filter = "Codeᴌ" + $('#filtAccSchCode').val() + "|Nameᴌ" + $('#filtAccSchName').val() + "|ParentNameᴌ" + $('#filtAccSchParentName').val();
    $('#tblAccSchedulePopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
    });
}

function GetFilterDataPurchaseInquiry() {
    var Filter = "DocDateᴌ" + $('#filtPurInquiryDate').val() + "|DocNoᴌ" + $('#filtPurInquiryNo').val() + "|DocTypeNameᴌ" + $('#filtPurInquiryType').val();
    $('#tblPurInquiryPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataQuotation() {
    var Filter = "DocDateᴌ" + $('#filtQuotationDate').val() + "|DocNoᴌ" + $('#filtQuotationNo').val() + "|DocTypeNameᴌ" + $('#filtQuotationType').val() + "|Vendor_Customer_Nameᴌ" + $('#filtQuotationParty').val() + "|QuotationTotalAmountᴌ" + $('#filtQuotationAmount').val();
    $('#tblQuotationPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataPendingAdPRDPO() {
    var Filter = "DocDateᴌ" + $('#filtAdPrdOrdDate').val() + "|DocNoᴌ" + $('#filtAdPrdOrdNo').val() + "|BaseItemNameᴌ" + $('#filtAdPrdOrdItem').val() + "|BaseItemQtyᴌ" + $('#filtAdPrdOrdQty').val() + "|ProcessNameᴌ" + $('#filtAdPrdOrdPrcess').val();
    $('#tblAdProdOrderPopupGrid').jtable('load', {
        Filters: Filter,
    });
}

function GetFilterDataAccountVoucher() {
    var Filter = "VtypeIdᴌ" + $('#filtVchTypeName').val() + "|VDateᴌ" + $('#filtVchDate').val() + "|VNoᴌ" + $('#filtVchNo').val() + "|AccNameᴌ" + $('#filtVchAccName').val() + "|Amountᴌ" + $('#filtVchAmount').val();
    $('#tblVoucherListPopupGrid').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam,
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType
    });
}

function GetFilterDataBinMaster() {
    var Filter = "Nameᴌ" + $('#filtBinName').val() + "|PlantNameᴌ" + $('#filtBinPlantName').val() + "|StoreNameᴌ" + $('#filtBinStoreName').val();
    $('#tblBinMasterPopupGrid').jtable('load', {
        Filters: Filter,
    });
}
function GetFilterDataAdPRDPOItemDetails_Chk() {
    var Filter = "DocNoᴌ" + $('#filtAdPrdOrdNo_Chk').val() + "|DocDateᴌ" + $('#filtAdPrdOrdDate_Chk').val() + "|POTypeNameᴌ" + $('#filtAdPrdOrdType_Chk').val() + "|ItemNameᴌ" + $('#filtAdPrdOrdItem_Chk').val() + "|Qtyᴌ" + $('#filtAdPrdOrdQty_Chk').val();
    $('#tblAdProdOrderPopupGrid_Chk').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}
function GetFilterDataBOMVarientItemDetails_Chk() {
    var Filter = "BOMTypeNameᴌ" + $('#filtBOMType_Chk').val() + "|BOMVarientᴌ" + $('#filtBOMVariant_Chk').val() + "|ItemCodeᴌ" + $('#filtBOMItemCode_Chk').val() + "|ItemNameᴌ" + $('#filtBOMItemName_Chk').val() + "|PlantNameᴌ" + $('#filtBOMPlant_Chk').val();
    $('#tblBOMItemPopupGrid_Chk').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}
function GetFilterDataPRDPOItemMappingPendingDetails_Chk() {
    var Filter = "GRNoᴌ" + $('#filtGRNo_Chk').val() + "|GRDateᴌ" + $('#filtGRDate_Chk').val() + "|PrdOrdNoᴌ" + $('#filtPrdOrdNo_Chk').val() + "|GRItemCodeᴌ" + $('#filtGRItemCode_Chk').val() + "|GRItemNameᴌ" + $('#filtGRItemName_Chk').val() + "|GRItemQtyᴌ" + $('#filtGRQty_Chk').val();
    $('#tblCustProdOrderPopupGrid_Chk').jtable('load', {
        Filters: Filter,
        QueryParam: gblQueryParam
    });
}
function TalukaGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;


    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblTalukaName', 'common', 'Taluka Name'),
                width: '10%'
            },
            CityName: {
                title: label('lblCityName', 'common', 'City Name'),
                width: '10%'
            },
            StateName: {
                title: label('lblStateName', 'common', 'State Name'),
                width: '10%'
            },
            CountryName: {
                title: label('lblCountryName', 'common', 'Country Name'),
                width: '10%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');


            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    //debugger;
                    var record = $(this).data('record');

                    $('.jtable-row-selected').removeClass('jtable-row-selected');

                    if (gblSetFunName == "SetTaluka")
                        SetTaluka(record.Name.toString(), record.Id.toString(), record.CityId.toString(), record.CityName.toString(), record.StateId.toString(), record.StateName.toString(), record.CountryId.toString(), record.CountryName.toString());
                    else
                        SetTaluka(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        QueryParam: gblQueryParam,
        Filters: "Nameᴌ" + $('#filtTalukaName').val() + "|CityNameᴌ" + $('#filtCityName').val() + "|StateNameᴌ" + $('#filtStateName').val() + "|CountryNameᴌ" + $('#filtCountryName').val()
    });
}
function ModeofAcknowledgementGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;


    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', ' Name'),
                width: '10%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');


            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    //debugger;
                    var record = $(this).data('record');

                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetModeofAcknowledgement(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        QueryParam: gblQueryParam,
        Filters: "Nameᴌ" + $('#filtModeofAcknowledgementName').val()
    });
}
function PendingReasonGroupGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;


    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', ' Name'),
                width: '10%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');


            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    //debugger;
                    var record = $(this).data('record');

                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetPendingReasonGroup(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        QueryParam: gblQueryParam,
        Filters: "Nameᴌ" + $('#filtPendingReasonGroupName').val()
    });
}
function TechnicianMasterGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, PObjectType, PObjectDocTypeId, PObjectTransType) {
    gblSetFunName = SetFunName;
    gblQueryParam = QueryParam;
    tmpObjectType = PObjectType;
    tmpObjectDocTypeId = PObjectDocTypeId;
    tmpObjectTransType = PObjectTransType;


    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {

            Name: {
                title: label('lblName', 'common', ' Name'),
                width: '10%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');


            if ($selectedRows.length > 0) {

                $selectedRows.each(function () {
                    //debugger;
                    var record = $(this).data('record');

                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    SetTechnicianMaster(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load', {
        PObjectType: tmpObjectType,
        PObjectDocTypeId: tmpObjectDocTypeId,
        PObjectTransType: tmpObjectTransType,
        QueryParam: gblQueryParam,
        Filters: "Nameᴌ" + $('#filtTechnicianMasterName').val()
    });
}

function VerticalGrid(ListAction, Controller, ObjectType, QueryParam, Filters, Sorting, SetFunName, DivName, ShowQuickCreate) {
    gblSetFunName = SetFunName;
    $(DivName).jtable({
        paging: true,
        pageSize: 10,
        sorting: true,
        selecting: true, //Enable selecting
        multiselect: false,
        selectOnRowClick: true,
        defaultSorting: 'Name ASC',
        actions: {
            listAction: ITX3ResolveUrl(Controller + '/' + ListAction + "?ObjectType=" + ObjectType + "&QueryParam=" + QueryParam)
        },
        fields: {
            Name: {
                title: label('lblVertical', 'common', 'Vertical'),
                width: '40%'
            }
        },
        selectionChanged: function () {

            var $selectedRows = $(DivName).jtable('selectedRows');
            if ($selectedRows.length > 0) {
                $selectedRows.each(function () {
                    var record = $(this).data('record');
                    $('.jtable-row-selected').removeClass('jtable-row-selected');
                    if (gblSetFunName == "SetVertical")
                        SetVertical(record.Name.toString(), record.Id.toString());
                });
            }
        }
    });
    $(DivName).jtable('load');
}
