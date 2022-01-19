using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    public BallMovement ballMovement;
    [SerializeField] private Vector3 _offset;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_playerObject.transform.position.x, 1.41f, _playerObject.transform.position.z) + _offset;
        
        if (ballMovement._body.Count >=3 )
        {
            ZoomOut();
        }
        if (ballMovement._body.Count >=8)
        {
            ZoomOut();
        }
        if (ballMovement._body.Count >= 12)
        {
            ZoomOut();
        }

    }

    void ZoomOut()
    {
        transform.position += Vector3.Lerp(transform.position, new Vector3(0, 0.2f, -0.5f),1);
        
    }
}
