using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script uses the CharacterController2D 

public class PlayerMovement2D : MonoBehaviour
{
    private CharacterController2D _characterController;

    public float runSpeed = 40f;
    private bool _jump = false;

    private float _horizontalInput;

    private Queue<KeyCode> _inputBuffer = new Queue<KeyCode>();
    [SerializeField]
    private float _inputDequeueTime;
        
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement Input
        
        _horizontalInput = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _inputBuffer.Enqueue(KeyCode.Space);
            Invoke("DequeueAction", _inputDequeueTime);
        }

        if (_inputBuffer.Count > 0)
        {
            if (_inputBuffer.Peek() == KeyCode.Space && !_characterController.hasJumped)
            {
                _inputBuffer.Dequeue();
                _jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        _characterController.Move(_horizontalInput * Time.fixedDeltaTime, _jump);
        _jump = false;
    }

    private void DequeueAction()
    {
        if (_inputBuffer.Count > 0) _inputBuffer.Dequeue();
    }
}
