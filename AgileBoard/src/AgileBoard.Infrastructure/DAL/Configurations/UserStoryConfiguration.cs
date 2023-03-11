using AgileBoard.Core.Domain;
using AgileBoard.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileBoard.Infrastructure.DAL.Configurations
{
    internal sealed class UserStoryConfiguration : IEntityTypeConfiguration<UserStory>
    {
        public void Configure(EntityTypeBuilder<UserStory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new UserStoryId(x));

            builder.Property(x => x.Title)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Title(x));

            builder.Property(x => x.Owner)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Owner(x));

            builder.Property(x => x.Description)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Description(x));

            builder.Property(x => x.AcceptanceCriteria)
                .IsRequired()
                .HasConversion(x => x.Value, x => new AcceptanceCriteria(x));

            builder.Property(x => x.DefinitionOfDone)
                .IsRequired()
                .HasConversion(x => x.Value, x => new DefinitionOfDone(x));

            builder.Property(x => x.DefinitionOfReady)
                .IsRequired()
                .HasConversion(x => x.Value, x => new DefinitionOfReady(x));

            builder.Property(x => x.Comment)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Comment(x));

            builder.Property(x => x.StoryPoints)
                .IsRequired()
                .HasConversion(x => x.Value, x => new(x));

            builder.Property(x => x.Priority)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Priority(x));

            builder.Property(x => x.Risk)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Risk(x));

            builder.Property(x => x.Deadline)
                .IsRequired()
                .HasConversion(x => x.Value, x => new(x));

            builder.Property(x => x.Created)
                .IsRequired()
                .HasConversion(x => x.Value, x => new(x));

            builder.Property(x => x.Updated)
                .IsRequired()
                .HasConversion(x => x.Value, x => new(x));
        }
    }
}
