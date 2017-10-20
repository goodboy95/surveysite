var questionList = new Array();
var quesNum = 1;
var optionNum = new Array();
var quesBody = null;
var optionBody = null;

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
    var quesId = parseInt(optJq.parents(".layui-form").attr("id").substring(1)) - 1;
    optionNum[quesId]++;
    optJq.siblings("#options").append(`                <div id=o${optionNum[quesId]}>
                    <input type="text" placeholder="请输入" autocomplete="off" class="layui-input">
                    <input type="text" placeholder="关联题号" autocomplete="off" class="layui-input">
                </div>`);
}

function RemoveOption(opt) {
    var optJq = $(opt);
    var quesStrId = optJq.parents(".layui-form").attr("id");
    var quesId = parseInt(quesStrId.substring(1)) - 1;
    if (optionNum[quesId] <= 0) return;
    $(`#${quesStrId} #o${optionNum[quesId]}`).remove();
    optionNum[quesId]--;
}

window.onload = function(){
    optionNum[0] = 1;
    //quesBody = document.getElementById("q1").cloneNode(true);
    //optionBody = document.getElementById("o1").cloneNode(true);
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
                ques.options = new Array();
                for (var j = 1; j <= optionNum[i - 1]; j++) {
                    var option = new Object();
                    console.log(`#o${j}`);
                    optionJq = quesObj.find(`#o${j}`);
                    option.text = optionJq.find("#optionText").val();
                    option.rel = optionJq.find("#relatedQues").val();
                    ques.options.push(option);
                }
                questionList.push(ques);
            }
        }
        console.log(questionList);
    }
};