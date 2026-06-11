using System.Windows;
using System.Windows.Controls;

namespace LearnXaml.Modules.Module02_Controls.Views;

public partial class SelectionControlsDemo : Page
{
    public SelectionControlsDemo()
    {
        InitializeComponent();
        UpdateOrderSummary();
    }

    private void ChkAccept_Checked(object sender, RoutedEventArgs e) =>
        TxtCheckBoxStatus.Text = "已同意服务条款 ✓";

    private void ChkAccept_Unchecked(object sender, RoutedEventArgs e) =>
        TxtCheckBoxStatus.Text = "未同意服务条款";

    private void ChkSubscribe_Checked(object sender, RoutedEventArgs e) =>
        TxtCheckBoxStatus.Text = "已订阅邮件通知";

    private void ChkSubscribe_Unchecked(object sender, RoutedEventArgs e) =>
        TxtCheckBoxStatus.Text = "已取消邮件订阅";

    private void ChkThreeState_Checked(object sender, RoutedEventArgs e) =>
        TxtCheckBoxStatus.Text = "三态: 全部选中 (Checked)";

    private void ChkThreeState_Unchecked(object sender, RoutedEventArgs e) =>
        TxtCheckBoxStatus.Text = "三态: 全部不选 (Unchecked)";

    private void ChkThreeState_Indeterminate(object sender, RoutedEventArgs e) =>
        TxtCheckBoxStatus.Text = "三态: 部分选中 (Indeterminate)";

    private void Payment_Checked(object sender, RoutedEventArgs e)
    {
        var payment = RbWechat.IsChecked == true ? "微信支付" :
                      RbAlipay.IsChecked == true ? "支付宝" :
                      RbBank.IsChecked == true ? "银行卡" : "货到付款";
        var shipping = RbExpress.IsChecked == true ? "顺丰速运" :
                       RbNormal.IsChecked == true ? "中通快递" : "邮政";
        TxtRadioStatus.Text = $"已选支付: {payment}, 已选快递: {shipping}";
    }

    private void Shipping_Checked(object sender, RoutedEventArgs e) => Payment_Checked(sender, e);

    private void CboCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CboCity.SelectedItem is ComboBoxItem item)
            TxtCitySelected.Text = $"当前选择: {item.Content}";
    }

    private void SldValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        TxtSliderValue.Text = ((int)e.NewValue).ToString();
    }

    private void SldVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        TxtVolume.Text = $"{(int)e.NewValue}%";
    }

    private void PizzaSize_Checked(object sender, RoutedEventArgs e) => UpdateOrderSummary();

    private void Topping_Changed(object sender, RoutedEventArgs e) => UpdateOrderSummary();

    private void SldQuantity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        TxtQuantity.Text = $"{(int)e.NewValue} 个";
        UpdateOrderSummary();
    }

    private void CboDrink_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateOrderSummary();

    private void UpdateOrderSummary()
    {
        decimal total = 0;
        string size = "小份 (6寸)";
        if (RbMedium.IsChecked == true) { size = "中份 (9寸)"; total = 49; }
        else if (RbLarge.IsChecked == true) { size = "大份 (12寸)"; total = 69; }
        else total = 29;

        var toppings = new List<string>();
        if (ChkCheese.IsChecked == true) { toppings.Add("额外芝士"); total += 5; }
        if (ChkPepperoni.IsChecked == true) { toppings.Add("意大利腊肠"); total += 8; }
        if (ChkMushroom.IsChecked == true) { toppings.Add("蘑菇"); total += 4; }
        if (ChkBacon.IsChecked == true) { toppings.Add("培根"); total += 6; }

        string toppingStr = toppings.Count > 0 ? "配料: " + string.Join(", ", toppings) : "无额外配料";

        int quantity = (int)SldQuantity.Value;

        string drink = "可乐";
        if (CboDrink.SelectedIndex == 1) drink = "雪碧";
        else if (CboDrink.SelectedIndex == 2) { drink = "橙汁"; total += 8; }
        else if (CboDrink.SelectedIndex == 3) { drink = "矿泉水"; total += 2; }
        else if (CboDrink.SelectedIndex == 4) drink = "无饮品";

        total *= quantity;
        TxtOrderSummary.Text = $"{size} 比萨 x {quantity}\n{toppingStr}\n饮品: {drink}";
        TxtOrderTotal.Text = $"💰 总计: ¥{total:F2}";
    }
}