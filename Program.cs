using EFuppgift;
using Microsoft.EntityFrameworkCore;

using BloggingContext? db = new();
// samma som using var db = bloggingcontext new();
db.Database.EnsureDeleted();
db.Database.Migrate();
db.Database.EnsureCreated();
db.SaveChanges();

Console.WriteLine($"SQLite DB located at: {db.DbPath}");
Console.WriteLine("RUNING PROGRAM");

string[] user = File.ReadAllLines("datauser.csv");
string[] post = File.ReadAllLines("datapost.csv");
string[] blog = File.ReadAllLines("datablog.csv");




foreach (string line in user)
{
    string[] split = line.Split(",");
    db.Add(new User { Username = split[1], Password = split[2] });
}
db.SaveChanges();


foreach (string line in blog)
{
    string[] split = line.Split(",");

    db.Add(new Blog { Name = split[2], Url = split[1], });
}
db.SaveChanges();

foreach (string line in post)
{
    string[] split = line.Split(",");



    if (DateOnly.TryParse(split[3], out DateOnly date))
    {
        //db.Add(new Post { PublishedOn = date });
    }
    else
    {
        Console.WriteLine("Wrong with parse date");
    }

    if (int.TryParse(split[4], out int blogid))
    {
        // db.Add(new Post { BlogId = blogid });
    }
    else
    {
        Console.WriteLine("Wrong with parse blog id");
    }

    if (int.TryParse(split[5], out int userid))
    {
        //db.Add(new Post { UserId = userid });
    }
    else
    {
        Console.WriteLine("Wrong with parse user id");
    }

    db.Add(new Post { PostId = int.Parse(split[0]), Title = split[1], Content = split[2], PublishedOn = date, BlogId = blogid, UserId = userid });
    //db.Add(new Post { Content = split[3] });

}
db.SaveChanges();

Console.WriteLine("\t\t\tRESULT");
Console.WriteLine();
foreach (User lineUser in db.Users)
{

    Console.WriteLine($"User: {lineUser.Username}");
    Console.WriteLine();


    foreach (Post linePost in lineUser.Posts)
    {
        Console.WriteLine($"Title: {linePost.Title}");
        Console.WriteLine($"Content: {linePost.Content}");
        Console.WriteLine($"Date: {linePost.PublishedOn}");
        Console.WriteLine();

        foreach (Blog lineBlog in linePost.Blog)
        {
            Console.WriteLine($"Blog{lineBlog.Name}");
            Console.WriteLine($"URL{lineBlog.Url}");
        }
    }
}
}













/*

Console.WriteLine($"---------------------NEXT BLOG---------------------------");



Console.WriteLine("\t\t\tBLOGS");
foreach (Blog b in db.Blogs)
{
    Console.WriteLine($"Name: {b.Name} ");
    Console.WriteLine($"URL: {b.Url} ");
    Console.WriteLine();
}

Console.WriteLine($"---------------------NEXT POST---------------------------");





Console.WriteLine("\t\t\tPOSTS");
foreach (Post p in db.Posts.OrderBy(o => o.PublishedOn))
{
    Console.WriteLine($"PostId: {p.PostId}|| ");
    Console.Write($"Title: {p.Title}|| ");
    Console.WriteLine($"Content: {p.Content}|| ");
    Console.Write($"Published On: {p.PublishedOn}|| ");
    Console.WriteLine($"BlogId: {p.BlogId} ");
    Console.WriteLine();
}



*/


/*
db.SaveChanges();

foreach (User u in db.Users)
{
    db.Remove(u);
}
db.SaveChanges();


foreach (Blog b in db.Blogs)
{
    db.Remove(b);
}
db.SaveChanges();

foreach (Post p in db.Posts)
{
    db.Remove(p);
}




db.Add(new Blog { Url = "First Blog" });
db.Add(new Blog { Url = "Second Blog" });

db.SaveChanges(); // Sparar 


        // SQL orderBy    //Intern foreach  //Order by "BlogId" 
Blog? blog = db.Blogs.OrderBy(b => b.BlogId).First();

//db.Blogs.Find // hitta blogId

Console.WriteLine($"Blog url before update: {blog.Url}");

blog.Url = "First blog changes";
db.SaveChanges();

blog = db.Blogs.OrderBy(b => b.BlogId).First();
Console.WriteLine($"Blog url after update: {blog.Url}");


//Update
blog.Posts.Add(new Post 
{
    Title = "First Post in the First Blog",
    Content = "No content to be found here"
});
db.SaveChanges();

//Delete
foreach (Blog b in db.Blogs)
{
    Console.WriteLine($"Deleting Blog: {b.Url}");
    Console.WriteLine($"Amount of posts deleted: {b.Posts.Count}");
    //En till foreeach för att göra ett "träd" på vad som är raderat.
    db.Remove(b);
   
}
db.SaveChanges();
*/