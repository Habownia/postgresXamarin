using Postgres.Lib;
using Postgres.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Postgres
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllPostsPage : ContentPage
    {
        private readonly HelperDB helper = new HelperDB();

        public AllPostsPage()
        {
            InitializeComponent();

            //ExecInsert(14, "test", "b");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // shows data from db in a view
            postListView.ItemsSource = await helper.ShowAllPosts();
        }

        private async void GoToDetails(object sender, EventArgs e)
        {
            ViewCell viewCell = (ViewCell)sender;
            Label idLabel = viewCell.FindByName<Label>("postId");

            long id = long.Parse(idLabel.Text);
            await Navigation.PushAsync(new DetailsPage(id));
            //
        }
    }
}