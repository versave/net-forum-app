using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace forum_app.Model {
    public class Comment {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        public int PostId { get; set; }
        public DateTime Date { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
