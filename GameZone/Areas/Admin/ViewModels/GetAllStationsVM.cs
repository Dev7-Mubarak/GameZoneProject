namespace GameZone.Areas.Admin.ViewModels
{
    public class GetAllStationsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Location { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public int RoomsCount { get; set; }
        public string OwnerName { get; set; }
    }
}
