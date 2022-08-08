using System.Collections.Generic;
using UnityEngine;

public class SpheresMover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.015f;
    [SerializeField] private float _amplitude = 4;
    [SerializeField] private float offset = 4.71f;
    private List<MeshRenderer> _spheres = new List<MeshRenderer>();
    private List<Color> _colors = new List<Color>();
    private float _radius = 40;
    private float _t;
    private float _sinusStep;
    private Color _highestSphereGradientColor;
    public Color HighestSphereGradientColor => _highestSphereGradientColor;

    private void Awake()
    {
        _spheres.AddRange(GetComponentsInChildren<MeshRenderer>());
        LineupInCircleAndColorize();
        _sinusStep = (2 * Mathf.PI) / _spheres.Count;
    }
    private void Update() =>
        Move();

    private void LineupInCircleAndColorize()
    {
        var initialVector = Vector3.right;
        float angleStep = 360 / _spheres.Count;
        for (int i = 0; i < _spheres.Count; i++)
        {
            float angle = angleStep * i;

            Transform sphere = _spheres[i].transform;
            var right = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle));
            var forward = new Vector3(-Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
            sphere.position = (initialVector.x * right + initialVector.z * forward) * _radius;

            Material material = _spheres[i].material;
            material.color = ColorUtility.GetColorFromAngle(angle);
            _colors.Add(material.color);
        }
        _colors.Reverse();
    }
    private void Move()
    {
        float x = 0;
        for (int i = 0; i < _spheres.Count; i++)
        {
            Transform sphere = _spheres[i].transform;
            Vector3 pos = sphere.transform.position;
            _t = (_t > 1) ? (_t = 0) : (_t + _speed * Time.deltaTime);
            x = Mathf.Lerp(0, 2 * Mathf.PI, _t);
            float yPos = Mathf.Sin(x + _sinusStep * i);
            sphere.transform.position = new Vector3(pos.x, yPos * _amplitude + transform.position.y, pos.z);
        }
        _highestSphereGradientColor = GetGradientColor(x + offset);
    }
    private Color GetGradientColor(float x)
    {
        x = x % (2 * Mathf.PI);

        int _highestSphereIndex = (int)(x / _sinusStep);
        float t = Mathf.InverseLerp(0, _sinusStep, x % _sinusStep);

        if (_highestSphereIndex == _colors.Count - 1)
            return Color.Lerp(_colors[_colors.Count - 1], _colors[0], t);
        else
            return Color.Lerp(_colors[_highestSphereIndex], _colors[_highestSphereIndex + 1], t);
    }

    
}
