using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Snappable : MonoBehaviour
{
    private Snappable closest;
    private const float scale = 0.1f;
    private const float width = 6.941f;
    private const float height = 6.038f;

    float GetDistance(Snappable other)
    {
        float xThis = transform.parent.position.x + transform.position.x;
        float yThis = transform.parent.position.y + transform.position.y;
        float zThis = transform.parent.position.z + transform.position.z;
        float xOther = other.transform.parent.position.x + other.transform.position.x;
        float yOther = other.transform.parent.position.y + other.transform.position.y;
        float zOther = other.transform.parent.position.z + other.transform.position.z;
        return (xOther - xThis) * (xOther - xThis) + (yOther - yThis) * (yOther - yThis) + (zOther - zThis) * (zOther - zThis);
    }
    void GetClosestFrame() {
        Snappable[] frames = (Snappable[]) GameObject.FindObjectsOfType(typeof(Snappable));
        int i = 0;
        if (frames.Length == 1)
        {
            closest = null;
            return;
        }
        float minDist = frames[0].GetDistance(this);
        closest = frames[0];
        if (minDist == 0)
        {
            minDist = frames[1].GetDistance(this);
            closest = frames[1];
        }
        foreach (Snappable frame in frames)
        {
            float dist = frame.GetDistance(this);
            if (dist > 0 && dist < minDist)
            {
                minDist = dist;
                closest = frame;
            }
            i++;
        }
    }

    void Snap(int hex)
    {
        GameObject temp = transform.parent.gameObject;
        if (temp == closest.transform.parent.gameObject)
        {
            return;
        }
        transform.SetParent(closest.transform.parent, false);
        Destroy(temp);
        Vector3 pos = new Vector3(0, 0, 0);
        Debug.Log(hex);
        switch (hex)
        {
            case 0:
                pos = closest.transform.position + new UnityEngine.Vector3(width/4 * scale, height/2 * scale, 0);
                break;
            case 1:
                pos = closest.transform.position - new UnityEngine.Vector3(-width/2 * scale, 0, 0);
                break;
            case 2:
                pos = closest.transform.position - new UnityEngine.Vector3(-width/4 * scale, height/2 * scale, 0);
                break;
            case 3:
                pos = closest.transform.position + new UnityEngine.Vector3(-width / 4 * scale, -height / 2 * scale, 0);
                break;
            case 4:
                pos = closest.transform.position - new UnityEngine.Vector3(width/2 * scale, 0, 0);
                break;
            case 5:
                pos = closest.transform.position - new UnityEngine.Vector3(width/4 * scale, -height/2 * scale, 0);
                break;
        }
        //Vector3 e = closest.transform.rotation.eulerAngles;
        //closest.transform.rotation.SetEulerRotation(90, e.y, e.z);
        transform.SetPositionAndRotation(pos, closest.transform.rotation);
        //transform.parent.RotateAround(new Vector3(1, 0, 0), 90 * Mathf.Deg2Rad);
    }

    public void DoSnap()
    {
        GetClosestFrame();
        if (closest == null)
        {
            return;
        }
        /*
        UnityEngine.Quaternion oldrot = transform.rotation;
        UnityEngine.Quaternion oldrot2 = closest.transform.rotation;
        Vector3 oldpos = transform.position;
        Vector3 oldpos2 = closest.transform.position;
        Transform parent = transform.parent;
        Transform parent2 = closest.transform.parent;
        transform.SetParent(parent.parent);
        closest.transform.SetParent(parent.parent);
        */
        //UnityEngine.Quaternion rot = transform.parent.rotation;
        //transform.SetPositionAndRotation(transform.parent.position, UnityEngine.Quaternion.Euler(new Vector3(90, 0, 0)));
        //closest.transform.SetPositionAndRotation(transform.position, UnityEngine.Quaternion.Euler(new Vector3(90, 0, 0)));
        //closest.transform.SetPositionAndRotation(oldpos, transform.rotation);
        float x = closest.transform.parent.position.x + closest.transform.position.x - transform.parent.position.x - transform.position.x;
        float y = closest.transform.parent.position.y + closest.transform.position.y - transform.parent.position.y - transform.position.y;
        //closest.transform.parent.SetPositionAndRotation(oldpos, oldrot);
        /*
        transform.SetParent(parent);
        closest.transform.SetParent(parent2);
        transform.SetPositionAndRotation(oldpos, oldrot);
        closest.transform.SetPositionAndRotation(oldpos2, oldrot2);
        */
        //transform.parent.SetPositionAndRotation(transform.parent.position, rot);
        if (GetDistance(closest) < 70f * scale * scale)
        {
            float angle = Mathf.Atan(y / x) * Mathf.Rad2Deg;
            Debug.Log(angle);
            Debug.Log(x);
            Debug.Log(y);
            if (x > 0)
            {
                if (angle < 0)
                {
                    angle += 180;
                }
                else
                {
                    angle -= 180;
                }
            }
            if (angle < 150f && angle > 90f)
            //Hex 5
            {
                Snap(5);
            }
            else if (angle < 90f && angle > 30f)
            //Hex 0
            {
                Snap(0);
            }
            else if (angle < 30f && angle > -30f)
            //Hex 1
            {
                Snap(1);
            }
            else if (angle < -30f && angle > -90f)
            //Hex 2
            {
                Snap(2);
            }
            else if (angle < -150f && angle > -150f)
            //Hex 3
            {
                Snap(3);
            }
            else
            //Hex 4
            {
                Snap(4);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
