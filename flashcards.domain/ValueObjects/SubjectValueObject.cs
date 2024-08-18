using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flashcards.domain.Entities;

namespace flashcards.domain.ValueObjects
{
    public class SubjectValueObject
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<QuestionValueObject> Questions { get; set; } = [];
        public string Slug { get; set; } = string.Empty;

        public SubjectValueObject(long id, string title, string? description, List<QuestionValueObject> questions, string slug)
        {
            Id = id;
            Title = title;
            Description = description;
            Questions = questions;
            Slug = slug;
        }

        public static implicit operator SubjectValueObject(Subject subject)
        {
            List<QuestionValueObject> questions = [];
            questions.AddRange(subject.Questions.Select(q => (QuestionValueObject) q));

            return new SubjectValueObject(subject.Id, subject.Title, subject.Description, questions, subject.Slug);
        }
    }
}