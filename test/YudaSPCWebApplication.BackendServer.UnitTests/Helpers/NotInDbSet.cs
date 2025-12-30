using System.Collections;
using System.Linq.Expressions;

namespace YudaSPCWebApplication.BackendServer.UnitTests.Helpers
{
    public class NotInDbSet<T> : IQueryable<T>, IAsyncEnumerable<T>, IEnumerable<T>, IEnumerable
    {
        private readonly List<T> _innerCollection;

        public NotInDbSet(IEnumerable<T> innerCollection)
        {
            _innerCollection = [.. innerCollection];
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new AsyncEnumerator(GetEnumerator());
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
}