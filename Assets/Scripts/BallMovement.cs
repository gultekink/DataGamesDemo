using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class BallMovement : MonoBehaviour
{
    
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private ConstantForce _constantForce;

    [SerializeField] private float _followSpeed;
    [SerializeField] private int _followGap;

    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _Force;
    

    private Rigidbody _ballRigidbody;

    private bool _isGameStart;
    public Text _text;
    public List<GameObject> _body;
    private List<Vector3> _position;

    private void Start()
    {
        //point[2].z = -5;
        //point[1] = point[0] + (point[2] - point[0]) / 2 + Vector3.up * _angle;
        _ballRigidbody = GetComponent<Rigidbody>();
        _isGameStart = false;
        _body = new List<GameObject>();
        _position = new List<Vector3>();
      
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _isGameStart = true;
                _text.enabled = false;

                //_constantForce.force = new Vector3(0, 3, 2.4f);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 pos = new Vector3(transform.position.x + touch.deltaPosition.x * _moveSpeed / 100 * Time.deltaTime, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, pos, 1);

            }
        }

        _position.Insert(0,transform.position);
        int index = 0;

        foreach (var body in _body)
        {
            Vector3 point = _position[Mathf.Min(index* _followGap, _position.Count-1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * _followSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
            
        }
    }

    void OnCollisionEnter(Collision otherObject)
    {
        if (otherObject.gameObject.tag == "Ground" && _isGameStart)
        {
            _constantForce.force = new Vector3(0, 2,3.1f);
            _ballRigidbody.velocity = Vector3.up * _jumpForce;
            Debug.Log(gameObject.transform.position);
        }
    }

    void OnTriggerEnter(Collider otherObject)
    {
        
        if (otherObject.gameObject.tag == "AddThree")
        {
            Destroy(otherObject.gameObject);
            
                AddBall(3);
            
        }
        if (otherObject.gameObject.tag == "AddFour")
        {
            Destroy(otherObject.gameObject);
            
                AddBall(4);
            
        }
        if (otherObject.gameObject.tag == "AddFive")
        {
            Destroy(otherObject.gameObject);
            
                AddBall(5);
            
        }
        if (otherObject.gameObject.tag == "AddSeven")
        {
            Destroy(otherObject.gameObject);
            
                AddBall(7);
            
        }

       
    }

    private void AddBall(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject body = Instantiate(_ballPrefab);
            _body.Add(body);
        }
            
    }

}
