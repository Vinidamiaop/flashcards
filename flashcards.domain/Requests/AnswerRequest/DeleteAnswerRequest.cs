namespace flashcards.domain.Requests.AnswerRequest
{
    public class DeleteAnswerRequest(long id) : Request
    {
        public long Id { get; set; } = id;

        public override bool IsValid()
        {
            if (Id <= 0)
            {
                Errors.Add("Invalid Id");
            }

            return Errors.Count <= 0;
        }
    }
}