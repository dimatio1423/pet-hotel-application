using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FeedbackModel.Request
{
    public class FeedbackCreateReqModel
    {
        [Required(ErrorMessage ="Please input comment")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Please select rating")]
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5 stars")]
        public int Rating { get; set; }
    }
}
