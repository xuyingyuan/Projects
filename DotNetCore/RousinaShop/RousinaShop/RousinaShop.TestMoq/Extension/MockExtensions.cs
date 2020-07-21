using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RousinaShop.TestMoq.Extension
{
    public static class MockExtensions
    {
        public static Mock<DbSet<T>> ToAsyncDbSetMock<T>(this IEnumerable<T> source)
           where T : class
        {
            var data = source.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();

            //mockSet.As<IAsyncEnumerable<T>>()
            //    .Setup(x => x.GetAsyncEnumerator())               
            //    .Returns(new MockAsyncEnumerator<T>(data.GetEnumerator()));

            //mockSet.As<IQueryable<T>>()
            //    .Setup(x => x.Provider)
            //    .Returns(new MockAsyncQueryProvider<T>(data.Provider));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => data.GetEnumerator());

            return mockSet;
        }
    }
}
