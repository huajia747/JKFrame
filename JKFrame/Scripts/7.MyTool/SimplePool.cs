using System.Text;
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
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        public static GameObject GetGameObject(GameObject gameObjectPrefab, Vector3 inPosition, Quaternion inRotation, string idPrefix = "")
        {
            string goName;
            
            if (string.IsNullOrEmpty(idPrefix))
            {
                goName = gameObjectPrefab.name;
            }
            else
            {
                StringBuilder.Clear();
                StringBuilder.AppendFormat("{0}_{1}", idPrefix, gameObjectPrefab.name);
                goName = StringBuilder.ToString();
            }
            
            var go = PoolSystem.GetGameObject(goName);
            if (go == null)
            {
                go = GameObject.Instantiate(gameObjectPrefab, inPosition, inRotation);
                go.name = goName;
            }
            else
            {
                go.transform.position = inPosition;
                go.transform.rotation = inRotation;
            }
            return go;
        }

        public static T GetGameObject<T>(GameObject gameObjectPrefab, Vector3 inPosition, Quaternion inRotation, string idPrefix = "")
            where T : Component
        {
            var go = GetGameObject(gameObjectPrefab, inPosition, inRotation, idPrefix);
            return go.GetComponent<T>();
        }
        
        public static GameObject GetGameObject(GameObject gameObjectPrefab, Transform inParent, string idPrefix = "")
        {
            string goName;
            
            if (string.IsNullOrEmpty(idPrefix))
            {
                goName = gameObjectPrefab.name;
            }
            else
            {
                StringBuilder.Clear();
                StringBuilder.AppendFormat("{0}_{1}", idPrefix, gameObjectPrefab.name);
                goName = StringBuilder.ToString();
            }
            
            var go = PoolSystem.GetGameObject(goName, inParent);
            if (go == null)
            {
                go = GameObject.Instantiate(gameObjectPrefab, inParent);
                go.name = goName;
            }

            return go;
        }

        public static T GetGameObject<T>(GameObject gameObjectPrefab, Transform inParent, string idPrefix = "")
        {
            var go = GetGameObject(gameObjectPrefab, inParent, idPrefix);
            return go.GetComponent<T>();
        }
    }
}
