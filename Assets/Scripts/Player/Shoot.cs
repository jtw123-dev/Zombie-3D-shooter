using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject  _bloodSplatterPrefab;
    private int _layerMask = 1 << 6;
    
    // Start is called before the first frame update
    void Start()
    {
        _layerMask = ~_layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }
    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin,out hitInfo,Mathf.Infinity,_layerMask,QueryTriggerInteraction.Ignore))
            {
                Debug.Log("You hit object " + hitInfo.collider.gameObject.name);
               // Health health = hitInfo.collider.GetComponentInParent<Health>();

                //if (health!=null)
                {
                   var cloneBlood = Instantiate(_bloodSplatterPrefab, hitInfo.point, Quaternion.FromToRotation(Vector3.forward,hitInfo.normal));
                   Destroy(cloneBlood, 0.5f); //make sure you do not destroy the prefab itself    
                   // h/ealth.Damage(10);
                }
            }
        }
    }
}
