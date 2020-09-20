using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ozh.Utility.Reflection;

namespace ElevatorLib.Models.Blogs
{
    public class BlogDto : ICloneable
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Sub title is required")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public DateTime Time { get; set; }
        public List<string> Links { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(Id)}: {Id}");
            stringBuilder.AppendLine($"{nameof(Title)}: {Title}");
            stringBuilder.AppendLine($"{nameof(SubTitle)}: {SubTitle}");
            stringBuilder.AppendLine($"{nameof(AuthorName)}: {AuthorName}");
            stringBuilder.AppendLine($"{nameof(Time)}: {Time.ToString()}");
            stringBuilder.AppendLine($"{nameof(Content)}: {Content}");
            return stringBuilder.ToString();
        }

        /// <inheritdoc />
        public object Clone()
        {
            var copy = new BlogDto();
            copy.CopyPublicProperties(this);
            return copy;
        }

        public void CopyFrom(BlogDto other)
        {
            this.CopyPublicProperties(other);
        }
    }
}