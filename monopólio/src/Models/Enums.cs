namespace Monopólio.Models;

/// <summary>
/// Tipo de espaço no tabuleiro
/// </summary>
public enum SpaceType
{
    Start,              // Partida (Start)
    Property,           // Propriedade (Teal, Pink, White, Red, Yellow, Green, Blue, Black)
    Chance,             // Sorte
    Community,          // Comunidade
    FreePark,           // Parque Gratuito
    Police,             // Polícia (vai preso)
    Prison,             // Cadeia (só de passagem)
    LuxTax              // Imposto de luxo
}

/// <summary>
/// Tipo de carta (Chance ou Comunidade)
/// </summary>
public enum CardType
{
    Chance,
    Community
}

/// <summary>
/// Estado do turno do jogador
/// </summary>
public enum TurnState
{
    RollingDice,        // Aguardando LD
    DiceRolled,         // Dados lançados, peça movida
    AwaitingAction,     // Pode comprar/pagar/etc
    Completed,          // Turno completado
    InPrison            // Preso na cadeia
}

/// <summary>
/// Cores das propriedades
/// </summary>
public enum PropertyColor
{
    Teal,
    Pink,
    White,
    Red,
    Yellow,
    Green,
    Blue,
    Black
}
