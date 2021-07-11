using Application_Domain.UserConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application_Database.UserConfig
{
    public class rights_config : IEntityTypeConfiguration<rights_cls>
    {
        public void Configure(EntityTypeBuilder<rights_cls> builder)
        {
            builder.ToTable("tbl_righmaster");

            builder.Property(e => e.RightId)
                .HasColumnName("RightId")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.RightId);

            builder.Property(e => e.RoleId)
                .HasColumnName("RoleId")
                .IsRequired();

            builder.HasOne(e => e.RoleDetails)
           .WithMany(p => p.RightDetails)
           .HasForeignKey(e => e.RoleId)
           .OnDelete(DeleteBehavior.ClientNoAction)
           .HasConstraintName("FK_RoleRights_RoleId");

            builder.Property(e => e.MenuId)
             .HasColumnName("MenuId")
             .IsRequired();

            builder.Property(e => e.View)
                .HasColumnName("View")
                .IsRequired();

            builder.Property(e => e.Add)
                .HasColumnName("Add")
                .IsRequired();

            builder.Property(e => e.Edit)
                .HasColumnName("Edit")
                .IsRequired();

            builder.Property(e => e.Delete)
               .HasColumnName("Delete")
               .IsRequired();
        }
    }
}