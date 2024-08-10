using flashcards.domain.Entities;

namespace flashcards.domain.Requests.QuestionRequest
{
    public class QuestionCorrectionRequest : Request
    {
        public List<Question> Questions { get; set; } = [];

        public override bool IsValid()
        {
            if (Questions.Count <= 0) Errors.Add("Invalid request");

            return Errors.Count <= 0;
        }
    }
}