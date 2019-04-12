using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace BasicCryptoOperations.ViewModels
{
    class MainViewModel:ViewModelBase
    {
        public Part1ViewModel Part1ViewModel { get; set; }
        public Part2ViewModel Part2ViewModel { get; set; }
        public Part3ViewModel Part3ViewModel { get; set; }

        public MainViewModel()
        {
            Part1ViewModel = new Part1ViewModel();
            Part2ViewModel = new Part2ViewModel();
            Part3ViewModel = new Part3ViewModel();
        }
        public ICommand HelpCommand
        {
            get { return new DelegateCommand(() =>
                {
                    MessageBox.Show("HELP!", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
                }); }
        }
        public ICommand AboutCommand
        {
            get { return new DelegateCommand(() =>
            {
                MessageBox.Show("ABOUT!", "About", MessageBoxButton.OK, MessageBoxImage.Information);
            }); }
        }
    }
}
