using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabable
{
    GrabableObject Grab();
}

public class GrabableObject
{
    public string name;
    
    public GameObject gameObject;
    public Rigidbody rigidbody;
    public Collider collider;

    public GrabableObject(string name,GameObject gameObject, Collider collider, Rigidbody rigidbody)
    {
        this.name = name;
        this.gameObject = gameObject;
        this.collider = collider;
        this.rigidbody = rigidbody;
        ChangeLayerRecursively(gameObject.transform, "HoldLayer");
    }
    public void ChangeLayerRecursively(Transform parent, string layerName)
    {
        parent.gameObject.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in parent)
        {
            ChangeLayerRecursively(child, layerName);
        }
    }
}