/*
 Created in Huang's indie game NuclearGear 
 *FileName:      LookAtMouse.cs
 *Author:        RunNHuang
 *Date:          2021/06/08 12:18:47
 *UnityVersion:  2020.3.11f1c1
 */

using UnityEngine;
using UnityEngine.InputSystem;

namespace Plugins.JKFrame.Scripts._7.MyTool.UI
{
    public class LookAtMouse : MonoBehaviour
    {
        [Space]
        public float zOffset = 1000;
        public float speed = 5;

        private Vector3 _curAimPos;

        private void OnEnable()
        {
            var selfTransform = transform;
            _curAimPos = selfTransform.position + selfTransform.forward * zOffset;
        }

        private void Update()
        {
            //获得鼠标位置，计算朝向点
            //var targetPos = new Vector3(Screen.width-inputReader.MousePosition.x, Screen.height-inputReader.MousePosition.y, zOffset) + transform.position;
            var mousePos = Mouse.current.position.ReadValue();
            var targetPos = new Vector3(0.5f * Screen.width - mousePos.x, 0.5f * Screen.height -mousePos.y, zOffset) + transform.position;
            _curAimPos = Vector3.Lerp(_curAimPos, targetPos, Time.deltaTime * speed);

            //旋转ui
            transform.LookAt(_curAimPos);
        }
    }
}