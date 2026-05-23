namespace EFCore_CodeFirst.Db.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string NickName { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
        public List<PlayerInstrument> Instruments { get; set; } = new List<PlayerInstrument>();
    }
}
