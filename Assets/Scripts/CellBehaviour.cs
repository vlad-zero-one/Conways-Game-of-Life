using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    /*
    public Collider2D collider;
    public bool isLive = false;
    public ContactPoint2D[] points = new ContactPoint2D[8];

    void Start()
    {
        collider = GetComponent<Collider2D>();
        if (isLive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //Debug.Log(name + " соприкасается с " + collider.GetContacts(points));

        Debug.Log(name + " зарегестрировал прикосновение с " + collider.GetContacts(points) + " точками");
        Debug.Log("А именно:");
        foreach(var point in points)
        {
            Debug.Log(point.point);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.contactCount < 2 || collision.contactCount > 3)
        //{
        //    collision.gameObject.SetActive(false);
        //    Debug.Log("!!!!!!!!!!!!!!");
        //}

        //Debug.Log(name + " зарегестрировал прикосновение с " + collision.GetContacts(points) + " точками");
    }
    */
}
