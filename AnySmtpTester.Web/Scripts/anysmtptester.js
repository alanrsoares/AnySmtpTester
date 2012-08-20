$(document).ready(function () {
    $("#fsStatus").hide();
    $("#testButton").click(function (e) {
        alert("Testing SMPT");
        $("#fsStatus").slideDown();
    });
});