using UnityEngine;

namespace JKFrame.MyTool
{
    public class PushPoolInTime : MonoBehaviour
    {
        public bool autoCalculate; // 目前只是是为了粒子特效，自动计算所有粒子都播放完成的时间
        public float lifetime = 1;

        private float _timer;

        private void Start()
        {
            if (autoCalculate)
            {
                if (gameObject.GetComponentInChildren<ParticleSystem>())
                {
                    //todo 每实例化一个Particle，都要算一遍，可以节省。考虑让所有的同类物体的delayLife共用一个。
                    lifetime = ParticleUtil.ParticleSystemLength(transform);
                }
                autoCalculate = false; // 只在第一次实例化出来的时候计算，之后从池中拿出来不需要再算了。
            }
            _timer = lifetime;
        }

        private void OnEnable()
        {
            _timer = lifetime;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                gameObject.GameObjectPushPool();
            }
        }
    }
}