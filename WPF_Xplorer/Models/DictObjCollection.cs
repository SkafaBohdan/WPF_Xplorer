using pdftron.SDF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Models
{
    public class DictObjCollection: IEnumerable<KeyValuePair<string, Obj>>
    {
        private readonly DictIterator dictIterator;


        public DictObjCollection(DictIterator iterator)
        {
            dictIterator = iterator;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public DictEnumerator GetEnumerator()
        {
            return new DictEnumerator(dictIterator);
        }

        IEnumerator<KeyValuePair<string, Obj>> IEnumerable<KeyValuePair<string, Obj>>.GetEnumerator()
        {
            return new DictEnumerator(dictIterator);
        }

        public void Dispose()
        {
            dictIterator?.Dispose();
        }
    }
    public class DictEnumerator : IEnumerator<KeyValuePair<string, Obj>>
    {
        private readonly DictIterator dictIterator;


        public DictEnumerator(DictIterator iterator)
        {
            dictIterator = iterator;
        }


        public bool MoveNext()
        {
            return dictIterator.HasNext();
        }

        public void Reset() { }

        public void Dispose()
        {
            dictIterator.Dispose();
        }

        public KeyValuePair<string, Obj> Current
        {
            get
            {
                var current = new Dictionary<string, Obj>
                {
                    {dictIterator.Key().GetName(), dictIterator.Value()}
                };
                dictIterator.Next();

                return current.First();
            }
        }

        object IEnumerator.Current => Current;
    }
}
