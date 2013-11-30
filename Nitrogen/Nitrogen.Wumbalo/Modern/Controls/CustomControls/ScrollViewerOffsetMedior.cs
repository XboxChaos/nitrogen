using System.Windows;
using System.Windows.Controls;

namespace Nitrogen.Wumbalo.Modern.Controls.CustomControls
{
	/// <summary>
	/// Mediator that forwards Offset property changes on to a ScrollViewer
	/// instance to enable the animation of Horizontal/VerticalOffset.
	/// </summary>
	public class ScrollViewerOffsetMediator : FrameworkElement
	{
		/// <summary>
		/// ScrollViewer instance to forward Offset changes on to.
		/// </summary>
		public ScrollViewer ScrollViewer
		{
			get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
			set { SetValue(ScrollViewerProperty, value); }
		}
		public static readonly DependencyProperty ScrollViewerProperty =
			DependencyProperty.Register(
				"ScrollViewer",
				typeof(ScrollViewer),
				typeof(ScrollViewerOffsetMediator),
				new PropertyMetadata(OnScrollViewerChanged));
		private static void OnScrollViewerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var mediator = (ScrollViewerOffsetMediator)o;
			var scrollViewer = (ScrollViewer)(e.NewValue);
			if (null != scrollViewer)
			{
				scrollViewer.ScrollToVerticalOffset(mediator.VerticalOffset);
			}
		}

		/// <summary>
		/// VerticalOffset property to forward to the ScrollViewer.
		/// </summary>
		public double VerticalOffset
		{
			get { return (double)GetValue(VerticalOffsetProperty); }
			set { SetValue(VerticalOffsetProperty, value); }
		}
		public static readonly DependencyProperty VerticalOffsetProperty =
			DependencyProperty.Register(
				"VerticalOffset",
				typeof(double),
				typeof(ScrollViewerOffsetMediator),
				new PropertyMetadata(OnVerticalOffsetChanged));
		public static void OnVerticalOffsetChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var mediator = (ScrollViewerOffsetMediator)o;
			if (null != mediator.ScrollViewer)
			{
				mediator.ScrollViewer.ScrollToVerticalOffset((double)(e.NewValue));
			}
		}

		/// <summary>
		/// Multiplier for ScrollableHeight property to forward to the ScrollViewer.
		/// </summary>
		/// <remarks>
		/// 0.0 means "scrolled to top"; 1.0 means "scrolled to bottom".
		/// </remarks>
		public double ScrollableHeightMultiplier
		{
			get { return (double)GetValue(ScrollableHeightMultiplierProperty); }
			set { SetValue(ScrollableHeightMultiplierProperty, value); }
		}
		public static readonly DependencyProperty ScrollableHeightMultiplierProperty =
			DependencyProperty.Register(
				"ScrollableHeightMultiplier",
				typeof(double),
				typeof(ScrollViewerOffsetMediator),
				new PropertyMetadata(OnScrollableHeightMultiplierChanged));
		public static void OnScrollableHeightMultiplierChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var mediator = (ScrollViewerOffsetMediator)o;
			var scrollViewer = mediator.ScrollViewer;
			if (null != scrollViewer)
				scrollViewer.ScrollToVerticalOffset((double)(e.NewValue) * scrollViewer.ScrollableHeight);
		}

		/// <summary>
		/// Multiplier for ScrollableWidth property to forward to the ScrollViewer.
		/// </summary>
		/// <remarks>
		/// 0.0 means "scrolled to left"; 1.0 means "scrolled to right".
		/// </remarks>
		public double ScrollableWidthMultiplier
		{
			get { return (double)GetValue(ScrollableWidthMultiplierProperty); }
			set { SetValue(ScrollableWidthMultiplierProperty, value); }
		}
		public static readonly DependencyProperty ScrollableWidthMultiplierProperty =
			DependencyProperty.Register(
				"ScrollableWidthMultiplier",
				typeof(double),
				typeof(ScrollViewerOffsetMediator),
				new PropertyMetadata(OnScrollableWidthMultiplierChanged));
		public static void OnScrollableWidthMultiplierChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var mediator = (ScrollViewerOffsetMediator)o;
			var scrollViewer = mediator.ScrollViewer;
			if (null != scrollViewer)
				scrollViewer.ScrollToHorizontalOffset((double)(e.NewValue) * scrollViewer.ScrollableWidth);
		}
	}
}
