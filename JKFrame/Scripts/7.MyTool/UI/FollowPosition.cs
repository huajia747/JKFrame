/*
 Created in Huang's indie game NuclearGear 
 *FileName:      FollowPosition.cs
 *Author:        RunNHuang
 *Date:          2021/08/21 12:08:26
 *UnityVersion:  2020.3.15f1c1
 */

using UnityEngine;

namespace Plugins.JKFrame.Scripts._7.MyTool.UI
{
    public class FollowPosition : MonoBehaviour
    {
        public Transform targetTransform; //要跟随的对象的transform，如果存在，优先跟随这个
        public Vector3 targetPos; //要跟随的Position，比上面的transform省一点运算

        private RectTransform _canvasTransform;
        private Camera _worldCamera;
        private RectTransform _rectTransform;

        private void Start()
        {
            _canvasTransform = GetComponentInParent<Canvas>().transform as RectTransform;
            _worldCamera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            var screenPos = CalculateScreenPos(_canvasTransform, _worldCamera,
                targetTransform ? targetTransform.position : targetPos);
            _rectTransform.anchoredPosition = screenPos;
        }

        private static Vector2 CalculateScreenPos(RectTransform canvasRect, Camera worldCamera, Vector3 worldPos)
        {
            var viewportPos3d = worldCamera.WorldToViewportPoint(worldPos);
            var viewPortRelative = new Vector2(viewportPos3d.x - 0.5f, viewportPos3d.y - 0.5f);
            var screenSize = canvasRect.sizeDelta;

            return viewPortRelative * screenSize;
        }
    }
}