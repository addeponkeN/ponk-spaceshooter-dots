
using UnityEngine;

public class CameraSingleton : MonoBehaviour
{
    public static CameraSingleton Get;

    [SerializeField] private float _startRadius;
    [SerializeField] private float _endRadius;
    [SerializeField] private float _startHeight;
    [SerializeField] private float _endHeight;
    [SerializeField] private float _speed;

    public float RadiusAtScale(float scale) => Mathf.Lerp(_startRadius, _endRadius, scale);
    public float HeightAtScale(float scale) => Mathf.Lerp(_startHeight, _endHeight, scale);
    public float Speed => _speed;

    private void Awake()
    {
        if(Get != null)
        {
            Destroy(gameObject);
            return;
        }

        Get = this;
    }
}