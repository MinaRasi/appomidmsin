using Microsoft.EntityFrameworkCore;
public class Context:DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder db)
    {
        db.UseSqlServer("data source=.;initial catalog = DBQuestions;integrated security=true");
    }
}