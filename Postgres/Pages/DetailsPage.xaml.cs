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
        private readonly HelperDB helperDB = new HelperDB();
        private readonly PostEditorHelper postEditorHelper = new PostEditorHelper();

        private new long Id { get; set; }
        private Post Post { get; set; }

        public DetailsPage(long id)
        {
            Id = id;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Post = await postEditorHelper.SetBindingContext(Id);
        }

        private async void GoToEdit(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage(Post));
        }


        private async void DeletePost(object sender, EventArgs e)
        {
            bool isSucceded = await helperDB.Delete(Post.Id);
            PostEditorHelper.PromptDBQuery(isSucceded, "usunięty");
        }
    }
}