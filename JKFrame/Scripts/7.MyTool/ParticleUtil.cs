using UnityEngine;

namespace JKFrame.MyTool
{
    public static class ParticleUtil
    {
        /// <summary>
        /// 获取自身和子节点的所有ParticleSystem，计算它们的单轮存在时间，选取最大的那个作为整体特效的单轮存在时间。
        /// </summary>
        /// <param name="transform">特效体自身，并包含他的递进子节点们</param>
        /// <returns>特效单轮存留时间，取所有ParticleSystem中最大的那一个</returns>
        public static float ParticleSystemLength(Transform transform)
        {
            ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>();

            float maxDuration = 0;

            foreach (ParticleSystem ps in particleSystems)
            {
                if (ps.emission.enabled)
                {
                    // if(ps.main.loop)
                    // {
                    //     return -1f;
                    // }
                    var duration = 0f;
                    var mainModule = ps.main;
                    if (ps.emission.rateOverTimeMultiplier <= 0)
                    {
                        duration = mainModule.startDelayMultiplier + mainModule.startLifetimeMultiplier;
                    }
                    else
                    {
                        duration = mainModule.startDelayMultiplier +
                                   Mathf.Max(mainModule.duration, mainModule.startLifetimeMultiplier);
                    }

                    if (duration > maxDuration)
                    {
                        maxDuration = duration;
                    }
                }
            }

            return maxDuration;
        }
    }
}