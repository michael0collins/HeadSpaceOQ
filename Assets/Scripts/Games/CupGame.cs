using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CupGame : Game
{
    public GameObject ballPrefab;
    public AudioClip instructions1;
    public GameObject Cups3, Cups4, Cups5;
    public GameObject mainMenu;
    public GameObject table;
    public int maxAllowedWrongAnswers = 2;

    private Cup winningCup;
    private AudioSource source;
    private bool hasSelected;
    private Animation anim;
    private bool animFinished;
    private float ballSpawnXOffset = .125f;
    private Text timer;
    private bool gameIsOver = false;
    private int score;
    private int currentRound;
    private int currentWrong = 0;
    public int g = 0;
    private bool instructionsRead = false;

    private float animationSpeed = 5;

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Text>();
        source = GetComponent<AudioSource>();
        
        StartCoroutine(Game());

    }

    private IEnumerator Game()
    {
        score = 0;
        currentRound = 0;
        currentWrong = 0;

        yield return new WaitForSeconds(3);

        for (g = 0; g < gameManager.objectTrackingTestGameDatas.Count; g++)
        {
            currentRound = g;
            //print("round " + g + " # cups: " + gameManager.objectTrackingTestGameDatas[g].numberOfCups
             //+ " round speed: " + gameManager.objectTrackingTestGameDatas[g].objectMovementSpeed + " round " + gameManager.objectTrackingTestGameDatas[g].roundDuration);
            if (!gameIsOver)
            {
                if(instructionsRead == false && gameManager.instructions)
                {
                    instructionsRead = true;
                    source.clip = instructions1;
                    source.Play();
                    yield return new WaitForSeconds(source.clip.length);
                }
                score += gameManager.objectTrackingTestGameDatas[currentRound].numberOfCups;
                Cup[] cups;
                animFinished = false;
                hasSelected = false;

                switch (gameManager.objectTrackingTestGameDatas[g].numberOfCups)
                {
                    case 3:
                        Cups3.SetActive(true);
                        break;
                    case 4:
                        Cups4.SetActive(true);
                        break;
                    case 5:
                        Cups5.SetActive(true);
                        break;
                    default:
                        print("no cups found");
                        break;
                }

                //Get the anim of the currently active GO.
                anim = GetComponentInChildren<Animation>();

                anim[anim.clip.name].speed = gameManager.objectTrackingTestGameDatas[g].objectMovementSpeed;

                //Get all active cups.
                cups = FindObjectsOfType<Cup>();
 
                //Choose the winning cup.
                int goalCup = Random.Range(0, cups.Length);
                cups[goalCup].isGoalObject = true;

                //Set as goal object.
                winningCup = cups[goalCup];

                //Wait for animation to play.
                while (animFinished == false)
                    yield return null;

                //Set tag to interactable
                foreach (Cup c in cups)
                    c.tag = "Interactable";

                //Wait for selection.
                while (hasSelected == false)
                    yield return null;

                //Reset cup object
                ResetCups();

                yield return new WaitForSeconds(5);

                if (g + 1 == gameManager.objectTrackingTestGameDatas.Count)
                {
                    timer.text = "That was the last round!";
                    StartCoroutine(EndGame(false));
                }    
                else 
                    timer.text = "";
            }
            yield return null;
        }
        
        if(FindObjectOfType<ProgressionBoard>() != null)
            FindObjectOfType<ProgressionBoard>().AddBoardElement("Cup Game: " + score);
    }

    public void CupSelected(Cup selectedCup)
    {
        if(selectedCup == winningCup)
        {
            timer.text = "Good job!";
            base.OnWin();
        } else
        {
            base.OnLoss();
            StartCoroutine(EndGame(true));
        }

        hasSelected = true;
    }

    private IEnumerator EndGame(bool loss)
    {
        currentWrong++;
        if (loss && currentWrong == maxAllowedWrongAnswers)
        {
            gameIsOver = true;
            ResetCups();
            timer.text = "Game Over!";
            yield return new WaitForSeconds(5.0f);
            timer.text = "";
            table.SetActive(false);
            mainMenu.SetActive(true);
            g--;
        } else {
            ResetCups();
            timer.text = "Oops, wrong one!";
            g--;
        } 
    }

    public void SetAnimFinished()
    {
        animFinished = true;
    }

    public void SpawnBall()
    {
        winningCup.SpawnBall(ballPrefab, ballSpawnXOffset);
    }

    public void Deleteball()
    {
        winningCup.DeleteBall();
    }

    public void SetAnimSpeed()
    {
        anim[anim.clip.name].speed = animationSpeed;
    }

    private void ResetCups()
    {
        Deleteball();
        foreach (Cup c in FindObjectsOfType<Cup>())
        {
            c.tag = "Untagged";
            c.isGoalObject = false;
        }

        Cups3.SetActive(false);
        Cups4.SetActive(false);
        Cups5.SetActive(false);
    }
}
