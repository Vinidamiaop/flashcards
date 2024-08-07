namespace flashcards.domain.Requests.AnswerRequest
{
    public class UpdateAnswerRequest(long id, string text, bool isCorrect) : Request
    {
        public long Id { get; set; } = id;
        public string Text { get; set; } = text;
        public bool IsCorrect { get; set; } = isCorrect;

        public override bool IsValid()
        {
            if (Id <= 0)
                Errors.Add("Invalid Id");


            return Errors.Count <= 0;
        }
    }
}