namespace flashcards.domain.Entities
{
    public class Answer : Entitie
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public Question Question = null!;
        public long QuestionId { get; set; }
        public bool IsChecked { get; set; } = false;

        public Answer() { }

        public Answer(string text, bool isCorrect, Question question, bool isChecked = false)
        {
            Text = text;
            IsCorrect = isCorrect;
            Question = question;
            IsChecked = isChecked;
        }
    }
}