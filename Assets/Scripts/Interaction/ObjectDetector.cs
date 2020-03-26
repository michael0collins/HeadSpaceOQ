using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDetector : MonoBehaviour
{
    public AudioClip interactSuccessSound;
    private AudioSource audioSource;

    public Image selectionCrosshair;
    public Image defaultCrosshair;

    //put highlight around object. 
    public float requiredDetectionTime = .5f;
    public GameObject focusedObject;

    private float startTime;
    private bool tracking;
    private float focusTimeCounter = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startTime = Time.time;
    }

    private void Update()
    {
        CastRay();

        Debug.DrawRay(transform.position, transform.forward * 100, Color.red, 1);

        if (focusedObject != null && focusedObject.tag == "Interactable")
        {
            defaultCrosshair.enabled = true;
        }
    }

    private void CastRay()
    {
        RaycastHit hit;

        focusedObject = null;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            focusedObject = hit.collider.gameObject;

            if (hit.collider.tag == "Interactable" && !tracking && hit.collider.GetComponent<IInteractable>() != null)
            {
                StartCoroutine(FocusTracker(hit.collider.gameObject));
                return;
            }
        }
    }

    private IEnumerator FocusTracker(GameObject objectToTrack)
    {
        tracking = true;
        while (focusedObject == objectToTrack)
        {
            defaultCrosshair.enabled = false;
            selectionCrosshair.enabled = true;

            focusTimeCounter += Time.deltaTime;

            if (focusTimeCounter < requiredDetectionTime)
            {
                selectionCrosshair.fillAmount = focusTimeCounter / requiredDetectionTime;

                if (selectionCrosshair.fillAmount > .9f && objectToTrack != null)
                {
                    foreach (IInteractable interactable in objectToTrack.GetComponentsInChildren<IInteractable>())
                    {
                        interactable.OnInteracted();
                    }

                    audioSource.clip = interactSuccessSound;
                    audioSource.Play();
                }
            }
            yield return null;
        }
        selectionCrosshair.fillAmount = 0;
        selectionCrosshair.enabled = false;
        defaultCrosshair.enabled = true;
        focusTimeCounter = 0;
        tracking = false;
    }
}