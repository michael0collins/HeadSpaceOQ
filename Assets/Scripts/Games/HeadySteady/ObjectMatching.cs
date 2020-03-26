using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ObjectMatching : MonoBehaviour
{
    //round duration--REMOVE

    //show duration
    public float showDuration = 2.5f;
    //number of goal objects
    public int totalGoalObjects = 4;

    public AudioClip instructions1;
    public List<MatchingObject> matchingObjectsPool = new List<MatchingObject>();
    public Transform objectShowPosition;

    private int gridWidth = 4;
    private float objectGridSpacing = 1.5f;

    private Text timer;
    private AudioSource source;
    private GameManager gameManager;

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Text>();
        source = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(Game());
    }

    private IEnumerator Game()
    {
        //int numOfGames = gameManager.objectTrackingTestGameDatas.Count;
        int numOfGames = 1;

        yield return new WaitForSeconds(3.0f);

        for (int i = 0; i < numOfGames; i++)
        {
            /*
            if (i == 0)
            {
                source.clip = instructions1;
                source.Play();
                yield return new WaitForSeconds(instructions1.length);
            }
            */
        
        }
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
            int seedIndex = UnityEngine.Random.Range(0, numbers.Count);
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
    }
}

/*
 *     private IEnumerator Game()
    {
        //int numOfGames = gameManager.objectTrackingTestGameDatas.Count;
        int numOfGames = 3;

        yield return new WaitForSeconds(3.0f);

        for (int i = 0; i < numOfGames; i++)
        {
            
            if (i == 0)
            {
                source.clip = instructions1;
                source.Play();
                yield return new WaitForSeconds(instructions1.length);
            }
            

//Create game specific list
List<MatchingObject> goalObjects = new List<MatchingObject>(matchingObjectsPool);

            //Replace the 3 with the GameManager data.
            foreach(int seed in RandomSeedGenerator(3, matchingObjectsPool))
            {
                MatchingObject objectClone = Instantiate(matchingObjectsPool[seed]) as MatchingObject;
goalObjects.Add(objectClone);
            }
            
            int spawnIndex = 0;
            for (int j = 0; j<gridWidth; j++)
            {
                for (int k = 0; k<gridWidth; k++)
                {
                    Vector3 gridPos = new Vector3(transform.position.x + i * objectGridSpacing, transform.position.y + j * objectGridSpacing, transform.position.z);
MatchingObject matchingObjectClone = Instantiate(goalObjects[spawnIndex], gridPos, transform.rotation, transform) as MatchingObject;
spawnIndex++;
                }
            }
            
        }
    }
    */
    