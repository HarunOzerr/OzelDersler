using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OzelDersler.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDersler.Data.Config
{
    public class BranchConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.BranchName).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Url).IsRequired().HasMaxLength(250);
            builder.Property(b => b.ImageUrl).HasMaxLength(250);

            builder.ToTable("Branches");

            builder.HasData
                (
                    new Branch
                    {
                        Id= 1,
                         BranchName="Matematik",
                          Url="matematik",
                          ImageUrl="mat.png"
                    },
                     new Branch
                     {
                         Id = 2,
                         BranchName = "Kimya",
                         Url = "kimya",
                         ImageUrl = "kimya.png"
                     }, new Branch
                     {
                         Id = 3,
                         BranchName = "İngilizce",
                         Url = "ingilizce",
                         ImageUrl = "ing.png"
                     }, new Branch
                     {
                         Id = 4,
                         BranchName = "Müzik",
                         Url = "muzik",
                         ImageUrl = "muzik.png"
                     }, new Branch
                     {
                         Id = 5,
                         BranchName = "Türkçe",
                         Url = "turkce",
                         ImageUrl = "turkce.png"
                     }, new Branch
                     {
                         Id = 6,
                         BranchName = "Bilgisayar",
                         Url = "bilgisayar",
                         ImageUrl = "bilgisayar.png"
                     }
                );
        }
    }
}
