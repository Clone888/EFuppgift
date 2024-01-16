using EFuppgift;
using Microsoft.EntityFrameworkCore;


using BloggingContext? db = new();

db.Database.EnsureDeleted();
db.Database.Migrate();
db.Database.EnsureCreated();
db.SaveChanges();

Console.WriteLine($"SQLite DB located at: {db.DbPath}");
Console.WriteLine("RUNING PROGRAM");

string[] userCsv = File.ReadAllLines("datauser.csv");
string[] postCsv = File.ReadAllLines("datapost.csv");
string[] blogCsv = File.ReadAllLines("datablog.csv");




foreach (string line in userCsv)
{
    string[] split = line.Split(",");
   
    var userExists = db.Users.Find(int.Parse(split[0]));

    if (userExists == null)
    {
        db.Add(new User { UserId = int.Parse(split[0]), Username = split[1], Password = split[2] });
        db.SaveChanges();
    }
    else
    {
        Console.WriteLine("FINNS");
        continue;
    }
}


foreach (string line in blogCsv)
{
    string[] split = line.Split(",");


    var blogExists = db.Blogs.Find(int.Parse(split[0]));

    if (blogExists == null)
    {
        db.Add(new Blog { BlogId = int.Parse(split[0]), Name = split[2], Url = split[1], });
        Console.WriteLine($"ID: {split[0]}");
        Console.WriteLine($"NAMN:   {split[1]}");
        Console.WriteLine($"URL:    {split[2]}");
        Console.WriteLine("_______________________");
        db.SaveChanges();
    }
    else
    {
        Console.WriteLine("FINNS");
        continue;
    }
}



foreach (string line in postCsv)
{
    string[] split = line.Split(",");



    if (DateOnly.TryParse(split[3], out DateOnly date))
    {
        
    }
    else
    {
        Console.WriteLine("Wrong with parse date");
    }

    if (int.TryParse(split[4], out int blogId))
    {
        
    }
    else
    {
        Console.WriteLine("Wrong with parse blogCsv id");
    }

    if (int.TryParse(split[5], out int userId))
    {
       
    }
    else
    {
        Console.WriteLine("Wrong with parse userCsv id");
    }

    db.Add(new Post { PostId = int.Parse(split[0]), Title = split[1], Content = split[2], PublishedOn = date, BlogId = blogId, UserId = userId });
    

}
db.SaveChanges();

Console.WriteLine("\t\t\tRESULT");
Console.WriteLine();
foreach (User userLine in db.Users.OrderBy(orderby => orderby.Username))
{

    Console.WriteLine($"User: \t\t{userLine.Username}");
    Console.WriteLine();


    foreach (Post postLine in userLine.Posts.OrderBy(order => order.PublishedOn))
    {
        Console.WriteLine($"Title: {postLine.Title}");
        Console.WriteLine($"Content: {postLine.Content}");
        Console.WriteLine($"Date: {postLine.PublishedOn}");
        Console.WriteLine();

        Console.WriteLine($"BlogID: {postLine.BlogId}");
        Console.WriteLine($"Name: {postLine.Blog?.Name}");
        Console.WriteLine($"URL: {postLine.Blog?.Url}");

        Console.WriteLine();
        Console.WriteLine("_______________________________________");
        Console.WriteLine();
    }
}