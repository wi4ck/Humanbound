using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject roomPrefab;

    private void Start()
    {
        Instantiate(roomPrefab, new Vector3(10, 0, 0), Quaternion.identity);
    }
}