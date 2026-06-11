using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LearnXaml.Modules.Module04_Advanced.Views;

public partial class AnimationDemo : Page
{
    public AnimationDemo()
    {
        InitializeComponent();
    }

    private void OnStartWidthAnimation(object sender, RoutedEventArgs e)
    {
        var sb = FindResource("WidthStoryboard") as Storyboard;
        sb?.Begin(AnimatedRectangle, true);
    }

    private void OnStartColorAnimation(object sender, RoutedEventArgs e)
    {
        var sb = FindResource("ColorStoryboard") as Storyboard;
        sb?.Begin(AnimatedRectangle, true);
    }

    private void OnStartTransformAnimation(object sender, RoutedEventArgs e)
    {
        var sb = FindResource("TransformStoryboard") as Storyboard;
        sb?.Begin(AnimatedRectangle, true);
    }

    private void OnStartBounceAnimation(object sender, RoutedEventArgs e)
    {
        var sb = FindResource("BounceStoryboard") as Storyboard;
        sb?.Begin(BounceBall, true);
    }

    private void OnStartCombinedAnimation(object sender, RoutedEventArgs e)
    {
        var sb = FindResource("CombinedStoryboard") as Storyboard;
        sb?.Begin(CombinedTarget, true);
    }

    private void OnPauseCombined(object sender, RoutedEventArgs e)
    {
        var sb = FindResource("CombinedStoryboard") as Storyboard;
        sb?.Pause(CombinedTarget);
    }

    private void OnResumeCombined(object sender, RoutedEventArgs e)
    {
        var sb = FindResource("CombinedStoryboard") as Storyboard;
        sb?.Resume(CombinedTarget);
    }

    private void OnStopCombined(object sender, RoutedEventArgs e)
    {
        var sb = FindResource("CombinedStoryboard") as Storyboard;
        sb?.Stop(CombinedTarget);
    }
}