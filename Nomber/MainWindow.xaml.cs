using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows;

namespace Nomber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void start_Click(object sender, RoutedEventArgs e)
        {
            string IP = Convert.ToString(apitb.Text);
            string url = @"http://ipwhois.app/json/" + IP;
            WebRequest request = WebRequest.Create(url);
            try
            {
                HttpWebRequest request3 = WebRequest.Create(url) as HttpWebRequest;
                request3.Method = "HEAD";
                HttpWebResponse response3 = request3.GetResponse() as HttpWebResponse;
                response3.Close();
                if (response3.StatusCode == HttpStatusCode.NotFound)
                {
                    string IP2 = "169.192.168.1.1";
                    WebRequest request2 = WebRequest.Create(@"http://ipwhois.app/json/" + IP2);
                    request2.Method = "GET";
                    request2.ContentType = "application/x-www-urlencoded";
                    WebResponse response2 = await request2.GetResponseAsync();
                    string answer = string.Empty;
                    using (Stream stream = response2.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            answer = await reader.ReadToEndAsync();
                        }
                    }
                    response2.Close();

                    apitb.Text = IP2;

                    ApiClasses.Main oW = JsonConvert.DeserializeObject<ApiClasses.Main>(answer);

                    cname.Content = "Country: " + oW.country;

                    org.Content = "Organization: " + oW.org;

                    city.Content = "City: " + oW.city;

                    currency_code.Content = "currency_code: " + oW.currency_code;

                    type.Content = "type: " + oW.type;

                    completed_requests.Content = "completed requests: " + oW.completed_requests; 
                }
            }
            catch
            {
                MessageBox.Show("IP not found");
            }

            try
            {
                HttpWebRequest request3 = WebRequest.Create(url) as HttpWebRequest;
                request3.Method = "HEAD";
                HttpWebResponse response3 = request3.GetResponse() as HttpWebResponse;
                response3.Close();
                if (response3.StatusCode == HttpStatusCode.OK)
                {
                    WebRequest request2 = WebRequest.Create(@"http://ipwhois.app/json/" + IP);
                    request2.Method = "GET";
                    request2.ContentType = "application/x-www-urlencoded";
                    WebResponse response2 = await request2.GetResponseAsync();
                    string answer = string.Empty;
                    using (Stream stream = response2.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            answer = await reader.ReadToEndAsync();
                        }
                    }
                    response2.Close();

                    ApiClasses.Main oW = JsonConvert.DeserializeObject<ApiClasses.Main>(answer);

                    cname.Content = "Country: " + oW.country;

                    org.Content = "Organization: " + oW.org;

                    city.Content = "City: " + oW.city;

                    currency_code.Content = "currency code: " + oW.currency_code;

                    type.Content = "type: " + oW.type;

                    completed_requests.Content = "completed requests: " + oW.completed_requests;
                }
            }
            catch
            {

            }
        }
    }
}
