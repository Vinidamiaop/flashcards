using flashcards.domain.Entities;

namespace flashcards.domain.Requests.SubjectRequest
{
    public class UpdateSubjectWithQuestionRequest : Request
    {
        public long SubjectId {get; set;}
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<Question> Questions {get; set;} = [];
        public List<Question> DeletedQuestions {get; set;} = [];
        public List<Answer> DeletedAnswers {get; set;} = [];

        public UpdateSubjectWithQuestionRequest(long subjectId, string title, string? description, List<Question> questions, List<Question> deletedQuestions, List<Answer> deletedAnswers)
        {
            SubjectId = subjectId;
            Title = title;
            Description = description;
            Questions = questions;
            DeletedQuestions = deletedQuestions;
            DeletedAnswers = deletedAnswers;
        }

        public override bool IsValid()
        {
            if(String.IsNullOrWhiteSpace(Title))
                Errors.Add("Title is required");

            if(SubjectId <= 0)
                Errors.Add("Invalid subject id");

            if(Title?.Length > 500)
                Errors.Add($"Title should be at most 255 caracters long. {Title.Length} caracteres sent.");

            if(Description?.Length > 255)
                Errors.Add($"Description should be at most 255 caracters long. {Description.Length} caracteres sent.");
                
            return Errors.Count <= 0;
        }
    }
}