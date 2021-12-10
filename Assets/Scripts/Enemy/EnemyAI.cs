using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField] private EnemyState _currentState = EnemyState.Chase;

    private CharacterController _char;
    [SerializeField] Transform _target;
    [SerializeField]private float _speed;
    private Vector3 _velocity;
    [SerializeField] private float _gravity;
    private Player _player;
    //private Health _playerHealth;
    [SerializeField]private float _attackDelay = 1.5f;
    private float _nextAttack = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        _char = GetComponent<CharacterController>();
        _player = GameObject.Find("Player").GetComponent<Player>();
      //  _playerHealth = _player.GetComponent<Health>();

         //if (_playerHealth ==null)            
        {
            Debug.LogError("player or playerhealth is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (_currentState ==EnemyState.Attack)
        {          
            if (Time.time>_nextAttack)
            {
               // _playerHealth.Damage(10);
                _nextAttack = Time.time + _attackDelay;
            }
              
        }
    }
    private void CalculateMovement()
    {
        if (_char.isGrounded == true)
        {
            if (_currentState==EnemyState.Chase)
            {
                Vector3 direction = _target.position - transform.position;
                direction.Normalize();
                _velocity = direction * _speed;
                direction.y = 0;
                transform.localRotation = Quaternion.LookRotation(direction);
            }
            else
            {
                return;
            }          
        }
        _velocity.y -= _gravity;
        _char.Move(_velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            _currentState = EnemyState.Attack;
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            _currentState = EnemyState.Chase;
        }
    }
}
/* private void OnTriggerExit(Collider other)
   {
       if (other.tag =="Player")
       {
           _isAttacking = false;
       }      
   }*/


/*private IEnumerator OnTriggerStay(Collider other)
   {
       Health health = other.GetComponent<Health>();
       if (other.tag=="Player")
       {
           if (_attackDelay == false)
           {
               _attackDelay = true;
               health.Damage(1);       
           }
           else
           {
               yield return new WaitForSeconds(2);
               _attackDelay = false;
           }                        
       }      
   }*/