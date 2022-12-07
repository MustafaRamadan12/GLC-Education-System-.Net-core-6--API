using GLC.Core.ExtendUser;
using GLC.Cores.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GLC.EF
{
  public class GLCDbContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Group> Groups { set; get; }
    public DbSet<Video> Videos { set; get; }
    public DbSet<StudentQuizeQuestionBank> QuizeQuestions { set; get; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<QuestionCategory> questionCategories { set; get; }
    public DbSet<Quiz> Quizes { get; set; }
    public DbSet<QuestionBank> questionBanks { get; set; }
    public DbSet<ChatingDetails> ChatingDetails { set; get; }
    public DbSet<GroupChat> GroupChats { set; get; }

    public GLCDbContext(DbContextOptions<GLCDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.Entity<ChatingDetails>().HasKey(table => new
      {
        table.StId,
        table.GroupChatId,
        table.GroupId,
        table.TeacherId,
      });

      builder.Entity<StudentQuizeQuestionBank>().HasKey(table => new
      {
        table.StudentId,
        table.QuizeId,
        table.QuestionId
      });

      builder.Entity<Teacher>(s => s.Property(u => u.TeacherId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<Student>(s => s.Property(u => u.StudentId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<Subject>(s => s.Property(u => u.SubjectId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<Video>(s => s.Property(u => u.VideoId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<Quiz>(s => s.Property(u => u.QuizId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<QuestionBank>(s => s.Property(u => u.QuestionId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<QuestionAnswer>(s => s.Property(u => u.QuestionAnswerId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<GroupChat>(s => s.Property(u => u.GroupChatId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<Group>(s => s.Property(u => u.GroupId).HasDefaultValueSql("newsequentialid()"));
      builder.Entity<QuestionCategory>(s => s.Property(u => u.QuestionCategoryId).HasDefaultValueSql("newsequentialid()"));
    }

  }
}
