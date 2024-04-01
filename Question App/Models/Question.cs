using DB;

namespace Question_App.Models
{
    public class Question
    {
        public int Id { get; private set; }
        public int TestId { get; }
        public string Content { get; private set; }
        public string Answer { get; private set; }

        public Question(int testId, string content, string answer)
        {
            TestId = testId;
            Content = content;
            Answer = answer.ToLower();
        }

        public void EditContent(string content)
        {
            Content = content;
            Database.Update("Questions", new string[] { "Content" }, new string[] { content }, Id);
        }

        public void EditAnswer(string answer)
        {
            Answer = answer.ToLower();
            Database.Update("Questions", new string[] { "Answer" }, new string[] { answer.ToLower() }, Id);
        }

        public void InsertDatabase()
        {
            Database.Insert("Questions", "TestId, Content, Answer", $"'{TestId}', N'{Content}', N'{Answer}'");
        }
    }
}
