using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Dialog : MonoBehaviour {

    #region Singleton
    private static Dialog instance;
    public static Dialog Instance { get { return instance; } }

    private void Awake() {
        instance = this;
    }
    #endregion

    public RectTransform box;
    public TextMeshProUGUI textBox;
    public float textDuration = 2f;
    private float textStart;
    private float lastLetter;

    private bool isShowingText;
    private bool isAList;
    private bool boxOpen;

    public float textSpeed = 1f;
    private string textToShow;
    private string currentText = string.Empty;

    private List<string> listOfTexts = new List<string>();
    private DialogType listType;

    Player player;

    private void Start() {
        player = FindObjectOfType<Player>();
        currentText = string.Empty;
    }

    private void Update() {
        if(isShowingText) {
            textBox.text = currentText;
            if(currentText.Length < textToShow.Length) {
                if(lastLetter < Time.time) {
                    currentText += textToShow[currentText.Length];
                    lastLetter = Time.time + .25f / textSpeed;
                }

                if(Input.GetKeyDown(KeyCode.F)) {
                    currentText = textToShow;
                }
            } else {
                if(isAList) {
                    if(listOfTexts.Count > 1) {
                        if(textStart < 0)
                            textStart = Time.time;
                        else {
                            if(textStart + textDuration < Time.time || Input.GetKeyDown(KeyCode.F)) {
                                listOfTexts.RemoveAt(0);
                                ShowText(listOfTexts[0], listType);
                            }
                        }
                        
                    } else {
                        isAList = false;
                    }
                } else {
                    if(textStart < 0)
                        textStart = Time.time;
                    else{
                        if(textStart + textDuration < Time.time || Input.GetKeyDown(KeyCode.F)) {
                            isShowingText = false;
                            textBox.text = string.Empty;
                            textStart = -1;
                        }
                    }
                }

            }
        }else{
            if(boxOpen)
                CloseBox();
        }
    }

    public void ShowText(string _text, DialogType _dT){
        if(!isShowingText)
            OpenBox();
        else
            return;
        

        listType = _dT;

        switch(_dT) {
            case DialogType.Text:
            default:
                textBox.color = Color.black;
                break;
            case DialogType.Alert:
                textBox.color = Color.red;
                break;
            case DialogType.Notification:
                textBox.color = Color.green;
                break;
        }

        textStart = -1;
        textToShow = string.Empty;
        currentText = string.Empty;      

        textBox.text = string.Empty;
        textToShow = _text;
        isShowingText = true;

        if(!isAList) {
            listOfTexts.Clear();
        }
    }

    public void ShowText(List<string> _textList, DialogType _dT) {
        if(!isShowingText)
            OpenBox();

        switch(_dT) {
            case DialogType.Text:
            default:
                textBox.color = Color.black;
                break;
            case DialogType.Alert:
                textBox.color = Color.red;
                break;
            case DialogType.Notification:
                textBox.color = Color.green;
                break;
        }

        listOfTexts = _textList;
        textStart = -1;
        textToShow = string.Empty;
        currentText = string.Empty;

        textBox.text = string.Empty;
        textToShow = listOfTexts[0];
        isShowingText = true;

        isAList = true;
    }

    private void CloseBox() {
        box.DOSizeDelta(new Vector2(80, 250), .1f);
        box.DOSizeDelta(new Vector2(0, 0), .25f).SetDelay(.2f).Delay();
        boxOpen = false;
        player.SetCanMove(true);
    }

    private void OpenBox() {
        box.DOSizeDelta(new Vector2(80, 250), .1f);
        box.DOSizeDelta(new Vector2(1000, 250), .25f).SetDelay(.2f).Delay();
        boxOpen = true;
        player.SetCanMove(false);
    }
}

public enum DialogType {
    Text,
    Alert,
    Notification
}