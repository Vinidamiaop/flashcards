using flashcards.domain.Entities;
using flashcards.domain.Requests.SubjectRequest;
using flashcards.domain.Responses;

namespace flashcards.domain.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Response<Subject?>> CreateAsync(CreateSubjectRequest request);
        Task<Response<Subject?>> UpdateAsync(UpdateSubjectRequest request);
        Task<Response<Subject?>> DeleteAsync(DeleteSubjectRequest request);
        Task<Response<Subject?>> GetByIdAsync(GetSubjectByIdRequest request);
        Task<PagedResponse<Subject?>> GetAllAsync(GetPagedSubjectRequest request);
    }
}