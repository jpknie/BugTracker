using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Security;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using BugTracker.Models;

namespace BugTracker.ViewModels
{
    public class IssueEntryByProjectViewModel
    {
        public int ProjectId { get; set; }
        public IEnumerable<BugTracker.Models.IssueEntry> IssuesByProject { get; set; }

    }

    public class EditIssueOnProjectViewModel
    {
        public EditIssueOnProjectViewModel()
        {
        }
        public int ProjectId { get; set; }
        public Guid IssueId { get; set; }
        public string SelectedPriority { get; set; }

        [Display(Name = "Priority select")]
        public IEnumerable<Priority> PrioritySelect { get; set; }

        public string SelectedProject { get; set; }

        [Display(Name = "Project select")]
        public IEnumerable<Project> ProjectSelect { get; set; }

        [Required(ErrorMessage = "Title to the issue is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Solved")]
        public bool IsSolved { get; set; }

        [Display(Name = "Assign To")]
        [Remote("ValidateAssigning", "Account", HttpMethod = "POST", ErrorMessage = "User name does not exist. Please enter a different user name.")]
        public string AssignedTo { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        [Remote("DoesUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
