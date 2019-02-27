using System;

namespace TeorForm_lab1.UniversalStateMachine
{
    struct StateMachineTransaction<T>
    {
        public StateMachineTransaction(Predicate<T> checkRule, Action<T> onStateChange, int newStateNumber) : this()
        {
            CheckRule = checkRule;
            OnStateChange = onStateChange;
            NewStateNumber = newStateNumber;
        }

        public Predicate<T> CheckRule { get; }
        public Action<T> OnStateChange { get; }
        public int NewStateNumber { get; }
    }
}
