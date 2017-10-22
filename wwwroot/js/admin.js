var layer;
var answerList;
window.onload = function () {
    layui.use("layer", function () {
        layer = layui.layer;
        $("#viewAnswer").click(function (btn) {
            var answerNum = parseInt($(btn.target).parents("#answerRow").find("#answerID").html());
            console.log(document.getElementById("answerText"));
            layer.open({
                type: 1,
                title: 'Answer List',
                content: $("#answerText"),
                success: function (dom, index) {
                    $("#answerText").empty();
                    $.get("/homeapi/questionnaire", { surveyID: answerNum }, function (resp, stat) {
                        var quesArr = resp.data;
                        var answers = JSON.parse(answerList[answerNum - 1].answerBody);
                        for (var i = 0; i < answers.length; i++) {
                            var answerElem = $("#answerElem").clone(true);
                            var quesNo = answers[i].quesNo;
                            var answerNo = parseInt(answers[i].answer);
                            var quesStr = quesArr[quesNo].quesName;
                            var answerStr = quesArr[quesNo].options[answerNo].text;
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

    $.get("/homeapi/answer_list", {}, function (resp, stat) {
        answerList = resp.data;
        for (var i = 0; i < answerList.length; i++) {
            var answerRow = $("#answerRow").clone(true);
            answerRow.find("#answerID").html(answerList[i].answerID);
            answerRow.find("#creator").html(answerList[i].answerCreator);
            $("#answerList").append(answerRow);
        }
    });
};