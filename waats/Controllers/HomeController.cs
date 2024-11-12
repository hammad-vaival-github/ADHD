using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using waats.Classes;
using waats.Models;
using waats.Models.data;
using waats.ViewModel;

namespace waats.Controllers
{
    public class HomeController : Controller
    {
        private ManageQueries _Managequeries = new ManageQueries();
        public void PreparePDF(WaatsFormVM vm,string TemplateName = null,string destFileName=null,string username=null,string password=null,string ActivationCode=null)
        {
            string templateFilePath = Server.MapPath("/App_Data/Templates/"+ TemplateName+".htm");
            StringWriter sw = new StringWriter();
            

            using (StreamReader sr = new StreamReader(templateFilePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Replace("DOB", vm.DoB);
                    line = line.Replace("(first name second name)", vm.Firstname+ " "+ vm.Surname);
                    line = line.Replace("(first name)",vm.Firstname);
                    line = line.Replace("[Q1]", vm.CompleteProjects.ToLower());
                    line = line.Replace("[Q2]", vm.RequiresOrganization.ToLower());
                    line = line.Replace("[Q3]", vm.keepAppointments.ToLower());
                    line = line.Replace("[Q4]", vm.StartTasksOnTime.ToLower());
                    line = line.Replace("[Q5]", vm.FidgetAndSquirm.ToLower());
                    line = line.Replace("[Q6]", vm.OverlyActiveAndCompelled.ToLower());
                    line = line.Replace("[Q7]", vm.MakeCarelessMistakes.ToLower());
                    line = line.Replace("[Q8]", vm.DifficultyKeepingMyAttention.ToLower());
                    line = line.Replace("[Q9]", vm.DifficultyConcentrating.ToLower());
                    line = line.Replace("[Q10]", vm.DifficultyFinding.ToLower());
                    line = line.Replace("[Q11]", vm.EasilyDistracted.ToLower());
                    line = line.Replace("[Q12]", vm.LeaveMySeatInMeetings.ToLower());
                    line = line.Replace("[Q13]", vm.DifficultyRelaxingOrUnwinding.ToLower());
                    line = line.Replace("[Q14]", vm.TalkingTooMuch.ToLower());
                    line = line.Replace("[Q15]", vm.FinishingTheSentences.ToLower());
                    line = line.Replace("[Q16]", vm.DifficultyWaitingMyTurn.ToLower());
                    line = line.Replace("[Q17]", vm.InterruptOtherPeople.ToLower());
                    line = line.Replace("[Q18]", vm.ReceiveWarningsAndPersistent=="Yes"? "did" : "did not");
                    line = line.Replace("[Q19]", vm.DisciplinedOrAacked == "Yes" ? "was" : "was not");
                    line = line.Replace("[Q20]", vm.EverExperiencedABreakdown == "Yes" ? "has" : "has not");

                    //Q18 + ONE OR BOTH of Q19 and Q20 is “yes
                    if (vm.ReceiveWarningsAndPersistent == "Yes" &&
                        (vm.DisciplinedOrAacked == "Yes" || vm.EverExperiencedABreakdown == "Yes")   ///IS/IS NOT
                      )
                    {
                        line = line.Replace("[Q18-20_A]","is");//Q18 + ONE OR BOTH of Q19 and Q20 is “yes
                    }
                    else
                    {
                        line = line.Replace("[Q18-20_A]", "is not");
                    }


                    //Q18 No 
                    if (vm.ReceiveWarningsAndPersistent == "No")
                    {
                        line = line.Replace("[Q18-20_B]", "be removed from the waiting list by sending them the attached discharge letter, with details of our free ADHD\r\n\t\t\t\t\t\t\tAssistant app, which they can then use to continue working on the underlying\r\n\t\t\t\t\t\t\ttraits of ADHD which (first name) does, nevertheless, possess and so will\r\n\t\t\t\t\t\t\tbenefit from this input."); //Q18 No 
                    }
                    else
                    {
                       line = line.Replace("[Q18-20_B]", "remain on the waiting list for a further assessment from your team."); //Q18 No
                                                                                                                                 
                    }
                    //Q18 yes” + BOTH Q18 and Q19 are “no” 
                    if (vm.ReceiveWarningsAndPersistent == "Yes" &&
                        (vm.DisciplinedOrAacked == "No" && vm.EverExperiencedABreakdown == "No")
                      )
                    {
                        line = line.Replace("[Q18-20_B]", "be removed from the waiting list by sending them the attached discharge letter, with details of our free ADHD\r\n\t\t\t\t\t\t\tAssistant app, which they can then use to continue working on the underlying\r\n\t\t\t\t\t\t\ttraits of ADHD which (first name) does, nevertheless, possess and so will\r\n\t\t\t\t\t\t\tbenefit from this input.");//Q18 yes” + BOTH Q18 and Q19 are “no” 
                    }
                    else
                    {
                        line = line.Replace("[Q18-20_B]", "remain on the waiting list for a further assessment from your team.");//Q18 yes” + BOTH Q18 and Q19 are “no” 
                        
                    }
                    line = line.Replace("[Patient Name]", vm.Firstname + " " + vm.Surname);
                    line = line.Replace("[Patient address]", vm.Address);
                    line = line.Replace("[Link]", "http://localhost/");
                    line = line.Replace("[Username]", username);
                    line = line.Replace("[Password]", password);
                    line = line.Replace("[code]", ActivationCode);
                    /// line = line.Replace("[Patient address]", textBoxInspBy.Text);
                    ///line = line.Replace("[Patient address]", textBoxInspBy.Text);

                    ////line = line.Replace("<Insp Date>", DateTime.Now.ToShortDateString());

                    sw.WriteLine(line);
                }
            }
            sw.Close();
            _Managequeries.HtmltoPDF(sw.ToString(), destFileName);
        }
        public void PreparePDF1()
        {
            string templateFilePath = Server.MapPath("/App_Data/Templates/LETTER TO PATIENT.htm");
            StringWriter sw = new StringWriter();


            using (StreamReader sr = new StreamReader(templateFilePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {


                    sw.WriteLine(line);
                }
            }
            sw.Close();
            _Managequeries.HtmltoPDF(sw.ToString(), "test");
        }
        public ActionResult Index(int FormId = 0)
        {
            ////PreparePDF1();
            var t=_Managequeries.ReturnWaatsForm(FormId);
            return View("_Index",t);
        }
        [HttpPost]
        public ActionResult Index(WaatsFormVM vm)
        {
            vm.flag = true;
            var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            var ErrorDictionary = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToList()
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToList());
            if (ModelState.IsValid)
            {
                int loclID = _Managequeries.InsertFormData_U(vm);
                if (loclID != 0)
                {
                    string name = vm.Firstname + " " + vm.Surname;
                    TempData["WFVM"] = vm;
                    return RedirectToAction("TriggerAdvice", "Home",new
                    {
                        vm.HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself, //q4d
                        vm.HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse, //q4e
                        vm.ReceiveWarningsAndPersistent,   //q18
                        vm.DisciplinedOrAacked,   //q19
                        vm.EverExperiencedABreakdown, //q20
                        name,vm.Email,vm.DoB,
                        loclID
                    }
                        );
                }
                else return View("_Index", vm);
            }
            else
            {
                return View("_Index", vm);
            }
        }
        public ActionResult TriggerAdvice(
            string HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself, //q4d
            string HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse, //q4e
            string ReceiveWarningsAndPersistent,   //q18
            string DisciplinedOrAacked,   //q19
            string EverExperiencedABreakdown, //q20
            string name,
            string Email,
            string DoB,
            int loclID
            )
        {
            //4 d) AND/OR 4 e) is “yes
            if (HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself == "Yes" ||
               HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse == "Yes")
            {
                _Managequeries.AddLogs(loclID,"Q4 d and/or e is yes");
                return RedirectToAction("AdviceQ4_D_E",new { name,  Email,  DoB, loclID });//4 d) AND/OR 4 e) is “yes
            }

            //Q18 + ONE OR BOTH of Q19 and Q20 is “yes
            if (ReceiveWarningsAndPersistent == "Yes" &&
                (DisciplinedOrAacked == "Yes" || EverExperiencedABreakdown == "Yes")
              )
            {
                _Managequeries.AddLogs(loclID, "Q18 + ONE OR BOTH of Q19 and Q20 is “yes");
                _Managequeries.AddLogs(loclID, "Preparing " + name + " report for the clinicians");
                var model = TempData["WFVM"] as WaatsFormVM;
                PreparePDF(model, "ADHD Triage Report", "ADHD_Triage_Report_"+ name, null, null, null);
                return RedirectToAction("AdviceQ_18Y_19Y_20Y",new { name, Email, DoB, loclID });//Q18 + ONE OR BOTH of Q19 and Q20 is “yes
            }


            //Q18 No 
            if (ReceiveWarningsAndPersistent == "No")
            {
                _Managequeries.AddLogs(loclID, "Q18 No");
                _Managequeries.AddLogs(loclID, "Preparing "+ name + " report for the clinicians");
                _Managequeries.AddLogs(loclID, "Preparing letter for the patient " + name);
                var model = TempData["WFVM"] as WaatsFormVM;
                PreparePDF(model, "ADHD Triage Report", "ADHD_Triage_Report_" + name,null,null,null);
                AccountController ac = new AccountController();
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                string activC = new Random().Next(100, 1000).ToString();
                List<string> t =ac.RegisterViaADHD(model, manager, activC);
                string username = t[0];
                string password = t[1];
                PreparePDF(model, "LETTER TO PATIENT", "LETTER_TO_PATIENT_" + name,username,password, activC);
                return RedirectToAction("AdviceQ_18N_19Y_20Y", new { name, Email, DoB, loclID }); //Q18 No 
            }
            //Q18 yes” + BOTH Q18 and Q19 are “no” 
            if (ReceiveWarningsAndPersistent == "Yes" &&
                (DisciplinedOrAacked == "No" && EverExperiencedABreakdown == "No")
              )
            {
                _Managequeries.AddLogs(loclID, "Q18 yes” + BOTH Q18 and Q19 are “no”");
                _Managequeries.AddLogs(loclID, "Preparing " + name + " report for the clinicians");
                _Managequeries.AddLogs(loclID, "Preparing letter for the patient " + name);
                var model = TempData["WFVM"] as WaatsFormVM;
                AccountController ac = new AccountController();
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                string activC = new Random().Next(100, 1000).ToString();
                List<string> t = ac.RegisterViaADHD(model, manager, activC);
                string username = t[0];
                string password = t[1];
                PreparePDF(model, "LETTER TO PATIENT", "LETTER_TO_PATIENT_" + name, username, password, activC);
                PreparePDF(model, "ADHD Triage Report", "ADHD_Triage_Report_" + name,null, null,null);                
                return RedirectToAction("AdviceQ_18N_19Y_20Y", new { name, Email, DoB, loclID });//Q18 yes” + BOTH Q18 and Q19 are “no” 
            }
            return View("_ThankYou");
        }
        public ActionResult AdviceQ4_D_E(string name, string Email, string DoB, int loclID = 0) 
        {
            string EmailTo = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailTo"];
            string nameDOB = name + " ( " + DoB + " )";
            string subject = "Urgent Referral for " + nameDOB;           
            MvcHtmlString mailBody = MvcHtmlString.Create("<font face='Calibri'>Dear colleague, Re: " + nameDOB + ",<br />" + "<br />" +
                " During an ADHD triage assessment today (" + name + ") wrote that they " +
                "have recent or current thoughts to harm themselves or others. " + "<br/>" +
                "This has therefore flagged up as a risk on our system and we recommend seeing this patient urgently for their mental health. " + "<br/>" +
                "Clearly the issues experienced go deeper " +
                "than ADHD alone and there are risk issues involved, so an urgent assessment by your team will be needed. " +
                "<br/>" + "<br/>" +
                "Thanks in advance for your cooperation" + "</font>");
            EmailManager e = new EmailManager();
            e.SendEmail(EmailTo,Email, subject, mailBody);
            _Managequeries.AddLogs(loclID, "Urgent Referral for " + nameDOB);
            return View();
        }
        public ActionResult AdviceQ_18Y_19Y_20Y(string name, string Email, string DoB, int loclID = 0) 
        {
            _Managequeries.UpdateFormData_U(loclID, "OUTPUT 2 ==> Place patient back on waiting list", "OUTPUT 2 ==> Place patient back on waiting list");
            AdviceQ_18Y_19Y_20Y_Email_to_Clician(name, Email, DoB, loclID);///"Keep "+ nameDOB + " on waiting list"
            return View();
        }
        public bool AdviceQ_18Y_19Y_20Y_Email_to_Clician(string name, string Email, string DoB, int loclID = 0)
        {
            string EmailTo = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailTo"];
            string nameDOB = name + " ( " + DoB + " )";
            string subject = "Keep "+ nameDOB + " on waiting list" ;
            MvcHtmlString mailBody = MvcHtmlString.Create("<font face='Calibri'>Dear colleague, Re: " + nameDOB + ",<br />" + "<br />" +
                name + " has undergone our triage process and we believe that "+ name + " may well meet the threshold for an ADHD diagnosis." +
                " As such we suggest they remain on the waiting list for your ADHD service. Details of their triage are attached. " +
                "They remain on your waiting list, as before, so please contact them accordingly at the appropriate time. <br/> " +

                "ADHD TRIAGE REPORT ATTACHED<br/><br/> " +
                "They do have a diagnosis and need to remain on the waiting list."+
                "<br/>" + "<br/>" +
                "Thanks in advance for your cooperation" + "</font>");
            EmailManager e = new EmailManager();
            e.SendEmail(EmailTo, Email, subject, mailBody);
            _Managequeries.AddLogs(loclID, "Keep " + nameDOB + " on waiting list email");
            return true;
        }
        public ActionResult AdviceQ_18N_19Y_20Y(string name, string Email, string DoB, int loclID = 0)
        {
            ViewBag.loclID = name;
            ViewBag.loclID = Email;
            ViewBag.loclID = DoB;
            ViewBag.loclID = loclID;
            AdviceQ_18N_19Y_20Y_Email_to_Clician(name, Email, DoB, loclID);//discharge email to clinicain
            return View();
        }
        public bool AdviceQ_18N_19Y_20Y_Email_to_Clician(string name, string Email, string DoB, int loclID = 0)
        {
            string EmailTo = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailTo"];
            string nameDOB = name + " ( " + DoB + " )";
            string subject = "Discharged " + nameDOB + " from your waiting list";
            MvcHtmlString mailBody = MvcHtmlString.Create("<font face='Calibri'>Dear colleague, Re: " + nameDOB + ",<br />" + "<br />" +
                " Having completed a detailed triage, our system does not assess "+ name + " to currently meet the threshold for a diagnosis of ADHD." +
                " Please find our full triage report attached. Accordingly, we believe that "+ name + " can now be discharged from your waiting list and" +
                " we have drafted a letter to this effect, which you can now forward to your admin to send "+ name + " to communicate this to them directly," +
                " should you be in agreement. The draft letter is also attached. " +

                "ADHD TRIAGE REPORT ATTACHED<br/> " +
                "LETTER TO PATIENT ATTACHED<br/><br/> " +
                "They DO NOT have a diagnosis and can be sent a discharge letter with details of the ADHD Assistant app in it." +
                "<br/>" + "<br/>" +
                "Thanks in advance for your cooperation" + "</font>");
            EmailManager e = new EmailManager();
            e.SendEmail(EmailTo, Email, subject, mailBody);
            _Managequeries.AddLogs(loclID, "Discharged " + nameDOB + " from your waiting list");
            return true;
        }
        /*
        [HttpPost]
        public ActionResult AdviceQ_18N_19Y_20Y(string submit, string name, string Email, string DoB, int loclID = 0)
        {
            if(submit=="Yes")
            {
                _Managequeries.UpdateFormData_U(loclID, "OUTPUT 3 ==> Yes button was pressed==> Take patient off the waiting list and patient would like to download app will be supported by via app",
                    "OUTPUT 3 ==> Yes button was pressed==> Take patient off the waiting list and patient would like to download app will be supported by via app");
                return RedirectToAction("AdviceQ_18N_19Y_20Y_Sub_Yes", new { name, Email, DoB, loclID });
            }
            else
            {
                _Managequeries.UpdateFormData_U(loclID, "OUTPUT 3 ==> No button was pressed==> Patient prefer to continue on our waiting list to be seen by a clinician to get another opinion and patient will email.",
                    "OUTPUT 3 ==> No button was pressed==> Patient prefer to continue on our waiting list to be seen by a clinician to get another opinion and patient will email.");
                return RedirectToAction("AdviceQ_18N_19Y_20Y_Sub_No", new { name, Email, DoB, loclID });
            }
        }
        public ActionResult AdviceQ_18N_19Y_20Y_Sub_Yes(string name, string Email, string DoB, int loclID = 0)
        {
            string nameDOB = name + " ( " + DoB + " )";
            string subject = "ADHD Assistant app instructions for " + nameDOB;
            MvcHtmlString mailBody = MvcHtmlString.Create("<font face='Calibri'>Dear " + name + ",<br />" + "<br />" + 
                "<p class="fw-normal">" +
                "Thank you for taking part in your online assessment process, as a result of which you will now " +
                "receive a programme of structured psychological support from our specialist app; 	ADHD Assistant"
                + "</p>" +
                "<p class="fw-normal">" +
                " Instructions for the ADHD Assistant app " + "</p>" +
                "<p class="fw-normal">" +
                    "Please go to one of the links below, depending on the device you use" +
                    " and from there, download the app." +
                    "</p>" +
                    "<p class="fw-normal">" +
                    "Once you have downloaded it, Enter the Activation Code:2023" +
                    "</p>" +
                    "<p class="fw-normal">" +
                    "On first log in – and every few weeks thereafter – you will be asked to complete a test." +
                    "Please complete as instructed and, once you do, you will then be taken to the main app." +
                    "</p>" +
                    "<p class="fw-normal">" +
                    "Inside the app you will find a list of “7 Commitments”. These are exercises that" +
                    "you should do every day and they are divided into morning and afternoon " +
                    "exercises. As you complete the exercises, your performance will be recorded in" +
                    "the stats tab. There,you can see how well you are doing in terms of completing the exercises each day." +
                    "</p>" +
                    "<p class="fw-normal">" +
                    "These exercises are specially designed to help you work on the core areas of deficit " +
                    "that people who have ADHD Traits often demonstrate. By working on them in this way, every day, " +
                    "you will likely reduce the impact these deficits have in your 	life over time, gradually rewiring and recalibrating your responses and behaviours." +
                    "The key is persistence. In order to help you keep on track, the app will send you notifications a couple of times a day and, for this reason, " +
                    "we ask you to keep the app notifications on. If you work with it, you will start to notice a difference over time." +
                    "</p>" +
                    "<p class="fw-normal">" +
                    "I will also forward these instructions to you by email. Good luck and we very much " +
                    "hope that you will be able to utilise and persevere with the support provided and " +
                    "consequently see improvements in the future. Thank you again for taking the time " +
                    "to engage in this assessment process and we wish you all the very best for the future." +
                    "</p>"  +
                    "<p class="fw-normal">" +
                    "<a href = 'https://apps.apple.com/app/id6450215525' > Download ADHD Assistant app </a> " +
                    "</p> "+" </font>");
            EmailManager e = new EmailManager();
            e.SendEmail(Email, null, subject, mailBody);
            _Managequeries.AddLogs(loclID, "ADHD Assistant app instructions for " + nameDOB);
            return View();
        }
        public ActionResult AdviceQ_18N_19Y_20Y_Sub_No(string name, string Email, string DoB, int loclID = 0)
        {
            string EmailTo_No_Button = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailTo_No_Button"];
            ViewBag.EmailTo_No_Button = EmailTo_No_Button;
            ViewBag.loclID = name;
            ViewBag.loclID = Email;
            ViewBag.loclID = DoB;
            ViewBag.loclID = loclID;
            return View();
        }
        [HttpPost]
        public ActionResult AdviceQ_18N_19Y_20Y_Sub_No(string submit, string name, string Email, string DoB, int loclID = 0)
        {
            string nameDOB = name + " ( " + DoB + " )";
            string subject = "I wish to remain on the waiting list " + nameDOB;
            MvcHtmlString mailBody = MvcHtmlString.Create("<font face='Calibri'>" +
                "<p class="fw-normal">" +
                "I wish to remain on the waiting list. " +
                "</p> " + " </font>");
            EmailManager e = new EmailManager();
            e.SendEmail(Email, null, subject, mailBody);
            _Managequeries.AddLogs(loclID, "I wish to remain on the waiting list " + nameDOB);
            return View("_ThankYou");
        }*/
        public ActionResult ThankYou()
        {
            return View("_ThankYou");
        }

    }
}