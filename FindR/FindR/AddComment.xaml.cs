using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddComment : Page
    {
        int id;
        public AddComment()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            id = (int)e.Parameter;
        }

        private void Cancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void Submit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comment.Text))
            {
                new MessageDialog("Your comment is empty.", "Error").ShowAsync();
                return;
            }
            using (var client = new HttpClient())
            {
                var param = new Dictionary<string, string>();
                param.Add("comment", comment.Text);
                param.Add("id", id.ToString());
                try
                {
                    var resp = await client.PostAsync("http://173.250.206.173:8080/findR/php/input/updateComments.php", new FormUrlEncodedContent(param));
                    var content = await resp.Content.ReadAsStringAsync();
                    new MessageDialog($"Your comment \"{comment.Text}\" has been posted.", "Sucess").ShowAsync();
                    Frame.GoBack();
                }
                catch
                {
                    new MessageDialog("Something Went Wrong.", "Something Went Wrong").ShowAsync();
                }                
            }
        }
    }
}
