using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSync
{
    class DirectoryNode
    {
        public DirectoryInfo dirInfo;
        public TreeNode node;
        public string FullName;

        public DirectoryNode(DirectoryInfo dir, TreeNode node)
        {
            this.dirInfo = dir;
            this.node = node;

            FullName = dir.FullName;
        }

        public void ImageIndex(int num)
        {
            node.ImageIndex = num;
            node.SelectedImageIndex = num;
        }
    }
}
