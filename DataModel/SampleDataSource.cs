//------------------------------------------------------------------------------
// <copyright file="SampleDataSource.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.ControlsBasics.DataModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Samples.Kinect.ControlsBasics.Common;
    using System.Globalization;

    // The data model defined by this file serves as a representative example of a strongly-typed
    // model that supports notification when members are added, removed, or modified.  The property
    // names chosen coincide with data bindings in the standard item templates.
    // Applications may use this model as a starting point and build on it, or discard it entirely and
    // replace it with something appropriate to their needs.

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]

    ///////////////////////////////////////
    // 메인 리스트의 아이템을 구성하는 부분
    ///////////////////////////////////////
    public sealed class SampleDataSource
    {
        private static SampleDataSource sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataCollection> allGroups = new ObservableCollection<SampleDataCollection>();

        private static Uri darkGrayImage = new Uri("Assets/DarkGray.png", UriKind.Relative);
        private static Uri mediumGrayImage = new Uri("assets/mediumGray.png", UriKind.Relative);
        private static Uri lightGrayImage = new Uri("assets/lightGray.png", UriKind.Relative);
        private static Uri sideLateralRaiseImage = new Uri("Images/beach.jpg", UriKind.Relative);

        public SampleDataSource()
        {
            string itemContent = string.Format(
                                    CultureInfo.CurrentCulture,
                                    "Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                                    "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var group1 = new SampleDataCollection(
                    "Group-1",
                    "Group Title: 3",
                    "Group Subtitle: 3",
                    "Assets/lightGray.png",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group1.Items.Add(
                    new SampleDataItem(
                        "Group-1-Item-1",
                        "운동목록", // 해당 아이템의 제목
                        string.Empty,
                        "Images/health.jpg", // 해당 아이템의 이미지 URL
                        "Several types of buttons with custom styles",
                        itemContent,
                        group1,
                        typeof(TrainingList))); // 해당 아이템의 Pages 컴포넌트 이름
            group1.Items.Add(
                    new SampleDataItem(
                        "SideLateralRaise",
                        "사이드레터럴레이즈",
                        string.Empty,
                        "Images/sidelateralraise.png",
                        "SideLateralRaise",
                        itemContent,
                        group1,
                        typeof(SideLateralRaise)));
            group1.Items.Add(
                    new SampleDataItem(
                        "Squat",
                        "스쿼트",
                        string.Empty,
                        "Assets/lightGray.png",
                        "CheckBox and RadioButton controls",
                        itemContent,
                        group1,
                        typeof(Squat)));
            group1.Items.Add(
                    new SampleDataItem(
                        "DumbbellShoulderPress",
                        "덤벨숄더프레스",
                        string.Empty,
                        "Assets/lightGray.png",
                        "ScrollViewer control hosting a photo, enabling scrolling and zooming.",
                        itemContent,
                        group1,
                        typeof(Squat)));
            this.AllGroups.Add(group1);
        }

        public ObservableCollection<SampleDataCollection> AllGroups
        {
            get { return this.allGroups; }
        }

        public static SampleDataCollection GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }

        public static SampleDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = sampleDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }
    }

    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataCollection"/> that
    /// defines properties common to both.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileHeaderFileNameDocumentationMustMatchTypeName", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]


    // Uri로 되어있던 imagePath를 string으로 변경
    public abstract class SampleDataCommon : BindableBase
    {
        /// <summary>
        /// baseUri for image loading purposes
        /// </summary>
        private static Uri baseUri = new Uri("pack://application:,,,/");

        /// <summary>
        /// Field to store uniqueId
        /// </summary>
        private string uniqueId = string.Empty;

        /// <summary>
        /// Field to store title
        /// </summary>
        private string title = string.Empty;

        /// <summary>
        /// Field to store subtitle
        /// </summary>
        private string subtitle = string.Empty;

        /// <summary>
        /// Field to store description
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// Field to store image
        /// </summary>
        private ImageSource image = null;

        /// <summary>
        /// Field to store image path
        /// </summary>
        private string imagePath = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataCommon" /> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of this item.</param>
        /// <param name="title">The title of this item.</param>
        /// <param name="subtitle">The subtitle of this item.</param>
        /// <param name="imagePath">A relative path of the image for this item.</param>
        /// <param name="description">A description of this item.</param>
        protected SampleDataCommon(string uniqueId, string title, string subtitle, string imagePath, string description)
        {
            this.uniqueId = uniqueId;
            this.title = title;
            this.subtitle = subtitle;
            this.description = description;
            this.imagePath = imagePath;
        }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public string UniqueId
        {
            get { return this.uniqueId; }
            set { this.SetProperty(ref this.uniqueId, value); }
        }

        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        public string Subtitle
        {
            get { return this.subtitle; }
            set { this.SetProperty(ref this.subtitle, value); }
        }

        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        public ImageSource Image
        {
            get
            {
                if (this.image == null && this.imagePath != null)
                {
                    this.image = new BitmapImage(new Uri(SampleDataCommon.baseUri, this.imagePath));
                }

                return this.image;
            }

            set
            {
                this.imagePath = null;
                this.SetProperty(ref this.image, value);
            }
        }

        public void SetImage(string path)
        {
            this.image = null;
            this.imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]

    // 메인 리스트 아이템의 공통 소스
    public class SampleDataItem : SampleDataCommon
    {
        private string content = string.Empty;
        private SampleDataCollection group;
        private Type navigationPage;

        public SampleDataItem(string uniqueId, string title, string subtitle, string imagePath, string description, string content, SampleDataCollection group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataItem" /> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of this item.</param>
        /// <param name="title">The title of this item.</param>
        /// <param name="subtitle">The subtitle of this item.</param>
        /// <param name="imagePath">A relative path of the image for this item.</param>
        /// <param name="description">A description of this item.</param>
        /// <param name="content">The content of this item.</param>
        /// <param name="group">The group of this item.</param>
        /// <param name="navigationPage">What page should launch when clicking this item.</param>
        public SampleDataItem(string uniqueId, string title, string subtitle, string imagePath, string description, string content, SampleDataCollection group, Type navigationPage)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = navigationPage;
        }

        public string Content
        {
            get { return this.content; }
            set { this.SetProperty(ref this.content, value); }
        }

        public SampleDataCollection Group
        {
            get { return this.group; }
            set { this.SetProperty(ref this.group, value); }
        }

        public Type NavigationPage
        {
            get { return this.navigationPage; }
            set { this.SetProperty(ref this.navigationPage, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>

    // 메인 리스트의 아이템 공통 소스
    public class SampleDataCollection : SampleDataCommon, IEnumerable
    {
        private ObservableCollection<SampleDataItem> items = new ObservableCollection<SampleDataItem>();
        private ObservableCollection<SampleDataItem> topItem = new ObservableCollection<SampleDataItem>();

        public SampleDataCollection(string uniqueId, string title, string subtitle, string imagePath, string description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.Items.CollectionChanged += this.ItemsCollectionChanged;
        }

        public ObservableCollection<SampleDataItem> Items
        {
            get { return this.items; }
        }

        public ObservableCollection<SampleDataItem> TopItems
        {
            get { return this.topItem; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        if (this.TopItems.Count > 12)
                        {
                            this.TopItems.RemoveAt(12);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        this.TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        this.TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        this.TopItems.RemoveAt(12);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        if (this.Items.Count >= 12)
                        {
                            this.TopItems.Add(this.Items[11]);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems[e.OldStartingIndex] = this.Items[e.OldStartingIndex];
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.TopItems.Clear();
                    while (this.TopItems.Count < this.Items.Count && this.TopItems.Count < 12)
                    {
                        this.TopItems.Add(this.Items[this.TopItems.Count]);
                    }

                    break;
            }
        }
    }


    ///////////////////////////////////////////
    // 운동 리스트의 아이템을 구성하는 부분
    ///////////////////////////////////////////
    public sealed class TrainingDataSource
    {
        private static TrainingDataSource trainingDataSource = new TrainingDataSource();

        private ObservableCollection<TrainingDataCollection> allGroups = new ObservableCollection<TrainingDataCollection>();

        private static Uri darkGrayImage = new Uri("Assets/clouds.jpg", UriKind.Relative);
        private static Uri mediumGrayImage = new Uri("assets/clouds.jpg", UriKind.Relative);
        private static Uri lightGrayImage = new Uri("assets/clouds.jpg", UriKind.Relative);
        private static Uri sideLateralRaiseImage = new Uri("Images/beach.jpg", UriKind.Relative);

        public TrainingDataSource()
        {
            string itemContent = string.Format(
                                    CultureInfo.CurrentCulture,
                                    "Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                                    "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var group1 = new TrainingDataCollection(
                    "Group-1",
                    "Group Title: 3",
                    "Group Subtitle: 3",
                    "Assets/lightGray.png",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group1.Items.Add(
                    new TrainingDataItem(
                        "Group-1-Item-1",
                        "사이드레터럴레이즈",
                        string.Empty,
                        "Images/sidelateralraise.png",
                        "Several types of buttons with custom styles",
                        itemContent,
                        group1,
                        typeof(SideLateralRaise)));
            group1.Items.Add(
                    new TrainingDataItem(
                        "SideLateralRaise",
                        "해머컬",
                        string.Empty,
                        "Images/hammercurl.png",
                        "SideLateralRaise",
                        itemContent,
                        group1,
                        typeof(SideLateralRaise)));
            group1.Items.Add(
                    new TrainingDataItem(
                        "Group-1-Item-2",
                        "스쿼트",
                        string.Empty,
                        "Images/squat.png",
                        "CheckBox and RadioButton controls",
                        itemContent,
                        group1,
                        typeof(Squat)));
            group1.Items.Add(
                    new TrainingDataItem(
                        "Group-1-Item-5",
                        "숄더프레스",
                        string.Empty,
                        "Images/shoulderpress.png",
                        "ScrollViewer control hosting a photo, enabling scrolling and zooming.",
                        itemContent,
                        group1,
                        typeof(DumbbellShoulderPress)));
            this.AllGroups.Add(group1);
        }

        // ObservableCollection<> : 항목이 추가 또는 제거되거나 전체 목록이 새로 고쳐진 경우 알림을 제공하는 동적 데이터 컬렉션
        public ObservableCollection<TrainingDataCollection> AllGroups
        {
            get { return this.allGroups; }
        }

        public static TrainingDataCollection GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = trainingDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }

        public static TrainingDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = trainingDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }
    }

    // Uri로 되어있던 imagePath를 string으로 변경
    public abstract class TrainingDataCommon : BindableBase
    {
        /// <summary>
        /// baseUri for image loading purposes
        /// </summary>
        private static Uri baseUri = new Uri("pack://application:,,,/");

        /// <summary>
        /// Field to store uniqueId
        /// </summary>
        private string uniqueId = string.Empty;

        /// <summary>
        /// Field to store title
        /// </summary>
        private string title = string.Empty;

        /// <summary>
        /// Field to store subtitle
        /// </summary>
        private string subtitle = string.Empty;

        /// <summary>
        /// Field to store description
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// Field to store image
        /// </summary>
        private ImageSource image = null;

        /// <summary>
        /// Field to store image path
        /// </summary>
        private string imagePath = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingDataCommon" /> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of this item.</param>
        /// <param name="title">The title of this item.</param>
        /// <param name="subtitle">The subtitle of this item.</param>
        /// <param name="imagePath">A relative path of the image for this item.</param>
        /// <param name="description">A description of this item.</param>
        protected TrainingDataCommon(string uniqueId, string title, string subtitle, string imagePath, string description)
        {
            this.uniqueId = uniqueId;
            this.title = title;
            this.subtitle = subtitle;
            this.description = description;
            this.imagePath = imagePath;
        }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public string UniqueId
        {
            get { return this.uniqueId; }
            set { this.SetProperty(ref this.uniqueId, value); }
        }

        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        public string Subtitle
        {
            get { return this.subtitle; }
            set { this.SetProperty(ref this.subtitle, value); }
        }

        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        public ImageSource Image
        {
            get
            {
                if (this.image == null && this.imagePath != null)
                {
                    this.image = new BitmapImage(new Uri(TrainingDataCommon.baseUri, this.imagePath));
                }

                return this.image;
            }

            set
            {
                this.imagePath = null;
                this.SetProperty(ref this.image, value);
            }
        }

        public void SetImage(string path)
        {
            this.image = null;
            this.imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    // 메인 리스트 아이템의 공통 소스
    // 각 아이템의 로직
    public class TrainingDataItem : TrainingDataCommon
    {
        private string content = string.Empty;
        private TrainingDataCollection group;
        private Type navigationPage;

        public TrainingDataItem(string uniqueId, string title, string subtitle, string imagePath, string description, string content, TrainingDataCollection group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingDataItem" /> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of this item.</param>
        /// <param name="title">The title of this item.</param>
        /// <param name="subtitle">The subtitle of this item.</param>
        /// <param name="imagePath">A relative path of the image for this item.</param>
        /// <param name="description">A description of this item.</param>
        /// <param name="content">The content of this item.</param>
        /// <param name="group">The group of this item.</param>
        /// <param name="navigationPage">What page should launch when clicking this item.</param>
        public TrainingDataItem(string uniqueId, string title, string subtitle, string imagePath, string description, string content, TrainingDataCollection group, Type navigationPage)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = navigationPage;
        }

        public string Content
        {
            get { return this.content; }
            set { this.SetProperty(ref this.content, value); }
        }

        public TrainingDataCollection Group
        {
            get { return this.group; }
            set { this.SetProperty(ref this.group, value); }
        }

        public Type NavigationPage
        {
            get { return this.navigationPage; }
            set { this.SetProperty(ref this.navigationPage, value); }
        }
    }

    // 메인 리스트의 아이템 공통 소스
    public class TrainingDataCollection : TrainingDataCommon, IEnumerable
    {
        private ObservableCollection<TrainingDataItem> items = new ObservableCollection<TrainingDataItem>();
        private ObservableCollection<TrainingDataItem> topItem = new ObservableCollection<TrainingDataItem>();

        public TrainingDataCollection(string uniqueId, string title, string subtitle, string imagePath, string description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.Items.CollectionChanged += this.ItemsCollectionChanged;
        }

        public ObservableCollection<TrainingDataItem> Items
        {
            get { return this.items; }
        }

        public ObservableCollection<TrainingDataItem> TopItems
        {
            get { return this.topItem; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        if (this.TopItems.Count > 12)
                        {
                            this.TopItems.RemoveAt(12);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        this.TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        this.TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        this.TopItems.RemoveAt(12);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        if (this.Items.Count >= 12)
                        {
                            this.TopItems.Add(this.Items[11]);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems[e.OldStartingIndex] = this.Items[e.OldStartingIndex];
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.TopItems.Clear();
                    while (this.TopItems.Count < this.Items.Count && this.TopItems.Count < 12)
                    {
                        this.TopItems.Add(this.Items[this.TopItems.Count]);
                    }

                    break;
            }
        }
    }
}
