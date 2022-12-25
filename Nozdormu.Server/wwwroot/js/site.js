// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function deletethething(id) {
    $.ajax({
        type: "Delete",
        url: "/Accounts/Delete/" + id,
        success: function (response) {
            window.location.href = window.location.href;
        }
    });
}