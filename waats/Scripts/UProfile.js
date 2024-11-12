$(document).ready(function () {
    var TableT;
    LoadTask();
    $("#myTab li:eq(0) a").tab("show"); // show second tab (0-indexed, like an array)

    $('body').on('click', "#AboutCom", function () {
        /// LoadSchedulingTaskModel() 
    });
    $('body').on('click', ".AddTask", function () {//#AddTask 
        LoadSchedulingTaskModel($(this).val());
    });
    $('body').on('click', ".EditTask", function () {
        LoadEditTaskModel($(this).attr('id'));
    });
    $('body').on('click', ".EditSubTask", function () {
        LoadEditSubTaskModel($(this).attr('id'));
    });


    $('body').on('click', ".MarkAsDone", function () {
        MarkAsDoneConfirmModel($(this).attr('id'),"maintask");
    });
    $('body').on('click', ".MarkAsDoneSubTask", function () {
        MarkAsDoneConfirmModel($(this).attr('id'), "subtask");
    });
    $('body').on('click', ".SaveCompletedTask", function () {
        MarkAsDoneConfirmed($(this).attr('id'),"maintask");
    });
    $('body').on('click', ".SaveCompletedSubTask", function () {
        MarkAsDoneConfirmed($(this).attr('id'), "subtask");
    });
        $('body').on('submit', "#staskForm", function (e) {
        e.preventDefault(); // avoid to execute the actual submit of the form.
        var form = $(this);
        var actionUrl = form.attr('action');
        SubmitNewTask(form, actionUrl);
    });
    function LoadTask() {
        debugger;
        TableT = $('#tbSTasks').DataTable({
            "destroy": true,
            "autoWidth": false,
            "scrollX": true,
            "scrollY": false,
            "searching": false,
            "ordering": false,
            "lengthMenu": [5, 10, 15, 20, 25, 50, 100],
            "pageLength": 5,
            rowReorder: {
                selector: 'td:nth-child(1)',
                update: false
            },
            order: [[0, "asc"]],
            columnDefs: [
                {
                    targets: 0,
                    visible: true
                }
            ],
            "ajax": {
                "url": baseUrl + "UProfile/GetUsersTaskList",
                "type": "POST",
                "datatype": "json"
            },
            //"columnDefs": [
            //    { className: "small", "targets": [0] },
            //    { targets: [0], className: "small text-center" }
            //],
            "columns": [
                {
                    title: null,
                    data: null,
                    render: (data, type, row, meta) => meta.row+1
                },
                ///{ "data": "LocalID", "name": "LocalID" },
                ////{ "data": "Title", "name": "Title" },
                {
                    mRender: function (data, type, row) {
                        var linkQuery = '';
                        linkQuery = ' <div class="card card border-dark">';
                        linkQuery += '<div class="card-header">';
                        linkQuery += '<span class="card-title h5">{title}</span>&nbsp;&nbsp;<small>({Tdate})</small>';
                        linkQuery += '</div>';
                        linkQuery += '<div class="card-body">';
                        linkQuery += '<span class="card-text text-end">{description}</span>';
                        linkQuery += '</div>';
                        linkQuery += '<div class="card-footer text-body-secondary">';
                        linkQuery += row.MarkAsCompleted == false ?
                            '&nbsp;&nbsp;<i id=Progress_{PT} class="btn fa fa-hourglass-start fw-semibold fa-1x text-danger" aria-hidden="true">&nbsp;In progress</i>'
                            : '&nbsp;&nbsp;<i id=Completed_{CT} class="btn fa fa-check fw-semibold fa-1x text-success" aria-hidden="true">&nbsp;Completed</i>'
                        linkQuery += row.MarkAsCompleted == false ?
                            '<i id=Edit_{ET} class="EditTask btn fa fa-pencil-square-o fw-semibold fa-1x text-secondary-emphasis" aria-hidden="true">&nbsp;Edit</i>' +
                            '&nbsp;&nbsp;<i id=Mark_{MT} class="MarkAsDone btn fa fa-calendar-check-o fw-semibold fa-1x text-secondary-emphasis" aria-hidden="true">&nbsp;Mark as done</i>'
                            : '&nbsp;'
                            ;
                        linkQuery += '&nbsp;&nbsp;<i  id=Break_{BT} class="details-control btn fa fa-level-down fw-semibold fa-1x text-secondary-emphasis" aria-hidden="true">&nbsp;Break it down</i>';
                        linkQuery += '</div>';
                        linkQuery += '</div>';
                        linkQuery = linkQuery.replace("{title}", row.Title);
                        linkQuery = linkQuery.replace("{Tdate}", parseToNiceDate(row.AddedDate));
                        linkQuery = linkQuery.replace("{description}", row.Description);
                        linkQuery = linkQuery.replace("{PT}", row.LocalID);
                        linkQuery = linkQuery.replace("{CT}", row.LocalID);
                        linkQuery = linkQuery.replace("{ET}", row.LocalID);
                        linkQuery = linkQuery.replace("{MT}", row.LocalID);
                        linkQuery = linkQuery.replace("{BT}", row.LocalID);
                        return linkQuery
                    }
                }

            ],
            "serverSide": "true",
            ///// "order": [0, "desc"],
            "processing": "true",
            "language": {
                "processing": "Loading Forms.. please wait"
            }
        });
    }
    $('body').on('click', '#tbSTasks tbody i.details-control', function () {
        let tr = $(this).closest('tr');
        let row = TableT.row(tr);
        if (row.child.isShown()) {
            ///$(this).empty();
            $(this).removeClass('fa-level-up');
            $(this).removeClass('text-danger');
            $(this).addClass('fa-level-down');
            $(this).addClass('text-secondary-emphasis');
            
            row.child.hide();
            tr.removeClass('shown');

        } else {
            ///$(this).empty();
            $(this).removeClass('fa-level-down');
            $(this).removeClass('text-secondary-emphasis');
            $(this).addClass('fa-level-up');
            $(this).addClass('text-danger');
            let data = formatDetails(row.data());
            row.child(data).show();
            tr.addClass('shown');
        }
        /////TableT.row(':eq(0)').child('child row content!').show();
    });
    $('#tbSTasks').on('row-reorder.dt', function (e, details, edit) {
        for (var i = 0; i < details.length; i++) {
            console.log(
                'Node', details[i].node,
                'moved from', details[i].oldPosition,
                'to', details[i].newPosition
            );
            var tr = details[i].node.tr;
            let row = TableT.row(tr);
            var i = row.child.hide(); ////find('i.details-control');
            ///alert(i.text());

        }
    });
    $('#tbSTasks').on('mousedown.rowReorder touchstart.rowReorder', 'tbody td:nth-child(1)', function (e, details, edit) {
        
        var tr = $(this).closest('tr');
        var nxtd = $(this).closest('td').next('td');
        var i = tr.find('i.details-control');///.text();
        let row = TableT.row(tr);
       /// console.log('Started dragging row', tr);

        ////////$(document).on('mousemove.rowReorder touchmove.rowReorder', function () {
        ////////    console.log('Dragging row', tr);
        ////////});
        if (row.child.isShown()) {
            ///alert(i.text());
            row.child.hide();
            tr.removeClass('shown');
            $(i).removeClass('fa-level-up');
            $(i).removeClass('text-danger');
            $(i).addClass('fa-level-down');
            $(i).addClass('text-primary-emphasis');
        }
        else {
            ////alert(i.text());
            $(i).removeClass('fa-level-down');
            $(i).removeClass('text-secondary-emphasis');
            $(i).addClass('fa-level-up');
            $(i).addClass('text-danger');
        }
        //row.child.hide();
        //tr.removeClass('shown');
    });
    function formatDetails(d) {
        let url = 'UProfile/LoadSubTasks';
        let mainID = d.LocalID;
        let appendData = '';
        $.ajax({
            async: false,
            cache: false,
            url: baseUrl + url,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: { LocalID: d.LocalID, UserId: d.UserId},
            success: function (result) {
                let dat = '';
                let tableData = '', tableDataE='';
                    tableData += '<div class="card">';
                    tableData += '<div class="spacer10"></div>';
                    tableData += '<div class="card-body">';
                    tableData += '<div class="table-responsive">';
                tableData += '<table id="Subtable" class="table table-striped table-hover table-responsive">';
                    tableData += '<thead>';
                    tableData += '<tr>';
                    tableData += '<th scope="col" class="small" style="width: 200px;">Sub task</th>';
                    tableData += '<th scope="col" class="small">Description</th>';
                    tableData += '<th scope="col" class="small">Action</th>';
                    tableData += '</tr>';
                    tableData += '</thead>';
                    tableData += '<tbody class="pb-3">';

                    tableDataE += '</tbody>';
                    tableDataE += '</table>';
                    tableDataE += '</div>';
                    tableDataE += '</div>';
                    tableDataE += '</div>';
                        for (var item of result.data) {

                            dat += '<tr><td>' + item.Title + '</td>'
                                + '<td>' + item.Description + '</td>'
                                + '<td>'
                                + ((item.MarkAsCompleted=='' && item.MarkAsCompleted == false) ?
                                 '<i id="Edit_' + item.SubLocalID + '" class="EditSubTask btn fa fa-pencil-square-o text-primary" aria-hidden="true">&nbsp; Edit</i> '
                                + '&nbsp;&nbsp;<i id="Mark_' + item.SubLocalID + '" class="MarkAsDoneSubTask btn fa fa-calendar-check-o text-primary" aria-hidden="true">&nbsp;Mark as done</i>'
                                : '&nbsp;&nbsp;<i id=Completed_{CT} class="btn fa fa-check fw-semibold fa-1x text-success" aria-hidden="true">&nbsp;Completed</i>')
                                +'</td ></tr > ';
                        }
                appendData += '<div class="ThisDiv"> &nbsp; &nbsp; &nbsp;' +
                    '<button class="btn AddTask fa fa-plus-circle o text-primary" aria-hidden="true" id="AddSubTask_' + mainID + '"  value=' + mainID +'>&nbsp; Add sub task</button > ' +
                    tableData + dat + tableDataE + '</div>';
            },
            error: function (xhr, textStatus, err) {
                alert("readyState: " + xhr.readyState);
                alert("responseText: " + xhr.responseText);
                alert("status: " + xhr.status);
                alert("text status: " + textStatus);
                alert("error: " + err);
            }
        });
        return appendData;
    }
});

