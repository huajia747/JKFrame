/*
 Created in Huang's indie game NuclearGear 
 *FileName:      PanelShowAnimRecall.cs
 *Author:        RunNHuang
 *Date:          2021/06/08 22:42:06
 *UnityVersion:  2020.3.11f1c1
 */

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.JKFrame.Scripts._7.MyTool.UI
{
    public class PanelSoloAnimRecall : MonoBehaviour
    {
        public Animator animShow;
        [LabelText("动画时关闭的组件")] public Behaviour[] componentsDisableInAnimation;

        private void Start()
        {
            if (!animShow)
                animShow = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            OnSoloAnimStart();
        }

        public void OnSoloAnimStart()
        {
            if (animShow)
                animShow.enabled = true;

            foreach (var behaviour in componentsDisableInAnimation)
            {
                if (behaviour)
                    behaviour.enabled = false;
            }
        }

        public void OnSoloAnimFinish()
        {
            if (animShow)
                animShow.enabled = false;
            
            foreach (var behaviour in componentsDisableInAnimation)
            {
                if (behaviour)
                    behaviour.enabled = true;
            }
        }
    }
}