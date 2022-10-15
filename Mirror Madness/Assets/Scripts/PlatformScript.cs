using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    Vector2 normal;
    // Start is called before the first frame update
    void Start()
    {
        normal = new Vector2(this.gameObject.transform.up.x, this.gameObject.transform.up.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //print("collided");
        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Fireball")
        {
            print("collided");
            BulletScript bullet = collision.gameObject.GetComponent <BulletScript> ();
            //print(bullet.velocity.y);
            Vector2 newVel = Vector2.Reflect(bullet.getVelocity(), normal);
            bullet.setVelocity(newVel);

            //print(newVel.y);
        }
        if (this.gameObject.tag == "Wooden" && collision.gameObject.tag == "Fireball")
        {
            Destroy(this.gameObject);
            //print(newVel.y);
        }
    }

}
