﻿using System;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Manager");
            Console.WriteLine(" 1) Add Post");
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Add();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Publish Date: ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            AuthorManager authMan = new AuthorManager(this, _connectionString);
            post.Author = authMan.Choose();

            _postRepository.Insert(post);
        }
    }
}