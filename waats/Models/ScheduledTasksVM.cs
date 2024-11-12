using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using waats.Models.data;

namespace waats.Models
{
 

    public class ScheduleTaskVM
    {
        public int LocalID { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(250, ErrorMessage = "! Max 250 characters")]
        [DisplayName("Task name ")]
        public string Title { get; set; }


        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        [DisplayName("Task description ")]
        public string Description { get; set; }

        public string UserId { get; set; }
        public DateTime? AddedDate { get; set; }

        public bool? MarkAsCompleted { get; set; }
        public DateTime? EditDate { get; set; }
        public bool? bDeleted { get; set; }
        public bool? SubTask { get; set; }

        public static implicit operator ScheduleTaskVM(ScheduleTask v)
        {
            return new ScheduleTaskVM
            {
                LocalID=v.LocalID,
                Title = v.Title,
                Description = v.Description,
                UserId = v.UserId,
                AddedDate = v.AddedDate,
                MarkAsCompleted = v.MarkAsCompleted,
                EditDate = v.EditDate,
                bDeleted = v.bDeleted,
                SubTask = v.SubTask
            };
        }
        public static implicit operator ScheduleTask(ScheduleTaskVM v)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            return new ScheduleTask
            {
                LocalID = v.LocalID,
                Title = v.Title,
                Description = v.Description,
                UserId = v.UserId,
                AddedDate = v.AddedDate,
                MarkAsCompleted = v.MarkAsCompleted,
                EditDate = v.EditDate,
                bDeleted = v.bDeleted,
                SubTask = v.SubTask
            };

        }
    }
    public class SubScheduleTaskVM
    {
        public int SubLocalID { get; set; }
        public int MainTaskID { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(250, ErrorMessage = "! Max 250 characters")]
        [DisplayName("Sub task name ")]
        public string Title { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        [DisplayName("Sub task description ")]
        public string Description { get; set; }

        public string UserId { get; set; }
        public DateTime? AddedDate { get; set; }

        public bool? MarkAsCompleted { get; set; }
        public DateTime? EditDate { get; set; }
        public bool? bDeleted { get; set; }

        public static implicit operator SubScheduleTaskVM(SubScheduleTask v)
        {
            return new SubScheduleTaskVM
            {
                SubLocalID = v.SubLocalID,
                MainTaskID=v.MainTaskID,
                Title = v.Title,
                Description = v.Description,
                UserId = v.UserId,
                AddedDate = v.AddedDate,
                MarkAsCompleted = v.MarkAsCompleted,
                EditDate = v.EditDate,
                bDeleted = v.bDeleted
            };
        }
        public static implicit operator SubScheduleTask(SubScheduleTaskVM v)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            return new SubScheduleTask
            {
                SubLocalID = v.SubLocalID,
                MainTaskID = v.MainTaskID,
                Title = v.Title,
                Description = v.Description,
                UserId = v.UserId,
                AddedDate = v.AddedDate,
                MarkAsCompleted = v.MarkAsCompleted,
                EditDate = v.EditDate,
                bDeleted = v.bDeleted,
            };

        }
    }
}