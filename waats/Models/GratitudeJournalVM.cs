using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using waats.Models.data;
namespace waats.Models
{
    public class GratitudeJournalVM
    {
        public int GratitudeJouralID { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        [DisplayName("Things I am gratfull for today : ")]
        public string thingsiamgratfullfortoday { get; set; }
        
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<bool> MarkAsCompleted { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<bool> bDeleted { get; set; }

        public static implicit operator GratitudeJournalVM(GratitudeJournal v)
        {
            return new GratitudeJournalVM
            {
                GratitudeJouralID = v.GratitudeJouralID,
                thingsiamgratfullfortoday = v.thingsiamgratfullfortoday,
                UserId = v.UserId,
                AddedDate = v.AddedDate,
                MarkAsCompleted = v.MarkAsCompleted,
                EditDate = v.EditDate,
                CompletionDate = v.CompletionDate,
                bDeleted = v.bDeleted
            };
        }
        public static implicit operator GratitudeJournal(GratitudeJournalVM v)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            return new GratitudeJournal
            {
                GratitudeJouralID = v.GratitudeJouralID,
                thingsiamgratfullfortoday = v.thingsiamgratfullfortoday,
                UserId = v.UserId,
                AddedDate = v.AddedDate,
                MarkAsCompleted = v.MarkAsCompleted,
                EditDate = v.EditDate,
                CompletionDate = v.CompletionDate,
                bDeleted = v.bDeleted
            };

        }
    }
}