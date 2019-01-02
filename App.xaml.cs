using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StringtableEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Displays generic error messagebox for given exception.
        /// </summary>
        /// <param name="ex">Exception to display.</param>
        public static void DisplayOperationFailed(Exception ex)
        {
            Current.Dispatcher.Invoke(() => MessageBox.Show(String.Format(StringtableEditor.Properties.Language.App_GenericOperationFailedMessageBox_Body, ex.Message, ex.GetType().FullName, ex.StackTrace), StringtableEditor.Properties.Language.App_GenericOperationFailedMessageBox_Title, MessageBoxButton.OK, MessageBoxImage.Warning));
        }
        /// <summary>
        /// Displays generic error messagebox for given exception.
        /// Will display the <paramref name="body"/> in front of the exception.
        /// </summary>
        /// <param name="ex">Exception to display.</param>
        /// <param name="body">The Text to display in front of the exception.</param>
            public static void DisplayOperationFailed(Exception ex, string body)
        {
            App.Current.Dispatcher.Invoke(() => MessageBox.Show(String.Concat(body, '\n', String.Format(StringtableEditor.Properties.Language.App_GenericOperationFailedMessageBox_Body, ex.Message, ex.GetType().FullName, ex.StackTrace)), StringtableEditor.Properties.Language.App_GenericOperationFailedMessageBox_Title, MessageBoxButton.OK, MessageBoxImage.Warning));
        }
    }
}
