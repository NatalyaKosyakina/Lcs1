namespace Lcs1
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            //Thread thread = new Thread(Chat.Server);
            //thread.Start();

            Chat.Server();

            Console.WriteLine("Укажите ник со стороны клиента");
            string nik = Console.ReadLine();
            while (String.IsNullOrEmpty(nik))
            {
                Console.WriteLine("Укажите ник со стороны клиента");
                nik = Console.ReadLine();
            }

            Thread thread2 = new Thread(() =>
            {
                Chat.Client(nik);
            });
            thread2.Start();
        }
    }
}
