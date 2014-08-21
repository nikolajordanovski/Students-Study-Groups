$(document).ready(function () {

    $("#add_question_comment").on("click", function () {
        $(".question-post-comment").toggle(500);
    });

    $("#post_question_comment").on("click", function () {
        var text = $.trim($("#ta_question_comment").val());

        if (text == "")
            alert("You must write something before you post.");
        else if (userId == 0)
            popup();
        else {
            postData = {
                userId: userId,
                text: text,
                questionId: questionId
            }
            var service = new Students_Study_Groups.MainService();
            service.PostQuestionComment(JSON.stringify(postData), function (result) {
                var res = JSON.parse(result);
                if (res.status == "success") {
                    window.location.href = "Question.aspx?QID=" + questionId;
                }
                else {
                    alert(res.message);
                }
            });
        }
    });
});