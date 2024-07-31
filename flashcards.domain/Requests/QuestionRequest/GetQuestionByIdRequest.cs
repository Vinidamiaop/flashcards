namespace flashcards.domain.Requests.QuestionRequest
{
    public class GetQuestionByIdRequest : Request
    {
        public long Id { get; set; }
    }
}