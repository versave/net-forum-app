using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace forum_app.Model {
    public class TodoList {
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
