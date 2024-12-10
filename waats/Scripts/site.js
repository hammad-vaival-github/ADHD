$(document).ready(function () {
    //$('#CRForm').hide();
    $('.Section1Btn').hide();
    //$('.Section1').hide();
    //$('.Section2').hide();
    //$('.Section3').hide();
    $('#liSectionOneTab').trigger('click');

    $('#sectionOneZero-tab').on('click', function () {
        $('#sectionOneZero-tab-pane').css("display", "block");
        $('#sectionOne-tab-pane').css("display","none");
        $('#sectionTwo-tab-pane').css("display","none");
        $('#sectionThree-tab-pane').css("display","none");
    });
    $('#sectionOne-tab').on('click', function () {
        $('#sectionOneZero-tab-pane').css("display", "none");
        $('#sectionOne-tab-pane').css("display", "block");
        $('#sectionTwo-tab-pane').css("display", "none");
        $('#sectionThree-tab-pane').css("display", "none");

    });
    $('#sectionTwo-tab').on('click', function () {
        $('#sectionOneZero-tab-pane').css("display", "none");
        $('#sectionOne-tab-pane').css("display", "none");
        $('#sectionTwo-tab-pane').css("display", "block");
        $('#sectionThree-tab-pane').css("display", "none");
    });
    $('#sectionThree-tab').on('click', function () {
        $('#sectionOneZero-tab-pane').css("display", "none");
        $('#sectionOne-tab-pane').css("display", "none");
        $('#sectionTwo-tab-pane').css("display", "none");
        $('#sectionThree-tab-pane').css("display", "block");
    });
    $('#Section0BtnID').on('click', function () {
        $('.Section1').fadeIn(1500);
        $('#divDisableFormContainer').removeClass('disable-form');
        $('#liSectionOneTab').removeClass('read-only');
        $('#sectionOne-tab').trigger('click');
    });
 


    $('video').on('ended', function () {
        $('.Section1Btn').show();
        $(this).addClass('fa-play-circle-o');
        $(this).removeClass('fa-pause-circle-o');

    });
    $('#sectionOneZero-tab').on('click', function () {
        $('#CRForm').hide();
        $('#sectionOneZero-tab').trigger('click');
    });
    $('#sectionOne-tab').on('click', function () {
        $('#CRForm').css("display","block");
    });
    
    $('#Section1BtnID').on('click', function () {
        if ($("#CRForm").valid())
        {
            $('#sectionOne-tab').removeClass('active');
            $('#sectionTwo-tab').addClass('active');
            $('#sectionTwo-tab-pane').trigger('click');
            $('#liSectionTwoTab').removeClass('read-only');
            $('#sectionOneZero-tab-pane').css("display", "none");
            $('#sectionOne-tab-pane').css("display", "none");
            $('#sectionTwo-tab-pane').css("display", "block");
            $('#sectionThree-tab-pane').css("display", "none");
            //$('.Section2').fadeIn(1500);
            //$('#sectionTwo-tab').trigger('click');
            ////$('html, body').animate({
            ////    scrollTop: $("#Section2Div").offset().top
            ////}, 1000);
        }
    });
    $('#section2_sub5Btn').on('click', function () {
        if ($("#CRForm").valid())
        {
            $('#sectionTwo-tab').removeClass('active');
            $('#sectionThree-tab').addClass('active');
            $('#sectionThree-tab-pane').trigger('click');
            $('#liSectionThreeTab').removeClass('read-only');
            $('#sectionOneZero-tab-pane').css("display", "none");
            $('#sectionOne-tab-pane').css("display", "none");
            $('#sectionTwo-tab-pane').css("display", "none");
            $('#sectionThree-tab-pane').css("display", "block");
            //$('.Section3').fadeIn(1500);
            //$('#section3').trigger('click');
            //$('html, body').animate({
            //    scrollTop: $("#Section3Div").offset().top
            //}, 1000);
        }
    });
    //$('#section2_subIntroBtn').on('click', function () {
    //    //if (MustCheck('section2C')) {
    //    $('.section2_sub1').fadeIn(1500);
    //    $('#section2_sub1').trigger('click');
    //    collapse('section2_sub1');
    //    /// }
    //});
    //$('#section2_sub1Btn').on('click', function () {
    //    if (MustCheck('section2a')) {
    //        $('.section2_sub2').fadeIn(1500);
    //        $('#section2_sub2').trigger('click');
    //        collapse('section2_sub2');
    //    }
    //});
    //$('#section2_sub2Btn').on('click', function () {
    //    if (MustCheck('section2b')) {
    //        $('.section2_sub3').fadeIn(1500);
    //        $('#section2_sub3').trigger('click');
    //        collapse('section2_sub3');
    //    }
    //});
    //$('#section2_sub3Btn').on('click', function () {
    //    if (MustCheck('section2cc')) {
    //        $('.section2_sub4').fadeIn(1500);
    //        $('#section2_sub4').trigger('click');
    //        collapse('section2_sub4');
    //    }
    //});
    //$('#section2_sub4Btn').on('click', function () {
    //    if (MustCheck('section2d')) {
    //        $('.section2_sub5').fadeIn(1500);
    //        $('#section2_sub5').trigger('click');
    //        collapse('section2_sub5');
    //    }
    //});

    //$('#sub-Section3_IntroBtn').on('click', function () {
    //    ///if (isAnyRadioButtonChecked(groupName)) {
    //    $('.sub-Section3_1').fadeIn(1500);
    //    $('#sub-Section3_1').trigger('click');
    //    collapse('sub-Section3_1');
    //    //// }
    //});
    //$('#sub-Section3_1Btn').on('click', function () {
    //    ////alert($(this).attr('id'));
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_2').fadeIn(1500);
    //        $('#sub-Section3_2').trigger('click');
    //        collapse('sub-Section3_2');
    //    }
    //});
    //$('#sub-Section3_2Btn').on('click', function () {
    //    ////alert($(this).attr("value"));
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_3').fadeIn(1500);
    //        $('#sub-Section3_3').trigger('click');
    //        collapse('sub-Section3_3');
    //    }
    //});
    //$('#sub-Section3_3Btn').on('click', function () {
    //    /////alert($(this).attr("value"));
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_4').fadeIn(1500);
    //        $('#sub-Section3_4').trigger('click');
    //        collapse('sub-Section3_4');
    //    }
    //});
    //$('#sub-Section3_4Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_5').fadeIn(1500);
    //        $('#sub-Section3_5').trigger('click');
    //        collapse('sub-Section3_5');
    //    }
    //});
    //$('#sub-Section3_5Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_6').fadeIn(1500);
    //        $('#sub-Section3_6').trigger('click');
    //        collapse('sub-Section3_6');
    //    }
    //});
    //$('#sub-Section3_6Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_7').fadeIn(1500);
    //        $('#sub-Section3_7').trigger('click');
    //        collapse('sub-Section3_7');
    //    }
    //});
    //$('#sub-Section3_7Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_8').fadeIn(1500);
    //        $('#sub-Section3_8').trigger('click');
    //        collapse('sub-Section3_8');
    //    }
    //});
    //$('#sub-Section3_8Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_9').fadeIn(1500);
    //        $('#sub-Section3_9').trigger('click');
    //        collapse('sub-Section3_9');
    //    }
    //});
    //$('#sub-Section3_9Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_10').fadeIn(1500);
    //        $('#sub-Section3_10').trigger('click');
    //        collapse('sub-Section3_10');
    //    }
    //});
    //$('#sub-Section3_10Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_11').fadeIn(1500);
    //        $('#sub-Section3_11').trigger('click');
    //        collapse('sub-Section3_11');
    //    }
    //});
    //$('#sub-Section3_11Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_12').fadeIn(1500);
    //        $('#sub-Section3_12').trigger('click');
    //        collapse('sub-Section3_12');
    //    }
    //});
    //$('#sub-Section3_12Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_13').fadeIn(1500);
    //        $('#sub-Section3_13').trigger('click');
    //        collapse('sub-Section3_13');
    //    }
    //});
    //$('#sub-Section3_13Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_14').fadeIn(1500);
    //        $('#sub-Section3_14').trigger('click');
    //        collapse('sub-Section3_14');
    //    }
    //});
    //$('#sub-Section3_14Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_15').fadeIn(1500);
    //        $('#sub-Section3_15').trigger('click');
    //        collapse('sub-Section3_15');
    //    }
    //});
    //$('#sub-Section3_15Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_16').fadeIn(1500);
    //        $('#sub-Section3_16').trigger('click');
    //        collapse('sub-Section3_16');
    //    }
    //});
    //$('#sub-Section3_16Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_17').fadeIn(1500);
    //        $('#sub-Section3_17').trigger('click');
    //        collapse('sub-Section3_17');
    //    }
    //});
    //$('#sub-Section3_17Btn').on('click', function () {
    //    if (isAnyRadioButtonChecked($(this).attr('id'), $(this).attr("value"))) {
    //        $('.sub-Section3_18').fadeIn(1500);
    //        $('#sub-Section3_18').trigger('click');
    //        collapse('sub-Section3_18');
    //    }
    //});
    //$('#sub-Section3_18Btn').on('click', function () {
    //    if (!Q_e_d()) {
    //        if (MustCheck('section18C')) {
    //            $('.sub-Section3_19').fadeIn(1500);
    //            $('#sub-Section3_19').trigger('click');
    //            collapse('sub-Section3_19');
    //        }
    //    }
    //    else {
    //        $('.sub-Section3_19').fadeIn(1500);
    //        $('#sub-Section3_19').trigger('click');
    //        collapse('sub-Section3_19');
    //    }
    //});
    //$('#sub-Section3_19Btn').on('click', function () {
    //    if (!Q_e_d()) {
    //        if (MustCheck('section19C')) {
    //            $('.sub-Section3_20').fadeIn(1500);
    //            $('#sub-Section3_20').trigger('click');
    //            collapse('sub-Section3_20');
    //        }
    //    }
    //    else {
    //        $('.sub-Section3_20').fadeIn(1500);
    //        $('#sub-Section3_20').trigger('click');
    //        collapse('sub-Section3_20');
    //    }
    //});

    //$('#sub-Section3_20Btn').on('click', function () {
    //    if (!Q_e_d()) {
    //        if (MustCheck('section20C')) {
    //            $('#saveBtn').fadeIn(1500);
    //        }
    //    }
    //    else {
    //        $('#saveBtn').fadeIn(1500);
    //    }
    //});

    //function collapse(_className) {
    //    ///$('.collapse').on('shown.bs.collapse', function (e) {
    //    $('.' + _className).on('shown.bs.collapse', function (e) {
    //        var $card = $(this).closest('.accordion-item');
    //        $('html,body').animate({
    //            scrollTop: $card.offset().top
    //        }, 1000);
    //    });
    //}
    //function isAnyRadioButtonChecked(itemname, groupName) {
    //    if (!Q_e_d()) {
    //        if ($('input[name="' + groupName + '"]:checked').length > 0) {
    //            setUncheckedToRed(itemname, groupName);
    //            return true;
    //        }
    //        else {
    //            setUncheckedToRed(itemname, groupName);
    //            return false;
    //        }
    //    }
    //    else return true;
    //}
    // Function to set radio buttons to red if not checked
    //function setUncheckedToRed(itemname, groupName) {
    //    $('input[name="' + groupName + '"]').each(function () {
    //        if (!$(this).is(':checked')) {
    //            $('#' + itemname).closest('div.card-footer').addClass("border border-danger").removeClass("border-info");
    //        } else {
    //            $('#' + itemname).closest('div.card-footer').removeClass("border border-danger").addClass("border-info");
    //        }
    //    });
    //}
    //function MustCheck(className) {
    //    var value = true;
    //    ///alert($(this).attr('id')+'    '+this.value);
    //    $('.' + className).each(function () { //loop through checked checkboxes            
    //        if (this.value === '') {
    //            $(this).css("cssText", "border: .20px solid red !important;");
    //            value = false;
    //        } else {
    //            $(this).css("cssText", "border: .20px solid green !important;");
    //        }

    //    });
    //    return value;
    //}
    //function Q_e_d() {
    //    var value = true;
    //    let _d = $('#HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself').val();//harm yourself
    //    let _e = $('#HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse').val();//harm anyone else
    //    /////alert(_d+" "+_e);
    //    if (_d == 'Yes' || _e == 'Yes') {
    //        $('#saveBtn').fadeIn(1500);
    //        value = true;
    //    }
    //    else {
    //        $('#saveBtn').fadeOut(0);
    //        value = false;
    //    }
    //    return value;
    //}
    $('.myHTMLvideo').on('click', function () {
        let _this = $(this).attr('id');
        var vid = document.getElementById($(this).attr('id') + "1");
        ///vid.paused ? vid.play() : vid.pause();
        // vid.prop('muted') ? vid.prop('muted', false):vid.prop('muted', true)
        if (vid.paused) {
            vid.play();
            $(this).removeClass('fa-play-circle-o');
            $(this).addClass('fa-pause-circle-o');
        } else {
            vid.pause();
            $(this).addClass('fa-play-circle-o');
            $(this).removeClass('fa-pause-circle-o');
        }

    });
   
    $('#Q3-1').on('ended', function () {
        var videoFile = '/Media/eng/Q3-a.mp4';
        $('video source').attr('src', videoFile);
        $("video")[0].load();
        $("video")[0].play();
    });


});

