namespace WebLearn.Configurations;
using WebLearn.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnrollmentConfiguration:IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("enrollments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Grade)
            .HasPrecision(5, 2);  
      
        
        builder.HasIndex(e => new { e.StudentId, e.CourseId })
            .IsUnique();
    }
}