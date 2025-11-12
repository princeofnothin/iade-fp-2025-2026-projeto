namespace Monopólio.Services;

using Monopólio.Models;

/// <summary>
/// Factory para gerar baralhos de cartas (Chance e Community)
/// Conforme as probabilidades do briefing
/// </summary>
public class DeckFactory
{
    /// <summary>
    /// Cria um baralho de Chance com as percentagens corretas
    /// </summary>
    public static List<Card> CreateChanceDeck()
    {
        var deck = new List<Card>();

        // 20% → +150
        deck.Add(new Card("Recebe 150€", "Recebe 150€ do banco", CardType.Chance, 0.20));
        deck.Last().Effect = (player, gameState) => player.AddMoney(150m);

        // 10% → +200
        deck.Add(new Card("Recebe 200€", "Recebe 200€ do banco", CardType.Chance, 0.10));
        deck.Last().Effect = (player, gameState) => player.AddMoney(200m);

        // 10% → −70
        deck.Add(new Card("Paga 70€", "Paga 70€ para o FreePark", CardType.Chance, 0.10));
        deck.Last().Effect = (player, gameState) => {
            if (player.DeductMoney(70m))
                gameState.AddToFreeParkPot(70m);
        };

        // 20% → Move-se para Start
        deck.Add(new Card("Vai para Start", "Move-se para Start", CardType.Chance, 0.20));
        deck.Last().Effect = (player, gameState) => {
            player.Position = gameState.Board.CoordinatesToLinearIndex(3, 3);
            player.AddMoney(200m);  // Bónus ao passar por Start
        };

        // 20% → Move-se para Police
        deck.Add(new Card("Vai para Police", "Move-se para Police", CardType.Chance, 0.20));
        deck.Last().Effect = (player, gameState) => {
            var policeSpace = gameState.Board.GetSpaceByName("Police");
            if (policeSpace != null)
                player.Position = gameState.Board.CoordinatesToLinearIndex(policeSpace.Row, policeSpace.Column);
            player.IsInPrison = true;
            player.TurnsInPrison = 0;
        };

        // 20% → Move-se para FreePark
        deck.Add(new Card("Vai para FreePark", "Move-se para FreePark", CardType.Chance, 0.20));
        deck.Last().Effect = (player, gameState) => {
            var freeParkSpace = gameState.Board.GetSpaceByName("FreePark");
            if (freeParkSpace != null)
                player.Position = gameState.Board.CoordinatesToLinearIndex(freeParkSpace.Row, freeParkSpace.Column);
        };

        return deck;
    }

    /// <summary>
    /// Cria um baralho de Community com as percentagens corretas
    /// </summary>
    public static List<Card> CreateCommunityDeck()
    {
        var deck = new List<Card>();

        // 10% → Paga 20 por casa
        deck.Add(new Card("Paga 20 por casa", "Paga 20€ por cada casa que possui", CardType.Community, 0.10));
        deck.Last().Effect = (player, gameState) => {
            int totalHouses = player.OwnedProperties.Sum(p => p.NumberOfHouses);
            decimal amount = totalHouses * 20m;
            if (player.DeductMoney(amount))
                gameState.AddToFreeParkPot(amount);
        };

        // 10% → Recebe 10 de cada jogador
        deck.Add(new Card("Recebe 10 de cada", "Recebe 10€ de cada outro jogador", CardType.Community, 0.10));
        deck.Last().Effect = (player, gameState) => {
            foreach (var other in gameState.Players)
            {
                if (other != player && other.DeductMoney(10m))
                    player.AddMoney(10m);
            }
        };

        // 20% → +100
        deck.Add(new Card("Recebe 100€", "Recebe 100€ do banco", CardType.Community, 0.20));
        deck.Last().Effect = (player, gameState) => player.AddMoney(100m);

        // 20% → +170
        deck.Add(new Card("Recebe 170€", "Recebe 170€ do banco", CardType.Community, 0.20));
        deck.Last().Effect = (player, gameState) => player.AddMoney(170m);

        // 10% → −40
        deck.Add(new Card("Paga 40€", "Paga 40€ para o FreePark", CardType.Community, 0.10));
        deck.Last().Effect = (player, gameState) => {
            if (player.DeductMoney(40m))
                gameState.AddToFreeParkPot(40m);
        };

        // 10% → Move-se para Pink1
        deck.Add(new Card("Vai para Pink1", "Move-se para Pink1", CardType.Community, 0.10));
        deck.Last().Effect = (player, gameState) => {
            var pinkSpace = gameState.Board.GetSpaceByName("Pink1");
            if (pinkSpace != null)
                player.Position = gameState.Board.CoordinatesToLinearIndex(pinkSpace.Row, pinkSpace.Column);
        };

        // 10% → Move-se para Teal2
        deck.Add(new Card("Vai para Teal2", "Move-se para Teal2", CardType.Community, 0.10));
        deck.Last().Effect = (player, gameState) => {
            var tealSpace = gameState.Board.GetSpaceByName("Teal2");
            if (tealSpace != null)
                player.Position = gameState.Board.CoordinatesToLinearIndex(tealSpace.Row, tealSpace.Column);
        };

        // 10% → Move-se para White2
        deck.Add(new Card("Vai para White2", "Move-se para White2", CardType.Community, 0.10));
        deck.Last().Effect = (player, gameState) => {
            var whiteSpace = gameState.Board.GetSpaceByName("White2");
            if (whiteSpace != null)
                player.Position = gameState.Board.CoordinatesToLinearIndex(whiteSpace.Row, whiteSpace.Column);
        };

        return deck;
    }

    /// <summary>
    /// Embaralha um baralho
    /// </summary>
    public static void ShuffleDeck(List<Card> deck, Random? random = null)
    {
        random ??= new Random();
        
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);
            (deck[i], deck[randomIndex]) = (deck[randomIndex], deck[i]);
        }
    }

    /// <summary>
    /// Remove uma carta do topo do baralho e coloca no fundo (cicla)
    /// </summary>
    public static Card DrawCard(List<Card> deck)
    {
        if (deck.Count == 0)
            throw new InvalidOperationException("Baralho vazio");

        var card = deck[0];
        deck.RemoveAt(0);
        deck.Add(card);
        
        return card;
    }
}

