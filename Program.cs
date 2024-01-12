using System.Net.NetworkInformation;

namespace ApplicationXmlConfiguration
{
    internal class Program
    {
        public const string address = "www.ya.ru";
        public const int attempts = 10;

        static void Main(string[] args)
        {
            Ping pinger = new Ping();

            try
            {
                PingAddress(pinger, attempts);
            }
            catch (PingException pingException)
            {
                Console.WriteLine(pingException.StackTrace);
            }
            finally
            {
                pinger?.Dispose();
            }
        }

        private static void PingAddress(Ping pinger, int attempts)
        {
            bool pingable = false;
            long responseTime = 0L;

            for (int i = 0; i < attempts; i++)
            {
                PingReply reply = pinger.Send(address);
                pingable = reply.Status == IPStatus.Success;
                responseTime = reply.RoundtripTime;

                string message = pingable ? $"Site {address} is avaliable, response time: {responseTime} ms" : $"Site {address} is unavaliable. Reply status: {reply.Status}";
                Console.WriteLine(message);

                Thread.Sleep(250);
            }
        }
    }
}
