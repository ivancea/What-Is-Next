using System;
using Couchbase;
using Couchbase.Core;
using Couchbase.IO;
using WhatIsNext.Model.Buckets;

namespace WhatIsNext.Services
{
    public class GraphService : IGraphService
    {
        private readonly IBucket bucket;
        
        public GraphService(IGraphsBucketProvider graphsBucketProvider)
        {
            this.bucket = graphsBucketProvider.GetBucket();
        }

        private IOperationResult<T> Retry<T>(Func<IOperationResult<T>> operation)
        {
            IOperationResult<T> result = null;

            for (int i = 0; i < 3; i++)
            {
                result = operation.Invoke();

                if (result.Status != ResponseStatus.TemporaryFailure)
                {
                    return result;
                }
            }

            return result;
        }

        // public InsertGraph(Graph){}

        public void test()
        {
            Console.WriteLine("::");
            Console.WriteLine(bucket.Get<dynamic>("TestId").Status);
            Console.WriteLine(bucket.Get<dynamic>("TestId").Success);
            Console.WriteLine(bucket.Upsert("TestId", new { a = 1, b = "b"}).Success);
        }
    }
}