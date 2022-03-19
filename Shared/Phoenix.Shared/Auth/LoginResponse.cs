namespace Phoenix.Shared.Auth
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public LoginResult LoginResult { get; set; }
    }
    public enum LoginResult
    {
        /// <summary>
        /// Khong tim thay user
        /// </summary>
        UserNotFound = 1,
        /// <summary>
        /// Chua phan cong
        /// </summary>
        NotAssignment = 2,
        /// <summary>
        /// Sai may
        /// </summary>
        InvalidPhone = 3,
        /// <summary>
        /// Su dung may nguoi khac
        /// </summary>
        PhoneOfOtherUser = 4,
        /// <summary>
        /// Sai vi tri
        /// </summary>
        InvalidPlace = 5,
        /// <summary>
        /// Khong lay duoc thong tin dia diem
        /// </summary>
        PositionZero = 6,
        //Khong tim thay ca lam viec
        ShiftNotFound = 7,
    }
}
