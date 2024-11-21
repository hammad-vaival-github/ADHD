$(document).ready(function (e) {
//    $('body').on('submit', "#staskForm", function (e) {
//        e.preventDefault(); // avoid to execute the actual submit of the form.
//        var form = $(this);
//        var actionUrl = form.attr('action');
//        SubmitNewTask(form, actionUrl);
//    });
});

//function parseToNiceDate(jsonDateString) {
//        return moment(jsonDateString).format('dddd DD MMMM YYYY HH:mm');
//}
function parseToNiceDate(jsonDateString) {
    return moment(jsonDateString).format('DD/MM/YYYY HH:mm');
}
function Modeldata(data, FootFlag, Modellabel, savebHtml) {
    debugger;
    $("#ModalLabel").html(Modellabel);
    $("#offcanvas-body").html(data);
    //$("#AddBrainFitness").modal("show");
    //$("#msModal").css("display","block");
    if (FootFlag == true) {
        //$("#AddBrainFitness").show();
        console.log(savebHtml);
        $('#saveb').html(savebHtml);
    }
    else {
        //$("#AddBrainFitness").hide();
        $('#saveb').html('');
    }
    //$('.offcanvas-backdrop.fade.show').slice(0, 2).remove();
}
function ModelPopUp(data, FootFlag, Modellabel, savebHtml) {
    debugger;
    $("#modal-popup").html(data);
    $("#exampleModal").modal("show");
    //$("#modal-popup").modal("show");
    //$("#msModal").css("display","block");
    if (FootFlag == true) {
        //$("#AddBrainFitness").show();
        console.log(savebHtml);
        $('#saveb').html(savebHtml);
    }
    else {
        //$("#AddBrainFitness").hide();
        $('#saveb').html('');
    }
    
}

function SubmitNewTask(form, actionUrl) {
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: form.serialize(), // serializes the form's elements.
        success: function (data) {
            //toastr.options = {
            //    "closeButton": true,           // Show close button to allow manual dismissal
            //    "timeOut": 0,                  // Keeps the toast on screen indefinitely
            //    "extendedTimeOut": 0,          // Prevents the toast from disappearing on mouse hover
            //    "progressBar": true            // Adds a progress bar for visual appeal
            //};
            toastr.success(data, 'Success');
            $("#msModal").modal("toggle");
            $('.modal-backdrop').remove(); // removes the overlay
            setTimeout(() => { document.location.reload() }, 2000);//2000

        }
    });
}
