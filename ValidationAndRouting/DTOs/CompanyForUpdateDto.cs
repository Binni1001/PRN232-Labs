namespace ValidationAndRouting.DTOs
{
    public class CompanyForUpdateDto : CompanyForManipulationDto
    {
        public IEnumerable<EmployeeForCreationDto> Employees
        {
            get;
            set;
        }
    }
}
