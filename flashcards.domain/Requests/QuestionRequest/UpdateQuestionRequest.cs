using System.ComponentModel.DataAnnotations;
using flashcards.domain.Entities;

namespace flashcards.domain.Requests.QuestionRequest
{
    public class UpdateQuestionRequest : Request
    {
        [Required(ErrorMessage = "Invalid Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; } = string.Empty;

        public List<Answer> Answers = [];

        [Required(ErrorMessage = "Subject is required")]
        public Subject Subject { get; set; } = null!;
    }
}