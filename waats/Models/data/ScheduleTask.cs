//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace waats.Models.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ScheduleTask
    {
        public int LocalID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<bool> MarkAsCompleted { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<bool> bDeleted { get; set; }
        public Nullable<bool> SubTask { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
    }
}
