using SQLite;

namespace JellyBin02
{
    public class Bin
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public double longit { get; set; }
        public double lat { get; set; }
        public string isFull { get; set; }
        public int colorID { get; set; }
    }
}