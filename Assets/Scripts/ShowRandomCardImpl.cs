using System;
using UnityEngine;

public class ShowRandomCardImpl
{
    // Now accessed via interface only!
    IText currentCardLabel;
    IGameObject parent;
    Action drawCard;

    IRandom rng;

    // We can inject anything that implements these interfaces!
    public ShowRandomCardImpl(
        IGameObject parent,
        IText currentCardLabel,
        IRandom rng,
        Action drawCard)
    {
        this.parent = parent;
        this.currentCardLabel = currentCardLabel;
        this.rng = rng;
        this.drawCard = drawCard;
    }

    public enum Suit
    {
        Spades, Hearts, Clubs, Diamonds
    }

    public enum Rank
    {
        Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Jack, Queen, King
    }

    T RandomSelect<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(rng.Next(values.Length));
    }

    Color SuitToColor(Suit suit)
    {
        switch (suit)
        {
            case Suit.Spades:
            case Suit.Clubs:
                return Color.black;
            case Suit.Hearts:
            case Suit.Diamonds:
                return Color.red;
            default:
                throw new InvalidOperationException($"Unknown suit: {suit}.");
        }
    }

    (string, Color) RandomCard()
    {
        Suit suit = RandomSelect<Suit>();
        Rank rank = RandomSelect<Rank>();
        return ($"{rank} of {suit}", SuitToColor(suit));
    }

    public void Awake()
    {
        if (currentCardLabel == null)
        {
            throw new MissingReferenceException(
                "Missing Current Card Label in Show Random Card on " +
                $"{parent.name}.");
        }
    }

    public void Start()
    {
        currentCardLabel.text = "";
    }

    public void OnMouseDown()
    {
        (currentCardLabel.text, currentCardLabel.color) = RandomCard();
        drawCard();
    }
}