using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static net_ef_videogame.Program;

namespace net_ef_videogame
{
    internal class VideogameManager
    {

        List<VideoGame> videoGames = new List<VideoGame>();
        List<Software_House> caseprod = GetSoftwareHouses();


        public void InsertNewGame()
        {

            // raccolta dati per creare videogioco

            Console.WriteLine("Primo passo per creare un videogioco: partiamo dalle caratteristiche");
            Console.WriteLine("Indica il nome del videogioco");
            string name = Console.ReadLine();
            while (name == null || name == "")
            {
                Console.WriteLine("Scegli un nome valido");
                name = Console.ReadLine();
            }
            Console.WriteLine("Descrivi brevemente il videogioco");
            string overview = Console.ReadLine();
            while (overview == null || overview == "")
            {
                Console.WriteLine("Scegli una descrizione valida valido");
                overview = Console.ReadLine();
            }
            Console.WriteLine("Indica la data di rilascio del videogioco (formato dd/mm/yyyy)");
            DateTime release;
            while (!DateTime.TryParse(Console.ReadLine(), out release))
                Console.WriteLine("Indica una data valida");
            DateTime created = DateTime.Now;
            DateTime updated = created;

            Software_House casaProd = SelectSoftwareHouse(caseprod);
            long software_house_id = casaProd.Id;

            using (VideoGamesDB db = new VideoGamesDB())
            {
                try
                {
                    VideoGame newGame = new VideoGame
                    {
                        Name = name,
                        Overview = overview,
                        Release = release,
                        Created = DateTime.UtcNow,
                        Updated = created,
                        Software_House = casaProd,
                    };
                    db.Add(newGame);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void SearchGameID()
        {
            Console.WriteLine("Digita l'ID del gioco che vuoi cercare");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Digita un numero valido.");

            for (int i = 0; i < this.videoGames.Count(); i++)
            {
                List<VideoGame> giochiId = new List<VideoGame>();
                if (videoGames[i].Id == Math.Abs(id))
                {
                    Console.WriteLine($"Il gioco cercato è: {videoGames[i].Name} del {videoGames[i].ReleaseDate}");
                    giochiId.Add(videoGames[i]);
                }
                if (giochiId == null)
                    Console.WriteLine($"Nessun gioco trovato con id pari a {id}");

            }


        }
        public void SearchGameName()
        {
            Console.WriteLine("Digita il nome del gioco che vuoi cercare (o parte di esso)");
            string name = Console.ReadLine();
            while (name == null || name == "")
            {
                Console.WriteLine("Assicurati di digitare almeno una lettera");
                name = Console.ReadLine();
            }
            for (int i = 0; i < this.videoGames.Count(); i++)
            {
                List<VideoGame> giochiId = new List<VideoGame>();
                if (videoGames[i].Name.Contains(name))
                {
                    Console.WriteLine($"Hai cercato {name}, contenuta in gioco n.{videoGames[i].Id} - {videoGames[i].Name}");
                    giochiId.Add(videoGames[i]);
                }
                if (giochiId == null)
                    Console.WriteLine($"Nessun gioco trovato contenente {name} nel nome");
            }

        }

        public List<VideoGame> GetVideoGame()
        {
            List < VideoGame > listaDB = new List < VideoGame >();
            using (VideoGamesDB db = new VideoGamesDB())
            {
                try
                {
                    listaDB = db.Videogames.OrderBy(videogames => videogames.Id).ToList<VideoGame>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return listaDB;
        }

        public static List<Software_House> GetSoftwareHouses()
        {
            
            List<Software_House> listaDB = new List<Software_House>();
            using (VideoGamesDB db = new VideoGamesDB())
            {
                try
                {
                    listaDB = db.Software_Houses.OrderBy(Software_Houses => Software_Houses.Name).ToList<Software_House>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return listaDB;
        }
        public Software_House SelectSoftwareHouse(List<Software_House> lista)
        {
            Software_House houseScelta = null;

            Console.WriteLine("Seleziona la casa di produzione del videogioco");
            foreach (Software_House a in lista)
                Console.WriteLine($"Numero: {a.Id} - {a.Name} - {a.Country}");
            int scelta;
            while (!int.TryParse(Console.ReadLine(), out scelta) && (scelta < 0 || scelta > 6))
                Console.WriteLine("Scegli un numero valido");
            switch (scelta)
            {
                case 1:
                    houseScelta = lista[0];
                    break;
                case 2:
                    houseScelta = lista[1];
                    break;
                case 3:
                    houseScelta = lista[2];
                    break;
                case 4:
                    houseScelta = lista[3];
                    break;
                case 5:
                    houseScelta = lista[4];
                    break;
                case 6:
                    houseScelta = lista[5];
                    break;
            }
            return houseScelta;
        }


        public void InsertNewSoftwareHouse()
        {
            //raccolta dati softwareHouse
            Console.WriteLine("Per creare una software house devi prima fornire tutti i dati necessari");
            Console.WriteLine("Indica il nome della Software_House");
            string name = Console.ReadLine();
            while(name == null || name == "")
            {
                Console.WriteLine("Indica un nome corretto");
                name = Console.ReadLine();
            }

            Console.WriteLine("Indica la partita IVA (VAT number) della Software_House (15 caratteri)");

            string vatNum = Console.ReadLine();
            while(vatNum == null || vatNum == "" || vatNum.Length < 15)
            {
                Console.WriteLine("Indica un VAT number valido. Ti ricordo che un VAT number ha 15 caratteri alfanumerici");
                vatNum = Console.ReadLine();
            }

            Console.WriteLine("Scrivi il nome della città in cui ha sede");
            string city = Console.ReadLine();
            while(city == null || city == "")
            {
                Console.WriteLine("Indica una città valida");
                city = Console.ReadLine();
            }

            Console.WriteLine("Scrivi ora lo stato");
            string country = Console.ReadLine();
            while (country == null || country == "")
            {
                Console.WriteLine("indica uno stato valido");
                country = Console.ReadLine();
            }

            DateTime created_at = DateTime.UtcNow;

            using (VideoGamesDB db = new VideoGamesDB())
            {
                try
                {
                    Software_House newSH = new Software_House
                    {
                        Name = name,
                        VATnum = vatNum,
                        City = city,
                        Country = country,
                        Created = created_at,
                        Updated = created_at,
                    };
                    db.Add(newSH);
                    db.SaveChanges();

                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        }
    }

    //DB creation

    public class Software_House
    {
        [Key] public int Id { get; private set; }
        [Column("name")] public string Name { get; set; }
        [Column("vat_num")] public string VATnum { get; set; }
        [Column("city")] public string City { get; set; }
        [Column("country")] public string Country { get; set; }
        [Column("created_at")] public DateTime Created { get; set; }
        [Column("updated_at")] public DateTime Updated { get; set; }

        public List<VideoGame> Videogames { get; set; }

        public Software_House() { }
        //public Software_House(string name, string vatnum, string city, string country)
        //{
        //    Name = name;
        //    VATnum = vatnum;
        //    City = city;
        //    Country = country;
        //    Created = DateTime.UtcNow;
        //    Updated = DateTime.UtcNow;
        //}
    }

    public class VideoGame
    {
        [Key] public int Id { get; private set; }
        [Column("name")] public string Name { get; set; }
        [Column("overview")] public string Overview { get; set; }
        [Column("release_date")] public DateTime Release { get; set; }
        [Column("created_at")] public DateTime Created { get; set; }
        [Column("updated_at")] public DateTime Updated { get; set; }
        //public int SoftwareHouseId { get; set; }
        public Software_House Software_House { get; set; }


        public VideoGame() { }
        //public VideoGame(string name, string overview, DateTime release, int softwarehouseid)
        //{
        //    Name = name;
        //    Overview = overview;
        //    Release = release;
        //    Created = DateTime.UtcNow;
        //    Updated = DateTime.UtcNow;
        //    SoftwareHouseId = softwarehouseid;
        //}
    }

    public class VideoGamesDB : DbContext
    {
        public DbSet<Software_House> Software_Houses { get; set; }
        public DbSet<VideoGame> Videogames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=new_videogame_DB;Integrated Security=True;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
