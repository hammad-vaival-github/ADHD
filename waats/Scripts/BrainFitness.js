$(document).ready(function () {
    LoadTask();
    $('body').on('click', ".brainExerciseDrawer", function () {
        LoadTriggerModel();
    });
    $("#AllBrainFitness").click(function () {
        $('#tbBrainFitness').DataTable().page.len(5).draw();
    });
    $("#reset").click(function () {
        $('#tbBrainFitness').DataTable().page.len(1).draw();
    });
    $('body').on('submit', "#BrainFitnessForm", function (e) {
        e.preventDefault(); 
        if ($('#_UI').val() == $('#Q_A').val()) {
            var form = $(this);
            var actionUrl = form.attr('action');
            SubmitNewTask(form, actionUrl);
        }
        else {
            $('#_UI').addClass('border border-danger');
            let c = $('#Q_NoofAttempts').val();
            if (c == 0) { $('#Q_NoofAttempts').val(1); }
            else { $('#Q_NoofAttempts').val(++c); }
            return false;
        }
    });

});
function LoadTriggerModel() {
    debugger;
    $("#offcanvas-body").html('');
    //$("#AddBrainFitness").modal("show");
    $.ajax({
        type: "Get",
        url: baseUrl + "UProfile/AddBrainFitness",
        success: function (data) {
            Modeldata(data, false, "Add new brain fitness", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
function LoadTask() {
    TableTrig = $('#tbBrainFitness').DataTable({
        "destroy": true,
        "autoWidth": false,
        "scrollX": true,
        "scrollY": false,
        "searching": false,
        "ordering": false,
        "lengthMenu": [1, 5, 10, 15, 20, 25, 50, 100],
        "pageLength": 1,
        rowReorder: true,
        "ajax": {
            "url": baseUrl + "UProfile/GetbrainFitnessList",
            "type": "POST",
            "datatype": "json"
        }, 
        "columns": [
            {
                "data": "StartingNumber",
                "render": function (AccountID, type, full, meta) {
                    return full.StartingNumber;
                }
            },
            {
                "data": "Title",
                "render": function (AccountID, type, full, meta) {
                    return "Add " + full.Q_Add + " , " + full.Q_Add + " times"
                    //return full.title;
                }
            },
            {
                "data": "StartDate",
                //"class": "text-align-center",
                "render": function (AccountID, type, full, meta) {
                    debugger;
                    return "Take " + full.Q_Take + " , " + full.Q_Take + " times";
                    
                    //return "<span class=''>" + ConvertDateTimeFormat(new Date(full.startDate)) + "</span>";
                }
            },
            {
                "data": "EndDate",
                //"class": "text-align-center",
                "render": function (AccountID, type, full, meta) {
                    return  full.Q_A;
                }
            },
            {
                "data": "IsActive",
                "render": function (AccountID, type, full, meta) {
                    return parseToNiceDate(full.AddedDate);
                    //linkQuery = linkQuery.replace("{av}", row.Q_NoofAttempts);
                }
            },
            {
                "data": "IsActive",
                "render": function (AccountID, type, full, meta) {
                    return '<div id= Completed_{CT} class="badge rounded-pill text-bg-success">Completed</div>';
                    //linkQuery = linkQuery.replace("{av}", row.Q_NoofAttempts);
                }
            },
            {
                "data": "IsActive",
                "render": function (AccountID, type, full, meta) {
                    return full.Q_NoofAttempts;
                    //linkQuery = linkQuery.replace("{av}", row.Q_NoofAttempts);
                }
            },
        ],
        //"columnDefs": [
        //    { "targets": [0] },
        //    { targets: [0], className: "small text-center" }
        //],
        //"columns": [
        //    {
        //        mRender: function (data, type, row) {
        //            var linkQuery = '';
        //            //linkQuery = ' <div class="">';
        //            //linkQuery += '<div class="mb-2">';
        //            //linkQuery += '<span class="h5 px-2">{Tdate}</span>&nbsp;&nbsp;'
        //           // linkQuery += '<span id = Completed_{CT} class="text-success ps-2 fw-bold"><i class="fa fa-check fw-semibold fa-1x text-success" aria-hidden="true" ></i>Completed</span> ';
        //            //linkQuery += '</div>';
        //            linkQuery += '<thead>'+
        //                '<tr>' +
        //                '<th>Starting Number</th>' +
        //                '<th>Add</th>'+
        //                '<th>Take</th>' +
        //                '<th>Answer</th>' +
        //                '<th>Date & Time</th>' +
        //                '<th>Status</th>' +
        //                '<th>No of attempts</th>' +
        //                '</thead >' +
        //                '<tbody>'+
        //                '<tr><td>{te}</td>' +
        //                '<td>{em}</td>' +
        //                '<td>{bf}</td>' +
        //                '<td>{whyt}</td>' +
        //                '<td>{Tdate}</td>' +
        //                '<td><div id = Completed_{CT} class="badge rounded-pill text-bg-success">Completed</div></td>' +
        //                '<td>{av}</td></thead></tbody>';
                   
        //            //linkQuery += '</div>';
        //            //linkQuery += '<div class="card-footer text-body-secondary">';
        //            //linkQuery += '</div>';
        //            //linkQuery += '</div>';

        //            /////linkQuery = linkQuery.replace("{te}", "Tilte");
                    
        //            linkQuery = linkQuery.replace("{te}", row.StartingNumber);
        //            linkQuery = linkQuery.replace("{em}", "Add " + row.Q_Add+" , "+row.Q_Add+" times");
        //            linkQuery = linkQuery.replace("{bf}", "Take " + row.Q_Take + " , " + row.Q_Take + " times");
        //            linkQuery = linkQuery.replace("{whyt}", row.Q_A);
        //            linkQuery = linkQuery.replace("{Tdate}", parseToNiceDate(row.AddedDate));
        //            linkQuery = linkQuery.replace("{av}", row.Q_NoofAttempts);
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
$('#tbBrainFitness').on('draw.dt', function () {
    $('[data-bs-toggle="tooltip"]').tooltip(); // Initialize Bootstrap tooltips
});
$('#sectionZero-tab').trigger('click');