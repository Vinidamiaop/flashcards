using System.ComponentModel.DataAnnotations;
using flashcards.domain.Entities;

namespace flashcards.domain.Requests.QuestionRequest
{
    public class CreateQuestionRequest : Request
    {
        public string Text { get; set; } = string.Empty;

        public List<Answer> Answers = [];

        public long SubjectId { get; set; }

        public override bool IsValid()
        {
            if(String.IsNullOrWhiteSpace(Text)) {
                Errors.Add("Text is required");
            }

            if(SubjectId <= 0) {
                Errors.Add("Invalid SubjectId");
            }

            return Errors.Count <= 0;
        }
    }
}