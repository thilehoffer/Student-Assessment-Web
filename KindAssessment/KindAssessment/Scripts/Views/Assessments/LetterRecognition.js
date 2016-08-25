function ScoreCard(total, countId, totalId) {
    this.total = total;
    this.$countElement = $('#' + countId);
    $('#' + totalId).text(total);
}
ScoreCard.prototype.setScore = function (score) {
    this.$countElement.text(score);
};
ScoreCard.prototype.allCorrect = function (score) {
    this.$countElement.text(this.total);
};
ScoreCard.prototype.allIncorrect = function (score) {
    this.$countElement.text("0");
};


$(document).ready(function () {

    var $container = $("#assessment-container"),
        isUpperCase = $("#AssessmentData_IsUpperCase").val(),
        scoreCard = new ScoreCard(26, "scoreCount", "totalCount");
    $container.find('.init-hide').hide();


    function answered($container, $hiddenForAnswer, $inputForIncorrect, value) {
        $container.removeClass("correct");
        $container.removeClass("incorrect");

        if (value === "True") {
            $hiddenForAnswer.val("True");
            $container.addClass("correct");
            $inputForIncorrect.hide(); 
        }

        if (value === 'False') {
            $hiddenForAnswer.val('False');
            $container.addClass("incorrect");
            $inputForIncorrect.show();
            $inputForIncorrect.focus(); 
        }

        if (value === '') {
            $hiddenForAnswer.val(''); 
            $inputForIncorrect.hide(); 
        }
    }
    function markAll(correct) {
        $container.find(".answer-container").each(function (index, element) {
            var $this = $(this),
            $hiddenForAnswer = $this.find(".answer"),
            $inputForIncorrect = $this.find(".incorrect-input");
            answered($this, $hiddenForAnswer, $inputForIncorrect, correct);
            scoreCard.allCorrect();
        });
    }


    $container.find(".answer-container").each(function (index, element) {
        var $this = $(this),
        $hiddenForAnswer = $this.find(".answer"),
        $inputForIncorrect = $this.find(".incorrect-input"),
        value = $hiddenForAnswer.val();
        answered($this, $hiddenForAnswer, $inputForIncorrect, value);

        
    });
    //after marking each on from the load set the scored
    scoreCard.setScore($(".answer[value='True']").length);

    $container.find(".answer-container").click(function (e) {
        e.preventDefault();
        console.log(e.target.className);
        if (e.target.className.indexOf("incorrect-input") >= 0) {
           
            return;
        }
        var $this = $(this) ,
        $hiddenForAnswer = $this.find(".answer"),
        $inputForIncorrect = $this.find(".incorrect-input"),
        oldValue = $hiddenForAnswer.val();
        $inputForIncorrect.val("");
        switch (oldValue)
        {
            case "":
                answered($this, $hiddenForAnswer,  $inputForIncorrect, "True");
                break;
            case "False":
                answered($this, $hiddenForAnswer, $inputForIncorrect, "");
                break;
            case "True":
                answered($this, $hiddenForAnswer, $inputForIncorrect, "False");
                break;    
        }
        //Update the score
        scoreCard.setScore($(".answer[value='True']").length);
    }); 
    $container.find("#markAllCorrect").click(function (e) {
        e.preventDefault();
        markAll('True');
        scoreCard.allCorrect();
    })
    $container.find("#markAllIncorrect").click(function (e) {
        e.preventDefault();
        markAll('False');
        scoreCard.allIncorrect();
        $container.find("#markAllIncorrect").focus();
    })

    if (isUpperCase === 'True')
    {
        $('.incorrect-input').keyup(function () {
            var $this = $(this);
            $this.val($this.val().toUpperCase());
        });
    }
    else
    {
        $('.letter').each(function () {
            var $this = $(this);
            $this.text($this.text().toLowerCase());
        })
        $('.incorrect-input').keyup(function () {
            var $this = $(this);
            $this.val($this.val().toLowerCase());
        });
    }
    


    $container.show();
});