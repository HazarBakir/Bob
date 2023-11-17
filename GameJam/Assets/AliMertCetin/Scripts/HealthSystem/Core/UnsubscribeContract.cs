using System;
using System.Collections.Generic;

namespace XIV.DesignPatterns.Common.HealthSystem
{
    public struct UnsubscribeContract<T> : IDisposable
    {
        IObserver<T> item;
        ICollection<IObserver<T>> collection;
        
        public UnsubscribeContract(IObserver<T> item, ICollection<IObserver<T>> collection)
        {
            this.item = item;
            this.collection = collection;
        }
        
        public void Dispose()
        {
            collection.Remove(item);
            item = default;
            collection = default;
        }
    }
}