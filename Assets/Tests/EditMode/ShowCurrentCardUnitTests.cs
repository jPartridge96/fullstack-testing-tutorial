using NUnit.Framework;
using Moq;
using System;
using UnityEngine;

public class ShowCurrentCardUnitTests
{
    // This is the class we will test
    ShowRandomCardImpl showRandomCardImpl;

    // These are the mocked dependencies that we inject for the test
    Mock<IText> currentCardLabel;
    Mock<IGameObject> parent;
    Mock<IRandom> rng;
    Mock<ILambda> drawCardAction; // For testing actions.

    public interface ILambda
    {
        void callOnAction();
    }

    // Good tests should look as close to test casses written in 
    // English as possible. Try to use helpers to achieve this!
    [Test]
    public void AceOfSpadesIsRightColor()
    {
        TestSuitRankMatchesColor(
            ShowRandomCardImpl.Suit.Hearts,
            ShowRandomCardImpl.Rank.Ten,
            Color.red);
    }

    [Test]
    public void TenOfHeartsIsRightColor()
    {
        TestSuitRankMatchesColor(
            ShowRandomCardImpl.Suit.Hearts,
            ShowRandomCardImpl.Rank.Ten,
            Color.red);
    }

    // It's also a good idea to test that your code fails properly.
    [Test]
    public void LabelMissingException()
    {
        showRandomCardImpl = new(
            parent.Object,
            null,
            rng.Object,
            () => drawCardAction.Object.callOnAction());
        Assert.Throws<MissingReferenceException>(showRandomCardImpl.Awake);
    }

    void TestSuitRankMatchesColor(
        ShowRandomCardImpl.Suit suit,
        ShowRandomCardImpl.Rank rank,
        Color color)
    {
        // Some people call this the "arrange" phase.
        SetupRng(suit, rank);
        SetupCurrentCardLabel();
        SetupDrawCardAction();

        // Some people call this the "act" phase.
        DrawCard();

        // And this the "assert" phase.
        VerifyCurrentCardLabel(suit, rank, color);
        VerifyDrawCardAction();
    }

    // Note that I avoid using `Mock.Setup()` inside of our [SetUp].
    // This helps avoid unused mock errors. [SetUp] is called before
    // every test.
    [SetUp]
    public void SetUp()
    {
        currentCardLabel = new Mock<IText>();
        parent = new Mock<IGameObject>();
        rng = new Mock<IRandom>();
        drawCardAction = new Mock<ILambda>();
        showRandomCardImpl = new(
            parent.Object,
            currentCardLabel.Object,
            rng.Object,
            () => drawCardAction.Object.callOnAction());
    }

    // Instead I put `Mock.Setup()`s into their own helpers.
    void SetupRng(
        ShowRandomCardImpl.Suit suit,
        ShowRandomCardImpl.Rank rank)
    {
        int numberofSuits =
            Enum.GetValues(typeof(ShowRandomCardImpl.Suit)).Length;
        rng.Setup(x => x.Next(numberofSuits)).Returns((int)suit);

        int numberofRanks =
            Enum.GetValues(typeof(ShowRandomCardImpl.Rank)).Length;
        rng.Setup(x => x.Next(numberofRanks)).Returns((int)rank);
    }

    void SetupCurrentCardLabel()
    {
        currentCardLabel.SetupSet(x => x.text = It.IsAny<string>());
    }

    // I find putting Verify commands into helpers make tests read batter
    void VerifyCurrentCardLabel(
        ShowRandomCardImpl.Suit suit,
        ShowRandomCardImpl.Rank rank,
        Color color)
    {
        currentCardLabel.VerifySet(
            x => x.text = $"{rank} of {suit}", Times.Once);
        currentCardLabel.VerifySet(x=> x.color = color, Times.Once);
    }

    void SetupDrawCardAction()
    {
        drawCardAction.Setup(x => x.callOnAction());
    }

    void VerifyDrawCardAction()
    {
        drawCardAction.Verify(x => x.callOnAction());
    }

    // Even these small methods help for readability.
    void DrawCard()
    {
        showRandomCardImpl.OnMouseDown();
    }
}
