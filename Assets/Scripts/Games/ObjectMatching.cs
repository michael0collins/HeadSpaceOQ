using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System.Linq;
using UnityEngine.UI;

public class ObjectMatching : Game
{
    //round duration--REMOVE

    //show duration
    public float showDuration = 2.5f;
    //number of goal objects
    public int totalGoalObjects = 4;
    public int totalCorrect = 0;
    public GameObject returnToMenu;
    public bool incorrectGuess = false;

    public AudioClip instructions1;
    public List<MatchingObject> matchingObjectsPool = new List<MatchingObject>();
    public Transform objectShowPosition;
    public Transform gridOriginPosition;

    private float objectGridSpacing = 1;
    private Text timer;
    private AudioSource source;

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Text>();
        source = GetComponent<AudioSource>();
        StartCoroutine(Game());
    }

    private IEnumerator Game()
    {
        yield return new WaitForSeconds(3.0f);

        for (int g = 0; g < gameManager.memoryTestGameDatas.Count; g++)
        {
            if (g == 0 && gameManager.instructions)
            {
                source.clip = instructions1;
                source.Play();
                yield return new WaitForSeconds(instructions1.length);
            }

            //Get 12 random numbers for objects.
            int[] seed = RandomSeedGenerator(12, matchingObjectsPool);

            //List of in game objects that are being tracked.
            List<MatchingObject> inGameObjects = new List<MatchingObject>();

            //Instantiate the objects and add them to a list of in game objects.
            for(int i = 0; i < 12; i++)
            {
                MatchingObject objectClone = Instantiate(matchingObjectsPool[seed[i]], gridOriginPosition) as MatchingObject;
                objectClone.transform.position = Vector3.one * -5;
                inGameObjects.Add(objectClone);
                objectClone.isGoalObject = false;
            }

            //Get seed for goal object indices.
            seed = RandomSeedGenerator(totalGoalObjects, inGameObjects);

            //Assign goal objects.
            for(int i = 0; i < totalGoalObjects; i++)
            {
                inGameObjects[seed[i]].isGoalObject = true;
            }

            foreach(MatchingObject matchingObject in inGameObjects)
            {
                if(matchingObject.isGoalObject)
                {
                    Vector3 pos = matchingObject.transform.position;
                    matchingObject.transform.position = objectShowPosition.position;
                    yield return new WaitForSeconds(showDuration);
                    matchingObject.transform.position = pos;
                }
            }

            //Place this objects on a grid.
            int spawnIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector3 gridPos = new Vector3(gridOriginPosition.position.x + i * objectGridSpacing, gridOriginPosition.position.y + j * objectGridSpacing, 0);
                    inGameObjects[spawnIndex].tag = "Interactable";
                    inGameObjects[spawnIndex].transform.localPosition = gridPos;
                    spawnIndex++;
                }
            }

            totalCorrect = 0;
            incorrectGuess = false;

            while (totalCorrect < totalGoalObjects && incorrectGuess == false)
            {
                yield return null;
            }

            //Delete all matching objects.
            inGameObjects.Clear();
            foreach(MatchingObject matchingObject in FindObjectsOfType<MatchingObject>())
            {
                Destroy(matchingObject.gameObject);
            }

            //Deal with win/loss.
            if (totalCorrect == totalGoalObjects)
            {
                base.OnWin();
                if(g == gameManager.memoryTestGameDatas.Count)
                    timer.text = "Good job, there are no more rounds.";
                else
                    timer.text = "Good job, next round.";
            }
            else

            if (incorrectGuess)
            {
                base.OnLoss();
                timer.text = "Oops, that was wrong";
            }

            yield return new WaitForSeconds(5.0f);

            if(FindObjectOfType<ProgressionBoard>() != null)
                FindObjectOfType<ProgressionBoard>().AddBoardElement("Object Matching " + g.ToString());

            timer.text = "";
        }
        returnToMenu.SetActive(true);
    }

    private int[] RandomSeedGenerator(int length, List<MatchingObject> matchingObjectPool)
    {
        List<int> numbers = new List<int>();

        //Add all possible numbers to list.
        for (int i = 0; i < matchingObjectPool.Count; i++)
        {
            numbers.Add(i);
        }

        //Choose at random numbers
        int[] seed = new int[length];
        for (int i = 0; i < seed.Length; i++)
        {
            int seedIndex = Random.Range(0, numbers.Count);
            seed[i] = numbers[seedIndex];
            numbers.RemoveAt(seedIndex);
        }
        return seed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(3, 3, .5f));
        Gizmos.DrawCube(objectShowPosition.position, Vector3.one / 2);
        Gizmos.DrawCube(gridOriginPosition.position, Vector3.one / 2);
    }
}

    