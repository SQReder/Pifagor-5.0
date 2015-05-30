using System;
using System.Collections.Generic;

namespace Pifagor.ClusterTree
{
    public class Tree<T>: CheckedDisposable
    {
        private List<Tree<T>> _children = new List<Tree<T>>();

        public Tree(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyList<Tree<T>> Children => _children;

        /// <summary>
        /// ѕрисоедин€ет указанное дерево к текущему
        /// </summary>
        /// <param name="child"></param>
        public void Attach(Tree<T> child)
        {
            _children.Add(child);
            child.Parent = this;
        }

        /// <summary>
        /// ќтсоедин€ет указанное дерево от текущего
        /// </summary>
        /// <param name="tree">ƒерево дл€ отсоединени€</param>
        public void Detach(Tree<T> tree)
        {
            if (_children.Remove(tree))
                tree.Parent = null;
        }

        /// <summary>
        /// ќбходит дерево в глубину и возвращает первый узел дерева, дл€ которого выполн€етс€ предикат
        /// </summary>
        /// <param name="predicate">ѕредикат определ€ющий подход€т ли данные в узле</param>
        /// <returns>ѕервый узел, дл€ которого выполнилс€ предикат, или null если такого не нашлось</returns>
        public Tree<T> Find(Predicate<T> predicate)
        {
            if (predicate(Data))
                return this;

            foreach (var child in _children)
            {
                var value = child.Find(predicate);
                if (value != null)
                    return value;
            }

            return null;
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var child in _children)
            {
                child.Dispose();
            }
        }
    }
}