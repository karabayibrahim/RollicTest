using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("CompletePanel")]
    [SerializeField] private GameObject CompletePanel;
    [SerializeField] private Button NextButton;
    [Header("FailPanel")]
    [SerializeField] private GameObject FailPanel;
    [SerializeField] private Button RetryButton;
    //[Header("GameStartPanel")]
    //[SerializeField] private GameObject GameStartPanel;
    //[SerializeField] private Button TapToButton;
    [Header("InGamePanel")]
    [SerializeField] private GameObject InGamePanel;
    [SerializeField] private Text LevelText;
    [SerializeField] private Button GameStartBUtton;
    //[SerializeField] private Button RestartButton;




    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        GameStartBUtton.onClick.AddListener(TapToStatus);
        //RestartButton.onClick.AddListener(RestartStatus);
        RetryButton.onClick.AddListener(RestartStatus);
        NextButton.onClick.AddListener(NextStatus);
        //GameManager.WinEvent += WinStatus;
        //GameManager.FailEvent += FailStatus;
        LevelTextUpdate();
        
    }

    private void OnDisable()
    {
        GameStartBUtton.onClick.RemoveListener(TapToStatus);
        //RestartButton.onClick.RemoveListener(RestartStatus);
        RetryButton.onClick.RemoveListener(RestartStatus);
        NextButton.onClick.RemoveListener(NextStatus);
        //GameManager.WinEvent -= WinStatus;
        //GameManager.FailEvent -= FailStatus;
    }

    // Update is called once per frame
    void Update()
    {

    }

   

    private void TapToStatus()
    {
        GameStartBUtton.gameObject.SetActive(false);
        GameManager.Instance.GameState = GameState.PLAY;
        //InGamePanel.SetActive(true);
    }
    private void RestartStatus()
    {
        FailPanel.SetActive(false);
        InGamePanel.SetActive(true);
        GameManager.Instance.RestartGameStatus();
    }

    private void NextStatus()
    {
        GameManager.Instance.NextGameStatus();
        CompletePanel.SetActive(false);
        InGamePanel.SetActive(true);
        //if (PlayerPrefs.GetInt("LevelIndex") >= 4)
        //{
        //    PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") + 1);
        //    SceneManager.LoadScene("Level" + Random.Range(1, 5));
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") + 1);
        //    SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("LevelIndex"));
        //}
    }

    public void LevelTextUpdate()
    {
        LevelText.text = "LEVEL" + " " + PlayerPrefs.GetInt("PlayerLevel");
    }
    public void PlayStatus()
    {

    }
    public void GameStartStatus()
    {
        GameStartBUtton.gameObject.SetActive(true);
    }

    public void WinStatus()
    {
        InGamePanel.SetActive(false);
        CompletePanel.SetActive(true);
    }

    public void FailStatus()
    {
        InGamePanel.SetActive(false);
        FailPanel.SetActive(true);
    }

}
