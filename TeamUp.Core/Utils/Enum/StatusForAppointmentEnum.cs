namespace TeamUp.Core.Utils.Enum
{
    public enum BookingStatus
    {
        Pending,      // Đang chờ xác nhận
        Approved,     // Đã xác nhận
        Rejected,     // Từ chối
        Cancelled,    // Đã hủy
        Completed     // Đã hoàn thành
    }

    public enum PaymentStatus
    {
        Unpaid,     // Chưa thanh toán
        Paid,       // Đã thanh toán
        Refunded,   // Đã hoàn tiền
        Failed      // Thanh toán thất bại
    }

    public enum PaymentMethod
    {
        VNPay,
        Cash,
        BankTransfer
    }

    public enum RoomStatus
    {
        Waiting,     // Đang chờ người chơi
        Full,        // Đã đủ người
        Playing,     // Đang chơi
        Ended,       // Đã kết thúc
        Cancelled    // Bị hủy
    }

    public enum UserRoleType
    {
        Player,
        Coach,
        Owner,
        Admin
    }

}
