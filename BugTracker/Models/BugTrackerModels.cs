using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Security;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class IssueEntry
    {
        public Guid IssueEntryId { get; set; }
        
        [Required(ErrorMessage = "Title to the issue is required")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name="Priority")]
        public virtual Priority IssuePriority { get; set; }

        [Display(Name="Solved")]
        public bool IsSolved { get; set; }

        [Display(Name="Project")]
        public virtual Project Project { get; set; }
 
        [Display(Name="Assign To")]
        [Remote("ValidateAssigning", "Account", HttpMethod = "POST", ErrorMessage = "User name does not exist. Please enter a different user name.")]
        public string AssignedTo { get; set; }

        [Display(Name="Created By")]
        public string CreatedBy { get; set; }
    }

    public class Priority
    {
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public virtual List<IssueEntry> IssueEntries { get; set; }
    }

    public class Project
    {
        public int ProjectId { get; set; }
        [Display(Name="Project Name")]
        public string ProjectName { get; set; }
        public virtual List<IssueEntry> IssueEntries { get; set; }
    }

    public class BugTrackerDB : DbContext
    {
        public DbSet<IssueEntry> IssueEntries { get; set; }
        public DbSet<Priority> IssuePriorities { get; set; }
        public DbSet<BugTrackerUser> BugTrackerUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
    }

    public class BugTrackerDBInitializer : DropCreateDatabaseIfModelChanges<BugTrackerDB>
    {
    
        protected override void Seed(BugTrackerDB context)
        {
            context.IssuePriorities.Add(new Priority { PriorityName = "Low" });
            context.IssuePriorities.Add(new Priority { PriorityName = "Medium" });
            context.IssuePriorities.Add(new Priority { PriorityName = "High" });
            context.SaveChanges();
        }
    }
}