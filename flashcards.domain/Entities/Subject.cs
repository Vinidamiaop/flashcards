using flashcards.domain.Utils;

namespace flashcards.domain.Entities
{
    public class Subject : Entitie
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = [];
        public string Slug { get; set; } = string.Empty;

        public Subject()
        {
            Slug = Slugfy.GenerateSlug(Title);
        }

        public Subject(string title, List<Question> questions, string? description)
        {
            Title = title;
            Questions = questions;
            Description = description;
            Slug = Slugfy.GenerateSlug(Title);
        }
    }
}