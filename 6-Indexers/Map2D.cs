namespace Indexers
{
    using System;
    using System.Collections.Generic;

    using System.Linq;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private readonly Dictionary<Tuple<TKey1, TKey2>, TValue> _data = new Dictionary<Tuple<TKey1, TKey2>, TValue>();
        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements
        {
            get => this._data.Count;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get => _data[Tuple.Create(key1, key2)];
            set => _data[Tuple.Create(key1, key2)] = value;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            return _data.Keys
                .Where(tuple => tuple.Item1.Equals(key1))
                .Select(tuple => Tuple.Create(tuple.Item2, this._data[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            return _data.Keys
                .Where(tuple => tuple.Item2.Equals(key2))
                .Select(tuple => Tuple.Create(tuple.Item1, _data[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            return _data.Keys
                .Select(tuple => Tuple.Create(tuple.Item1, tuple.Item2, _data[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            foreach (var key1 in keys1) 
            {
                foreach (var key2 in keys2)
                {
                    if (!_data.ContainsKey(Tuple.Create(key1,key2)))
                    {
                        _data[Tuple.Create(key1,key2)] = generator(key1, key2);
                    }
                }
            }
            
        }

         /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(Map2D<TKey1, TKey2, TValue> other)
        {
            return Equals(this._data, other._data);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            if (other is Map2D<TKey1, TKey2, TValue> otherMap2d)
            {
                return this.Equals(otherMap2d);
            }

            return false;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj == this)
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals(obj as Map2D<TKey1, TKey2, TValue>);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return this._data != null ? this._data.GetHashCode() : 0;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            return "{ " + string.Join(", ", this.GetElements()
                       .Select(e => $"({e.Item1}, {e.Item2}) -> {e.Item3}")) + "}";
        }
    }
}
