using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    Vector2 normal;
    // Start is called before the first frame update
    void Start()
    {
        normal = new Vector2(this.gameObject.transform.forward.x, this.gameObject.transform.forward.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //print("collided");
        if (collision.gameObject.tag == "Projectile")
        {
            print("collided");
            BulletScript bullet = collision.gameObject.GetComponent <BulletScript> ();
            Vector2 newVel = Vector2.Reflect(bullet.getVelocity(), normal);
            bullet.setVelocity(newVel);
        }
    }

}
