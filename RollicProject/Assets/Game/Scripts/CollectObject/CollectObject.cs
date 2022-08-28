using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour,IPlatformCollect,IPlatformCollision
{
    private Rigidbody _rb;
    [SerializeField]private bool calculateControl = false;
    private bool collidercontrol = false;

    public void DoPlatformCollect()
    {
        calculateControl = true;
        if (_rb!=null)
        {
            _rb.AddForce(new Vector3(-1, -5,0) * 800f);
        }
    }

    public void DoPlatformCollision(PlatformCalculator platformCalculator)
    {
        if (!collidercontrol)
        {
            platformCalculator.MyCount++;
            collidercontrol = true;
        }
        calculateControl = true;
        //gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject,1f);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.localPosition.y > 0.35f && !calculateControl)
        {
            _rb.MovePosition(new Vector3(transform.position.x, -0.65f, transform.position.z));
            //transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
        }
    }


}
