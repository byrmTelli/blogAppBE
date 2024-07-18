using blogAppBE.CORE.DBModels;
using Microsoft.EntityFrameworkCore;

namespace blogAppBE.DAL.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(){}
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Your_Db_ConnectionString");
        }

        #region DbSets
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
            .HasOne(post => post.PostCategory)
            .WithMany(category => category.Posts)
            .HasForeignKey(post => post.CategoryId);
        }


    }
}