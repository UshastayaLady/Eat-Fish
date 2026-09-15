using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private PlayerMove _player;
    [SerializeField] private float _offset;

    void LateUpdate()
    {
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z + _offset);
    }
}
