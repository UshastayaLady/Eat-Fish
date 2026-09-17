using System.Collections;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float _speedMove;
    [SerializeField] private float _rotationTime;
    private float _rotationVelocity;
    private float _angle;
    private float _targetAngle;

    private void OnEnable()
    {
        StartCoroutine("RandRotation");
    }

    void Update()
    {
        Move();
        Rotation();
    }

    private void Move()
    {

        transform.Translate(Vector3.forward * _speedMove * Time.deltaTime);

    }

    private void Rotation()
    {

        _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _rotationVelocity, _rotationTime);
        transform.rotation = Quaternion.Euler(0, _angle, 0);

    }

    private IEnumerator RandRotation()
    {
        while (enabled)
        {
            _targetAngle = Random.Range(0f, 360f);
            _rotationTime = Random.Range(1,4);            
            yield return new WaitForSeconds(Random.Range(3f, 10f));
        }       
    }

    private void OnDisable()
    {
        StopCoroutine("RandRotation");
    }
}
