using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace Домашнее_задание_3;

public class CalculatorModel
    {
        public double Add(double x, double y) => x + y;
        public double Subtract(double x, double y) => x - y;
        public double Multiply(double x, double y) => x * y;
        public double Divide(double x, double y) => x / y;
        public double Mod(double x, double y) => x % y;
        public double Power(double x, double y) => Math.Pow(x, y);
        public double Factorial(int x)
        {
            if (x < 0)
                throw new ArgumentException("Error");
            if (x == 0)
                return 1;
            double result = 1;
            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            return result;
        }
        public double Log10(double x) => Math.Log10(x);
        public double Ln(double x) => Math.Log(x);
        public double Sin(double x) => Math.Sin(Math.PI*x/180);
        public double Cos(double x) => Math.Cos(Math.PI*x/180);
        public double Tg(double x) => Math.Tan(Math.PI*x/180);
        public double Floor(double x) => Math.Floor(x);
        public double Ceiling(double x) => Math.Ceiling(x);
    }


public class DataContextMainWindow : INotifyPropertyChanged{
    private string _displayText = "0";
    private string _currentInput = "";
    private double _currentValue = 0;
    private string _lastOperation = "";
    private bool _operationPending = false;
    private CalculatorModel _calculator = new CalculatorModel();

    private int iterate;

    public string Text
    {
        get
        {
            return _displayText;
        }

        set
        {
            _displayText = value;
            OnPropertyChanged(nameof(Text));
        }
    }

    public void ExecuteCommand(string? text)
    {
        if (text != null)
        {
            Text = text;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void AppendInput(string input)
    {
        if (_operationPending)
        {
            _currentInput = "";
            _operationPending = false;
        }

        _currentInput += input;
        Text = _currentInput;
    }

    public void ButtonClick(string content)
    {
        if (int.TryParse(content, out int _)){
            AppendInput(content);
        }
        else{
            switch(content){
                case ",":
                    AppendInput(",");
                    break;

                case "C":
                    _currentInput = "";
                    _currentValue = 0;
                    Text = "0";
                    break;

                case "⌫":
                    if (_currentInput.Length > 0){
                        if(_currentInput.Length - 1 == 0){
                            _currentInput = _currentInput.Remove(_currentInput.Length - 1);
                            Text = "0";
                            }
                        else{
                            _currentInput = _currentInput.Remove(_currentInput.Length - 1);
                            Text = _currentInput;
                        }
                    }
                    break;

                case "=":
                    Operations(_lastOperation);
                    Text = _currentValue.ToString();
                    break;

                case "floor":
                    if(string.IsNullOrEmpty(_currentInput))  _currentValue = _calculator.Floor(_currentValue);
                    else _currentValue = _calculator.Floor(double.Parse(_currentInput));
                    Text = _currentValue.ToString();
                    _currentInput = "";
                    break;

                case "ceil":
                    if(string.IsNullOrEmpty(_currentInput))  _currentValue = _calculator.Ceiling(_currentValue);
                    else _currentValue = _calculator.Ceiling(double.Parse(_currentInput));
                    Text = _currentValue.ToString();
                    _currentInput = "";
                    break;

                case "lg":
                    if(string.IsNullOrEmpty(_currentInput))  _currentValue = _calculator.Log10(_currentValue);
                    else _currentValue = _calculator.Log10(double.Parse(_currentInput));
                    Text = _currentValue.ToString();
                    _currentInput = "";
                    break;

                case "ln":
                    if(string.IsNullOrEmpty(_currentInput))  _currentValue = _calculator.Ln(_currentValue);
                    else _currentValue = _calculator.Ln(double.Parse(_currentInput));
                    Text = _currentValue.ToString();
                    _currentInput = "";
                    break;

                case "sin":
                    if(string.IsNullOrEmpty(_currentInput))  _currentValue = _calculator.Sin(_currentValue);
                    else _currentValue = _calculator.Sin(double.Parse(_currentInput));
                    Text = _currentValue.ToString();
                    _currentInput = "";
                    break;

                case "cos":
                    if(string.IsNullOrEmpty(_currentInput))  _currentValue = _calculator.Cos(_currentValue);
                    else _currentValue = _calculator.Cos(double.Parse(_currentInput));
                    Text = _currentValue.ToString();
                    _currentInput = "";
                    break;

                case "tg":
                    if(string.IsNullOrEmpty(_currentInput))  _currentValue = _calculator.Tg(_currentValue);
                    else _currentValue = _calculator.Tg(double.Parse(_currentInput));
                    Text = _currentValue.ToString();
                    _currentInput = "";
                    break;

                case "!":
                    if(string.IsNullOrEmpty(_currentInput))  _currentValue = _calculator.Factorial((int)_currentValue);
                    else _currentValue = _calculator.Factorial((int)double.Parse(_currentInput));
                    Text = _currentValue.ToString();
                    _currentInput = "";
                    break;

                default:
                    Operations(content);
                    break;
            }
        }
    }

    private void Operations(string operation){
        if(!string.IsNullOrEmpty(_currentInput)){
            double input = double.Parse(_currentInput);
            switch(operation){
                case "+":
                    if (iterate == 0)
                    {
                        _currentValue = double.Parse(_currentInput);
                        _currentInput = "";
                    }
                    else
                    {
                        _currentValue = _calculator.Add(_currentValue, double.Parse(_currentInput));
                        _currentInput = ""; 
                    }
                    iterate += 1; 
                    break;
                
                case "-":
                    if (iterate == 0)
                    {
                        _currentValue = double.Parse(_currentInput);
                        _currentInput = "";
                    }
                    else
                    {
                        _currentValue = _calculator.Subtract(_currentValue, double.Parse(_currentInput));
                        _currentInput = ""; 
                    }
                    iterate += 1; 
                    break;
                
                case "/":
                    if (iterate == 0)
                    {
                        _currentValue = double.Parse(_currentInput);
                        _currentInput = "";
                    }
                    else
                    {
                        _currentValue = _calculator.Divide(_currentValue, double.Parse(_currentInput));
                        _currentInput = ""; 
                    }
                    iterate += 1; 
                    break;
                
                case "*":
                    if (iterate == 0)
                    {
                        _currentValue = double.Parse(_currentInput);
                        _currentInput = "";
                    }
                    else
                    {
                        _currentValue = _calculator.Multiply(_currentValue, double.Parse(_currentInput));
                        _currentInput = ""; 
                    }
                    iterate += 1; 
                    break;

                case "^":
                    if (iterate == 0)
                    {
                        _currentValue = double.Parse(_currentInput);
                        _currentInput = "";
                    }
                    else
                    {
                        _currentValue = _calculator.Power(_currentValue, double.Parse(_currentInput));
                        _currentInput = ""; 
                    }
                    iterate += 1; 
                    break;

                case "mod":
                    if (iterate == 0)
                    {
                        _currentValue = double.Parse(_currentInput);
                        _currentInput = "";
                    }
                    else
                    {
                        _currentValue = _calculator.Mod(_currentValue, double.Parse(_currentInput));
                        _currentInput = ""; 
                    }
                    iterate += 1; 
                    break;
            }
        }
        _lastOperation = operation;
    }

}

