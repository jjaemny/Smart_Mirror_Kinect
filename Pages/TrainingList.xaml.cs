namespace Microsoft.Samples.Kinect.ControlsBasics
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Wpf.Controls;
    using Microsoft.Samples.Kinect.ControlsBasics.DataModel;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class TrainingList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingList"/> class. 
        /// </summary>
        public TrainingList()
        {
            this.InitializeComponent();
            var trainingDataSource = TrainingDataSource.GetGroup("Group-1");
            this.itemsControl.ItemsSource = trainingDataSource;
        }

        private void TrainingItemButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)e.OriginalSource;
            TrainingDataItem trainingDataItem = button.DataContext as TrainingDataItem;

            if (trainingDataItem != null && trainingDataItem.NavigationPage != null)
            {
                trainingNavigationRegion.Content = Activator.CreateInstance(trainingDataItem.NavigationPage);
            }
            else
            {
                var selectionDisplay = new SelectionDisplay(button.Content as string);
                this.trainingRegionGrid.Children.Add(selectionDisplay);
                
                // Selection dialog covers the entire interact-able area, so the current press interaction
                // should be completed. Otherwise hover capture will allow the button to be clicked again within
                // the same interaction (even whilst no longer visible).
                selectionDisplay.Focus();

                // Since the selection dialog covers the entire interact-able area, we should also complete
                // the current interaction of all other pointers.  This prevents other users interacting with elements
                // that are no longer visible.

                e.Handled = true;
            }
        }
    }
}