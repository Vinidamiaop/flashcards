namespace flashcards.domain.Requests.QuestionRequest
{
    public class GetPagedQuestionRequest(int pageNumber, int pageSize) : PagedRequest(pageNumber, pageSize)
    {
        
    }
}