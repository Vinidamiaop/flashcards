using System.Text.Json.Serialization;
using flashcards.api;
using flashcards.api.Endpoints;
using flashcards.api.Repositories;
using flashcards.domain.Repositories;
using flashcards.infra.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options => {
    options.AddPolicy(name: ApiConfiguration.DEFAULT_CORS, policy => {
        policy.WithOrigins(ApiConfiguration.FRONTEND_URL)
            .AllowAnyHeader()
            .AllowAnyMethod();
        //policy.AllowAnyOrigin()
        //    .AllowAnyHeader()
        //    .AllowAnyMethod();
    } );
});

builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<IAnswerRepository, AnswerRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(ApiConfiguration.DEFAULT_CORS);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
