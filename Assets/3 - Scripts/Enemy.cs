using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour {
   
    
    [SerializeField]
    protected float warnRange = 8;
    [SerializeField]
    protected float chaseRange = 5;
    [SerializeField]
    protected float attackRange = 2;

    [SerializeField]
    protected float moveSpeed = 2;
    [SerializeField]
    protected float chaseSpeed = 5;
    float currentSpeed;

    public ParticleSystem chaseParticle;

    protected float distance;
    protected bool warning;

    protected Rigidbody body;
    protected Animator anim;
    protected Player player;
    Quaternion dirRot;
    float rotateSpeed;
    [Space(5)]
    public bool killPlayer;
    ParticleSystem.MainModule p;

    float changeDirValue;
    float lastChange;
    Vector3 velocity;

    protected void Start() {
        player = FindObjectOfType<Player>();
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if(chaseParticle != null)
            p = chaseParticle.main;

        changeDirValue = Random.Range(3.2f, 7.5f);

        currentSpeed = moveSpeed;
        rotateSpeed = 2.5f;
    }

    protected void Update() {
        transform.rotation = Quaternion.Lerp(transform.rotation, dirRot, rotateSpeed * Time.deltaTime);
        distance = Vector3.Distance(transform.position, player.transform.position);
        body.velocity = velocity;

        if(chaseParticle != null) {
            if(warning && !chaseParticle.isEmitting) {
                p.loop = true;
                chaseParticle.Play();
            }

            if(!warning && chaseParticle.isEmitting) {
                p.loop = false;
            }
        }


        anim.SetBool("Walk", body.velocity != Vector3.zero);
        anim.SetBool("Warn", warning);

        if((warning && distance > chaseRange) || player.InHuman()) {
            if(warning)
                warning = false;
        }

        velocity = (transform.forward * moveSpeed);
        CheckDirections();

        if(Input.GetKeyDown("l"))
            ChangeDirection();


        if(lastChange < Time.time) {
            ChangeDirection();
            lastChange = Time.time + changeDirValue;
        }

        if(player.InHuman())
            return;
        
        if(distance < attackRange) {
            Attack();
        } else if(distance <= warnRange) {
            

            currentSpeed = moveSpeed;

            dirRot = Quaternion.LookRotation(player.transform.position - transform.position);


            if((distance <= chaseRange)) {
                currentSpeed = chaseSpeed;
                rotateSpeed = 2.5f;

                if(!warning) {
                    warning = true;
                }

            } else {
                rotateSpeed = 0.5f;
            }


            velocity = ((player.transform.position - transform.position).normalized * currentSpeed);
        }
    }

    protected virtual void Attack() {
        body.velocity = Vector3.zero;
        anim.SetTrigger("Attack");

        if(killPlayer)
            StartCoroutine(AttackCO());
    }

    IEnumerator AttackCO() {
        yield return new WaitForSeconds(.1f);
        if(distance <= attackRange) {
            SpawnItens.Instance.Lose();
        }
    }

    public bool isChasing() {
        return warning;
    }

    void CheckDirections() {
        Vector3 rotateDir = transform.forward;
        float range = 2;

        if(Physics.Raycast(transform.position, transform.forward, range * 1.5f)) {
            if(!Physics.Raycast(transform.position, transform.right, range)) {
                rotateDir = transform.right;
            } else if(!Physics.Raycast(transform.position, -transform.right, range)) {
                rotateDir = -transform.right;
            }
        } else {
            return;
        }

        dirRot = Quaternion.LookRotation(rotateDir);
    }

    void ChangeDirection() {
        float _rotx = Random.Range(-1, 2);
        float _rotz = Random.Range(-1, 2);

        Vector3 _newDir = new Vector3(_rotx, 0, _rotz).normalized;

        if(_newDir == Vector3.zero)
            return;

        dirRot = Quaternion.LookRotation(_newDir);
    }

    private void OnDrawGizmos() {
        return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, warnRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, attackRange);
    }

    
}