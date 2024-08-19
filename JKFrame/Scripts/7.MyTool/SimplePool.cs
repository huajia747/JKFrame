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

        /// <summary>
        /// 根据物体的名字和传入的前缀，得到实例化后将作为Pool的key的名字。
        /// 做成Public是因为，在其他系统里会用到。比如目前特效系统，要根据特效预制体判断是否场上有已经存在的特效。
        /// </summary>
        /// <param name="gameObjectPrefab">要计算名字的物体</param>
        /// <param name="idPrefix">算Key时会加上的前缀</param>
        /// <returns>Pool中使用的KeyName</returns>
        public static string GetKeyName(GameObject gameObjectPrefab, string idPrefix = "")
        {
            string keyName;
            if (string.IsNullOrEmpty(idPrefix))
            {
                keyName = gameObjectPrefab.name;
            }
            else
            {
                StringBuilder.Clear();
                StringBuilder.AppendFormat("{0}_{1}", idPrefix, gameObjectPrefab.name);
                keyName = StringBuilder.ToString();
            }

            return keyName;
        }
        
        public static GameObject GetGameObject(GameObject gameObjectPrefab, Vector3 inPosition, Quaternion inRotation, string idPrefix = "")
        {
            var goName = GetKeyName(gameObjectPrefab, idPrefix);
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
            var goName = GetKeyName(gameObjectPrefab, idPrefix);
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
