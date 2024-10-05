namespace CarRentalSystem.Application.Bases
{
    public class BaseFilteration
    {
        public string[] OrderBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string? SearchText { get; set; }
    }
}
