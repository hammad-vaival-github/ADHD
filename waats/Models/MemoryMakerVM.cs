using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using waats.Models.data;
namespace waats.Models
{
    public class MemoryMakerVM
    {
        public int memoryId { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        public string One { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        public string two { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        public string three { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(4000, ErrorMessage = "! Max 4000 characters")]
        public string four { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<bool> MarkAsCompleted { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<bool> bDeleted { get; set; }

        public static implicit operator MemoryMakerVM(MemoryMaker v)
        {
            return new MemoryMakerVM
            {
                memoryId = v.memoryId,
                One = v.One,
                two = v.two,
                three = v.three,
                four = v.four,
                UserId = v.UserId,
                AddedDate = v.AddedDate,
                MarkAsCompleted = v.MarkAsCompleted,
                EditDate = v.EditDate,
                CompletionDate = v.CompletionDate,
                bDeleted = v.bDeleted
            };
        }
        public static implicit operator MemoryMaker(MemoryMakerVM v)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            return new MemoryMaker
            {
                memoryId = v.memoryId,
                One = v.One,
                two = v.two,
                three = v.three,
                four = v.four,
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