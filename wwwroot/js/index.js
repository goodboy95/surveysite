window.onload = function () {
    document.getElementById("create").onclick = function () {
        window.location.href = "/home/createquestionnaire";
    };
    document.getElementById("doSurvey").onclick = function () {
        var quesID = parseInt(document.getElementById("surveyNo").val());
        window.location.href = `/home/survey/${quesID}`;
    };
};