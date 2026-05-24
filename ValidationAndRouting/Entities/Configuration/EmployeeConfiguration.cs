using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValidationAndRouting.Models;

namespace ValidationAndRouting.Entities.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
            (
                new Employee
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                    Name = "John",
                    Age = 26,
                    Position = "Manager",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },

                new Employee
                {
                    Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                    Name = "Anna",
                    Age = 24,
                    Position = "Developer",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },

                new Employee
                {
                    Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    Name = "Tom",
                    Age = 30,
                    Position = "HR",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991880")
                }
            );
        }
    }
}