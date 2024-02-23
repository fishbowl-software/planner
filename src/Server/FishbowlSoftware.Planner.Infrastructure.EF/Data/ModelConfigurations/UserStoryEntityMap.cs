using FishbowlSoftware.Planner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;

internal class UserStoryEntityMap : IEntityTypeConfiguration<UserStory>
{
    public void Configure(EntityTypeBuilder<UserStory> builder)
    {
        builder.ToTable("UserStories");

        builder.HasMany(i => i.ApplicationObjects)
            .WithOne(i => i.UserStory)
            .HasForeignKey(i => i.UserStoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
