using UnityEngine;

public enum ConnectionDirection
{
    Left,
    Right,
    Top,
    Bottom
}

public class RoomConnection : MonoBehaviour
{
    public ConnectionDirection direction;
}