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
    
    public partial class Trigger
    {
        public int TriggerID { get; set; }
        public string whatwasthetriggerevent { get; set; }
        public string whatwastheemotionthatyoufelt { get; set; }
        public string howdiditfeelinyourbody { get; set; }
        public string whymightthisbeatriggerforyou_thinkaboutyourpast { get; set; }
        public string howcanyouavoidthesituationand_ormanageyourresponsetoitinfuture { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<bool> MarkAsCompleted { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<bool> bDeleted { get; set; }
    }
}