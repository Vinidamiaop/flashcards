namespace flashcards.domain.Entities
{
    public class Question : Entitie
    {
        public string Text { get; set; } = string.Empty;
        public List<Answer> Answers { get; set; } = [];
        public long SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public Question() { }
        public Question(string text, long subjectId, List<Answer> answers)
        {
            Text = text;
            SubjectId = subjectId;
            Answers = answers;
        }

        public bool IsCorrect()
        {
            if (!Answers.Any(x => x.IsChecked)) return false;

            if (Answers.Any(x => x.IsChecked && !x.IsCorrect)) return false;

            return true;
        }
    }
}