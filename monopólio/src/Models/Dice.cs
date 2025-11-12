namespace Monopólio.Models;

/// <summary>
/// Representa o lançamento de dois dados
/// Valores: -3, -2, -1, 1, 2, 3 (sem 0)
/// Duplos dobram o resultado
/// </summary>
public class Dice
{
    private Random _random;

    public int FirstDie { get; private set; }
    public int SecondDie { get; private set; }
    public int Total { get; private set; }
    public bool IsDouble { get; private set; }
    public int DoubleCount { get; set; } = 0;  // Número de duplos consecutivos

    public Dice()
    {
        _random = new Random();
        FirstDie = 0;
        SecondDie = 0;
        Total = 0;
        IsDouble = false;
    }

    public Dice(Random randomProvider)
    {
        _random = randomProvider;
        FirstDie = 0;
        SecondDie = 0;
        Total = 0;
        IsDouble = false;
    }

    /// <summary>
    /// Lança os dados (valores de -3 a 3, excluindo 0)
    /// </summary>
    public void Roll()
    {
        FirstDie = GenerateDiceValue();
        SecondDie = GenerateDiceValue();

        IsDouble = FirstDie == SecondDie;
        Total = FirstDie + SecondDie;

        if (IsDouble)
        {
            DoubleCount++;
        }
        else
        {
            DoubleCount = 0;
        }
    }

    /// <summary>
    /// Gera um valor para um dado (-3 a 3, excluindo 0)
    /// </summary>
    private int GenerateDiceValue()
    {
        // Gera -3, -2, -1, 1, 2, 3
        int value = _random.Next(1, 4);  // 1, 2 ou 3
        bool isNegative = _random.Next(2) == 0;  // 50% de chance de ser negativo
        return isNegative ? -value : value;
    }

    /// <summary>
    /// Reseta o contador de duplos
    /// </summary>
    public void ResetDoubleCount()
    {
        DoubleCount = 0;
    }

    public override string ToString()
    {
        return $"{FirstDie}/{SecondDie}";
    }
}

