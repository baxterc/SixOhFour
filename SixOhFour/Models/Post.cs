using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SixOhFour.Models
{
    public class Post
    {
        private int PostId { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private string Content { get; set; }
        private DateTime CreatedDate { get; set; }
        [StringLength(64, MinimumLength = 2)]
        [RegularExpression("[a-z.0-9-]")]
        public string Slug { get; set; }

        public Post()
        {

        }

        public static string Sluggify(string slug)
        {
            slug = slug.ToLower();
            //TODO: Handle non-Latin and accented letters

            //Replace any non-lowercase alphanumeric, non-whitespace, non-hyphen character with nothing
            slug = Regex.Replace(slug, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);
            //Replace whitespace and underscores of any length or type with a hyphen
            slug = Regex.Replace(slug, @"[\s_]+", "-", RegexOptions.Compiled);
            //Replace underscores that are not at the end of the string and not followed by a number with a hyphen -- deprecated
            //slug = Regex.Replace(slug, @"_^[0-9]+$", "-", RegexOptions.Compiled);
            //Enforce maximum length of 64 chars
            if (slug.Length > 64)
            {
                slug = slug.Substring(0, 64);
            }
            //Remove leading and ending hyphens
            slug = slug.Trim('-');

            //If the slug is null or all hypens, create a random slug

            if (slug == null || slug == "")
            {
                slug = Post.GenerateSlug();
                return slug;
            }
            else if (Regex.IsMatch(slug, @"^-*$"))
            {
                slug = Post.GenerateSlug();
                return slug;
            }
            return slug;
        }

        public static string GenerateSlug()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            System.Text.StringBuilder newSlug = new System.Text.StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                char nextChar = chars[random.Next(chars.Length)];
                newSlug.Append(nextChar);
            }
            return newSlug.ToString();
        }
    }
}
