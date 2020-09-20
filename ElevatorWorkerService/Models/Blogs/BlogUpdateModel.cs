using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorWorkerService.Models.Blogs
{
    public class BlogUpdateModel
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public List<string> Links { get; set; }
    }
}
