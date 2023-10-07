using NUnit.Framework;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

// system test classes should be marked with so that they
// they are skipped during code coverage 
[TestFixture, ConditionalIgnore("IgnoreForCoverage", "This is a system test")]
public class FullStackTutorialSystemTests
{
    GameObject[] allObjects;
    ShowRandomCard showRandomCard;
    WinCondition winCondition;
    GameObject winLabel;
    TextMeshProUGUI currentCard;

    // Notice: Lots of asserts & reads like a test case witten in English!
    [Test]
    public void WinningWorkflow()
    {
        VerifyIsCurrentCardShowing(false);
        for (int i = 0; i < winCondition.pointsToWin; i++)
        {
            VerifyIsWin(false);
            DrawCard();
            VerifyIsCurrentCardShowing(true);
        }
        VerifyIsWin(true);
    }

    // Verifying that things are set up in the scene isn't a bad idea either.
    [Test]
    public void CheckExistsCollidersPhysicsRaycastEventSystem()
    {
        // Will throw if doesn't exist.
        FindGameObject<MeshCollider>("Safety Hat");
        FindGameObject<PhysicsRaycaster>("Main Camera");
        FindGameObject<EventSystem>("EventSystem");
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("SampleScene");
        yield return null; // Scene is loaded on next frame.

        GetAllObjects();
        (_, showRandomCard) = FindGameObject<ShowRandomCard>("Safety Hat");
        (_, winCondition) = FindGameObject<WinCondition>("Safety Hat");
        (winLabel, _) = FindGameObject<TextMeshProUGUI>("Win Text");
        (_, currentCard) = FindGameObject<TextMeshProUGUI>("Current Card");
    }

    void DrawCard()
    {
        showRandomCard.OnMouseDown(); // Raycasting will need to be manually tested.
    }

    void GetAllObjects()
    {
        allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
    }

    (GameObject, T) FindGameObject<T>(string name)
    {
        foreach (GameObject gameObject in allObjects)
        {
            T component = gameObject.GetComponent<T>();
            if (component != null && gameObject.name == name)
            {
                return (gameObject, component);
            }
        }
        throw new Exception($"Cannot find {typeof(T)} \"{name}\"");
    }

    void VerifyIsWin(bool isWin)
    {
        Assert.AreEqual(isWin, winLabel.activeInHierarchy);
    }

    void VerifyIsCurrentCardShowing(bool isShowing)
    {
        Assert.AreEqual(isShowing, currentCard.text.Length > 0);
    }
}