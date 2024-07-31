using flashcards.domain.Entities;
using flashcards.domain.Requests.QuestionRequest;
using flashcards.domain.Responses;

namespace flashcards.domain.Interfaces
{
    public interface IQuestionRepository
    {
        Task<Response<Question?>> CreateAsync(CreateQuestionRequest request);
        Task<Response<Question?>> UpdateAsync(UpdateQuestionRequest request);
        Task<Response<Question?>> DeleteAsync(DeleteQuestionRequest request);
        Task<Response<Question?>> GetByIdAsync(GetQuestionByIdRequest request);
        Task<PagedResponse<Question?>> GetAllAsync(GetPagedQuestionRequest request);
    }
}