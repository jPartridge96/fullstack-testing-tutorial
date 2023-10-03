using TMPro;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public int currentPoints;
    public int pointsToWin;
    public TextMeshProUGUI winningLabel;

    public void AddPoints(int input)
    {
        currentPoints += input;
        if (currentPoints >= pointsToWin)
        {
            winningLabel.gameObject.SetActive(true);
        }
    }
}