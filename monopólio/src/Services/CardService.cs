namespace Monopólio.Services;

using Monopólio.Models;

public class CardService
{
    private readonly List<Card> _chanceDeck;
    private readonly List<Card> _communityDeck;
    private readonly Random _random;

    public CardService(Random? random = null)
    {
        _random = random ?? new Random();
        _chanceDeck = DeckFactory.CreateChanceDeck();
        _communityDeck = DeckFactory.CreateCommunityDeck();
        DeckFactory.ShuffleDeck(_chanceDeck, _random);
        DeckFactory.ShuffleDeck(_communityDeck, _random);
    }

    public Card DrawChanceCard()
    {
        var card = DrawWeighted(_chanceDeck);
        // cycle card to bottom
        if (card != null)
        {
            _chanceDeck.Remove(card);
            _chanceDeck.Add(card);
        }
        return card!;
    }

    public Card DrawCommunityCard()
    {
        var card = DrawWeighted(_communityDeck);
        if (card != null)
        {
            _communityDeck.Remove(card);
            _communityDeck.Add(card);
        }
        return card!;
    }

    public Card? DrawCard(string type)
    {
        return type.Equals("Chance", StringComparison.OrdinalIgnoreCase) ? DrawChanceCard() : DrawCommunityCard();
    }

    private Card? DrawWeighted(List<Card> deck)
    {
        if (deck == null || deck.Count == 0)
            return null;

        double r = _random.NextDouble();
        double cumulative = 0.0;
        foreach (var c in deck)
        {
            cumulative += c.Probability;
            if (r <= cumulative)
                return c;
        }

        // Floating rounding: return last
        return deck.Last();
    }

    public int GetChanceDeckSize() => _chanceDeck.Count;
    public int GetCommunityDeckSize() => _communityDeck.Count;
    public void ShuffleAllDecks()
    {
        DeckFactory.ShuffleDeck(_chanceDeck, _random);
        DeckFactory.ShuffleDeck(_communityDeck, _random);
    }
}
