using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIAScript : MonoBehaviour
{
    public enum States 
    {
        Patrol,
        Guard,
        Chase
    }
    public States currentState;

    [Header("States - Guard")]
    float waitedTime;
    public float cooldown;

    [Header("States - Patrol")]
    public Transform wayPointA;
    public Transform wayPointB;

    [Header("Others")]
    NavMeshAgent agent;
    public Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Guard();
    }

    private void Update()
    {
        ProcessState();
        agent.SetDestination(target.position);
    }

    private void ProcessState()
    {
        switch (currentState)
        {
            case States.Patrol:
                Patrol();
                break; 
            case States.Guard:
                Guard();
                break;
            case States.Chase:
                break;
        }
    }

    private void Patrol()
    {
        if (closeToObjective())
        {
            ChangeWayPoints();
        }
    }

    private void Guard()
    {
        if (waitedEnough())
        {
            StartPatroll();
        }
        else
        {
            target = transform;
        }
    }

    private void StartPatroll()
    {
        currentState = States.Patrol;
    }
    

    private void ChangeWayPoints()
    {
        if(target == wayPointA)
        {
            target = wayPointB;
        }
        else
        {
            target = wayPointA;
        }
    }

    private bool closeToObjective()
    {
        return Vector3.Distance(transform.position, target.position) <= 0.2f;
    }

    private bool waitedEnough()
    {
        return waitedTime + cooldown <= Time.time;
    }

    private void StartGuard()
    {
        currentState = States.Guard;
        waitedTime = Time.time;
    }
}
