using System;
using Uno.UI;
using Windows.UI.Xaml.Controls.Primitives;

#if __IOS__
using UIKit;
#endif

namespace Windows.UI.Xaml.Controls
{
	/// <summary>
	/// Represents a switch that can be toggled between two states.
	/// </summary>
	public partial class ToggleSwitch : Control, IFrameworkTemplatePoolAware
	{
		/// <summary>
		/// Initializes a new instance of the ToggleSwitch class.
		/// </summary>
		public ToggleSwitch()
		{
			DefaultStyleKey = typeof(ToggleSwitch);

			//TODO Uno specific: Calling prepare state here, but should be called from DXamlCore.
			PrepareState();
		}

		/// <summary>
		/// Occurs when "On"/"Off" state changes for this ToggleSwitch.
		/// </summary>
		public event RoutedEventHandler Toggled;

		private protected override void OnLoaded()
		{
			base.OnLoaded();

			OnLoadedPartial();
		}

		partial void OnLoadedPartial();

		private bool IsNativeTemplate
		{
			get
			{
#if __ANDROID__
				return this.FindFirstChild<Uno.UI.Controls.BindableSwitchCompat>() != null;
#elif __IOS__
				return this.FindFirstChild<Uno.UI.Views.Controls.BindableUISwitch>() != null;
#else
				return false;
#endif
			}
		}

		#region Header (DP)

		/// <summary>
		/// Gets or sets the header content.
		/// </summary>
		public object Header
		{
			get => GetValue(HeaderProperty);
			set => SetValue(HeaderProperty, value);
		}

		/// <summary>
		/// Identifies the Header dependency property.
		/// </summary>
		public static DependencyProperty HeaderProperty { get; } =
			DependencyProperty.Register(nameof(Header), typeof(object), typeof(ToggleSwitch), new FrameworkPropertyMetadata(null));

		#endregion

		#region HeaderTemplate (DP)

		/// <summary>
		/// Gets or sets the DataTemplate used to display the control's header.
		/// </summary>
		public DataTemplate HeaderTemplate
		{
			get => (DataTemplate)GetValue(HeaderTemplateProperty);
			set => SetValue(HeaderTemplateProperty, value);
		}

		/// <summary>
		/// Identifies the HeaderTemplate dependency property.
		/// </summary>
		public static DependencyProperty HeaderTemplateProperty { get; } =
			DependencyProperty.Register(nameof(HeaderTemplate), typeof(DataTemplate), typeof(ToggleSwitch), new FrameworkPropertyMetadata(null, options: FrameworkPropertyMetadataOptions.ValueDoesNotInheritDataContext));

		#endregion

		#region IsOn (DP)

		/// <summary>
		/// Gets or sets a value that declares whether the state of the ToggleSwitch is "On".
		/// </summary>
		public bool IsOn
		{
			get => (bool)GetValue(IsOnProperty);
			set => SetValue(IsOnProperty, value);
		}

		/// <summary>
		/// Identifies the IsOn dependency property.
		/// </summary>
		public static DependencyProperty IsOnProperty { get; } =
			DependencyProperty.Register(nameof(IsOn), typeof(bool), typeof(ToggleSwitch), new FrameworkPropertyMetadata(false));

		#endregion

		#region OffContent (DP)

		/// <summary>
		/// Provides the object content that should be displayed using the OffContentTemplate when this ToggleSwitch has state of "Off".
		/// </summary>
		public object OffContent
		{
			get => GetValue(OffContentProperty);
			set => SetValue(OffContentProperty, value);
		}

		/// <summary>
		/// Identifies the OffContent dependency property.
		/// </summary>
		public static DependencyProperty OffContentProperty { get; } =
			DependencyProperty.Register(nameof(OffContent), typeof(object), typeof(ToggleSwitch), new FrameworkPropertyMetadata(null));

		#endregion

		#region OffContentTemplate (DP)

		/// <summary>
		/// Gets or sets the DataTemplate used to display the control's content while in "Off" state.
		/// </summary>
		public DataTemplate OffContentTemplate
		{
			get => (DataTemplate)GetValue(OffContentTemplateProperty);
			set => SetValue(OffContentTemplateProperty, value);
		}

		/// <summary>
		/// Identifies the OffContentTemplate dependency property.
		/// </summary>
		public static DependencyProperty OffContentTemplateProperty { get; } =
			DependencyProperty.Register(nameof(OffContentTemplate), typeof(DataTemplate), typeof(ToggleSwitch), new FrameworkPropertyMetadata(null, options: FrameworkPropertyMetadataOptions.ValueDoesNotInheritDataContext));

		#endregion

		#region OnContent (DP)

		/// <summary>
		/// Provides the object content that should be displayed using the OnContentTemplate when this ToggleSwitch has state of "On".
		/// </summary>
		public object OnContent
		{
			get => GetValue(OnContentProperty);
			set => SetValue(OnContentProperty, value);
		}

		/// <summary>
		/// Identifies the OnContent dependency property.
		/// </summary>
		public static DependencyProperty OnContentProperty { get; } =
			DependencyProperty.Register(nameof(OnContent), typeof(object), typeof(ToggleSwitch), new FrameworkPropertyMetadata(null));

		#endregion

		#region OnContentTemplate (DP)

		/// <summary>
		/// Gets or sets the DataTemplate used to display the control's content while in "On" state.
		/// </summary>
		public DataTemplate OnContentTemplate
		{
			get => (DataTemplate)GetValue(OnContentTemplateProperty);
			set => SetValue(OnContentTemplateProperty, value);
		}

		/// <summary>
		/// Identifies the OnContentTemplate dependency property.
		/// </summary>
		public static DependencyProperty OnContentTemplateProperty { get; } =
			DependencyProperty.Register(nameof(OnContentTemplate), typeof(DataTemplate), typeof(ToggleSwitch), new FrameworkPropertyMetadata(null, options: FrameworkPropertyMetadataOptions.ValueDoesNotInheritDataContext));

		#endregion

		/// <summary>
		/// Gets an object that provides calculated values that can be referenced as TemplateBinding sources when defining templates for a ToggleSwitch control.
		/// </summary>
		public ToggleSwitchTemplateSettings TemplateSettings { get; private set; }

		private double GetEndAbsoluteOffset()
		{
			var minOffset = 0;
			var maxOffset = GetMaxOffset();
			var startOffset = IsOn ? maxOffset : 0;
			var absoluteOffset = startOffset;
			absoluteOffset = Math.Max(minOffset, absoluteOffset);
			absoluteOffset = Math.Min(maxOffset, absoluteOffset);
			return absoluteOffset;
		}

		private double GetMaxOffset()
		{
			if (_tpKnobBounds == null || _tpKnob == null)
			{
				return 0;
			}

			return _tpKnobBounds.ActualWidth - _tpKnob.ActualWidth;
		}

		private void ForceSwitchKnobEndPosition()
		{
			if (_spKnobTransform != null)
			{
				_spKnobTransform.X = GetEndAbsoluteOffset();
			}
		}

		public void OnTemplateRecycled() => IsOn = false;

		internal void AutomationPeerToggle() => IsOn = !IsOn;
	}
}
