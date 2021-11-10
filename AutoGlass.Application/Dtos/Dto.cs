namespace AutoGlass.Application.Dtos
{
    public abstract class Dto
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}