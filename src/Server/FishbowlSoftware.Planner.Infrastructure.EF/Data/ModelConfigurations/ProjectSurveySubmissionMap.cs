using FishbowlSoftware.Planner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;

public class ProjectSurveySubmissionMap : IEntityTypeConfiguration<ProjectSurveySubmission>
{
    public void Configure(EntityTypeBuilder<ProjectSurveySubmission> builder)
    {
        builder.ToTable("ProjectSurveySubmissions");
        
        builder.HasOne(i => i.Survey)
            .WithMany()
            .HasForeignKey(i => i.SurveyId);
    }
}
