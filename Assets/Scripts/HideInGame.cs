using UnityEngine;

public class HideInGame : MonoBehaviour
{
    void Awake()
    {
        Destroy(GetComponent<SpriteRenderer>());
    }
}