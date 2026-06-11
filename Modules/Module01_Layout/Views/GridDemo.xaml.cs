using System;
using System.Windows;
using System.Windows.Controls;

namespace LearnXaml.Modules.Module01_Layout.Views;

public partial class GridDemo : Page
{
    private double _currentValue = 0;
    private double _previousValue = 0;
    private string _operator = string.Empty;
    private bool _isNewInput = true;

    public GridDemo()
    {
        InitializeComponent();
    }

    private void NumberButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Content is string number)
        {
            if (_isNewInput)
            {
                CalculatorDisplay.Text = number;
                _isNewInput = false;
            }
            else
            {
                if (CalculatorDisplay.Text == "0" && number != ".")
                    CalculatorDisplay.Text = number;
                else
                    CalculatorDisplay.Text += number;
            }
        }
    }

    private void DecimalButton_Click(object sender, RoutedEventArgs e)
    {
        if (_isNewInput)
        {
            CalculatorDisplay.Text = "0.";
            _isNewInput = false;
        }
        else if (!CalculatorDisplay.Text.Contains("."))
        {
            CalculatorDisplay.Text += ".";
        }
    }

    private void OperatorButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Content is string op)
        {
            if (!_isNewInput)
            {
                Calculate();
            }

            _previousValue = double.Parse(CalculatorDisplay.Text);
            _operator = op;
            _isNewInput = true;
        }
    }

    private void EqualsButton_Click(object sender, RoutedEventArgs e)
    {
        Calculate();
        _operator = string.Empty;
        _isNewInput = true;
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        _currentValue = 0;
        _previousValue = 0;
        _operator = string.Empty;
        _isNewInput = true;
        CalculatorDisplay.Text = "0";
    }

    private void NegateButton_Click(object sender, RoutedEventArgs e)
    {
        if (double.TryParse(CalculatorDisplay.Text, out double value))
        {
            CalculatorDisplay.Text = (-value).ToString();
        }
    }

    private void PercentButton_Click(object sender, RoutedEventArgs e)
    {
        if (double.TryParse(CalculatorDisplay.Text, out double value))
        {
            CalculatorDisplay.Text = (value / 100).ToString();
        }
    }

    private void Calculate()
    {
        if (double.TryParse(CalculatorDisplay.Text, out double current))
        {
            _currentValue = _operator switch
            {
                "+" => _previousValue + current,
                "−" => _previousValue - current,
                "×" => _previousValue * current,
                "÷" => current != 0 ? _previousValue / current : double.NaN,
                _ => current
            };

            CalculatorDisplay.Text = _currentValue.ToString();
        }
    }
}