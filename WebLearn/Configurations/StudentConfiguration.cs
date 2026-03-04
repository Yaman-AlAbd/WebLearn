namespace WebLearn.Configurations;
using WebLearn.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class StudentConfiguration:IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Phone)
            .HasMaxLength(20);

        builder.HasIndex(s => s.Email)
            .IsUnique();
        
        builder.HasMany(s => s.Enrollments)
            .WithOne(e => e.Student)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}