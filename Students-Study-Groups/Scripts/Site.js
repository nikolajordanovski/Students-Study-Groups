$(document).ready(function () {

    $("#lbCancel").click(function () {
        if (window.location.href.indexOf("Profile.aspx") > -1) {
            window.location.href = "Index.aspx";
        }
        $('#dialog-overlay, #login-dialog').hide();
        return false;
    });

    $(window).resize(function () {
        //only do it if the dialog box is not hidden
        if (!$('#login-dialog').is(':hidden')) popup();
    });

    $("#lbLogin").click(function () {

        var user = $("#tbUsernameLogin").val();
        var pass = $("#tbPasswordLogin").val();

        var userData = {
            username: user,
            password: pass
        }

        var service = new Students_Study_Groups.MainService();
        service.LoginUser(JSON.stringify(userData), function (result) {
            var res = JSON.parse(result);
            if (res.status == "success") {
                $('#dialog-overlay, #login-dialog').hide();
                if (window.location.href.indexOf("SignUp.aspx") > -1) {
                    window.location.href = "Index.aspx";
                }
                else location.reload();
            }
            else if (res.status == "error") {
                alert(res.message);
            }
        });

    });
});

//Popup dialog
function popup() {
    // get the screen height and width  
    var maskHeight = $(document).height();
    var maskWidth = $(window).width();

    // calculate the values for center alignment
    var dialogTop = (maskHeight / 3) - ($('#login-dialog').height());
    var dialogLeft = (maskWidth / 2) - ($('#login-dialog').width() / 2);

    // assign values to the overlay and dialog box
    $('#dialog-overlay').css({ height: maskHeight, width: maskWidth }).show();
    $('#login-dialog').css({ top: dialogTop, left: dialogLeft }).show();
}

function logout() {
    var service = new Students_Study_Groups.MainService();

    service.LogoutUser(function (result) {
        alert(result);
        location.reload();
    });
}