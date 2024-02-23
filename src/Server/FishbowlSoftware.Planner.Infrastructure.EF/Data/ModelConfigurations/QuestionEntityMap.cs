using FishbowlSoftware.Planner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;

internal class QuestionEntityMap : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasMany(i => i.Options)
            .WithOne(i => i.Question)
            .HasForeignKey(i => i.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
