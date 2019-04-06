using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using BasicCryptoOperations.Models;
using DevExpress.Mvvm;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace BasicCryptoOperations.ViewModels
{
    class Part3ViewModel:ViewModelBase
    {
        private Part3Model _part3Model;

        public SeriesCollection SeriesCollection { get; set; }
        private ChartValues<ObservablePoint> myEncryptValues;
        private ChartValues<ObservablePoint> myDecryptValues;
        private ChartValues<ObservablePoint> encryptValues;
        private ChartValues<ObservablePoint> decryptValues;
        public Func<ObservablePoint, string> XFormatter { get; set; }
        public Func<ObservablePoint, string> YFormatter { get; set; }

        public String RC4Key { get; set; } = "RC4";
        public String VermanKey { get; set; } = "Verman";
        public String DESKey { get; set; } = "A1B2C3D4E5F6A7B8";

        public string Mode { get; set; } = "ECB";
        
        public bool IsReady { get; set; } = true;

        public bool IsWorking => !IsReady;


        public Part3ViewModel()
        {
            myEncryptValues = new ChartValues<ObservablePoint>();
            encryptValues= new ChartValues<ObservablePoint>();
            decryptValues= new ChartValues<ObservablePoint>();
            myDecryptValues= new ChartValues<ObservablePoint>();

            SeriesCollection =new SeriesCollection(){
                new LineSeries
                {
                    Title = "My Encrypt",
                    Values = myEncryptValues,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "My Decrypt",
                    Values = myDecryptValues,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Standard Encrypt",
                    Values = encryptValues,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Standard Decrypt",
                    Values = decryptValues,
                    LineSmoothness = 0
                },
            };
                


            XFormatter = val => val.X.ToString();
            YFormatter = val => val.Y.ToString();
            _part3Model =new Part3Model();
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
                    if (DESKey.Length == 16)
                    {
                        
                        String fileName = GetFileName();
                        var standardRes = DesEncrypt(fileName,DESKey);
                        var sPoint = new ObservablePoint(standardRes.Key, standardRes.Value);
                        encryptValues.Add(sPoint);
                        Task.Factory.StartNew(() =>
                        {
                            App.Current.Dispatcher.Invoke(() => IsReady = false);
                            

                            if (fileName != "")
                            {
                                var res = _part3Model.EncodeDes(fileName, DESKey, Mode);

                                var point = new ObservablePoint(res.Key,res.Value);
                                App.Current.Dispatcher.Invoke(() => myEncryptValues.Add(point));
                                
                            }

                            App.Current.Dispatcher.Invoke(() => IsReady = true);
                        });
                    }
                });

            }
        }

        public KeyValuePair<long, float> DesEncrypt(string pToEncrypt, string sKey)
        {
            sKey = sKey.Substring(0, 8);
            FileInfo file = new FileInfo(pToEncrypt);
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var str = File.ReadAllText(pToEncrypt);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            watch.Stop();
            return new KeyValuePair<long, float>(file.Length, watch.ElapsedMilliseconds*10);
        }


        public ICommand DecryptDESCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (DESKey.Length == 16)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            App.Current.Dispatcher.Invoke(() => IsReady = false);
                            String fileName = GetFileName();
                            if (fileName != "")
                                _part3Model.DecodeDES(fileName, DESKey, Mode);
                            App.Current.Dispatcher.Invoke(() => IsReady = true);
                        });
                    }
                });
            }
        }

        public ICommand SwitchDesModeCommand
        {
            get
            {
                return new DelegateCommand<String>((mode) =>
                {
                    Mode = mode;
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
