using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using curmudgeon.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixOhFour.Models;
using SixOhFour.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SixOhFour.Controllers
{
    public class PostsController : Controller
    {
        private readonly SixOhFourDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public async Task<IActionResult> Index(int page = 0)
        {
            var posts = _db.Posts.Skip(page * 10).Take(10).ToList();

            Paginator paginator = new Paginator(posts.Count, page, 10);

            var paginatedPosts = posts.Skip((paginator.CurrentPage - 1) * paginator.PageLength).Take(paginator.PageLength).OrderBy(p => p.CreatedDate);

            PostsIndexViewModel model = new PostsIndexViewModel()
            {
                Posts = paginatedPosts,
                Paginator = paginator
            };

            return View(model);
        }

        public IActionResult Read(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var thisPost = _db.Posts
                .Where(p => p.PostId == id)
                .FirstOrDefault();

            //thisPost.Content = Post.PostContentParser(thisPost.Content);

            return View(thisPost);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var thisPost = _db.Posts
                           .Where(p => p.PostId == id)
                           .FirstOrDefault();

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            WritePostViewModel editPost = new WritePostViewModel(thisPost);
            
            return View(editPost);
        }

        [HttpPost]
        public IActionResult Edit(WritePostViewModel editPost)
        {
            
            Post savePost = WritePostViewModel.WritePostConvert(editPost);

            //check to see if the user making the POST request is the same as the owner of the post being edited
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string originalPostUserId = TempData["AccountId"].ToString();
            if (userId == originalPostUserId)
            {
                //Save changes to the actual post
                _db.Entry(savePost).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Delete(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(p => p.PostId == id);
            return View(thisPost);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPost = _db.Posts
                .Where(p => p.PostId == id)
                .FirstOrDefault();
            _db.Posts.Remove(thisPost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
