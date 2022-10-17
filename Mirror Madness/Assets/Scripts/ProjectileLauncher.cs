using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject player;
    public GameObject arrow;
    public GameObject bulletBlueprint;
    public List<GameObject> bullets;
    // Start is called before the first frame update
    void Start()
    {
        
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

        // rotating arrow shows the player where the bullets will go when they fire
        Vector3 arrowPos = (launchDir * 2) + player.transform.position;
        arrow.transform.position = new Vector3(arrowPos.x, arrowPos.y, arrow.transform.position.z);

        // setting up the rotation
        Vector2 unitVec = new Vector2(0, 1);
        Vector2 dirVec2 = new Vector2(launchDir.x, launchDir.y);
        arrow.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(unitVec, dirVec2));

        //controls
        if (Input.GetMouseButtonDown(0))
        {
            //add bullets to the list of bullets, this will make them easy to manage.
            bullets.Add(Instantiate(bulletBlueprint, new Vector3(arrow.transform.position.x, arrow.transform.position.y, 0), Quaternion.identity));
            bullets[bullets.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            bullets[bullets.Count - 1].GetComponent<BulletScript>().SetVelocity(launchDir.x*10, launchDir.y*10);
        }
    }
}
