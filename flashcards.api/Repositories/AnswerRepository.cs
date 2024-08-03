using flashcards.domain.Entities;
using flashcards.domain.Repositories;
using flashcards.domain.Requests.AnswerRequest;
using flashcards.domain.Responses;
using flashcards.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace flashcards.api.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _dbContext;
        public AnswerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Response<Answer?>> CreateAsync(CreateAnswerRequest request)
        {
            try
            {
                var question = await _dbContext.Questions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.QuestionId);

                if (question == null)
                    return new Response<Answer?>(null, 400, null, ["Question doesn't exists"]);

                var task = new Answer
                {
                    Text = request.Text,
                    QuestionId = request.QuestionId,
                    IsCorrect = request.IsCorrect
                };

                await _dbContext.Answers.AddAsync(task);
                await _dbContext.SaveChangesAsync();

                return new Response<Answer?>(task, 201, "Answer created");
            }
            catch
            {
                return new Response<Answer?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<Response<Answer?>> DeleteAsync(DeleteAnswerRequest request)
        {
            try
            {
                var task = await _dbContext.Answers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (task == null)
                    return new Response<Answer?>(null, 404, null, ["Answer not found"]);

                _dbContext.Answers.Remove(task);
                await _dbContext.SaveChangesAsync();
                return new Response<Answer?>(task, 200, null, ["Answer deleted"]);
            }
            catch
            {
                return new Response<Answer?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<PagedResponse<List<Answer>>> GetAllAsync(GetPagedAnswerRequest request)
        {
            try
            {
                var query = _dbContext.Answers
                .AsNoTracking();

                var totalCount = await query.CountAsync();
                var tasks = await query
                    .Skip((request.GetPageNumber() - 1) * request.GetPageSize())
                    .Take(request.GetPageSize())
                    .ToListAsync();


                return new PagedResponse<List<Answer>>(tasks, totalCount, request.GetPageNumber(), request.GetPageSize());
            }
            catch
            {
                return new PagedResponse<List<Answer>>(null, 500, null, ["Error finding questions"]);
            }
        }

        public async Task<Response<Answer?>> GetByIdAsync(GetAnswerByIdRequest request)
        {
            try
            {
                var task = await _dbContext.Answers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (task == null)
                    return new Response<Answer?>(null, 404, null, ["Answer not found"]);

                return new Response<Answer?>(task, 200, "Ok");
            }
            catch
            {

                return new Response<Answer?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<Response<Answer?>> UpdateAsync(UpdateAnswerRequest request)
        {
            try
            {
                var task = await _dbContext.Answers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (task == null)
                    return new Response<Answer?>(null, 404, null, ["Answer not found"]);

                task.Text = request.Text;
                task.IsCorrect = request.IsCorrect;

                _dbContext.Answers.Update(task);
                await _dbContext.SaveChangesAsync();

                return new Response<Answer?>(task, 200, "Answer updated");
            }
            catch
            {
                return new Response<Answer?>(null, 500, null, ["Something went wrong"]);
            }
        }
    }
}