using System;
using System.Collections;

namespace MemcachedLib
{
    public class IteratorIsolateCollection : IEnumerable
    {
        private IEnumerable _enumerable;

        public IteratorIsolateCollection(IEnumerable enumerable)
        {
            this._enumerable = enumerable;
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)new IteratorIsolateCollection.IteratorIsolateEnumerator(this._enumerable.GetEnumerator());
        }

        internal class IteratorIsolateEnumerator : IEnumerator
        {
            private ArrayList items = new ArrayList();
            private int currentItem;

            public object Current
            {
                get
                {
                    return this.items[this.currentItem];
                }
            }

            internal IteratorIsolateEnumerator(IEnumerator enumerator)
            {
                while (enumerator.MoveNext())
                    this.items.Add(enumerator.Current);
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
                this.currentItem = -1;
            }

            public void Reset()
            {
                this.currentItem = -1;
            }

            public bool MoveNext()
            {
                ++this.currentItem;
                return this.currentItem != this.items.Count;
            }
        }
    }
}
