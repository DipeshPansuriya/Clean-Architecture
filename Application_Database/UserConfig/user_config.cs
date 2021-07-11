using Application_Domain.UserConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application_Database.UserConfig
{
    public class user_config : IEntityTypeConfiguration<User_cls>
    {
        public void Configure(EntityTypeBuilder<User_cls> builder)
        {
            builder.ToTable("tbl_usermaster");

            builder.Property(e => e.UserId)
                .HasColumnName("UserId")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.UserId);

            builder.Property(e => e.EmailId)
                .HasColumnName("EmailId")
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Password)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.UserName)
                .HasColumnName("UserName")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();
        }
    }
}