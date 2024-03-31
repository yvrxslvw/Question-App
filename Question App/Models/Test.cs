using DB;

namespace Question_App.Models
{
    internal class Test
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Timer { get; private set; }
        //public Question[] Questions { get; private set; }

        public Test(string name, int timer)
        {
            Name = name;
            Timer = timer;
            //Questions = questions;
        }

        public Test(int id, string name, int timer)
        {
            Id = id;
            Name = name;
            Timer = timer;
            //Questions = questions;
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
            Id = Database.Insert("Test", "Name, Timer", $"'{Name}', '{Timer}'");
        }
    }
}
