using Postgres.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Postgres.Lib
{
    public class PostEditorHelper
    {
        public static Page CurrPage = Application.Current.MainPage;
        private readonly HelperDB helper = new HelperDB();


        public static async void PromptDBQuery(bool isSucceded, string text)
        {
            if (isSucceded)
                await CurrPage.Navigation.PopAsync();
            else
                await CurrPage.DisplayAlert("Błąd", $"Post nie został {text}. Spróbuj ponownie!", "OK");
        }



        /// <summary>
        /// Checks if title and desc is not empty
        /// </summary>
        /// <param name="title">Post title</param>
        /// <param name="desc">Post description</param>
        /// <returns><c>true</c> if empty, <c>false</c> if not empty</returns>
        public static async Task<bool> IsEmptyEntries(string title, string desc)
        {            
            if (title == null || title.Length <= 0)
                await CurrPage.DisplayAlert("Błąd", "Hola, hola! Nie zapominaj o tytule!", "OK");
            else if (desc == null || desc.Length <= 0)
                await CurrPage.DisplayAlert("Błąd", "Cóż za brak wyobraźni! Musisz dodać jakąś treść!", "OK");
            else
                return false;
            

            return true;
        }

        public async Task<Post> SetBindingContext(long id)
        {
            var post = await helper.GetPostFromId(id);

            Post postWithDate = new Post
            {
                Id = post.Id,
                Name = post.Name,
                Description = post.Description,
                Date = helper.GetDateFromId(post.Id),
            };

            CurrPage.BindingContext = postWithDate;
            return postWithDate;
        }
    }
}
