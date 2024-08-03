using System.ComponentModel.DataAnnotations;
using flashcards.domain.Entities;

namespace flashcards.domain.Requests.QuestionRequest
{
    public class UpdateQuestionRequest : Request
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public override bool IsValid()
        {
            if(Id <= 0) {
                Errors.Add("Invalid Id");
            }


            return Errors.Count <= 0;
        }
    }
}