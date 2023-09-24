namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            List<List<int>> resultPaths = new List<List<int>>();
            List<int> currentPath = new List<int>();

            this.FindAllPaths(this, sum, currentPath, resultPaths);
            return resultPaths;


        }

        private void FindAllPaths(Tree<int> node, int sum, List<int> currentPath, List<List<int>> resultPaths)
        {
            if(node is null)
            {
                return;
            }
            currentPath.Add(node.Key);
            int currentSum = currentPath.Sum();
            if (currentSum == sum && node.Children.Count == 0)
            {
                resultPaths.Add(new List<int>(currentPath));
            }
            foreach (var child in node.Children)
            {
                FindAllPaths(child, sum, currentPath, resultPaths);
            }
            currentPath.RemoveAt(currentPath.Count - 1);
        }

        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            List<Tree<int>> resultPaths = new List<Tree<int>>();

            this.FindAllTreePaths(this, sum, resultPaths);
            return resultPaths;
        }

        private int FindAllTreePaths(Tree<int> node, int sum, List<Tree<int>> resultPaths)
        {
            if (node == null)
            {
                return 0;
            }
            int subtreeSum = node.Key;

            foreach (var child in node.Children)
            {
                subtreeSum += FindAllTreePaths(node, sum, resultPaths);
            }
            if(subtreeSum == sum)
            {
                resultPaths.Add(new IntegerTree(node.Key,node.Children.ToArray()));
            }
            return subtreeSum;
        }
    }
}
