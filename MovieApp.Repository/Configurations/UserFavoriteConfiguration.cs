using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Repository.Configurations
{
    public class UserFavoriteConfiguration : IEntityTypeConfiguration<UserFavorities>
    {
        public void Configure(EntityTypeBuilder<UserFavorities> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MovieId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
