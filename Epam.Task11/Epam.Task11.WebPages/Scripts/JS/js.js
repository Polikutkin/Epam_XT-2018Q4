"use strict";

var success_message = "Operation successful";
var error_message = "Cannot to make operation";

function openPage(partialPage, container) {
    var $cont = $(container);
    $.ajax({
        method: "POST",
        url: partialPage,
        success: function (response) {
            $cont.html(response);
        },
        error: function () { $cont.text("Cannot to load the page"); }
    });
}

function getId(form, page) {
    event.preventDefault();
    var id = form.id.value;
    var $result = $('#result');
    $.ajax({
        method: "POST",
        url: page,
        data: {
            id: id
        },
        success: function (response) {
            form.reset();
            $result.html(response);
        },
        error: function () { $result.text("Cannot to get information"); }
    });
    return false;
}

function sendUserInfo(form, page) {
    event.preventDefault();
    var id = form.id.value;
    var first_name = form.first_name.value;
    var last_name = form.last_name.value;
    var birth_date = form.birth_date.value;
    var $result = $('#result');
    $.ajax({
        method: "POST",
        url: page,
        data: {
            id: id,
            first_name: first_name,
            last_name: last_name,
            birth_date: birth_date
        },
        success: function (response) {
            form.reset();
            $result.text(response);
        },
        error: function () { $result.text("Cannot to get information"); }
    });
    return false;
}

function acceptChoice(message, func, form, page) {
    event.preventDefault();
    if (confirm(message)) {
        func(form, page);
    }
    return false;
}

function getUserAwardId(form, page) {
    event.preventDefault();
    var user_id = form.user_id.value;
    var award_id = form.award_id.value;
    var $result = $('#result');
    $.ajax({
        method: "POST",
        url: page,
        data: {
            user_id: user_id,
            award_id: award_id
        },
        success: function (response) {
            form.reset();
            $result.text(response);
        },
        error: function () { $result.text("Cannot to get information"); }
    });
}