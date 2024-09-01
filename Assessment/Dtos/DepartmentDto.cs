namespace Assessment.Dtos
{
    public class DepartmentDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}