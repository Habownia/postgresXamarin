using Postgres.Lib;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Postgres
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatorPage : ContentPage
    {
        private readonly HelperDB helper = new HelperDB();
        public CreatorPage()
        {
            InitializeComponent();
        }

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