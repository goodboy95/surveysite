var form;
var currentQues = 0;
var quesCount = 0;
var surveyBody;
var quesRoute = new Array();
var answerArr = new Array();

function OnNextClick(surveyID, data) {
    var nextQues;
    var ansObj = new Object();
    var quesType = data.quesType;
    var optionsObj = surveyBody[currentQues].options;
    if (quesType === "single") {
        nextQues = parseInt(surveyBody[currentQues].nextQues);
    }
    else {
        var opt = parseInt(data.field.answer);
        nextQues = parseInt(optionsObj[opt].rel);
    }
    ansObj.quesNo = currentQues;
    ansObj.answer = data.field.answer;
    answerArr[quesCount] = ansObj;
    quesRoute[quesCount] = currentQues;
    if (nextQues > 0) {
        currentQues = nextQues - 1;
        RenderQuestion();
    }
    else {
        $.post("/quizApi/answer", {surveyID: surveyID, answer: JSON.stringify(answerArr)}, function(resp, stat){
            alert("You have successfully finished this quiz!");
            window.location.href = "/";
        });
    }
    quesCount++;
    return false;
}

function RenderQuestion() {
    $("#optQuesTitle").html("");
    $("#textQuesTitle").html("");
    $("#optionArea").html("");
    $("#answerArea").val("");
    var ques = surveyBody[currentQues];
    var quesName = ques.quesName;
    var optionArr = ques.options;
    $(".ques-title").html(`Question: ${quesName}`);
    if (parseInt(ques.answerType) === 2) {
        $("#optionQues").show();
        $("#textQues").hide();
        for (var i = 0; i < optionArr.length; i++) {
            var optionBody = document.getElementById("optAnswer").cloneNode(true);
            optionBody.value = i;
            optionBody.title = optionArr[i].text;
            if (i === 0) {
                optionBody.checked = true;
            }
            $("#optionArea").append($(optionBody)).append("<br />");
        }
        form.render('radio');
    }
    else {
        $("#optionQues").hide();
        $("#textQues").show();
    }
}

window.onload = function () {
    headerMenu();
    var surveyID = document.getElementById("surveyID").value;
    layui.use("form", function(){
        form = layui.form;
        form.on("submit(optNext)", function(data){
            data.quesType = "multiple";
            OnNextClick(surveyID, data);
        });
        form.on("submit(textNext)", function(data){
            data.quesType = "single";
            OnNextClick(surveyID, data);
        });
        form.on("submit(prev)", function(data) {
            currentQues = quesRoute[quesCount-1];
            quesCount--;
            answerArr.pop();
            quesRoute.pop();
            RenderQuestion();
        });
        $.get("/quizApi/questionnaire", {surveyID: surveyID}, function(resp, stat){
            surveyBody = resp.data;
            RenderQuestion();
        });
    });
    document.getElementById("startSurvey").onclick = function() {
        $("#intro").hide();
        RenderQuestion();
    }
};