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


            App app = ((App)Application.Current);

            // Use the default sensor

            //// Add in display content
            var sampleDataSource = SampleDataSource.GetGroup("Group-1");
            this.itemsControl.ItemsSource = sampleDataSource;
        }
    }
}