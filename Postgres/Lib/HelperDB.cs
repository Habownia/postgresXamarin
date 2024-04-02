using Npgsql;
using Postgres.Models;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
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


            try
            {
                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
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


            var posts = await GetPostsFromReader(reader);


            await conn.CloseAsync();

            return posts;
        }


        /// <summary>
        /// Returns the date the post was created
        /// </summary>
        public DateTime GetDateFromId(long id)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(id);

            return dateTimeOffset.DateTime;
        }


        /// <summary>
        /// Returns post data by its id
        /// </summary>
        /// <param name="postId">Post id</param>
        public async Task<Post> GetPostFromId(long postId)
        {
            var conn = await InitDB();


            var cmd = new NpgsqlCommand("SELECT * FROM posts WHERE id = $1", conn)
            {
                Parameters =
                {
                    new NpgsqlParameter() { Value = postId },
                }
            };
            var reader = await cmd.ExecuteReaderAsync();


            var posts = await GetPostsFromReader(reader);


            await conn.CloseAsync();
            return posts[0];
        }



        /// <summary>
        /// Creates observable collection out of data reader
        /// </summary>
        /// <param name="reader">Rows from db</param>
        private async Task<ObservableCollection<Post>> GetPostsFromReader(NpgsqlDataReader reader)
        {
            var posts = new ObservableCollection<Post>();

            while (await reader.ReadAsync())
            {
                var id = (long)reader["id"];
                var name = (string)reader["name"];
                var desc = (string)reader["description"];

                posts.Add(new Post { Id = id, Name = name, Description = desc });
            }

            return posts;
        }


        public async Task<bool> Delete(long id)
        {
            var conn = await InitDB();

            var cmd = new NpgsqlCommand("DELETE FROM posts WHERE id = $1", conn)
            {
                Parameters =
                {
                    new NpgsqlParameter() { Value = id },
                }
            };


            try
            {
                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Query execution failed: {ex.Message}");
                return false;
            }
            finally { await conn.CloseAsync(); }
        }


        public async Task<bool> Update(Post post)
        {
            var conn = await InitDB();

            var cmd = new NpgsqlCommand("UPDATE posts SET name = $1, description = $2 WHERE id = $3", conn)
            {
                Parameters =
                {
                    new NpgsqlParameter() { Value = post.Name },
                    new NpgsqlParameter() { Value = post.Description },
                    new NpgsqlParameter() { Value = post.Id },
                }
            };


            try
            {
                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Query execution failed: {ex.Message}");
                return false;
            }
            finally { await conn.CloseAsync(); }
        }

    }
}
