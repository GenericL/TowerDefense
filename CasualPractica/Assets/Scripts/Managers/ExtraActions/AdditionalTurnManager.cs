using UnityEngine;
using UnityEngine.Events;

public class AdditionalTurnManager
{

        private event UnityAction additionalTurnQueue;
        public event UnityAction AdditionalTurnQueue
        {
            add { additionalTurnQueue += value; }
            remove { additionalTurnQueue -= value; }
        }
        public void Invoke()
        {
            additionalTurnQueue?.Invoke();
        }
}
