namespace Monopólio.Rules;

using Monopólio.Models;

/// <summary>
/// Constrói o layout do tabuleiro 7x7 conforme o enunciado
/// Start no centro (3,3)
/// </summary>
public static class BoardLayout
{
    /// <summary>
    /// Cria um tabuleiro completamente configurado
    /// </summary>
    public static Board CreateBoard()
    {
        var board = new Board();
        ConfigureBoard(board);
        return board;
    }

    private static void ConfigureBoard(Board board)
    {
        // Criar todos os espaços com suas cores
        var spaces = new Dictionary<string, (int row, int col, SpaceType type, decimal price, PropertyColor? color)>
        {
            // Canto superior-esquerdo (Teal)
            { "Teal1", (0, 0, SpaceType.Property, 100m, PropertyColor.Teal) },
            { "Teal2", (0, 1, SpaceType.Property, 120m, PropertyColor.Teal) },

            // Canto superior-direito (Pink)
            { "Pink1", (0, 5, SpaceType.Property, 180m, PropertyColor.Pink) },
            { "Pink2", (0, 6, SpaceType.Property, 200m, PropertyColor.Pink) },

            // Canto inferior-direito (Red)
            { "Red1", (6, 5, SpaceType.Property, 260m, PropertyColor.Red) },
            { "Red2", (6, 6, SpaceType.Property, 280m, PropertyColor.Red) },

            // Canto inferior-esquerdo (Yellow)
            { "Yellow1", (6, 0, SpaceType.Property, 220m, PropertyColor.Yellow) },
            { "Yellow2", (6, 1, SpaceType.Property, 240m, PropertyColor.Yellow) },

            // Lado superior-esquerdo (White)
            { "White1", (0, 2, SpaceType.Property, 140m, PropertyColor.White) },
            { "White2", (0, 3, SpaceType.Property, 160m, PropertyColor.White) },

            // Lado superior-direito (Green)
            { "Green1", (1, 6, SpaceType.Property, 200m, PropertyColor.Green) },
            { "Green2", (2, 6, SpaceType.Property, 220m, PropertyColor.Green) },

            // Lado inferior-direito (Blue)
            { "Blue1", (5, 6, SpaceType.Property, 280m, PropertyColor.Blue) },
            { "Blue2", (6, 5, SpaceType.Property, 300m, PropertyColor.Blue) },

            // Lado inferior-esquerdo (Orange)
            { "Orange1", (5, 0, SpaceType.Property, 240m, PropertyColor.Yellow) },
            { "Orange2", (6, 0, SpaceType.Property, 260m, PropertyColor.Yellow) },

            // Lado esquerdo (Black)
            { "Black1", (3, 0, SpaceType.Property, 320m, PropertyColor.Black) },
            { "Black2", (4, 0, SpaceType.Property, 340m, PropertyColor.Black) },

            // Lado direito (Dark Blue)
            { "DarkBlue1", (3, 6, SpaceType.Property, 350m, PropertyColor.Blue) },
            { "DarkBlue2", (4, 6, SpaceType.Property, 400m, PropertyColor.Blue) },

            // Espaços especiais
            { "Start", (3, 3, SpaceType.Start, 0, null) },
            { "Police", (1, 1, SpaceType.Police, 0, null) },
            { "Prison", (5, 5, SpaceType.Prison, 0, null) },
            { "FreePark", (5, 1, SpaceType.FreePark, 0, null) },
            { "LuxTax", (1, 5, SpaceType.LuxTax, 0, null) },
            { "Chance1", (0, 4, SpaceType.Chance, 0, null) },
            { "Chance2", (4, 6, SpaceType.Chance, 0, null) },
            { "Community1", (2, 0, SpaceType.Community, 0, null) },
            { "Community2", (6, 2, SpaceType.Community, 0, null) },
            { "Community3", (6, 4, SpaceType.Community, 0, null) },
        };

        // Registar todos os espaços
        foreach (var (name, (row, col, type, price, color)) in spaces)
        {
            var space = new Space(name, type, price, color);
            board.RegisterSpace(space, row, col);
        }

        // Preencher quaisquer células vazias com espaços por defeito (garante 7x7 completo)
        for (int r = 0; r < 7; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                if (board.GetSpaceAtPosition(r, c) == null)
                {
                    string autoName = $"Esp_{r}_{c}";
                    var autoSpace = new Space(autoName, SpaceType.Property, 100m, null);
                    board.RegisterSpace(autoSpace, r, c);
                }
            }
        }
    }
}

