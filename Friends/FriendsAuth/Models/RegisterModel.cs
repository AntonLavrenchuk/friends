namespace FriendsAuth.Models
{
    public class RegisterModel
    {
        public string FirstName {get; set;}

        public string LastName {get; set;}
        
        public string Email {get; set;}

        public string PhoneNumber {get; set;}

        public DateTime BirthDate { get; set; }

        public string Password { get; set; }
    }
}
