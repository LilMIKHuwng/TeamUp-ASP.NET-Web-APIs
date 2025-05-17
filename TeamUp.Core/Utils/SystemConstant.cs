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

        public class Type
        {
            public static string Soccer = "Bóng Đá";
            public static string Badminton = "Cầu Lông";
            public static string PickleBall = "Pickleball";
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

        public class RoomStatus
        {
            public static string Waiting = "Waiting"; 
            public static string Full = "Full";       
            public static string InProgress = "InProgress"; 
            public static string Completed = "Completed";   
            public static string Cancelled = "Cancelled";   
        }

        public static class RoomJoinRequestStatus
        {
            public const string Pending = "Pending";       
            public const string Accepted = "Accepted";    
            public const string Rejected = "Rejected";     
            public const string Cancelled = "Cancelled";   
        }

        public static class PaymentStatus
        {
            public const string Pending = "Pending";         
            public const string Paid = "Paid";               
            public const string Failed = "Failed";          
            public const string Refunded = "Refunded";      
            public const string Cancelled = "Cancelled";     
        }

        public class RoomPlayerStatus
        {
            public static string Accepted = "Accepted";
            public static string InProgress = "InProgress";
            public static string Completed = "Completed";
            public static string Cancelled = "Cancelled";
        }

        public class PackageStatus
        {
            public static string Active = "Active";
            public static string InActive = "InActive";
        }
    }
}
