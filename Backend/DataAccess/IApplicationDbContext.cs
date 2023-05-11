namespace ACMEIndustries.Database
{
    public interface IApplicationDbContext
    {
        JsonDbContext Json { get; set; }

        Task SaveContextAsync();
    }
}