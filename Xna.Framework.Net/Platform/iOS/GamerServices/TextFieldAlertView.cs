// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace Microsoft.Xna.Framework.GamerServices
{
    [CLSCompliant(false)]
    public class TextFieldAlertView : UIAlertView
    {
        private UITextField _tf = null;
        private bool _secureTextEntry;

        private string _initEditValue;
        private string _placeHolderValue;

        public TextFieldAlertView() : this(false) { }

        [CLSCompliant(false)]
        public TextFieldAlertView(bool secureTextEntry, string title, string message, UIAlertViewDelegate alertViewDelegate, string cancelBtnTitle, params string[] otherButtons)
            : base(title, message, alertViewDelegate, cancelBtnTitle, otherButtons)
        {
            InitializeControl(secureTextEntry);
        }

        public TextFieldAlertView(bool secureTextEntry)
        {
            InitializeControl(secureTextEntry);
        }

        public TextFieldAlertView(bool secureTextEntry, string initEditValue, string placeHolderValue)
        {
            InitializeControl(secureTextEntry);
            _initEditValue = initEditValue;
            _placeHolderValue = placeHolderValue;
        }

        private void InitializeControl(bool secureTextEntry)
        {
            _secureTextEntry = secureTextEntry;
            this.AddButton("Cancel");
            this.AddButton("Ok");

            // build out the text field
            _tf = ComposeTextFieldControl(_secureTextEntry);

            // add the text field to the alert view
            this.AddSubview(_tf);
        }

        public string EnteredText
        {
            get
            {
                return _tf.Text;
            }
        }

        public override void LayoutSubviews()
        {
            // layout the stock UIAlertView control
            base.LayoutSubviews();

            // We can only force it to become a First Responder after it has been added to the MainView.
            _tf.BecomeFirstResponder();
        }

        private UITextField ComposeTextFieldControl(bool secureTextEntry)
        {
            UITextField textField = new UITextField(new System.Drawing.RectangleF(12f, 45f, 260f, 25f));
            textField.BackgroundColor = UIColor.White;
            textField.UserInteractionEnabled = true;
            textField.AutocorrectionType = UITextAutocorrectionType.No;
            textField.AutocapitalizationType = UITextAutocapitalizationType.None;
            textField.ReturnKeyType = UIReturnKeyType.Done;
            textField.SecureTextEntry = secureTextEntry;

            textField.Text = _initEditValue;
            textField.Placeholder = _placeHolderValue;
            textField.BecomeFirstResponder();
            return textField;
        }

        public override void Show()
        {
            base.Show();

            // shift the contents of the alert view around to accomodate the extra text field
            this.AdjustControlSize();
        }

        private void AdjustControlSize()
        {
            nfloat tfExtH = _tf.Frame.Size.Height + 16.0f;

            CGRect frame = new CGRect(this.Frame.X,
                                              this.Frame.Y - tfExtH / 2,
                                              this.Frame.Size.Width,
                                              this.Frame.Size.Height + tfExtH);
            this.Frame = frame;

            foreach (var view in this.Subviews)
            {
                if (view is UIControl)
                {
                    view.Frame = new CGRect(view.Frame.X,
                                                view.Frame.Y + tfExtH,
                                                view.Frame.Size.Width,
                                                view.Frame.Size.Height);
                }
            }
        }
    }
}