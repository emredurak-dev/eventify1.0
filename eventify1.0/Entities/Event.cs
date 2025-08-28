﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventify1._0.Entities
{
    public class Event
    {
        public int? EventId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
    }
}