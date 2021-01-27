using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Timer variables
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    //trash variables

    //Game Flow
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    private bool isGameWon = false;
    private bool isGameOver = false;
    public bool isWinnable = false;
    public int playerScore;
    public int playerMaxScore = 3;
    public int playerLives = 3;
    //public Text playerScoreText;
    public Text LivesText;
    public List<Text> playerScoreTextList = new List<Text>();

    //objects
    //public List<GameObject> trashParentList = new List<GameObject>();
    public GameObject trashParent;
    public GameObject trash;
    public List<GameObject> BinParentList = new List<GameObject>();
    //public GameObject binParent;
    public GameObject bin;

    //trash/Bin SOs/data
    public List<UnityEngine.Object> TrashSOs { get; set; } = new List<UnityEngine.Object>();
    private TrashSO trashData;
    public List<UnityEngine.Object> BinSOs { get; set; } = new List<UnityEngine.Object>();
    private BinSO binData;

    public List<GameObject> trashList = new List<GameObject>();

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;

        DisplayLives(playerLives);

        PopulateTrashSOsList();
        PopulateBinSOsList();
        //PopulateTrashList();
        InstantiateNewTrash();
        InstantiateNewBin();

        GameEvents.current.OnGameOver += GameOver;
    }

    void Update()
    {
        if (timerIsRunning && !isGameWon)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                GameEvents.current.GameOver(true);
            }
        }
    }

    public void GameOver(bool gameOver)
    {
        if (gameOver)
        {
            timeRemaining = 0;
            timerIsRunning = false;

            isGameOver = gameOver;

            gameOverScreen.SetActive(true);
            Debug.Log("game over");
        }
    }

    public void RemoveLife()
    {
        playerLives--;
        DisplayLives(playerLives);
        if (playerLives <= 0)
        {
            GameEvents.current.GameOver(true);
        }
    }

    public void IncreaseScore()
    {
        playerScore++;
        DisplayPlayerScore(playerScore);

        //for prototype only
        if (playerScore == playerMaxScore && isWinnable)
        {
            isGameWon = true;
            gameWinScreen.SetActive(true);
        }
    }

    void DisplayPlayerScore(int score)
    {
        foreach (var scoreText in playerScoreTextList)
        {
            scoreText.text = score.ToString();
        }
    }

    public void DisplayLives(int lives) 
    {
        LivesText.text = lives.ToString();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void PopulateTrashList()
    {
        foreach (Transform child in trashParent.transform)
        {
            trashList.Add(child.gameObject);
        }
    }

    private List<UnityEngine.Object> PopulateTrashSOsList()
    {
        //gets a list of SO objects from the Resources
        UnityEngine.Object[] SOArray = Resources.LoadAll("TrashSOs", typeof(ScriptableObject));
        for (int i = 0; i < SOArray.Length; i++)
        {
            TrashSOs.Add(SOArray[i]);

        }
        return TrashSOs;
    }

    public TrashSO SendRandomTrashData()
    {
        //TODO: Remove count logic
        if (TrashSOs.Count > 0)
        {
            //chooses a random question from the SO pool
            int index = UnityEngine.Random.Range(0, TrashSOs.Count);
            ScriptableObject so = (ScriptableObject)TrashSOs[index];

            //assigns the randomly chosen question
            trashData = (TrashSO)so;

            return trashData;
        }
        else
        {
            return null;
        }
    }

    private List<UnityEngine.Object> PopulateBinSOsList()
    {
        //gets a list of SO objects from the Resources
        UnityEngine.Object[] SOArray = Resources.LoadAll("TrashBinSOs", typeof(ScriptableObject));
        for (int i = 0; i < SOArray.Length; i++)
        {
            BinSOs.Add(SOArray[i]);
        }
        return BinSOs;
    }

    public BinSO SendRandomBinData()
    {
        //TODO: Remove count logic
        if (BinSOs.Count > 0)
        {
            //chooses a random question from the SO pool
            int index = UnityEngine.Random.Range(0, BinSOs.Count);
            ScriptableObject so = (ScriptableObject)BinSOs[index];

            //assigns the randomly chosen question
            binData = (BinSO)so;

            return binData;
        }
        else
        {
            return null;
        }
    }

    public void InstantiateNewTrash()
    {
        if (!isGameOver)
        {
            Instantiate(trash, trashParent.transform.position, Quaternion.identity, trashParent.transform);
        }
    }

    public void InstantiateNewBin()
    {
        foreach (var binParent in BinParentList)
        {
            var currentBin = Instantiate(bin, binParent.transform.position, Quaternion.identity, binParent.transform);
            BinSOs.Remove(currentBin.GetComponent<ItemSlot>().binSO);
        }
    }

    public void DestroyAllBinsAndGeneratteNewOnes()
    {
        foreach (var binParent in BinParentList)
        {
            Destroy(binParent.GetComponentInChildren<ItemSlot>().gameObject);
        }

        if (!isGameOver)
        {
            PopulateBinSOsList();
            InstantiateNewBin();
        }
    }


    public void ChangeTimer(float time, bool isCorrect)
    {
        if (isCorrect)
        {
            timeRemaining += time;
        }
        else
        {
            timeRemaining -= time;
        }
    }
}
