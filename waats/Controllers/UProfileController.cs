using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using waats.Classes;
using waats.Models;

namespace waats.Controllers
{
    [Authorize]
    public class UProfileController : Controller
    {
        public ManageQueries _q = new ManageQueries();
        public string GetuserId()
        {
            var context = new ApplicationDbContext();
            var username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                var user = context.Users.SingleOrDefault(u => u.Email == username);
                return user.Id;
            }
            return null;
        }

        // GET: UProfile
        public ActionResult Index()
        {
            var result = _q.DashboardTasksPercentage(GetuserId());
            DashboardTasksPercentage _dashboardTasksPercentage = new DashboardTasksPercentage();
            _dashboardTasksPercentage = result;
            return View(_dashboardTasksPercentage);
        }
        public ActionResult SchedulingTasksList()
        {
            
            return View("_SchedulingtasksList");
        }
        public ActionResult GetUsersTaskList(DataTableAjaxPostModel dtParams)
        {
            var searchValue = Request["search[value]"];
            var take = dtParams.length;
            var skip = dtParams.start;
            var resultData = _q.ReturnScheduleTasks(GetuserId());
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                resultData = resultData.Where(x =>
                                (x.UserId == GetuserId() && x.Title != null && x.Title.ToString().StartsWith(searchValue))
                             ).OrderByDescending(x => x.AddedDate).ThenBy(n => n.MarkAsCompleted==false).ToList();

            }
            else {
                resultData = resultData.Where(x =>
                             (x.UserId == GetuserId() && x.Title != null)).OrderByDescending(x => x.AddedDate).ThenBy(n => n.MarkAsCompleted==false).ToList();
            }

            int recordsTotal = resultData.Count();
            var data = resultData.Skip(skip).Take(take).ToList();

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            var resultDataN = new
            {
                dtParams.draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(resultDataN),
                ContentType = "application/json"
            };

