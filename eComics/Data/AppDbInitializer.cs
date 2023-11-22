using eComics.Data.Enums;
using eComics.Models;


namespace eComics.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            { 
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Publisher
                if (!context.Publishers.Any())
                {
                    context.Publishers.AddRange(new List<Publisher>()
                    { 
                        new Publisher() 
                        {
                           Logo = "https://upload.wikimedia.org/wikipedia/en/thumb/f/f8/Dark_Horse_Comics_logo.svg/640px-Dark_Horse_Comics_logo.svg.png",
                           Name = "Dark Horse Comics",
                           Description = "Dark Horse Comics is an American comic book, graphic novel, and manga publisher founded in Milwaukie, " +
                           "Oregon by Mike Richardson in 1986. The company was created using funds earned from Richardson's chain of Portland, Oregon, " +
                           "comic book shops known as Pegasus Books and founded in 1980."
                        },
                        new Publisher()
                        {
                           Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/71/Marvel-Comics-Logo.svg/2560px-Marvel-Comics-Logo.svg.png",
                           Name = "Marvel Comics",
                           Description = "Marvel Comics is an American comic book series published by Marvel Entertainment Inc. Marvel's " +
                            "best-known comics include The Fantastic Four, Spider-Man, The Hulk, Iron Man, Captain America ​​and The X-Men. " +
                            "Most of the Marvel-created of characters are set in a world known as the Marvel Universe."
                        },
                        new Publisher()
                        {
                           Logo = "https://static1.srcdn.com/wordpress/wp-content/uploads/2020/03/DC-Comics-Logo.jpg",
                           Name = "DC Comics",
                           Description = "DC Comics is a comic book division of DC Entertainment founded in 1934. DC is " +
                           "owned by Warner Brothers, which in turn is owned by Time Warner. It is one of the largest comic " +
                           "book producers worldwide, next to Marvel Comics. DC Comics' best-known superheroes include Superman, Batman, and Wonder Woman."
                        },
                        new Publisher()
                        {
                           Logo = "https://upload.wikimedia.org/wikipedia/en/thumb/1/1d/Action_Lab_Comics_Logo.jpg/220px-Action_Lab_Comics_Logo.jpg",
                           Name = "Action Lab Comics",
                           Description = "Action Lab Entertainment is an American comic book publisher best known for publishing the all-ages fantasy " +
                           "series Printsless and a wide variety of titles from all genres."
                        },
                        new Publisher()
                        {
                           Logo = "https://upload.wikimedia.org/wikipedia/commons/e/ea/Updated_BOOM%21_logo%2C_fair_use.jpg",
                           Name = "BOOM Studios",
                           Description = "BOOM Studios is an American publisher of comics and graphic novels headquartered in Los Angeles, California."
                        },
                        new Publisher()
                        {
                           Logo = "https://upload.wikimedia.org/wikipedia/en/8/82/Fantagraphics_logo_2020.png",
                           Name = "Fantagraphics",
                           Description = "Fantagraphics (previously Fantagraphics Books) is an American publisher of alternative comics, classic comic " +
                           "strip anthologies, manga, magazines, graphic novels, and the erotic Eros Comix imprint."
                        },
                        new Publisher()
                        {
                           Logo = "https://upload.wikimedia.org/wikipedia/commons/a/aa/DC_Vertigo_Logo.png",
                           Name = "Vertigo Comics",
                           Description = "Vertigo Comics (also known as DC Vertigo or simply Vertigo) was an imprint of American " +
                           "comic book publisher DC Comics started by editor Karen Berger in 1993. "
                        },
                        new Publisher()
                        {
                           Logo = "https://www.anbmedia.com/wp-content/uploads/2020/03/kodansha-comics-logo-1024x780-1024x780.jpg",
                           Name = "Kodansha Comics",
                           Description = "Kodansha publishes the manga magazines Nakayoshi, Afternoon, Evening, Weekly Shōnen Magazine " +
                           "and Bessatsu Shōnen Magazine, as well as the more literary magazines Gunzō, Shūkan Gendai, and the " +
                           "Japanese dictionary Nihongo Daijiten."
                        }
                    });
                    context.SaveChanges();
                }
                //Artist
                if (!context.Artists.Any())
                {
                    context.Artists.AddRange(new List<Artist>()
                    {
                        new Artist()
                        {
                            FullName = "James Harren",
                            Bio = "James Harren is the writer/artist of ULTRAMEGA, his creator-owned series from Image Comics.",
                            ProfilePictureURL = "https://static.wikia.nocookie.net/marveldatabase/images/2/2e/James_Harren.jpg/revision/latest?cb=20170510132034"
                        },
                          new Artist()
                        {
                            FullName = "Mike Norton",
                            Bio = "Mike Norton is an American comic book artist and writer, known for his work on Battlepug.",
                            ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/b/b5/10.16.11MikeNortonByLuigiNovi1.jpg"
                        },
                          new Artist()
                        {
                            FullName = "Davide Tinto",
                            Bio = "Davide Tinto is a comic book artist that worked on Star Wars Adventures: The Clone Wars – Battle Tales.",
                            ProfilePictureURL = "https://static.wikia.nocookie.net/marveldatabase/images/a/a2/Davide_Tinto.jpg/revision/latest/thumbnail/width/360/height/360?cb=20191226183505"
                        },
                          new Artist()
                        {
                            FullName = "Marco Checchetto",
                            Bio = "Marco Checchetto is one of the most appreciated Italian comic book artist in the US.",
                            ProfilePictureURL = "https://www.lospaziobianco.it/wp-content/uploads/2021/02/Comics_Ospiti_MarcoChecchettoINTERNA-1.jpg"
                        },
                          new Artist()
                        {
                            FullName = "Doug Mahnke",
                            Bio = "Douglas Mahnke is an American comic book artist, known for his work and penciling books including The Mask, JLA, Batman, Final Crisis, and Green Lantern.",
                            ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/Doug_Mahnke%2C_February_2010.jpg/440px-Doug_Mahnke%2C_February_2010.jpg"
                        },
                          new Artist()
                        {
                            FullName = "Davi Leon Dias",
                            Bio = "Comic book illustrator and cover artist.",
                            ProfilePictureURL = "https://media.licdn.com/dms/image/C4E03AQFI-3brkQMOyA/profile-displayphoto-shrink_200_200/0/1641912522386?e=1700697600&v=beta&t=kkdcdirSGXFiXHDwYxPWSkBcLXJ16eFLYTRw6kgwW1I"
                        },
                          new Artist()
                        {
                            FullName = "German Garcia",
                            Bio = "Comic book author.",
                            ProfilePictureURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT_i0aPEB5MpWaQStnnri_QUGX4umDmibNCCsHQQZV9Q9878xfVqR45sFdvN33ix0xhjPM&usqp=CAU"
                        },
                          new Artist()
                        {
                            FullName = "Junggeun Yoon",
                            Bio = "Freelancer Illustrator and concept Artist",
                            ProfilePictureURL = "https://art.sideshow.com/wp/wp-content/uploads/sites/4/2022/06/junggeunyoon-thumb2.jpg"
                        },
                          new Artist(){
                           ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Daniel_Clowes._37_Comic_Barcelona.jpg/440px-Daniel_Clowes._37_Comic_Barcelona.jpg",
                            FullName = "Daniel Clowes",
                            Bio = "Daniel Gillespie Clowes (/klaʊz/; born April 14, 1961) is an American cartoonist, graphic novelist, illustrator, and screenwriter."

                          },
                          new Artist(){
                           ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/17/CCXP_Cologne_2019_Artists%27_Alley_David_Lloyd.jpg/440px-CCXP_Cologne_2019_Artists%27_Alley_David_Lloyd.jpg",
                            FullName = "David Lloyd",
                            Bio = "David Lloyd is an English comics artist best known as the illustrator of the story V for Vendetta, written by Alan Moore."
                          },
                          new Artist()
                          {
                            ProfilePictureURL = "https://www.animenewsnetwork.com/images/cms/news.6/200477/bloodblade01.jpg",
                            FullName = "Oma Sei",
                            Bio = "Oma Sei's debut solo work is BLOOD BLADE, being released now by Kodansha Originals."
                          }
                    });
                    context.SaveChanges();
                }
                //Writer
                if (!context.Writers.Any())
                {
                    context.Writers.AddRange(new List<Writer>() {
                        new Writer()
                        {
                            ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0f/MikeMignolaJune2011.jpg/640px-MikeMignolaJune2011.jpg",
                            FullName = "Mike Mignola",
                            Bio = "Michael Mignola is an American comic book artist and writer best known for creating Hellboy for Dark Horse Comics."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/10.10.10JohnArcudiByLuigiNovi1.jpg/440px-10.10.10JohnArcudiByLuigiNovi1.jpg",
                            FullName = "John Arcudi",
                            Bio = "John Arcudi is an American comic book writer, best known for his work on The Mask and B.P.R.D. and his series Major Bummer."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/49/Chris_Roberson_by_Gage_Skidmore.jpg/440px-Chris_Roberson_by_Gage_Skidmore.jpg",
                            FullName = "Chris Roberson",
                            Bio = "John Christian Roberson known professionally as Chris Roberson, is an American science fiction author " +
                            "and publisher who is best known for alternate history novels and short stories."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://www.bobafettfanclub.com/multimedia/galleries/albums/userpics/10001/ethan-sacks-0-1635923279.jpeg",
                            FullName = "Ethan Sacks",
                            Bio = "Ethan Sacks is a comic book writer who wrote The Weapon which was published by Marvel Comics in the 2019 issue Age of Republic Special 1."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://cdn.marvel.com/u/prod/marvel/i/mg/7/10/5cd9c7870670e/standard_incredible.jpg",
                            FullName = "Jason Aaron",
                            Bio = "Jason Aaron is an American comic book writer, known for his creator-owned series Scalped and Southern Bastards."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://pbs.twimg.com/profile_images/1532404794/Twit_Pic_400x400.jpg",
                            FullName = "Rylend Grant",
                            Bio = "Rylend Grant is a screenwriter, author, and Ringo Award-winning comic book creator from Detroit, MI."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcSk31gD0t7mDDLnItxsNxyZ5JZVivJmghWOCmWIYaMeyklHspaF",
                            FullName = "Christopher Cantwell",
                            Bio = "Christopher Cantwell is an American writer, producer, and director who has worked in television, film, and comic books."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1d/Daniel_Clowes._37_Comic_Barcelona.jpg/440px-Daniel_Clowes._37_Comic_Barcelona.jpg",
                            FullName = "Daniel Clowes",
                            Bio = "Daniel Gillespie Clowes, born April 14, 1961) is an American cartoonist, graphic novelist, illustrator, and screenwriter."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2d/Alan_Moore_%282%29.jpg/440px-Alan_Moore_%282%29.jpg",
                            FullName = "Alan Moore",
                            Bio = "Alan Moore (born 18 November 1953) is an English author known primarily for his work in comic books including Watchmen, V for Vendetta, The Ballad of Halo Jones, Swamp Thing, Batman: The Killing Joke, and From Hell."
                        },
                        new Writer()
                        {
                            ProfilePictureURL = "https://www.animenewsnetwork.com/images/cms/news.6/200477/bloodblade01.jpg",
                            FullName = "Oma Sei",
                            Bio = "Oma Sei's debut solo work is BLOOD BLADE, being released now by Kodansha Originals."
                        },
                    });
                    context.SaveChanges();
                }
                //Book
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Title = "B.P.R.D. OMNIBUS VOLUME 8 TPB",
                            Description = "The B.P.R.D. continues to lead the defense against the apocalyptic Ogdru Hem from Japan to America, as the team splits up and Kate is possessed.",
                            Price = 12.99,
                            ImageURL = "https://d2lzb5v10mb0lj.cloudfront.net/covers/300/30/3009928.jpg",
                            ReleaseDate = DateTime.Now.AddDays(10),
                            BookGenre = BookGenre.Horror,
                            PublisherId = 1
                        },
                         new Book()
                        {
                            Title = "Star Wars: Bounty Hunters (2020) #40",
                            Description = "THE SHOCKING BETRAYAL! - A DARK DROIDS TIE-IN! The Galaxy's deadliest BOUNTY HUNTERS must stop a corrupted VALANCE!",
                            Price = 22.99,
                            ImageURL = "https://cdn.marvel.com/u/prod/marvel/i/mg/f/e0/654baff90fce8/detail.jpg",
                            ReleaseDate = DateTime.Now.AddDays(-4),
                            BookGenre = BookGenre.SciFi,
                            PublisherId = 2
                        },
                          new Book()
                        {
                            Title = "BATMAN: OFF-WORLD #1",
                            Description = "A routine night in Gotham City for a young Batman proves to be anything but routine when the crime-fighter is confronted with a sort of foe he’s never faced before.",
                            Price = 15.99,
                            ImageURL = "https://static.dc.com/2023-11/BMOW_Cv1_00111_DIGITAL.jpg?w=640",
                            ReleaseDate = DateTime.Now.AddDays(-10),
                            BookGenre = BookGenre.Adventure,
                            PublisherId = 3
                        },
                        new Book()
                        {
                            Title = "Aberrant Season 2 #1",
                            Description = "Chapter 1: 'I Need A Hero.' As a wholly new and delightfully twisted story arc begins, David is forced to team with the man he hates most.",
                            Price = 6.99,
                            ImageURL = "https://s3.amazonaws.com/comicgeeks/comics/covers/large-5753597.jpg?1556072477",
                            ReleaseDate = DateTime.Now.AddDays(+10),
                            BookGenre = BookGenre.Adventure,
                            PublisherId = 4
                        },
                         new Book()
                        {
                            Title = "Briar Vol. 1 BOOM! Studios Exclusive",
                            Description = "One-hundred years after Briar Rose first fell into her slumber, the sleeper has now become the sleepwalker, and she must face a brutal, bleak world ruled by a tyrant from her past.",
                            Price = 19.99,
                            ImageURL = "https://shop.boom-studios.com/cdn/shop/products/Briar_v1_SC_Cover_BoomEx_LOW_720x.jpg?v=1682011478",
                            ReleaseDate = DateTime.Now.AddDays(-20),
                            BookGenre = BookGenre.Historical,
                            PublisherId = 5
                        },
                          new Book()
                        {
                            Title = "Monica",
                            Description = "This long-awaited new graphic novel from Daniel Clowes ( Ghost World and Patience ) is a genre-bending thriller from one of the most assured storytellers of all time.",
                            Price = 30,
                            ImageURL = "https://www.fantagraphics.com/cdn/shop/products/Monica-3DCover_540x.jpg?v=1680213169",
                            ReleaseDate = DateTime.Now.AddDays(-30),
                            BookGenre = BookGenre.Fantasy,
                            PublisherId = 6
                        },
                          new Book()
                        {
                            Title = "V for Vendetta",
                            Description = "Count Dracula is reborn as a katana-wielding young vampiress in a new, never-seen-before action manga set in a gritty, alternate-history Europe.",
                            Price = 24.99,
                            ImageURL = "https://upload.wikimedia.org/wikipedia/en/c/c0/V_for_vendettax.jpg",
                            ReleaseDate = DateTime.Now.AddDays(-50),
                            BookGenre = BookGenre.Fantasy,
                            PublisherId = 7
                        },
                          new Book()
                        {
                            Title = "BLOOD BLADE",
                            Description = "In the near future, England has become a corrupt, totalitarian state, opposed only by V, the mystery man wearing a white porcelain mask who intends to free the masses through absurd acts of terrorism.",
                            Price = 12.99,
                            ImageURL = "https://img.thriftbooks.com/api/images/i/xl/AB64ADB796130AFA00CBD7948FBC7685DBA08BBB.jpg",
                            ReleaseDate = DateTime.Now.AddDays(20),
                            BookGenre = BookGenre.Supernatural,
                            PublisherId = 8
                        }
                    });
                    context.SaveChanges();
                }
                //Artist_Book
                if (!context.Artists_Books.Any())
                {
                    context.Artists_Books.AddRange(new List<Artist_Book>()
                    {
                        new Artist_Book()
                        { 
                            ArtistId = 1,
                            BookId = 1,
                        },
                        new Artist_Book()
                        {
                            ArtistId = 2,
                            BookId = 1,
                        },
                        new Artist_Book()
                        {
                            ArtistId = 3,
                            BookId = 2,
                        },
                        new Artist_Book() 
                        {
                            ArtistId = 4,
                            BookId = 2,
                        },
                        new Artist_Book()
                        {
                            ArtistId = 5,
                            BookId = 3,
                        },
                         new Artist_Book()
                        {
                            ArtistId = 6,
                            BookId = 4,
                        },
                        new Artist_Book()
                        {
                            ArtistId = 7,
                            BookId = 5,
                        },
                        new Artist_Book()
                        { 
                            ArtistId = 8,
                            BookId = 5
                        },
                        new Artist_Book()
                        {
                            ArtistId = 9,
                            BookId = 6
                        },
                        new Artist_Book()
                        {
                            ArtistId = 10,
                            BookId = 7
                        },
                        new Artist_Book()
                        {
                            ArtistId = 11,
                            BookId = 8
                        }
                    });
                    context.SaveChanges();
                }
                //Writer_Book
                if (!context.Writers_Books.Any())
                {
                    context.Writers_Books.AddRange(new List<Writer_Book>()
                    {
                        new Writer_Book()
                        {
                            WriterId = 1,
                            BookId = 1,
                        },
                        new Writer_Book()
                        {
                            WriterId = 2,
                            BookId = 1,
                        },
                        new Writer_Book()
                        {
                            WriterId = 3,
                            BookId = 1,
                        },
                        new Writer_Book()
                        {
                            WriterId = 4,
                            BookId = 2,
                        },
                         new Writer_Book()
                        {
                            WriterId = 5,
                            BookId = 3,
                        },
                         new Writer_Book()
                        {
                            WriterId = 6,
                            BookId = 4,
                        },
                         new Writer_Book(){ 
                            WriterId = 7,
                            BookId = 5
                         },
                          new Writer_Book(){
                            WriterId = 8,
                            BookId = 6
                         },
                          new Writer_Book(){
                            WriterId = 9,
                            BookId = 7
                         },
                           new Writer_Book(){
                            WriterId = 10,
                            BookId = 8
                         }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
