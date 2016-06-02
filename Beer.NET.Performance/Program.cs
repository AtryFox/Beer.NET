using System;
using System.Diagnostics;
using System.Text;
using DerAtrox.BeerNET;

namespace BeerNET.Performance {
    internal class Program {

        private static string MakeRandomString(int length) {
            var rnd = new Random();
            var builder = new StringBuilder();
            for(int i = 0; i < length; i++) {
                builder.Append((char)rnd.Next('A', 'z'));
            }
            return builder.ToString();
        }

        private static void Main(string[] args) {
            const int n = 1000;
            const int length = 10000;

            string[] inputs = new string[n];
            string[] results = new string[n];

            var beer = new Beer();
            var beerEx = new BeerEx();

            for (int i = 0; i < n; i++) {
                inputs[i] = MakeRandomString(length);
            }

            Stopwatch sw;

            sw = Stopwatch.StartNew();
            for (int i = 0; i < n; i++) {
                results[i] = beerEx.Encode(inputs[i]);
            }
            sw.Stop();
            var timeBeerExSerialize = sw.Elapsed;

            sw = Stopwatch.StartNew();
            for (int i = 0; i < n; i++) {
                results[i] = beer.Encode(inputs[i]);
            }
            sw.Stop();
            var timeBeerSerialize = sw.Elapsed;

            sw = Stopwatch.StartNew();
            for (int i = 0; i < n; i++) {
                inputs[i] = beerEx.Decode(results[i]);
            }
            sw.Stop();
            var timeBeerExDeserialize = sw.Elapsed;

            sw = Stopwatch.StartNew();
            for (int i = 0; i < n; i++) {
                inputs[i] = beer.Decode(results[i]);
            }
            sw.Stop();
            var timeBeerDeserialize = sw.Elapsed;

            Console.WriteLine("Time for Beer (Serialize): " + timeBeerSerialize.ToString());
            Console.WriteLine("Time for BeerEx (Serialize): " + timeBeerExSerialize.ToString());
            Console.WriteLine("Time for Beer (Deserialize): " + timeBeerDeserialize.ToString());
            Console.WriteLine("Time for BeerEx (Deserialize): " + timeBeerExDeserialize.ToString());

            if(Debugger.IsAttached)
            {
                Console.ReadKey();
            }
        }
    }
}
