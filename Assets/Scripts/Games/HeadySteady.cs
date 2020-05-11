﻿using System.Collections;
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

    private Text timer;
    private AudioSource source;
    public Animator anim;
    private GameObject headObject;

    [Header("Game Settings")]
    public float dataCollectionInterval = .1f;

    private void Start()
    {
        headObject = FindObjectOfType<ObjectDetector>().gameObject;
        timer = GameObject.Find("Timer").GetComponent<Text>();
        //anim = GameObject.Find("Environment").GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        if (anim == null)
            timer.text = "Anim is null";

        StartCoroutine(Game());
    }

    private IEnumerator Game()
    {
        //Start the instructions.
        if(gameManager.instructions)
        {
            source.clip = baselineInstructions;
            source.Play();
        }

        //Wait until instructions are done.
        yield return new WaitForSeconds(source.clip.length + 3.0f);

        timer.enabled = true;

        //Recording
        float loggedTime = Time.time;
        float averageBaselineMovement;
        List<float> baselineMovementVariation = new List<float>();
        Vector3 startLocation = headObject.transform.position;

        while (Time.time - loggedTime < animClip.length)
        {
            timer.text = Convert.ToInt16(Time.time - loggedTime) + " / " + Convert.ToInt16(25);
            baselineMovementVariation.Add(Vector3.Distance(startLocation, headObject.transform.position));
            yield return new WaitForSeconds(dataCollectionInterval);
        }

        timer.enabled = false;

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

        timer.text = "Anim";

        anim.SetTrigger("StartSimulation");

        float averageTestedMovement;
        startLocation = headObject.transform.position;
        loggedTime = Time.time;
        timer.enabled = true;

        List<float> testedMovementVariation = new List<float>();

        while (Time.time - loggedTime < 25)
        {
            timer.text = Convert.ToInt16(Time.time - loggedTime) + " / " + Convert.ToInt16(25);
            testedMovementVariation.Add(Vector3.Distance(startLocation, headObject.transform.position));
            yield return new WaitForSeconds(dataCollectionInterval);
        }

        anim.ResetTrigger("StartSimulation");

        timer.enabled = false;

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
            FindObjectOfType<ProgressionBoard>().AddBoardElement("HeadySteady");
    }
}
