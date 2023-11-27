using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] anchors;
    private int _currentAnchorIndex = 0;
    
    [SerializeField] private float speed = 2f;
    private void Update()
    {
        if (Vector2.Distance(anchors[_currentAnchorIndex].transform.position, transform.position) <  .1f)
        {
            _currentAnchorIndex++;
            if (_currentAnchorIndex >= anchors.Length)
            {
                _currentAnchorIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, anchors[_currentAnchorIndex].transform.position,
            Time.deltaTime * speed);
    }
}
