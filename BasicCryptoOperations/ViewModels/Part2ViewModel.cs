using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicCryptoOperations.Models;
using DevExpress.Mvvm;

namespace BasicCryptoOperations.ViewModels
{
    class Part2ViewModel : ViewModelBase
    {
        private Part2Model _part2Model;
        private string _number;
        private int _shiftIndex;
        private string _swapRules;
        private string _binaryNum;

        public Part2ViewModel()
        {
            _part2Model = new Part2Model();
        }

        public String Number
        {
            get => _number;
            set
            {
                try
                {
                    _number = value;
                    BinaryNum = Convert.ToString(Convert.ToInt32(_number), 2);
                    _part2Model.SetNumber(_number);
                    _part2Model.SetBinaryNumber(_binaryNum);
                    MaxDivider = _part2Model.GetMaxDivider();
                    Limits = _part2Model.Limits();
                    SelfXored = _part2Model.XorItself();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }

            }
        }

        public int ShiftIndex
        {
            get => _shiftIndex;
            set
            {
                _shiftIndex = value;
                LeftShift = _part2Model.LeftShift(value);
                RightShift = _part2Model.RightShift(value);
            }
        }

        public String SwapRules
        {
            get => _swapRules;
            set
            {
                _swapRules = value;
                SwappedNumber = _part2Model.Swap(SwapRules);
            }
        }

        public String SwappedNumber { get; set; }

        public String BinaryNum
        {
            get => _binaryNum;
            set
            {
                try
                {
                    _binaryNum = value;
                    _part2Model.SetBinaryNumber(_binaryNum);
                    Number = Convert.ToInt32(_binaryNum, 2).ToString();
                    _part2Model.SetNumber(_number);
                    MaxDivider = _part2Model.GetMaxDivider();
                    Limits = _part2Model.Limits();
                    SelfXored = _part2Model.XorItself();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
        }

        public String LeftShift { get; set; }
        public String RightShift { get; set; }
        public String MaxDivider { get; set; }
        public bool SelfXored { get; set; }
        public String Limits { get; set; }
    }
}
