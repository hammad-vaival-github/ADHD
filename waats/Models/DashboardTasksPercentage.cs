using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waats.Models
{
    public class DashboardTasksPercentage
    {
        public decimal ScheduleTasksPercentage { get; set; }
        public decimal MindfulnessMeditation { get; set; }
        public decimal BrainFitness { get; set; }
        public decimal SpottingTriggers { get; set; }
        public decimal MemoryMaker { get; set; }
        public decimal MindfulAttention { get; set; }
        public decimal GratitudeJournal { get; set; }
    }
}