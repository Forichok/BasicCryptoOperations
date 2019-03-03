﻿using System;
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
    class Part1ViewModel:ViewModelBase
    {
        private Part1Model _part1Model;


        private string _number = "0";
        private int _bitPosition = 0;

        public String Number
        {
            get => _number;
            set
            {
                _number = value;
                BitValue = _part1Model.GetBitValue(_number, _bitPosition);
            }
        }
        public int BitPosition
        {
            get => _bitPosition;
            set
            {
                _bitPosition = value;
                BitValue = _part1Model.GetBitValue(_number, _bitPosition);

            }
        }
        public int BitI { get; set; }
        public int BitJ { get; set; }
        public char BitValue { get; set; } = ' ';
        public int BitsCountToReset { get; set; }


        public Part1ViewModel()
        {
            _part1Model=new Part1Model();
        }

        public ICommand InverseBitCommand
        {
            get
            {
                return new DelegateCommand(() => { Number = _part1Model.InverseBit(Number, BitPosition); });
            }
        }

        public ICommand ChangeBitsCommand
        {
            get
            {
                return new DelegateCommand(() => { Number = _part1Model.ChangeBits(Number, BitI, BitJ); });
            }
        }

        public ICommand ResetBitsCommand
        {
            get
            {
                return new DelegateCommand(() => { Number = _part1Model.ResetBits(Number, BitsCountToReset); });
            }
        }
    }
}
