var App;
(function (App) {
    var Assessments;
    (function (Assessments) {
        var LetterRecognition = (function () {
            function LetterRecognition() {
                var _this = this;
                var $container = $("#assessment-container");
                this.scoreCard = new Assessments.ScoreCard(26, $("#scoreCount"), $("#totalCount"));
                $container.find(".answer-container").each(function (index, element) {
                    var $this = $(element), $hiddenForAnswer = $this.find(".answer"), $inputForIncorrect = $this.find(".incorrect-input"), value = $hiddenForAnswer.val();
                    _this.answered($this, $hiddenForAnswer, $inputForIncorrect, value);
                });
                //after marking each on from the load set the scored
                this.scoreCard.setScore($(".answer[value='True']").length);
                $container.find(".answer-container").click(function (e) {
                    e.preventDefault();
                    console.log(e.target.className);
                    if (e.target.className.indexOf("incorrect-input") >= 0) {
                        return;
                    }
                    var $target = $(e.target), $hiddenForAnswer = $target.find(".answer"), $inputForIncorrect = $target.find(".incorrect-input"), oldValue = $hiddenForAnswer.val();
                    $inputForIncorrect.val("");
                    switch (oldValue) {
                        case "":
                            _this.answered($target, $hiddenForAnswer, $inputForIncorrect, "True");
                            break;
                        case "False":
                            _this.answered($target, $hiddenForAnswer, $inputForIncorrect, "");
                            break;
                        case "True":
                            _this.answered($target, $hiddenForAnswer, $inputForIncorrect, "False");
                            break;
                    }
                    //Update the score
                    _this.scoreCard.setScore($(".answer[value='True']").length);
                });
                $container.find("#markAllCorrect").click(function (e) {
                    e.preventDefault();
                    _this.markAll("True");
                    _this.scoreCard.allCorrect();
                });
                $container.find("#markAllIncorrect").click(function (e) {
                    e.preventDefault();
                    _this.markAll("False");
                    _this.scoreCard.allIncorrect();
                    $container.find("#markAllIncorrect").focus();
                });
                var isUpperCaseString = $("#AssessmentData_IsUpperCase").val();
                console.log(isUpperCaseString);
                this.configureLetterCase((isUpperCaseString === "True"));
            }
            LetterRecognition.prototype.answered = function ($container, $hiddenForAnswer, $inputForIncorrect, value) {
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
            };
            LetterRecognition.prototype.markAll = function (correct) {
                var _this = this;
                var $container = $("#assessment-container");
                $container.find(".answer-container").each(function (index, element) {
                    var $this = $(element), $hiddenForAnswer = $this.find(".answer"), $inputForIncorrect = $this.find(".incorrect-input");
                    _this.answered($this, $hiddenForAnswer, $inputForIncorrect, correct);
                });
                this.scoreCard.allCorrect();
            };
            LetterRecognition.prototype.configureLetterCase = function (isUpperCase) {
                if (isUpperCase) {
                    $(".incorrect-input").keyup(function (e) {
                        var $target = $(e.target);
                        $target.val($target.val().toUpperCase());
                    });
                }
                else {
                    $(".letter").each(function () {
                        var $this = $(this);
                        $this.text($this.text().toLowerCase());
                    });
                    $(".incorrect-input").keyup(function () {
                        var $this = $(this);
                        $this.val($this.val().toLowerCase());
                    });
                }
            };
            return LetterRecognition;
        }());
        Assessments.LetterRecognition = LetterRecognition;
    })(Assessments = App.Assessments || (App.Assessments = {}));
})(App || (App = {}));
//# sourceMappingURL=LetterRecognition.js.map