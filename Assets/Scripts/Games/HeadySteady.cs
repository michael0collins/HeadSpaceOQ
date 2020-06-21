using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeadySteady : Game
{
    public GameObject resultsBoard;
    public AnimationClip animClip;
    public AudioClip baselineInstructions;
    public AudioClip testedInstructions;
    public AudioClip resultsInstructions;
    public GameObject timerObject;

    private Text timer;
    private AudioSource source;
    public Animator anim;
    private GameObject headObject;
    private float totalTestedMovement = 0;

    [Header("Game Settings")]
    public float dataCollectionInterval = .1f;

    private void Start()
    {
        headObject = FindObjectOfType<ObjectDetector>().gameObject;
        timer = GameObject.Find("Timer").GetComponent<Text>();
        source = GetComponent<AudioSource>();
        if (anim == null)
            timer.text = "Anim is null";

        StartCoroutine(Game());
    }

    private IEnumerator Game()
    {
        if(gameManager.instructions)
        {
            source.clip = baselineInstructions;
            source.Play();
        }

        yield return new WaitForSeconds(source.clip.length + 3.0f);

        float loggedTime = Time.time;
        float averageBaselineMovement;
        List<float> baselineMovementVariation = new List<float>();
        Vector3 startLocation = headObject.transform.position;

        timerObject.SetActive(true);
        UIManager.Instance.TogglePlayerHUD(false);

        while (Time.time - loggedTime < animClip.length)
        {
            timerObject.GetComponentInChildren<Text>().text = Convert.ToInt16(Time.time - loggedTime) + " / " + Convert.ToInt16(30);
            baselineMovementVariation.Add(Vector3.Distance(startLocation, headObject.transform.position));
            yield return new WaitForSeconds(dataCollectionInterval);
        }
        UIManager.Instance.TogglePlayerHUD(true);


        timerObject.SetActive(false);

        float average = 0;

        float[] distances = baselineMovementVariation.ToArray();

        for (int i = 0; i < distances.Length; i++)
            average += distances[i];
        
        averageBaselineMovement = average / baselineMovementVariation.Count;

        if(gameManager.instructions)
        {
            source.clip = testedInstructions;
            source.Play();
        }

        yield return new WaitForSeconds(source.clip.length + 3.0f);

        anim.SetTrigger("StartSimulation");

        float averageTestedMovement;
        startLocation = headObject.transform.position;
        loggedTime = Time.time;

        timerObject.SetActive(true);

        List<float> testedMovementVariation = new List<float>();
        UIManager.Instance.TogglePlayerHUD(false);

        while (Time.time - loggedTime < 30)
        {
            timerObject.GetComponentInChildren<Text>().text = Convert.ToInt16(Time.time - loggedTime) + " / " + Convert.ToInt16(30);
            float movementTotal = Vector3.Distance(startLocation, headObject.transform.position);
            testedMovementVariation.Add(movementTotal);
            totalTestedMovement += movementTotal;
            yield return new WaitForSeconds(dataCollectionInterval);
        }
        UIManager.Instance.TogglePlayerHUD(true);

        anim.ResetTrigger("StartSimulation");

        timerObject.SetActive(false);

        average = 0;
        distances = testedMovementVariation.ToArray();
        for (int i = 0; i < distances.Length; i++)
        {
            average += distances[i];
        }

        averageTestedMovement = average / testedMovementVariation.Count;
        timer.enabled = false;

        yield return new WaitForSeconds(3.0f);

        if(gameManager.instructions)
        {
            source.clip = resultsInstructions;
            source.Play();
        }

        resultsBoard.SetActive(true);

        FindObjectOfType<LineGraphManager>().CreateHeadySteadyGraph(baselineMovementVariation, testedMovementVariation);

        if(FindObjectOfType<ProgressionBoard>() != null)
            FindObjectOfType<ProgressionBoard>().AddBoardElement("HeadySteady: " + System.Math.Round(totalTestedMovement, 2));
    }   
}
