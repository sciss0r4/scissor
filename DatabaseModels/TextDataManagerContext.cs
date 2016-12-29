using System.Data.Entity;

namespace DatabaseModels
{
    public class TextDataManagerContext : DbContext
    {
        public DbSet<FileRepository> FileRepositories { get; set; }
        public DbSet<TiffFile> TiffFiles { get; set; }
        public DbSet<OcrFile> OcrFiles { get; set; }
        public DbSet<IndexRepository> IndexRepositories { get; set; }
        public DbSet<Index> Indexes { get; set; }
        public DbSet<IndexDocument> IndexDocuments { get; set; }
    }
}
