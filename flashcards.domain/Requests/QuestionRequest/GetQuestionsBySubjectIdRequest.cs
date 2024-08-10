namespace flashcards.domain.Requests.QuestionRequest
{
    public class GetQuestionsBySubjectIdRequest(string subjectSlug, int pageNumber, int pageSize) : PagedRequest(pageNumber, pageSize)
    {
        public string SubjectSlug { get; set; } = subjectSlug;

        public override bool IsValid()
        {
            if(String.IsNullOrWhiteSpace(SubjectSlug)) {
                Errors.Add("Invalid subject");
            }

            if(PageNumber <= 0) {
                Errors.Add("Invalid page");
            }

            if(PageSize <= 0) {
                Errors.Add("Invalid page size");
            }

            return Errors.Count <= 0;
        }
    }
}