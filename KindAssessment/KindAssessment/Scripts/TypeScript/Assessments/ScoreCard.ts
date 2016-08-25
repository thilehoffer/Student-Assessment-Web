namespace App.Assessments {
    export class ScoreCard {
        private total: number;
        private $countElement: JQuery;
        private $totalElement: JQuery;
        public setScore(score: number) {
            console.log("current score is " + score.toString());
            this.$countElement.html(score.toString());
        }
        public allCorrect() {
            this.$countElement.text(this.total);
        }
        public allIncorrect() {
            this.$countElement.text("0");
        }
        constructor(total : number, $countElement : JQuery,  $totalElement : JQuery) {
            this.total = total;
            this.$countElement = $countElement;
            this.$totalElement = $totalElement;
            this.$totalElement.html(total.toString());
        }
    }
}