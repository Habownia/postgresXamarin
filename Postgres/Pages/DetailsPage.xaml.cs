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
        public DetailsPage(long id)
        {
            InitializeComponent();

            BindingContext = new Post
            {
                Name = "Przykładowy tytuł",
                Description = "Przykładowy opis",
                Date = GetDateFromId(id),
            };
        }

        private DateTime GetDateFromId(long id)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(id);

            return dateTimeOffset.DateTime;
        }

        private async void SavePost(object sender, EventArgs e)
        {
        }
    }
}