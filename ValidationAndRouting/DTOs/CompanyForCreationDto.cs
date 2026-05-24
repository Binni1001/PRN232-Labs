namespace ValidationAndRouting.DTOs
{
    public class CompanyForCreationDto : CompanyForManipulationDto
    {
        public IEnumerable<EmployeeForCreationDto> Employees
        {
            get;
            set;
        }
    }
}
