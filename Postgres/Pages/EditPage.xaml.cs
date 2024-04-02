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
    public partial class EditPage : ContentPage
    {
        private readonly HelperDB helper = new HelperDB();
        private readonly Post OldPost;

        public EditPage(Post post)
        {
            // move
            InitializeComponent();
            BindingContext = post;
            OldPost = post;
        }

        // TODO: Move to seperate file
        private async void SavePost(object sender, EventArgs e)
        {
            string title = titleEntry.Text;
            string desc = descriptionEntry.Text;

            Post updatedPost = new Post()
            {
                Id = OldPost.Id,
                Name = title,
                Description = desc,
            };


            bool isEntryEmpty = await PostEditorHelper.IsEmptyEntries(title, desc);

            if (!isEntryEmpty)
            {
                bool isSucceded = await helper.Update(updatedPost);

                PostEditorHelper.PromptDBQuery(isSucceded, "dodany");
            }
        }
    }
}