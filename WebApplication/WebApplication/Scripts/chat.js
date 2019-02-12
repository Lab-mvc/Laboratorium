  $(document).ready(function () {

    // login
    $("#btnLogin").click(function () {
        var nickName = $("#txtUserName").val();
        if (nickName) {
            var href = "/Home?user=" + encodeURIComponent(nickName);
            href = href + "&logOn=true";
            $("#LoginButton").attr("href", href).click();
            //pole
            $("#Username").text(nickName);
        }
    });
});

//przejscie dalej 
function LoginOnSuccess(result) {

    Scroll();
    ShowLastRefresh();

    //aktualizacja co 5 sek
    setTimeout("Refresh();", 5000);

    //wysylanie wiadomosci za pomoca enter
    $('#txtMessage').keydown(function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            $("#btnMessage").click();
        }
    });

    $("#btnMessage").click(function () {
        var text = $("#txtMessage").val();
        if (text) {
            //za pomoca Index przesylamy parametr "chatMessage"
            var href = "/Home?user=" + encodeURIComponent($("#Username").text());
            href = href + "&chatMessage=" + encodeURIComponent(text);
            $("#ActionLink").attr("href", href).click();
            $("#txtMessage").empty();
        }
    });

    //btn wyjscia
    $("#btnLogOff").click(function () {
        //za pomoca Index przesylamy parametr "logOff"
        var href = "/Home?user=" + encodeURIComponent($("#Username").text());
        href = href + "&logOff=true";
        $("#ActionLink").attr("href", href).click();
        document.location.href = "Home";
    });
}

//komunikat o zly login 
function LoginOnFailure(result) {
    $("#Username").val("");
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty();", 2000);
}

// aktualizacja co 5 sec
function Refresh() {
    var href = "/Home?user=" + encodeURIComponent($("#Username").text());
    $("#ActionLink").attr("href", href).click();
    setTimeout("Refresh();", 5000);
}

//komunikat
function ChatOnFailure(result) {
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty();", 2000);
}

function ChatOnSuccess(result) {
    Scroll();
    ShowLastRefresh();
}

function Scroll() {
    var win = $('#Messages');
    var height = win[0].scrollHeight;
    win.scrollTop(height);
}

//ostatna aktualizacja
function ShowLastRefresh() {
    var dt = new Date();
    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
    $("#LastRefresh").text("Ostatna aktualizacja : " + time);
}
    
    
    
