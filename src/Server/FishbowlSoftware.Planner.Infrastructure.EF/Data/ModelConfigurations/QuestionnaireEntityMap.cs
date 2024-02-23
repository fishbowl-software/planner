using FishbowlSoftware.Planner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;

internal class QuestionnaireEntityMap : IEntityTypeConfiguration<Questionnaire>
{
    public void Configure(EntityTypeBuilder<Questionnaire> builder)
    {
        builder.ToTable("Questionnaires");

        builder.HasOne(i => i.Project)
            .WithOne(i => i.Questionnaire)
            .HasForeignKey<Questionnaire>(i => i.ProjectId);

        builder.HasMany(i => i.Questions)
            .WithOne(i => i.Questionnaire)
            .HasForeignKey(i => i.QuestionnaireId);
    }
}
