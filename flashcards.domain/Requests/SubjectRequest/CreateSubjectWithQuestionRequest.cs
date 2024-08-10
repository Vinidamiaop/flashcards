using flashcards.domain.Entities;

namespace flashcards.domain.Requests.SubjectRequest
{
    public class CreateSubjectWithQuestionRequest : Request
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public List<Question> Questions {get; set;} = [];

        public CreateSubjectWithQuestionRequest(string title, string? description, List<Question> questions)
        {
            Title = title;
            Description = description;
            Questions = questions;
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