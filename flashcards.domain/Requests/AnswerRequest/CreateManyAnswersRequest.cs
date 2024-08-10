using flashcards.domain.Entities;

namespace flashcards.domain.Requests.AnswerRequest
{
    public class CreateManyAnswersRequest(long questionId, List<Answer> answers) : Request
    {
        public List<Answer> Answers { get; set; } = answers;

        public long QuestionId { get; set; } = questionId;

        public override bool IsValid()
        {
            if (QuestionId <= 0)
            {
                Errors.Add("Invalid questionId");
            }

            return Errors.Count <= 0;
        }
    }
}