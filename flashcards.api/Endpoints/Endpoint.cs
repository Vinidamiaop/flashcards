using flashcards.api.Endpoints.Answers;
using flashcards.api.Endpoints.Questions;
using flashcards.api.Endpoints.Subjects;
using flashcards.api.Interfaces.Endpoints;

namespace flashcards.api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app) 
        {
            var endpoints = app.MapGroup("v1/");

            endpoints.MapGroup("/")
                .WithTags("Health check")
                .WithOrder(1)
                .MapGet("/", () => new { message = "Ok" })
                .WithDisplayName("Health Check")
                .WithSummary("Checks system's health")
                .WithDescription("Health Check");
            
            endpoints.MapGroup("/subjects")
                .WithTags("Subjects")
                .WithOrder(2)
                .MapEndpoint<SubjectEndpoints>();
            
            endpoints.MapGroup("/questions")
                .WithTags("Questions")
                .WithOrder(3)
                .MapEndpoint<QuestionEndpoints>();
            
            endpoints.MapGroup("/answers")
                .WithTags("Answers")
                .WithOrder(4)
                .MapEndpoint<AnswerEndpoints>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEntpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}