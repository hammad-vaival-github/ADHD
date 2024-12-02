using DotNet.Highcharts.Options;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using waats.Models;
using waats.Models.data;
using waats.ViewModel;
namespace waats.Classes
{
    public class ManageQueries
    {
        private waatsEntities db = new waatsEntities();
        public WaatsFormVM ReturnWaatsForm(int FormId=0)
        {
            WaatsFormVM vm = new WaatsFormVM();
            var t = db.waatsForm.Find(FormId);
            if(t!=null)
            {
                vm = t;                
            }           
            return vm;
        }
        public int InsertFormData_U(WaatsFormVM vm)
        {
            waatsForm w = vm;
            w.Completedby = "developer";
            w.CompletionDate = DateTime.Now.ToString();
            db.waatsForm.Add(w);
            try
            {
                db.SaveChanges();
                int t = w.localID;
                AddLogs(t, "Waats form saved");
                return t;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return 0;
            }
        }
        public int UpdateFormData_U(int localId,string Action,string Description)
        {
            var t = db.waatsForm.Find(localId);
            if (t != null)
            {
                t.Action = Action;
                db.Entry(t).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                AddLogs(localId, Description);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<GratitudeJournal> ReturnGratitudeJournal(string UserId = null)
        {
            var t = db.GratitudeJournal.Where(x => x.UserId == UserId);
            if (t != null)
            {
                return t.ToList();
            }
            return null;
        }
        public List<MindFullAttention> ReturnMindFullAttention(string UserId = null)
        {
            var t = db.MindFullAttention.Where(x => x.UserId == UserId);
            if (t != null)
            {
                return t.ToList();
            }
            return null;
        }
        public List<MemoryMaker> ReturnMemoryMaker(string UserId = null)
        {
            var t = db.MemoryMaker.Where(x => x.UserId == UserId);
            if (t != null)
            {
                return t.ToList();
            }
            return null;
        }
        public List<Trigger> ReturnTriggers(string UserId = null)
        {
            var t = db.Trigger.Where(x => x.UserId == UserId);
            if (t != null)
            {
                return t.ToList();
            }
            return null;
        }
        public List<BrainFitness> ReturnBrainFitness(string UserId = null)
        {
            var t = db.BrainFitness.Where(x => x.UserId == UserId);
            if (t != null)
            {
                return t.ToList();
            }
            return null;
        }
        public BrainFitnessVM CreateBrainFitness()
        {
            Random rng = new Random();
            BrainFitnessVM vm = new BrainFitnessVM();
            for(int i=0;i<5;i++)
                
            vm=SubCreateBrainFitness(rng.Next(100, 1000), rng.Next(1, 10), rng.Next(1, 10));
            return vm;
        }
        public BrainFitnessVM SubCreateBrainFitness(int stn=345,int _aa=4,int _tt = 7)
        {
            BrainFitnessVM vm = new BrainFitnessVM();
            Random rng = new Random();
            int StartingNumber = stn;
            int _add = _aa;
            int _take = _tt;
            vm.StartingNumber = StartingNumber;
            vm.Q_Add = _add;
            vm.Q_Take = _take;
            vm.Q_A = (StartingNumber + (_add * _add))-(_take * _take);
            string a = "Add "+ _add + ", "+ _add + " times";
            string t = "Take " + _take + ", " + _take + " times";
            vm.Q_NoofAttempts = 0;
            vm._a = a;
            vm._t = t;
            return vm;
        }
        //DashboardTasksPercentage
        public DashboardTasksPercentage DashboardTasksPercentage(string UserId = null)
        {
            DashboardTasksPercentage _dashboardTasksPercentage = new DashboardTasksPercentage();
            var scheduleTasks = db.ScheduleTask
                                .Where(x => x.UserId == UserId)
                                .GroupBy(x => x.MarkAsCompleted)
                                .Select(group => new
                                {
                                    MarkAsCompleted = group.Key,
                                    Count = group.Count(),
                                    Records = group.ToList()
                                })
                                .ToList();
            if (scheduleTasks.Count() > 0)
            {
                var allRecords = scheduleTasks.SelectMany(g => g.Records).ToList();
                var trueCount = scheduleTasks.FirstOrDefault(g => g.MarkAsCompleted == true)?.Count ?? 0;
                int schedulePercentage = (int)Math.Ceiling((trueCount / (double)allRecords.Count) * 100);
                _dashboardTasksPercentage.ScheduleTasksPercentage = schedulePercentage;
            }
            else
            {
                _dashboardTasksPercentage.ScheduleTasksPercentage = 0;
            }

            var brainFitness = db.BrainFitness
                                .Count(x => x.UserId == UserId && x.MarkAsCompleted == true);
            if (brainFitness > 0)
            {
                int brainFitnessPercentage = (int)Math.Ceiling((brainFitness / (double)brainFitness) * 100);
                _dashboardTasksPercentage.BrainFitness = brainFitnessPercentage;
            }
            else 
            {
                _dashboardTasksPercentage.BrainFitness = 0;
            }

            var spottingTriggers = db.Trigger
                                .Count(x => x.UserId == UserId && x.MarkAsCompleted == true);
            if (spottingTriggers > 0)
            {
                int spottingTriggersPercentage = (int)Math.Ceiling((spottingTriggers / (double)spottingTriggers) * 100);
                _dashboardTasksPercentage.SpottingTriggers = spottingTriggersPercentage;
            }
            else 
            {
                _dashboardTasksPercentage.SpottingTriggers = 0;
            }

            var memoryMaker = db.MemoryMaker
                              .Count(x => x.UserId == UserId && x.MarkAsCompleted == true);
            if (memoryMaker > 0)
            {
                int memoryMakerPercentage = (int)Math.Ceiling((memoryMaker / (double)memoryMaker) * 100);
                _dashboardTasksPercentage.MemoryMaker = memoryMakerPercentage;
            }
            else
            {
                _dashboardTasksPercentage.MemoryMaker = 0;
            }

            var mindfulAttention = db.MindFullAttention
                              .Count(x => x.UserId == UserId && x.MarkAsCompleted == true);
            if (mindfulAttention > 0)
            {
                int mindfulAttentionPercentage = (int)Math.Ceiling((mindfulAttention / (double)mindfulAttention) * 100);
                _dashboardTasksPercentage.MindfulAttention = mindfulAttentionPercentage;
            }
            else 
            {
                _dashboardTasksPercentage.MindfulAttention = 0;
            }

            var gratitudeJournal = db.GratitudeJournal
                              .Count(x => x.UserId == UserId && x.MarkAsCompleted == true);
            if (gratitudeJournal > 0)
            {
                int gratitudeJournalPercentage = (int)Math.Ceiling((gratitudeJournal / (double)gratitudeJournal) * 100);
                _dashboardTasksPercentage.GratitudeJournal = gratitudeJournalPercentage;
            }
            else
            {
                _dashboardTasksPercentage.GratitudeJournal = 0;
            }

            return _dashboardTasksPercentage;
        }
        public List<ScheduleTask> ReturnScheduleTasks(string UserId = null)
        {
            var t = db.ScheduleTask.Where(x=>x.UserId==UserId);
            if (t != null)
            {
                return t.ToList();
            }
            return null;
        }
        public List<SubScheduleTask> ReturnScheduleSubTasks(int MainTaskID, string UserId = null)
        {
            var t = db.SubScheduleTask.Where(x => x.UserId == UserId && x.MainTaskID== MainTaskID);
            if (t != null)
            {
                return t.ToList();
            }
            return null;
        }
        public ScheduleTaskVM ReturnScheduleTask(int TaskID = 0, string UserID=null)
        {
            ScheduleTaskVM vm = new ScheduleTaskVM();
            var t = GetTask(TaskID,UserID);
            if (t != null)
            {
                vm = t;
            }
            return vm;
        }
        public int InsertScheduleTask(ScheduleTaskVM vm,string UserId,string Username)
        {
            ScheduleTask w = vm;
            w.UserId = UserId;
            w.MarkAsCompleted = false;
            w.SubTask = false;
            w.bDeleted = false;
            w.AddedDate = DateTime.Now;
            db.ScheduleTask.Add(w);
            try
            {
                db.SaveChanges();
                int t = w.LocalID;
                AddLogs(t, "Task scheduled successfully by Username " + Username);
                return t;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return 0;
            }
        }
        public int InsertScheduleSubTask(SubScheduleTaskVM vm, string UserId, string Username)
        {
            SubScheduleTask w = vm;
            w.UserId = UserId;
            w.MarkAsCompleted = false;
            w.MainTaskID = vm.MainTaskID;
            w.bDeleted = false;
            w.AddedDate = DateTime.Now;
            db.SubScheduleTask.Add(w);
            try
            {
                db.SaveChanges();
                int t = w.SubLocalID;
                AddLogs(t, "Sub Task scheduled successfully by Username " + Username);
                return t;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return 0;
            }
        }
        public int InsertBrainFitness(BrainFitnessVM vm, string UserId, string Username)
        {
            BrainFitness w = vm;
            w.UserId = UserId;
            w.MarkAsCompleted = true;
            w.bDeleted = false;
            w.CompletionDate = DateTime.Now;
            db.BrainFitness.Add(w);
            try
            {
                db.SaveChanges();
                int t = w.BrainFitnessID;
                AddLogs(t, "Brain Fitness saved successfully by Username " + Username);
                return t;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return 0;
            }
        }
        public int InsertTriggers(TriggerVM vm, string UserId, string Username)
        {
            Trigger w = vm;
            w.UserId = UserId;
            w.MarkAsCompleted = true;
            w.bDeleted = false;
            w.AddedDate = DateTime.Now;
            w.CompletionDate = DateTime.Now;
            db.Trigger.Add(w);
            try
            {
                db.SaveChanges();
                int t = w.TriggerID;
                AddLogs(t, "Trigger saved successfully by Username " + Username);
                return t;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return 0;
            }
        }
        public int InsertMemoryMaker(MemoryMakerVM vm, string UserId, string Username)
        {
            MemoryMaker w = vm;
            w.UserId = UserId;
            w.MarkAsCompleted = true;
            w.bDeleted = false;
            w.AddedDate = DateTime.Now;
            w.CompletionDate = DateTime.Now;
            db.MemoryMaker.Add(w);
            try
            {
                db.SaveChanges();
                int t = w.memoryId;
                AddLogs(t, "Memory Maker saved successfully by Username " + Username);
                return t;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return 0;
            }
        }
        public int InsertMindFullAttention(MindFullAttentionVM vm, string UserId, string Username)
        {
            MindFullAttention w = vm;
            w.UserId = UserId;
            w.MarkAsCompleted = true;
            w.bDeleted = false;
            w.AddedDate = DateTime.Now;
            w.CompletionDate = DateTime.Now;
            db.MindFullAttention.Add(w);
            try
            {
                db.SaveChanges();
                int t = w.MindFulID;
                AddLogs(t, "Mindful attention saved successfully by Username " + Username);
                return t;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return 0;
            }
        }
        public int InsertGratitudeJournal(GratitudeJournalVM vm, string UserId, string Username)
        {
            GratitudeJournal w = vm;
            w.UserId = UserId;
            w.MarkAsCompleted = true;
            w.bDeleted = false;
            w.AddedDate = DateTime.Now;
            w.CompletionDate = DateTime.Now;
            db.GratitudeJournal.Add(w);
            try
            {
                db.SaveChanges();
                int t = w.GratitudeJouralID;
                AddLogs(t, "Gratitude Journal saved successfully by Username " + Username);
                return t;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return 0;
            }
        }
        public bool CompletedConfirmed(int id,string UserID)
        {
            ScheduleTask w = GetTask(id,UserID);
            w.MarkAsCompleted = true;
            w.CompletionDate = DateTime.Now;
            db.Entry(w).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                AddLogs(id, "Task completed successfully task id " + id);
                return true;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return false;
            }
        }
        public bool SubTaskCompletedConfirmed(int id, string UserID)
        {
            SubScheduleTask w = GetSubTask(id, UserID);
            w.MarkAsCompleted = true;
            w.CompletionDate = DateTime.Now;
            db.Entry(w).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                AddLogs(id, "Sub task completed successfully sub task id " + id);
                return true;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return false;
            }
        }
        public bool EditTask(ScheduleTaskVM vm,string Username)
        {
            ScheduleTask w = GetTask(vm.LocalID, Username);
            w.Title = vm.Title;
            w.Description = vm.Description;
            w.EditDate = DateTime.Now;
            db.Entry(w).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                AddLogs(vm.LocalID, "Task edited successfully by Username " + Username);
                return true;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return false;
            }
        }
        public bool EditSubTask(SubScheduleTaskVM vm, string Username)
        {
            SubScheduleTask w = GetSubTask(vm.SubLocalID, Username);
            w.Title = vm.Title;
            w.Description = vm.Description;
            w.EditDate = DateTime.Now;
            db.Entry(w).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                AddLogs(vm.SubLocalID, "Sub task edited successfully by Username " + Username);
                return true;
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
                return false;
            }
        }
        public ScheduleTask GetTask(int id,string UserID)
        {
            var t= db.ScheduleTask.Find(id);
            if(t!=null)
            {
                return t.UserId==UserID? t:null;
            }
            return null;
        }
        public SubScheduleTask GetSubTask(int id, string UserID)
        {
            var t = db.SubScheduleTask.Find(id);
            if (t != null)
            {
                return t.UserId == UserID ? t : null;
            }
            return null;
        }
        public void AddLogs(int localId,string Description)
        {
            Logs w = new Logs();
            w.WaatsFormId = localId.ToString();
            w.Description = Description;
            w.CreationDate = DateTime.Now.ToString();
            db.Logs.Add(w);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //throw new HttpException("An error occurred: " + ex);
            }
        }
        public RegisterViewModel getRVM(string UserGUID)
        {
            RegisterViewModel vm = new RegisterViewModel();
            var rv = db.AspNetUsers.Where(x => x.id == UserGUID).ToList();
            if (rv.Any())
            {
                
                vm.UserGUID =  new Guid(rv.First().id);
                vm.Activationcode = rv.First().Activationcode;
                vm.Email = rv.First().Email;
                vm.FirstName = rv.First().Firstname;
                vm.Surname = rv.First().Lastname;
                vm.Dob = rv.First().Dob;
                vm.Gender = rv.First().Gender;
                vm.EthnicOrigin = rv.First().EthnicOrigin;
                vm.EmploymentStatus = rv.First().EmploymentStatus;
                vm.ProfilePic = rv.First().ProfilePic?? false;
                vm.ConsentToSharedata = rv.First().ConsentToSharedata ?? false;
                vm.AgreeToADHD = rv.First().AgreeToADHD ?? false;
            }
             return vm;

        }
        public bool UploadWaiverDoc(HttpPostedFileBase file,string uGuid)
        {
            if (file != null)
            {
                    string FileExtension = Path.GetExtension(file.FileName);
                    if (FileExtension.ToLower() == ".jpg"
                        || FileExtension.ToLower() == ".png"
                        || FileExtension.ToLower() == ".gif"
                        || FileExtension.ToLower() == ".jpeg")
                    {
                    ///string int = getUserGuid(userId);
                    var validate = db.ProfilePicT.Where(x=>x.UserGUID== uGuid).ToList();
                        bool response = validate != null && validate.Any() ? true : false;
                        if (!response)
                        {
                            AddProfilePic(file, uGuid);
                        }
                        else
                        {
                            DeletProfilePic(file, uGuid, validate);
                        }
                    }
                return true;
            }
            return false;
        }
        public bool AddProfilePic(HttpPostedFileBase file, string UserGUID = null)
        {
            try
            {
                string FileType = file.ContentType;
                string FileName = Path.GetFileName(file.FileName);
                string FileExtension = Path.GetExtension(file.FileName);
                Stream FileData = file.InputStream;
                int FileLength = file.ContentLength;
                byte[] fileData = new byte[FileLength];
                FileData.Read(fileData, 0, FileLength);
                ProfilePicT ppObj = new ProfilePicT
                {
                    UserGUID = UserGUID,
                    FileType = FileType,
                    FileName = FileName.Replace(" ", "_").Replace("&", "_"),
                    FileData = fileData,
                    FileUploadDateTime = DateTime.Now
                };
                db.ProfilePicT.Add(ppObj);
                ////db.Entry(WaiverUploadFilesAttachedFileObj).State = EntityState.Detached;
                db.SaveChanges();
                ///AddLogs(getUserGuid(UserGUID), "Saving user profile pic for user id " + UserGUID);
                return true;
            }
            catch (Exception ex)
            {
               // AddLogs(getUserGuid(UserGUID), "Exception when saving user profile pic for user id " + UserGUID +"Exception detials :"+ ex);
                return true;
            }
        }
        public bool DeletProfilePic(HttpPostedFileBase file, string UserGUID = null, List<ProfilePicT> p=null)
        {
            try
            {
                ProfilePicT ppObj = p.FirstOrDefault();
                {
                    ppObj.bDeleted = true;
                };
                db.Entry(ppObj).State = EntityState.Detached;
                db.SaveChanges();
               /// AddLogs(getUserGuid(UserGUID), "deleting profile pic for user id " + UserGUID);
                AddProfilePic(file, UserGUID);
                return true;
            }
            catch (Exception ex)
            {
               // AddLogs(getUserGuid(UserGUID), "Exception when saving user profile pic for user id " + UserGUID + "Exception detials :" + ex);
                return false;
            }
        }
        //public int getUserGuid(string UserGUID)
        //{
        //    var temp = db.AspNetUsers.Where(x => x.id == UserGUID).Select(x => x.LocalId).ToList();
        //    if (temp.Any())
        //    {
        //        return temp.First();
        //    }
        //    else return 0;
        //}


        public byte[] HtmltoPDF(string htmlToConvert, string FileName)
        {
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
            try
            {
                // instantiate the HTML to PDF converter


                htmlToPdfConverter.CustomWkHtmlArgs = "--enable-local-file-access --print-media-type --title \"SMS Script " + FileName + "\" --dpi 300 --disable-smart-shrinking";
                ////--print-media-type --title \"SMS Script " + FileName + "\" --dpi 300 --disable-smart-shrinking";
                // htmlToPdfConverter.PageHeight = 215;
                //// htmlToPdfConverter.PageWidth = 176;

                var margins = new PageMargins();
                margins.Bottom = 20;
                margins.Top = 20;
                margins.Left = 5;
                margins.Right = 5;
                htmlToPdfConverter.Margins = margins;
                htmlToPdfConverter.Orientation = PageOrientation.Portrait;
                htmlToPdfConverter.Size = PageSize.A4;
                htmlToPdfConverter.LowQuality = false;
                htmlToPdfConverter.PageHeaderHtml = $@"<div style='border-bottom: 1px solid #e5e5e5;'><div  style='text-align: center;'> <img class=""navbar-brand"" src=""D:\Project\Source\Waats\waats\images\adhd.png"" style=""width:150px;height:60px;text-align: left;"" /><br/><strong>ADHD</strong></div>";
                htmlToPdfConverter.PageFooterHtml = $@"<div style='border-bottom: 1px solid #e5e5e5;text-align: left;'><strong>WAATS</strong></div>";
                // render the HTML code as PDF in memory
                byte[] pdfBuffer = htmlToPdfConverter.GeneratePdf(htmlToConvert);

                //FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");///code to return PDF
                //var path = Path.Combine(Server.MapPath("~/uploads"),"HR.pdf");
                var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/PDFs/" + FileName + ".pdf");
                var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                fileStream.Write(pdfBuffer, 0, pdfBuffer.Length);
                fileStream.Close();               
                return pdfBuffer;
            }
            catch (Exception ex)
            {
                htmlToPdfConverter.Quiet = false;
                htmlToPdfConverter.LogReceived += (sender, e) => {
                    Console.WriteLine("WkHtmlToPdf Log: {0}", e.Data);
                };
                throw new HttpException("An error occurred: " + ex);
            }

        }



        public List<SelectListItem> GetSelectlist(string tableName)
        {
            if (tableName == "GenderT")
            {
                var DistinctItems = db.GenderT.ToList().GroupBy(x => x.Description).Select(y => y.First());
                return (from o in DistinctItems
                        select new SelectListItem
                        { Value = o.LocalId.ToString(), Text = o.Description }).Distinct().OrderBy(x => x.Text).ToList();
            }
            else if (tableName == "EST") {
                var DistinctItems = db.EST.ToList().GroupBy(x => x.Description).Select(y => y.First());
                return (from o in DistinctItems
                        select new SelectListItem
                        { Value = o.LocalId.ToString(), Text = o.Description }).Distinct().OrderBy(x => x.Text).ToList();
            }
            else if (tableName == "EOT") {
                var DistinctItems = db.EOT.ToList().GroupBy(x => x.Description).Select(y => y.First());
                return (from o in DistinctItems
                        select new SelectListItem
                        { Value = o.LocalId.ToString(), Text = o.Description }).Distinct().OrderBy(x => x.Text).ToList();
            }
            else return null;

            
        }
    }
}