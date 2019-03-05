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

        public String RC4Key { get; set; }
        public String VermanKey { get; set; }
        public String DESKey { get; set; }


        public bool IsReady { get; set; } = true;
        
        

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
                        Task.Factory.StartNew(() =>
                        {
                            App.Current.Dispatcher.Invoke(() => IsReady = false);
                            String fileName = GetFileName();
                            if (fileName != "")
                                _part3Model.Encrypt(fileName);
                            App.Current.Dispatcher.Invoke(() => IsReady = true);
                        });
                    });
                
            }
        }

        public ICommand DecryptCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        App.Current.Dispatcher.Invoke(() => IsReady = false);
                        String fileName = GetFileName();
                        if (fileName != "")
                            _part3Model.Decrypt(fileName);
                        App.Current.Dispatcher.Invoke(() => IsReady = true);
                    });
                });
            }
        }


        public ICommand EncryptDESCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        App.Current.Dispatcher.Invoke(() => IsReady = false);
                        String fileName = GetFileName();
                        if (fileName != "")
                            _part3Model.EncodeDES(fileName,DESKey);
                        App.Current.Dispatcher.Invoke(() => IsReady = true);
                    });
                });

            }
        }

        public ICommand DecryptDESCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        App.Current.Dispatcher.Invoke(() => IsReady = false);
                        String fileName = GetFileName();
                        if (fileName != "")
                            _part3Model.DecodeDES(fileName, DESKey);
                        App.Current.Dispatcher.Invoke(() => IsReady = true);
                    });
                });
            }
        }

        public ICommand StartRC4Command
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        App.Current.Dispatcher.Invoke(() => IsReady = false);
                        String fileName = GetFileName();
                        if (fileName != "")
                            _part3Model.StartRC4(fileName,RC4Key);
                        App.Current.Dispatcher.Invoke(() => IsReady = true);
                    });
                });
            }
        }

        public ICommand StartVermanCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        App.Current.Dispatcher.Invoke(() => IsReady = false);
                        String fileName = GetFileName();
                        if (fileName != "")
                            _part3Model.StartVernam(fileName,VermanKey);
                        App.Current.Dispatcher.Invoke(() => IsReady = true);
                    });
                });
            }
        }

        private static String GetFileName()
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (theDialog.ShowDialog() == true)
            {
                return theDialog.FileName;
            }
            else
            {
                return "";
            }
        }
    }
}
