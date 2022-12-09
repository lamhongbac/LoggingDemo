using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousDemo
{
    class Program
    {
        internal class Bacon { }
        internal class Coffee { }
        internal class Egg { }
        internal class Juice { }
        internal class Toast { }
        public static async Task Main()
        {
            string url = "https://github.com/TechMarDay/Cache/blob/master/Cache/MemoryCache/Startup.cs";
            var fileTask = DownloadFileASynchronous(url);
            Console.WriteLine("Làm gì đó khi file đang tải");
            //var file = await fileTask;

            //Console.WriteLine($"File có độ dài {file?.Length}");
            Console.WriteLine("Làm gì đó khi file tải xong");

            Console.WriteLine($"Banh mì dang nướng");
            //Toast toast = await ToastBreadAsync(2);
            
            Console.WriteLine("Bánh mì nướng xong rùi ăn thui");
            Console.ReadLine();
            Task.WaitAll(fileTask, ToastBreadAsync(2));
        }

        public static async Task<string> DownloadFileASynchronous(string url)
        {
            HttpClient client = new HttpClient();
            var fileTask = await client.GetStringAsync(url); //Phương thức này là Synchronous được cung cấp bởi WebClient của .net
            Thread.Sleep(9000); //Giả sử việc download file sẽ mất 9000 miliseconds 
            Console.WriteLine("Đã hoàn thành tải file");
            return fileTask;
        }
        //static async Task Main(string[] args)
        //{
        //    //Console.WriteLine("Hello World!");
        //    //Coffee cup = PourCoffee();
        //    //Console.WriteLine("coffee is ready");

        //    //Egg eggs = FryEggs(2);
        //    //Console.WriteLine("eggs are ready");

        //    //Bacon bacon = FryBacon(3);
        //    //Console.WriteLine("bacon is ready");

        //    //Toast toast = ToastBread(2);
        //    //ApplyButter(toast);
        //    //ApplyJam(toast);
        //    //Console.WriteLine("toast is ready");

        //    //Juice oj = PourOJ();
        //    //Console.WriteLine("oj is ready");
        //    //Console.WriteLine("Breakfast is ready!");

        //    Coffee cup = PourCoffee();
        //    Console.WriteLine("coffee is ready");

        //    await FryEggsAsync(2);
        //    Console.WriteLine("eggs are ready");

        //    await FryBaconAsync(3);
        //    Console.WriteLine("bacon is ready");

        //    Toast toast =await ToastBreadAsync(2);
        //    ApplyButter(toast);
        //    ApplyJam(toast);
        //    Console.WriteLine("toast is ready");

        //    Juice oj = PourOJ();
        //    Console.WriteLine("oj is ready");
        //    Console.WriteLine("Breakfast is ready!");
        //}

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }
        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }
        private static Bacon FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put bacon on plate");
            return new Bacon();
        }
        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
           await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
          await  Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");
            return  new Bacon();
            //return new Bacon();
        }
        private static Egg FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            Task.Delay(3000).Wait();
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }
        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");
            return new Egg();
            //return await Task.FromResult(new Egg()) ;
        }
        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
        private static async Task<Coffee> PourCoffeeAsync()
        {
            Console.WriteLine("Pouring coffee");
            return await Task.FromResult(new Coffee());
            //return new Coffee();
        }
    }
}
