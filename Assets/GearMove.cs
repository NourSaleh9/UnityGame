using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMove : MonoBehaviour
{
    RectTransform rt;

    bool holding;

    Vector2 orgPos;

    public RectTransform target;

    private void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        orgPos = rt.position;
    }

    void Update()
    {
        if (holding)
        {
            Vector2 pos = Input.mousePosition;
            rt.position = pos;
        }
        else
        {
            if (rt.position.x <= target.position.x + 30 && rt.position.x >= target.position.x - 30 && rt.position.y <= target.position.y + 30 && rt.position.y >= target.position.y - 30)
            {
                rt.position = target.position;
                target.gameObject.GetComponent<JointScript>().Done = true;
                return;
            }
            rt.position = orgPos;
        }
    }

    public void hold()
    {
        holding = true;
    }
    public void donHold()
    {
        holding = false;
    }
}