            return result;
        }
        public ActionResult ScheduleNewTask()
        {
            ScheduleTaskVM vm = new ScheduleTaskVM();
            return View("_ScheduleNewTask", vm);
        }
        //New task
        [HttpPost]
        public ActionResult ScheduleNewTask(ScheduleTaskVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_ScheduleNewTask", vm);
            }
            else
            {
                var t= _q.InsertScheduleTask(vm, GetuserId(), User.Identity.Name);
                return Json("Task saved successfully", JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult EditTask(int localID)
        {
            var t = _q.GetTask(localID, GetuserId());
            ScheduleTaskVM vm = new ScheduleTaskVM();
            vm = t;
            return View("_ScheduleNewTask", vm);
        }
        [HttpPost]
        public ActionResult EditTask(ScheduleTaskVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_ScheduleNewTask", vm);
            }
            else
            {
                var t = _q.EditTask(vm, GetuserId());
                return Json("Task saved successfully", JsonRequestBehavior.AllowGet);
            }

        }
        // sub task 
        public ActionResult SubScheduleNewTask(int MainID)
        {
            SubScheduleTaskVM vm = new SubScheduleTaskVM();
            vm.MainTaskID = MainID;
            return View("_SubScheduleNewTask", vm);
        }
        //New task
        [HttpPost]
        public ActionResult SubScheduleNewTask(SubScheduleTaskVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_SubScheduleNewTask", vm);
            }
            else
            {
                var t = _q.InsertScheduleSubTask(vm, GetuserId(), User.Identity.Name);
                return Json("Task saved successfully", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult EditSubTask(int localID)
        {
            var t = _q.GetSubTask(localID, GetuserId());
            SubScheduleTaskVM vm = new SubScheduleTaskVM();
            vm = t;
            return View("_SubScheduleNewTask", vm);
        }
        [HttpPost]
        public ActionResult EditSubTask(SubScheduleTaskVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_ScheduleNewTask", vm);
            }
            else
            {
                var t = _q.EditSubTask(vm, GetuserId());
                return Json("Sub task saved successfully", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult MarkAsDoneConfirm(int localID)
        {
            var t = _q.GetTask(localID, GetuserId());
            ViewBag.TaskTitle = t.Title;
            ViewBag.dat = t.AddedDate;
            ViewBag.localID = localID;
            return View("_MarkAsDoneConfirm");  
        }
        public ActionResult MarkAsDoneConfirmed(int localID)
        {
            var t = _q.CompletedConfirmed(localID, GetuserId());
            if (t)
            {
                return Json("Task marked as done successfully", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("failed to mark task as done", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SubtaskMarkAsDoneConfirm(int localID)
        {
            var t = _q.GetSubTask(localID, GetuserId());
            ViewBag.MainTaskID = t.MainTaskID;
            ViewBag.SubLocalID = t.SubLocalID;
            ViewBag.TaskTitle = t.Title;
            ViewBag.dat = t.AddedDate;
            return View("_MarkAsDoneConfirm");
        }
        public ActionResult SubtaskMarkAsDoneConfirmed(int localID)
        {
            var t = _q.SubTaskCompletedConfirmed(localID, GetuserId());
            if (t)
            {
                return Json("Sub task marked as done successfully", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("failed to mark task as done", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BreakDown()
        {
            ScheduleTaskVM vm = new ScheduleTaskVM();
            return View("_ScheduleNewTask", vm);
        }
        //New task
        [HttpPost]
        public ActionResult BreakDown(ScheduleTaskVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_ScheduleNewTask", vm);
            }
            else
            {
                var t = _q.InsertScheduleTask(vm, GetuserId(), User.Identity.Name);
                return View("_Schedulingtasks");
            }

        }

        public ActionResult LoadSubTasks(DataTableAjaxPostModel dtParams,int LocalID,string UserId)
        {
            var searchValue = Request["search[value]"];
            var take = dtParams.length;
            var skip = dtParams.start;
            var resultData = _q.ReturnScheduleSubTasks(LocalID,GetuserId());
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                resultData = resultData.Where(x =>
                                (x.UserId == GetuserId() && x.Title != null && x.Title.ToString().StartsWith(searchValue))
                             ).OrderByDescending(x => x.AddedDate).ThenBy(n => n.MarkAsCompleted == false).ToList();

            }
            else
            {
                resultData = resultData.Where(x =>
                             (x.UserId == GetuserId() && x.Title != null)).OrderByDescending(x => x.AddedDate).ThenBy(n => n.MarkAsCompleted == false).ToList();
            }

            int recordsTotal = resultData.Count();
            var data = resultData.ToList();/////.Skip(skip).Take(take).ToList();

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            var resultDataN = new
            {
                dtParams.draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(resultDataN),
                ContentType = "application/json"
            };

            return result;
        }
        /// ------------------------   -------------------------MindMeditation-------------------------
        public ActionResult MindMeditation()
        {
            return View("_MindMeditation");
        }
        /// ------------------------   -------------------------Brain Fitness-----------------------------
        public ActionResult BrainFitness()
        {
            return View("_BrainFitnessMain");
        }
        public ActionResult GetBrainFitnessList(DataTableAjaxPostModel dtParams)
        {
            var searchValue = Request["search[value]"];
            var take = dtParams.length;
            var skip = dtParams.start;
            var resultData = _q.ReturnBrainFitness(GetuserId());
            resultData = resultData.OrderByDescending(x => x.CompletionDate).ThenBy(n => n.MarkAsCompleted == false).ToList();
            int recordsTotal = resultData.Count();
            var data = resultData.Skip(skip).Take(take).ToList();

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            var resultDataN = new
            {
                dtParams.draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(resultDataN),
                ContentType = "application/json"
            };

            return result;
        }
        public ActionResult AddBrainFitness()
        {
            BrainFitnessVM vm = new BrainFitnessVM();
            vm = _q.CreateBrainFitness();
            return View("_BrainFitness", vm);
        }
        [HttpPost]
        public ActionResult AddBrainFitness(BrainFitnessVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_BrainFitness", vm);
            }
            else
            {
                var t = _q.InsertBrainFitness(vm, GetuserId(), User.Identity.Name);
                return Json("Brain Fitness saved successfully", JsonRequestBehavior.AllowGet);
            }

        }
        /// ------------------------   -------------------------Triggers-------------------------
        public ActionResult SpottingTriggers()
        {
            return View("_SpottingTriggers");
        }
        public ActionResult GetUsersTriggersList(DataTableAjaxPostModel dtParams)
        {
            var searchValue = Request["search[value]"];
            var take = dtParams.length;
            var skip = dtParams.start;
            var resultData = _q.ReturnTriggers(GetuserId());
                resultData = resultData.OrderByDescending(x => x.AddedDate).ThenBy(n => n.MarkAsCompleted == false).ToList();         
            int recordsTotal = resultData.Count();
            var data = resultData.Skip(skip).Take(take).ToList();

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            var resultDataN = new
            {
                dtParams.draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(resultDataN),
                ContentType = "application/json"
            };

            return result;
        }
        public ActionResult AddTriggers()
        {
            TriggerVM vm = new TriggerVM();
            return View("_Trigger", vm);
        }
        [HttpPost]
        public ActionResult AddTriggers(TriggerVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_Trigger", vm);
            }
            else
            {
                var t = _q.InsertTriggers(vm, GetuserId(), User.Identity.Name);
                return Json("Trigger saved successfully", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult EditTriggers()
        {
            TriggerVM vm = new TriggerVM();
            return View("_Triggers", vm);
        }

        /// ------------------------   -------------------------MemoryMaker-------------------------
        public ActionResult MemoryMaker()
        {
            return View("_MemoryMakerMain");
        }
        public ActionResult GetUsersMemoryMakerList(DataTableAjaxPostModel dtParams)
        {
            var searchValue = Request["search[value]"];
            var take = dtParams.length;
            var skip = dtParams.start;
            var resultData = _q.ReturnMemoryMaker(GetuserId());
            resultData = resultData.OrderByDescending(x => x.AddedDate).ThenBy(n => n.MarkAsCompleted == false).ToList();
            int recordsTotal = resultData.Count();
            var data = resultData.Skip(skip).Take(take).ToList();

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            var resultDataN = new
            {
                dtParams.draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(resultDataN),
                ContentType = "application/json"
            };

            return result;
        }
        public ActionResult AddMemoryMaker()
        {
            MemoryMakerVM vm = new MemoryMakerVM();
            return View("_MemoryMaker", vm);
        }
        [HttpPost]
        public ActionResult AddMemoryMaker(MemoryMakerVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_MemoryMaker", vm);
            }
            else
            {
                var t = _q.InsertMemoryMaker(vm, GetuserId(), User.Identity.Name);
                return Json("Memory maker saved successfully", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult EditMemoryMaker()
        {
            MemoryMakerVM vm = new MemoryMakerVM();
            return View("_MemoryMaker", vm);
        }
        /// ------------------------   -------------------------MindFull Attention-------------------------
        public ActionResult MindFullAttention()
        {
             return View("_MindFullAttentionMain");
        }
        public ActionResult GetUsersMindFullAttentionList(DataTableAjaxPostModel dtParams)
        {
            var searchValue = Request["search[value]"];
            var take = dtParams.length;
            var skip = dtParams.start;
            var resultData = _q.ReturnMindFullAttention(GetuserId());
            resultData = resultData.OrderByDescending(x => x.AddedDate).ThenBy(n => n.MarkAsCompleted == false).ToList();
            int recordsTotal = resultData.Count();
            var data = resultData.Skip(skip).Take(take).ToList();

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            var resultDataN = new
            {
                dtParams.draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(resultDataN),
                ContentType = "application/json"
            };

            return result;
        }
        public ActionResult AddMindFullAttention()
        {
            MindFullAttentionVM vm = new MindFullAttentionVM();
            return View("_MindFullAttention", vm);
        }
        [HttpPost]
        public ActionResult AddMindFullAttention(MindFullAttentionVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_MindFullAttention", vm);
            }
            else
            {
                var t = _q.InsertMindFullAttention(vm, GetuserId(), User.Identity.Name);
                return Json("Mindfull attention saved successfully", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult EditMindFullAttention()
        {
            MindFullAttentionVM vm = new MindFullAttentionVM();
            return View("_MindFullAttention", vm);
        }
        /// ------------------------   -------------------------Gratitude Journal-------------------------
        public ActionResult GratitudeJournal()
        {
            return View("_GratitudeJournalMain");
        }
        public ActionResult GetUsersGratitudeJournalList(DataTableAjaxPostModel dtParams)
        {
            var searchValue = Request["search[value]"];
            var take = dtParams.length;
            var skip = dtParams.start;
            var resultData = _q.ReturnGratitudeJournal(GetuserId());
            resultData = resultData.OrderByDescending(x => x.AddedDate).ThenBy(n => n.MarkAsCompleted == false).ToList();
            int recordsTotal = resultData.Count();
            var data = resultData.Skip(skip).Take(take).ToList();

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            var resultDataN = new
            {
                dtParams.draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            var result = new ContentResult
            {
                Content = serializer.Serialize(resultDataN),
                ContentType = "application/json"
            };

            return result;
        }
        public ActionResult AddGratitudeJournal()
        {
            GratitudeJournalVM vm = new GratitudeJournalVM();
            return View("_GratitudeJournal", vm);
        }
        [HttpPost]
        public ActionResult AddGratitudeJournal(GratitudeJournalVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("_GratitudeJournal", vm);
            }
            else
            {
                var t = _q.InsertGratitudeJournal(vm, GetuserId(), User.Identity.Name);
                return Json("Gratitude Journal saved successfully", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult EditGratitudeJournal()
        {
            GratitudeJournalVM vm = new GratitudeJournalVM();
            return View("_GratitudeJournal", vm);
        }
    }
    public static class ControllerExtensions
    {
        public static ToastMessage AddToastMessage(this Controller controller, string title, string message, ToastType toastType = ToastType.Info)
        {
            Toastr toastr = controller.TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(title, message, toastType);
            controller.TempData["Toastr"] = toastr;
            return toastMessage;
        }
    }
}