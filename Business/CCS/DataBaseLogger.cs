namespace Business.CCS
{
    public class DataBaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("veritabanına loglandı");
        }

        public void Log(string message)
        {
            throw new NotImplementedException();
        }
    }
}
