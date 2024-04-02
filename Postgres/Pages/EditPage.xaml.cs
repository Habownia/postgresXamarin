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

        public EditPage(long id)
        {
            InitializeComponent();

            // move
            SetBindingContext(id);
        }


        // move
        private async void SetBindingContext(long id)
        {
            var post = await helper.GetPostFromId(id);

            BindingContext = new Post
            {
                Id = post.Id,
                Name = post.Name,
                Description = post.Description,
                Date = helper.GetDateFromId(id),
            };
        }


        // TODO: Move to seperate file
        private async void SavePost(object sender, EventArgs e)
        {
            string title = titleEntry.Text;
            string desc = descriptionEntry.Text;


            // Checks if title and desc is present
            if (title == null || title.Length <= 0)
                await DisplayAlert("Błąd", "Hola, hola! Nie zapominaj o tytule!", "OK");
            else if (desc == null || desc.Length <= 0)
                await DisplayAlert("Błąd", "Cóż za brak wyobraźni! Musisz dodać jakąś treść!", "OK");
            else
            {
                bool isSuccessful = await helper.ExecInsert(title, desc);
                PromptDBQuery(isSuccessful);
            }
        }

        private async void PromptDBQuery(bool isSuccessful)
        {
            if (isSuccessful)
            {
                await DisplayAlert("Sukces", "Udało się dodać post!", "OK");
                await Navigation.PopAsync();

            }
            else
                await DisplayAlert("Błąd", "Post nie został dodany. Spróbuj ponownie!", "OK");
        }
    }
}