using Awesomium.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TestUI.Helpers;

namespace TestUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void BindMethods(IWebView _webView)
        {
            JSValue result = _webView.CreateGlobalJavascriptObject("app");
            if (result.IsObject)
            {
                JSObject appObject = result;
                appObject.Bind("login", false, new JavascriptMethodEventHandler(Login));
            }
        }

        private void Login(object obj, JavascriptMethodEventArgs jsMethodArgs)
        {
            string username = jsMethodArgs.Arguments[0].ToString();
            string password = jsMethodArgs.Arguments[1].ToString();
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("You must fill in both a username and a password");
                return;
            }
            MessageBox.Show("Username: " + username + Environment.NewLine + "Password: " + password);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainWebUI.ShowContextMenu += Html5_ShowContextMenu;
            var source = new Uri(PathHelper.GetStartupPath() + "/Resources/login.html");
            MainWebUI.Source = source;
            BindMethods(MainWebUI);
        }
                     
        void Html5_ShowContextMenu(object sender, Awesomium.Core.ContextMenuEventArgs e)
        {
            e.Handled = true;
        }

        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ExitButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
