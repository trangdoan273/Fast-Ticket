namespace TICKETBOX.Models{
    public class UpdateUserModel{
        public int UserId { get; set; }
    public string UserFullname { get; set; }
    public string UserPhoneNumber { get; set; }
    public string UserSex { get; set; }
    public DateTime? DoB { get; set; }
    public string UserAddress { get; set; }
    public string UserEmail { get; set; }
    }
}