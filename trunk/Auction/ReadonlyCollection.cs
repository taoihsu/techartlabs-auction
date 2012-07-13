#region Using's

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#endregion Using's

namespace Auction
{
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class ReadOnlyCollection<TItem> : ICollection<TItem>, IEnumerable<TItem>, IEnumerable
    {
        #region Fields

        private readonly ICollection<TItem> items;

        #endregion Fields

        #region Constructor(s)

        public ReadOnlyCollection(ICollection<TItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            } //if

            this.items = items;
        }

        #endregion Constructor(s)

        #region Properties

        protected ICollection<TItem> Items
        {
            [DebuggerStepThrough]
            get { return items; }
        }

        #endregion Properties

        #region Methods

        private Exception CreateReadOnlyException()
        {
            return new NotSupportedException("Collection is read-only!");
        }

        #endregion Methods

        #region ICollection<TItem> Members

        void ICollection<TItem>.Add(TItem item)
        {
            throw CreateReadOnlyException();
        }

        void ICollection<TItem>.Clear()
        {
            throw CreateReadOnlyException();
        }

        public bool Contains(TItem item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            [DebuggerStepThrough]
            get { return Items.Count; }
        }

        bool ICollection<TItem>.IsReadOnly
        {
            [DebuggerStepThrough]
            get { return true; }
        }

        bool ICollection<TItem>.Remove(TItem item)
        {
            throw CreateReadOnlyException();
        }

        #endregion ICollection<TItem> Members

        #region IEnumerable<TItem> Members

        public IEnumerator<TItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion IEnumerable<TItem> Members

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable Members
    }

}