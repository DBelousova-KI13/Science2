using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Science2.Model.BuisnessLogic;
using Science2.Model.Interfaces;
using Science2.Model.Models;
using Science2.ViewModel.Annotations;

namespace Science2.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ICommand _getFile1;
        private ICommand _getFile2;
        private ICommand _getResult;

        private IDataParser _parser1;
        private IDataParser _parser2;


        private string _dataString1;
        private string _dataString2;
        private string _resultString;
        private ICommand _getStepAndSquare;
        private string _step;
        private string _xSquareLength;
        private string _ySquareLength;

        public MainViewModel()
        {
            _parser1 = new DataParser1();
            _parser2 = new DefaultDataParser();
        }

        public string XSquareLength
        {
            get { return _xSquareLength; }
            set
            {
                _xSquareLength = value; 
                OnPropertyChanged();
            }
        }

        public string YSquareLength
        {
            get { return _ySquareLength; }
            set
            {
                _ySquareLength = value;
                OnPropertyChanged();
            }
        }

        public string Step
        {
            get { return _step; }
            set { _step = value; OnPropertyChanged(); }
        }

        public string DataString1
        {
            get { return _dataString1; }
            set
            {
                _dataString1 = value;
                OnPropertyChanged();
            }
        }

        public string DataString2
        {
            get { return _dataString2; }
            set
            {
                _dataString2 = value;
                OnPropertyChanged();
            }
        }

        public string ResultString
        {
            get { return _resultString; }
            set
            {
                _resultString = value; 
                OnPropertyChanged();
            }
        }

        public ICommand GetFile1
        {
            get { return _getFile1 ?? (_getFile1 = new CommandHandler(() => OpenFile1(), true)); }
        }

        private void OpenFile1()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                DataString1 = File.ReadAllText(openFileDialog.FileName);
        }

        public ICommand GetFile2
        {
            get { return _getFile1 ?? (_getFile2 = new CommandHandler(() => OpenFile2(), true)); }
        }

        private void OpenFile2()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                DataString2 = File.ReadAllText(openFileDialog.FileName);
            var x = 0;
        }

        public ICommand GetStepAndSquare
        {
            get { return _getStepAndSquare ?? (_getResult = new CommandHandler(() => CalculateStepAndSquare(), true)); }
        }

        private void CalculateStepAndSquare()
        {
            XSquareLength = "1";
            YSquareLength = "1";
            Step = "1";
        }

        public ICommand GetResult
        {
            get { return _getResult ?? (_getResult = new CommandHandler(() => CalculateResult(), true)); }
        }

        private void CalculateResult()
        {
            //todo: error handle123
            var list1 = _parser1.ParseString(DataString1);
            var list2 = _parser2.ParseString(DataString2);

            var dataStore = new DataStorage(list1,list2);
            var worker = new DataWorker(int.Parse(_step), int.Parse(XSquareLength), int.Parse(YSquareLength), dataStore);
            var result = worker.GetResult();
            ResultString = "";
            foreach (var res in result)
            {
                ResultString += res.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

/*  TODOS  *
 * SquareLength and Step in separate model
 * Remove redundant models (like Square)
 * 
 *  
 */
