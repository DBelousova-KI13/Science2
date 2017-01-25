using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        private int _pointsCount;
        private int _stepLength;
        private int _userDefinedWidth;
        private int _userDefinedHeidht;
        private int _pointHeight;
        private int _pointWidth;

        public MainViewModel()
        {
            Step = "5";
            XSquareLength = "20";
            YSquareLength = "20";
            
            //GetDataFromCSV();
            //CountCorrelation();

            //_parser1 = new DataParser1();
            //_parser2 = new DefaultDataParser();
        }

        public DataStorage CurrentStorage { get; set; }

        public DataWorker Worker { get; set; }

        public void Count( )
        {
            PointWidth = UserDefinedWidth/StepLength;
            PointHeight = UserDefinedHeidht/StepLength;
            ErrorInputMessage = "Начато вычисление";
            CurrentStorage = new DataStorage(CurrentPoints);
            Worker = new DataWorker(StepLength, PointHeight, PointWidth, CurrentStorage);
            ErrorInputMessage = "Вычисления выполнены, можо сохранить файл";
            //var res = worker.Res;

            // CountCorrelation();
        }

        public void CreateWindow()
        {
           // XSquareLength = PointWidth;

        }

        public int MapWidthInPoints
        {
            get { return _mapWidthInPoints; }
            set { _mapWidthInPoints = value; OnPropertyChanged(); }
        }

        public int MapWidthInMetrs
        {
            get { return _mapWidthInMetrs; }
            set { _mapWidthInMetrs = value; OnPropertyChanged(); }
        }

        public int MapHeightInPoints
        {
            get { return _mapHeightInPoints; }
            set { _mapHeightInPoints = value; OnPropertyChanged(); }
        }

        public int MapHeightInMetrs
        {
            get { return _mapHeightInMetrs; }
            set { _mapHeightInMetrs = value; OnPropertyChanged(); }
        }

        public int HowFar
        {
            get { return _howFar; }
            set { _howFar = value; OnPropertyChanged(); }
        }

        public string HowFarString
        {
            get { return _howFarString; }
            set { _howFarString = value; OnPropertyChanged(); CountFar();}
        }

        public void CountFar()
        {
            HowFar = Convert.ToInt32(HowFarString);
            IEnumerable<DataPoint> result = CurrentPoints.OrderBy(x => x.Y).ToList();
            int howMuchY = 0;
            int howMuchX = 0;
            if (result.Count() > 0)
            {
                var temp = result.FirstOrDefault().Y;
                foreach (var res in result)
                {
                    if (res.Y != temp)
                    {
                        temp = res.Y;
                        howMuchY++;
                    }
                
                }
                var temp2 = result.FirstOrDefault().Y;
                foreach (var res in result)
                {

                    if (res.Y == temp2)
                    {

                        howMuchX++;
                    }
                    else
                    {
                        break;
                    }
                }
                MapHeightInPoints = howMuchY;
                MapHeightInMetrs = howMuchY * HowFar;
                MapWidthInPoints = howMuchX;
                MapWidthInMetrs = howMuchX * HowFar;
            }
           
        }


        public bool NotUseFirstString
        {
            get { return _notUseFirstString; }
            set { _notUseFirstString = value; OnPropertyChanged();}
        }

        public int PointsCount
        {
            get { return _pointsCount; }
            set { _pointsCount = value; OnPropertyChanged(); }
        }

        public int StepLength 
        {
            get { return _stepLength; }
            set { _stepLength = value; OnPropertyChanged();}
        }

        public int UserDefinedWidth
        {
            get { return _userDefinedWidth; }
            set { _userDefinedWidth = value; OnPropertyChanged();}
        }

        public int UserDefinedHeidht
        {
            get { return _userDefinedHeidht; }
            set { _userDefinedHeidht = value; OnPropertyChanged();  }
        }


        public int PointHeight
        {
            get { return _pointHeight; }
            set { _pointHeight = value; OnPropertyChanged();}
        }

        public int PointWidth
        {
            get
            {
                return _pointWidth;
            }
            set { _pointWidth = value; }
        }


        public List<DataPoint> CurrentPoints = new List<DataPoint>();
        public List<Point3D> OutputPoints = new List<Point3D>();
        private string _errorInputMessage;
        private ICommand _getDataFile;
        private string _howFarString;
        private int _mapWidthInPoints;
        private int _mapWidthInMetrs;
        private int _mapHeightInPoints;
        private int _mapHeightInMetrs;
        private int _howFar;
        private bool _notUseFirstString;
        private string _ySquareLengthMetr;
        private string _xSquareLengthMetr;
        private ICommand _saveDataFile;

        public void GetDataFromCSV()
        {
            StreamReader f = new StreamReader(@"c:\inputs\input_test_square.dat");
        
            var Points = new List<DataPoint>();
            while (!(f.EndOfStream))
            {
                string[] line = f.ReadLine().Split(' ');
                 bool isEnabled = true;
                foreach (var s in line)
                {
                    if (s == "")
                    {
                        isEnabled = false;
                    }
}
                if (isEnabled)
                {
                    Points.Add(new DataPoint
                        (Double.Parse(line[0], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                        Double.Parse(line[1], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                         Double.Parse(line[2], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                          Double.Parse(line[3], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture)
                        ));
                    //Decimal.Parse(temp2, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture)

                }
            }

            PointsCount = Points.Count;
            CurrentPoints = Points;
            f.Close();
        }



        public void CountCorrelation()
        {
            //currentCulture
            
            //здесь вычисления  DataStorage и потом DataWorker
            var testStorage = new DataStorage(CurrentPoints);
            //   var worker  = new DataWorker(StepLength, PointHeight, PointWidth, testStorage);
            var worker = new DataWorker(1, 30, 30, testStorage);
            var res = worker.Res;
            var test = worker.TestMistakes;
            var test2 = worker.WhatsWrong;
            StreamWriter ftest = new StreamWriter(@"c:\inputs\map_to_save_mistakes.dat");
            foreach (var point in test2)
            {
                if (point != null)
                {
                    ftest.WriteLine(point.X.ToString() + " " + point.Y.ToString() + " " + point.Z1.ToString() + " " + point.Z2.ToString());
                }
                
            }
            ftest.Close();
            StreamWriter ftest2 = new StreamWriter(@"c:\inputs\map_to_findpoints.dat");
            StreamReader ftest1 = new StreamReader(@"c:\inputs\map_to_save_mistakes.dat");
           while (!(ftest1.EndOfStream))
            {
                string line = ftest1.ReadLine();
                var line1 = line.ToCharArray();
                for (int index = 0; index < line1.Length; index++)
                {
                    var c = line[index];
                    if (c == ',')
                    {
                        line1[index] = '.';
                    }
                }
                ftest2.WriteLine(line1);
            }
            ftest2.Close();
            ftest1.Close();
            StreamWriter f2 = new StreamWriter(@"c:\inputs\map_to_test_square.dat");
            foreach (var point in res)
            {
                f2.WriteLine(point.X.ToString() + " " + point.Y.ToString() + " " + Math.Abs(point.Z).ToString());
            }

            f2.Close();
            StreamReader f1 = new StreamReader(@"c:\inputs\map_to_test_square.dat");
            StreamWriter f3 = new StreamWriter(@"c:\inputs\map_to_test_square_abs_30_1.dat");
            while (!(f1.EndOfStream))
            {
                string line = f1.ReadLine();
                var line1 = line.ToCharArray();
                for (int index = 0; index < line1.Length; index++)
                {
                    var c = line[index];
                    if (c == ',')
                    {
                        line1[index] = '.'; 
                    }
                }
                f3.WriteLine(line1);
            }
            f1.Close();
            f3.Close();
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

        public string YSquareLengthMetr
        {
            get { return _ySquareLengthMetr; }
            set { _ySquareLengthMetr = value; OnPropertyChanged();}
        }

        public string XSquareLengthMetr
        {
            get { return _xSquareLengthMetr; }
            set { _xSquareLengthMetr = value; OnPropertyChanged(); }
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


        public ICommand GetDataFile
        {
            get { return _getDataFile ?? (_getDataFile = new CommandHandler(() => GetFile(), true)); }
        }


        public ICommand SaveDataFile
        {
            get { return _saveDataFile ?? (_saveDataFile = new CommandHandler(() => SaveFile(), true)); }
        }


        public void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "dat files (*.dat)|*.dat";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;
                StreamWriter f2 = new StreamWriter(filename);
                foreach (var point in Worker.Res)
                {
                    f2.WriteLine(point.X.ToString().Replace(",", ".") + " " + point.Y.ToString().Replace(",", ".") + " " + Math.Abs(point.Z).ToString().Replace(",", "."));
                }
                f2.Close();
            }

        }


        public void GetFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "dat files (*.dat)|*.dat|csv files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
             
                if (openFileDialog.SafeFileName.Substring(openFileDialog.SafeFileName.Length - 3, 3) == "csv")
                {
                    FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);

                    StreamReader f = new StreamReader(openFileDialog.FileName);

                    var Points = new List<DataPoint>();
                    while (!(f.EndOfStream))
                    {
                        
                        string[] line = f.ReadLine().Split(';');
                        bool isEnabled = true;
                        if (NotUseFirstString && Points.Count == 0)
                        {
                            isEnabled = true;

                        }
                        else
                        {
                            foreach (var s in line)
                            {
                                if (s == "")
                                {
                                    isEnabled = false;
                                }
                            }
                        }
                        
                        if (isEnabled)
                        {
                            Points.Add(new DataPoint(Convert.ToDouble(line[0]), Convert.ToDouble(line[1]), Convert.ToDouble(line[2]), Convert.ToDouble(line[3])));
                            //Points.Add(new DataPoint
                            //    (Decimal.Parse(line[0], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                            //   Decimal.Parse(line[1], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                            //     Decimal.Parse(line[2], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                            //      Decimal.Parse(line[3], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture)
                            //    ));
                            //Decimal.Parse(temp2, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture)

                        }
                    }

                    PointsCount = Points.Count;
                    CurrentPoints = Points;
                    
                    f.Close();
                    CountFar();
                }

                if (openFileDialog.SafeFileName.Substring(openFileDialog.SafeFileName.Length - 3, 3) == "dat")
                {
                    FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);

                    StreamReader f = new StreamReader(openFileDialog.FileName);

                    var Points = new List<DataPoint>();
                    while (!(f.EndOfStream))
                    {
                        if (NotUseFirstString)
                        {
                            f.ReadLine();
                        }
                        string[] line = f.ReadLine().Split(' ');
                        bool isEnabled = true;
                        foreach (var s in line)
                        {
                            if (s == "")
                            {
                                isEnabled = false;
                            }
                        }
                        if (isEnabled)
                        {
                            // Points.Add(new DataPoint(Convert.ToDouble(line[0]), Convert.ToDouble(line[1]), Convert.ToDouble(line[2]), Convert.ToDouble(line[3])));
                            Points.Add(new DataPoint
                                (Decimal.Parse(line[0], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                               Decimal.Parse(line[1], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                                 Decimal.Parse(line[2], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture),
                                  Decimal.Parse(line[3], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture)
                                ));
                            //Decimal.Parse(temp2, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture)

                        }
                    }

                    PointsCount = Points.Count;
                    CurrentPoints = Points;
                    f.Close();
                    CountFar();
                }

                
                //var testStorage = new DataStorage(CurrentPoints);
                //var worker  = new DataWorker(StepLength, PointHeight, PointWidth, testStorage);
                //var res = worker.Res;
                //StreamWriter f2 = new StreamWriter(@"c:\test\testmap.dat");
                //foreach (var point in res)
                //{
                //    f2.WriteLine(point.X.ToString() + " " + point.Y.ToString() + " " + Math.Abs(point.Z).ToString());
                //}

                //f2.Close();
                //StreamReader f1 = new StreamReader(@"c:\test\testmap.dat");
                //StreamWriter f3 = new StreamWriter(@"c:\test\SurferMap.dat");
                //while (!(f1.EndOfStream))
                //{
                //    string line = f1.ReadLine();
                //    var line1 = line.ToCharArray();
                //    for (int index = 0; index < line1.Length; index++)
                //    {
                //        var c = line[index];
                //        if (c == ',')
                //        {
                //            line1[index] = '.';
                //        }
                //    }
                //    f3.WriteLine(line1);
                //}
                //f1.Close();
                //f3.Close();
            }
        }

        public string ErrorInputMessage
        {
            get { return _errorInputMessage; }
            set { _errorInputMessage = value; OnPropertyChanged();}
        }

        private void CalculateStepAndSquare() // здесь должна быть проверка на безопасные числа
        {
            try
            {
                int temp = Convert.ToInt32(Step);
                if (temp < 0)
                {
                    throw new Exception();
                }
                else
                {
                    StepLength = temp;
                }
            }
            catch (Exception)
            {

                ErrorInputMessage = "Введено некорректное значение шага! Введите целое неотрицательное число";
            }

            try
            {
                int temp = Convert.ToInt32(XSquareLength);
                if (temp < 0)
                {
                    throw new Exception();
                }
                else
                {
                    UserDefinedWidth = temp;
                    if (temp > 0)
                    {
                        XSquareLengthMetr = (temp*HowFar).ToString();
                    }
                }
            }
            catch (Exception)
            {

                ErrorInputMessage = "Введено некорректное значение ширины окна! Введите целое неотрицательное число";
            }

            try
            {
                int temp = Convert.ToInt32(YSquareLength);
                if (temp < 0)
                {
                    throw new Exception();
                }
                else
                {
                    UserDefinedHeidht = temp;
                    if (temp > 0)
                    {
                        YSquareLengthMetr = (temp*HowFar).ToString();
                    }
                }
            }
            catch (Exception)
            {

                ErrorInputMessage = "Введено некорректное значение длины окна! Введите целое неотрицательное число";
            }


            try
            {
                int temp = Convert.ToInt32(XSquareLengthMetr);
                if (temp < 0)
                {
                    throw new Exception();
                }
                else
                {
                    if (temp > 0)
                    {
                        temp = temp / HowFar;
                        UserDefinedWidth = temp;
                        XSquareLength = temp.ToString();
                    }
                    
                }
            }
            catch (Exception)
            {

                ErrorInputMessage = "Введено некорректное значение ширины окна! Введите целое неотрицательное число";
            }

            try
            {
                int temp = Convert.ToInt32(YSquareLengthMetr);
                if (temp < 0)
                {
                    throw new Exception();
                }
                else
                {
                    if (temp > 0)
                    {
                        temp = temp / HowFar;
                        UserDefinedHeidht = temp;
                        YSquareLength = temp.ToString();
                    }
                    
                }
            }
            catch (Exception)
            {

                ErrorInputMessage = "Введено некорректное значение длины окна! Введите целое неотрицательное число";
            }

            if ((StepLength != 0) && (UserDefinedWidth != 0) && (UserDefinedHeidht != 0) &&
                (UserDefinedWidth > StepLength) && (UserDefinedHeidht > StepLength))
            {
                ErrorInputMessage = "Показатели верны";
                Count();
                
            }
            else
            {
                ErrorInputMessage = "Невозможно выполнить вычисления! Проверьте показатели";
            }

            //XSquareLength = "1";
            //YSquareLength = "1";
            //Step = "1";
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
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

/*  TODOS  *
 * SquareLength and Step in separate model
 * Remove redundant models (like Square)
 * 
 *  
 */
