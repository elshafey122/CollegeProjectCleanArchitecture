namespace SchoolProject.Core.Features.ApplicationUser.Queires.ViewModel
{
    public class GetUserPaginatedResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public GetUserPaginatedResponse(string _fullName, string _email, string _country, string _address)
        {
            FullName = _fullName;
            Email = _email;
            Country = _country;
            Address = _address;
        }
    }
}
