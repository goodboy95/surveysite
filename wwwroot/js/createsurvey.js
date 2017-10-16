var questionList = new Array();
var quesNum = 1;

function ShowChoiceBox(opt) {
    var choice = opt.value;
    if (choice >= 2) {
        $("#optionInput").show();
        //document.getElementById("optionInput").style.visibility = "visible";
    }
}
window.onload = function(){
    layui.use("form", function(){
        var form = layui.form;
    });
    //questionList.
    //document.getElementById("submit")
};