using System.ComponentModel.DataAnnotations;

namespace flashcards.domain.Requests.SubjectRequest
{
    public class CreateSubjectRequest : Request
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Description should be at most 255 caracters long")]
        public string? Description { get; set; } = string.Empty;

        public CreateSubjectRequest(string title, string? description)
        {
            Title = title;
            Description = description;
        }

        public override bool IsValid()
        {
            if(String.IsNullOrWhiteSpace(Title))
                Errors.Add("Title is required");

            if(Title?.Length > 500)
                Errors.Add($"Title should be at most 255 caracters long. {Title.Length} caracteres sent.");

            if(Description?.Length > 255)
                Errors.Add($"Description should be at most 255 caracters long. {Description.Length} caracteres sent.");
                
            return Errors.Count <= 0;
        }
    }
}