var quesNum = 0;
var optionNum = new Array();
var quesBody = null;
var optionBody = null;
var form = null;

function AddOption(optBox) {
    var quesBody = document.getElementById("q0").cloneNode(true);
    var optionJq = $(quesBody).find("#o1");
    var optBoxJq = $(optBox);
    var quesStrId = optBoxJq.parents(".layui-form").attr("id");
    var quesId = parseInt(optBoxJq.parents(".layui-form").attr("id").substring(1)) - 1;
    optionNum[quesId]++;
    optionJq.attr("id", `o${optionNum[quesId]}`);
    optBoxJq.siblings("#options").append(optionJq);
};

function RemoveOption(optBox) {
    var optBoxJq = $(optBox);
    var quesStrId = optBoxJq.parents(".layui-form").attr("id");
    var quesId = parseInt(quesStrId.substring(1)) - 1;
    if (optionNum[quesId] <= 0) return;
    $(`#${quesStrId} #o${optionNum[quesId]}`).remove();
    optionNum[quesId]--;
};

function AddQuestion() {
    quesNum++;
    optionNum[quesNum] = 1;
    var quesBody = document.getElementById("q0").cloneNode(true);
    quesBody.id = `q${quesNum}`;
    quesBodyJq = $(quesBody);
    quesBodyJq.find("#title").html(`Question ${quesNum}:`);
    $("#questionBox").append(quesBodyJq);
    form.render('select');
}

window.onload = function(){
    optionNum[0] = 1;
    layui.use("form", function(){
        form = layui.form;
        form.on("select(answerType)", function(data) {
            var choice = data.value;
            var quesId = $(data.elem).parents(".layui-form")[0].id;
            if (choice >= 2) {
                $(`#${quesId} #optionInput`).show();
            }
            else {
                $(`#${quesId} #optionInput`).hide();
            }
        });
        AddQuestion();
    });

    document.getElementById("addQues").onclick = AddQuestion;

    document.getElementById("removeQues").onclick = function(){
        if (quesNum >= 1) {
            $(`#q${quesNum}`).remove();
            quesNum--;
        }
    };

    document.getElementById("submit").onclick = function(){
        var questionList = new Array();
        var questionnaireTitle = $("#surveyName").val();
        for (var i = 1; i <= quesNum; i++)
        {
            var ques = new Object();
            var quesObj = $(`#q${i}`);
            if (quesObj != undefined) {
                ques.quesName = quesObj.find("#quesName").val();
                ques.answerType = quesObj.find("#answerType").val();
                ques.options = new Array();
                for (var j = 1; j <= optionNum[i - 1]; j++) {
                    var option = new Object();
                    optionJq = quesObj.find(`#o${j}`);
                    option.text = optionJq.find("#optionText").val();
                    option.rel = optionJq.find("#relatedQues").val();
                    if (isNaN(parseInt(option.rel))) {
                        alert("Relating-question number must be a number!");
                        return;
                    }
                    ques.options.push(option);
                }
                questionList.push(ques);
            }
        }
        console.log(questionList);
        $.post("/homeApi/questionnaire", {quesName: questionnaireTitle, quesJson: JSON.stringify(questionList)}, function(resp, stat) {
            window.location.href = "/";
        });
    };
};