
namespace flashcards.domain.Requests
{
    public abstract class Request
    {
        protected readonly ICollection<string> Errors = [];
        public abstract bool IsValid();

        public ICollection<string> GetErrors()
        {
            return [.. Errors];
        }
    }
}