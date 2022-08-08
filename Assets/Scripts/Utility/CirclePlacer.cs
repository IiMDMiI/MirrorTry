using System.Collections.Generic;
using UnityEngine;

public class CirclePlacer : MonoBehaviour
{
    [SerializeField] private float _radius;
    private void Start()
    {   
        List<Transform> children = new List<Transform>();
        children.AddRange(GetComponentsInChildren<Transform>());
        children.Remove(transform);

        var initialVector = Vector3.right;
        float angleStep = 360 / children.Count;
        for (int i = 0; i < children.Count; i++)
        {
            float angle = angleStep * i;
           
            var right = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle));
            var forward = new Vector3(-Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
            children[i].transform.position = (initialVector.x * right + initialVector.z * forward) * _radius + transform.position;
        }
    }   
}
