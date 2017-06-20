using Runniac.Data;
using Runniac.Membership.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Tests.Fakes
{
    class FakeData
    {
        internal static IQueryable<Event> GetEvents()
        {
            return new List<Event>
            {
                new Event { 
                    Id = 1,
                    Name = "London Marathon", 
                    Location = "London", 
                    Type="Marathon", 
                    DistanceKms=42, 
                    EventDate=new DateTime(2014, 7, 5, 12, 0, 0)
                },
                new Event { 
                    Name = "Berlin Marathon", 
                    Location = "Berlin", 
                    Type="Marathon", 
                    DistanceKms=42, 
                    EventDate=new DateTime(2014, 7, 5, 12, 0, 0) 
                },
                new Event { 
                    Name = "San Silvestre Vallecana", 
                    Location = "Madrid", 
                    Type="San Silvestre", 
                    DistanceKms=6, 
                    EventDate=new DateTime(2014, 7, 5, 12, 0, 0) 
                }, 
                new Event { 
                    Name = "Behobia-San Sebastián", 
                    Location = "Guipúzkoa",  
                    DistanceKms=21, 
                    EventDate=new DateTime(2014, 7, 5, 12, 0, 0) 
                },
                new Event { 
                    Name = "Ultra Marathon Hawaii", 
                    Location = "Hawaii", 
                    Type="Ultra Marathon", 
                    DistanceKms=100, 
                    EventDate=new DateTime(2014, 7, 5, 12, 0, 0)
                }
            }
            .AsQueryable();
        }

        internal static IEnumerable<string> GetLocations()
        {
            return new List<string> { "Pamplona", "Madrid", "Oviedo", "Huesca" };
        }

        internal static IEnumerable<Comment> GetComments()
        {
            return new List<Comment>
            {
                new Comment { 
                    Text = "test comment",
                    Id = 1,
                    UserId = 1,
                    Event = GetEvents().First(),
                    CommentDate=new DateTime(2014, 7, 5, 12, 0, 0)
                },
                new Comment { 
                    Text = "test comment 2",
                    Id = 2,
                    UserId = 1,
                    EventId = 1,
                    CommentDate=new DateTime(2014, 7, 6, 12, 0, 0)
                },
                new Comment { 
                    Text = "test comment 3",
                    Id = 3,
                    UserId = 2,
                    Event = GetEvents().First(),
                    CommentDate=new DateTime(2014, 7, 5, 18, 0, 0)
                }            
            };
        }

        internal static IEnumerable<Vote> GetVotes()
        {
            return new List<Vote> {
                new Vote {
                    Comment = GetComments().ElementAt(0),
                    Positive = true,
                    UserId = 2
                },
                new Vote {
                    Comment = GetComments().ElementAt(1),
                    Positive = true,
                    UserId = 1
                },
                new Vote {
                    Comment = GetComments().ElementAt(2),
                    Positive = false,
                    UserId = 3
                }
            };
        }

        internal static IEnumerable<Photo> GetPhotos()
        {
            return new List<Photo>{
                new Photo {
                    UserId = 1,
                    Url = "foo.com/image1.jpg",
                    Event = GetEvents().ElementAt(1),
                    Id = 1,
                    PhotoDate = DateTime.Today
                },
                new Photo {
                    UserId = 2,
                    Url = "foo.com/image2.jpg",
                    Event = GetEvents().ElementAt(1),
                    Id = 2,
                    PhotoDate = DateTime.Today
                }
            };
        }

        internal static IEnumerable<Town> GetTowns()
        {
            return new List<Town>{
                new Town {
                    Id=1,
                    Name="Oviedo",
                    PostalCode = "33012"
                },
                new Town {
                    Id=2,
                    Name="Zaragoza",
                    PostalCode = "51000"
                },
                new Town {
                    Id=3,
                    Name="Pamplona",
                    PostalCode = "31012"
                }
            };
        }

        internal static User GetUser()
        {
            return new User
            {
                Id = 1,
                Name = "Scott",
                Lastname = "Tiger",
                UserName = "scott"
            };
        }
    }
}
