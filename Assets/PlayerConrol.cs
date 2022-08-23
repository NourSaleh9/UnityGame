using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConrol : MonoBehaviour
{
    public Transform walkU;
    public Transform walkD;
    public Transform walkL;
    public Transform walkR;

    public Sprite u;
    public Sprite d;
    public Sprite l;
    public Sprite r;

    public Transform Gfx;
    public SpriteRenderer sr;

    public float speed = 3;

    public bool nearTheDoor;

    public GameObject openDoorHint;

    public Image soundIcon;

    public Sprite soundon;
    public Sprite soundoff;

    public new AudioSource audio;


    private string dir;
    private Vector2 dirV;
    private bool nm;

    private void Start()
    {
        disableAnim();

        Gfx.gameObject.SetActive(true);
        sr = Gfx.gameObject.GetComponent<SpriteRenderer>();

        switchGFX(u);
        dir = "down";
        dirV = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switchGFX(walkL);
            dir = "left";
            dirV = Vector2.left;
            if (!audio.isPlaying && !nm)
            {
                audio.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            switchGFX(walkR);
            dir = "right";
            dirV = Vector2.right;
            if (!audio.isPlaying && !nm)
            {
                audio.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            switchGFX(walkU);
            dir = "up";
            dirV = Vector2.up;
            if (!audio.isPlaying && !nm)
            {
                audio.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            switchGFX(walkD);
            dir = "down";
            dirV = Vector2.down;
            if (!audio.isPlaying && !nm)
            {
                audio.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            stop(dir);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            switchGFX(walkL);
            stop(dir);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            switchGFX(walkL);
            stop(dir);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            switchGFX(walkL);
            stop(dir);
        }
        transform.Translate(Vector2.Lerp(Vector2.zero,dirV * speed,Time.deltaTime));
        
    }

    void switchGFX(Sprite s)
    {
        disableAnim();
        Gfx.gameObject.SetActive(true);
        sr.sprite = s;
    }

    void switchGFX(Transform t)
    {
        disableAnim();
        Gfx.gameObject.SetActive(false);
        t.gameObject.SetActive(true);
    }

    void disableAnim()
    {
        walkU.gameObject.SetActive(false);
        walkD.gameObject.SetActive(false);
        walkL.gameObject.SetActive(false);
        walkR.gameObject.SetActive(false);
    }

    void stop(string dir)
    {
        if (audio.isPlaying)
        {
            audio.Pause();
        }
        dirV = Vector2.zero;
        switch (dir)
        {
            case "left":
                switchGFX(l);
                break;
            case "right":
                switchGFX(r);
                break;
            case "up":
                switchGFX(u);
                break;
            case "down":
                switchGFX(d);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            openDoorHint.SetActive(true);
            openDoorHint.gameObject.GetComponent<Animator>().SetBool("Door Near",true);
            nearTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            openDoorHint.SetActive(false);
            openDoorHint.gameObject.GetComponent<Animator>().SetBool("Door Near",false);
            nearTheDoor = false;
        }
    }

    public void noMusic()
    {
        if (nm)
        {
            nm = false;
            soundIcon.sprite = soundon;
            return;
        }
        if (!nm)
        {
            nm = true;
            soundIcon.sprite = soundoff;
            return;
        }
    }
}
