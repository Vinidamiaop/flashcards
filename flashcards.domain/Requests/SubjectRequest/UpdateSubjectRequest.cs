using System.ComponentModel.DataAnnotations;
using flashcards.domain.Entities;

namespace flashcards.domain.Requests.SubjectRequest
{
    public class UpdateSubjectRequest : Request
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public override bool IsValid()
        {
            if(Id <= 0) {
                Errors.Add("Invalid Id");
            }

            if(String.IsNullOrWhiteSpace(Title)) {
                Errors.Add("Title is required");
            }

            if(Title?.Length > 500)
                Errors.Add($"Title should be at most 255 caracters long. {Title.Length} caracteres sent.");

            if(Description?.Length > 255) {
                Errors.Add($"Description should be at most 255 caracters long. {Description.Length} caracteres sent.");
            }

            return Errors.Count <= 0;
        }
    }
}