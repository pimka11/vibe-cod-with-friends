using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vibe_cod_with_friends
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var friends = new List<string>
            {
                "Oleksii Shmyhol",
                "Yevhen Linchytskyi",
                "Kyrylo Kikhtenko",
                "Yaroslav Rykov",
                "Vsevolod Diatlov"
            };

            Console.WriteLine("My friends:");

            foreach (var friend in friends)
            {
                Console.WriteLine("- " + friend);
            }

            Console.ReadKey();
        }
    }
}
