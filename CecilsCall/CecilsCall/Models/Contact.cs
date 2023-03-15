using SQLite;

namespace CecilsCall.Models
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public Contact()
        {
            number = string.Empty;
            name= string.Empty;
        }
    }
}
