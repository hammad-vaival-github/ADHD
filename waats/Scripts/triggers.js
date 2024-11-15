$(document).ready(function () {
    LoadTask();
    $('body').on('click', "#AddTrigger", function () {
        LoadTriggerModel();
    });
    //$('body').on('click', ".EditTask", function () {
    //    LoadEditTaskModel($(this).attr('id'));
    //});
    
    $("#AllTrigger").click(function () {
        $('#tbtriggers').DataTable().page.len(5).draw();
    });
    $("#reset").click(function () {
        ////$.fn.dataTable.ext.search.pop();
        $('#tbtriggers').DataTable().page.len(1).draw();
       ///TableTrig.draw();
    });
    $('body').on('submit', "#TriggerForm", function (e) {
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
        url: baseUrl + "UProfile/AddTriggers",
        success: function (data) {
            Modeldata(data, false, "Add new trigger", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
function LoadTask() {
    TableTrig = $('#tbtriggers').DataTable({
        "destroy": true,
        "autoWidth": false,
        "scrollX": true,
        "scrollY": false,
        "searching": false,
        "ordering": false,
        "lengthMenu": [1, 5, 10, 15, 20, 25, 50, 100],
        "pageLength": 1,
        "ajax": {
            "url": baseUrl + "UProfile/GetUsersTriggersList",
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
                        +'<span id = Completed_{CT}  class="text-success ps-2 fw-bold"><i class="fa fa-check fw-semibold fa-1x text-success" aria-hidden="true" ></i>Completed</span> ';
                    linkQuery += '</div>';
                    linkQuery += '<div class="card-body">';
                    linkQuery += '<span class="card-text text-end h6">Trigger Event</span> :<br/><span class="card-text text-end">{te}</span><br/><hr>';
                    linkQuery += '<span class="card-text text-end h6">Emotion</span> :<br/><span class="card-text text-end">{em}</span><br/><hr>';
                    linkQuery += '<span class="card-text text-end h6">Body Feeling</span> :<br/><span class="card-text text-end">{bf}</span><br/><hr>';
                    linkQuery += '<span class="card-text text-end h6">Why is this a trigger?</span> :<br/><span class="card-text text-end">{whyt}</span><br/><hr>';
                    linkQuery += '<span class="card-text text-end h6">Avoidance/Management of trigger</span> :<br/><span class="card-text text-end">{av}</span><br/><hr>';
                    linkQuery += '</div>';
                    linkQuery += '<div class="card-footer text-body-secondary">';
                    linkQuery += '</div>';
                    linkQuery += '</div>';
                    /////linkQuery = linkQuery.replace("{te}", "Tilte");
                    linkQuery = linkQuery.replace("{Tdate}", parseToNiceDate(row.AddedDate));
                    linkQuery = linkQuery.replace("{te}", row.whatwasthetriggerevent);
                    linkQuery = linkQuery.replace("{em}", row.whatwastheemotionthatyoufelt);
                    linkQuery = linkQuery.replace("{bf}", row.howdiditfeelinyourbody);
                    linkQuery = linkQuery.replace("{whyt}", row.whymightthisbeatriggerforyou_thinkaboutyourpast);
                    linkQuery = linkQuery.replace("{av}", row.howcanyouavoidthesituationand_ormanageyourresponsetoitinfuture);
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