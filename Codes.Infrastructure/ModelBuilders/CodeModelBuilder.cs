using Codes.Domain.Entities;
using Codes.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Codes.Infrastructure.ModelBuilders
{
    public static class CodeModelBuilder
    {
        public static void BuildCodeModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Code>()
                .HasKey(x => x.Id);
        
            modelBuilder.Entity<Code>()
                .Property(x => x.Id).ValueGeneratedOnAdd()
                .IsRequired();
        
            modelBuilder
                .Entity<Code>()
                .Property(x => x.CodeType)
                .HasConversion(new EnumToStringConverter<CodeType>());
        }
    }
}