using Postgres.Lib;
using Postgres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Postgres.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        private readonly HelperDB helper = new HelperDB();
        private readonly Post OldPost;

        public EditPage(Post post)
        {
            // move
            InitializeComponent();
            BindingContext = post;
            OldPost = post;
        }

        // TODO: Move to seperate file
        private async void SavePost(object sender, EventArgs e)
        {
            string title = titleEntry.Text;
            string desc = descriptionEntry.Text;

            Post updatedPost = new Post()
            {
                Id = OldPost.Id,
                Name = title,
                Description = desc,
            };


            // Checks if title and desc is present
            if (title == null || title.Length <= 0)
                await DisplayAlert("Błąd", "Hola, hola! Nie zapominaj o tytule!", "OK");
            else if (desc == null || desc.Length <= 0)
                await DisplayAlert("Błąd", "Cóż za brak wyobraźni! Musisz dodać jakąś treść!", "OK");
            else
            {
                // change after moved
                bool isSuccessful = await helper.Update(updatedPost);

                PromptDBQuery(isSuccessful);
            }
        }

        //move
        private async void PromptDBQuery(bool isSuccessful)
        {
            if (isSuccessful)
            {
                await DisplayAlert("Sukces", "Udało się zedytować post!", "OK");
                await Navigation.PopAsync();

            }
            else
                await DisplayAlert("Błąd", "Post nie został dodany. Spróbuj ponownie!", "OK");
        }
    }
}