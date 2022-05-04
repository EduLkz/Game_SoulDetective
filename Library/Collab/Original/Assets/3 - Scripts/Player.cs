using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    public Transform cameraPivot;
    public float turnSpeed = 2;

    public float speed = 5;

    public float possessionDuration = 5;

    public ParticleSystem inHumanParticle;

    float startPosses;
    float hor;
    float ver;
    bool inHuman;
    bool canMove = true;


    Rigidbody body;
    Animator anim;

    Soul soulNear;
    Item itemNear;
    GameObject itemCarring;

    Human humanNear;
    Human humanControl;
    Vector3 dir;


    private void Start() {
        body = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        canMove = true;
    }

    private void Update() {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical") ;

        dir = (cameraPivot.forward * ver + cameraPivot.right * hor).normalized * speed;

        if(inHuman) {
            humanControl.UpdateAnim(hor != 0 || ver != 0);
            if(startPosses < Time.time) {
                ExitHuman();
            }
        }

        if(Input.GetKeyDown(KeyCode.F)) {
            if(inHuman) {
                ExitHuman();
                return;
            }
            if(humanNear != null && humanNear.canBePossessed) {
                humanControl = humanNear;
                canMove = false;
                StartCoroutine(TakeHuman());
                return;
            }

            if(soulNear != null) {
                if(itemCarring != soulNear.item) {
                    soulNear.Interact();
                } else {
                    soulNear.GiveItem();
                }
                return;
            }

            if(itemNear != null) {
                itemNear.Interact();
                itemCarring = itemNear.gameObject;
                return;
            }

        }

        if(Input.GetKey(KeyCode.E)) {
            cameraPivot.Rotate(-Vector3.up * turnSpeed);
        }

        if(Input.GetKey(KeyCode.Q)) {
            cameraPivot.Rotate(Vector3.up * turnSpeed);
        }

        anim.SetBool("Walking", hor != 0 || ver != 0);

        if(dir != Vector3.zero) {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 5 * Time.deltaTime);
        }
    }

    private void FixedUpdate() {
        body.velocity = (canMove)?dir:Vector3.zero;
    }

    private void OnTriggerEnter(Collider other) {
        if(inHuman) {
            return;
        }

        if(other.CompareTag("Soul") && soulNear == null) {
            soulNear = other.GetComponent<Soul>();
        }

        if(other.CompareTag("Item") && itemNear == null) {
            itemNear = other.GetComponent<Item>();
            anim.SetTrigger("GetItem");
        }

        if(other.CompareTag("Humano") && humanNear == null) {
            humanNear = other.GetComponent<Human>();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Soul") && soulNear != null) {
            if(other.gameObject == soulNear.gameObject) {
                soulNear = null;
            }
        }

        if(other.CompareTag("Item") && itemNear != null) {
            if(other.gameObject == itemNear.gameObject) {
                itemNear = null;
            }
        }

        if(other.CompareTag("Humano") && humanNear != null) {
            if(humanNear.gameObject == other.gameObject)
                humanNear = null;
        }
    }

    void ExitHuman() {
        humanControl.transform.SetParent(null);
        transform.DOMove(humanControl.transform.position + humanControl.transform.forward * 4f, .2f);
        anim.gameObject.SetActive(true);
        humanControl.OutTheBody();
        humanControl = null;
        humanNear = null;
        inHuman = false;
        inHumanParticle.Play();
    }

    IEnumerator TakeHuman() {
        humanControl.InTheBody();
        inHumanParticle.Play();
        anim.gameObject.SetActive(false);
        transform.DOMove(humanControl.transform.position, .25f);
        humanControl.transform.DORotate(transform.rotation.eulerAngles, .25f);
        yield return new WaitForSeconds(.3f);
        humanControl.transform.SetParent(transform);
        humanControl.transform.localPosition = Vector3.zero;
        startPosses = Time.time + possessionDuration;
        inHuman = true;
        canMove = true;
    }

    public void SetCanMove(bool _value) {
        canMove = _value;
    }

    public bool CanMove() {
        return canMove;
    }

    public bool InHuman() {
        return inHuman;
    }
}