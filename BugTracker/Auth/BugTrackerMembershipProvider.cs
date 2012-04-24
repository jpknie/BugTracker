using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;
using BugTracker.Models;

namespace BugTracker.Auth
{
    public class BugTrackerMembershipProvider: MembershipProvider
    {
        public BugTrackerDB db = new BugTrackerDB();

        public BugTrackerUser User
        {
            get;
            private set;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            string hash = EncryptPassword(password);
            DateTime dateNow = DateTime.Now;
          
            BugTrackerUser bugTrackerUser = new BugTrackerUser { 
                   UserName = username,
                   Password = hash,
                   Email = email,
                   CreationDate = dateNow,
                   LastActivityDate = dateNow,
                   LastLoginDate = dateNow
            };

            CustomUser user = new CustomUser("BugTrackerMembershipProvider", passwordQuestion, bugTrackerUser);
                                                             
            status = MembershipCreateStatus.Success;
            return user;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public string EncryptPassword(string password)
        {
            byte[] passwordBytes = Encoding.GetEncoding(1252).GetBytes(password);
            byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(passwordBytes);
            return Encoding.GetEncoding(1252).GetString(hashBytes);
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection userCollection = new MembershipUserCollection();
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            int startIndex = pageIndex * pageSize;
            int endIndex = startIndex + pageSize - 1;
            if (pageSize >= System.Int32.MaxValue)
            {
                return GetAllUsers(out totalRecords);
            }
            IList<BugTrackerUser> bugTrackerUsers = db.BugTrackerUsers.OrderBy(m => m.UserName).ToList().GetRange(startIndex, endIndex - startIndex);
            foreach (BugTrackerUser user in bugTrackerUsers)
            {
                userCollection.Add(new CustomUser("BugTrackerMembershipProvider", string.Empty, user));
            }
            totalRecords = bugTrackerUsers.Count();
            return userCollection;
        }

        public MembershipUserCollection GetAllUsers(out int totalRecords)
        {
            MembershipUserCollection userCollection = new MembershipUserCollection();
            IList<BugTrackerUser> bugTrackerUsers = db.BugTrackerUsers.OrderBy(m => m.UserName).ToList();
           
            foreach (BugTrackerUser user in bugTrackerUsers)
            {
                userCollection.Add(new CustomUser("BugTrackerMembershipProvider", string.Empty, user));
            }
            totalRecords = bugTrackerUsers.Count();
            return userCollection;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (db.BugTrackerUsers.Count() == 0) return null;
            else
            {
                 BugTrackerUser bugTrackerUser = (from user in db.BugTrackerUsers
                                                     where
                                                         username.Equals(user.UserName)
                                                     select user).SingleOrDefault();
                 if (bugTrackerUser == null) return null;
                 return new CustomUser("BugTrackerMembershipProvider", string.Empty, bugTrackerUser);
                }
           }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            BugTrackerUser bugTrackerUser = (from user in db.BugTrackerUsers
                                  where
                                      user.Email.Equals(email)
                                  select user).Single();
            return bugTrackerUser.UserName;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public BugTrackerUser GetByUserName(string username)
        {
            if (db.BugTrackerUsers.Count() == 0) return null;

            BugTrackerUser bugTrackerUser = (from user in db.BugTrackerUsers.AsEnumerable() 
                                                where user.UserName.ToString().Equals(username) 
                                            select user).Single();

            return bugTrackerUser;

        }

        public override bool ValidateUser(string username, string password)
        {
            if(string.IsNullOrEmpty(password.Trim())) return false;
            string hash = EncryptPassword(password);
           
            BugTrackerUser user = GetByUserName(username);
            if(user == null) return false;
            if(user.Password == hash)
            {
                this.User = user;
                return true;
            }
            return false;
        }
    }


}