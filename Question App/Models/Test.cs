using DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Question_App.Models
{
    public class Test
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float Timer { get; private set; }
        public List<Question> questions = new List<Question> { };

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

        public void EditTimer(float timer)
        {
            Timer = timer;
            Database.Update("Tests", new string[] { "Timer" }, new string[] { timer.ToString() }, Id);
        }

        public void InsertDatabase()
        {
            Database.Insert("Tests", "Name, Timer", $"N'{Name}', '{Timer}'");
        }

        public void RemoveDatabase()
        {
            Database.Delete("Tests", "Id", Id.ToString());
        }

        public bool GetQuestions()
        {
            questions.Clear();

            SqlDataReader reader = null;

            try
            {
                reader = Database.Select("Questions", "TestId", Id.ToString());
                if (!reader.HasRows) return false;
                Question item = null;

                while (reader.Read())
                {
                    item = new Question(Convert.ToInt32(reader["Id"]), Id, Convert.ToString(reader["Content"]).Trim(), Convert.ToString(reader["Answer"]).Trim());
                    questions.Add(item);
                }

                return true;
            }
            catch (Exception exc)
            {
                throw new Exception(string.Empty, exc);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                
                Utils.ShuffleList(ref questions);
            }
        }
    }
}
