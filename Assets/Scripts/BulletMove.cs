using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float SpeedX;
    public float SpeedY;
    public float Speed;
    private bool IsSet;
    private int tick;
    public int ticklimit = 250;
    public GameObject Impactfx;
    public ContactFilter2D filter2D;

    private Vector3 Pos1, Pos2;
    public TrailRenderer tr;
    void Update()
    {
        if (!IsSet) return;
        Pos1 = transform.position;
        transform.Translate(SpeedX, SpeedY, 0, Space.World);
        Pos2 = transform.position;
        GameObject hitObject = CheckNextHit(Pos1, Pos2);
        if (tick > ticklimit || hitObject != null)
        {           
            if (hitObject.GetComponent<Rigidbody>() != null)
            {
                hitObject.GetComponent<Rigidbody>().AddForce(new Vector3(SpeedX * 10, SpeedY * 10, 0), ForceMode.Impulse);
            }
            Deactivate();
        }
        tick++;
    }

    public void Setup(Vector2 pos, float bulletspeed){
        transform.position = pos;
        tr.Clear();
        tick = 0;
        Speed = bulletspeed;
        SpeedY *= Speed;
        SpeedX *= Speed;
        IsSet = true;
    }


    public void Deactivate(){
        IsSet = false;
        Instantiate(Impactfx, new Vector3(transform.position.x - SpeedX, transform.position.y -SpeedY, 5), Quaternion.identity);
        gameObject.SetActive(false);
    }

    private GameObject CheckNextHit(Vector2 pos1, Vector2 pos2)
    {
        RaycastHit hit;
        bool IsHit = Physics.Linecast(pos1, pos2, out hit);
        if (IsHit == true)
        {
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
            
    }
}
