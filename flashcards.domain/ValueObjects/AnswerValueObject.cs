namespace flashcards.domain.ValueObjects
{
    public class AnswerValueObject
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public long QuestionId { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}