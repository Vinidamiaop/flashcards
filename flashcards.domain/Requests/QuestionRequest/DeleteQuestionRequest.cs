namespace flashcards.domain.Requests.QuestionRequest
{
    public class DeleteQuestionRequest(long id) : Request
    {
        public long Id { get; set; } = id;

        public override bool IsValid()
        {
            if(Id <= 0) {
                Errors.Add("Invalid Id");
            }

            return Errors.Count <= 0;
        }
    }
}