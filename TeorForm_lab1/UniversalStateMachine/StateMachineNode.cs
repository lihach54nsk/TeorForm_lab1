namespace TeorForm_lab1.UniversalStateMachine
{
    class StateMachineNode<T>
    {
        private readonly StateMachineTransaction<T>[] _transactions;
        private readonly int _stateAsDefault;

        public StateMachineNode(StateMachineTransaction<T>[] transactions, int stateAsDefault)
        {
            _transactions = transactions;
            _stateAsDefault = stateAsDefault;
        }

        public int GetNextStateNumber(T value)
        {
            foreach (var item in _transactions)
            {
                if(item.CheckRule(value))
                {
                    item.OnStateChange?.Invoke(value);
                    return item.NewStateNumber;
                }
            }

            return _stateAsDefault;
        }
    }
}
