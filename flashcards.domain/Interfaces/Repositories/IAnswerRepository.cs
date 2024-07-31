using flashcards.domain.Entities;
using flashcards.domain.Requests.AnswerRequest;
using flashcards.domain.Responses;

namespace flashcards.domain.Interfaces.Repositories
{
    public interface IAnswerRepository
    {
        Task<Response<Answer?>> CreateAsync(CreateAnswerRequest request);
        Task<Response<Answer?>> UpdateAsync(UpdateAnswerRequest request);
        Task<Response<Answer?>> DeleteAsync(DeleteAnwserRequest request);
        Task<Response<Answer?>> GetByIdAsync(GetAnswerByIdRequest request);
        Task<PagedResponse<Answer?>> GetAllAsync(GetPagedAnwserRequest request);
    }
}