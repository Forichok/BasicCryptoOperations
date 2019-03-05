using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using BasicCryptoOperations.Models;
using DevExpress.Mvvm;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace BasicCryptoOperations.ViewModels
{
    class Part3ViewModel:ViewModelBase
    {
        private Part3Model _part3Model;

        public Part3ViewModel()
        {
            _part3Model=new Part3Model();
        }

        public ICommand EncryptCommand
        {
            get
            {
                    return new DelegateCommand(() =>
                    {
                        OpenFileDialog theDialog = new OpenFileDialog();
                        theDialog.Title = "Open Text File";
                        theDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        if (theDialog.ShowDialog() == true)
                        {
                            _part3Model.Encrypt(theDialog.FileName);
                        }
                    });
                
            }
        }

        public ICommand DecryptCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OpenFileDialog theDialog = new OpenFileDialog();
                    theDialog.Title = "Open Text File";
                    theDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    if (theDialog.ShowDialog() == true)
                    {
                        _part3Model.Decrypt(theDialog.FileName);
                    }
                });
            }
        }
    }
}
