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

        public DetailsPage(long id)
        {
            InitializeComponent();

            SetBindingContext(id);
        }

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

        private async void SavePost(object sender, EventArgs e)
        {
        }
    }
}