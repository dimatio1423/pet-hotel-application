using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FeedbackModel.Request
{
    public class FeedbackUpdateReqModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please input comment")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Please select rating")]
        public int Rating { get; set; }
    }
}
