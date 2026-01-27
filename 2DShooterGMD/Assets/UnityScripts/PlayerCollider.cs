using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public PlayerController thePlayer;
    public BoxCollider2D playerCol;

    [SerializeField] Vector2 standOffset, standSize;
    [SerializeField] Vector2 crouchOffset, crouchSize;
    [SerializeField] Vector2 moveOffset, moveSize;

    void Start()
    {
        thePlayer = GetComponent<PlayerController>();

        standSize = playerCol.size;
        standOffset = playerCol.offset;
    }


    void Update()
    {
        if (thePlayer.isCrouched && !thePlayer.isJumping)
        {
            playerCol.size = crouchSize;
            playerCol.offset = crouchOffset;
        }
        if (!thePlayer.isCrouched || (!thePlayer.isCrouched && !thePlayer.isMoving))
        {
            playerCol.size = standSize;
            playerCol.offset = standOffset;
        }
        if (thePlayer.isMoving || (thePlayer.isShooting && !thePlayer.isCrouched))
        {
            playerCol.size = moveSize;
            playerCol.offset = moveOffset;
        }
    }
}
