using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace BugTracker.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class CustomUser: MembershipUser
    {
        public BugTrackerUser UserAccount { get; set; }

        public CustomUser(string providername,
                            string passwordQuestion,
                                BugTrackerUser userAccount
            )
            : base(providername,
                       userAccount.UserName,
                       userAccount.BugTrackerUserId,
                       userAccount.Email,
                       passwordQuestion,
                       string.Empty,
                       true,
                       false,
                       userAccount.CreationDate,
                       userAccount.LastLoginDate,
                       userAccount.LastActivityDate,
                       new DateTime(),
                       new DateTime())
        {
            UserAccount = userAccount;
        }
    }

    public class BugTrackerUser
    {
        public int BugTrackerUserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastActivityDate { get; set; }

        public virtual List<IssueEntry> IssueEntries { get; set; }
    }
}
