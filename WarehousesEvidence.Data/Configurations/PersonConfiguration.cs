using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehousesEvidence.Core.Entities;

namespace WarehousesEvidence.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasData(
                new Person {
                    PersonId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateTime(1990, 1, 1),
                    PhoneNumber = 1434567890,
                    Email = "john.doe@gmail.com" 
                },
                new Person
                {
                    PersonId = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    BirthDate = new DateTime(1995, 1, 1),
                    PhoneNumber = 1233567890,
                    Email = "jane.doe@gmail.com"
                },
                new Person
                {
                    PersonId = 3,
                    FirstName = "Alice",
                    LastName = "Smith",
                    BirthDate = new DateTime(1985, 1, 1),
                    PhoneNumber = 1234568890,
                    Email = "alice.smith@gmail.com"
                });
        }
    }
}
