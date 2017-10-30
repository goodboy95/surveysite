var layer;
var answerList;
window.onload = function () {
    headerMenu();
    layui.use("layer", function () {
        layer = layui.layer;
        $(".viewAnswer").click(function (btn) {
            var answerNum = parseInt($(btn.target).parents("#ansRow").find(".quizID").html());
            layer.open({
                type: 1,
                title: 'Answer List',
                content: $("#answerText"),
                success: function (dom, index) {
                    $("#answerText").empty();
                    $.get("/quizapi/questionnaire", { surveyID: answerNum }, function (resp, stat) {
                        var quesArr = resp.data;
                        var answers = JSON.parse(answerList[answerNum - 1].answerBody);
                        for (var i = 0; i < answers.length; i++) {
                            var answerElem = $("#answerElem").clone(true);
                            var quesNo = answers[i].quesNo;
                            var quesStr = quesArr[quesNo].quesName;
                            var answerStr = "";
                            if (parseInt(quesArr[quesNo].answerType) >= 2) {
                                var answerNo = parseInt(answers[i].answer);
                                answerStr = quesArr[quesNo].options[answerNo].text;
                            }
                            else {
                                answerStr = answers[i].answer;
                            }
                            answerElem.find("#ques").append(quesStr);
                            answerElem.find("#ans").append(answerStr);
                            console.log(answerElem);
                            $("#answerText").append(answerElem);
                        }
                    });
                }
            });
        });
    });

    document.getElementById("homepage").onclick = function () {
        window.location.href = "/";
    };

    $.get("/quizapi/answer_list", {}, function (resp, stat) {
        answerList = resp.data;
        for (var i = 0; i < answerList.length; i++) {
            var answerRow = $("#ansRow").clone(true);
            console.log(answerList[i]);
            answerRow.find(".ansID").html(answerList[i].answerID);
            answerRow.find(".quizID").html(answerList[i].surveyID);
            answerRow.find(".ansCreator").html(answerList[i].answerCreator);
            $("#answerList").append(answerRow);
        }
    });
};