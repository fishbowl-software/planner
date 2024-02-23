using FishbowlSoftware.Planner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;

internal class ApplicationObjectEntityMap : IEntityTypeConfiguration<ApplicationObject>
{
    public void Configure(EntityTypeBuilder<ApplicationObject> builder)
    {
        builder.ToTable("ApplicationObjects");
    }
}
