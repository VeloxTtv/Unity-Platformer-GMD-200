using UnityEngine;

public class muzzleFlash : MonoBehaviour
{
    public PlayerController thePlayer;
    public float flashDuration = 0.05f;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (thePlayer.isShooting)
        {
            anim.PlayInFixedTime("muzzleFlash");
        }
    }
}
