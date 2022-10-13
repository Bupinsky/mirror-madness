using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
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
        Vector3 mousePos = Input.mousePosition;
        Vector3 launchDir = (mousePos - new Vector3(Screen.width / 2, Screen.height / 2, 0)) - this.gameObject.transform.position;
        launchDir = launchDir.normalized;
        Vector3 arrowPos = (launchDir * 1) + this.gameObject.transform.position;
        arrow.transform.position = new Vector3(arrowPos.x, arrowPos.y, arrow.transform.position.z);
        //arrow.transform.rotation = Quaternion.LookRotation(launchDir);

        //controls
        if (Input.GetMouseButtonDown(0))
        {
            bullets.Add(Instantiate(bulletBlueprint, new Vector3(arrow.transform.position.x, arrow.transform.position.y, 0), Quaternion.identity));
            bullets[bullets.Count - 1].transform.localScale = new Vector3(1, 1, 1);
            bullets[bullets.Count - 1].GetComponent<BulletScript>().SetVelocity(launchDir.x*10, launchDir.y*10);
        }
    }
}
