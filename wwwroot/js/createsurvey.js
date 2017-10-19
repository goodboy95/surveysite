var questionList = new Array();
var quesNum = 1;
var optionNum = new Array();

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

function AddOption(opt) {
    var optJq = $(opt);
    var id = this.id;
}

window.onload = function(){
    optionNum[0] = 1;
    layui.use("form", function(){
        var form = layui.form;
    });

    $("#answerType").click(function(){
        console.log("dsdsds");
    });
    
    document.getElementById("addQues").onclick = function(){
        quesNum++;
        optionNum[quesNum] = 1;
        var quesBody = document.getElementById("q1").cloneNode(true);
        quesBody.id = `q${quesNum}`;
        quesBodyJq = $(quesBody);
        quesBodyJq.find("#title").html(`问题${quesNum}:`);
        quesBody = quesBodyJq[0];
        document.getElementById("questionBox").appendChild(quesBody);
    }

    document.getElementById("removeQues").onclick = function(){
        //var thisQues = document.getElementById(quesId);
        //document.getElementById("questionBox").remove(thisQues);
        $(`#q${quesNum}`).remove();
        quesNum--;
    }

    document.getElementById("submit").onclick = function(){
        for (var i = 1; i <= quesNum; i++)
        {
            var ques = new Object();
            var quesObj = $(`#q${i}`);
            if (quesObj != undefined) {
                ques.quesName = quesObj.find("#quesName").val();
                ques.answerType = quesObj.find("#answerType").val();
                questionList.push(ques);
            }
            console.log(ques);
        }
    }
};