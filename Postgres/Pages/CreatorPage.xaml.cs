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
    public partial class CreatorPage : ContentPage
    {
        private readonly HelperDB helper = new HelperDB();
        public CreatorPage()
        {
            InitializeComponent();
        }

        private void SavePost(object sender, EventArgs e)
        {
            string title = titleEntry.Text;
            string desc = descriptionEntry.Text;

            helper.ExecInsert(title, desc);
        }
    }
}