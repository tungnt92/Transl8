using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Transl8.Resources;
using Windows.Phone.Speech.Synthesis;
using Windows.Phone.Speech.Recognition;
using Coding4Fun.Toolkit.Controls;
using AppGhiChu.Model;
using Windows.System;

namespace Transl8
{
    public partial class MainPage : PhoneApplicationPage
    {
        string strLngTo = "vi";
        string strLngFrom = "en";
        int fromvar = 1;
        private static string strTextToTranslate = "";
        SpeechRecognizerUI speechRecognizer;
        SpeechSynthesizer synthesizer;

        public MainPage()
        {
            InitializeComponent();
            this.speechRecognizer = new SpeechRecognizerUI();
            this.synthesizer = new SpeechSynthesizer();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<MoTa> mt = new List<MoTa>();
            mt.Add(new MoTa("Url", "SHTP-TRAINING", "/Assets/Item-53e88f6f-42e4-4d2c-ab41-a8d568894434.png", "bingmaps:?cp=10.855064~106.785569&lvl=17"));
            mt.Add(new MoTa("Url", "Address: Lô E1 – Khu Công nghệ cao, Xa lộ Hà Nội, Phường Hiệp Phú, Quận 9, TP.HCM", "/Assets/location.png", "bingmaps:?cp=10.855064~106.785569&lvl=17"));
            mt.Add(new MoTa("Phone", "Phone: (84-8) 39.322.230 (17) ", "/Assets/phoneicon.png", "tel:(84-8) 39.322.230 (17)"));
            mt.Add(new MoTa("Phone", "Hỗ trợ dịch từ 10 ngôn ngữ phổ biến nhất trên thế giới.", "/Assets/ApplicationIcon.png", "tel:(84-8) 39.322.230 (17)"));
            mt.Add(new MoTa("Url", "Author: tungnt92", "/Assets/User-Monitor.png", ""));
            mt.Add(new MoTa("Url", "Version: 1.0.0", "/Assets/Download.png", ""));
            ListMT.ItemsSource = mt;
        }

        private void optSpanish_Checked(object sender, RoutedEventArgs e)
        {
            strLngTo = "es";
        }

        private void optGerman_Checked(object sender, RoutedEventArgs e)
        {
            strLngTo = "vi";
        }

