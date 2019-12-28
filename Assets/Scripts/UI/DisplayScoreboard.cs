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

        for (int i = 0; i < PlayerObjectRegistry.players.Count; i++)
        {
            // IPvpPlayerState playerState = PlayerObjectRegistry.players[i].character;
            PlayerScoreboardController playerScore = Instantiate(scoreboardPlayerDataPrefab, scoreboardPanel);
            playerScore.ShowText("TEST", 100, 99);
        }
    }

    private void SetScoreboardVisible(bool state)
    {
        scoreboardPanel.gameObject.SetActive(state);
    }
}