using pdftron.SDF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WPF_Xplorer.Models
{
    public class ObjectArrayCollection: IEnumerable<KeyValuePair<string, Obj>>, IDisposable
    {
        private readonly Obj arrayObj;
        private readonly string ancestorName;

        public ObjectArrayCollection(Obj array, string ancestorName)
        {
            arrayObj = array;
            this.ancestorName = ancestorName;
        }

        public IEnumerator<KeyValuePair<string, Obj>> GetEnumerator()
        {
            return new ArrayEnumerator(arrayObj, ancestorName);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            arrayObj.Dispose();
        }
    }

    public class ArrayEnumerator : IEnumerator<KeyValuePair<string, Obj>>
    {
        private readonly Obj arrayObj;
        private readonly string parentName;
        private readonly int size;
        private int position = -1;


        public ArrayEnumerator(Obj array, string parentName)
        {
            arrayObj = array;
            size = array.Size();
            this.parentName = parentName;
        }


        public bool MoveNext()
        {
            position++;

            return position < size;
        }

        public void Reset()
        {
            position = -1;
        }

        public KeyValuePair<string, Obj> Current
        {
            get
            {
                var value = arrayObj.GetAt(position);

                var dict = new Dictionary<string, Obj>()
                {
                    {GetName(value), value}
                };

                return dict.First();
            }
        }

        private string GetName(Obj obj)
        {
            try
            {
                return obj.GetName();
            }
            catch
            {
                return $"{parentName}[{position}]";
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            arrayObj.Dispose();
        }
    }

}
