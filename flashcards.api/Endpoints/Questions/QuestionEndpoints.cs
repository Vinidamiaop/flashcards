using flashcards.api.Interfaces.Endpoints;
using flashcards.domain;
using flashcards.domain.Entities;
using flashcards.domain.Repositories;
using flashcards.domain.Requests.QuestionRequest;
using flashcards.domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace flashcards.api.Endpoints.Questions
{
    public class QuestionEndpoints : IEntpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleGetAllQuestions)
                .WithName("Get paged questions")
                .WithDescription("Gets paged questions")
                .WithSummary("Gets all questions")
                .WithOrder(1)
                .Produces<Response<Question?>>(StatusCodes.Status200OK)
                .Produces<Response<Question?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Question?>>(StatusCodes.Status500InternalServerError);

            app.MapGet("/{id}", HandleGetByIdAsync)
                .WithName("Get question by id")
                .WithDescription("Gets question by id")
                .WithSummary("Gets question by id")
                .WithOrder(2)
                .Produces<Response<Question?>>(StatusCodes.Status200OK)
                .Produces<Response<Question?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Question?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Question?>>(StatusCodes.Status500InternalServerError);

            app.MapPost("/", HandleCreateQuestionAsync)
                .WithName("Create Question")
                .WithDescription("Creates a new question")
                .WithSummary("Creates a new question")
                .WithOrder(3)
                .Produces<Response<Question?>>(StatusCodes.Status201Created)
                .Produces<Response<Question?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Question?>>(StatusCodes.Status500InternalServerError);
            
            app.MapPut("/", HandleUpdateQuestionAsync)
                .WithName("Update Question")
                .WithDescription("Updates question")
                .WithSummary("Updates question")
                .WithOrder(4)
                .Produces<Response<Question?>>(StatusCodes.Status200OK)
                .Produces<Response<Question?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Question?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Question?>>(StatusCodes.Status500InternalServerError);

            app.MapDelete("/{id}", HandleDeleteQuestion)
                .WithName("Delete Question")
                .WithDescription("Deletes a question")
                .WithSummary("Deletes a question")
                .WithOrder(5)
                .Produces<Response<Question?>>(StatusCodes.Status200OK)
                .Produces<Response<Question?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Question?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Question?>>(StatusCodes.Status500InternalServerError);
        }

        private static async Task<IResult> HandleGetAllQuestions(
            IQuestionRepository repository,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
        {
            var request = new GetPagedQuestionRequest(pageNumber, pageSize);
            if(! request.IsValid()) {
                return TypedResults.Json<Response<List<Question>?>>(new Response<List<Question>?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }
            var result = await repository.GetAllAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<List<Question>>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleCreateQuestionAsync(
            IQuestionRepository repository,
            CreateQuestionRequest request
        )
        {
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Question?>>(new Response<Question?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }

            var result = await repository.CreateAsync(request);            
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.Json<Response<Question?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleDeleteQuestion(
            IQuestionRepository repository,
            [FromRoute] long id
        )
        {
            var request = new DeleteQuestionRequest(id);
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Question?>>(new Response<Question?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }
            var result = await repository.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Question?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleGetByIdAsync(
            IQuestionRepository repository,
            [FromRoute] long id
        )
        {
            var request = new GetQuestionByIdRequest(id);
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Question?>>(new Response<Question?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }

            var result = await repository.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Question?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleUpdateQuestionAsync(
            IQuestionRepository repository,
            UpdateQuestionRequest request
        )
        {
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Question?>>(new Response<Question?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }

            var result = await repository.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Question?>>(result, statusCode: result.Code);
        }
    }
}