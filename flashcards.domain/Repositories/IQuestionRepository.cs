using flashcards.domain.Entities;
using flashcards.domain.Requests.QuestionRequest;
using flashcards.domain.Responses;
using flashcards.domain.ValueObjects;

namespace flashcards.domain.Repositories
{
    public interface IQuestionRepository
    {
        Task<Response<Question?>> CreateAsync(CreateQuestionRequest request);
        Task<Response<List<Question>?>> CreateAsync(CreateManyQuestionsRequest request);
        Task<Response<CorrectedQuestion>> QuestionCorrectionAsync(QuestionCorrectionRequest request);
        Task<Response<Question?>> UpdateAsync(UpdateQuestionRequest request);
        Task<Response<Question?>> DeleteAsync(DeleteQuestionRequest request);
        Task<Response<Question?>> GetByIdAsync(GetQuestionByIdRequest request);
        Task<PagedResponse<List<Question>>> GetAllAsync(GetPagedQuestionRequest request);
        Task<PagedResponse<List<QuestionValueObject>>> GetBySubjectIdAsync(GetQuestionsBySubjectIdRequest request);
    }
}