using FriendsData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.EntityConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(evt => evt.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(_ => _.Members)
                .WithMany(_ => _.Events);

            builder.HasOne(_ => _.Organizator)
                .WithMany();


        }
    }
}
