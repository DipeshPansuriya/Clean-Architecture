using Application_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application_Database.Notificaion
{
    public class Notification_Config : IEntityTypeConfiguration<NotficationCls>
    {
        public void Configure(EntityTypeBuilder<NotficationCls> builder)
        {
            builder.ToTable("tbl_Notification");

            builder.Property(e => e.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.MsgType)
                .HasColumnName("MsgType")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.MsgFrom)
               .HasColumnName("MsgFrom")
               .HasMaxLength(200);

            builder.Property(e => e.MsgTo)
               .HasColumnName("MsgTo");

            builder.Property(e => e.MsgCC)
               .HasColumnName("MsgCC");

            builder.Property(e => e.MsgSubject)
               .HasColumnName("MsgSubject")
               .IsRequired();

            builder.Property(e => e.MsgBody)
               .HasColumnName("MsgBody")
               .IsRequired();

            builder.Property(e => e.MsgSatus)
             .HasColumnName("MsgSatus")
             .IsRequired()
             .HasMaxLength(50);

            builder.Property(e => e.FailDetails)
             .HasColumnName("FailDetails");
        }
    }
}