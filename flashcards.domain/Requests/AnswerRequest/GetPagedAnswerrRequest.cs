namespace flashcards.domain.Requests.AnswerRequest
{
    public class GetPagedAnswerRequest(int pageNumber, int pageSize) : PagedRequest(pageNumber, pageSize)
    {
        
    }
}