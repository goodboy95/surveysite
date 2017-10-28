var layer, form;
window.onload = function () {
    var isLogin = document.getElementById("islogin").value;
    if (isLogin === "true") {
        //写入登录后的逻辑
    }
    layui.use('element', function () {
        var element = layui.element;
    });
    layui.use('layer', function() {
        layer = layui.layer;
    });
    layui.use('form', function() {
        form = layui.form;
        form.on('submit(login)', function(data){
            $.post("/homeapi/login", {
                username: data.field.username,
                password: data.field.password,
            }, function(resp, stat){
                if (resp.code === 0) {
                    alert("logined");
                }
                else {
                    alert("login error");
                }
                
            });
            return false;
        });
        form.on('submit(reg)', function(data){
            $.post("/homeapi/register", {
                username: data.field.username,
                password: data.field.password,
            }, function(resp, stat){
                if (resp.code === 0) {
                    alert("registered");
                }
                else {
                    alert("register error");
                }
            });
            return false;
        });
    });

    document.getElementById("create").onclick = function () {
        window.location.href = "/survey/createquestionnaire";
        return false;
    };
/*    document.getElementById("doSurvey").onclick = function () {
        var quesID = parseInt(document.getElementById("surveyNo").value);
        window.location.href = `/home/survey/${quesID}`;
    };*/
    document.getElementById("template-manage").onclick = function () {
        window.location.href = "/home/admin";
        return false;
    };
    document.getElementById("login-link").onclick = function () {
        layer.open({
            type: 1,
            content: $('#login'),
            title: "Sign In"
        });
        return false;
    };
    document.getElementById("reg-link").onclick = function () {
        layer.open({
            type: 1,
            content: $('#register'),
            title: "Sign Up"
        });
        return false;
    };
};