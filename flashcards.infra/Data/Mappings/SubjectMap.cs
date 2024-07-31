using flashcards.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace flashcards.infra.Data.Mappings
{
    public class SubjectMap : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Title)                
                .IsRequired(true)                         
                .HasColumnName("Title")
                .HasColumnType("TEXT");

            builder.HasIndex(x => x.Title, "IX_Subject_Title").IsUnique();

            builder.Property(x => x.Description)                
            .IsRequired(false)                         
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);


            builder.HasMany<Question>(x => x.Questions)
                .WithOne(x => x.Subject)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}