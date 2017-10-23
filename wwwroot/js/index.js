window.onload = function () {
    layui.use('element', function () {
        var element = layui.element;
    });

    document.getElementById("create").onclick = function () {
        window.location.href = "/survey/createquestionnaire";
    };
    document.getElementById("doSurvey").onclick = function () {
        var quesID = parseInt(document.getElementById("surveyNo").value);
        window.location.href = `/home/survey/${quesID}`;
    };
    document.getElementById("adminPage").onclick = function () {
        window.location.href = "/home/admin";
    };
};