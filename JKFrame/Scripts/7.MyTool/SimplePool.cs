using UnityEngine;

namespace JKFrame.MyTool
{
    public static class SimplePool
    {
        // 由于一般实例化物体都要设置位置，所以这个应该不常用，注释掉。
        // public static GameObject GetGameObject(GameObject gameObjectPrefab)
        // {
        //     var go = PoolSystem.GetGameObject(gameObjectPrefab.name);
        //     if (go == null)
        //     {
        //         go = GameObject.Instantiate(gameObjectPrefab);
        //         go.name = gameObjectPrefab.name;
        //     }
        //     return go;
        // }

        public static GameObject GetGameObject(GameObject gameObjectPrefab, Vector3 inPosition, Quaternion inRotation)
        {
            var go = PoolSystem.GetGameObject(gameObjectPrefab.name);
            if (go == null)
            {
                go = GameObject.Instantiate(gameObjectPrefab, inPosition, inRotation);
                go.name = gameObjectPrefab.name;
            }
            else
            {
                go.transform.position = inPosition;
                go.transform.rotation = inRotation;
            }
            return go;
        }

        public static T GetGameObject<T>(GameObject gameObjectPrefab, Vector3 inPosition, Quaternion inRotation)
            where T : Component
        {
            var go = GetGameObject(gameObjectPrefab, inPosition, inRotation);
            return go.GetComponent<T>();
        }
        
        public static GameObject GetGameObject(GameObject gameObjectPrefab, Transform inParent)
        {
            var go = PoolSystem.GetGameObject(gameObjectPrefab.name, inParent);
            if (go == null)
            {
                go = GameObject.Instantiate(gameObjectPrefab, inParent);
                go.name = gameObjectPrefab.name;
            }

            return go;
        }

        public static T GetGameObject<T>(GameObject gameObjectPrefab, Transform inParent)
        {
            var go = GetGameObject(gameObjectPrefab, inParent);
            return go.GetComponent<T>();
        }
    }
}
