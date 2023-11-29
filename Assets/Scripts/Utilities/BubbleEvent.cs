using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Utilities
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack = () => { };
        public event UnityAction OnBubbleCompleteAttack = () => { };
        public event UnityAction OnBubbleHit = () => { };
        private void OnStartAttack()
        {
            OnBubbleStartAttack.Invoke();
        }

        private void OnCompleteAttack()
        {
            OnBubbleCompleteAttack.Invoke();
        }

        private void OnHit()
        {
            OnBubbleHit.Invoke();
        }
    }
}
