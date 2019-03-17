using System.Collections.Generic;

namespace TeorForm_lab1.UniversalStateMachine
{
    class UniversalStateMachine<T>
    {
        private readonly StateMachineNode<T>[] _nodes;
        private int _currentState;

#if DEBUG
        private List<int> traceData = new List<int>();
        public IReadOnlyList<int> TraceData => traceData;
#endif

        public UniversalStateMachine(StateMachineNode<T>[] nodes)
        {
            _nodes = nodes;
            _currentState = 0;
        }

        public void PutSymbol(T item)
        {
#if DEBUG
            traceData.Add(_currentState);
#endif
            _currentState = _nodes[_currentState].GetNextStateNumber(item);
        }
    }
}