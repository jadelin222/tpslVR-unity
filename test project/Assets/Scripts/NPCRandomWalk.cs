using UnityEngine;
using UnityEngine.AI; 

public class NPCRandomWalk : MonoBehaviour
{
    public Transform[] walkPoints; 
    public float waitTime = 60.0f;
    public bool wasWalkingBeforeInterrupted = false;

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 
        timer = waitTime; // initialize timer
    }

    void Update()
    {
        // update the timer
        timer += Time.deltaTime;

        // check if the character has reached its destination and the timer exceeds the waitTime
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && timer >= waitTime)
        {
            //choose a random walk point
            int randomIndex = Random.Range(0, walkPoints.Length);
            agent.SetDestination(walkPoints[randomIndex].position);
            timer = 0f; //reset timer
        }

        //determine if the character is moving
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isMoving); //control the walking animation
    }


    public void StopWalking()
    {
        wasWalkingBeforeInterrupted = agent.velocity.magnitude > 0.1f;
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
    }
    
    public void StartWalking()
    {
        if (wasWalkingBeforeInterrupted)
        {
            agent.isStopped = false;
            timer = waitTime; //reset the timer
            wasWalkingBeforeInterrupted = false; //reset flag
        }
        else
        {
            timer = waitTime;
        }




    }
}
