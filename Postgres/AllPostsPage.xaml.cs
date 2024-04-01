using Postgres.Lib;
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
    }
}