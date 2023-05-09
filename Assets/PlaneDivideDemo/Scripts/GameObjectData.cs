using UnityEngine;

public class GameObjectData
{
    public int uid;
    public string resPath;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Vector3 size;

    public GameObjectData(string _resPath,Vector3 _position, Quaternion _rotation,Vector3 _scale,Vector3 _size)
    {
        resPath = _resPath;
        position = _position;
        rotation = _rotation;
        scale = _scale;
        size = _size;
    }

    public Bounds GetBounds()
    {
        return new Bounds(position, new Vector3(scale.x * size.x, scale.y * size.y, scale.z * size.z));
    }
}
