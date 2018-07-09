using SixOhFour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SixOhFour.ViewModels
{
    public class WritePostViewModel : Post
    {
        public static Post WritePostConvert(WritePostViewModel viewModel)
        {
            Post returnPost = new Post();
            returnPost.Content = viewModel.Content;
            returnPost.CreatedDate = viewModel.CreatedDate;
            returnPost.PostId = viewModel.PostId;
            returnPost.Slug = viewModel.Slug;
            returnPost.Title = viewModel.Title;
            return returnPost;
        }

        public WritePostViewModel()
        {

        }

        public WritePostViewModel(Post post)
        {
            this.Content = post.Content;
            this.CreatedDate = post.CreatedDate;
            this.PostId = post.PostId;
            this.Slug = post.Slug;
            this.Title = post.Title;
        }
    }
}
