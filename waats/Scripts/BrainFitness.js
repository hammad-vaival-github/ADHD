﻿$(document).ready(function () {
    LoadTask();
    $('body').on('click', "#AddBrainFitness", function () {
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
    $("#modal-body").html('');
    $("#msModal").modal("show");
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
        }, "columnDefs": [
            { className: "small", "targets": [0] },
            { targets: [0], className: "small text-center" }
        ],
        "columns": [
            {
                mRender: function (data, type, row) {
                    var linkQuery = '';
                    linkQuery = ' <div class="">';
                    linkQuery += '<div class="mb-2">';
                    linkQuery += '<span class="h5 px-2">{Tdate}</span>&nbsp;&nbsp;'
                        +'<span id = Completed_{CT} class="text-success ps-2 fw-bold"><i class="fa fa-check fw-semibold fa-1x text-success" aria-hidden="true" ></i>Completed</span> ';
                    linkQuery += '</div>';
                    linkQuery += '<div class=""><table class="table"><thead>'+
                        '<tr role="row" ><th class="smallcard-text h6">Starting Number</th>' +
                        '<th class="smallcard-text h6">Add</th>'+
                        '<th class="smallcard-text h6">Take</th>' +
                        '<th class="smallcard-text h6">Answer</th>' +
                        '<th class="smallcard-text h6">No of attempts</th></thead><tbody>' +
                        '<tr role="row" ><td class="card-text">{te}</td>' +
                        '<td class="smallcard-text ">{em}</td>' +
                        '<td class="smallcard-text ">{bf}</td>' +
                        '<td class="smallcard-text ">{whyt}</td>' +
                        '<td class="smallcard-text ">{av}</td></thead></tbody></table>'
                        ;
                    //linkQuery += '<span class="card-text text-end h6">StartingNumber</span> :<br/><span class="card-text text-end">{te}</span>';
                    //linkQuery += '<span class="card-text text-end h6">Add</span> :<br/><span class="card-text text-end">{em}</span>';
                    //linkQuery += '<span class="card-text text-end h6">Take</span> :<br/><span class="card-text text-end">{bf}</span>';
                    //linkQuery += '<span class="card-text text-end h6">Answer</span> :<br/><span class="card-text text-end">{whyt}</span>';
                    //linkQuery += '<span class="card-text text-end h6">No of attempts</span> :<br/><span class="card-text text-end">{av}</span>';
                    linkQuery += '</div>';
                    linkQuery += '<div class="card-footer text-body-secondary">';
                    linkQuery += '</div>';
                    linkQuery += '</div>';
                    /////linkQuery = linkQuery.replace("{te}", "Tilte");
                    linkQuery = linkQuery.replace("{Tdate}", parseToNiceDate(row.AddedDate));
                    linkQuery = linkQuery.replace("{te}", row.StartingNumber);
                    linkQuery = linkQuery.replace("{em}", "Add " + row.Q_Add+" , "+row.Q_Add+" times");
                    linkQuery = linkQuery.replace("{bf}", "Take " + row.Q_Take + " , " + row.Q_Take + " times");
                    linkQuery = linkQuery.replace("{whyt}", row.Q_A);
                    linkQuery = linkQuery.replace("{av}", row.Q_NoofAttempts);
                    return linkQuery
                }
            }

        ],        
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