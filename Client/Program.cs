namespace Client
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //for (int i = 0; i < 4; i++)
            //{

            //    string s = "Name " + i;
            //    new Thread(() => Client.ClientStart(s)).Start();               
            //}
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 4; i++)
            {
                string s = "Name " + i;
                tasks.Add(Task.Run(async () => { await Client.ClientStart(s); }));
            }
            Task.WaitAll(Task.WhenAll(tasks));

                

        }
    }
}