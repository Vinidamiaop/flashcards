using System.ComponentModel.DataAnnotations;
using flashcards.domain.Entities;

namespace flashcards.domain.Requests.AnswerRequest
{
    public class CreateAnswerRequest(string text, long questionId, bool isCorrect = false) : Request
    {
        public string Text { get; set; } = text;
        public bool IsCorrect { get; set; } = isCorrect;

        public long QuestionId { get; set; } = questionId;

        public override bool IsValid()
        {
            if (String.IsNullOrWhiteSpace(Text))
            {
                Errors.Add("Text is required");
            }

            if (QuestionId <= 0)
            {
                Errors.Add("Invalid questionId");
            }

            return Errors.Count <= 0;
        }
    }
}