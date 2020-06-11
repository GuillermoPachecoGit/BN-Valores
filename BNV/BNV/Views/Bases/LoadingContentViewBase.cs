using Lottie.Forms;
using Xamarin.Forms;

namespace BNV.Views.Bases
{
    public class LoadingContentViewBase : ContentView
    {
        #region Properties

        /// <summary>
        /// Default animation file
        /// </summary>
        private const string DEFAULT_ANIMATION_FILE = "loader.json";

        /// <summary>
        /// Grid container
        /// </summary>
        private readonly Grid layout;

        /// <summary>
        /// Is busy
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return (bool)GetValue(IsBusyProperty);
            }
            set
            {
                SetValue(IsBusyProperty, value);
            }
        }

        /// <summary>
        /// IsBusy bindable property
        /// </summary>
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy),
                                                                 typeof(bool),
                                                                 typeof(LoadingContentViewBase),
                                                                 false);

        /// <summary>
        /// Animation file name
        /// </summary>
        public string AnimationFile
        {
            get
            {
                return (string)GetValue(AnimationFileProperty);
            }
            set
            {
                SetValue(AnimationFileProperty, value);
            }
        }

        /// <summary>
        /// AnimationFile bindable property
        /// </summary>
        public static readonly BindableProperty AnimationFileProperty = BindableProperty.Create(nameof(AnimationFile),
                                                                                                typeof(string),
                                                                                                typeof(LoadingContentViewBase),
                                                                                                DEFAULT_ANIMATION_FILE);

        /// <summary>
        /// Content
        /// </summary>
        public new View Content
        {
            get
            {
                return GetValue(ContentProperty) as View;
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }

        /// <summary>
        /// Content bindable property
        /// </summary>
        public new static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content),
                                                                      typeof(View),
                                                                      typeof(LoadingContentViewBase),
                                                                      propertyChanged: OnContentChanged);

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Class constructor
        /// </summary>
        public LoadingContentViewBase()
        {
            layout = new Grid();
            base.Content = layout;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Fire when Content is Changed
        /// </summary>
        /// <param name="bindable">Bindable object</param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is LoadingContentViewBase loadingContentView)
            {
                loadingContentView.SetContent(newValue as View);
            }
        }

        /// <summary>
        /// Set the content
        /// </summary>
        /// <param name="view">View</param>
        private void SetContent(View view)
        {
            try
            {
                if (view != null)
                {
                    layout.Children.Remove(view);
                    layout.Children.Add(view, left: default, top: default);
                }

                SetLoadingControl();
            }
            catch
            {
                //If the contents could not be placed the flow would continue in a normal way
            }
        }

        /// <summary>
        /// Set the loading control
        /// </summary>
        public void SetLoadingControl()
        {
            var backLoader = new BoxView
            {
                Opacity = 0.6,
                BackgroundColor = Color.Black
            };

            var activityIndicator = new AnimationView
            {
                Animation = AnimationFile,
                Loop = true,
                IsEnabled = true,
                IsPlaying = true,
                IsVisible = false,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(130, default(int))
            };

            layout.Children.Add(backLoader, left: default, top: default);
            activityIndicator.SetBinding(IsVisibleProperty, nameof(IsBusy));
            backLoader.SetBinding(IsVisibleProperty, nameof(IsBusy));
            layout.Children.Add(activityIndicator, left: default, top: default);
        }

        #endregion Methods
    }
}
