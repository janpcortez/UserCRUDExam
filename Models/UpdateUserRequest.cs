namespace UserCRUDExam.Models
{
    public class UpdateUserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? ContactNumber { get; set; }
    }
}
