using System.Collections.Generic;
using JKFrame;
using JKFrame.MyTool;
using UnityEngine;
using UnityEngine.Playables;

public class FxManager : SingletonMono<FxManager>
{
    private void InternalPlayFx(GameObject fxObj, bool autoPoolBack)
    {
        var playableTime = -1.0f;
        // 如果有timeline要播放
        if (fxObj.TryGetComponent(out PlayableDirector p))
        {
            p.Play();
            playableTime = (float)p.duration;
        }
        if (autoPoolBack && !fxObj.TryGetComponent(out PushPoolInTime pushPool))
        {
            var newPushPool = fxObj.AddComponent<PushPoolInTime>();
            if (playableTime > 0)
            {
                // 如果有Timeline的话，以Timeline时长为准，因为会隐藏某些子节点，导致计算粒子时长不对。
                newPushPool.lifetime = playableTime;
                newPushPool.autoCalculate = false;
            }
            else
            {
                newPushPool.autoCalculate = true;
            }
        }
    }
    
    public GameObject PlayFx(GameObject fxPrefab, Transform parentTransform, bool autoPoolBack, string idPrefix = "")
    {
        var fxObj = SimplePool.GetGameObject(fxPrefab, parentTransform, idPrefix);

        InternalPlayFx(fxObj, autoPoolBack);

        return fxObj;
    }
    public GameObject PlayFx(GameObject fxPrefab, Vector3 position, Quaternion rotation, bool autoPoolBack, string idPrefix = "")
    {
        var fxObj = SimplePool.GetGameObject(fxPrefab, position, rotation, idPrefix);
        
        InternalPlayFx(fxObj, autoPoolBack);

        return fxObj;
    }

    private Dictionary<string, GameObject> _singletonFxRecord = new Dictionary<string, GameObject>(); // 所有单例的特效，都要记录下来，下次播放单例特效时，先检测所有记录中是否有正在播放的。
    
    public GameObject PlaySingletonFx(GameObject fxPrefab, Vector3 position, Quaternion rotation, bool autoPoolBack, string idPrefix = "")
    {
        // 先从所有记录的单例特效中，检测是否有同一个特效
        var fxKeyName = SimplePool.GetKeyName(fxPrefab, idPrefix);
        if (_singletonFxRecord.ContainsKey(fxKeyName)) // 记录中已经有此单例特效
        {
            if (_singletonFxRecord[fxKeyName].activeSelf) // 次单例特效激活着，没被回收
            {
                _singletonFxRecord[fxKeyName].GameObjectPushPool(); // 回收它
            }

            _singletonFxRecord.Remove(fxKeyName); // 清楚记录，为之后播新的，加入新的做准备。
        }

        var fxGo = PlayFx(fxPrefab, position, rotation, autoPoolBack, idPrefix);
        
        // 新播放的特效记录进单例记录中
        _singletonFxRecord.Add(fxKeyName, fxGo);

        return fxGo;
    }

    public void StopSingletonFx(GameObject fxPrefab, string idPrefix = "")
    {
        var fxKeyName = SimplePool.GetKeyName(fxPrefab, idPrefix);
        if (_singletonFxRecord.ContainsKey(fxKeyName)) // 记录中已经有此单例特效
        {
            if (_singletonFxRecord[fxKeyName].activeSelf) // 次单例特效激活着，没被回收
            {
                _singletonFxRecord[fxKeyName].GameObjectPushPool(); // 回收它
            }

            _singletonFxRecord.Remove(fxKeyName); // 清楚记录，为之后播新的，加入新的做准备。
        }
    }
}
