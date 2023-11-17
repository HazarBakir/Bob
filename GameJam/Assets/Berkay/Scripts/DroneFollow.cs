using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFollow : MonoBehaviour
{
    public float MaxDistance = 35;
    public float MinDistance = 10;
    public float Speed = 7;
    public Transform Player;

    public float upDownSpeed = 3f;

    public float amp;

    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * amp + startPos.y, transform.position.z);

        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) >= MinDistance)
        {
            Vector3 follow = Player.position;
            //setting always the same Y position
            follow.y = this.transform.position.y;

            // remenber to use the new 'follow' position, not the Player.transform.position or else it'll move directly to the player
            this.transform.position = Vector3.MoveTowards(this.transform.position, follow, Speed * Time.deltaTime);
        }
    }
}
