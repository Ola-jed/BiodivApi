namespace BiodivApi.Services.PaginatorService
{
    // Note that it is not really a service
    public record Paginator
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int Offset => (PageNumber - 1) * PageSize;
    }
}