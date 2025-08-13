/*
 Created in Huang's indie game NuclearGear 
 *FileName:      HoverFade.cs
 *Author:        RunNHuang
 *Date:          2021/06/08 21:10:02
 *UnityVersion:  2020.3.11f1c1
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Plugins.JKFrame.Scripts._7.MyTool.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HoverFade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public CanvasGroup canvasGroup;
        public float hoverAlpha = 1;
        public float outerAlpha = 0.45f;
        public float speed = 5;

        // public GraphicRaycaster graphicRaycaster;

        private float _curAlpha;
        private bool _isHover;

        private void OnEnable()
        {
            // graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            canvasGroup = GetComponent<CanvasGroup>();
            _curAlpha = canvasGroup.alpha;
            
            //一打开的瞬间就要检测是否鼠标hover在面板上，因为刚生成的面板是没有机会触发pointerEnter或PointerExit的
            List<RaycastResult> results = new List<RaycastResult>();
            var eventData = new PointerEventData(EventSystem.current) {position = Mouse.current.position.ReadValue()};
            // graphicRaycaster.Raycast(eventData, results);
            EventSystem.current.RaycastAll(eventData, results);
            _isHover = results.Exists(o=> o.gameObject == gameObject);
        }

        private void Update()
        {
            if (_isHover)
            {
                if (Mathf.Approximately(_curAlpha, hoverAlpha)) return;
                //淡出
                _curAlpha = Mathf.Lerp(_curAlpha, hoverAlpha, Time.deltaTime * speed);
                canvasGroup.alpha = _curAlpha;
            }
            else
            {
                if (Mathf.Approximately(_curAlpha, outerAlpha)) return;
                //淡入
                _curAlpha = Mathf.Lerp(_curAlpha, outerAlpha, Time.deltaTime * speed);
                canvasGroup.alpha = _curAlpha;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHover = false;
        }
    }
}