
namespace Repository.Models.Entity
{
    public class Passanger
    {
        public string NationalID { get; set; }
        public string EFirstName { get; set; }
        public string ELastNAme { get; set; }
        public int Gender { get; set; }
        public string BirthDay_Day { get; set; }
        public string BirthDay_Month { get; set; }
        public string BirthDay_Year { get; set; }
        public int Nationality { get; set; }
        public string PassportID { get; set; }
        public string PassportExpireDate { get; set; }
        public string Tell { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string EmailConfirm { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
    }
}
