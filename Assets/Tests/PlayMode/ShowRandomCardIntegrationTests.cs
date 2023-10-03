using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ShowRandomCardIntegrationTests
{
    const int POINTS_TO_WIN = 10;

    ShowRandomCard randomCard;
    WinCondition winCondition;
    TextMeshProUGUI currentCardLabel;
    TextMeshProUGUI winLabel;

    // Notice: Lots of asserts, and reads like an English test case.
    [Test]
    public void Draw10Cards_WinEvent()
    {
        VerifyIsWinLabelActive(false);
        for (int i = 0; i < POINTS_TO_WIN; i++)
        {
            VerifyIsWinLabelActive(false);
            DrawCard();
        }
        VerifyIsWinLabelActive(true);
    }

    // Let's make this one an IEnumerator [UnityTest],
    // which allows us to advance to future frames.
    [UnityTest]
    public IEnumerator MouseDownOnce_UpdateCardText()
    {
        VerifyIsCurrentCardLabelEmpty(true);
        DrawCard();

        //  The following will advance us one frame.
        // "yield return new WaitForFixedUpdate();" is more useful for physics
        yield return null;

        VerifyIsCurrentCardLabelEmpty(false);
    }

    [SetUp]
    public void SetUp()
    {
        ClearScene(); // Stop state from persisting between tests!
        SetupCurrentCardLabel();
        SetupWinLabel();
        SetupRandomCard();
    }

    // Even methods like these help with readability!
    void DrawCard()
    {
        randomCard.OnMouseDown();
    }

    void SetupCurrentCardLabel()
    {
        GameObject currentCardLabelContainer = new GameObject();
        currentCardLabelContainer.SetActive(false); // Avoid calling Awake()/Start() early.

        currentCardLabelContainer.AddComponent<TextMeshProUGUI>();
        currentCardLabel = currentCardLabelContainer.GetComponent<TextMeshProUGUI>();

        currentCardLabelContainer.SetActive(true); // Awake()/Start() is called now.
    }

    void SetupWinLabel()
    {
        GameObject winLabelContainer = new GameObject();
        winLabelContainer.SetActive(false);

        winLabelContainer.AddComponent<TextMeshProUGUI>();
        winLabel = winLabelContainer.GetComponent<TextMeshProUGUI>();

        // Don't activate this yet!
    }

    void SetupRandomCard()
    {
        GameObject randomCardContainer = new GameObject();
        randomCardContainer.SetActive(false);

        randomCard = randomCardContainer.AddComponent<ShowRandomCard>();
        randomCard.currentCardLabel = currentCardLabel;
        randomCard.drawCard = new UnityEvent();
        randomCard.drawCard.AddListener(() => winCondition.AddPoints(1));

        winCondition = randomCardContainer.AddComponent<WinCondition>();
        winCondition.winningLabel = winLabel;
        winCondition.pointsToWin = POINTS_TO_WIN;
        winCondition.currentPoints = 0;

        randomCardContainer.SetActive(true);
    }

    // We prefer creating a new scene vs. trying to reset the current scene inside of a [TearDown].
    int sceneCounter = 0;
    void ClearScene()
    {
        SceneManager.SetActiveScene(SceneManager.CreateScene($"TestScene{sceneCounter}"));
        if (sceneCounter > 0)
        {
            SceneManager.UnloadSceneAsync($"TestScene{sceneCounter - 1}");
        }
        sceneCounter++;
    }

    void VerifyIsCurrentCardLabelEmpty(bool isEmpty)
    {
        Assert.AreEqual(isEmpty, currentCardLabel.text.Length == 0);
    }

    void VerifyIsWinLabelActive(bool isActive)
    {
        Assert.AreEqual(isActive, winLabel.isActiveAndEnabled);
    }
}