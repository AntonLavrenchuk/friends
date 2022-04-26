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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(_ => _.Events)
                .WithMany(_ => _.Members)
                .UsingEntity(_ => _.ToTable("dbo.EventsUsers"));

            
        }
    }
}
