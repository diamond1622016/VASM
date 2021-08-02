function login() {
    notify.loading();
    $.ajax({
        type: 'POST',
        url: '/account/checkLogin',
        dataType: 'json',
        data: { userName: $('#Username').val(), passWord: $('#Password').val() },
        success: function (response) {
            if (response.status) {
                notify.push(response.mess, notify.EType.SUCCESS);
                var rtUrl = $('#rtUrl').val();
                if (rtUrl === "" || rtUrl === undefined) {
                    rtUrl = "/";
                }
                setTimeout(function () {
                    window.location.href = rtUrl;
                }, 3000);
            } else {
                notify.done();
                notify.push(response.mess, notify.EType.DANGER);
            }
        },
        error: (jqXHR, textStatus) => {
            let mess = "Request failed: " + textStatus;
            notify.done();
            notify.push(mess, notify.EType.DANGER);
        }
    });
};
function login_old() {
    utils.loading();

    $.ajax({
        type: 'POST',
        url: '/account/checkLogin',
        dataType: 'json',
        data: { userName: $('#Username').val(), passWord: $('#Password').val() },
        success: function (response) {
            if (response.status) {
                utils.done();
                Lobibox.alert("success", {
                    msg: response.mess,
                    beforeClose: function () {
                        var rtUrl = $('#rtUrl').val();
                        if (rtUrl === "") {
                            rtUrl = "/";
                        }
                        window.location.href = rtUrl;
                    }
                });
            } else {
                utils.done();
                Lobibox.alert("error", {
                    msg: response.mess
                });
            }
        },
        error: function (jqXHR) {
            utils.done();
            Lobibox.alert("error", {
                msg: "ERR"
            });
        }
    });
};
$(document).ready(function () {
    $('input').keypress(function (e) {
        if (e.which === 13) {
            e.preventDefault();
            login();
            return false;
        }
    });
    $('#btnLogin').click(function (e) {
        e.preventDefault();
        login();
    });

    $('#frmLogin').submit(function (e) {
        e.preventDefault();
    });

    $('#frmReg').submit(function (e) {
        e.preventDefault();
        notify.loading();
        var formData = new FormData(this);
        $.ajax({
            url: '/account/register',
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.status) {
                    notify.push(response.mess, notify.EType.SUCCESS);
                    var rtUrl = $('#rtUrl').val();
                    if (rtUrl === "" || rtUrl === undefined) {
                        rtUrl = "/home";
                    }
                    setTimeout(function () {
                        window.location.href = rtUrl;
                    }, 3000);
                } else {
                    notify.done();
                    notify.push(response.mess, notify.EType.DANGER);
                }
            },
            cache: false,
            contentType: false,
            processData: false
        });
    });
});