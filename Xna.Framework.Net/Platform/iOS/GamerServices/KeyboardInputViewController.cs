// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Drawing;

using Foundation;
using UIKit;

namespace Microsoft.Xna.Framework {
	class KeyboardInputViewController : UIViewController {
		private readonly string _titleText;
		private readonly string _descriptionText;
		private readonly string _defaultText;
		private readonly bool _usePasswordMode;
        private UIViewController _gameViewController;

		public KeyboardInputViewController (
			string titleText, string descriptionText, string defaultText, bool usePasswordMode, UIViewController gameViewController)
		{
			_titleText = titleText;
			_descriptionText = descriptionText;
			_defaultText = defaultText;
			_usePasswordMode = usePasswordMode;
            _gameViewController = gameViewController;
		}

		private readonly List<NSObject> _keyboardObservers = new List<NSObject> ();
		public override void LoadView ()
		{
			var view = new KeyboardInputView (new RectangleF (0, 0, 240, 320));
			view.Title = _titleText;
			view.Description = _descriptionText;
			view.Text = _defaultText;
			view.UsePasswordMode = _usePasswordMode;

			view.ActivateFirstField ();

			base.View = view;

			_keyboardObservers.Add (
				NSNotificationCenter.DefaultCenter.AddObserver(
					UIKeyboard.DidShowNotification, Keyboard_DidShow));
			_keyboardObservers.Add (
				NSNotificationCenter.DefaultCenter.AddObserver(
					UIKeyboard.WillHideNotification, Keyboard_WillHide));
		}

		public new KeyboardInputView View {
			get { return (KeyboardInputView) base.View; }
		}

        [Obsolete]
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();

			NSNotificationCenter.DefaultCenter.RemoveObservers (_keyboardObservers);
			_keyboardObservers.Clear ();

            _gameViewController = null;
		}

		private void Keyboard_DidShow(NSNotification notification)
		{
			var keyboardSize = UIKeyboard.FrameBeginFromNotification (notification).Size;

			if (InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft ||
			    InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
            {
                var tmpkeyboardSize = keyboardSize;
				keyboardSize.Width = (nfloat)Math.Max(tmpkeyboardSize.Height, tmpkeyboardSize.Width);
				keyboardSize.Height = (nfloat)Math.Min(tmpkeyboardSize.Height, tmpkeyboardSize.Width);
			}

			var view = (KeyboardInputView)View;
			var contentInsets = new UIEdgeInsets(0f, 0f, keyboardSize.Height, 0f);
			view.ContentInset = contentInsets;
			view.ScrollIndicatorInsets = contentInsets;

			view.ScrollActiveFieldToVisible ();
		}

		private void Keyboard_WillHide(NSNotification notification)
		{
			var view = (KeyboardInputView)View;
			view.ContentInset = UIEdgeInsets.Zero;
			view.ScrollIndicatorInsets = UIEdgeInsets.Zero;
		}

        #region Autorotation for iOS 5 or older
        [Obsolete]
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
            var requestedOrientation = OrientationConverter.ToDisplayOrientation(toInterfaceOrientation);
            var supportedOrientations = (_gameViewController as iOSGameViewController).SupportedOrientations;

            return (supportedOrientations & requestedOrientation) != 0;
		}
        #endregion

        #region Autorotation for iOS 6 or newer
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
        {
            return OrientationConverter.ToUIInterfaceOrientationMask((_gameViewController as iOSGameViewController).SupportedOrientations);
        }
        
        public override bool ShouldAutorotate ()
        {
            return true;
        }
        
        public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation ()
        {
            return _gameViewController.PreferredInterfaceOrientationForPresentation();
        }
        #endregion

		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate(toInterfaceOrientation, duration);
			View.LayoutSubviews ();
		}
	}

	struct PaddingF {
		public float Left;
		public float Top;
		public float Right;
		public float Bottom;

		public float Horizontal {
			get { return Left + Right; }
		}

		public float Vertical {
			get { return Top + Bottom; }
		}

		public PaddingF (float all)
		{
			Left = Top = Right = Bottom = all;
		}

		public PaddingF (float left, float top, float right, float bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}
	}
}

