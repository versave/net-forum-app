using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace forum_app.Model {
    public class Post {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
