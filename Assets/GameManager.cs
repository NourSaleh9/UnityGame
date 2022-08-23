using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject YouWin;
    public GameObject YouLost;
    public GameObject Settings;
    public GameObject about;
    public GameObject gameOver;

    public AudioClip menu;
    public AudioClip game;
    public AudioSource music;

    public List<GameObject> Rooms;

    public GameObject curtPlayer;
    public GameObject curtCamera;

    public GameObject puzzels;

    public GameObject TimerPan;

    public Text timme;
    public Image soundIcon;

    public Sprite soundon;
    public Sprite soundoff;

    public Image background;
    public Sprite artist;
    public Sprite developer;


    private bool fadeIn;
    private bool fadeOut;
    private AudioClip nextClip;
    private float maximam_limit;

    public AudioSource clic;
    public AudioSource fail;

    private float TimeFade = 0.01f;
    private float curtTimeFade;
    private GameObject curtPuzzel;

    private List<GameObject> joints;

    float Timer = 20;

    private bool v;
    private bool nm = false;

    public GameObject back;

    private void Start()
    {
        switchUi(MainMenu);
        nextClip = menu;
        music.Play();
        fadeIn = true;
        maximam_limit = 0.5f;
        curtTimeFade = TimeFade;
        joints = new List<GameObject>();
        curtPlayer.SetActive(false);
        back.SetActive(false);
    }


    public void play()
    {
        allNotActive();
        nextClip = game;
        fadeOut = true;
        maximam_limit = 0.35f;
        curtPlayer.SetActive(true);
        back.SetActive(true);
    }

    public void GoSettings()
    {
        switchUi(Settings);
    }

    public void mainMenu()
    {
        switchUi(MainMenu);
        nextClip = menu;
        fadeOut = true;
        curtPlayer.SetActive(true);
        back.SetActive(false);
    }

    public void About()
    {
        switchUi(about);
    }

    public void Uwin()
    {
        switchUi(YouWin);
    }

    public void Ulost()
    {
        switchUi(YouLost);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void switchUi(GameObject newPanal)
    {
        clic.enabled = false;
        allNotActive();
        newPanal.SetActive(true);
        clic.enabled = true;
    }

    void allNotActive()
    {
        MainMenu.SetActive(false);
        YouWin.SetActive(false);
        YouLost.SetActive(false);
        Settings.SetActive(false);
        about.SetActive(false);
        gameOver.SetActive(false);
    }
    
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F) && Rooms.Count > 0 && curtPlayer.gameObject.GetComponent<PlayerConrol>().nearTheDoor)
        {
            OpenPuzzel();
            v = true;
        }

        if (!nm)
        {
            if (curtTimeFade <= 0)
            {
                curtTimeFade = TimeFade;
                if (fadeIn)
                {
                    music.volume = music.volume < maximam_limit ? music.volume + 0.01f : music.volume;
                    if (music.volume >= 0.5)
                    {
                        fadeIn = false;
                    }
                }
                if (fadeOut)
                {
                    music.volume = music.volume > 0 ? music.volume - 0.01f : music.volume;
                    if (music.volume <= 0)
                    {
                        fadeOut = false;
                        fadeIn = true;
                        music.clip = nextClip;
                        if (!music.isPlaying)
                        {
                            music.Play();
                        }
                    }
                }
            }
            else
            {
                curtTimeFade -= Time.deltaTime;
            }
        }
        
        if (joints.Count > 0)
        {
            curtPlayer.SetActive(false);
            if (! (Timer <= 0 && joints.Count > 0))
            {
                checkPuzzle();
                Timer -= Time.deltaTime;
                timme.text = "" + (Mathf.Floor(Timer) < 0 ? 0 : Mathf.Floor(Timer));
            }
            else
            {
                GameOver();
                TimerPan.SetActive(false);
                fail.enabled = true;
            }
        }
    }

    void GameOver()
    {
        ClosePuzzle();
        joints.Clear();
        switchUi(gameOver);
    }


    void OpenPuzzel ()
    {
        fail.enabled = false;
        TimerPan.SetActive(true);
        int index = Random.Range(0,puzzels.GetComponent<Puzzels>().puzzels.Count);
        puzzels.GetComponent<Puzzels>().puzzels[index].SetActive(true);
        curtPuzzel = puzzels.GetComponent<Puzzels>().puzzels[index];
        FillJoints();
    }

    void FillJoints()
    {
        RectTransform rt = curtPuzzel.gameObject.GetComponent<RectTransform>();
        GameObject joint1 = rt.Find("Joint1").gameObject;
        if (joint1 != null)
        {
            joints.Add(joint1); Debug.Log("Add");
        }
        GameObject joint2 = rt.Find("Joint2").gameObject;
        if (joint1 != null)
        {
            joints.Add(joint2); Debug.Log("Add");
        }
        GameObject joint3 = rt.Find("Joint3").gameObject;
        if (joint1 != null)
        {
            joints.Add(joint3); Debug.Log("Add");
        }
        GameObject joint4 = rt.Find("Joint4").gameObject;
        if (joint1 != null)
        {
            joints.Add(joint4); Debug.Log("Add");
        }
        GameObject joint5 = rt.Find("Joint5").gameObject;
        if (joint1 != null)
        {
            joints.Add(joint5); Debug.Log("Add");
        }
        for (int i = 0; i < joints.Count; i++)
        {
            GameObject curtJoint = joints[i];
            curtJoint.gameObject.GetComponent<JointScript>().Done = false;
        }
    }

    void checkPuzzle()
    {
        if (joints.Count > 0)
        {
            Debug.Log(joints.Count);
            for (int i = 0; i < joints.Count; i++)
            {
                GameObject curtJoint = joints[i];
                if (curtJoint.gameObject.GetComponent<JointScript>().Done)
                {
                    joints.Remove(curtJoint);
                }
                if (joints.Count <= 0)
                {
                    OpenRoom();
                    TimerPan.SetActive(false);
                    Timer = 20;
                    curtPlayer.SetActive(true);
                }
            }
        }
    }
    
    void OpenRoom()
    {
        int indexe = Random.Range(0, Rooms.Count);
        GameObject curtRoom = Rooms[indexe];
        Vector3 CameraPos = curtRoom.transform.Find("CameraPos").transform.position;
        Vector2 Player = curtRoom.transform.Find("SpwonPoint").transform.position;

        CameraPos = new Vector3(CameraPos.x, CameraPos.y, -10);
        curtCamera.transform.position = CameraPos;
        curtPlayer.transform.position = Player;
        ClosePuzzle();
    }

    void  ClosePuzzle()
    {
        for (int i = 0; i < puzzels.GetComponent<Puzzels>().puzzels.Count; i++)
        {
            puzzels.GetComponent<Puzzels>().puzzels[i].gameObject.SetActive(false);
        }
        
    }

    public void noMusic()
    {
        if (nm)
        {
            nm = false;
            music.volume = maximam_limit;
            soundIcon.sprite = soundon;
            if (!music.isPlaying)
            {
                music.Play();
            }
            return;
        }
        if (!nm)
        {
            nm = true;
            soundIcon.sprite = soundoff;
            if (music.isPlaying)
            {
                music.Stop();
            }
            return;
        }
    }

    public void pause()
    {
        mainMenu();
    }

    public void art()
    {
        background.sprite = artist;
    }

    public void dev()
    {
        background.sprite = developer;
    }
}
