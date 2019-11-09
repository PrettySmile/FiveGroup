using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveGroup.Models;
using PagedList;

namespace FiveGroup.ViewModel
{
    public class ScheduleView
    {
        public List<doctor> Doctors { get; set; }
        public List<schedule> Schedules { get; set; }
        public List<hospital> Hospitals { get; set; }
        public List<department> Departments { get; set; }
        public IPagedList<schedule> schedulesList { get; set; }
        public schedule schedule { get; set; }
    }
}