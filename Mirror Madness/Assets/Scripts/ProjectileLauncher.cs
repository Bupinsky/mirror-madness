using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject player;
    public GameObject arrow;
    public GameObject bulletBlueprint;
    public List<GameObject> bullets;
    public int numBullets;
    PlayerScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // convert mouse position to the relative world position of the mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        //calculate the direction vector for the bullet trajectory
        Vector3 launchDir = mousePosWorld - this.gameObject.transform.position;
        launchDir.z = 0;
        launchDir = launchDir.normalized;

        // locking to 8 directions
        Vector2 unitVec = new Vector2(0.0f, 1.0f);

        //simpler version I couldnt figure out sorryyyy
        //float theAngle = (Vector2.SignedAngle(unitVec, launchDir) + 22.5f) % 45;
        //theAngle *= 45;
        //launchDir.x = Mathf.Cos(theAngle * Mathf.PI / 180);
        //launchDir.y = Mathf.Sin(theAngle * Mathf.PI / 180);


        print(Vector2.SignedAngle(unitVec, launchDir));
        //UP
        if (Vector2.SignedAngle(unitVec, launchDir) < 22.5 && Vector2.SignedAngle(unitVec, launchDir) > -22.5)
        {
            launchDir.x = 0;
            launchDir.y = 1;
        }
        //UP RIGHT
        else if (Vector2.SignedAngle(unitVec, launchDir) < 45+22.5 && Vector2.SignedAngle(unitVec, launchDir) > 45-22.5)
        {
            launchDir.x = 1;
            launchDir.y = 1;
        }
        //RIGHT
        else if (Vector2.SignedAngle(unitVec, launchDir) < 90+22.5 && Vector2.SignedAngle(unitVec, launchDir) > 90-22.5)
        {
            launchDir.x = 1;
            launchDir.y = 0;
        }
        //DOWN RIGHT
        else if (Vector2.SignedAngle(unitVec, launchDir) < 135+22.5 && Vector2.SignedAngle(unitVec, launchDir) > 135-22.5)
        {
            launchDir.x = 1;
            launchDir.y = -1;
        }
        // DOWN LEFT
        else if (Vector2.SignedAngle(unitVec, launchDir) < -135+22.5 && Vector2.SignedAngle(unitVec, launchDir) > -135-22.5)
        {
            launchDir.x = -1;
            launchDir.y = -1;
        }
        // LEFT
        else if (Vector2.SignedAngle(unitVec, launchDir) < -90+22.5 && Vector2.SignedAngle(unitVec, launchDir) > -90-22.5)
        {
            launchDir.x = -1;
            launchDir.y = 0;
        }
        // UP LEFT
        else if (Vector2.SignedAngle(unitVec, launchDir) < -45+22.5 && Vector2.SignedAngle(unitVec, launchDir) > -45-22.5)
        {
            launchDir.x = -1;
            launchDir.y = 1;
        }
        else
        {
            // DOWN
            launchDir.x = 0;
            launchDir.y = -1;
        }
        // it was backwards idk lol
        launchDir.x *= -1;
        // normalizing again cuz didn't wanna look up 45 degree direction vectors
        launchDir = launchDir.normalized;

        // rotating arrow shows the player where the bullets will go when they fire
        Vector3 arrowPos = (launchDir * 2.2f) + player.transform.position;
        arrow.transform.position = new Vector3(arrowPos.x, arrowPos.y, arrow.transform.position.z);

        // setting up the rotation
        //Vector2 unitVec = new Vector2(0, 1);
        Vector2 dirVec2 = new Vector2(launchDir.x, launchDir.y);
        arrow.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(unitVec, dirVec2));

        //controls
        if (Input.GetMouseButtonDown(0))
        {
            // only shoot if the player has bullets and is on the ground
            if (numBullets > 0 && playerScript.isGrounded)
            {
                //add bullets to the list of bullets, this will make them easy to manage.
                bullets.Add(Instantiate(bulletBlueprint, new Vector3(arrow.transform.position.x, arrow.transform.position.y, 0), Quaternion.identity));
                bullets[bullets.Count - 1].transform.localScale = new Vector3(1, 1, 1);
                bullets[bullets.Count - 1].GetComponent<BulletScript>().SetVelocity(launchDir.x * 10, launchDir.y * 10);
                
                numBullets -= 1;
            }
        }
    }

    void OnGUI()
    {

        GUI.color = Color.white;
        GUI.skin.box.fontSize = 24;
        GUI.skin.box.wordWrap = false;


        GUI.Box(new Rect(10, 10, 100, 30), "Shots: " + numBullets);
    }
}
