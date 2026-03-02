using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public PlayerController thePlayer;
    public BoxCollider2D playerCol;

    [SerializeField] Vector2 standOffset, standSize;
    [SerializeField] Vector2 slideOffset, slideSize;

    void Start()
    {
        thePlayer = GetComponent<PlayerController>();

        standSize = playerCol.size;
        standOffset = playerCol.offset;
    }


    void Update()
    {
        if (thePlayer._isSliding && !thePlayer._isJumping)
        {
            playerCol.size = slideSize;
            playerCol.offset = slideOffset;
        }
        if (!thePlayer._isSliding)
        {
            playerCol.size = standSize;
            playerCol.offset = standOffset;
        }
    }
}
