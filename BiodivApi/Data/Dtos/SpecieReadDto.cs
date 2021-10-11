namespace BiodivApi.Data.Dtos
{
    public record SpecieReadDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string TaxonomicGroup { get; init; }
        public string ScientificName { get; init; }
    }
}