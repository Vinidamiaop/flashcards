using System.Net.Mime;
using flashcards.api.Interfaces.Endpoints;
using flashcards.domain;
using flashcards.domain.Entities;
using flashcards.domain.Repositories;
using flashcards.domain.Requests.SubjectRequest;
using flashcards.domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace flashcards.api.Endpoints.Subjects
{
    public class SubjectEndpoints : IEntpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleGetAllSubjects)
                .WithName("Get paged subjects")
                .WithDescription("Gets paged subjects")
                .WithSummary("Gets all subjects")
                .WithOrder(1)
                .Produces<Response<Subject?>>(StatusCodes.Status200OK)
                .Produces<Response<Subject?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Subject?>>(StatusCodes.Status500InternalServerError);

            app.MapGet("/{id}", HandleGetByIdAsync)
                .WithName("Get subject by id")
                .WithDescription("Gets subject by id")
                .WithSummary("Gets subject by id")
                .WithOrder(2)
                .Produces<Response<Subject?>>(StatusCodes.Status200OK)
                .Produces<Response<Subject?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Subject?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Subject?>>(StatusCodes.Status500InternalServerError);

            app.MapPost("/", HandleCreateSubjectAsync)
                .WithName("Create Subject")
                .WithDescription("Creates a new subject")
                .WithSummary("Creates a new subject")
                .WithOrder(3)
                .Produces<Response<Subject?>>(StatusCodes.Status201Created)
                .Produces<Response<Subject?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Subject?>>(StatusCodes.Status500InternalServerError);
            
            // app.MapPost("/", HandleCreateSubjectWithQuestionAsync)
            //     .WithName("Create Subject with questions")
            //     .WithDescription("Creates a new subject with questions")
            //     .WithSummary("Creates a new subject with questions")
            //     .WithOrder(3)
            //     .Produces<Response<Subject?>>(StatusCodes.Status201Created)
            //     .Produces<Response<Subject?>>(StatusCodes.Status400BadRequest)
            //     .Produces<Response<Subject?>>(StatusCodes.Status500InternalServerError);
            
            app.MapPut("/", HandleUpdateSubjectAsync)
                .WithName("Update Subject")
                .WithDescription("Updates subject")
                .WithSummary("Updates subject")
                .WithOrder(4)
                .Produces<Response<Subject?>>(StatusCodes.Status200OK)
                .Produces<Response<Subject?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Subject?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Subject?>>(StatusCodes.Status500InternalServerError);

            app.MapDelete("/{id}", HandleDeleteSubject)
                .WithName("Delete Subject")
                .WithDescription("Deletes a subject")
                .WithSummary("Deletes a subject")
                .WithOrder(5)
                .Produces<Response<Subject?>>(StatusCodes.Status200OK)
                .Produces<Response<Subject?>>(StatusCodes.Status400BadRequest)
                .Produces<Response<Subject?>>(StatusCodes.Status404NotFound)
                .Produces<Response<Subject?>>(StatusCodes.Status500InternalServerError);
        }

        private static async Task<IResult> HandleGetAllSubjects(
            ISubjectRepository repository,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
        {
            var request = new GetPagedSubjectRequest(pageNumber, pageSize);
            if(! request.IsValid()) {
                return TypedResults.Json<Response<List<Subject>?>>(new Response<List<Subject>?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }
            var result = await repository.GetAllAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<List<Subject>>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleCreateSubjectAsync(
            ISubjectRepository repository,
            CreateSubjectWithQuestionRequest request
        )
        {
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Subject?>>(new Response<Subject?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }

            var result = await repository.CreateWithQuestionsAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.Json<Response<Subject?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleDeleteSubject(
            ISubjectRepository repository,
            [FromRoute] long id
        )
        {
            var request = new DeleteSubjectRequest(id);
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Subject?>>(new Response<Subject?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }
            var result = await repository.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Subject?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleGetByIdAsync(
            ISubjectRepository repository,
            [FromRoute] long id
        )
        {
            var request = new GetSubjectByIdRequest()
            {
                Id = id,
            };

            if(! request.IsValid()) {
                return TypedResults.Json<Response<Subject?>>(new Response<Subject?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }

            var result = await repository.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Subject?>>(result, statusCode: result.Code);
        }

        private static async Task<IResult> HandleUpdateSubjectAsync(
            ISubjectRepository repository,
            UpdateSubjectRequest request
        )
        {
            if(! request.IsValid()) {
                return TypedResults.Json<Response<Subject?>>(new Response<Subject?>(null, 400, null, request.GetErrors()), statusCode: StatusCodes.Status400BadRequest);
            }
            var result = await repository.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Json<Response<Subject?>>(result, statusCode: result.Code);
        }
    }
}