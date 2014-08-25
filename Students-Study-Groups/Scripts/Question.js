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

    $("#lbPostAnswer").on("click", function () {
        var text = $.trim($("#wmd-preview").html());
        if (text == "" || text.length < 30) {
            alert("Your answer must be atleast 30 characters long.");
        }
        else if (userId == 0) {
            popup();
        }
        else {
            var answerData = {
                userId: userId,
                questionId: questionId,
                text: text
            }
            var service = new Students_Study_Groups.MainService();
            service.PostAnswer(JSON.stringify(answerData), function (result) {
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

    $("a[id^='add_answer_comment_']").on("click", function () {
        var answerId = $(this).attr("id").split("_");
        $("#answer_post_comment_" + answerId[3]).toggle(500);
    });

    $("a[id^='post_answer_comment_']").on("click", function () {
        var answerId = $(this).attr("id").split("_");
        var text = $.trim($("#ta_answer_comment_" + answerId[3]).val());

        if (text == "")
            alert("You must write something before you post.");
        else if (userId == 0)
            popup();
        else {
            postData = {
                userId: userId,
                text: text,
                answerId: answerId[3]
            }
            var service = new Students_Study_Groups.MainService();
            service.PostAnswerComment(JSON.stringify(postData), function (result) {
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

    $("#upvote-question").on("click", function () {
        if (userId == 0)
            popup();
        else {
            var service = new Students_Study_Groups.MainService();
            service.UpvoteQuestion(userId, questionId, function (result) {
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

    $("#downvote-question").on("click", function () {
        if (userId == 0)
            popup();
        else {
            var service = new Students_Study_Groups.MainService();
            service.DownvoteQuestion(userId, questionId, function (result) {
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

    $("a[id^=upvote-answer]").on("click", function () {
        if (userId == 0)
            popup();
        else {
            var answerId = $(this).attr("id").split("-");
            var service = new Students_Study_Groups.MainService();
            service.UpvoteAnswer(userId, answerId[2], function (result) {
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

    $("a[id^=downvote-answer]").on("click", function () {
        if (userId == 0)
            popup();
        else {
            var answerId = $(this).attr("id").split("-");
            var service = new Students_Study_Groups.MainService();
            service.DownvoteAnswer(userId, answerId[2], function (result) {
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