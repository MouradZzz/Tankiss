using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("Tank Variable")]
    [Space]
    public float speedTanks;
    [Space] 
    public float rotationSpeed;


    #region Private Reference

    private Rigidbody _rb;
    
    private Renderer _renderer;
    
    private JoyconDemo _inputJoycon;

    #endregion

    #region Private Move Variable

    private Vector3 _moveDir;
    
    #endregion

    #region Unity CallBack Function

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _renderer = GetComponent<Renderer>();

        _inputJoycon = GetComponent<JoyconDemo>();

        switch (_inputJoycon.jc_ind)
        {
            case 0:
                _renderer.material.color = Color.yellow;
                break;
            
            case 1:
                _renderer.material.color = Color.green;
                break;
                
        }
    }
    

    private void FixedUpdate()
    {
        switch (_inputJoycon.jc_ind)
        {
            case 0:
                Move();
                break;
            
            case 1:
                Move1();
                break;
        }
    }

    #endregion

    #region Move Function

    void Move()
    {
        _moveDir = new Vector3(_inputJoycon.stick[1], _rb.velocity.y, -_inputJoycon.stick[0]);
        
        _moveDir.Normalize();

        _rb.velocity = _moveDir * speedTanks;
        
        AimDirection();
    }
    
    void Move1()
    {
        _moveDir = new Vector3(-_inputJoycon.stick[1], _rb.velocity.y, _inputJoycon.stick[0]);
        
        _moveDir.Normalize();

        _rb.velocity = _moveDir * speedTanks;
        
        AimDirection();
    }


    void AimDirection()
    {
        if (_moveDir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(_moveDir, Vector3.up);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation, rotationSpeed);
        }
    }


    #endregion

}
