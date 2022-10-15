using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOnContact : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OtherCollider " + collision.otherCollider.transform.parent);
        //Debug.Log("Collider " + collision.collider.transform.parent);
        //Debug.Log("this " + this.transform.parent);
        if (!collision.collider.CompareTag("Food")) return;
        if (collision.collider.transform.IsChildOf(this.transform.parent)) return;

        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log("Stuck");
    }
}
