using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using waats.Models.data;

namespace waats.Models
{
    public class BrainFitnessVM
    {
        public int BrainFitnessID { get; set; }
        public string UserId { get; set; }
        public int StartingNumber { get; set; }
        public Nullable<int> Q_Add { get; set; }
        public Nullable<int> Q_Take { get; set; }
        public Nullable<int> Q_A { get; set; }
        public string _a { get; set; }
        public string _t { get; set; }
        public int _UI { get; set; }
        public Nullable<int> Q_NoofAttempts { get; set; }
        public Nullable<bool> MarkAsCompleted { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<bool> bDeleted { get; set; }
        
        public static implicit operator BrainFitnessVM(BrainFitness v)
        {
            return new BrainFitnessVM
            {
                BrainFitnessID = v.BrainFitnessID,
                UserId = v.UserId,
                StartingNumber = v.StartingNumber??0,
                Q_Add = v.Q_Add,
                Q_Take = v.Q_Take,
                Q_A = v.Q_A,
                Q_NoofAttempts = v.Q_NoofAttempts ?? 0,
                MarkAsCompleted = v.MarkAsCompleted,
                CompletionDate = v.CompletionDate,
                bDeleted = v.bDeleted
                
            };
        }
        public static implicit operator BrainFitness(BrainFitnessVM v)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            return new BrainFitness
            {
                BrainFitnessID = v.BrainFitnessID,
                UserId = v.UserId,
                StartingNumber = v.StartingNumber,
                Q_Add = v.Q_Add,
                Q_Take = v.Q_Take,
                Q_A = v.Q_A,
                Q_NoofAttempts = v.Q_NoofAttempts ?? 0,
                MarkAsCompleted = v.MarkAsCompleted,
                CompletionDate = v.CompletionDate,
                bDeleted = v.bDeleted
            };

        }
    }
    public class SubBrainFitnessVM
    {
        public int BrainFitnessID { get; set; }
        public int SubBrainFitnessID { get; set; }
        public int StartingNumber { get; set; }
        public Nullable<int> Q_Add { get; set; }
        public Nullable<int> Q_Take { get; set; }
        public Nullable<int> Q_A { get; set; }
        public string _a { get; set; }
        public string _t { get; set; }
        public int _UI { get; set; }
        public Nullable<int> Q1_NoofAttempts { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<bool> MarkAsCompleted { get; set; }
        public Nullable<bool> bDeleted { get; set; }

    }
}