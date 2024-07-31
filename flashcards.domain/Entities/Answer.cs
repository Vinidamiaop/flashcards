namespace flashcards.domain.Entities
{
    public class Answer : Entitie
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public Question Question = null!;
        public int QuestionId { get; set; }
    }
}