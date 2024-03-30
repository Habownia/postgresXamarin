﻿using Npgsql;
using Postgres.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Postgres
{
    public partial class MainPage : ContentPage
    {
        public static Task<NpgsqlConnection> conn = InitDB();

        public MainPage()
        {
            InitializeComponent();

            //ExecInsert(12, "a", "b");
            ShowAllPosts();
        }


        public static async Task<NpgsqlConnection> InitDB()
        {
            // nie powinno się dawać hasła do db w kodzie, ale w takim razie jak Pan miałby to odpalić? ;)
            string connStr = $"Host={GetHost()};Username=postgres;Password=postgres;Database=blog";
            var conn = new NpgsqlConnection(connStr);

            try { await conn.OpenAsync(); }
            catch (Exception ex) { Console.WriteLine($"Connection to DB failed: {ex.Message}"); }

            return conn;
        }



        public async void ExecInsert(int id, string name, string desc)
        {
            // https://www.npgsql.org/doc/basic-usage.html#parameters
            var cmd = new NpgsqlCommand("INSERT INTO posts (id, name, description) VALUES ($1, $2, $3)", await conn)
            {
                Parameters =
                {
                    new NpgsqlParameter() { Value = id }, 
                    new NpgsqlParameter() { Value = name}, 
                    new NpgsqlParameter() { Value = desc} 
                }
            };


            try { await cmd.ExecuteNonQueryAsync(); }
            catch (Exception ex) { Console.WriteLine($"Query execution failed: {ex.Message}"); }
        }



        public async void ShowAllPosts()
        {
            var cmd = new NpgsqlCommand("SELECT * FROM posts", await conn);
            var reader = await cmd.ExecuteReaderAsync();

            var posts = new ObservableCollection<Post>();

            while (await reader.ReadAsync())
            {
                var id = (int)reader["id"];
                var name = (string)reader["name"];
                var desc = (string)reader["description"];

                posts.Add(new Post { Id = id, Name = name, Description = desc });
            }

            postListView.ItemsSource = posts;
        }

        /// <summary>
        /// Method that returns the host address depending on the device platform
        /// </summary>
        /// <returns>Host address</returns>
        public static string GetHost()
        {
            var platform = Device.RuntimePlatform;

            if (platform == Device.Android) return "10.0.2.2";

            return "localhost";
        }

    }
}
