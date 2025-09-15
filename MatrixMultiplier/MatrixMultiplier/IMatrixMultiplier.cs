// <copyright file="IMatrixMultiplier.cs" company="Ilya Krivtsov">
// Copyright (c) Ilya Krivtsov. All rights reserved.
// </copyright>

namespace MatrixMultiplier;

/// <summary>
/// Matrix multiplier.
/// </summary>
public interface IMatrixMultiplier
{
    /// <summary>
    /// Multiplies two matrices.
    /// </summary>
    /// <param name="left">Left matrix.</param>
    /// <param name="right">Right matrix.</param>
    /// <param name="result">Result matrix.</param>
    /// <returns>
    /// <see langword="true"/> if column count in <paramref name="left"/> is equal to row count in <paramref name="right"/>,
    /// row count in <paramref name="left"/> is equal to row count in <paramref name="result"/> and
    /// column count in <paramref name="right"/> equal column count in <paramref name="result"/>.
    /// </returns>
    public bool Multiply(Matrix left, Matrix right, Matrix result);
}
