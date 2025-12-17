using Microsoft.EntityFrameworkCore;
using SurveyApi.Models;

namespace SurveyApi.Data;

public class SurveyDbContext : DbContext
{
    public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
        : base(options) { }

    public DbSet<QuestionCategory> QuestionCategories => Set<QuestionCategory>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Survey> Surveys => Set<Survey>();
    public DbSet<Answer> Answers => Set<Answer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionCategory>(entity =>
        {
            entity.ToTable("question_category");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).HasMaxLength(10).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("question");
            entity.HasKey(e => e.Id);
        
            entity.Property(e => e.Text)
                  .HasColumnName("text")
                  .IsRequired();
        
            entity.Property(e => e.MinValue)
                  .HasColumnName("min_value")
                  .IsRequired();
        
            entity.Property(e => e.MaxValue)
                  .HasColumnName("max_value")
                  .IsRequired();
        
            entity.Property(e => e.IsActive)
                  .HasColumnName("is_active")
                  .HasDefaultValue(true);
        
            entity.Property(e => e.CategoryId)
                  .HasColumnName("category_id");
        
            entity.HasOne(e => e.Category)
                .WithMany(c => c.Questions)
                .HasForeignKey(e => e.CategoryId);
        });


        modelBuilder.Entity<Survey>(entity =>
        {
            entity.ToTable("survey");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("answer");
        
            entity.HasKey(e => e.Id);
        
            entity.Property(e => e.Id)
                .HasColumnName("id");
        
            entity.Property(e => e.SurveyId)
                .HasColumnName("survey_id");
        
            entity.Property(e => e.QuestionId)
                .HasColumnName("question_id");
        
            entity.Property(e => e.Value)
                .HasColumnName("value");
        
            entity.HasOne(e => e.Survey)
                .WithMany(s => s.Answers)
                .HasForeignKey(e => e.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }
}
