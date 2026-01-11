using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    //It's considered as a collection: children.
    [SerializeField] private Transform patrolPath;
    [SerializeField] private float speed;
    
    private List<Vector3> patrolPoints = new List<Vector3>();

    private int currentIndex = 0;
    private void Awake()
    {
        foreach (Transform child in patrolPath)
        {
            //making the list of points that I have to patrol
            patrolPoints.Add(child.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //My position is equal to the calculation of the MoveTowards Method
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentIndex], speed * Time.deltaTime);

        if (transform.position == patrolPoints[currentIndex])
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        //currentIndex + 1 mod(Length)
        currentIndex = (currentIndex + 1) % patrolPoints.Count;
        transform.eulerAngles = transform.position.x > patrolPoints[currentIndex].x ? new Vector3(0, 180, 0) : Vector3.zero;

        //If the current destination is at the left side of your character
        /*if (transform.position.x > patrolPoints[currentIndex].x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }*/
    }
}
