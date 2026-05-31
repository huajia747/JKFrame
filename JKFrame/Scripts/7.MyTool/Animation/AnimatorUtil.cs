using UnityEngine;

namespace Plugins.JKFrame.Scripts._7.MyTool.Animation
{
    public static class AnimatorUtil
    {
        public static void ResetAllParametersToDefault(Animator animator)
        {
            // 手动遍历重置参数（性能开销取决于参数数量，但比 Rebind() 小）
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                switch (param.type)
                {
                    case AnimatorControllerParameterType.Float:
                        animator.SetFloat(param.nameHash, param.defaultFloat);
                        break;
                    case AnimatorControllerParameterType.Int:
                        animator.SetInteger(param.nameHash, param.defaultInt);
                        break;
                    case AnimatorControllerParameterType.Bool:
                        animator.SetBool(param.nameHash, param.defaultBool);
                        break;
                    case AnimatorControllerParameterType.Trigger:
                        animator.ResetTrigger(param.nameHash);
                        break;
                }
            }
        }
    }
}
