using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BasicCryptoOperations.Models;
using DevExpress.Mvvm;
using Microsoft.Build.Framework;

namespace BasicCryptoOperations.ViewModels
{
    class Part1ViewModel : ViewModelBase
    {

        #region Properties 

        private Part1Model _part1Model;
        private string _number = "0";
        private int _bitPosition = 0;

        #endregion

        #region Fields

        public String Number
        {
            get => _number;
            set
            {
                _number = value;
                _part1Model.SetNumber(_number);
                BitValue = _part1Model.GetBitValue(_bitPosition);

            }
        }

        public String ConcatinatedBits { get; set; }
        public String SwapedBits { get; set; }
        public String BitsFromMiddle { get; set; }

        public int BitPosition
        {
            get => _bitPosition;
            set
            {
                _bitPosition = value;
                BitValue = _part1Model.GetBitValue(_bitPosition);

            }
        }

        public int BitI { get; set; }
        public int BitJ { get; set; }

        public int ByteI { get; set; }
        public int ByteJ { get; set; }

        public int BitsToConcatinate { get; set; }
        public int BitsToExtractFromMiddle { get; set; }
        public int BitsToReset { get; set; }

        public char BitValue { get; set; } = ' ';

        #endregion

        #region Constructors

        public Part1ViewModel()
        {
            _part1Model = new Part1Model();
        }

        #endregion

        #region Commands

        public ICommand InverseBitCommand
        {
            get { return new DelegateCommand(() => { Number = _part1Model.InverseBit(BitPosition); }); }
        }

        public ICommand ChangeBitsCommand
        {
            get { return new DelegateCommand(() => { Number = _part1Model.ChangeBits(BitI, BitJ); }); }
        }

        public ICommand ResetBitsCommand
        {
            get { return new DelegateCommand(() => { Number = _part1Model.ResetBits(BitsToReset); }); }
        }

        public ICommand ConcatinateBitsCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ConcatinatedBits = _part1Model.ConcatinateBits(BitsToConcatinate);
                });
            }
        }

        public ICommand GetBitsFromMiddleCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    BitsFromMiddle = _part1Model.GetBitsFromMiddle(BitsToExtractFromMiddle);
                });
            }
        }

        public ICommand SwapBytesCommand
        {
            get { return new DelegateCommand(() => { SwapedBits = _part1Model.SwapBytes(ByteI, ByteJ); }); }
        }

        #endregion
    }
}
