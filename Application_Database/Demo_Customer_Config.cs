using Application_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application_Database
{
    public class Demo_Customer_Config : IEntityTypeConfiguration<Demo_Customer>
    {
        public void Configure(EntityTypeBuilder<Demo_Customer> builder)
        {
            builder.ToTable("Demo_Customer");

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code)
                .HasColumnName("Code")
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}