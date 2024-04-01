using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Npgsql;
using Postgres.Models;
using Postgres.Lib;

namespace Postgres
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToAllPosts(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllPostsPage());
        }

        private async void GoToPostCreator(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatorPage());
        }

    }
}
