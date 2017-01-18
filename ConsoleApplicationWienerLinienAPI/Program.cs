using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationWienerLinienAPI
{
    class Program
    {
        static void Main(string[] args){
            Console.WriteLine("Bitte tippen Sie die gewünschte Station ein: ");

            bool exit = false;
            while (exit == false)
            {
                var input = Console.ReadLine();
                if (input==null)
                    throw new ArgumentNullException("Sie müseen eine gültige Station eintippen!");
                exit = true;




            }
            Console.ReadKey();
        }
    }
}
