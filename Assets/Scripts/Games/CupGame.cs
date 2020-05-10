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

    private Cup winningCup;
    private GameManager gameManager;
    private AudioSource source;
    private bool hasSelected;
    private Animation anim;
    private bool animFinished;
    private float ballSpawnXOffset = .125f;
    private Text timer;
    private bool gameIsOver = false;

    private float animationSpeed = 5;

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Text>();
        source = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        
        StartCoroutine(Game());
    }

    private IEnumerator Game()
    {
        yield return new WaitForSeconds(3);

        //Remove for gameManager;
        int numOfGames = 3;
        //replace for gamemanager
        int numOfCups = 3;

        for (int g = 0; g < numOfGames; g++)
        {
            if (!gameIsOver)
            {
                if(g == 0)
                {
                    source.clip = instructions1;
                    source.Play();
                    yield return new WaitForSeconds(source.clip.length);
                }

                Cup[] cups;
                animFinished = false;
                hasSelected = false;

                switch (numOfCups)
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
                }

                //Get the anim of the currently active GO.
                anim = GetComponentInChildren<Animation>();

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

                if (g + 1 == numOfGames)
                {
                    timer.text = "That was the last round!";
                    StartCoroutine(EndGame(false));
                }    
                else 
                    timer.text = "";

                numOfCups++;
            }
            yield return null;
        }
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
        if(FindObjectOfType<ProgressionBoard>() != null)
            FindObjectOfType<ProgressionBoard>().AddBoardElement("Cup Game 3/3");

        if (loss)
        {
            gameIsOver = true;
            ResetCups();
            timer.text = "Oops, wrong one!";
        }
        yield return new WaitForSeconds(5.0f);
        //gameManager.gamesComplete++;
        //5gameManager.CheckGamesComplete();
        FindObjectOfType<EndScreenDelete>().AddGame();
        timer.text = "";
        table.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SetAnimFinished()
    {
        animFinished = true;
        print("anim finished");
    }

    public void SpawnBall()
    {
        print("Spawn ball");
        winningCup.SpawnBall(ballPrefab, ballSpawnXOffset);
    }

    public void Deleteball()
    {
        print("Deleted ball");
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


//TODO
//Get the round info from GameManager
//Add win and loss feedback
//Make player win when rounds reaches max
