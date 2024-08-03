using flashcards.domain.Entities;
using flashcards.domain.Repositories;
using flashcards.domain.Requests.QuestionRequest;
using flashcards.domain.Responses;
using flashcards.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace flashcards.api.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _dbContext;
        public QuestionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Response<Question?>> CreateAsync(CreateQuestionRequest request)
        {
            try
            {
                var subject = await _dbContext.Subjects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.SubjectId);

                if (subject == null)
                    return new Response<Question?>(null, 400, null, ["Subject doesn't exists"]);

                var question = new Question
                {
                    Text = request.Text,
                    SubjectId = request.SubjectId
                };

                await _dbContext.Questions.AddAsync(question);
                await _dbContext.SaveChangesAsync();

                return new Response<Question?>(question, 201, "Question created");
            }
            catch
            {
                return new Response<Question?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<Response<Question?>> DeleteAsync(DeleteQuestionRequest request)
        {
            try
            {
                var question = await _dbContext.Questions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (question == null)
                    return new Response<Question?>(null, 404, null, ["Question not found"]);

                _dbContext.Questions.Remove(question);
                await _dbContext.SaveChangesAsync();
                return new Response<Question?>(question, 200, null, ["Question deleted"]);
            }
            catch
            {
                return new Response<Question?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<PagedResponse<List<Question>>> GetAllAsync(GetPagedQuestionRequest request)
        {
            try
            {
                var query = _dbContext.Questions
                .AsNoTracking();

                var totalCount = await query.CountAsync();
                var questions = await query
                    .Skip((request.GetPageNumber() - 1) * request.GetPageSize())
                    .Take(request.GetPageSize())
                    .ToListAsync();


                return new PagedResponse<List<Question>>(questions, totalCount, request.GetPageNumber(), request.GetPageSize());
            }
            catch
            {
                return new PagedResponse<List<Question>>(null, 500, null, ["Error finding subjects"]);
            }
        }

        public async Task<Response<Question?>> GetByIdAsync(GetQuestionByIdRequest request)
        {
            try
            {
                var question = await _dbContext.Questions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (question == null)
                    return new Response<Question?>(null, 404, null, ["Question not found"]);

                return new Response<Question?>(question, 200, "Ok");
            }
            catch
            {

                return new Response<Question?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<Response<Question?>> UpdateAsync(UpdateQuestionRequest request)
        {
            try
            {
                var question = await _dbContext.Questions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (question == null)
                    return new Response<Question?>(null, 404, null, ["Question not found"]);

                question.Text = request.Text;

                _dbContext.Questions.Update(question);
                await _dbContext.SaveChangesAsync();

                return new Response<Question?>(question, 200, "Question updated");
            }
            catch
            {
                return new Response<Question?>(null, 500, null, ["Something went wrong"]);
            }
        }
    }
}