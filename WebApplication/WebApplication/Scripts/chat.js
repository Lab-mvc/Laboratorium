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
function LoginOnSuccess(result) {
    
    Scroll();
    ShowLastRefresh();
    
    //aktualizacja co 5 sek
    setTimeout("Refresh();", 5000);
    
    //wysyłanie wiadomości za pomoca enter
    $( '#txtMessage ').keydown(function) (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#btnMessage").click();
        }
    });
    
