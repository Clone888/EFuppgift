﻿namespace EFuppgift;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class BloggingContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.CurrentDirectory;
        DbPath = System.IO.Path.Join(folder, "cs.forts-dan-waisanen.db");
    }

    // The following configures EF to create a Sqlite database file in the
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class User
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Blog
{
    public int BlogId { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateOnly PublishedOn { get; set; }

    public Blog? Blog { get; set; }
    public int BlogId { get; set; }

    public User? User { get; set; }
    public int UserId { get; set; }
}