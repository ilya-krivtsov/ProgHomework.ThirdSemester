// <copyright file="ParallelMultiplier.cs" company="Ilya Krivtsov">
// Copyright (c) Ilya Krivtsov. All rights reserved.
// </copyright>

namespace MatrixMultiplier;

/// <summary>
/// Multi-threaded matrix multiplier.
/// </summary>
public class ParallelMultiplier : IMatrixMultiplier
{
    /// <inheritdoc/>
    public bool Multiply(Matrix left, Matrix right, Matrix result)
    {
        if (!Matrix.VerifyMultiplication(left, right, out int columns, out int rows) || rows != result.Rows || columns != result.Columns)
        {
            return false;
        }

        var queue = new Queue<Workload>();
        for (int row = 0; row < left.Rows; row++)
        {
            queue.Enqueue(new(row));
        }

        var threadCount = Environment.ProcessorCount;
        var threads = new Thread[threadCount];
        var workers = new ThreadWorker[threadCount];
        for (int i = 0; i < threadCount; i++)
        {
            workers[i] = new ThreadWorker(left, right, result, queue);
            threads[i] = new Thread(workers[i].Process);
            threads[i].Start();
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }

        return true;
    }

    private record struct Workload(int ResultRow);

    private struct ThreadWorker(Matrix left, Matrix right, Matrix result, Queue<Workload> workloads)
    {
        public readonly void Process()
        {
            while (true)
            {
                Workload workload;
                lock (workloads)
                {
                    if (!workloads.TryDequeue(out workload))
                    {
                        return;
                    }
                }

                for (int column = 0; column < right.Columns; column++)
                {
                    result[workload.ResultRow, column] = ScalarProduct(left, right, workload.ResultRow, column);
                }
            }
        }

        private static int ScalarProduct(Matrix left, Matrix right, int leftRow, int rightColumn)
        {
            int accum = 0;
            for (int i = 0; i < left.Columns; i++)
            {
                accum += left[leftRow, i] * right[i, rightColumn];
            }

            return accum;
        }
    }
}
