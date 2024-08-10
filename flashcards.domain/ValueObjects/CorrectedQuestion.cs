using flashcards.domain.Entities;

namespace flashcards.domain.ValueObjects
{
    public class CorrectedQuestion
    {
        public Double Score { get; set; }
        public int Total { get; set; }
        public int CorrectTotal { get; set; }
        public List<Question> Incorrect { get; set; } = [];
        public List<Question> Questions {get; set;} = [];

        public CorrectedQuestion(Double score, int total, int correctTotal, List<Question> incorrect, List<Question> questions)
        {
            Score = score;
            Total = total;
            CorrectTotal = correctTotal;
            Incorrect = incorrect;
            Questions = questions;
        }
    }
}