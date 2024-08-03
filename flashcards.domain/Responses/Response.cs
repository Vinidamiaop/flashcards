using System.Text.Json.Serialization;

namespace flashcards.domain.Responses
{
    public class Response<TData>
    {
        public readonly int Code;
        public TData? Data {get; set;}
        public string? Message {get; set;}
        public ICollection<string>? Errors {get; set;}

        [JsonConstructor]
        public Response() => Code = Configuration.DefaultStatusCode;

        public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null, ICollection<string>? errors = null)
        {
            Data = data;
            Message = message;
            Code = code;
            Errors = errors; 
        }

        [JsonIgnore]
        public bool IsSuccess => Code is >= 200 and <= 299;
    }
}