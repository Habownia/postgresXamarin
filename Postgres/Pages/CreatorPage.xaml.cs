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


            bool isEntryEmpty = await PostEditorHelper.IsEmptyEntries(title, desc);

            if (!isEntryEmpty)
            {
                bool isSucceded = await helper.Insert(title, desc);
                PostEditorHelper.PromptDBQuery(isSucceded, "dodany");
            }
        }
    }
}