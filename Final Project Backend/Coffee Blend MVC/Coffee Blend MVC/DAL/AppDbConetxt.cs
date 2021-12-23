using Coffee_Blend_MVC.Models;
using Coffee_Blend_MVC.Models.ServicesModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<HomeSlider> HomeSliders {get; set;}
        public DbSet<MainImage>  MainImages { get; set; }
        public DbSet<FtcoServices> FtcoServices { get; set; }
        public DbSet<OurMenu> OurMenus { get; set; }
        public DbSet<OurMenuImages> OurMenuImages { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<BestSellers> BestSellers { get; set; }
        public DbSet<BestSellersImage> BestSellersImages { get; set; }
        public DbSet<FtcoGallery2> FtcoGallery2s { get; set; }
        public DbSet<Testimony> Testimonies { get; set; }
        public DbSet<Recent> Recents { get; set; }
        public DbSet<RecentImage> RecentImages { get; set; }
        public DbSet<Discover> Discovers { get; set; }
        public DbSet<DiscoverImage> DiscoverImages { get; set; }

    }
}
