using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiPatrol : MonoBehaviour {

    private GameObject[] patrolPoints;
    private Vector3 nextPos;
    private NavMeshAgent navMeshAgent;
    private int a = 0;
    private float distanceToPoint;
    private bool corotineStarted = false;
    private Animator animator;
    private int pausetime = 5;

    // Use this for initialization
    void Start () {
        patrolPoints = GameObject.FindGameObjectsWithTag("Point");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GetNextPos();      
    }
	
	// Update is called once per frame
	void Update () {

        Patrol();
    }

    private void Patrol()
    {
        navMeshAgent.SetDestination(nextPos);
        distanceToPoint = Vector3.Distance(navMeshAgent.transform.position, nextPos);
       if (distanceToPoint < 1)
        {
            if(corotineStarted == false)               
            StartCoroutine(Pause());    
        }
      
    }

    private void GetNextPos()
    {   
      animator.SetBool("isWalking", true);
        nextPos = patrolPoints[a].transform.position;
        a++;
        if (a > (patrolPoints.Length - 1))       
            a = 0;         
    }

    IEnumerator Pause()
    {
        animator.SetBool("isWalking", false);
        corotineStarted = true;
        yield return new WaitForSeconds(pausetime);
        GetNextPos();
        corotineStarted = false;
    }
}
