using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot5 : MonoBehaviour
{
    private int passes = 1;
    private enum BotStates
    {
        Roam,
        Alert
    };

    public float roamTimer;
    public float roamRadius;
    public float searchTime;
    public float searchRadius;
    public Maze mazeGen;

    private BotTargetPosHolderScript botPosHolder;
    private float timer;
    private BotStates currState;
    private NavMeshAgent agent;
    private GameObject target;
    private Vector3 lastKnownPos;
    [SerializeField]
    private Vector3 currTargetPos;


    // Start is called before the first frame update
    void Start()
    {
        timer = roamTimer;
        currState = BotStates.Roam;
        agent = this.GetComponent<NavMeshAgent>();
        mazeGen = GameObject.FindObjectOfType<Maze>();
        botPosHolder = GetComponent<BotTargetPosHolderScript>();
        Vector3 newPos = mazeGen.GetRandomPosOnMaze();
        agent.SetDestination(newPos);
        currTargetPos = newPos;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (currState)
        {
            case BotStates.Roam:
                {
                    if (passes > 0)
                    {
                        if (Vector3.Distance(transform.position, agent.destination) <= 0.1)
                        {
                            Vector3 newPos = mazeGen.GetRandomPosOnMaze();
                            agent.SetDestination(newPos);
                            currTargetPos = newPos; 
                            passes--;
                        }
                        RaycastHit hitF, hitB, hitL, hitR;

                        if (Physics.Raycast(transform.position, transform.forward, out hitF, 5f))
                        {
                            if (hitF.transform.gameObject.GetComponent<PlayerMovement>())
                            {
                                target = hitF.transform.gameObject;
                                agent.SetDestination(target.transform.position);
                                transform.LookAt(target.transform);
                                Debug.Log("LOoking at player 1");
                                currState = BotStates.Alert;
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out hitB, 5f))
                        {
                            if (hitB.transform.gameObject.GetComponent<PlayerMovement>())
                            {
                                target = hitB.transform.gameObject;
                                agent.SetDestination(target.transform.position);
                                transform.LookAt(target.transform);
                                Debug.Log("LOoking at player 2");
                                currState = BotStates.Alert;
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out hitR, 5f))
                        {
                            if (hitR.transform.gameObject.GetComponent<PlayerMovement>())
                            {
                                target = hitR.transform.gameObject;
                                agent.SetDestination(target.transform.position);
                                transform.LookAt(target.transform);
                                Debug.Log("LOoking at player 3");
                                currState = BotStates.Alert;
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out hitL, 5f))
                        {
                            if (hitL.transform.gameObject.GetComponent<PlayerMovement>())
                            {
                                target = hitL.transform.gameObject;
                                agent.SetDestination(target.transform.position);
                                transform.LookAt(target.transform);
                                Debug.Log("LOoking at player 4");
                                currState = BotStates.Alert;
                            }
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, agent.destination) <= 0.1)
                        {
                            Vector3 newPos = GameObject.FindObjectOfType<PlayerMovement>().transform.position;
                            agent.SetDestination(newPos);
                            currTargetPos = newPos;
                            passes = 1;
                        }
                        RaycastHit hitF, hitB, hitL, hitR;

                        if (Physics.Raycast(transform.position, transform.forward, out hitF, 5f))
                        {
                            if (hitF.transform.gameObject.GetComponent<PlayerMovement>())
                            {
                                target = hitF.transform.gameObject;
                                agent.SetDestination(target.transform.position);
                                transform.LookAt(target.transform);
                                Debug.Log("LOoking at player 1");
                                currState = BotStates.Alert;
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.forward, out hitB, 5f))
                        {
                            if (hitB.transform.gameObject.GetComponent<PlayerMovement>())
                            {
                                target = hitB.transform.gameObject;
                                agent.SetDestination(target.transform.position);
                                transform.LookAt(target.transform);
                                Debug.Log("LOoking at player 2");
                                currState = BotStates.Alert;
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.right, out hitR, 5f))
                        {
                            if (hitR.transform.gameObject.GetComponent<PlayerMovement>())
                            {
                                target = hitR.transform.gameObject;
                                agent.SetDestination(target.transform.position);
                                transform.LookAt(target.transform);
                                Debug.Log("LOoking at player 3");
                                currState = BotStates.Alert;
                            }
                        }
                        if (Physics.Raycast(transform.position, -transform.right, out hitL, 5f))
                        {
                            if (hitL.transform.gameObject.GetComponent<PlayerMovement>())
                            {
                                target = hitL.transform.gameObject;
                                agent.SetDestination(target.transform.position);
                                transform.LookAt(target.transform);
                                Debug.Log("LOoking at player 4");
                                currState = BotStates.Alert;
                            }
                        }
                    }


                    break;
                }
            case BotStates.Alert:
                {

                    //transform.LookAt(target.transform);

                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
                    {
                        if (hit.transform.gameObject.GetComponent<PlayerMovement>())
                        {
                            transform.LookAt(target.transform);
                            agent.SetDestination(target.transform.position);
                            lastKnownPos = target.transform.position;
                        }
                        else
                        {
                            passes = 0;
                            currState = BotStates.Roam;
                        }
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, transform.forward * 10f, Color.red);
                    }
                    break;
                }




        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerEntry>())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}

