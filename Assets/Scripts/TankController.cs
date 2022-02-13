using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("Tank Movement Variable")]
    [Space]
    public float speedTanks;
    [Space] 
    public float rotationSpeed;
  
    
    [Space]
    [Header("Tank Roll Variable")]
    [Space] 
    public float rollSpeed;
    [Space] 
    public float delayRoll;

    [Space] [Header("Tank Shoot Variable")] 
    [Space]
    public GameObject bulletGo;
    [Space]
    public Transform firePointTransform;
    [Space] 
    public float delayShoot;


    
    [Space]
    [Header("Tank Renderer Reference")]
    [Space]
    public Renderer _renderer;

    
    #region Private Reference
    
    
    private Rigidbody _rigidbody;

    private JoyconInputHandler _input;

    #endregion

    #region Private Move Variable

    private float hInput;
    private float VInput;

    private Vector3 _directionPlayer1;
    private Vector3 _directionPlayer2;

    #endregion

    #region Private Roll Variable

    private Vector3 rollDirectionPlayer1;
    private Vector3 rollDirectionPlayer2;

    private bool _canRollPlayer1;
    private bool _canRollPlayer2;


    #endregion

    #region Bullet Variable

    private bool _canShootPlayer1;
    private bool _canShootPlayer2;


    #endregion

    #region Unity CallBack Function

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _input = GetComponent<JoyconInputHandler>();

        _input.OnClickSlEvent += OnSlButton;
        
        _input.OnClickDpadUpEvent += OnDpadDownButton;
        
        _input.OnClickDpadDownEvent += OnDpadUpButton;


        switch (_input.jc_ind)
        {
            case 0:
                _renderer.material.color = Color.yellow;
                break;
            
            case 1:
                _renderer.material.color = Color.green;
                break;
                
        }
    }

    private void Update()
    {
        GetDirectionPlayer1();
        
        GetDirectionPlayer2();
    }

    private void FixedUpdate()
    {
        switch (_input.jc_ind)
        {
            case 0:
                MovePlayer1();
                break;
            
            case 1:
                MovePlayer2();
                break;
        }
    }

    #endregion

    #region Move Function

    void MovePlayer1()
    {
        _rigidbody.MovePosition(transform.position + (_directionPlayer1 * speedTanks * Time.deltaTime));
        
        AimDirection(_directionPlayer1);
    }
    
    void MovePlayer2()
    {
        _rigidbody.MovePosition(transform.position + (_directionPlayer2 * speedTanks * Time.deltaTime));

        AimDirection(_directionPlayer2);
    }


    void AimDirection(Vector3 move)
    {
        if (move != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(move, Vector3.up);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation, rotationSpeed);
        }
    }

    void GetDirectionPlayer1()
    {
        _directionPlayer1 = new Vector3(_input.stick[1], 0, -_input.stick[0]);
    }

    void GetDirectionPlayer2()
    {
        _directionPlayer2 = new Vector3(-_input.stick[1], 0,_input.stick[0]);
    }
    #endregion

    #region Roll Function
    
    void OnSlButton()
    {
        switch (_input.jc_ind)
        {
            case 0:
                if (!_canRollPlayer1)
                {
                    RollPlayer1();
                }
                break;
            
            case 1:
                if (!_canRollPlayer2)
                {
                    RollPlayer2();
                }
                break;
        }
    }

    IEnumerator RollDelayPlayer1()
    {
        yield return new WaitForSeconds(delayRoll);
        _canRollPlayer1 = false;
    }
    
    IEnumerator RollDelayPlayer2()
    {
        yield return new WaitForSeconds(delayRoll);
        _canRollPlayer2 = false;
    }


    void RollPlayer1()
    {
        rollDirectionPlayer1 = _directionPlayer1;
        
        _rigidbody.AddForce(new Vector3(rollDirectionPlayer1.x,rollDirectionPlayer1.y,rollDirectionPlayer1.z) * rollSpeed,ForceMode.Impulse);
        
        _canRollPlayer1 = true;
        
        StartCoroutine(RollDelayPlayer1());

    }
    
    void RollPlayer2()
    {
        rollDirectionPlayer2 = _directionPlayer2;
        
        _rigidbody.AddForce(new Vector3(rollDirectionPlayer2.x,rollDirectionPlayer2.y,rollDirectionPlayer2.z) * rollSpeed,ForceMode.Impulse);
        
        _canRollPlayer2 = true;

        StartCoroutine(RollDelayPlayer2());

    }
    #endregion

    #region ShootPlayer1 Function

    void ShootPlayer1()
    {
        Debug.Log("ShootPlayer1");
        Instantiate(bulletGo, firePointTransform.position, firePointTransform.rotation);
        StartCoroutine(ShootDelayPlayer1());
    }
    
    void ShootPlayer2()
    {
        Debug.Log("ShootPlayer2");
        Instantiate(bulletGo, firePointTransform.position, firePointTransform.rotation);
        StartCoroutine(ShootDelayPlayer2());
    }
    void OnDpadUpButton()
    {
        switch (_input.jc_ind)
        {
            case 1:
                if (!_canShootPlayer2)
                {
                    ShootPlayer2();
                }
                break;

        }
    }
    
    void OnDpadDownButton()
    {
        switch (_input.jc_ind)
        {
            case 0:
                if (!_canShootPlayer1)
                {
                    ShootPlayer1();
                }
                break;
            
        }
    }


    IEnumerator ShootDelayPlayer1()
    {
        yield return new WaitForSeconds(delayShoot);
        _canShootPlayer1 = false;
    }
    
    IEnumerator ShootDelayPlayer2()
    {
        yield return new WaitForSeconds(delayShoot);
        _canShootPlayer2 = false;
    }

    #endregion

  

}
