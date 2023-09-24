namespace Demo
{
    using System;
    using System.Collections.Generic;
    using Tree;

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = new string[]
           {
                "7 19",
                "7 21",
                "7 14",
                "19 1",
                "19 12",
                "19 31",
                "14 23",
                "14 6"
           };
            IntegerTreeFactory factory = new IntegerTreeFactory();
            var tree = factory.CreateTreeFromStrings(input);
            //Console.WriteLine(tree.AsString());
            //Console.WriteLine(tree.GetDeepestKey());
            //IEnumerable<int> list = tree.GetLongestPath();
            //Console.WriteLine(string.Join(" ",list));
            IEnumerable<IEnumerable<int>> results = tree.GetPathsWithGivenSum(27);

            foreach (var items in results)
            {
                Console.WriteLine(string.Join(", ",results.ToString()));
            }
        }
    }
}
