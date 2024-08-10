using flashcards.domain.Entities;
using flashcards.domain.Requests.SubjectRequest;
using flashcards.domain.Responses;

namespace flashcards.domain.Repositories
{
    public interface ISubjectRepository
    {
        Task<Response<Subject?>> CreateAsync(CreateSubjectRequest request);
        Task<Response<Subject?>> CreateWithQuestionsAsync(CreateSubjectWithQuestionRequest request);
        Task<Response<Subject?>> UpdateAsync(UpdateSubjectRequest request);
        Task<Response<Subject?>> DeleteAsync(DeleteSubjectRequest request);
        Task<Response<Subject?>> GetByIdAsync(GetSubjectByIdRequest request);
        Task<PagedResponse<List<Subject>>> GetAllAsync(GetPagedSubjectRequest request);
    }
}