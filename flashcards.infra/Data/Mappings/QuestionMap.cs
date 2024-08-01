using flashcards.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace flashcards.infra.Data.Mappings
{
    public class QuestionMap : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Text)
                .IsRequired(true)
                .HasColumnName("Text")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(500);

            builder.Property(x => x.SubjectId)
                .IsRequired(true);
            
            builder.HasOne<Subject>(x => x.Subject)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany<Answer>(x => x.Answers)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);                
        }
    }
}