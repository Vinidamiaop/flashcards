using System.ComponentModel.DataAnnotations;
using flashcards.domain.Entities;

namespace flashcards.domain.Requests.QuestionRequest
{
    public class CreateManyQuestionsRequest : Request
    {
        public List<Question> Questions {get; set;} = [];

        public long SubjectId { get; set; }

        public CreateManyQuestionsRequest(long subjectId, List<Question> questions)
        {
            SubjectId = subjectId;
            Questions = questions;
        }

        public override bool IsValid()
        {
            if(SubjectId <= 0) {
                Errors.Add("Invalid SubjectId");
            }

            foreach(var question in Questions) {
                if(String.IsNullOrWhiteSpace(question.Text))
                    Errors.Add("Text is required");
            }

            return Errors.Count <= 0;
        }
    }
}