using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using BugTracker.ViewModels;

namespace BugTracker.Controllers
{ 
    public class IssueController : Controller
    {
        private BugTrackerDB db = new BugTrackerDB();

        //
        // GET: /Home/

        public ViewResult Index()
        {
            return View(db.IssueEntries.ToList());
        }

        public ActionResult GetIssueCounter()
        {
            BugTrackerUser bugTrackerUser = (from user in db.BugTrackerUsers where user.UserName.Equals(User.Identity.Name) select user).SingleOrDefault();
            IEnumerable<IssueEntry> issues = bugTrackerUser.IssueEntries;
            int issueCounter = issues.Count();
            return Json(new { issueCount = issueCounter });
        }

        [Authorize]
        public ViewResult MyIssues()
        {
            BugTrackerUser bugTrackerUser = (from user in db.BugTrackerUsers where user.UserName.Equals(User.Identity.Name) select user).SingleOrDefault();
            IEnumerable<IssueEntry> issues = bugTrackerUser.IssueEntries;
            if (bugTrackerUser != null) return View(issues);
            return View();
        }

        public ViewResult IssuesByProject(int projectId)
        {
            IssueEntryByProjectViewModel viewModel = new IssueEntryByProjectViewModel();
            viewModel.IssuesByProject = from issue in db.IssueEntries
                                            where
                                                issue.Project.ProjectId == projectId
                                            select issue;
            viewModel.ProjectId = projectId;
            return View(viewModel);
        }

        //
        // GET: /Home/Details/5

        public ViewResult Details(Guid? issueId)
        {
            IssueEntry issueentry = db.IssueEntries.Find(issueId);
            return View(issueentry);
        }

        //
        // GET: /Home/Create
        [Authorize]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> prioritySelectItems = from priority in db.IssuePriorities.AsEnumerable()
                                                              select new SelectListItem { 
                                                                  Text = priority.PriorityName, 
                                                                  Value = priority.PriorityId.ToString() 
                                                              };
            ViewData["PrioritySelect"] = prioritySelectItems;
            IEnumerable<SelectListItem> projectSelectItems = from project in db.Projects.AsEnumerable()
                                                             select new SelectListItem
                                                             {
                                                                 Text = project.ProjectName,
                                                                 Value = project.ProjectId.ToString()
                                                             };
            ViewData["ProjectSelect"] = projectSelectItems;
            return View();
        }

        [Authorize]
        public ActionResult EditIssueOnProject(Guid? issueId)
        {
            IssueEntry issue = db.IssueEntries.Find(issueId);
            Guid? tmpId = issueId;

            /** Do not allow to edit issue if user is not responsible on this bug, or created it, or Admin user */
            if (issue.AssignedTo != null && (
                issue.AssignedTo.Equals(User.Identity.Name) ||
                issue.CreatedBy.Equals(User.Identity.Name) ||
                User.Identity.Name.Equals("Admin"))
                )
            {
                EditIssueOnProjectViewModel viewModel = new EditIssueOnProjectViewModel
                {
                    ProjectId = issue.Project.ProjectId,
                    IssueId = issue.IssueEntryId,
                    Title = issue.Title,
                    Description = issue.Description,
                    PrioritySelect = db.IssuePriorities,
                    ProjectSelect = db.Projects,
                    AssignedTo = issue.AssignedTo,
                    IsSolved = issue.IsSolved
                };
                return View(viewModel);
            }
            else return RedirectToAction("Details", new { issueId = tmpId });
        }

