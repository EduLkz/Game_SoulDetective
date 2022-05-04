using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public Transform target;

    private void FixedUpdate() {
        transform.position = target.position;
    }
}