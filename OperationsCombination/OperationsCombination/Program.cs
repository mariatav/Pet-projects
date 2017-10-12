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
            var operationStack = new LinkedList<Operation>();
            foreach (var num in this._numbers)
                operationStack.AddFirst(new Operation())
            return null;
        }


        private enum Operator
        {
            Multiply,
            Add,
            Subtract,
            DivideBy
        }
        private class Operation
        {
            public int LeftOperand { get; private set; }
            public int RightOperand { get; private set; }
            public Operator Operator { get; private set; }
            public int Result { get; private set; }

            public bool Equals(Operation other)
            {
                return this.Result == ((Operation)other).Result;
            }

            public static bool TryApplyOperator(int _rightOperand, int _leftOperand,
                Operator _operator, out Operation _operation)
            {
                double result = 0;
                switch (_operator)
                {
                    case Operator.Add:
                        result = _rightOperand + _leftOperand;
                        break;
                    case Operator.Multiply:
                        result = _rightOperand * _leftOperand;
                        break;
                    case Operator.DivideBy:
                        result = (double)_rightOperand / _leftOperand;
                        break;
                    case Operator.Subtract:
                        result = _rightOperand - _leftOperand;
                        break;
                }
                if (result < 0 || result != (int)result)
                {
                    _operation = null;
                    return false;
                }
                _operation = new Operation() { LeftOperand = _leftOperand, RightOperand = _rightOperand, Operator = _operator, Result = (int)result };
                return true;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
