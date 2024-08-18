using flashcards.domain.Entities;

namespace flashcards.domain.Requests.AnswerRequest
{
    public class DeleteAnswersRequest(List<Answer> answers) : Request
    {
        public List<Answer> Answers { get; set; } = answers;

        public override bool IsValid()
        {
            if (Answers.Count <= 0)
            {
                Errors.Add("Answers not found");
            }

            return Errors.Count <= 0;
        }
    }
}