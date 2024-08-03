using flashcards.domain.Entities;
using flashcards.domain.Repositories;
using flashcards.domain.Requests.SubjectRequest;
using flashcards.domain.Responses;
using flashcards.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace flashcards.api.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _dbContext;
        public SubjectRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Response<Subject?>> CreateAsync(CreateSubjectRequest request)
        {
            try
            {
                var titleIsUnique = await SubjectTitleIsUnique(request.Title);
                if (!titleIsUnique)
                    return new Response<Subject?>(null, 400, null, ["Title must be unique"]);

                var subject = new Subject
                {
                    Title = request.Title,
                    Description = request.Description
                };

                await _dbContext.Subjects.AddAsync(subject);
                await _dbContext.SaveChangesAsync();

                return new Response<Subject?>(subject, 201, "Subject created successfully");
            }
            catch
            {
                return new Response<Subject?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<Response<Subject?>> DeleteAsync(DeleteSubjectRequest request)
        {
            try
            {
                var subject = await _dbContext.Subjects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (subject == null)
                    return new Response<Subject?>(null, 404, null, ["Subject not found"]);

                _dbContext.Subjects.Remove(subject);
                await _dbContext.SaveChangesAsync();
                return new Response<Subject?>(subject, 200, "Subject deleted successfully");
            }
            catch
            {
                return new Response<Subject?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<PagedResponse<List<Subject>>> GetAllAsync(GetPagedSubjectRequest request)
        {
            try
            {
                var query = _dbContext.Subjects
                .AsNoTracking()
                .OrderBy(x => x.Title);

                var totalCount = await query.CountAsync();
                var subjects = await query
                    .Skip((request.GetPageNumber() - 1) * request.GetPageSize())
                    .Take(request.GetPageSize())
                    .ToListAsync();


                return new PagedResponse<List<Subject>>(subjects, totalCount, request.GetPageNumber(), request.GetPageSize());
            }
            catch
            {
                return new PagedResponse<List<Subject>>(null, 500, null, ["Error finding subjects"]);
            }
        }

        public async Task<Response<Subject?>> GetByIdAsync(GetSubjectByIdRequest request)
        {
            try
            {
                var subject = await _dbContext.Subjects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (subject == null)
                    return new Response<Subject?>(null, 404, null, ["Subject not found"]);

                return new Response<Subject?>(subject, 200, "Ok");
            }
            catch
            {

                return new Response<Subject?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<Response<Subject?>> UpdateAsync(UpdateSubjectRequest request)
        {
            try
            {
                var titleIsUnique = await SubjectTitleIsUnique(request.Title, request.Id);
                if (!titleIsUnique)
                    return new Response<Subject?>(null, 400, null, ["Title must be unique"]);

                var subject = await _dbContext.Subjects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (subject == null)
                    return new Response<Subject?>(null, 404, null, ["Subject not found"]);

                subject.Title = request.Title;
                subject.Description = request.Description;




                _dbContext.Subjects.Update(subject);
                await _dbContext.SaveChangesAsync();

                return new Response<Subject?>(subject, 200, "Subject updated");
            }
            catch
            {
                return new Response<Subject?>(null, 500, null, ["Something went wrong"]);
            }
        }

        private async Task<bool> SubjectTitleIsUnique(string title, long? SubjectId = null)
        {
            var query = _dbContext.Subjects.Where(x => x.Title.Equals(title));

            if (SubjectId != null)
                query = query.Where(x => x.Id != SubjectId);

            return !await query.AnyAsync();
        }
    }
}