using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Padre : MonoBehaviour {

    [SerializeField]
    protected float chaseRange = 5;
    [SerializeField]
    protected float attackRange = 2;

    [SerializeField]
    protected float moveSpeed = 2.5f;
    [SerializeField]
    protected float chaseSpeed = 5;
    
    protected Animator anim;
    protected Player player;
    float distance;
    NavMeshAgent agent;

    protected void Start() {
        player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= chaseRange) {
            agent.speed = chaseSpeed;
            if(distance < attackRange && !player.InHuman()) {
                StartCoroutine(Attack());
            }
        } else {
            agent.speed = moveSpeed;
        }

            anim.SetBool("Walk", agent.hasPath);
    }

    public void ComeHere(Vector3 _pos) {
        agent.SetDestination(_pos);

    }

    IEnumerator Attack() {
        yield return new WaitForSeconds(.1f);
        if(distance <= attackRange) { 
            //Kill
        }
    }

}
