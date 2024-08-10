using flashcards.domain.Entities;
using flashcards.domain.Requests.AnswerRequest;
using flashcards.domain.Responses;

namespace flashcards.domain.Repositories
{
    public interface IAnswerRepository
    {
        Task<Response<Answer?>> CreateAsync(CreateAnswerRequest request);
        Task<Response<List<Answer>?>> CreateAsync(CreateManyAnswersRequest request);
        Task<Response<Answer?>> UpdateAsync(UpdateAnswerRequest request);
        Task<Response<Answer?>> DeleteAsync(DeleteAnswerRequest request);
        Task<Response<Answer?>> GetByIdAsync(GetAnswerByIdRequest request);
        Task<PagedResponse<List<Answer>>> GetAllAsync(GetPagedAnswerRequest request);
    }
}