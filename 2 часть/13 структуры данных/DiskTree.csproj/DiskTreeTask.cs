using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskTree
{
    public class DiskTreeTask
    {
        private class Folder
        {
            private readonly string name;
            public readonly List<Folder> InnerFolders;
            public readonly string DepthInSpaces;


            public Folder(string name)
            {
                this.name = name;
                DepthInSpaces = "";
                InnerFolders = new List<Folder>();
            }

            public Folder(string name, string parentDeepness)
            {
                this.name = name;
                DepthInSpaces = parentDeepness + " ";
                InnerFolders = new List<Folder>();
            }

            public Folder FindFolder(string folderName)
            {
                foreach (var e in InnerFolders)
                {
                    if (e.name == folderName) return e;
                }
                return default;
            }

            public List<string> ToList()
            {
                var result = new List<string>();
                InnerFolders.Sort((folder1, folder2) =>
                    string.Compare(folder1.name, folder2.name, StringComparison.Ordinal));

                foreach (var folder in InnerFolders)
                {
                    result.Add($"{DepthInSpaces}{folder.name}");
                    result.AddRange(folder.ToList());
                }
                return result;
            }
        }

        public static List<string> Solve(List<string> input)
        {
            var root = new Folder("");
            foreach (var folderPath in input)
            {
                var path = folderPath.Split('\\');
                var node = root;
                foreach (var name in path)
                {
                    var newFolder = node.FindFolder(name);
                    if (newFolder != null)
                    {
                        node = newFolder;
                        continue;
                    }
                    newFolder = new Folder(name, node.DepthInSpaces);
                    node.InnerFolders.Add(newFolder);
                    node = newFolder;
                }
            }
            return root.ToList();
        }
    }
}
