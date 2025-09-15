// <copyright file="MultiplierTests.cs" company="Ilya Krivtsov">
// Copyright (c) Ilya Krivtsov. All rights reserved.
// </copyright>

namespace MatrixMultiplier.Tests;

public class MultiplierTests
{
    private static readonly int[] MatrixSizes = [32, 64, 128];
    private static readonly IMatrixMultiplier[] Multipliers = [new SerialMultiplier(), new ParallelMultiplier()];

    [Test]
    public void Test_SquareMatrixMultiplication([ValueSource(nameof(MatrixSizes))] int size)
    {
        var random = new Random(125684356);
        var left = MatrixGenerator.GenerateMatrix(size, size, random);
        var right = MatrixGenerator.GenerateMatrix(size, size, random);

        var results = new Matrix[2];
        for (int i = 0; i < 2; i++)
        {
            results[i] = new(size, size);
            Multipliers[i].Multiply(left, right, results[i]);
        }

        for (int row = 0; row < size; row++)
        {
            Assert.That(results[0].GetRow(row).SequenceEqual(results[1].GetRow(row)), Is.True);
        }
    }

    [Test]
    public void Test_RectangleMatrixMultiplication([ValueSource(nameof(MatrixSizes))] int size)
    {
        var random = new Random(125684356);
        var left = MatrixGenerator.GenerateMatrix(size, size / 2, random);
        var right = MatrixGenerator.GenerateMatrix(size / 2, size, random);

        var results = new Matrix[2];
        for (int i = 0; i < 2; i++)
        {
            results[i] = new(size, size);
            Multipliers[i].Multiply(left, right, results[i]);
        }

        for (int row = 0; row < size; row++)
        {
            Assert.That(results[0].GetRow(row).SequenceEqual(results[1].GetRow(row)), Is.True);
        }
    }

    [Test]
    public void Test_IdentityMultiplication([ValueSource(nameof(MatrixSizes))] int size)
    {
        var random = new Random(125684356);
        var left = MatrixGenerator.GenerateMatrix(size, size, random);
        var right = MatrixGenerator.Identity(size, size);

        for (int i = 0; i < 2; i++)
        {
            var result = new Matrix(size, size);
            Multipliers[i].Multiply(left, right, result);

            for (int row = 0; row < size; row++)
            {
                Assert.That(left.GetRow(row).SequenceEqual(result.GetRow(row)), Is.True);
            }
        }
    }

    [Test]
    public void Should_ReturnFalse_OnIncompatibleSizes([ValueSource(nameof(Multipliers))] IMatrixMultiplier multiplier, [ValueSource(nameof(MatrixSizes))] int size)
    {
        Assert.Multiple(() =>
        {
            // left and right are incompatible
            Assert.That(multiplier.Multiply(MatrixGenerator.Identity(size, size / 2), MatrixGenerator.Identity(size, size / 2), new(size, size)), Is.False);

            // result is incompatible with left * right
            Assert.That(multiplier.Multiply(MatrixGenerator.Identity(size, size / 2), MatrixGenerator.Identity(size / 2, size), new(size, size / 2)), Is.False);
        });
    }

    [Test]
    public void ShouldThrow_OnIncorrectSize()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix(-1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix(0, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix(-1, -1));

        var matrix = new Matrix(24, 36);
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.GetRow(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.GetRow(matrix.Rows));

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = matrix[0, -1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = matrix[0, matrix.Columns]);
    }
}
