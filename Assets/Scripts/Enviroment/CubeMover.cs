using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private SpheresMover _spheresMover;
    private Material _material;
    private void Awake() => 
        _material = GetComponent<MeshRenderer>().material;

    private void Update()
    {
        transform.Rotate(_rotationDirection, Space.World);
        _material.color = _spheresMover.HighestSphereGradientColor;       
    }
   
}
