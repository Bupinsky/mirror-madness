using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector2 velocity;
    public int speed;
    float x;
    float y;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        x = transform.position.x + Time.deltaTime * velocity.x;
        y = transform.position.y + Time.deltaTime * velocity.y;
        transform.position = new Vector3(x, y, 0);


        // destroy out of bounds bullets
        if (!isInBounds())
        {
            Destroy(gameObject);
            //Debug.Log("bullet destroyed");
        }
    }


    public void SetVelocity(float xVel, float yVel)
    {
        velocity = new Vector3(xVel * speed, yVel * speed);
    }

    public bool isInBounds()
    {
        if (Mathf.Abs(x) > 10 || Mathf.Abs(y) > 10) return false;
        return true;
    }

    public Vector2 getVelocity()
    {
        return velocity;
    }

    public void setVelocity(Vector2 newVel)
    {
        velocity = newVel;
    }
}
