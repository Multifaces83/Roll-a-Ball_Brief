using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Move_D_Mobile : MonoBehaviour
{
    [SerializeField] private Button _buttonUp;
    [SerializeField] private Button _buttonDown;
    [SerializeField] private Button _buttonLeft;
    [SerializeField] private Button _buttonRight;
    private float _speed = 5.0f;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        // _buttonUp.onClick.AddListener(() => MoveUp());
        // _buttonDown.onClick.AddListener(() => MoveDown());
        // _buttonLeft.onClick.AddListener(() => MoveLeft());
        // _buttonRight.onClick.AddListener(() => MoveRight());
        // rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
       
    }

}
