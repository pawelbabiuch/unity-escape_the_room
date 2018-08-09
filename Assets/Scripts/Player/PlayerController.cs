using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 10)]
    [Tooltip("Prędkość poruszania się gracza")]
    public int speed = 3;
    private float v, h; // vertical move, horizontal move
    private float rotateY = 0;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");

        if(v != 0 || h != 0)
        {
            anim.SetBool("isWalk", true);

            if (v != 0)
                rotateY = (v > 0) ? 270 : 90;
            else if (h != 0)
                rotateY = (h > 0) ? 0 : 180;

            if (transform.eulerAngles.y != rotateY)
                transform.eulerAngles = new Vector3(0, rotateY, 0);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if(anim.GetBool("isWalk"))
        {
            anim.SetBool("isWalk", false);
        }
    }
}
