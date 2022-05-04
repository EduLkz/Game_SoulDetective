using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlaColisao : MonoBehaviour
{
    public float targetTime;
    private float maxTime = 2.0f;

    public Image loadingButton;
    public AudioSource audio;
    public Transform target;
    
    [SerializeField]
    private bool isPressed;
    private bool isFinished;


    protected void Awake()
    {
        target = FindObjectOfType<Player>().transform;
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
       // Debug.Log("startou os role ai e tals");
       // contador = 0;
        targetTime = maxTime;
        isFinished = false;
        isPressed = false;

        loadingButton.transform.parent.gameObject.SetActive(false);

    }

    // Update is called once per frame
    protected void Update()
    {
        if(isFinished != true )
        {
            if(isPressed == true )
            {
                targetTime -= Time.deltaTime;

                Vector3 targetPosition = new Vector3(loadingButton.transform.parent.position.x, target.position.y, loadingButton.transform.parent.position.z);
                loadingButton.transform.parent.LookAt(targetPosition);

                //loadingButton.transform.parent.LookAt(target,Vector3.up);
                if (targetTime <= 0.0f)
                 {
                    timerEnded();
                    //buttonAction();
                 }
            }
        }
        else
        {

        }
        
    }

    protected void OnTriggerEnter(Collider other)
     {
        loadingButton.transform.parent.gameObject.SetActive(true);
        //TODO mudar o audio
        //audio.PlayOneShot(audio.clip);

    }

    // Applies an upwards force to all rigidbodies that enter the trigger.
    protected void OnTriggerStay(Collider other)
    {
        float updatedTime;

    	if(isFinished != true )
        {
            isPressed = true;

            updatedTime = calculaRemainingTime();
            loadingButton.fillAmount = updatedTime;
        }
    }

    protected void OnTriggerExit(Collider other)
     {
        //contador = 0;
        targetTime = maxTime;
        isPressed = false;
        loadingButton.fillAmount = 0;
        loadingButton.transform.parent.gameObject.SetActive(false);

    }

    protected void timerEnded() 
     {
         Debug.Log("Terminou o timer");
        buttonAction();
        isFinished = false;
     }


     
     protected virtual void buttonAction()
     {
        Debug.Log("AAAAA gatinhoooooo");
     }



     protected float calculaRemainingTime()
     {
        float restante = 0;
        restante = targetTime / maxTime;
        return restante;
     }


}
