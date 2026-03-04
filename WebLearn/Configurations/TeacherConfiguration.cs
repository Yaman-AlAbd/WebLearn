namespace WebLearn.Configurations;
using WebLearn.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TeacherConfiguration:IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        
        builder.ToTable("teachers");

       
        builder.HasKey(t => t.Id);

        
        builder.Property(t => t.FullName)
            .IsRequired()       
            .HasMaxLength(100);  

        builder.Property(t => t.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.Phone)
            .HasMaxLength(20);

        builder.Property(t => t.Specialty)
            .IsRequired()
            .HasMaxLength(100);

     
        builder.HasIndex(t => t.Email)
            .IsUnique();

        
        builder.HasMany(t => t.Courses)    
            .WithOne(c => c.Teacher)          
            .HasForeignKey(c => c.TeacherId)  
            .OnDelete(DeleteBehavior.Restrict); 
    }
}