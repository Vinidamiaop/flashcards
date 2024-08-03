using flashcards.api.Interfaces.Endpoints;
using flashcards.domain;
using flashcards.domain.Entities;
using flashcards.domain.Repositories;
using flashcards.domain.Requests.AnswerRequest;
using flashcards.domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace flashcards.api.Endpoints.Answers
{
    public class AnswerEndpoints : IEntpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleGetAllAnswers)
                .WithName("Get paged answers")
                .WithDescription("Gets paged answers")
                .WithSummary("Gets all answers")
                .WithOrder(1)
                .Produces<Response<Answer?>>(StatusCodes.Status200OK)
                .Produces<Response<Answer?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Answer?>>(StatusCodes.Status500InternalServerError);

            app.MapGet("/{id}", HandleGetByIdAsync)
                .WithName("Get answer by id")
                .WithDescription("Gets answer by id")
                .WithSummary("Gets answer by id")
                .WithOrder(2)
                .Produces<Response<Answer?>>(StatusCodes.Status200OK)
                .Produces<Response<Answer?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Answer?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Answer?>>(StatusCodes.Status500InternalServerError);

            app.MapPost("/", HandleCreateAnswerAsync)
                .WithName("Create Answer")
                .WithDescription("Creates a new answer")
                .WithSummary("Creates a new answer")
                .WithOrder(3)
                .Produces<Response<Answer?>>(StatusCodes.Status201Created)
                .Produces<Response<Answer?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Answer?>>(StatusCodes.Status500InternalServerError);
            
            app.MapPut("/", HandleUpdateAnswerAsync)
                .WithName("Update Answer")
                .WithDescription("Updates answer")
                .WithSummary("Updates answer")
                .WithOrder(4)
                .Produces<Response<Answer?>>(StatusCodes.Status200OK)
                .Produces<Response<Answer?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Answer?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Answer?>>(StatusCodes.Status500InternalServerError);

            app.MapDelete("/{id}", HandleDeleteAnswer)
                .WithName("Delete Answer")
                .WithDescription("Deletes a answer")
                .WithSummary("Deletes a answer")
                .WithOrder(5)
                .Produces<Response<Answer?>>(StatusCodes.Status200OK)
                .Produces<Response<Answer?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Answer?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Answer?>>(StatusCodes.Status500InternalServerError);
        }

        private static async Task<IResult> HandleGetAllAnswers(
            IAnswerRepository repository,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
        {
            var request = new GetPagedAnswerRequest(pageNumber, pageSize);
            if(! request.IsValid()) {
                return TypedResults.Json<Response<List<Answer>?>>(new Response<List<Answer>?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }
            var result = await repository.GetAllAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<List<Answer>>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleCreateAnswerAsync(
            IAnswerRepository repository,
            CreateAnswerRequest request
        )
        {
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Answer?>>(new Response<Answer?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }

            var result = await repository.CreateAsync(request);            
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.Json<Response<Answer?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleDeleteAnswer(
            IAnswerRepository repository,
            [FromRoute] long id
        )
        {
            var request = new DeleteAnswerRequest(id);
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Answer?>>(new Response<Answer?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }
            var result = await repository.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Answer?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleGetByIdAsync(
            IAnswerRepository repository,
            [FromRoute] long id
        )
        {
            var request = new GetAnswerByIdRequest(id);
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Answer?>>(new Response<Answer?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }

            var result = await repository.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Answer?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleUpdateAnswerAsync(
            IAnswerRepository repository,
            UpdateAnswerRequest request
        )
        {
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Answer?>>(new Response<Answer?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }

            var result = await repository.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Answer?>>(result, statusCode: result.Code);
        }
    }
}