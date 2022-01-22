// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function() {

    $("input, select, textarea").each(function () {
        //to add placeholder for all input that not have placeholder from label
        var $InputEl = $(this);
        var placeholder = $InputEl.attr("placeholder");
        var required = $InputEl.attr("data-val-required");
        var disabled = $InputEl.attr("disabled");
        if ((typeof placeholder === typeof undefined || placeholder === false) &&
            (typeof disabled === typeof undefined || disabled === false)) {
            $InputEl.attr("placeholder", $($InputEl.parents("div.form-group").find('label[for="' + $InputEl.attr("id") + '"]')).text());

            //$($InputEl.parents("div.form-group").find('label[for="' + $InputEl.attr("id") + '"]')).remove();
        }
    });

});

function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}