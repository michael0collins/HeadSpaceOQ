using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IInteractable
{
    public bool isGoalObject = false;

    private GameObject ballClone;
    private CupGame cupGame;

    void Start()
    {
        cupGame = FindObjectOfType<CupGame>();
    }

    public void OnInteracted()
    {
        ReportSelected();
    }

    private void ReportSelected()
    {
        cupGame.CupSelected(this);
    }

    public void SpawnBall(GameObject ball, float xOffset)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 100))
        {
            if (hit.collider.tag == "Table")
            {
                ballClone = Instantiate(ball, new Vector3(hit.point.x + xOffset, hit.point.y,
                    hit.point.z), Quaternion.identity) as GameObject;
            }
        }
    }

    public void DeleteBall()
    {
        if (ballClone != null)
            Destroy(ballClone);
    }

    private void OnDrawGizmos()
    {
        if (isGoalObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position, Vector3.one / 3);
        }
    }
}
