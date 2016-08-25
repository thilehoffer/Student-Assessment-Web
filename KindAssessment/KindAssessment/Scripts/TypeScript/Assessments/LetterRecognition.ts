namespace App.Assessments {
    export class LetterRecognition {
        private scoreCard: ScoreCard;  
        public answered($container : JQuery, $hiddenForAnswer : JQuery, $inputForIncorrect: JQuery, value : string) {
           $container.removeClass("correct");
           $container.removeClass("incorrect");

            if (value === "True") {
                $hiddenForAnswer.val("True");
                 $container.addClass("correct");
                $inputForIncorrect.hide();
            }

            if (value === "False") {
                $hiddenForAnswer.val("False");
                $container.addClass("incorrect");
                $inputForIncorrect.show();
                $inputForIncorrect.focus();
            }

            if (value === "") {
                $hiddenForAnswer.val("");
                $inputForIncorrect.hide();
            }
        }
        public markAll(correct: string) {
            var $container = $("#assessment-container");
            $container.find(".answer-container").each((index, element) => {
                var $this = $(element),
                    $hiddenForAnswer = $this.find(".answer"),
                    $inputForIncorrect = $this.find(".incorrect-input");
                this.answered($this, $hiddenForAnswer, $inputForIncorrect, correct);
            
            });
            this.scoreCard.allCorrect(); 
        }

        private configureLetterCase(isUpperCase: boolean) {
            if (isUpperCase) {
                $(".incorrect-input").keyup((e) => {
                    var $target = $(e.target);
                    $target.val($target.val().toUpperCase());
                });
            }
            else {
                $(".letter").each(function () {
                    var $this = $(this);
                    $this.text($this.text().toLowerCase());
                })
                $(".incorrect-input").keyup(function () {
                    var $this = $(this);
                    $this.val($this.val().toLowerCase());
                });
            }
        }
        constructor() {
            var $container = $("#assessment-container"); 
            this.scoreCard = new ScoreCard(26,  $("#scoreCount"),  $("#totalCount"));

            $container.find(".answer-container").each(  (index, element)=> {
                var $this = $(element),
                    $hiddenForAnswer = $this.find(".answer"),
                    $inputForIncorrect = $this.find(".incorrect-input"),
                    value = $hiddenForAnswer.val();
                this.answered($this, $hiddenForAnswer, $inputForIncorrect, value);


            });
            //after marking each on from the load set the scored
            this.scoreCard.setScore($(".answer[value='True']").length);

            $container.find(".answer-container").click((e)=> {
                e.preventDefault();
                console.log(e.target.className);
                if (e.target.className.indexOf("incorrect-input") >= 0) { 
                    return;
                }
                var $target = $(e.target),
                    $hiddenForAnswer = $target.find(".answer"),
                    $inputForIncorrect = $target.find(".incorrect-input"),
                    oldValue = $hiddenForAnswer.val();
                $inputForIncorrect.val("");
                switch (oldValue) {
                    case "":
                        this.answered($target, $hiddenForAnswer, $inputForIncorrect, "True");
                        break;
                    case "False":
                        this.answered($target, $hiddenForAnswer, $inputForIncorrect, "");
                        break;
                    case "True":
                        this.answered($target, $hiddenForAnswer, $inputForIncorrect, "False");
                        break;
                }
                //Update the score
                this.scoreCard.setScore($(".answer[value='True']").length);
            });
            $container.find("#markAllCorrect").click((e)=> {
                e.preventDefault();
                this.markAll("True");
                this.scoreCard.allCorrect();
            })
            $container.find("#markAllIncorrect").click((e)=> {
                e.preventDefault();
                this.markAll("False");
                this.scoreCard.allIncorrect();
                $container.find("#markAllIncorrect").focus();
            })

            var isUpperCaseString = $("#AssessmentData_IsUpperCase").val() as string;
            console.log(isUpperCaseString);
            this.configureLetterCase((isUpperCaseString === "True"));

        }
    }
}