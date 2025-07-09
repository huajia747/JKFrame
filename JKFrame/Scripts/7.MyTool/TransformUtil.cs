/*
 Created in Huang's indie game NuclearGear 
 *FileName:      ClearTransformChildren.cs
 *Author:        RunNHuang
 *Date:          2021/05/30 22:34:29
 *UnityVersion:  2020.3.10f1c1
 */

using UnityEngine;

namespace JKFrame.MyTool
{
    public class TransformUtil : MonoBehaviour
    {
        public static void ClearChildren(Transform parent)
        {
            var childCount = parent.childCount;
            if (childCount == 0) return;

            for (int i = childCount - 1; i >= 0; i--)
            {
                if (Application.isPlaying)
                {
                    Destroy(parent.GetChild(i).gameObject);
                }
                else
                {
                    DestroyImmediate(parent.GetChild(i).gameObject);
                }
            }
            parent.DetachChildren();
        }
    }

    public static class TransformUtilExtension
    {
        public static void ClearChildren(this Transform transform)
        {
            TransformUtil.ClearChildren(transform);
        }
    }
}