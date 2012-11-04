﻿using System;
using System.Windows.Forms;
using PopupWindow;

namespace PopupWindow
{
    public class PopupControll {
        private class PopupWindow : IDisposable {
            public int Delay {
                get {
                    return popup.Delay;
                }
                set {
                    popup.Delay = value;
                }
            }
            private PopupNotifier popup;
            public PopupWindow() {
                popup = new PopupNotifier();
                popup.AnimationDuration = 1000;
                popup.AnimationInterval = 1;
                popup.BodyColor = System.Drawing.Color.FromArgb(0,0,0);
                popup.BorderColor = System.Drawing.Color.FromArgb(0,0,0);
                popup.ContentColor = System.Drawing.Color.FromArgb(255,255,255);
                popup.ContentFont = new System.Drawing.Font("Tahoma",8F);
                popup.ContentHoverColor = System.Drawing.Color.FromArgb(255,255,255);
                popup.ContentPadding = new Padding(0);
                popup.Delay = 15000;
                popup.GradientPower = 100;
                popup.HeaderHeight = 1;
                popup.Scroll = true;
                popup.ShowCloseButton = false;
                popup.ShowGrip = false;
                popup.ClickForm += new EventHandler(popup_ClickForm);
            }
            private void popup_ClickForm(object sender,EventArgs args) {
                Form popup = sender as Form;
                popup.Hide();
            }
            public void Show(string message) {
                popup.Hide();
                popup.ContentText = message;
                popup.Popup();
            }
            public void Hide() {
                popup.Hide();
            }
            public void Dispose() {
                popup.Dispose();
            }
        }
        public int Delay {
            get;
            set;
        }

        public PopupControll() {
            this.Delay = 10000;
        }
        public void Show(string message) {
            PopupWindow popup = this.SettingsApply(new PopupWindow());
            popup.Show(message);
        }
        private PopupWindow SettingsApply(PopupWindow popup) {
            popup.Delay = this.Delay;
            return popup;
        }
    }
}
