namespace GameZone.Models
{
    public partial class AccountsInformation
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public int GameStationId { get; set; }
        public string AccountNumber { get; set; } = null!;

        public virtual GameStation GameStation { get; set; } = null!;
    }
}
