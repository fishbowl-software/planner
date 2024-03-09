using FishbowlSoftware.Planner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;

internal class ProjectEntityMap : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasMany(i => i.SurveySubmissions)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId);

        builder.HasMany(i => i.Applications)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Flows)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId);
    }
}
