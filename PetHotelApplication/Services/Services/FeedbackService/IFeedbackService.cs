﻿using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FeedbackService
{
    public interface IFeedbackService
    {
        List<Feedback> GetFeedbacks();
        Feedback GetFeedbackById(string id);
        void Add(Feedback feedback);
        void Delete(Feedback feedback);
        void Update(Feedback feedback);
    }
}
