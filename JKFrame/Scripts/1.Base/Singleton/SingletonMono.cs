using UnityEngine;

namespace JKFrame
{
    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (FindObjectsByType<T>(FindObjectsSortMode.None).Length > 1)
                    {
                        Debug.LogWarning("单例物体重复存在：<color=#6cd6cd>" + _instance.name);
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        var instanceName = typeof(T).Name;
                        var instanceObj = GameObject.Find(instanceName);
                        if (!instanceObj)
                        {
                            instanceObj = new GameObject(instanceName);
                            instanceObj.AddComponent<T>();
                            // DontDestroyOnLoad(instanceObj);
                        }
                    }
                }
                return _instance;
            }
        }

        public static bool IsCreated()
        {
            return _instance is not null;
        }

        public static bool NotCreated()
        {
            return _instance == null;
        }

        protected virtual void Awake()
        {
            //在切换场景、加载场景时，当新场景中检测到之前存在同类单例时，把自己销毁
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this as T;
        }
    }
}

