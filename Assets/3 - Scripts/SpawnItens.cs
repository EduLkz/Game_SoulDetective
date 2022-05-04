using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SpawnItens : MonoBehaviour{

    #region Singleton
    private static SpawnItens instance;
    public static SpawnItens Instance { get { return instance; } }

    private void Awake() {
        instance = this;
    }
    #endregion

    public int lostFounds;

    public AudioSource musicSource;
    public AudioClip normalClip;
    public AudioClip ChaseClip;

    public AudioSource audioSource;
    public AudioClip lostClip;
    public AudioClip wonClip;

    public GameObject lost;
    public GameObject won;

    public GameObject pause;
    bool paused;

    public List<Transform> possibleLocations = new List<Transform>();
    public List<GameObject> itens = new List<GameObject>();
    public GameObject soul;
    public List<Transform> soulSpawn = new List<Transform>();

    public GameObject evilSoul;
    public List<Transform> evilSoulSpawn = new List<Transform>();
    List<Enemy> evil = new List<Enemy>();

    bool someoneChasing;

    private void Start() {
        Time.timeScale = 1;
        for(int i = 0; i < 6; i++) {
            SpawnItem();
        }

        for(int i = 0; i < 4; i++) {
            GameObject _evilSoul = Instantiate(evilSoul, evilSoulSpawn[i].position, Quaternion.identity);
            evil.Add(_evilSoul.GetComponent<Enemy>());
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;

            if(paused) {
                pause.SetActive(true);
                Time.timeScale = 0;
            } else {
                pause.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Resume() {
        pause.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    private void LateUpdate() {
        foreach(Enemy e in evil) {
            someoneChasing = false;
            if(e.isChasing()) {
                someoneChasing = true;
                break;
            }
        }

        if(someoneChasing) {
            if(musicSource.clip != ChaseClip) {
                StartCoroutine(ChageMusic(ChaseClip));
            }
        } else {
            if(musicSource.clip != normalClip) {
                StartCoroutine(ChageMusic(normalClip));
            }
        }
    }

    IEnumerator ChageMusic(AudioClip _music) {
        musicSource.DOFade(0, .5f);
        
        yield return new WaitForSeconds(.5f);
        musicSource.clip = (_music);
        musicSource.Play();
        musicSource.DOFade(0.15f, .5f);
    }

    public void Found() {
        Debug.Log("+1");
        SpawnEnemy();
        
        lostFounds++;
        if(lostFounds >= 6) {
            EndGame(true);
        }
    }

    public void Lose() {
        EndGame(false);
    }

    void EndGame(bool _win) {
        Time.timeScale = 0;

        if(_win) {
            won.SetActive(true);
            audioSource.PlayOneShot(wonClip);
        } else {
            lost.SetActive(true);
            audioSource.PlayOneShot(lostClip);
        }
    }

    void SpawnItem() {
        int _place = Random.Range(0, possibleLocations.Count);
        int _soulplace = Random.Range(0, soulSpawn.Count);

        GameObject _item = Instantiate(itens[Random.Range(0, itens.Count)], possibleLocations[_place].position, Quaternion.identity);
        GameObject _soul = Instantiate(soul, soulSpawn[_soulplace].position, Quaternion.identity);

        _item.name = _item.name.Replace("(Clone)", "");

        Item _i = _item.GetComponent<Item>();
        _i.place = possibleLocations[_place].name;

        _soul.GetComponent<Soul>().Initialize(_i);


        possibleLocations.RemoveAt(_place);
        soulSpawn.RemoveAt(_soulplace);
    }

    void SpawnEnemy() {
        int _evilSoulplace = Random.Range(0, evilSoulSpawn.Count);

        GameObject _evilSoul = Instantiate(evilSoul, evilSoulSpawn[_evilSoulplace].position, Quaternion.identity);
        evilSoulSpawn.RemoveAt(_evilSoulplace);
        evil.Add(_evilSoul.GetComponent<Enemy>());
    }

    public void Retry() {
        SceneManager.LoadScene(1);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
