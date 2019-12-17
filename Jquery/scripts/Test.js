var mEmpId = 0;
var $table = $('#table');
var mydata = [{ "id": 0, "name": "test0", "price": "$0" }, { "id": 1, "name": "test1", "price": "$1" }, { "id": 2, "name": "test2", "price": "$2" }, { "id": 3, "name": "test3", "price": "$3" }, { "id": 4, "name": "test4", "price": "$4" }, { "id": 5, "name": "test5", "price": "$5" }];




$(document).ready(function () {
    //alert('Test');

    FillDesignation();
    FillDataGrid();

    $("#btn_getData").click(function () {
        GetData();
    });

    $("#btn_showVal").click(function () {
        alert($('#txt_name').val());
    });

    $("#btn_Save").click(function () {
        saveData();
    });

    $("#btn_go").click(function () {
        window.location.href = 'aa.html';
    });

    //$(function () {
    //    $('#table').bootstrapTable({
    //        data: mydata
    //    });
    //});

    //$(document).ready(function () {
    //    $('#table').DataTable({
    //        data: mydata,
    //        columns: [
    //            { title: "Name", data: 'id' },
    //            { title: "Position", data: 'name' },
    //            { title: "Office", data: 'price' }

    //        ]
    //    });
    //});

});





function GetData() {
    $.ajax({
        type: "POST",
        url: "CodeBehind/TestCode.aspx/GetData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#Content").text(response.d);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}


function validate() {
    if ($.trim($("#txt_name").val()) === "") {
        alert("Please Enter Name");
        $("#txt_name").focus();
        return false;
    }
    else if ($.trim($("#ddl_designation").val()) === "-1") {
        alert("Please Select ");
        $("#ddl_category").focus();
        return false;
    }
    return true;
}

function saveData() {
    var name = $('#txt_name').val();
    var designationId = $('#ddl_designation').val();
    var salary = $('#txt_Salary').val();
    var status;
    if ($("#ddl_status").val() == "active") {
        status = true;
    }
    else {
        status = false;
    }
    if (validate() == true) {
        try {
            
            var parameter = { "EmpId": mEmpId, "Name": name, "Designation": designationId, "Salary": salary, "Status": status };
            $.ajax({
                
                type: "POST",
                url: "CodeBehind/TestCode.aspx/SaveData",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
               
                success: function (response) {
                    debugger;
                    var data = eval('(' + response.d + ')');
                    if (data == -1) {
                        alert("failed");
                    }

                    else if (data == 1) {
                        alert("Emp Details successfully inserted  ");
                        ClearData();
                    }

                    else if (data == 2) {
                        alert("Emp Details successfully Updated  ");
                        ClearData();
                    }
                }
            });
        }
        catch (e) {
            alert("Error " + e);
        }
    }
}

function ClearData() {
    $('#ddl_designation').val('-1');
    $('#txt_name').val('');
    $('#txt_Salary').val('');
    mEmpId = 0;

}

function FillDataGrid() {
    try {
        $.ajax({
            type: "POST",
            url: "CodeBehind/TestCode.aspx/GetEmp",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var jsonObj = result.d;
                var data2 = $.parseJSON(jsonObj);
                var dataTable = data2.Table
                var table = $('#table').DataTable(
                {
                    data: dataTable,
                    //"bFilter": false,
                    //"bAutoWidth": false,
                    //"bLengthChange": false,
                    columns: [
                     { title: "Sl No", defaultContent: '', "orderable": false },
                    { title: "EmpID", data: 'EmpID', "visible": false },
                    { title: "Employee Name", data: 'Empname' },
                    { title: "DeignationId", data: 'Designation', "visible": false },
                    { title: "Salary", data: 'EmpSalary'},
                    { title: "Status", data: 'Status', "visible": false },
                    { title: "Action", data: null, "mRender": function (o) { return '<button type="button" class="btn btn-info" onclick="ViewData(' + o.EmpID + ')" ><span class="fa fa-eye"></span></button>&nbsp;&nbsp;<button type="button" class="btn btn-danger" onClick="DeleteProduct(' + o.ProductKey + ')" ><span class="fa fa-trash"></span></button>'; } }
                    ],
                    "order": [[1, 'asc']]
                });
                table.on('order.dt search.dt', function () {
                    table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
                }).draw();
            },
            failure: function (msg) {
                alert("Error " + msg);
            },
            error: function (err) {
                alert("Error " + err.response);
            }
        });
    } catch (e) {
        alert("Error " + e);
    }
};

function ViewData(EmpId) {
    mEmpId = EmpId;
    var parameter = { "EmpId": mEmpId};
    $.ajax({
        type: "POST",
        url: "CodeBehind/TestCode.aspx/GetEmpById",
        data: JSON.stringify(parameter),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            dataResult = eval('(' + response.d + ')');;

            $('#txt_name').val(dataResult[0].Empname);
            $('#txt_Salary').val(dataResult[0].EmpSalary);
            $('#ddl_designation').val(dataResult[0].Designation);
            if (dataResult[0].Status == true) {
                $("#ddl_status").val('active');
            }
            else {
                $("#ddl_status").val('notactive');
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

//-------Function Fill Designation------//
function FillDesignation() {
    try {
        $.ajax({
            type: "POST",
            url: "CodeBehind/TestCode.aspx/GetActiveCategoryNames",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                var listItems = "";
                listItems += '<option value=-1>' + "---Select---" + '</option>';
                var data = result.d;
                var datas = eval('(' + result.d + ')');
                $("#ddl_designation").empty();
                if (datas !== null) {
                    for (var i = 0; i < datas.length; i++) {
                        var mydata = datas[i];
                        listItems += "<option value='" + mydata.DesignationID + "'>" + mydata.Designation + "</option>";
                    }
                }
                $("#ddl_designation").html(listItems);
            },
            failure: function (response) {
                alert(response.d);
            }
        });

    } catch (e) {
        alert("Error " + e);
    }
};


