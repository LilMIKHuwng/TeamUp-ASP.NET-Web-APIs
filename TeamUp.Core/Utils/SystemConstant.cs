using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyCare.Core.Utils
{
    public class SystemConstant
    {
        public static int PAGE_SIZE = 10;
        public static int MAX_PER_COMMENT = 10;

        public class Provider
        {
            public static string GOOGLE = "Google";
        }
        public class Role
        {
            public static string ADMIN = "Admin";
            public static string OWNER = "Owner";
            public static string USER = "User";
            public static string COACH = "Coach";
        }
        public enum Gender
        {
            Female = 0,
            Male = 1,
            Unknown = 2,
        }

        public class BookingStatus
        {
            public static string Pending = "Pending";
            public static string Confirmed = "Confirmed";
            public static string InProgress = "InProgress";
            public static string Completed = "Completed";
            public static string CancelledByUser = "CancelledByUser";
            public static string CancelledByOwner = "CancelledByOwner";
            public static string NoShow = "NoShow";
            public static string Failed = "Failed";
            public static string CancelledByCoach = "CancelledByCoach";
        }

        public enum UserStatus
        {
            Active = 1,
            InActive = 0,

        }
        public enum EmployeeStatus
        {
            Active = 1,
            InActive = 0
        }
    }
}
