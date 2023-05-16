using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameObjectsManager : MonoBehaviour
{
    public static GameObjectsManager Instance { private set; get; }
    private List<int> unLoadUids = new List<int>();
    private Dictionary<int, SceneGameObjectData> activeSceneGameObjectDatas = new Dictionary<int, SceneGameObjectData>();
    public Transform gameObjectsManager;
    public enum SceneGameObjectsStatus
    {
        Loading,
        Loaded
    }
    private void Awake()
    {
        Instance = this;
    }
    public void LoadAsync(GameObjectData gameObjectData)
    {
        if (activeSceneGameObjectDatas.ContainsKey(gameObjectData.uid)) return;

        StartCoroutine(LoadGameObjects(gameObjectData));
    }

    private IEnumerator LoadGameObjects(GameObjectData gameObjectData)
    {
        SceneGameObjectData sceneGameObjectData = new SceneGameObjectData(gameObjectData);
        sceneGameObjectData.status = SceneGameObjectsStatus.Loading;
        activeSceneGameObjectDatas.Add(gameObjectData.uid, sceneGameObjectData);
        GameObject resourceObj = null;
        ResourceRequest request = Resources.LoadAsync<GameObject>(gameObjectData.resPath);
        yield return request;

        resourceObj = request.asset as GameObject;
        yield return new WaitUntil(() => resourceObj != null);

        sceneGameObjectData.status = SceneGameObjectsStatus.Loaded;
        SetGameObjectTransform(resourceObj, sceneGameObjectData);
    }

    private void SetGameObjectTransform(GameObject prefab,SceneGameObjectData sceneGameObjectData)
    {
        sceneGameObjectData.gameObject = Instantiate(prefab);
        sceneGameObjectData.gameObject.transform.position = sceneGameObjectData.gameObjectData.position;
        sceneGameObjectData.gameObject.transform.rotation = sceneGameObjectData.gameObjectData.rotation;
        sceneGameObjectData.gameObject.transform.parent = gameObjectsManager;
        //sceneGameObjectData.gameObject.layer = 6;
        sceneGameObjectData.gameObject.transform.localScale = sceneGameObjectData.gameObjectData.scale;

    }

    public void UnLoad(int uid)
    {
        if (activeSceneGameObjectDatas.ContainsKey(uid) && unLoadUids.Contains(uid) == false)
        {
            unLoadUids.Add(uid);
        }
        for(int i = 0; i < unLoadUids.Count; i++)
        {
            if(activeSceneGameObjectDatas[unLoadUids[i]].status == SceneGameObjectsStatus.Loaded)
            {
                //activeSceneGameObjectDatas[unLoadUids[i]].gameObject.layer = 7;
                Destroy(activeSceneGameObjectDatas[unLoadUids[i]].gameObject);
                activeSceneGameObjectDatas.Remove(unLoadUids[i]);
                unLoadUids.RemoveAt(i);
            }
        }
    }

    public class SceneGameObjectData
    {
        public GameObjectData gameObjectData;
        public SceneGameObjectsStatus status;
        public GameObject gameObject;

        public SceneGameObjectData(GameObjectData _gameObjectData)
        {
            gameObjectData = _gameObjectData;
            gameObject = null;
        }
    }

}
