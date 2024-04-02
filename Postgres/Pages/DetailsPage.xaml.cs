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
    public partial class DetailsPage : ContentPage
    {

        private readonly HelperDB helper = new HelperDB();

        private new long Id { get; set; }

        public DetailsPage(long id)
        {
            Id = id;
            InitializeComponent();

            SetBindingContext(id);
        }

        //move
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

        private async void GoToEdit(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage(Id));
        }

        private async void DeletePost(object sender, EventArgs e)
        {
            bool isSucceded = await helper.Delete(Id);
            PromptDBQuery(isSucceded);
        }

        //move
        private async void PromptDBQuery(bool isSucceded)
        {
            if (isSucceded)
            {
                await DisplayAlert("Sukces", "Udało się usunąć post!", "OK");
                await Navigation.PopAsync();

            }
            else
                await DisplayAlert("Błąd", "Post nie został usunięty. Spróbuj ponownie!", "OK");
        }

    }
}