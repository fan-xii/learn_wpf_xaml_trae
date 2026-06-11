using System.Windows;
using System.Windows.Controls;
using LearnXaml.Modules.Module02_Controls.Views;

namespace LearnXaml.Modules.Module02_Controls;

public partial class ControlsWindow : Window
{
    public ControlsWindow()
    {
        InitializeComponent();
        FrameButton.Navigate(new ButtonDemo());
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MainTabControl.SelectedItem is not TabItem tab) return;

        if (tab == TabButton && FrameButton.Content == null)
            FrameButton.Navigate(new ButtonDemo());
        else if (tab == TabTextBox && FrameTextBox.Content == null)
            FrameTextBox.Navigate(new TextBoxDemo());
        else if (tab == TabSelection && FrameSelection.Content == null)
            FrameSelection.Navigate(new SelectionControlsDemo());
        else if (tab == TabList && FrameList.Content == null)
            FrameList.Navigate(new ListControlsDemo());
        else if (tab == TabDataGrid && FrameDataGrid.Content == null)
            FrameDataGrid.Navigate(new DataGridDemo());
        else if (tab == TabTreeView && FrameTreeView.Content == null)
            FrameTreeView.Navigate(new TreeViewDemo());
        else if (tab == TabMenu && FrameMenu.Content == null)
            FrameMenu.Navigate(new MenuToolBarDemo());
        else if (tab == TabTabControl && FrameTabControl.Content == null)
            FrameTabControl.Navigate(new TabControlDemo());
    }
}