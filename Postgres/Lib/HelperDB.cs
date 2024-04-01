using Npgsql;
using Postgres.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Postgres.Lib
{
    public class HelperDB
    {
        /// <summary>
        /// If you <c>OPEN</c> a DB connection you <c>HAVE TO</c> close it! ( Most of the time :3 )
        /// </summary>
        /// <returns>You get a DB connection</returns>
        public static async Task<NpgsqlConnection> InitDB()
        {
            // nie powinno się dawać hasła do db w kodzie, ale w takim razie jak Pan miałby to odpalić? ;)
            string connStr = $"Host={GetHost()};Username=postgres;Password=postgres;Database=blog";
            var conn = new NpgsqlConnection(connStr);

            try { await conn.OpenAsync(); }
            catch (Exception ex) { Console.WriteLine($"Connection to DB failed: {ex.Message}"); }

            return conn;
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



        public async Task<bool> ExecInsert(string name, string desc)
        {
            var conn = await InitDB();

            // https://www.npgsql.org/doc/basic-usage.html#parameters
            var cmd = new NpgsqlCommand("INSERT INTO posts (id, name, description) VALUES ($1, $2, $3)", conn)
            {
                Parameters =
                {
                    new NpgsqlParameter() { Value = GenerateId() },
                    new NpgsqlParameter() { Value = name},
                    new NpgsqlParameter() { Value = desc}
                }
            };


            try { 
                await cmd.ExecuteNonQueryAsync(); 
                return true;
            }
            catch (Exception ex) { 
                Console.WriteLine($"Query execution failed: {ex.Message}");
                return false;
            }
            finally { await conn.CloseAsync(); }

        }

        /// <summary>
        /// Generates id for db
        /// </summary>
        /// <returns>Milis since the beginning of the unix era</returns>
        private long GenerateId()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }



        public async Task<ObservableCollection<Post>> ShowAllPosts()
        {
            var conn = await InitDB();


            var cmd = new NpgsqlCommand("SELECT * FROM posts", conn);
            var reader = await cmd.ExecuteReaderAsync();


            var posts = new ObservableCollection<Post>();

            while (await reader.ReadAsync())
            {
                var id = (long)reader["id"];
                var name = (string)reader["name"];
                var desc = (string)reader["description"];

                posts.Add(new Post { Id = id, Name = name, Description = desc });
            }
            

            await conn.CloseAsync();

            return posts;
        }
    }
}
