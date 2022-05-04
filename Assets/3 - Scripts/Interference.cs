using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interference : MonoBehaviour
{
    private Light lamp;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {

        lamp = this.GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {

        if (Vector3.Distance(this.transform.position, player.transform.position) < 8 && Vector3.Distance(this.transform.position, player.transform.position) > 5.8f)
            lamp.intensity = Random.Range(1, 5);

        else if (Vector3.Distance(this.transform.position, player.transform.position) < 5.8f)
            lamp.intensity = 0;

        else
            lamp.intensity = 5;


    }

    
}
