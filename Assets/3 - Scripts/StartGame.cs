using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartGame : MonoBehaviour {

    public Image fade;
    public GameObject[] startObjects;

    private void Start() {
        fade.color = Color.black;
        fade.DOFade(0, 1f).SetDelay(.15f).Delay();
    }
    public void ReadytoStart() {
        foreach(GameObject item in startObjects) {
            item.SetActive(true);
        }
    }
}