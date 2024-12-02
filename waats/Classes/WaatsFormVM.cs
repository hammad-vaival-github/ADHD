using Foolproof;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using waats.Models.data;

namespace waats.ViewModel
{
    public class UniqueEmailAttribute : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success; // Server-side logic not required here as it will be checked with AJAX.
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = "Email already exists.",
                ValidationType = "uniqueemail"
            };
            yield return rule;
        }
    }
    public class WaatsFormVM
    {

        public int localID { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[a-zA-Z'-/_. ]*$", ErrorMessage = "Only accept a-z,A-Z,-'")]
        [StringLength(300, ErrorMessage = "! Max 300 characters")]
        [Display(Name = "* First name: ")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[a-zA-Z'-/_. ]*$", ErrorMessage = "Only accept a-z,A-Z,-'")]
        [StringLength(300, ErrorMessage = "! Max 300 characters")]
        [Display(Name = "* Surname: ")]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [StringLength(20, ErrorMessage = "! Max 20 characters")]
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "* Date of birth: ")]
        public string DoB { get; set; }

        [Required(ErrorMessage = "* Required")]
        //[StringLength(20, ErrorMessage = "! Max 20 characters")]
        [Display(Name = "* Age: ")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "* Gender: ")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "* Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [UniqueEmail]
        [Display(Name = "* Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[a-zA-Z'-/_. ]*$", ErrorMessage = "Only accept a-z,A-Z,-'")]
        [StringLength(500, ErrorMessage = "! Max 500 characters")]
        [Display(Name = "* GP name: ")]
        public string GPName { get; set; }

        [Required(ErrorMessage = "* Required")]
        ///[RegularExpression(@"^[a-zA-Z'-/_.!,@# ]*$", ErrorMessage = "Only accept a-z,A-Z,.!@#'")]
        [StringLength(500, ErrorMessage = "! Max 500 characters")]
        [Display(Name = "* Practice name: ")]
        public string PracticeName { get; set; }

        [Required(ErrorMessage = "* Required")]
        ////[RegularExpression(@"^[a-zA-Z'-/_.!,@# ]*$", ErrorMessage = "Only accept a-z,A-Z,.!@#'")]
        [StringLength(500, ErrorMessage = "! Max 500 characters")]
        [Display(Name = "* Address: ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "* Required")]
        [RegularExpression(@"^[A-Z]{1,2}[0-9R][0-9A-Z]? [0-9][A-Z]{2}$", ErrorMessage = "Invalid UK postal code")]
        [StringLength(10, ErrorMessage = "! Max 10 characters")]
        [Display(Name = "* Postcode: ")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "* Required")]
        ///[RegularExpression(@"^[a-zA-Z'-/_.!,@# ]*$", ErrorMessage = "Only accept a-z,A-Z,.!@#'")]
        [StringLength(1000, ErrorMessage = "! Max 1000 characters")]
        [Display(Name = "* Do you have any history of mental illness? If so, please outline it and any treatment you received: ")]
        public string HistoryofMentalIllness { get; set; }

        [Required(ErrorMessage = "* Required")]
        ////[RegularExpression(@"^[a-zA-Z'-/_.!,@# ]*$", ErrorMessage = "Only accept a-z,A-Z,.!@#'")]
        [StringLength(1000, ErrorMessage = "! Max 1000 characters")]
        [Display(Name = "* Were you ever referred to mental health/psychiatric services and seen by a specialist before? If so, please give details: ")]
        public string ReferredtoMentalHealth { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(1000, ErrorMessage = "! Max 1000 characters")]
        [Display(Name = "* Have you ever had any thoughts or made any attempts to harm yourself or end your life or to harm others? ")]
        public string HaveYouEverHadAnyThoughtsorMadeAttemptstoHarmYourself { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(10, ErrorMessage = "! Max 10 characters")]
        [Display(Name = "* Do you currently or have you recently had any thoughts or made any attempts to harm yourself or end your life?  ")]
        public string HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(10, ErrorMessage = "! Max 10 characters")]
        [Display(Name = "* Do you currently or have you recently had any thoughts or made any attempts to harm anyone else?  ")]
        public string HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse { get; set; }

        public bool BoolW
        {
            get
            {
                return (HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself == "No"
                    && HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse=="No");
            }
        }

        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I am able to complete projects that I start (whether for work, hobbies or home life).  ")]
        public string CompleteProjects { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "When a task requires organization I am able to organize and order things accordingly.  ")]
        public string RequiresOrganization { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I am able to keep appointments.  ")]
        public string keepAppointments { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I am able to start tasks on time including those that require some thought.  ")]
        public string StartTasksOnTime { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I fidget and squirm with my hands or feet. ")]
        public string FidgetAndSquirm { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I feel overly active and compelled to do things.  ")]
        public string OverlyActiveAndCompelled { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I make careless mistakes.  ")]
        public string MakeCarelessMistakes { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I find difficulty keeping my attention on anything I'm doing. ")]
        public string DifficultyKeepingMyAttention { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I find difficulty concentrating on what people are saying to me.  ")]
        public string DifficultyConcentrating { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I misplace or have difficulty finding things.  ")]
        public string DifficultyFinding { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I become easily distracted by noise or activity around me.  ")]
        public string EasilyDistracted { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I leave my seat when in meetings or other situations where I am expected to remain seated.  ")]
        public string LeaveMySeatInMeetings { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I have difficulty relaxing or unwinding when I have time to myself.  ")]
        public string DifficultyRelaxingOrUnwinding { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I find myself talking too much in social occasions.  ")]
        public string TalkingTooMuch { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I find myself finishing the sentences of other people when I am talking to them. ")]
        public string FinishingTheSentences { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I have difficulty waiting my turn when in a situation where turn talking is required.  ")]
        public string DifficultyWaitingMyTurn { get; set; }

        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [Display(Name = "I interrupt other people when they are busy.  ")]
        public string InterruptOtherPeople { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [StringLength(10, ErrorMessage = "! Max 10 characters")]
        [Display(Name = "18. Did you receive warnings and persistent negative reports from your teachers at school – across all or most subjects – about under performance due to lack of focus and concentration in class and/or high levels of overactive behaviour?  ")]
        public string ReceiveWarningsAndPersistent { get; set; }
        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [StringLength(10, ErrorMessage = "! Max 10 characters")]
        [Display(Name = "19. Have you ever been disciplined or sacked from work due to bad performance as a result of poor focus and concentration in your job?  ")]
        public string DisciplinedOrAacked { get; set; }

        [RequiredIf("BoolW", true, ErrorMessage = "* Required")]
        [StringLength(10, ErrorMessage = "! Max 10 characters")]
        [Display(Name = "20. Have you ever experienced a breakdown of a relationship because your partner thought you were always distracted, forgetting and ignoring them and what’s important?  ")]
        public string EverExperiencedABreakdown { get; set; }
        public string Completedby { get; set; }
        public string CompletionDate { get; set; }
        public bool flag { get; set; }
        public static implicit operator WaatsFormVM(waatsForm v)
        {
            return new WaatsFormVM
            {
                Firstname = v.Firstname,
                Surname = v.Surname,
                DoB = v.DoB,
                Age = v.Age,
                Gender = v.Gender,
                Email = v.Email,
                GPName = v.GPName,
                PracticeName=v.PracticeName,
                Address=v.Address,
                Postcode=v.Postcode,
                HistoryofMentalIllness = v.HistoryofMentalIllness,
                ReferredtoMentalHealth = v.ReferredtoMentalHealth,
                HaveYouEverHadAnyThoughtsorMadeAttemptstoHarmYourself = v.HaveYouEverHadAnyThoughtsorMadeAttemptstoHarmYourself,
                HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself = v.HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself,
                HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse = v.HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse,
                CompleteProjects = v.CompleteProjects,
                RequiresOrganization = v.RequiresOrganization,
                keepAppointments = v.keepAppointments,
                StartTasksOnTime = v.StartTasksOnTime,
                FidgetAndSquirm = v.FidgetAndSquirm,
                OverlyActiveAndCompelled = v.OverlyActiveAndCompelled,
                MakeCarelessMistakes = v.MakeCarelessMistakes,
                DifficultyKeepingMyAttention = v.DifficultyKeepingMyAttention,
                DifficultyConcentrating = v.DifficultyConcentrating,
                DifficultyFinding = v.DifficultyFinding,
                EasilyDistracted = v.EasilyDistracted,
                LeaveMySeatInMeetings = v.LeaveMySeatInMeetings,
                DifficultyRelaxingOrUnwinding = v.DifficultyRelaxingOrUnwinding,
                TalkingTooMuch = v.TalkingTooMuch,
                FinishingTheSentences = v.FinishingTheSentences,
                DifficultyWaitingMyTurn = v.DifficultyWaitingMyTurn,
                InterruptOtherPeople = v.InterruptOtherPeople,
                ReceiveWarningsAndPersistent = v.ReceiveWarningsAndPersistent,
                DisciplinedOrAacked = v.DisciplinedOrAacked,
                EverExperiencedABreakdown = v.EverExperiencedABreakdown,
                Completedby = v.Completedby,
                CompletionDate = v.CompletionDate,

            };
        }
        public static implicit operator waatsForm(WaatsFormVM v)
        {
            CultureInfo culture = new CultureInfo("en-GB");
            return new waatsForm
            {
                Firstname = v.Firstname,
                Surname = v.Surname,
                DoB = v.DoB,
                Age = v.Age,
                Gender = v.Gender,
                Email = v.Email,
                GPName = v.GPName,
                PracticeName = v.PracticeName,
                Address = v.Address,
                Postcode = v.Postcode,
                HistoryofMentalIllness = v.HistoryofMentalIllness,
                ReferredtoMentalHealth = v.ReferredtoMentalHealth,
                HaveYouEverHadAnyThoughtsorMadeAttemptstoHarmYourself = v.HaveYouEverHadAnyThoughtsorMadeAttemptstoHarmYourself,
                HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself = v.HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself,
                HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse = v.HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse,
                CompleteProjects = v.CompleteProjects,
                RequiresOrganization = v.RequiresOrganization,
                keepAppointments = v.keepAppointments,
                StartTasksOnTime = v.StartTasksOnTime,
                FidgetAndSquirm = v.FidgetAndSquirm,
                OverlyActiveAndCompelled = v.OverlyActiveAndCompelled,
                MakeCarelessMistakes = v.MakeCarelessMistakes,
                DifficultyKeepingMyAttention = v.DifficultyKeepingMyAttention,
                DifficultyConcentrating = v.DifficultyConcentrating,
                DifficultyFinding = v.DifficultyFinding,
                EasilyDistracted = v.EasilyDistracted,
                LeaveMySeatInMeetings = v.LeaveMySeatInMeetings,
                DifficultyRelaxingOrUnwinding = v.DifficultyRelaxingOrUnwinding,
                TalkingTooMuch = v.TalkingTooMuch,
                FinishingTheSentences = v.FinishingTheSentences,
                DifficultyWaitingMyTurn = v.DifficultyWaitingMyTurn,
                InterruptOtherPeople = v.InterruptOtherPeople,
                ReceiveWarningsAndPersistent = v.ReceiveWarningsAndPersistent,
                DisciplinedOrAacked = v.DisciplinedOrAacked,
                EverExperiencedABreakdown = v.EverExperiencedABreakdown,
                Completedby = v.Completedby,
                CompletionDate = v.CompletionDate
            };
        }
    }
    
}