using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public LayerMask clickableArea;

    private NavMeshAgent agent;

    [SerializeField]
    ParticleSystem moveParticle;

    [SerializeField]
    ParticleSystem attackParticle;

    [SerializeField]
    float attackRange = 2;

    RaycastHit hit;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckRange();
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 150, clickableArea))
            {
                Instantiate(moveParticle, hit.point, Quaternion.identity);

                agent.SetDestination(hit.point);

                animator.SetBool("Walking", true);
            }

        }

        // Accesses the speed to see if player is walking
        if (agent.velocity.magnitude <= 0f)
        {
            animator.SetBool("Walking", false);
        }


    }

    private void CheckRange()
    {
        if (agent.hasPath == true)
        {
            if (hit.transform.gameObject.CompareTag("Enemy") && agent.remainingDistance < attackRange)
            {
                GameObject enemy = hit.transform.gameObject as GameObject;
                if (enemy != null)
                {
                    agent.isStopped = true;

                    StartCoroutine (Punch());

                    Destroy(enemy.gameObject, 1f);
                }

            }
        }
    }

    private IEnumerator Punch()
    {
        animator.SetBool("Punching", true);
        
        yield return new WaitForSeconds(1);

        Instantiate(attackParticle, hit.point, Quaternion.identity);

        animator.SetBool("Punching", false);
        agent.isStopped = false;
    }
}
