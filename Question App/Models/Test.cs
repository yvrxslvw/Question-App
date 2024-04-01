using DB;

namespace Question_App.Models
{
    public class Test
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float Timer { get; private set; }

        public Test(string name, float timer)
        {
            Name = name;
            Timer = timer;
        }

        public Test(int id, string name, float timer)
        {
            Id = id;
            Name = name;
            Timer = timer;
        }

        public void EditName(string name)
        {
            Name = name;
            Database.Update("Tests", new string[] { "Name" }, new string[] { name }, Id);
        }

        public void EditTimer(int timer)
        {
            Timer = timer;
            Database.Update("Test", new string[] { "Timer" }, new string[] { timer.ToString() }, Id);
        }

        public void InsertDatabase()
        {
            Database.Insert("Tests", "Name, Timer", $"N'{Name}', '{Timer}'");
        }

        public void RemoveDatabase()
        {
            Database.Delete("Tests", "Id", Id.ToString());
        }
    }
}
