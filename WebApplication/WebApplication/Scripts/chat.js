$(document).ready(function () {
    // login
    $("#btnLogin").click(function () {
        var nickName = $("#txtUserName").val();
        if (nickName) {
           var href = "/Home?user=" + encodeURIComponent(nickName);      
           href = href + "&logOn=true";
           $("#LoginButton").attr("href", href).click();
          //pole
            $("Username").text(nickName);
        }
    });
});
//przejście dalej
