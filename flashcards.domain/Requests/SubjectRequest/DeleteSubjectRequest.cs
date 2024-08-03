namespace flashcards.domain.Requests.SubjectRequest
{
    public class DeleteSubjectRequest : Request
    {
        public long Id { get; set; }

        public DeleteSubjectRequest(long id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            if(Id <= 0) {
                Errors.Add("Invalid Id");
            }

            return Errors.Count <= 0;
        }
    }
}