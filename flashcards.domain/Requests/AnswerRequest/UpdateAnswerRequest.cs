using System.ComponentModel.DataAnnotations;
using flashcards.domain.Entities;

namespace flashcards.domain.Requests.AnswerRequest
{
    public class UpdateAnswerRequest : Request
    {
        [Required(ErrorMessage = "Invalid Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;

        [Required(ErrorMessage = "Question is required")]
        public Question Question = null!;
    }
}