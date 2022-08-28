using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private void LateUpdate()
    {
        transform.position = new Vector3(_player.transform.position.x + 5, transform.position.y, transform.position.z);

    }
}
