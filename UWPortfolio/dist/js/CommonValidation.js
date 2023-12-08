/// <reference path="../../plugins/jQuery/jquery-2.2.3.min.js" />
// Not allow copy pest.
// Event Binding Function Begins.

var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_endRequest(function () {
    alert('came in');
    $('input,textarea').bind("cut copy paste drag drop", function (e) {
        e.preventDefault();
        return false;
    });
    //for stop the autocomplete textboxes. 
    $("input[type=text]").attr("autocomplete", "off");

});

// Allow only Numeric data.
if ($("html").hasClass("no-touch")) {
    $(document).on('keypress', '.Numeric', function (e) {
        var keycode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        return ((keycode == 8 || keycode == 9) || (keycode >= 48 && keycode <= 57));
    });
} else {
    $(document).on('keyup', '.Numeric', function (e) {

        this.value = this.value.replace(/[^\d\.\-]/g, '');
    });
}

// not allow backspace from read only feilds.

$(document).keydown(function (e) {
    var element = e.target.nodeName.toLowerCase();
    if ((element != 'input' && element != 'textarea') || $(e.target).attr("readonly") || (e.target.getAttribute("type") === "checkbox")) {

        if (e.keyCode === 8) {
            return false;
        }
    }
});

//alpha numeric data without space.
$(".alphanumeric").keypress(function (e) {
    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57));
});


// //alpha numeric data with space.
$(".alphanumericwithspace").keypress(function (e) {
    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57) || k == 32 || k == 44 || k == 46);

});

// Allow only alphbets.
$(".onlyalphabets").keypress(function (e) {
    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8);
});

//to validate address feilds.
$(".address").keypress(function (e) {
    //added || k == 9 to allow TAB - Rajeev Sengar 
    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 9 || k == 32 || k == 35 || k == 37 || k == 38 || k == 40 || k == 41 || k == 44 || k == 45 || k == 46 || (k >= 48 && k <= 57));

});

//not allow multiple back space.
$(".RemoveContinueMultipleSpaces").keyup(function () {
    while (this.value.indexOf("  ") != -1) {
        this.value = this.value.replace("  ", " ");
    }
    if (this.value.charAt(0) == " ") {
        this.value = this.value.trim();
    }
});


$('input,textarea').bind("cut copy paste drag drop", function (e) {
    e.preventDefault();
    return false;
});
//for stop the autocomplete textboxes. 
$("input[type=text]").attr("autocomplete", "off");

// Allow only Numeric data.
if ($("html").hasClass("no-touch")) {
    $(document).on('keypress', '.Numeric', function (e) {
        var keycode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        return ((keycode == 8 || keycode == 9) || (keycode >= 48 && keycode <= 57));
    });
} else {
    $(document).on('keyup', '.Numeric', function (e) {

        this.value = this.value.replace(/[^\d\.\-]/g, '');
    });
}



// not allow backspace from read only feilds.

$(document).keydown(function (e) {
    var element = e.target.nodeName.toLowerCase();
    if ((element != 'input' && element != 'textarea') || $(e.target).attr("readonly") || (e.target.getAttribute("type") === "checkbox")) {

        if (e.keyCode === 8) {
            return false;
        }
    }
});

//alpha numeric data without space.
$(".alphanumeric").keypress(function (e) {
    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57));
});


// //alpha numeric data with space.
$(".alphanumericwithspace").keypress(function (e) {
    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57) || k == 32 || k == 44 || k == 46);

});

// Allow only alphbets.
$(".onlyalphabets").keypress(function (e) {
    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8);
});

//to validate address feilds.
$(".address").keypress(function (e) {
    //added || k == 9 to allow TAB - Rajeev Sengar 
    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 9 || k == 32 || k == 35 || k == 37 || k == 38 || k == 40 || k == 41 || k == 44 || k == 45 || k == 46 || (k >= 48 && k <= 57));

});

//not allow multiple back space.
$(".RemoveContinueMultipleSpaces").keyup(function () {
    while (this.value.indexOf("  ") != -1) {
        this.value = this.value.replace("  ", " ");
    }
    if (this.value.charAt(0) == " ") {
        this.value = this.value.trim();
    }
});

//Pan Number validation

