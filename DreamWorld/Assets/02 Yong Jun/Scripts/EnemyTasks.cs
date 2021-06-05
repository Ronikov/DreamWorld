using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class EnemyTasks : MonoBehaviour
{
    public bool gotHit;
    public int hp;
    public NavMeshAgent meshAgent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Task]
    void isAlive()
    {
        if(hp > 0)
        {
            Task.current.Succeed();
        }
        else if(hp <= 0)
        {
            Task.current.Fail();
        }
    }

    [Task]
    void die()
    {
        Debug.Log("enemy is dead");
    }

    [Task]
    void isHit()
    {
        if(gotHit == true)
        {
            Task.current.Succeed();
            gotHit = false;
        }
        else if(gotHit == false)
        {
            Task.current.Fail();
        }
    }

    [Task]
    void hit()
    {
        if(hp > 0)
        {
            hp--;
        }
        Debug.Log("enemy got hit");
        Task.current.Succeed();
    }

    [Task]
    void moveClose(string tag)
    {
        float distance = 2f;
        Transform target = GameObject.FindGameObjectWithTag(tag).transform;

        if (Vector3.Distance(gameObject.transform.position, target.transform.position) < distance)
        {
            Debug.Log("enemy is near player");
            Task.current.Succeed();
        }
        else if (Vector3.Distance(gameObject.transform.position, target.transform.position) > distance)
        {
            Debug.Log("moving towards player");
            meshAgent.SetDestination(target.position);
            meshAgent.stoppingDistance = distance;
        }
    }

    [Task]
    void moveFar(string tag)
    {
        float distance = 7.5f;
        Transform target = GameObject.FindGameObjectWithTag(tag).transform;

        if (Vector3.Distance(gameObject.transform.position, target.transform.position) < distance)
        {
            Debug.Log("enemy is near player");
            Task.current.Succeed();
        }
        else if (Vector3.Distance(gameObject.transform.position, target.transform.position) > distance)
        {
            Debug.Log("moving towards player");
            meshAgent.SetDestination(target.position);
            meshAgent.stoppingDistance = distance;
        }
    }

    [Task]
    void attack()
    {
        #region idk
        //float distance = 2f;
        //Transform target = GameObject.FindGameObjectWithTag("Player").transform;

        //if (Vector3.Distance(gameObject.transform.position, target.transform.position) < distance)
        //{
        //    Debug.Log("enemy hits the player");
        //    Task.current.Succeed();
        //}
        //else
        //{
        //    Task.current.Fail();
        //}
        #endregion
        Debug.Log("enemy hits the player");
        Task.current.Succeed();
    }
}
