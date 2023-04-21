using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_ef_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }

        public class Software_House
        {
            [Key] public int Id { get; private set; }
            [Column("name")] public string Name { get; set; }
            [Column("tax_id")] public string TaxId { get; set; }
            [Column("city")] public string City { get; set; }
            [Column("country")] public string Country { get; set; }
            [Column("created_at")] public DateTime Created { get; set; }
            [Column("updated_at")] public DateTime Updated { get; set; }

            public List<VideoGame> Videogames { get; set; }


            public Software_House(string name, string taxid, string city, string country)
            {
                Name = name;
                TaxId = taxid;
                City = city;
                Country = country;
                Created = DateTime.UtcNow;
                Updated = DateTime.UtcNow;
            }
        }

        public class VideoGame
        {
            [Key] public int Id { get; private set; }
            [Column("name")] public string Name { get; set; }
            [Column("overview")] public string Overview { get; set; }
            [Column("release_date")] public DateTime Release { get; set; }
            [Column("created_at")] public DateTime Created { get; set; }
            [Column("updated_at")] public DateTime Updated { get; set; }
            [Column("software_house_id")] public int SoftwareHouseId { get; set; }
            public Software_House Software_House { get; set; }

            public VideoGame(string name, string overview, DateTime release, int softwarehouseid)
            {
                Name = name;
                Overview = overview;
                Release = release;
                Created = DateTime.UtcNow;
                Updated = DateTime.UtcNow;
                SoftwareHouseId = softwarehouseid;
            }
        }

        public class VideoGames : DbContext
        {
            public DbSet<Software_House> Software_Houses { get; set;}
            public DbSet<VideoGame> Videogames { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=new_videogame_DB;Integrated Security=True");
                base.OnConfiguring(optionsBuilder);
            }
        }
}