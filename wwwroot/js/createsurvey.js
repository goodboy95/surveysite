var questionList = new Array();
var quesNum = 1;

function ShowChoiceBox(opt) {
    var choice = opt.value;
    var quesId = $(opt).parents(".layui-form")[0].id;
    if (choice >= 2) {
        $(`#${quesId} #optionInput`).show();
    }
    else {
        $(`#${quesId} #optionInput`).hide();
    }
}

function RemoveQues(opt) {
    var quesId = $(opt).parents(".layui-form")[0].id;
    console.log(quesId);
    var thisQues = document.getElementById(quesId);
    document.getElementById("questionBox").remove(thisQues);
}

window.onload = function(){
    layui.use("form", function(){
        var form = layui.form;
    });
    
    document.getElementById("addQues").onclick = function(){
        quesNum++;
        var quesBody = document.getElementById("q1").cloneNode(true);
        quesBody.id = "q" + quesNum.toString();
        document.getElementById("questionBox").appendChild(quesBody);
    }
    //questionList.
    //document.getElementById("submit")
};