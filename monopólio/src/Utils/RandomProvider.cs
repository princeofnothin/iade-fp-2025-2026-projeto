namespace Monopólio.Utils;

/// <summary>
/// Abstração de Random para permitir testes determinísticos
/// </summary>
public interface IRandomProvider
{
    int Next(int minValue, int maxValue);
    int Next(int maxValue);
    double NextDouble();
}

/// <summary>
/// Implementação padrão de IRandomProvider
/// </summary>
public class RandomProvider : IRandomProvider
{
    private readonly Random _random;

    public RandomProvider()
    {
        _random = new Random();
    }

    public RandomProvider(int seed)
    {
        _random = new Random(seed);
    }

    public int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }

    public int Next(int maxValue)
    {
        return _random.Next(maxValue);
    }

    public double NextDouble()
    {
        return _random.NextDouble();
    }
}

/// <summary>
/// Implementação mock de IRandomProvider para testes
/// </summary>
public class MockRandomProvider : IRandomProvider
{
    private readonly Queue<int> _nextIntegers = new();
    private readonly Queue<double> _nextDoubles = new();

    public void QueueInteger(int value)
    {
        _nextIntegers.Enqueue(value);
    }

    public void QueueDouble(double value)
    {
        _nextDoubles.Enqueue(value);
    }

    public int Next(int minValue, int maxValue)
    {
        if (_nextIntegers.Count > 0)
            return _nextIntegers.Dequeue();

        throw new InvalidOperationException("No more mocked values available");
    }

    public int Next(int maxValue)
    {
        if (_nextIntegers.Count > 0)
            return _nextIntegers.Dequeue();

        throw new InvalidOperationException("No more mocked values available");
    }

    public double NextDouble()
    {
        if (_nextDoubles.Count > 0)
            return _nextDoubles.Dequeue();

        throw new InvalidOperationException("No more mocked values available");
    }
}
