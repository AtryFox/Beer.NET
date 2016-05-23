using System;
using System.Diagnostics;
using System.Text;
using DerAtrox.BeerNET;

namespace BeerNET.Performance {
    class Program {

        private static string MakeRandomString(int length) {
            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < length; i++) {
                builder.Append((char)rnd.Next('A', 'z'));
            }
            return builder.ToString();
        }

        static void Main(string[] args) {
            Random rnd = new Random();
            const int N = 1000;
            const int Length = 10000;

            string[] inputs = new string[N];
            string[] results = new string[N];

            var beer = new Beer();
            var beerEx = new BeerEx();

            for (int i = 0; i < N; i++) {
                inputs[i] = MakeRandomString(Length);
            }

            Stopwatch sw;

            sw = Stopwatch.StartNew();
            for (int i = 0; i < N; i++) {
                results[i] = beerEx.SerializeBeer(inputs[i]);
            }
            sw.Stop();
            var timeBeerExSerialize = sw.Elapsed;

            sw = Stopwatch.StartNew();
            for (int i = 0; i < N; i++) {
                results[i] = beer.SerializeBeer(inputs[i]);
            }
            sw.Stop();
            var timeBeerSerialize = sw.Elapsed;

            sw = Stopwatch.StartNew();
            for (int i = 0; i < N; i++) {
                inputs[i] = beerEx.DeserializeBeer(results[i]);
            }
            sw.Stop();
            var timeBeerExDeserialize = sw.Elapsed;

            sw = Stopwatch.StartNew();
            for (int i = 0; i < N; i++) {
                inputs[i] = beer.DeserializeBeer(results[i]);
            }
            sw.Stop();
            var timeBeerDeserialize = sw.Elapsed;

            Console.WriteLine("Time for Beer (Serialize): " + timeBeerSerialize.ToString());
            Console.WriteLine("Time for BeerEx (Serialize): " + timeBeerExSerialize.ToString());
            Console.WriteLine("Time for Beer (Deserialize): " + timeBeerDeserialize.ToString());
            Console.WriteLine("Time for BeerEx (Deserialize): " + timeBeerExDeserialize.ToString());
        }
    }
}
