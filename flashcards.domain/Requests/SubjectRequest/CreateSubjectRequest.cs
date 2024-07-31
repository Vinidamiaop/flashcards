using System.ComponentModel.DataAnnotations;
using flashcards.domain.Entities;

namespace flashcards.domain.Requests.SubjectRequest
{
    public class CreateSubjectRequest : Request
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Description should be at most 255 caracters long")]
        public string? Description { get; set; } = string.Empty;

        public List<Question> Questions { get; set; } = [];
    }
}