function LoadEditTaskModel(id) {
    $("#modal-body").html('');
    $("#msModal").modal("show");
    let _id = id.split('_')
    $.ajax({
        type: "Get",
        url: baseUrl + "UProfile/EditTask",
        data: { localID: _id[1] },
        success: function (data) {
            Modeldata(data, false, "Edit task", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
function LoadEditSubTaskModel(id) {
    $("#modal-body").html('');
    $("#msModal").modal("show");
    let _id = id.split('_')
    $.ajax({
        type: "Get",
        url: baseUrl + "UProfile/EditSubTask",
        data: { localID: _id[1] },
        success: function (data) {
            Modeldata(data, false, "Edit sub task", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
function MarkAsDoneConfirmModel(id,taskType) {
    $("#modal-body").html('');
    $("#msModal").modal("show");
    let _id = id.split('_');
    let act = taskType == "maintask" ? "UProfile/MarkAsDoneConfirm" : "UProfile/SubtaskMarkAsDoneConfirm";
    $.ajax({
        type: "Post",
        url: baseUrl + act ,
        data: { localID: _id[1]},
        success: function (data) {
            Modeldata(data, false, "Confirm", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
function MarkAsDoneConfirmed(id,taskType) {
    $("#modal-body").html('');
    $("#msModal").modal("show");
    let act = taskType == "maintask" ? "UProfile/MarkAsDoneConfirmed" : "UProfile/SubtaskMarkAsDoneConfirmed";
    $.ajax({
        type: "Post",
        url: baseUrl + act,
        data: { localID: id },
        success: function (data) {
            toastr.success(data, 'Success');
            setTimeout(() => { document.location.reload() }, 2000);
        },
        error: function () {
            toastr.error(data, 'Error');
        }
    });
}
function LoadSchedulingTaskModel(MainID) {
    let link = MainID == 0 ? "ScheduleNewTask" : "SubScheduleNewTask";
    $("#modal-body").html('');
    $("#msModal").modal("show");
    $.ajax({
        type: "Get",
        data: {MainID: MainID },
        url: baseUrl + "UProfile/" + link ,
        success: function (data) {
            Modeldata(data, false, MainID==0? "Add new task":"Add new sub task", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}

