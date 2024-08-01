using flashcards.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace flashcards.infra.Data.Mappings
{
    public class AnswerMap : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answer");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            
            builder.Property(x => x.Text)
                .IsRequired(true)
                .HasColumnName("Text")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(500);
            
            builder.Property(x => x.IsCorrect)
                .HasDefaultValue(false);

            builder.Property(x => x.QuestionId)
                .IsRequired(true);

            builder.HasOne<Question>(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}