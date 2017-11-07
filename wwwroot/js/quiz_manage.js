window.onload = function () {
    headerMenu();
    $.get("/quizApi/quiz_list", {}, function (resp, stat) {
        var surveyList = resp.data;
        for (var i = 0; i < surveyList.length; i++) {
            var quizRow = $("#quizRow").clone(true);
            quizRow.find(".quesID").html(surveyList[i].surveyID);
            quizRow.find(".quesName").html(surveyList[i].surveyName);
            quizRow.find(".quesURL").html(`http://www.seekerhut.com/quiz/quizpage/${surveyList[i].surveyID}`);
            $("#quizList").append(quizRow);
        }
    });

    document.getElementById("homepage").onclick = function () {
        window.location.href = "/";
    };
};

function EditQuesGroup(editBtn) {
    var quesNum = parseInt($(editBtn).parents(".quiz-row").find(".quesID").html());
    window.location.href = `/quiz/createquiz/${quesNum}`;
}

function GotoQuiz(quizBtn) {
    var quesNum = parseInt($(quizBtn).parents(".quiz-row").find(".quesID").html());
    window.location.href = `/quiz/quizpage/${quesNum}`;
}