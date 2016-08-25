var App;
(function (App) {
    var Assessments;
    (function (Assessments) {
        var ScoreCard = (function () {
            function ScoreCard(total, $countElement, $totalElement) {
                this.total = total;
                this.$countElement = $countElement;
                this.$totalElement = $totalElement;
                this.$totalElement.html(total.toString());
            }
            ScoreCard.prototype.setScore = function (score) {
                console.log("current score is " + score.toString());
                this.$countElement.html(score.toString());
            };
            ScoreCard.prototype.allCorrect = function () {
                this.$countElement.text(this.total);
            };
            ScoreCard.prototype.allIncorrect = function () {
                this.$countElement.text("0");
            };
            return ScoreCard;
        }());
        Assessments.ScoreCard = ScoreCard;
    })(Assessments = App.Assessments || (App.Assessments = {}));
})(App || (App = {}));
//# sourceMappingURL=ScoreCard.js.map