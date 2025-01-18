public class QuaternionTensor
{
    public double[,] Tensor { get; private set; }

    public QuaternionTensor(int rows, int columns)
    {
        Tensor = new double[rows, columns];
    }

    public QuaternionTensor(double[,] tensor)
    {
        Tensor = tensor;
    }

    public double this[int row, int column]
    {
        get { return Tensor[row, column]; }
        set { Tensor[row, column] = value; }
    }

    public static QuaternionTensor operator +(QuaternionTensor a, QuaternionTensor b)
    {
        if (a.Tensor.GetLength(0) != b.Tensor.GetLength(0) || a.Tensor.GetLength(1) != b.Tensor.GetLength(1))
        {
            throw new ArgumentException("Tensor dimensions must match for addition.");
        }

        int rows = a.Tensor.GetLength(0);
        int columns = a.Tensor.GetLength(1);
        double[,] result = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }

        return new QuaternionTensor(result);
    }

    public static QuaternionTensor operator -(QuaternionTensor a, QuaternionTensor b)
    {
        if (a.Tensor.GetLength(0) != b.Tensor.GetLength(0) || a.Tensor.GetLength(1) != b.Tensor.GetLength(1))
        {
            throw new ArgumentException("Tensor dimensions must match for subtraction.");
        }

        int rows = a.Tensor.GetLength(0);
        int columns = a.Tensor.GetLength(1);
        double[,] result = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }

        return new QuaternionTensor(result);
    }

    public static QuaternionTensor operator *(QuaternionTensor a, double scalar)
    {
        int rows = a.Tensor.GetLength(0);
        int columns = a.Tensor.GetLength(1);
        double[,] result = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = a[i, j] * scalar;
            }
        }

        return new QuaternionTensor(result);
    }

    public static QuaternionTensor operator /(QuaternionTensor a, double scalar)
    {
        if (scalar == 0)
        {
            throw new DivideByZeroException("Scalar must not be zero.");
        }

        int rows = a.Tensor.GetLength(0);
        int columns = a.Tensor.GetLength(1);
        double[,] result = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = a[i, j] / scalar;
            }
        }

        return new QuaternionTensor(result);
    }
}