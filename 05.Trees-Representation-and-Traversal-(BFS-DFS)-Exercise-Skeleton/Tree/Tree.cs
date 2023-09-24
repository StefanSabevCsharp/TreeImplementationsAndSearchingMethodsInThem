namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;
        public Tree(T key, params Tree<T>[] children)
        {
            this.children = new List<Tree<T>>();
            this.Key = key;
            foreach (var child in children)
            {
                this.children.Add(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string AsString()
        {

            StringBuilder sb = new StringBuilder();

            this.BfsToString(sb, this, 0);

            return sb.ToString().Trim();
        }

        private void BfsToString(StringBuilder sb, Tree<T> tree, int indent)
        {
            sb
                .Append(' ', indent)
                .AppendLine(tree.Key.ToString());

            foreach (var child in tree.children)
            {
                this.BfsToString(sb, child, indent + 2);
            }
        }

        public IEnumerable<T> GetInternalKeys()
        {

            List<T> leafList = new List<T>();
            this.SearchByBfs(leafList, x => x.children.Count != 0 && x.Parent != null);
            return leafList;
        }

        public IEnumerable<T> GetLeafKeys()
        {
            List<T> leafList = new List<T>();
            this.SearchByBfs(leafList, x => x.children.Count == 0);
            return leafList;
        }

        private void SearchByBfs(List<T> leafs, Predicate<Tree<T>> predicate)
        {

            foreach (var child in this.children)
            {
                if (predicate.Invoke(child))
                {
                    leafs.Add(child.Key);
                }
                child.SearchByBfs(leafs, predicate);

            }


        }

        public T GetDeepestKey()
        {
            List<Tree<T>> leafs = new List<Tree<T>>();

            leafs = this.GetAllLeafs(leafs);

            Tree<T> node = null;
            int maxDepth = 0;

            foreach (var leaf in leafs)
            {
                var depth = this.GetDeapth(leaf);
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                    node = leaf;
                }
            }
            return node.Key;
        }

        private int GetDeapth(Tree<T> leaf)
        {
            int depth = 0;
            while (leaf.Parent != null)
            {
                depth++;
                leaf = leaf.Parent;
            }
            return depth;
        }

        public List<Tree<T>> GetAllLeafs(List<Tree<T>> leafs)
        {

            foreach (var child in this.children)
            {
                if (child.children.Count == 0)
                {
                    leafs.Add(child);

                }
                child.GetAllLeafs(leafs);
            }
            return leafs;
        }



        public IEnumerable<T> GetLongestPath()
        {
            List<T> longestPath = new List<T>();
            List<T> currentPath = new List<T>();

            FindLongestPath(this, currentPath, ref longestPath);

            return longestPath;
        }

        private void FindLongestPath(Tree<T> node, List<T> currentPath, ref List<T> longestPath)
        {
            currentPath.Add(node.Key);

            if (node.children.Count == 0)
            {
                if (currentPath.Count > longestPath.Count)
                {
                    longestPath.Clear();
                    longestPath.AddRange(currentPath);
                }
            }
            else
            {
                foreach (var child in node.children)
                {
                    FindLongestPath(child, currentPath, ref longestPath);
                }
            }

            currentPath.RemoveAt(currentPath.Count - 1);
        }


        public override string ToString()
        {
            return this.Key.ToString();
        }
    }
}
