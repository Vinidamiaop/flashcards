using flashcards.domain.Entities;
using flashcards.domain.Repositories;
using flashcards.domain.Requests.QuestionRequest;
using flashcards.domain.Responses;
using flashcards.domain.ValueObjects;
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

                var question = new Question(request.Text, request.SubjectId, []);

                await _dbContext.Questions.AddAsync(question);
                await _dbContext.SaveChangesAsync();

                return new Response<Question?>(question, 201, "Question created");
            }
            catch
            {
                return new Response<Question?>(null, 500, null, ["Something went wrong"]);
            }
        }

        public async Task<Response<List<Question>?>> CreateAsync(CreateManyQuestionsRequest request)
        {
            try
            {
                var subject = await _dbContext.Subjects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.SubjectId);

                if (subject == null)
                    return new Response<List<Question>?>(null, 400, null, ["Subject doesn't exists"]);

                if (request.Questions.Count <= 0)
                    return new Response<List<Question>?>(null, 200, "Empty questions list");

                var questionsToAdd = new List<Question>();
                foreach (var question in request.Questions ?? Enumerable.Empty<Question>())
                {
                    question.SubjectId = subject.Id;
                    questionsToAdd.Add(question);
                }

                await _dbContext.Questions.AddRangeAsync(questionsToAdd);
                await _dbContext.SaveChangesAsync();

                return new Response<List<Question>?>(questionsToAdd, 201, "Question created");
            }
            catch
            {
                return new Response<List<Question>?>(null, 500, null, ["Something went wrong"]);
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

        public async Task<PagedResponse<List<QuestionValueObject>>> GetBySubjectIdAsync(GetQuestionsBySubjectIdRequest request)
        {
            try
            {
                var query = _dbContext.Questions
                    .AsNoTracking()
                    .Where(x => x.Subject.Slug == request.SubjectSlug)
                    .OrderBy(x => Guid.NewGuid());

                var totalCount = await query.CountAsync();
                var anonymousQuestions = await query
                    .Skip((request.GetPageNumber() - 1) * request.GetPageSize())
                    .Take(request.GetPageSize())
                    .Select(q => new
                    {
                        q.Id,
                        q.Text,
                        q.SubjectId,
                        q.Subject,
                        Answers = q.Answers.Select(a => new
                        {
                            a.Id,
                            a.Text,
                            a.QuestionId,
                            a.IsChecked
                        }).ToList()
                    })
                    .ToListAsync();

                var questions = anonymousQuestions.Select(q => new QuestionValueObject
                {
                    Id = q.Id,
                    Text = q.Text,
                    SubjectId = q.SubjectId,
                    Subject = q.Subject,
                    Answers = q.Answers.Select(a => new AnswerValueObject
                    {
                        Id = a.Id,
                        Text = a.Text,
                        QuestionId = a.QuestionId,
                        IsChecked = a.IsChecked
                    }).ToList()
                }).ToList();

                return new PagedResponse<List<QuestionValueObject>>(questions, totalCount, request.GetPageNumber(), request.GetPageSize());
            }
            catch
            {
                return new PagedResponse<List<QuestionValueObject>>(null, 500, null, ["Error finding subjects"]);
            }
        }

        public async Task<Response<CorrectedQuestion>> QuestionCorrectionAsync(QuestionCorrectionRequest request)
        {
            try
            {
                var totalQuestions = request.Questions.Count;
                var questionsIds = request.Questions.Select(x => x.Id).ToList();
                var questions = await _dbContext.Questions
                    .AsNoTracking()
                    .Where(x => questionsIds.Contains(x.Id))
                    .Include(x => x.Answers)
                    .ToListAsync();

                List<Question> correctQuestions = [];
                List<Question> incorrectQuestions = [];
                foreach (var question in questions)
                {
                    var currentQuestion = request.Questions.FirstOrDefault(x => x.Id == question.Id);
                    var checkedAnswers = currentQuestion?.Answers.Where(x => x.IsChecked).ToList();

                    foreach (var answer in question.Answers)
                    {
                        answer.IsChecked = currentQuestion?.Answers?.FirstOrDefault(x => x.Id == answer.Id)?.IsChecked ?? false;
                    }
                    if (question.IsCorrect())
                    {
                        correctQuestions.Add(question);
                    }
                    else
                    {
                        incorrectQuestions.Add(question);
                    }
                }

                Double score = (double)correctQuestions.Count / totalQuestions * 100;
                int correctTotal = correctQuestions.Count;

                CorrectedQuestion correctedQuestions = new(score, totalQuestions, correctTotal, incorrectQuestions, questions);

                return new Response<CorrectedQuestion>(correctedQuestions);
            }
            catch
            {
                return new Response<CorrectedQuestion>(null, 500, null, ["Something went wrong"]);
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