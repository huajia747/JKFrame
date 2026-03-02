using UnityEngine;

namespace Plugins.JKFrame.Scripts._7.MyTool.Animation
{
    public class StateWaitTime : StateMachineBehaviour
    {
        [SerializeField] private Vector2 stayTimeRange = new Vector2(0, 1);
        [SerializeField] private string nextStateName;
        [SerializeField] private float transitionDuration = 0.2f;

        private bool _bStopRunning;
        private float _timer;
        
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _timer =  Random.Range(stayTimeRange.x, stayTimeRange.y);
            _bStopRunning = false;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_bStopRunning) return;
            
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _bStopRunning = true;
                animator.CrossFadeInFixedTime(nextStateName, transitionDuration);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        // public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        //
        // }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
