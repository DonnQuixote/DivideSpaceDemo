using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    Bounds bound { get; set; }
    /// <summary>
    /// ��ʼ������һ����������
    /// </summary>
    /// <param name="obj"></param>
    void InsertObjData(GameObjectData obj);
    /// <summary>
    /// �������ߣ����ǣ��ڸýڵ���ʱ��ʾ����
    /// </summary>
    /// <param name="camera"></param>
    void Inside(Camera camera);
    /// <summary>
    /// �������ߣ����ǣ����ڸýڵ���ʱ��������
    /// </summary>
    /// <param name="camera"></param>
    void Outside(Camera camera);
    void DrawBound();
}
