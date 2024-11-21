$(document).ready(function () {
    LoadTask();
    $('body').on('click', ".AddMemoryMaker", function () {
        LoadTriggerModel();
    });
    $('body').on('click', ".EditTask", function () {
        LoadEditTaskModel($(this).attr('id'));
    });
    
    $("#AllMemoryMaker").click(function () {
        $('#tbMemoryMaker').DataTable().page.len(5).draw();
    });
    $("#reset").click(function () {
        ////$.fn.dataTable.ext.search.pop();
        $('#tbMemoryMaker').DataTable().page.len(1).draw();
       ///TableTrig.draw();
    });
    $('body').on('submit', "#MemoryMakerForm", function (e) {
        e.preventDefault(); // avoid to execute the actual submit of the form.
        var form = $(this);
        var actionUrl = form.attr('action');
        SubmitNewTask(form, actionUrl);
    });

});
function LoadTriggerModel() {
    $("#offcanvas-body").html('');
    //$("#msModal").modal("show");
    $.ajax({
        type: "Get",
        url: baseUrl + "UProfile/AddMemoryMaker",
        success: function (data) {
            Modeldata(data, false, "Add new memory", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
function LoadTask() {
    TableTrig = $('#tbMemoryMaker').DataTable({
        "destroy": true,
        "autoWidth": false,
        "scrollX": true,
        "scrollY": false,
        "searching": false,
        "ordering": false,
        "lengthMenu": [1, 5, 10, 15, 20, 25, 50, 100],
        "pageLength": 1,
        "ajax": {
            "url": baseUrl + "UProfile/GetUsersMemoryMakerList",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "One",
                "render": function (AccountID, type, full, meta) {
                    let text = full.One;
                    let truncated = text.length > 10 ? text.substring(0, 10) + '...' : text;
                    return `<div class="detailTaskData" data-bs-toggle="tooltip"
                        title="${full.One}">${truncated}</div>`;
                }
            },
            {
                "data": "Two",
                "render": function (AccountID, type, full, meta) {
                    let text = full.two;
                    let truncated = text.length > 10 ? text.substring(0, 10) + '...' : text;
                    return `<div class="detailTaskData" data-bs-toggle="tooltip"
                        title="${full.two}">${truncated}</div>`;
                }
            },
            {
                "data": "Three",
                "render": function (AccountID, type, full, meta) {
                    let text = full.three;
                    let truncated = text.length > 10 ? text.substring(0, 10) + '...' : text;
                    return `<div class="detailTaskData" data-bs-toggle="tooltip"
                        title="${full.three}">${truncated}</div>`;
                }
            },
            {
                "data": "Four",
                "render": function (AccountID, type, full, meta) {
                    let text = full.four;
                    let truncated = text.length > 10 ? text.substring(0, 10) + '...' : text;
                    return `<div class="detailTaskData" data-bs-toggle="tooltip"
                        title="${full.four}">${truncated}</div>`;
                }
            },
            {
                "data": "DateTime",
                "render": function (AccountID, type, full, meta) {
                    return parseToNiceDate(full.AddedDate);
                }
            },
            {
                "data": "IsActive",
                "render": function (AccountID, type, full, meta) {
                    return '<div id= Completed_{CT} class="badge rounded-pill text-bg-success">Completed</div>';
                }
            },
        ],
        //"columnDefs": [
        //    { className: "small", "targets": [0] },
        //    { targets: [0], className: "small text-center" }
        //],
        //"columns": [
        //    {
        //        mRender: function (data, type, row) {
        //            var linkQuery = '';
        //            linkQuery = ' <div class="">';
        //            linkQuery += '<div class="mb-2">';
        //            linkQuery += '<span class="h5 px-0">{Tdate}</span>&nbsp;&nbsp;'
        //                +'<span id = Completed_{CT}  class="text-success ps-2 fw-bold"><i class="btn fa fa-check fw-semibold fa-1x text-success" aria-hidden="true" ></i>Completed</span> ';
        //            linkQuery += '</div>';
        //            linkQuery += '<div class="card-body">';
        //            linkQuery += '<span class="card-text text-end h6">One</span> :<br/><span class="card-text text-end">{One}</span><br/><hr>';
        //            linkQuery += '<span class="card-text text-end h6">Two</span> :<br/><span class="card-text text-end">{two}</span><br/><hr>';
        //            linkQuery += '<span class="card-text text-end h6">Three</span> :<br/><span class="card-text text-end">{three}</span><br/><hr>';
        //            linkQuery += '<span class="card-text text-end h6">Four</span> :<br/><span class="card-text text-end">{four}</span><br/><hr>';
        //            linkQuery += '</div>';
        //            linkQuery += '<div class="card-footer text-body-secondary">';
        //            linkQuery += '</div>';
        //            linkQuery += '</div>';
        //            linkQuery = linkQuery.replace("{Tdate}", parseToNiceDate(row.AddedDate));
        //            linkQuery = linkQuery.replace("{One}", row.One);
        //            linkQuery = linkQuery.replace("{two}", row.two);
        //            linkQuery = linkQuery.replace("{three}", row.three);
        //            linkQuery = linkQuery.replace("{four}", row.four);
        //            return linkQuery
        //        }
        //    }

        //],
        "serverSide": "true",
        ///// "order": [0, "desc"],
        "processing": "true",
        "language": {
            "processing": "Loading Forms.. please wait",
            "paginate": {
                "next": ">",    // Customize "Next" button text
                "previous": "<"    // Customize "Previous" button text
            }
        }
    });
}
$('#tbMemoryMaker').on('draw.dt', function () {
    $('[data-bs-toggle="tooltip"]').tooltip(); // Initialize Bootstrap tooltips
});
$('#sectionZero-tab').trigger('click');