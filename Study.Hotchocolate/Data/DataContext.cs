namespace Study.HotChocolate.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var user = modelBuilder.Entity<User>();
        user.HasKey(_ => _.Id);
        user.Property(_ => _.Name).HasMaxLength(255);
        user.Property(_ => _.Email).HasMaxLength(255);
        user.OwnsOne(_ => _.Address, one =>
        {
            one.ToJson();
            one.Property(_ => _.AdCode).HasMaxLength(255);
            one.Property(_ => _.Detail).HasMaxLength(255);
        });
        user.OwnsOne(_ => _.State, one =>
        {
            one.ToJson();
            one.Property(_ => _.Remark).HasMaxLength(255);
        });
        user.HasMany(_ => _.Posts).WithOne(_ => _.User).HasForeignKey(_ => _.User_Id);

        var post = modelBuilder.Entity<Post>();
        post.HasKey(_ => _.Id);
        post.Property(_ => _.Title).HasMaxLength(255);
        post.Property(_ => _.Content).HasMaxLength(255);
        post.HasOne(_ => _.User).WithMany(_ => _.Posts).HasForeignKey(_ => _.User_Id);
    }
}
