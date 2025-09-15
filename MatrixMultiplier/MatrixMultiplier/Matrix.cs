// <copyright file="Matrix.cs" company="Ilya Krivtsov">
// Copyright (c) Ilya Krivtsov. All rights reserved.
// </copyright>

namespace MatrixMultiplier;

/// <summary>
/// Matrix of integer numbers.
/// </summary>
public class Matrix
{
    private readonly int[] data;

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class.
    /// </summary>
    /// <param name="rows">Row count.</param>
    /// <param name="columns">Column count.</param>
    public Matrix(int rows, int columns)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rows, nameof(rows));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rows, nameof(rows));

        Rows = rows;
        Columns = columns;
        data = new int[rows * columns];
    }

    /// <summary>
    /// Gets column count.
    /// </summary>
    public int Columns { get; }

    /// <summary>
    /// Gets row count.
    /// </summary>
    public int Rows { get; }

    /// <summary>
    /// Gets value at row and column.
    /// </summary>
    /// <param name="row">Zero-based row index.</param>
    /// <param name="column">Zero-based column index.</param>
    /// <returns>Value at specified row and column.</returns>
    public ref int this[int row, int column]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfNegative(column, nameof(column));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, Columns, nameof(column));
            return ref GetRow(row)[column];
        }
    }

    /// <summary>
    /// Checks if two matrices are valid for multiplication.
    /// </summary>
    /// <param name="left">Left matrix.</param>
    /// <param name="right">Right matrix.</param>
    /// <param name="newRows">Number of rows in result matrix.</param>
    /// <param name="newColumns">Number of columns in result matrix.</param>
    /// <returns>
    /// <see langword="true"/> if multiplication of <paramref name="left"/> and <paramref name="right"/> matrices is correct, <see langword="false"/> otherwise.
    /// </returns>
    public static bool VerifyMultiplication(Matrix left, Matrix right, out int newRows, out int newColumns)
    {
        if (left.Columns != right.Rows)
        {
            newRows = 0;
            newColumns = 0;
            return false;
        }

        newRows = left.Rows;
        newColumns = right.Columns;
        return true;
    }

    /// <summary>
    /// Gets row of matrix.
    /// </summary>
    /// <param name="row">Zero-based row index.</param>
    /// <returns>Row of matrix.</returns>
    public Span<int> GetRow(int row)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(row, nameof(row));
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, Rows, nameof(row));

        return data.AsSpan(row * Columns, Columns);
    }
}
