using flashcards.domain.Entities;

namespace flashcards.domain.Requests.QuestionRequest
{
    public class DeleteQuestionsRequest(List<Question> questions) : Request
    {
        public List<Question> Questions {get; set;} = questions;

        public override bool IsValid()
        {
            if(Questions.Count <= 0) {
                Errors.Add("Questions not found");
            }

            return Errors.Count <= 0;
        }
    }
}