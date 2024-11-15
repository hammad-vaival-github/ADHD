$(document).ready(function () {
    LoadTask();
    $('body').on('click', "#AddGratitudeJournal", function () {
        LoadTriggerModel();
    });
    //$('body').on('click', ".EditTask", function () {
    //    LoadEditTaskModel($(this).attr('id'));
    //});
    
    $("#AllGratitudeJournal").click(function () {
        $('#tbGratitudeJournal').DataTable().page.len(5).draw();
    });
    $("#reset").click(function () {
        ////$.fn.dataTable.ext.search.pop();
        $('#tbGratitudeJournal').DataTable().page.len(1).draw();
       ///TableTrig.draw();
    });
    $('body').on('submit', "#GratitudeJournalForm", function (e) {
        e.preventDefault(); // avoid to execute the actual submit of the form.
        var form = $(this);
        var actionUrl = form.attr('action');
        SubmitNewTask(form, actionUrl);
    });

});
function LoadTriggerModel() {
    $("#modal-body").html('');
    $("#msModal").modal("show");
    $.ajax({
        type: "Get",
        url: baseUrl + "UProfile/AddGratitudeJournal",
        success: function (data) {
            Modeldata(data, false, "Add new gratitude journal", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
function LoadTask() {
    TableTrig = $('#tbGratitudeJournal').DataTable({
        "destroy": true,
        "autoWidth": false,
        "scrollX": true,
        "scrollY": false,
        "searching": false,
        "ordering": false,
        "lengthMenu": [1, 5, 10, 15, 20, 25, 50, 100],
        "pageLength": 1,
        "ajax": {
            "url": baseUrl + "UProfile/GetUsersGratitudeJournalList",
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
                    linkQuery += '<span class="h5 px-0">{Tdate}</span>&nbsp;&nbsp;'
                        +'<span id = Completed_{CT} class="text-success ps-2 fw-bold"><i id = Completed_{CT} class="btn fa fa-check fw-semibold fa-1x text-success" aria-hidden="true" ></i>Completed</span> ';
                    linkQuery += '</div>';
                    linkQuery += '<div class="card-body">';
                    linkQuery += '<span class="card-text text-end h6">Things I am gratefull for today</span> :<br/><span class="card-text text-end">{One}</span><br/>';
                    linkQuery += '</div>';
                    linkQuery += '<div class="card-footer text-body-secondary">';
                    linkQuery += '</div>';
                    linkQuery += '</div>';
                    linkQuery = linkQuery.replace("{Tdate}", parseToNiceDate(row.AddedDate));
                    linkQuery = linkQuery.replace("{One}", row.thingsiamgratfullfortoday);
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