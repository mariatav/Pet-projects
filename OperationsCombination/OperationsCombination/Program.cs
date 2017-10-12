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

            this.operationSequence = "";

            var operationStack = new LinkedList<List<Operation>>();
            operationStack.AddFirst(_numbers.Select(x => new Operation(x)).ToList());
            while (operationStack.Count != 0)
            {
                var currentOperations = operationStack.First.Value;
                operationStack.RemoveFirst();

                for (int i = 0; i < currentOperations.Count; i++)
                {
                    var operation = currentOperations[i];
                    if (operation.Result == this._target)
                    {
                        this.operationSequence = operation.ToString();
                        return this.operationSequence;
                    }
                    for (int j = i + 1; j < currentOperations.Count; j++)
                    {
                        var newOperations = applyOperation(i, j, Operation.Operators.Add, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(i, j, Operation.Operators.Subtract, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(j, i, Operation.Operators.Subtract, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(i, j, Operation.Operators.Multiply, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(i, j, Operation.Operators.DivideBy, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);

                        newOperations = applyOperation(j, i, Operation.Operators.DivideBy, currentOperations);
                        if (newOperations != null)
                            operationStack.AddFirst(newOperations);
                    }
                }
            }
            return null;
        }

        private List<Operation> applyOperation(int left, int right, Operation.Operators @operator, List<Operation> operations)
        {
            if (!Operation.TryApplyOperator(operations[left], operations[right], @operator,
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

        private class Operation
        {
            public enum Operators
            {
                Multiply,
                Add,
                Subtract,
                DivideBy
            }

            private Operation LeftOperand { get; set; }
            private Operation RightOperand { get; set; }
            private Operators Operator { get; set; }
            public int Result { get; private set; }

            public Operation(int number)
            {
                this.Result = number;
            }

            private Operation()
            {
            }

            public static bool TryApplyOperator(Operation _rightOperand, Operation _leftOperand,
                Operators operators, out Operation _result)
            {
                double result = 0;
                switch (operators)
                {
                    case Operators.Add:
                        result = _rightOperand.Result + _leftOperand.Result;
                        break;
                    case Operators.Multiply:
                        result = _rightOperand.Result * _leftOperand.Result;
                        break;
                    case Operators.DivideBy:
                        result = (double)_rightOperand.Result / _leftOperand.Result;
                        break;
                    case Operators.Subtract:
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
                    Operator = operators,
                    Result = (int)result
                };
                return true;
            }

            public override string ToString()
            {
                return ToString(this);
            }

            private static string ToString(Operation operation)
            {
                if (operation.RightOperand == null || operation.LeftOperand == null)
                    return operation.Result.ToString();
                return ($"({ToString(operation.LeftOperand)} {ToString(operation.Operator)} {ToString(operation.RightOperand)})");
            }

            public static string ToString(Operators @operator)
            {
                switch (@operator)
                {
                    case Operators.Add:
                        return "+";
                    case Operators.DivideBy:
                        return "/";
                    case Operators.Multiply:
                        return "*";
                    case Operators.Subtract:
                        return "-";
                }
                throw new InvalidOperationException();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(new OperationSequenceFinder(new[] { 1, 2, 3 }, 10).FindOperationSequence());
        }
    }
}

