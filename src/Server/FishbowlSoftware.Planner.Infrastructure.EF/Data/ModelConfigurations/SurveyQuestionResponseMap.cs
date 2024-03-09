using FishbowlSoftware.Planner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;

public class SurveyQuestionResponseMap : IEntityTypeConfiguration<SurveyQuestionResponse>
{
    public void Configure(EntityTypeBuilder<SurveyQuestionResponse> builder)
    {
        builder.ToTable("SurveyQuestionResponses");

        builder.HasOne(i => i.Question)
            .WithMany()
            .HasForeignKey(i => i.QuestionId);

        builder.HasOne(i => i.Option)
            .WithMany()
            .HasForeignKey(i => i.OptionId);
    }
}
