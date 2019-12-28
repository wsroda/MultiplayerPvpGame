using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboardController : MonoBehaviour
{
    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text killsText;

    [SerializeField]
    private Text deathsText;

    public void ShowText(string name, int kills, int deaths)
    {
        nameText.text = name;
        killsText.text = kills.ToString();
        deathsText.text = deaths.ToString();
    }
}
