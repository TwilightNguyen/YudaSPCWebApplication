using System;

public class NotInDbSet<T>: IQuerable<T>, IAsysncEnumerable<T>, IEmumerable<T>, IEnumerable
{
    private readonly List<T> _innerCollection;

    public NotInDbSet(IEnumerable<T> innerCollection)
    {
        _innerCollection = new List<T>(innerCollection);
    }
    
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new cancellationToken())
    {
        return new IAsyncEnumerator<T>(GetEnumerator());
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _innerCollection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class AsyncEnumerator : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _innerEnumerator;
        public AsyncEnumerator(IEnumerator<T> innerEnumerator)
        {
            _innerEnumerator = innerEnumerator;
        }
        public T Current => _innerEnumerator.Current;
        public ValueTask DisposeAsync()
        {
            _innerEnumerator.Dispose();
            return ValueTask.CompletedTask;
        }
        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_innerEnumerator.MoveNext());
        }
    }

    public Type ElementType => typeof(T);
    public Expression Expression => _innerCollection.AsQueryable().Expression;
    public IQueryProvider Provider => _innerCollection.AsQueryable().Provider;
}