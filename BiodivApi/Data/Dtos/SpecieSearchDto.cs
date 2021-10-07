namespace BiodivApi.Data.Dtos
{
    public record SpecieSearchDto
    {
        public string SearchCriteria { get; init; }
        public string Search { get; init; }
    }
}