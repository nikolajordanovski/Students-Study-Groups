$(document).ready(function () {

    $("#MainContent_tbTags").tokens({
        keyCode: {
            UP: 38,
            DOWN: 40,
            BACKSPACE: 8,
            TAB: 9,
            ENTER: 13,
            ESC: 27,
            COMMA: 188,
        }
    });

    $("#lbAskQuestion").on("click", function(){
                Page_ClientValidate();

                if(Page_IsValid) {
                    
                    if(userId != 0) {
                        var titlestr = $("#MainContent_tbTitle").val();
                        var bodystr = $("#wmd-preview").html();
                        var tagsList = $("#MainContent_tbTags").val().split(",");
                    
                        var questionData = {
                            title : titlestr,
                            body : bodystr,
                            tags : tagsList,
                            UID : userId
                        }

                        var service = new Students_Study_Groups.MainService();
                        service.AskQuestion(JSON.stringify(questionData), function(result) { 
                            var res = JSON.parse(result);
                            if(res.status == "success") {
                                window.location.href = "Question.aspx?QID=" + res.QID;
                            }
                            else {
                                alert(res.message);
                            }
                        });
                    }
                    else {
                        popup();
                    }    
                }
                
            });
});