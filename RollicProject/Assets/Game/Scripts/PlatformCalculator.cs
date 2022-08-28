using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlatformCalculator : MonoBehaviour
{
    public float MyTargetCount;
    public float MyCount;
    public bool ContinueControl;
    [SerializeField] TMP_Text myText;
    [SerializeField] private BoxCollider myTriggerCollider;
    void Start()
    {
        TextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherObj = other.gameObject.GetComponent<IPlatformCollect>();
        if (otherObj!=null)
        {
            otherObj.DoPlatformCollect();
            StartCoroutine(RepatCollision(otherObj));
            GameManager.Instance.MyPlatformCalculateObject = this;
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        var collisionObj = collision.gameObject.GetComponent<IPlatformCollision>();
        if (collisionObj!=null)
        {
            collisionObj.DoPlatformCollision(this);
            TextUpdate();
        }
    }

    private void TextUpdate()
    {
        myText.text = MyCount.ToString() + "/" + MyTargetCount.ToString();
    }

    public void CompleteControl()
    {
        if (MyCount>=MyTargetCount)
        {
            ContinueControl = true;
        }
        else
        {
            ContinueControl = false;
        }
    }

    private IEnumerator RepatCollision(IPlatformCollect platformCollect)
    {
        if (GameManager.Instance.GameState==GameState.CALCULATE)
        {
            yield return new WaitForSeconds(0.05f);
            myTriggerCollider.size = new Vector3(1.7f, 2, 4);
            platformCollect.DoPlatformCollect();
        }
        

    }


}
