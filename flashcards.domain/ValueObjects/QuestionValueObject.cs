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

        public QuestionValueObject(long id, string text, long subjectId, Subject subject, List<AnswerValueObject> answers)
        {
            Id = id;
            Text = text;
            SubjectId = subjectId;
            Subject = subject;
            Answers = answers;
        }

        public static implicit operator QuestionValueObject(Question question)
        {
            List<AnswerValueObject> answers = [];
            foreach(var answer in question.Answers) {
                answers.Add(answer);
            }
            
            return new QuestionValueObject(question.Id, question.Text, question.SubjectId, question.Subject, answers);
        }
    }
}