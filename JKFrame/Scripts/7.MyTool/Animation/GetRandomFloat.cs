using UnityEngine;

namespace Plugins.JKFrame.Scripts._7.MyTool.Animation
{
    public class GetRandomFloat : StateMachineBehaviour
    {
        [SerializeField] private string paramName;
        [SerializeField] private Vector2 range;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var randomValue = Random.Range(range.x, range.y);
            animator.SetFloat(paramName, randomValue);
        }
    }
}
