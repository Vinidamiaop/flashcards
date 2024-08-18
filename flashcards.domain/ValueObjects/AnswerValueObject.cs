using flashcards.domain.Entities;

namespace flashcards.domain.ValueObjects
{
    public class AnswerValueObject
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public long QuestionId { get; set; }
        public bool IsChecked { get; set; } = false;
        public bool IsCorrect {get;} = false;

        public AnswerValueObject(long id, string text, long questionId, bool isChecked) {
            Id = id;
            Text = text;
            QuestionId = questionId;
            IsChecked = isChecked;
        }

        public static implicit operator AnswerValueObject(Answer answer) {
            return new AnswerValueObject(answer.Id, answer.Text, answer.QuestionId, answer.IsChecked);
        }
    }
}