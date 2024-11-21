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
        MarkAsDoneConfirmModel($(this).attr('id'), "maintask");
    });
    $('body').on('click', ".MarkAsDoneSubTask", function () {
        MarkAsDoneConfirmModel($(this).attr('id'), "subtask");
    });
    $('body').on('click', ".SaveCompletedTask", function () {
        MarkAsDoneConfirmed($(this).attr('id'), "maintask");
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
            //columnDefs: [
            //    {
            //        targets: 0,
            //        visible: true
            //    }
            //],
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
                    "data": "Title",
                    "render": function (AccountID, type, full, meta) {
                        let text = full.Title;
                        let truncated = text.length > 10 ? text.substring(0, 18) + '...' : text;
                        return `<div class="detailTaskData" data-bs-toggle="tooltip"
                        title="${full.Title}">${truncated}</div>`;
                    }
                },
                {
                    "data": "Description",
                    "render": function (AccountID, type, full, meta) {
                        let text = full.Description;
                        let truncated = text.length > 10 ? text.substring(0, 70) + '...' : text;
                        return `<div class="detailTaskData" data-bs-toggle="tooltip"
                        title="${full.Description}">${truncated}</div>`;
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
                        return full.MarkAsCompleted == false ?
                            '<div id=Progress_"' + full.LocalID + '" class="badge rounded-pill text-bg-primary">In Progress</div>' :
                            '<div id=Completed_"' + full.LocalID + '" class="badge rounded-pill text-bg-success">Completed</div>';
                    }
                },
                {
                    "data": "Action",
                    "render": function (AccountID, type, full, meta) {
                        var strHTML = "";

                        strHTML += '<div class="dropdown">';
                        strHTML += '<button class="btn btn-none dropdown-toggle" type="button" id="dropdownMenuButton' + full.LocalID + '" data-bs-toggle="dropdown" aria-expanded="false">';
                        strHTML += '<i class="fas fa-ellipsis-h"></i></button>';
                        strHTML += '<ul class="dropdown-menu" aria-labelledby="dropdownMenuButton' + full.LocalID + '">';
                        strHTML += '<li><a id="Break_' + full.LocalID + '" class="details-control dropdown-item" href="#"><i class="fa-solid fa-turn-down"></i> Break it Down</a></li>';
                        if (full.MarkAsCompleted == false) {
                            strHTML += '<li><a id="Edit_' + full.LocalID + '" class="EditTask dropdown-item" href="#"><i class="fas fa-edit"></i> Edit</a></li>';
                            strHTML += '<li><a id="Mark_' + full.LocalID + '" class="MarkAsDone dropdown-item" href="#"><i class="fa-solid fa-file-circle-check me-2"></i>Mark as done</a></li>';
                        }
                        strHTML += '</ul></div>';
                        return strHTML;
                    }
                }
            ],
            //"columns": [
            //    {
            //        title: null,
            //        data: null,
            //        render: (data, type, row, meta) => meta.row+1
            //    },
            //    ///{ "data": "LocalID", "name": "LocalID" },
            //    ////{ "data": "Title", "name": "Title" },
            //    {
            //        mRender: function (data, type, row) {
            //            var linkQuery = '';
            //            linkQuery = ' <div class="scpagewrap">';
            //            linkQuery += '<div class="card-header">';
            //            linkQuery += '<span class="card-title h5">{title}</span>&nbsp;&nbsp;<small>({Tdate})</small>';
            //            linkQuery += '</div>';
            //            linkQuery += '<div class="card-body py-2">';
            //            linkQuery += '<span class="card-text text-end">{description}</span>';
            //            linkQuery += '</div>';
            //            linkQuery += '<div class="card-footer text-body-secondary">';
            //            linkQuery += row.MarkAsCompleted == false ?
            //                '<span><i id=Progress_{PT} class="fa fa-hourglass-start fw-semibold fa-1x text-danger" aria-hidden="true"></i>In progress</span>'
            //                : '<span><i id=Completed_{CT} class="fa fa-check fw-semibold fa-1x text-success" aria-hidden="true"></i>Completed</span>'
            //            linkQuery += row.MarkAsCompleted == false ?
            //                '<span><i id=Edit_{ET} class="EditTask fa fa-pencil-square-o fw-semibold fa-1x text-secondary-emphasis" aria-hidden="true"></i>Edit</span>' +
            //                '<span><i id=Mark_{MT} class="MarkAsDone fa fa-calendar-check-o fw-semibold fa-1x text-secondary-emphasis" aria-hidden="true"></i>Mark as done</span>'
            //                : ''
            //                ;
            //            linkQuery += '<span><i  id=Break_{BT} class="details-control fa fa-level-down fw-semibold fa-1x text-secondary-emphasis" aria-hidden="true"></i>Break it down</span>';
            //            linkQuery += '</div>';
            //            linkQuery += '</div>';
            //            linkQuery = linkQuery.replace("{title}", row.Title);
            //            linkQuery = linkQuery.replace("{Tdate}", parseToNiceDate(row.AddedDate));
            //            linkQuery = linkQuery.replace("{description}", row.Description);
            //            linkQuery = linkQuery.replace("{PT}", row.LocalID);
            //            linkQuery = linkQuery.replace("{CT}", row.LocalID);
            //            linkQuery = linkQuery.replace("{ET}", row.LocalID);
            //            linkQuery = linkQuery.replace("{MT}", row.LocalID);
            //            linkQuery = linkQuery.replace("{BT}", row.LocalID);
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
    $('#tbSTasks').on('draw.dt', function () {
        $('[data-bs-toggle="tooltip"]').tooltip(); // Initialize Bootstrap tooltips
    });
    //$('body').on('click', '#tbSTasks tbody i.details-control', function () {
    //    let tr = $(this).closest('tr');
    //    let row = TableT.row(tr);
    //    if (row.child.isShown()) {
    //        ///$(this).empty();
    //        $(this).removeClass('fa-level-up');
    //        $(this).removeClass('text-danger');
    //        $(this).addClass('fa-level-down');
    //        $(this).addClass('text-secondary-emphasis');

    //        row.child.hide();
    //        tr.removeClass('shown');

    //    } else {
    //        ///$(this).empty();
    //        $(this).removeClass('fa-level-down');
    //        $(this).removeClass('text-secondary-emphasis');
    //        $(this).addClass('fa-level-up');
    //        $(this).addClass('text-danger');
    //        let data = formatDetails(row.data());
    //        row.child(data).show();
    //        tr.addClass('shown');
    //    }
    //    /////TableT.row(':eq(0)').child('child row content!').show();
    //});

    $('body').on('click', '#tbSTasks tbody li a.details-control', function () {
        let tr = $(this).closest('tr');
        let row = TableT.row(tr);

        if (!row.data()) return;

        // Fetch and display details
        let detailsHtml = formatDetails(row.data());

        // Hide the DataTable container
        $('#dataTableContainer').hide();

        // Show the details container with the fetched details
        $('#detailsContent').html(detailsHtml);
        $('#detailsContainer').show();
    });
    // Back to Table button functionality
    $(document).on('click', '#backToTable', function () {
        // Show the DataTable container
        $('#dataTableContainer').show();

        // Hide the details container
        $('#detailsContainer').hide();
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
    //function formatDetails(d) {
    //    let url = 'UProfile/LoadSubTasks';
    //    let mainID = d.LocalID;
    //    let appendData = '';
    //    $.ajax({
    //        async: false,
    //        cache: false,
    //        url: baseUrl + url,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: 'json',
    //        data: { LocalID: d.LocalID, UserId: d.UserId},
    //        success: function (result) {
    //            let dat = '';
    //            let tableData = '', tableDataE='';
    //                tableData += '<div class="">';
    //                tableData += '<div class="spacer10"></div>';
    //                tableData += '<div class="card-body">';
    //                tableData += '<div class="table-responsive">';
    //                tableData += '<table id="Subtable" class="table table-striped table-hover table-responsive">';
    //                tableData += '<thead>';
    //                tableData += '<tr>';
    //                tableData += '<th scope="col" class="small" style="width: 200px;">Sub task</th>';
    //                tableData += '<th scope="col" class="small">Description</th>';
    //                tableData += '<th scope="col" class="small">Action</th>';
    //                tableData += '</tr>';
    //                tableData += '</thead>';
    //                tableData += '<tbody class="pb-3">';

    //                tableDataE += '</tbody>';
    //                tableDataE += '</table>';
    //                tableDataE += '</div>';
    //                tableDataE += '</div>';
    //                tableDataE += '</div>';
    //                    for (var item of result.data) {

    //                        dat += '<tr><td>' + item.Title + '</td>'
    //                            + '<td>' + item.Description + '</td>'
    //                            + '<td>'
    //                            + ((item.MarkAsCompleted=='' && item.MarkAsCompleted == false) ?
    //                            '<span><i id="Edit_' + item.SubLocalID + '" class="EditSubTask btn fa fa-pencil-square-o text-secondary-emphasis" aria-hidden="true"></i>Edit</span> '
    //                            + '<span><i id="Mark_' + item.SubLocalID + '" class="MarkAsDoneSubTask btn fa fa-calendar-check-o text-secondary-emphasis" aria-hidden="true"></i>Mark as done</span>'
    //                            : '<span><i id=Completed_{CT} class="btn fa fa-check fw-semibold fa-1x text-success" aria-hidden="true"></i>Completed</span>')
    //                            +'</td ></tr > ';
    //                    }
    //            appendData += '<div class="ThisDiv">' +
    //                '<button class="btn AddTask o text-primary btn-primary rounded-pill text-white btn-x10-y30 my-3" aria-hidden="true" id="AddSubTask_' + mainID + '"  value=' + mainID +'><i class="fa fa-plus-circle"></i> Add sub task</button > ' +
    //                tableData + dat + tableDataE + '</div>';
    //        },
    //        error: function (xhr, textStatus, err) {
    //            alert("readyState: " + xhr.readyState);
    //            alert("responseText: " + xhr.responseText);
    //            alert("status: " + xhr.status);
    //            alert("text status: " + textStatus);
    //            alert("error: " + err);
    //        }
    //    });
    //    return appendData;
    //}
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
            data: { LocalID: d.LocalID, UserId: d.UserId },
            success: function (result) {
                let dat = '';
                let tableData = '', tableDataE = '';

                // Start of Main Content
                tableData += `
                            <div class="mt-4">
                                <div class="tab-content" id="myTabContent">
                                    <div class="tab-pane fade show active" id="sectionZero-tab-pane" role="tabpanel" aria-labelledby="sectionZero-tab" tabindex="0">
                                        <div class="mainDataTablePage">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <h4 id="backToTable" class="mainTitleUnderDatatable" style="cursor:pointer">
                                                        <i class="fa-solid fa-arrow-left-long text-dark me-2"></i> Scheduling Tasks
                                                    </h4>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="d-flex justify-content-end">
                                                        <div class="ThisDiv">
                                                            <button class="btn AddTask text-primary btn-primary rounded-pill text-white btn-x10-y30 my-3" aria-hidden="true" id="AddSubTask_${mainID}" value="${mainID}">
                                                                <i class="fa-solid fa-circle-plus me-2"></i> Add subtask
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <table id="Subtable" class="display dataTable no-footer" style="width:100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>Subtask</th>
                                                                    <th>Description</th>
                                                                    <th>Actions</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                            `;

                                            // Append dynamic data
                                            result.data.forEach((item, index) => {
                                                const rowClass = index % 2 === 0 ? 'even' : 'odd'; // Determine the class based on index

                                                dat += `
                                                        <tr class="${rowClass}">
                                                            <td>${item.Title}</td>
                                                            <td>${item.Description}</td>
                                                            <td>
                                                                ${item.MarkAsCompleted === '' || item.MarkAsCompleted === false
                                                                             ? `
                                                                            <div class="d-flex justify-content-center">
                                                                            <div style="cursor:pointer" id="Edit_${item.SubLocalID}" class="EditSubTask btn btn-outline-primary me-3 border-0" data-bs-toggle="offcanvas" data-bs-target="#brainExerciseDrawer">
                                                                                <i id="Edit_${item.SubLocalID}" class="fas fa-edit me-2"></i> Edit
                                                                            </div>
                                                                            <div style="cursor:pointer" id="Mark_${item.SubLocalID}" class="MarkAsDoneSubTask btn btn-outline-primary me-3 border-0">
                                                                                Mark as done
                                                                            </div></div>`
                                                                            : `<div class="d-flex"><div id="Completed_${item.SubLocalID}" class="badge rounded-pill text-bg-success"> Completed
                                                                              </div></div>`
                                                                        }
                                                            </td>
                                                        </tr>
                                                    `;
                                            });


                                            // End of Table and Main Content
                                            tableDataE += `
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            `;

                                            // Combine all parts
                                            appendData += `${tableData + dat + tableDataE}`;

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

    //function formatDetails(d) {
    //    let url = 'UProfile/LoadSubTasks';
    //    let mainID = d.LocalID;
    //    let detailsHtml = '';

    //    $.ajax({
    //        async: false,
    //        cache: false,
    //        url: baseUrl + url,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: 'json',
    //        data: { LocalID: d.LocalID, UserId: d.UserId },
    //        success: function (result) {
    //            detailsHtml += `<div>
    //            <h3>Task Details</h3>
    //            <p><strong>Title:</strong> ${d.Title}</p>
    //            <p><strong>Description:</strong> ${d.Description}</p>
    //            <h4>Subtasks</h4>
    //            <table class="table table-bordered">
    //                <thead>
    //                    <tr>
    //                        <th>Subtask</th>
    //                        <th>Description</th>
    //                        <th>Actions</th>
    //                    </tr>
    //                </thead>
    //                <tbody>`;

    //            result.data.forEach(item => {
    //                detailsHtml += `<tr>
    //                <td>${item.Title}</td>
    //                <td>${item.Description}</td>
    //                <td>
    //                    ${(item.MarkAsCompleted ?
    //                        `<span><i class="fa fa-check text-success"></i> Completed</span>` :
    //                        `<button class="btn btn-sm btn-primary mark-as-done" data-id="${item.SubLocalID}">Mark as Done</button>`)}
    //                </td>
    //            </tr>`;
    //            });

    //            detailsHtml += '</tbody></table>';
    //            detailsHtml += '<div class="ThisDiv">';
    //            detailsHtml += '<button class="btn AddTask o text-primary btn-primary rounded-pill text-white btn-x10-y30 my-3" aria-hidden="true" id="AddSubTask_' + mainID + '"  value=' + mainID + '><i class="fa fa-plus-circle"></i> Add sub task</button > ';
    //            detailsHtml += '</div>';

    //        //    <button class="btn AddTask o text-primary btn-primary rounded-pill text-white btn-x10-y30 my-3" aria-hidden="true" id="AddSubTask_${mainID}  value=${mainID}><i class=" fa fa-plus-circle"></i> Add sub task</button >   
    //        //</div>`;
    //        },
    //        error: function (xhr, textStatus, err) {
    //            alert("Error fetching details.");
    //        }
    //    });

    //    return detailsHtml;
    //}

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
    //$("#modal-body").html('');
    //$("#msModal").modal("show");
    debugger;
    $("#offcanvas-body").html('');
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
function MarkAsDoneConfirmModel(id, taskType) {
    //$("#modal-body").html('');
    //$("#msModal").modal("show");
    $("#offcanvas-body").html('');
    let _id = id.split('_');
    let act = taskType == "maintask" ? "UProfile/MarkAsDoneConfirm" : "UProfile/SubtaskMarkAsDoneConfirm";
    $.ajax({
        type: "Post",
        url: baseUrl + act,
        data: { localID: _id[1] },
        success: function (data) {
            ModelPopUp(data, false, "Confirm", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
function MarkAsDoneConfirmed(id, taskType) {
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
    $("#offcanvas-body").html('');
    //$("#modal-body").html('');
    //$("#msModal").modal("show");
    $.ajax({
        type: "Get",
        data: { MainID: MainID },
        url: baseUrl + "UProfile/" + link,
        success: function (data) {
            Modeldata(data, false, MainID == 0 ? "Add new task" : "Add new sub task", 'Not required');
        },
        error: function () {
            alert('error!');
        }
    });
}
$('#sectionZero-tab').trigger('click');
