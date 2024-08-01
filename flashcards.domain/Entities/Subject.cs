namespace flashcards.domain.Entities
{
    public class Subject : Entitie
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = [];

        public Subject() {

        }
    }
}