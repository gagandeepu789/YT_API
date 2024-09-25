using Microsoft.EntityFrameworkCore;
using YT_VillaApi.Models;

namespace YT_VillaApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {
            
        }
        public DbSet<Villa> Villas { get; set; }//villas is a tablename which will be created after migration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Name",
                    ImageUrl = "",
                    Occupancy = 3,
                    Rate = 4,
                    sqft = 5,
                    Amenity = "",
                    Details="djsgjfgsdjgfsdjjsjsg"
                }
                );
        }
    }
}
