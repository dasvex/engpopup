using System;
using System.Windows.Forms;
using PopupWindow;

namespace PopupWindow
{
    class PopupControll
    {
        private PopupNotifier popup;
        public PopupControll()
        {
            popup = new PopupNotifier();
            popup.AnimationDuration = 1000;
            popup.AnimationInterval = 1;
            popup.BodyColor = System.Drawing.Color.FromArgb(0, 0, 0);
            popup.BorderColor = System.Drawing.Color.FromArgb(0, 0, 0);
            popup.ContentColor = System.Drawing.Color.FromArgb(255, 255, 255);
            popup.ContentFont = new System.Drawing.Font("Tahoma", 8F);
            popup.ContentHoverColor = System.Drawing.Color.FromArgb(255, 255, 255);
            popup.ContentPadding = new Padding(0);
            popup.Delay = 4000;
            popup.GradientPower = 100;
            popup.HeaderHeight = 1;
            popup.Scroll = true;
            popup.ShowCloseButton = false;
            popup.ShowGrip = false;


            popup.ClickForm += new EventHandler(popup_ClickForm);
        }
        private void popup_ClickForm(object sender, EventArgs args)
        {
            this.Hide();
        }
        public void Show(string message)
        {
            popup.Hide();
            /*int height;
            if (message.Length > 60)
                height = (message.Length / 60) * 15;
            else height = 45;
            popup.ContentText = message;
            popup.Size = new System.Drawing.Size(400, height);*/
            popup.ContentText = message;//
            popup.Popup();
        }
        public void Hide()
        {
            popup.Hide();
        }
    }
}
