using UnityEngine;
using UnityEngine.Events;

public class AdditionalTurnManager
{
        private static AdditionalTurnManager _i;

        public static AdditionalTurnManager i
        {
            get
            {
                if (_i == null)
                {
                    _i = new AdditionalTurnManager();
                }
                return _i;
            }
        }
        private AdditionalTurnManager() { }

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
