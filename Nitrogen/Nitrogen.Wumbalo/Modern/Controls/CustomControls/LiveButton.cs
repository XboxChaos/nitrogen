using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Nitrogen.Wumbalo.Modern.Controls.CustomControls
{
	/// <summary>
	/// 
	/// </summary>
	public class LiveButton : Button
	{
		public static DependencyProperty BackgroundImageProperty;
		public static DependencyProperty DescriptionProperty;
		public static DependencyProperty TitleProperty;

		/// <summary>
		/// 
		/// </summary>
		static LiveButton()
		{
			BackgroundImageProperty = DependencyProperty.Register("BackgroundImage", typeof(ImageSource), typeof(LiveButton));
			DescriptionProperty = DependencyProperty.Register("Description", typeof(String), typeof(LiveButton));
			TitleProperty = DependencyProperty.Register("Title", typeof(String), typeof(LiveButton));
		}

		public LiveButton()
		{
			_animationTimer.Interval = new TimeSpan(0, 0, 0, 3);
			_animationTimer.Tick += (sender, args) => DoAnimation();
			_animationTimer.Start();
		}

		private readonly DispatcherTimer _animationTimer = new DispatcherTimer();
		private State _animationState = State.Begin;
		private bool _isAnimating;
		public void DoAnimation()
		{
			// Do checks
			if (_isAnimating) return;
			if (new Random().Next(0, 5) != 3) return;

			// Load storyboard from template resources
			Storyboard storyBoard = null;
			switch (_animationState)
			{
				case State.Begin:
					storyBoard = (Template.Resources["BeginLive"] as Storyboard).Clone();
					break;
				case State.End:
					storyBoard = (Template.Resources["EndLive"] as Storyboard).Clone();
					break;
			}

			// Update checks
			if (storyBoard == null) return;
			switch (_animationState)
			{
				case State.Begin:
					_animationState = State.End;
					_isAnimating = true;
					break;
				case State.End:
					_animationState = State.Begin;
					_isAnimating = true;
					break;
			}

			// Run storyboard
			storyBoard.Completed += (sender, args) => { _isAnimating = false; };
			storyBoard.Begin(this, Template);
		}

		#region Enums

		public enum State
		{
			Begin,
			End
		}

		#endregion

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public ImageSource BackgroundImage
		{
			get { return (ImageSource)GetValue(BackgroundImageProperty); }
			set { SetValue(BackgroundImageProperty, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public String Description
		{
			get { return (String)GetValue(DescriptionProperty); }
			set { SetValue(DescriptionProperty, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public String Title
		{
			get { return (String)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		#endregion
	}
}
