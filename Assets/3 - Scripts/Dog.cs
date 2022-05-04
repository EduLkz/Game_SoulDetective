using UnityEngine;

public class Dog : Enemy {

    public Padre padre;
    protected override void Attack() {
        padre.ComeHere(player.transform.position);
    }
}