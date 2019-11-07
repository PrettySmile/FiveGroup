using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FiveGroup.Models;

namespace FiveGroup.ViewModel
{
    public class FeedbackClass
    {
        public List<feedback> Feedbacks { get; set; }
        public List<feedback_class> Feedback_Classes { get; set; }
    }
}