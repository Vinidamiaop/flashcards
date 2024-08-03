namespace flashcards.domain.Requests.SubjectRequest
{
    public class GetSubjectByIdRequest : Request
    {
        public long Id { get; set; }

        public override bool IsValid()
        {
            if(Id <= 0) {
                Errors.Add("Invalid Id");
            }

            return Errors.Count <= 0;
        }
    }
}