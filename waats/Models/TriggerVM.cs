using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using waats.Models.data;

namespace waats.Models
{
    public class TriggerVM
    {
        public int TriggerID { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        [DisplayName("Describe what was the trigger event? ")]
        public string whatwasthetriggerevent { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        [DisplayName("What was the emotion that you felt? ")]
        public string whatwastheemotionthatyoufelt { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        [DisplayName("How did it feel in your body? ")]
        public string howdiditfeelinyourbody { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        [DisplayName("Why might this be a trigger for you(think about your past)? ")]
        public string whymightthisbeatriggerforyou_thinkaboutyourpast { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        [DisplayName("How can you avoid the situation and/or manage your response to it in future? ")]
        public string howcanyouavoidthesituationand_ormanageyourresponsetoitinfuture { get; set; }

        public string UserId { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<bool> MarkAsCompleted { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<bool> bDeleted { get; set; }
        public static implicit operator TriggerVM(Trigger v)
        {
            return new TriggerVM
            {
                TriggerID=v.TriggerID,
                whatwasthetriggerevent = v.whatwasthetriggerevent,
                whatwastheemotionthatyoufelt = v.whatwastheemotionthatyoufelt,
                howdiditfeelinyourbody = v.howdiditfeelinyourbody,
                whymightthisbeatriggerforyou_thinkaboutyourpast = v.whymightthisbeatriggerforyou_thinkaboutyourpast,
                howcanyouavoidthesituationand_ormanageyourresponsetoitinfuture = v.howcanyouavoidthesituationand_ormanageyourresponsetoitinfuture,
                UserId = v.UserId,
                AddedDate = v.AddedDate,
                MarkAsCompleted = v.MarkAsCompleted,
                EditDate = v.EditDate,
                CompletionDate = v.CompletionDate,
                bDeleted = v.bDeleted
            };
        }
        public static implicit operator Trigger(TriggerVM v)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            return new Trigger
            {
                TriggerID = v.TriggerID,
                whatwasthetriggerevent = v.whatwasthetriggerevent,
                whatwastheemotionthatyoufelt = v.whatwastheemotionthatyoufelt,
                howdiditfeelinyourbody = v.howdiditfeelinyourbody,
                whymightthisbeatriggerforyou_thinkaboutyourpast = v.whymightthisbeatriggerforyou_thinkaboutyourpast,
                howcanyouavoidthesituationand_ormanageyourresponsetoitinfuture = v.howcanyouavoidthesituationand_ormanageyourresponsetoitinfuture,
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