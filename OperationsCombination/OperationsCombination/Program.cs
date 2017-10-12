using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsCombination
{
    public class OperationSequenceFinder
    {
        private readonly int[] _numbers;
        private readonly int _target;
        private string operationSequence;

        public OperationSequenceFinder(int[] numbers, int target)
        {
            this._numbers = new int[numbers.Length];
            Array.Copy(numbers, this._numbers, numbers.Length);
            this._target = target;
            operationSequence = null;
        }

        public string FindOperationSequence()
        {
            if (operationSequence != null)
                return operationSequence;

            var operationStack = new LinkedList<List<Operation>>();
            foreach (var num in this._numbers)
                operationStack.AddFirst(new List<Operation> { new Operation(num) });

            while (operationStack.Count != 0)
            {
                var currentOperations = operationStack.First.Value;
                operationStack.RemoveFirst();

                for (int i = 0; i < currentOperations.Count; i++)
                {
                    var operation = currentOperations[i];
                    if (operation.Result == this._target)
                        return operation.ToString();
                    for (int j = i + 1; j < currentOperations.Count; j++)
                    {
                        var newOperations = applyOperation(i, j, Operator.Add, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(i, j, Operator.Subtract, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(j, i, Operator.Subtract, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(i, j, Operator.Multiply, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(i, j, Operator.DivideBy, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(j, i, Operator.DivideBy, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);
                    }
                }
            }
            return null;
        }

        private List<Operation> applyOperation(int left, int right, Operator _operator, List<Operation> operations)
        {
            if (!Operation.TryApplyOperator(operations[left], operations[right], _operator,
                out var newOperation))
                return null;
            var newOperations = new List<Operation>();
            newOperations.Add(newOperation);
            for (int i = 0; i < operations.Count; i++)
            {
                if (i == left || i == right)
                    continue;
                ; newOperations.Add(operations[i]);
            }
            return newOperations;
        }


        public enum Operator
        {
            Multiply,
            Add,
            Subtract,
            DivideBy
        }

        public static string ToString2(Operator _operator)
        {
            switch (_operator)
            {
                case Operator.Add:
                    return "+";
                case Operator.DivideBy:
                    return "/";
                case Operator.Multiply:
                    return "*";
                case Operator.Subtract:
                    return "-";
            }
            throw new InvalidOperationException();
        }

        public class Operation
        {
            public Operation LeftOperand { get; private set; }
            public Operation RightOperand { get; private set; }
            public Operator Operator { get; private set; }
            public int Result { get; private set; }

            public Operation(int number)
            {
                this.Result = number;
            }

            private Operation()
            {
            }

            public bool Equals(Operation other)
            {
                return this.Result == ((Operation)other).Result;
            }

            public static bool TryApplyOperator(Operation _rightOperand, Operation _leftOperand,
                Operator _operator, out Operation _result)
            {
                double result = 0;
                switch (_operator)
                {
                    case Operator.Add:
                        result = _rightOperand.Result + _leftOperand.Result;
                        break;
                    case Operator.Multiply:
                        result = _rightOperand.Result * _leftOperand.Result;
                        break;
                    case Operator.DivideBy:
                        result = (double)_rightOperand.Result / _leftOperand.Result;
                        break;
                    case Operator.Subtract:
                        result = _rightOperand.Result - _leftOperand.Result;
                        break;
                }
                if (result < 0 || result != (int)result)
                {
                    _result = null;
                    return false;
                }
                _result = new Operation()
                {
                    LeftOperand = _leftOperand,
                    RightOperand = _rightOperand,
                    Operator = _operator,
                    Result = (int)result
                };
                return true;
            }

            public override string ToString()
            {
                return ToString(this);
            }

            private string ToString(Operation operation)
            {
                if (RightOperand == null || LeftOperand == null)
                    return operation.Result.ToString();
                return ($"({ToString(operation.LeftOperand)} {ToString2(operation.Operator)} {ToString(operation.RightOperand)})");
            }
        }

        static void Main(string[] args)
        {
        }

    }
}

