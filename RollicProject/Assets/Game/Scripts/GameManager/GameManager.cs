//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameState gameState;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject platformObject;
    [SerializeField] private GameObject levelPlatform;
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private Vector3 playerStartPosition;
    [SerializeField] private int maxLevel;
    public PlatformCalculator MyPlatformCalculateObject;
    public GameState GameState
    {
        get
        {
            return gameState;
        }
        set
        {
            if (GameState==value)
            {
                return;
            }
            gameState = value;
            OnStateChange();
        }
    }

    private void OnStateChange()
    {
        switch (GameState)
        {
            case GameState.START:
                uIManager.GameStartStatus();
                LevelSpawner();
                break;
            case GameState.PLAY:
                playerController.MoveSpeed = 1;
                break;
            case GameState.CALCULATE:
                CalculateStatus();
                break;
            case GameState.WIN:
                uIManager.WinStatus();
                break;
            case GameState.FAIL:
                uIManager.FailStatus();
                break;
            default:
                break;
        }
    }

    void Start()
    {
        GameManager.Instance.GameState = GameState.START;
        LevelSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextGameStatus()
    {
        PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
        playerController.gameObject.transform.position = playerStartPosition;
        LevelSpawner();
        uIManager.LevelTextUpdate();
        GameState = GameState.PLAY;
    }
    public void RestartGameStatus()
    {
        playerController.gameObject.transform.position = playerStartPosition;
        LevelSpawner();
        GameState = GameState.PLAY;
    }
    private void CalculateStatus()
    {
        StartCoroutine(CalculateStatusTimer());
    }
    //private void PlayStatus()
    //{
    //    playerController.MoveSpeed = 1;
    //}

    private IEnumerator CalculateStatusTimer()
    {
        playerController.MoveSpeed = 0;
        yield return new WaitForSeconds(2f);
        MyPlatformCalculateObject.CompleteControl();
        if (MyPlatformCalculateObject.ContinueControl)
        {
            var newPlatformObject = Instantiate(platformObject, MyPlatformCalculateObject.gameObject.transform.position, Quaternion.identity, levelPlatform.transform);
            MyPlatformCalculateObject.gameObject.SetActive(false);
            newPlatformObject.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                GameState = GameState.PLAY;

            });
        }
        else
        {
            GameState = GameState.FAIL;
        }

    }

    private void LevelSpawner()
    {
        if (currentLevel!=null)
        {
            Destroy(currentLevel.gameObject);
        }
        if (PlayerPrefs.GetInt("PlayerLevel")>maxLevel)
        {
            var rndLevel = Random.Range(1, maxLevel);
            currentLevel = Resources.Load("LevelData/Level" + rndLevel) as GameObject;
            currentLevel = Instantiate(currentLevel, Vector3.zero, Quaternion.identity);
            levelPlatform = currentLevel.transform.Find("PlatformLevel").gameObject;

        }
        else
        {
            currentLevel = Resources.Load("LevelData/Level" + PlayerPrefs.GetInt("PlayerLevel")) as GameObject;
            currentLevel = Instantiate(currentLevel, Vector3.zero, Quaternion.identity);
            levelPlatform = currentLevel.transform.Find("PlatformLevel").gameObject;
        }
        
        

    }

    

    


}
