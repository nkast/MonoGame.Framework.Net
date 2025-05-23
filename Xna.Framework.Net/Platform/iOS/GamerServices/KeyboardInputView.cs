// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace Microsoft.Xna.Framework
{
    class KeyboardInputView : UIScrollView
    {
        private static readonly PaddingF TitleMargin = new PaddingF(10, 7, 10, 2);
        private static readonly PaddingF DescriptionMargin = new PaddingF(12, 2, 10, 5);
        private static readonly PaddingF TextFieldMargin = new PaddingF(10, 5, 10, 5);

        private readonly UIToolbar _toolbar;
        private readonly UILabel _title;
        private readonly UILabel _description;
        private readonly UITextField _textField;
        private readonly UIScrollView _textFieldContainer;

        public KeyboardInputView(RectangleF frame)
            : base(frame)
        {
            _toolbar = new UIToolbar(frame);

            var toolbarItems = new UIBarButtonItem[]
            {
                new UIBarButtonItem (UIBarButtonSystemItem.Cancel, CancelButton_Tapped),
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace, null),
                new UIBarButtonItem (UIBarButtonSystemItem.Done, DoneButton_Tapped)
            };

            _toolbar.SetItems(toolbarItems, false);
            _toolbar.SizeToFit();

            _title = new UILabel(RectangleF.Empty);
            _title.Font = UIFont.SystemFontOfSize(UIFont.LabelFontSize * 1.2f);
            _title.BackgroundColor = UIColor.Clear;
            _title.LineBreakMode = UILineBreakMode.TailTruncation;
            _title.Lines = 2;

            _description = new UILabel(RectangleF.Empty);
            _description.Font = UIFont.SystemFontOfSize(UIFont.LabelFontSize);
            _description.TextColor = UIColor.DarkTextColor.ColorWithAlpha(0.95f);
            _description.BackgroundColor = UIColor.Clear;
            _title.LineBreakMode = UILineBreakMode.TailTruncation;
            _description.Lines = 2;

            _textFieldContainer = new UIScrollView(new RectangleF(0, 0, 100, 100));

            _textField = new UITextField(_textFieldContainer.Bounds);
            _textField.AutoresizingMask =
                UIViewAutoresizing.FlexibleWidth |
                UIViewAutoresizing.FlexibleHeight;
            _textField.BorderStyle = UITextBorderStyle.RoundedRect;
            _textField.Delegate = new TextFieldDelegate(this);

            _textFieldContainer.Add(_textField);

            Add(_toolbar);
            Add(_title);
            Add(_description);
            Add(_textFieldContainer);

            AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
            AutosizesSubviews = false;
            Opaque = true;
            BackgroundColor = UIColor.FromRGB(0xC5, 0xCC, 0xD4);

            SetNeedsLayout();
        }

        #region Properties

        public string Title
        {
            get { return _title.Text; }
            set
            {
                if (_title.Text != value)
                {
                    _title.Text = value;
                    SetNeedsLayout();
                }
            }
        }

        public new string Description
        {
            get { return _description.Text; }
            set
            {
                if (_description.Text != value)
                {
                    _description.Text = value;
                    SetNeedsLayout();
                }
            }
        }

        public string Text
        {
            get { return _textField.Text; }
            set
            {
                if (_textField.Text != value)
                {
                    _textField.Text = value;
                }
            }
        }

        public bool UsePasswordMode
        {
            get { return _textField.SecureTextEntry; }
            set
            {
                if (_textField.SecureTextEntry != value)
                {
                    _textField.SecureTextEntry = value;
                }
            }
        }

        #endregion Properties

        #region Events

        public event EventHandler<EventArgs> InputAccepted;
        public event EventHandler<EventArgs> InputCanceled;

        #endregion Events

        public void ActivateFirstField()
        {
            _textField.BecomeFirstResponder();
        }

        public void ScrollActiveFieldToVisible()
        {
            if (!_textField.IsFirstResponder)
                return;

            var bounds = Bounds;
            bounds.X += ContentInset.Left;
            bounds.Width -= (ContentInset.Left + ContentInset.Right);
            bounds.Y += ContentInset.Top;
            bounds.Height -= (ContentInset.Top + ContentInset.Bottom);

            if (!bounds.Contains(_textFieldContainer.Frame))
            {
                ScrollRectToVisible(_textFieldContainer.Frame, true);
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            _textField.ResignFirstResponder();
        }

        public override void LayoutSubviews()
        {
            _toolbar.SizeToFit();

            var titleSize = SizeThatFitsWidth(_title, Bounds.Width - TitleMargin.Horizontal);
            _title.Frame = new CGRect(
                TitleMargin.Left, _toolbar.Bounds.Bottom + TitleMargin.Top,
                titleSize.Width, titleSize.Height);

            var descriptionSize = SizeThatFitsWidth(
                _description, Bounds.Width - DescriptionMargin.Horizontal);
            _description.Frame = new CGRect(
                DescriptionMargin.Left,
                _title.Frame.Bottom + TitleMargin.Bottom + DescriptionMargin.Top,
                descriptionSize.Width, descriptionSize.Height);

            var textFieldSize = _textField.SizeThatFits(
                new CGSize(Bounds.Width - TextFieldMargin.Horizontal, Bounds.Height));
            _textFieldContainer.Frame = new CGRect(
                TextFieldMargin.Left,
                _description.Frame.Bottom + DescriptionMargin.Bottom + TextFieldMargin.Top,
                Bounds.Width - TextFieldMargin.Horizontal, textFieldSize.Height);

            ContentSize = new CGSize(Bounds.Width, _textFieldContainer.Frame.Bottom + TextFieldMargin.Bottom);
        }

        private static CGSize SizeThatFitsWidth(UILabel label, nfloat width)
        {
            var font = label.Font;
            return label.SizeThatFits(new CGSize(width, font.LineHeight * label.Lines));
        }

        private void DoneButton_Tapped(object sender, EventArgs e)
        {
            OnInputAccepted(e);
        }

        private void CancelButton_Tapped(object sender, EventArgs e)
        {
            OnInputCanceled(e);
        }

        private void OnInputAccepted(EventArgs e)
        {
            var handler = InputAccepted;
            if (handler != null)
                handler(this, e);
        }

        private void OnInputCanceled(EventArgs e)
        {
            var handler = InputCanceled;
            if (handler != null)
                handler(this, e);
        }

        private class TextFieldDelegate : UITextFieldDelegate
        {
            private readonly KeyboardInputView _owner;
            public TextFieldDelegate(KeyboardInputView owner)
            {
                if (owner == null)
                    throw new ArgumentNullException("owner");
                _owner = owner;
            }

            public override bool ShouldReturn(UITextField textField)
            {
                _owner.OnInputAccepted(EventArgs.Empty);
                return true;
            }
        }
    }
}

