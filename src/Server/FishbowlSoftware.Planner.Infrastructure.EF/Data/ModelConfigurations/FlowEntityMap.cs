using FishbowlSoftware.Planner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;

internal class FlowEntityMap : IEntityTypeConfiguration<Flow>
{
    public void Configure(EntityTypeBuilder<Flow> builder)
    {
        builder.ToTable("Flows");

        builder.HasMany(i => i.UserStories)
            .WithOne(i => i.Flow)
            .HasForeignKey(i => i.FlowId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