        private bool ModifyAssigningBasedOnIssue(string assignedToNew, IssueEntry issueEntry)
        {
            BugTrackerUser oldAssignedUser = (from user in db.BugTrackerUsers
                                              where user.UserName.Equals(issueEntry.AssignedTo)
                                              select user).SingleOrDefault();
            bool assigningChanged = false;

            if (!issueEntry.AssignedTo.Equals(assignedToNew))
            {
                assigningChanged = true;
               
                if (oldAssignedUser != null)
                {
                    /** Remove old reference to issue entry */
                    oldAssignedUser.IssueEntries.Remove(issueEntry);
                }
            }
            else
            {
                BugTrackerUser assignedTo = oldAssignedUser;
                /** Issue is marked as solved so remove the entry */
                if (issueEntry.IsSolved)
                {
                    oldAssignedUser.IssueEntries.Remove(issueEntry);
                }
                /** Issue is marked as not solved so add entry */
                else
                {
                    oldAssignedUser.IssueEntries.Add(issueEntry);
                }
            }

            if (assigningChanged && issueEntry.IsSolved == false)
            {
                issueEntry.AssignedTo = assignedToNew;
                /** Get new user from database for assigning */
                BugTrackerUser assignedToNewUser = (from user in db.BugTrackerUsers
                                                 where user.UserName.Equals(issueEntry.AssignedTo)
                                                 select user).SingleOrDefault();

                if (assignedToNewUser != null) assignedToNewUser.IssueEntries.Add(issueEntry);
            }

            return assigningChanged;
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditIssueOnProject(EditIssueOnProjectViewModel editIssueOnProjectViewModel)
        {
            if (ModelState.IsValid)
            {
                bool assigningChanged = false;
                bool priorityChanged = false;
                bool projectChanged = false;
                
                IssueEntry issueentry = db.IssueEntries.Find(editIssueOnProjectViewModel.IssueId);
                
                int redirectToProject = issueentry.Project.ProjectId;
                issueentry.IsSolved = editIssueOnProjectViewModel.IsSolved;

                assigningChanged = ModifyAssigningBasedOnIssue(editIssueOnProjectViewModel.AssignedTo, issueentry);

                /*
                if (!issueentry.AssignedTo.Equals(editIssueOnProjectViewModel.AssignedTo))
                {
                    assigningChanged = true;
                    BugTrackerUser assignee = (from user in db.BugTrackerUsers
                                               where user.UserName.Equals(issueentry.AssignedTo)
                                               select user).SingleOrDefault();
                    if (assignee == null)
                    {

                    }
                    else
                    {
                        assignee.IssueEntries.Remove(issueentry);
                    }
                }
                else
                {
                    BugTrackerUser assignedTo = (from user in db.BugTrackerUsers
                                                 where user.UserName.Equals(issueentry.AssignedTo)
                                                 select user).SingleOrDefault();
                
                    if (issueentry.IsSolved)
                    {                   
                        assignedTo.IssueEntries.Remove(issueentry);
                    }
                    else
                    {
                        assignedTo.IssueEntries.Add(issueentry);
                    }
                }
                */
                issueentry.Description = editIssueOnProjectViewModel.Description;
                issueentry.Title = editIssueOnProjectViewModel.Title;
                
                string selectedProject = editIssueOnProjectViewModel.SelectedProject;
                Int32 projectSelector = Convert.ToInt32(selectedProject);
                if (projectSelector != issueentry.Project.ProjectId)
                {
                    projectChanged = true;
                    Project projectReference = (from project in db.Projects
                                                where project.ProjectId == issueentry.Project.ProjectId
                                                select project).SingleOrDefault();
                    projectReference.IssueEntries.Remove(issueentry);

                }
                string selectedPriority = editIssueOnProjectViewModel.SelectedPriority;
                Int32 prioritySelector = Convert.ToInt32(selectedPriority);
                if (prioritySelector != issueentry.IssuePriority.PriorityId)
                {
                    priorityChanged = true;
                    Priority priorityReference = (from priority in db.IssuePriorities
                                                    where priority.PriorityId == issueentry.IssuePriority.PriorityId
                                                    select priority).SingleOrDefault();
                    priorityReference.IssueEntries.Remove(issueentry);
                }
                if (projectChanged)
                {
                    issueentry.Project = db.Projects.Find(projectSelector);
                    issueentry.Project.IssueEntries.Add(issueentry);
                }
                /*
                if (assigningChanged && issueentry.IsSolved == false)
                {
                    issueentry.AssignedTo = editIssueOnProjectViewModel.AssignedTo;
                    BugTrackerUser assignedToUser = (from user in db.BugTrackerUsers
                                                     where user.UserName.Equals(issueentry.AssignedTo)
                                                     select user).SingleOrDefault();

                    if (assignedToUser != null) assignedToUser.IssueEntries.Add(issueentry);
                }
                 */
                if (priorityChanged)
                {
                    issueentry.IssuePriority = db.IssuePriorities.Find(prioritySelector);
                    issueentry.IssuePriority.IssueEntries.Add(issueentry);
                }
                try
                {
                    db.Entry(issueentry).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                return RedirectToAction("IssuesByProject", "Issue", new { projectId = redirectToProject } );
            }
            
            return View(editIssueOnProjectViewModel);
        }

        // Create issue on project id
        // GET: /Home/Create
        [Authorize]
        public ActionResult CreateIssueOnProject(int projectId)
        {
            EditIssueOnProjectViewModel viewModel = new EditIssueOnProjectViewModel();
            viewModel.ProjectId = projectId;
            viewModel.ProjectSelect = db.Projects;
            viewModel.PrioritySelect = db.IssuePriorities;
            return View(viewModel);
        } 

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult CreateIssueOnProject(EditIssueOnProjectViewModel createIssueOnProjectViewModel)
        {
            if (ModelState.IsValid)
            {
                IssueEntry issueentry = new IssueEntry();
                
                BugTrackerUser authoringUser = (from user in db.BugTrackerUsers
                                                where (String.Compare(user.UserName, User.Identity.Name) == 0)
                                                select user).Single();
                issueentry.CreatedBy = authoringUser.UserName;
                issueentry.AssignedTo = createIssueOnProjectViewModel.AssignedTo;
                issueentry.Description = createIssueOnProjectViewModel.Description;
                issueentry.Title = createIssueOnProjectViewModel.Title;
                issueentry.IsSolved = createIssueOnProjectViewModel.IsSolved;
                issueentry.IssueEntryId = Guid.NewGuid();
                string selectedProject = createIssueOnProjectViewModel.SelectedProject;
                string selectedPriority = createIssueOnProjectViewModel.SelectedPriority;
                issueentry.Project = db.Projects.Find(Convert.ToInt32(selectedProject));
                issueentry.Project.IssueEntries.Add(issueentry);

                BugTrackerUser assignedToUser = (from user in db.BugTrackerUsers
                                                 where user.UserName.Equals(issueentry.AssignedTo)
                                                 select user).SingleOrDefault();
                

                issueentry.IssuePriority = db.IssuePriorities.Find(Convert.ToInt32(selectedPriority));
                issueentry.IssuePriority.IssueEntries.Add(issueentry);
                if (assignedToUser != null) assignedToUser.IssueEntries.Add(issueentry);

                try
                {
                    db.IssueEntries.Add(issueentry);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                return RedirectToAction("IssuesByProject", new { projectId = createIssueOnProjectViewModel.ProjectId } );
            }

            return View(createIssueOnProjectViewModel);
        }


        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            IssueEntry issueentry = db.IssueEntries.Find(id);
            return View(issueentry);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(IssueEntry issueentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issueentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issueentry);
        }

        //
        // GET: /Home/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? issueId)
        {
            Guid? tmpId = issueId;
            IssueEntry issueentry = db.IssueEntries.Find(issueId);
            if (!issueentry.CreatedBy.Equals(User.Identity.Name) || !User.Identity.Name.Equals("Admin"))
            {
                return RedirectToAction("Details", new { issueId = tmpId });
            }
            return View(issueentry);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(Guid? issueId)
        {            
            IssueEntry issueentry = db.IssueEntries.Find(issueId);
            db.IssueEntries.Remove(issueentry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}