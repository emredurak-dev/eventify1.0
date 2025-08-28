using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eventify1._0.Context
{
    public class EventContext:DbContext
    {
        public EventContext():base("name=EventContext")
        {
        }
        public DbSet<Entities.Event> Events { get; set; }
    }
}