jQuery.validator.addMethod("enforcetrue", function (value, element, param) {
    return element.checked;
});
jQuery.validator.unobtrusive.adapters.addBool("enforcetrue");
function showall() {
    $('.AvatarIntro').fadeIn(1500);
    $('.Section1').fadeIn(1500);
    $('.Section2').fadeIn(1500);
    $('.Section3').fadeIn(1500);
    $('#section3').trigger('click');
    //$('html,body').animate({
    //    scrollTop: $('.Section3').offset().top
    //}, 500);
}
//function myFunction(IdTag) {
//    var dob = document.getElementById(IdTag).value;
//    var ageInput = document.getElementById('Age'); // Assuming the input field has id 'Age'

//    if (dob !== '') {
//        var age = moment().diff(moment(dob, 'YYYY-MM-DD'), 'years');

//        if (age >= 18) {
//            ageInput.value = age;
//            ageInput.style.color = ''; // Reset the text color
//            ageInput.setCustomValidity(''); // Reset the validation message
//        } else {
//            ageInput.value = 18;
//            ageInput.style.color = 'red'; // Set text color to red
//            ageInput.setCustomValidity('Age should be greater than 18'); // Trigger validation message
//        }
//    }
//}

function myFunction(IdTag) {
    var dob = document.getElementById(IdTag).value;
    if (dob != '') {
        var age = moment().diff(moment(dob, 'YYYY-MM-DD'), 'years');
        if (age >= 18) {
            $('#Age-error').addClass('d-none');
        }
        $('#Age').val(age);
        }
}