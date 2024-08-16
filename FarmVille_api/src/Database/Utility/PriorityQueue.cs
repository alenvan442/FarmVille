using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmVille.FarmVille_api.src.Database.Utility
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> data;

        public PriorityQueue() {
            this.data = new List<T>();
        }

        public void Append(T item) {
            this.data.Add(item);
            this.data.Sort();
        }

        public T GetAt(int index) {
            this.data.Sort();
            return this.data[index];
        }

        public T GetLast() {
            this.data.Sort();
            return this.data[-1];
        }

        public T Pop() {
            this.data.Sort();
            T result = this.data[0];
            this.data.RemoveAt(0);
            return result;
        }

        public T Peek() {
            this.data.Sort();
            T result = this.data[0];
            return result;
        }

        public int Count() {
            return this.data.Count;
        }

        public IEnumerator GetEnumerator() {
            return this.data.GetEnumerator();
        }
    }
}