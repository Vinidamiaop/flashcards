namespace flashcards.domain.Entities
{
    public class Question : Entitie
    {
        public string Text {get; set;} = string.Empty;
        public List<Answer> Answers = [];
        public int SubjectId {get; set;}
        public Subject Subject {get; set;} = null!;
    }
}