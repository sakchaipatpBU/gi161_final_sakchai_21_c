using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameEndUi;
    public TMP_Text GameEndText;
    public bool IsWin = false;
    public int RemainingEnemy = 0;
    public bool IsNoWaveRemain = false;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        GameEndUi.SetActive(false);
    }

    public void ShowGameEnd()
    {
        GameEndUi.SetActive(true);
        if (IsWin)
        {
            GameEndText.text = "Congratulations! You Win!";
        }
        else
        {
            GameEndText.text = "Keep trying, don't give up.";
        }
    }
    public void EnemyIsDestroy()
    {
        RemainingEnemy--;
        if (RemainingEnemy == 0 && IsNoWaveRemain)
        {
            IsWin = true;
            ShowGameEnd();
        }
    }
    public void OnReplayButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
