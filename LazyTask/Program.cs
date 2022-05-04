using System;

namespace LazyTask
{
    class Program
    {
        private static Guid GetId()
        {
            return Guid.NewGuid();
        }
        static void Main(string[] args)
        {
            Lazy<Guid> getId = new Lazy<Guid>(() => GetId());
            Console.WriteLine(getId.Value);//run GetId(), lay gia tri
            Console.WriteLine(getId.Value);//not run GetId,lay gia tri

        }
    }
}
