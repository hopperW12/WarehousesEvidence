namespace WarehousesEvidence.Data.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public DateTime BirthDate { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
