using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

    float possesCD = 3.5f;
    public bool canBePossessed = true;
    float possesTimer;

    public Animator anim;

    private void Update() {
        if(!canBePossessed) {
            if(possesTimer > 0) {
                possesTimer -= Time.deltaTime;
            } else {
                canBePossessed = true;
            }
        }
    }

    public void OutTheBody() {
        possesTimer = possesCD;
        canBePossessed = false;
        anim.SetTrigger("posses");
        anim.SetBool("Walk", false);
    }

    public void InTheBody() {
        anim.SetTrigger("posses");
    }

    public void UpdateAnim(bool _walk) {
        anim.SetBool("Walk", _walk);
    }
}