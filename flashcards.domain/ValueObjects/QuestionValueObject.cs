using flashcards.domain.Entities;

namespace flashcards.domain.ValueObjects
{
    public class QuestionValueObject
{
    public long Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public long SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
    public List<AnswerValueObject> Answers { get; set; } = [];
}
}