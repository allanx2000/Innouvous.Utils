using Innouvous.Utils.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SQLiteClient wrapper = new SQLiteClient("test.sqlite", true);
            wrapper.Close();

            Console.WriteLine("Create Wrapper OK");

            Console.ReadKey();
        }
    }
}
