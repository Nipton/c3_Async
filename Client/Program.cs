namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)
            {

                string s = "Name " + i;
                new Thread(() => Client.ClientStart(s)).Start();               
            }
            
        }
    }
}