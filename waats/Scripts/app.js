$(document).ready(function (e) {
//    $('body').on('submit', "#staskForm", function (e) {
//        e.preventDefault(); // avoid to execute the actual submit of the form.
//        var form = $(this);
//        var actionUrl = form.attr('action');
//        SubmitNewTask(form, actionUrl);
//    });
});

function parseToNiceDate(jsonDateString) {
        return moment(jsonDateString).format('dddd DD MMMM YYYY HH:mm');
}
function Modeldata(data, FootFlag, Modellabel, savebHtml) {
    $("#ModalLabel").html(Modellabel);
    $("#modal-body").html(data);
    $("#msModal").modal("show");
    if (FootFlag == true) {
        $("#Modelfooter").show();
        $('#saveb').html(savebHtml);
    }
    else {
        $("#Modelfooter").hide();
        $('#saveb').html('');
    }
}

function SubmitNewTask(form, actionUrl) {
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(), // serializes the form's elements.
        success: function (data) {
            toastr.success(data, 'Success');
            $("#msModal").modal("toggle");
            $('.modal-backdrop').remove(); // removes the overlay
            setTimeout(() => { document.location.reload() }, 2000);

        }
    });
}