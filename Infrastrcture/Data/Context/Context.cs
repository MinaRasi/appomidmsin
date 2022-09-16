using Microsoft.EntityFrameworkCore;
public class Context:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<MainQuestion> MainQuestions { get; set; }
    public DbSet<Answer> answers { get; set; }
    public DbSet<Points> Pointes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder db)
    {
        db.UseSqlServer("data source=.;initial catalog = DBQuestions;integrated security=true");
    }
}