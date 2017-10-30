var quesNum = 0;
var optionNum = 0;
var questionList = new Array();
var form = null;

function AddOption(optBox) {
    var quesBody = document.getElementById("q0").cloneNode(true);
    var optionJq = $(quesBody).find("#o1");
    var optBoxJq = $(optBox);
    var quesStrId = optBoxJq.parents(".layui-form").attr("id");
    var quesId = parseInt(optBoxJq.parents(".layui-form").attr("id").substring(1)) - 1;
    optionNum++;
    optionJq.attr("id", `o${optionNum}`);
    $("#options").append(optionJq);
};

function RemoveOption(optBox) {
    var optBoxJq = $(optBox);
    var quesStrId = optBoxJq.parents(".layui-form").attr("id");
    var quesId = parseInt(quesStrId.substring(1)) - 1;
    if (optionNum <= 0) return;
    $(`#${quesStrId} #o${optionNum}`).remove();
    optionNum--;
};

function ShowQuestionDialog() {
    layer.open({
        type: 1,
        content: $('#quesEditor'),
        title: "Question Editor"
    });
};

function AddQuestion() {
    quesNum++;
    optionNum = 1;
    var quesBody = document.getElementById("q0").cloneNode(true);
    quesBody.id = `q${quesNum}`;
    quesBodyJq = $(quesBody);
    quesBodyJq.find("#title").html(`Question ${quesNum}:`);
    $("#questionBox").append(quesBodyJq);
    form.render('select');
}

window.onload = function () {
    headerMenu();
    optionNum = 1;
    layui.use("form", function(){
        form = layui.form;
        form.on("select(answerType)", function(data) {
            var choice = data.value;
            var quesId = $(data.elem).parents(".layui-form")[0].id;
            if (choice >= 2) {
                $(`#optionInput`).show();
                $(`#textQuesDetail`).hide();
            }
            else {
                $(`#optionInput`).hide();
                $(`#textQuesDetail`).show();
            }
        });
        form.on('submit(saveques)', function (data) {
            var ques = new Object();
            ques.quesName = data.field.quesName;
            ques.answerType = data.field.answerType;
            if (parseInt(quesType) >= 2) {
                ques.options = new Array();
                for (var i = 1; i <= optionNum; i++) {
                    var option = new Object();
                    optionJq = quesObj.find(`#o${i}`);
                    option.text = optionJq.find("#optionText").val();
                    option.rel = optionJq.find("#relatedQues").val();
                    if (isNaN(parseInt(option.rel))) {
                        alert("Relating-question number must be a number!");
                        return;
                    }
                    ques.options.push(option);
                    //后续清除操作
                    if (i > 1) {
                        optionJq.remove();
                    }
                    else {
                        optionJq.find("#optionText").val("");
                        optionJq.find("#relatedQues").val("");
                    }
                }
            }
            else {
                ques.nextQues = data.field.nextQues;
                $("#textNextQues").val("");
            }
        });
    });

    document.getElementById("addQues").onclick = ShowQuestionDialog;

    document.getElementById("removeQues").onclick = function(){
        if (quesNum >= 1) {
            $(`#q${quesNum}`).remove();
            quesNum--;
        }
    };

    document.getElementById("submit").onclick = function(){
        
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
        $.post("/surveyApi/questionnaire", {quesName: questionnaireTitle, quesJson: JSON.stringify(questionList)}, function(resp, stat) {
            window.location.href = "/";
        });
    };
};