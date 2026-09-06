using UnityEngine;

public enum RoomType
{
    Start,
    Normal,
    Ability,
    Secret,
    Boss,
    Gate
}

public class RoomDefinition : MonoBehaviour
{
    public string roomId;
    public RoomType roomType;
    public GameObject roomPrefab;
}