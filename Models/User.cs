namespace AutoPartsStore.Models
{
    public class User
    {
        public int UserID { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Role { get; set; }

        public required ICollection<PurchaseHistory> PurchaseHistory { get; set; }
    }
}
