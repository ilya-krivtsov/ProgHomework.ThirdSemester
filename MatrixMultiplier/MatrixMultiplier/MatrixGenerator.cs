// <copyright file="MatrixGenerator.cs" company="Ilya Krivtsov">
// Copyright (c) Ilya Krivtsov. All rights reserved.
// </copyright>

namespace MatrixMultiplier;

/// <summary>
/// Matrix of integer numbers.
/// </summary>
public class MatrixGenerator
{
    /// <summary>
    /// Generates random matrix.
    /// </summary>
    /// <param name="rows">Row count.</param>
    /// <param name="columns">Column count.</param>
    /// <param name="random">Random to use.</param>
    /// <param name="min">Min value in matrix (inclusive).</param>
    /// <param name="max">Max value in matrix (inclusive).</param>
    /// <returns>Generated matrix.</returns>
    public static Matrix GenerateMatrix(int rows, int columns, Random random, int min = int.MinValue, int max = int.MaxValue)
    {
        var matrix = new Matrix(rows, columns);
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                matrix[row, column] = (int)random.NextInt64(min, max + 1);
            }
        }

        return matrix;
    }

    /// <summary>
    /// Returns identity matrix.
    /// </summary>
    /// <param name="rows">Row count.</param>
    /// <param name="columns">Column count.</param>
    /// <returns>Identity matrix.</returns>
    public static Matrix Identity(int rows, int columns)
    {
        var matrix = new Matrix(rows, columns);
        for (int i = 0; i < Math.Min(rows, columns); i++)
        {
            matrix[i, i] = 1;
        }

        return matrix;
    }
}
