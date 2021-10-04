using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens.Tree
{
    public interface ITreeNode<T>
    {
        T State { get; }
        ITreeNode<T> ParentNode { get; }
        bool IsNodeCompliant();
        int PathCost { get; }
        int Depth { get; }

        IEnumerable<ITreeNode<T>> GetChildren();
    }
}
