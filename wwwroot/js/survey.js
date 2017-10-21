var form;
var currentQues = 0;
var quesNumbers = 0;
var surveyBody;
var quesRoute = new Array();
var answerArr = new Array();

function OnSubmitClick(surveyID, data) {
    var nextQues;
    var ansObj = new Object();
    var quesType = data.quesType;
    var optionsObj = surveyBody[currentQues].options;
    if (quesType === "single") {
        nextQues = parseInt(optionsObj[0].rel);
    }
    else {
        var opt = parseInt(data.field.answer);
        nextQues = parseInt(optionsObj[opt].rel);
    }
    console.log(optionsObj[0]);
    ansObj.quesNo = currentQues;
    ansObj.answer = data.field.answer;
    console.log(ansObj);
    answerArr[quesNumbers] = ansObj;
    quesRoute[quesNumbers] = currentQues;
    if (nextQues > 0) {
        currentQues = nextQues - 1;
        RenderQuestion();
    }
    else {
        $.post("/homeApi/answer", {surveyID: surveyID, answer: JSON.stringify(answerArr)}, function(resp, stat){
            alert("You have successfully finished this questionnaire!");
            window.location.href = "/";
        });
    }
    quesNumbers++;
    return false;
}

function RenderQuestion() {
    $("#optionArea").empty();
    $("#answerArea").empty();
    var ques = surveyBody[currentQues];
    var optionArr = ques.options;
    if (parseInt(ques.answerType) === 2) {
        $("#optionQues").show();
        for (var i = 0; i < optionArr.length; i++) {
            var optionBody = document.getElementById("optAnswer").cloneNode(true);
            optionBody.value = i;
            optionBody.title = optionArr[i].text;
            if (i === 0) {
                optionBody.checked = true;
            }
            $("#optionArea").append($(optionBody));
        }
        form.render('radio');
    }
    else {
        $("#textQues").show();
    }
}

window.onload = function(){
    var surveyID = document.getElementById("surveyID").value;
    layui.use("form", function(){
        form = layui.form;
        form.on("submit(optSubmit)", function(data){
            data.quesType = "multiple";
            OnSubmitClick(surveyID, data);
        });
        form.on("submit(textSubmit)", function(data){
            data.quesType = "single";
            OnSubmitClick(surveyID, data);
        });
        $.get("/homeApi/questionnaire", {surveyID: surveyID}, function(resp, stat){
            surveyBody = resp.data;
            console.log(surveyBody);
            RenderQuestion();
        });
    });

};