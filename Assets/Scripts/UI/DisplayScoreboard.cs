using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreboard : MonoBehaviour
{
    [SerializeField]
    private RectTransform scoreboardPanel;

    [SerializeField]
    private PlayerScoreboardController scoreboardPlayerDataPrefab;
    [SerializeField]
    private RectTransform scoreboardHeaderPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SetScoreboardVisible(true);
            RefreshScoreboard();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            SetScoreboardVisible(false);
        }
    }

    private void RefreshScoreboard()
    {
        foreach (Transform child in scoreboardPanel.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }

        Instantiate(scoreboardHeaderPrefab, scoreboardPanel);

        ChrControllerBolt[] players = FindObjectsOfType(typeof(ChrControllerBolt)) as ChrControllerBolt[];

        foreach (var player in players)
        {
            PlayerScoreboardController playerScore = Instantiate(scoreboardPlayerDataPrefab, scoreboardPanel);
            playerScore.ShowText(" ", player.state.Kills, player.state.Deaths);
        }
    }

    private void SetScoreboardVisible(bool state)
    {
        scoreboardPanel.gameObject.SetActive(state);
    }
}