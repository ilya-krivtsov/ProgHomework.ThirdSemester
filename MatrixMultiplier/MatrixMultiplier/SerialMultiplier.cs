// <copyright file="SerialMultiplier.cs" company="Ilya Krivtsov">
// Copyright (c) Ilya Krivtsov. All rights reserved.
// </copyright>

namespace MatrixMultiplier;

/// <summary>
/// Single-threaded matrix multiplier.
/// </summary>
public class SerialMultiplier : IMatrixMultiplier
{
    /// <inheritdoc/>
    public bool Multiply(Matrix left, Matrix right, Matrix result)
    {
        if (!Matrix.VerifyMultiplication(left, right, out int columns, out int rows) || rows != result.Rows || columns != result.Columns)
        {
            return false;
        }

        for (int row = 0; row < left.Rows; row++)
        {
            for (int column = 0; column < right.Columns; column++)
            {
                result[row, column] = ScalarProduct(left, right, row, column);
            }
        }

        return true;
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