        private void optFrench_Checked(object sender, RoutedEventArgs e)
        {
            strLngTo = "fr";
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (this.popup.IsOpen)
            {
                this.popup.IsOpen = false;
                e.Cancel = true;
            }

            base.OnBackKeyPress(e);
        }
        private void btnTraslate_Click(object sender, RoutedEventArgs e)
        {
            if(txtToTrans.Text != "")
            {
                strTextToTranslate = txtToTrans.Text;
                String strTranslatorAccessURI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
                System.Net.WebRequest req = System.Net.WebRequest.Create(strTranslatorAccessURI);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                IAsyncResult writeRequestStreamCallback = (IAsyncResult)req.BeginGetRequestStream(new AsyncCallback(RequestStreamReady), req);
            }
            else
            {
                MessageBox.Show("Please tell me what do you want to translate!?");
            }
        }
        private void RequestStreamReady(IAsyncResult ar)
        {
            string clientID = "tungnt92";
            string clientSecret = "nguyentungvungocthaonguyen";
            String strRequestDetails = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientID), HttpUtility.UrlEncode(clientSecret));

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)ar.AsyncState;

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strRequestDetails);
            System.IO.Stream postStream = request.EndGetRequestStream(ar);
            postStream.Write(bytes, 0, bytes.Length);
            postStream.Close();

            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }
        private void GetResponseCallback(IAsyncResult ar)
        {
            HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new
                System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AdmAccessToken));
                AdmAccessToken token =
                  (AdmAccessToken)serializer.ReadObject(response.GetResponseStream());

                string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Net.HttpUtility.UrlEncode(strTextToTranslate) + "&from="+ strLngFrom +"&to=" + strLngTo;
                System.Net.WebRequest translationWebRequest = System.Net.HttpWebRequest.Create(uri);

                string headerValue = "Bearer " + token.access_token;
                translationWebRequest.Headers["Authorization"] = headerValue;

                IAsyncResult writeRequestStreamCallback = (IAsyncResult)translationWebRequest.BeginGetResponse(new AsyncCallback(translationReady), translationWebRequest);
            }
            catch (Exception ex)
            {

            }
        }
        private void translationReady(IAsyncResult ar)
        {
            HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
            System.IO.Stream streamResponse = response.GetResponseStream();
            System.IO.StreamReader streamRead = new System.IO.StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();
            System.Xml.Linq.XDocument xTranslation =
              System.Xml.Linq.XDocument.Parse(responseString);
            string strTest = xTranslation.Root.FirstNode.ToString();
            Deployment.Current.Dispatcher.BeginInvoke(() => lblTranslatedText.Text = strTest);
        }
        private void eng_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "en";
                btnFrom.Content = "English";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "en";
                btnTo.Content = "English";
            }
        }

        private void viet_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "vi";
                btnFrom.Content = "Vietnamese";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "vi";
                btnTo.Content = "Vietnamese";
            }
        }

        private void btnFrom_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            fromvar = 1;
            this.popup.IsOpen = true;
            //btnFrom.Content = strNameLngFrom;
        }

        private void btnTo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            fromvar = 0;
            this.popup.IsOpen = true;
            //btnTo.Content = strNameLngTo;
        }

        private async void SpeakAppBr_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry! I only listen to you in English!");

            strLngFrom = "en";
            btnFrom.Content = "English";

            this.speechRecognizer.Recognizer.Grammars.Clear();
            this.speechRecognizer.Recognizer.Grammars.AddGrammarFromPredefinedType("message", SpeechPredefinedGrammar.Dictation);
            await this.speechRecognizer.Recognizer.PreloadGrammarsAsync();
            try
            {
                SpeechRecognitionUIResult recognitionResult = await this.speechRecognizer.RecognizeWithUIAsync();
                if (recognitionResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
                {
                    strTextToTranslate = recognitionResult.RecognitionResult.Text;
                    String strTranslatorAccessURI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
                    System.Net.WebRequest req = System.Net.WebRequest.Create(strTranslatorAccessURI);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    IAsyncResult writeRequestStreamCallback = (IAsyncResult)req.BeginGetRequestStream(new AsyncCallback(RequestStreamReady), req);
                    txtToTrans.Text = recognitionResult.RecognitionResult.Text;
                }
                else
                {
                    //MessageBox.Show("Sorry! I didn't catch you.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void span_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "es";
                btnFrom.Content = "Spanish";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "es";
                btnTo.Content = "Spanish";
            }
        }

        private void china_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "zh-cn";
                btnFrom.Content = "Chinese";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "zh-cn";
                btnTo.Content = "Chinese";
            }
        }

        private void hindi_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "hi";
                btnFrom.Content = "Hindi";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "hi";
                btnTo.Content = "Hindi";
            }
        }

        private void russian_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "ru";
                btnFrom.Content = "Russian";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "ru";
                btnTo.Content = "Russian";
            }
        }

        private void french_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "fr";
                btnFrom.Content = "French";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "fr";
                btnTo.Content = "French";
            }
        }

        private void korean_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "ko";
                btnFrom.Content = "Korean";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "ko";
                btnTo.Content = "Korean";
            }
        }

        private void japanese_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "ja";
                btnFrom.Content = "Japanese";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "ja";
                btnTo.Content = "Japanese";
            }
        }

        private void german_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (fromvar == 1)
            {
                this.popup.IsOpen = false;
                strLngFrom = "de";
                btnFrom.Content = "German";
            }
            else
            {
                this.popup.IsOpen = false;
                strLngTo = "de";
                btnTo.Content = "German";
            }
        }

        private async void imgSpeak_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (lblTranslatedText.Text != "")
                await synthesizer.SpeakTextAsync(lblTranslatedText.Text);
        }

        private async void ListMT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListMT.SelectedItem != null)
            {
                if (((MoTa)ListMT.SelectedItem).Target != "")
                {
                    await Launcher.LaunchUriAsync(new Uri(((MoTa)ListMT.SelectedItem).Target, UriKind.Absolute));
                }
            }
            ListMT.SelectedIndex = -1;
        }
    }
}