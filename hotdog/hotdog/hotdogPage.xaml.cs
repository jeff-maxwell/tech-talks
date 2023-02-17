using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using hotdog.Model;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace hotdog
{
    public partial class hotdogPage : ContentPage
    {
		// Prediction Key from customvision.ai
		private static string predictionKey = "[ENTER PREDICTION KEY FROM CUSTOM VISION API]";

		// Prediction URL from customvision.ai
        private static string predictionURL = "[ENTER PREDICTION URL FROM CUSTOM VISION API]";

		public hotdogPage()
		{
			InitializeComponent();
		}

        private async void UploadPictureButton_Clicked(object sender, System.EventArgs e)
        {
            HotDogStatus.Text = "";
            HotDogStatus.BackgroundColor = Color.White;

            string HotdogStr = "NOT HOTDOG";
            Color StatusColor = Color.Red;


            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Upload", "Selecting a photo is not supported", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            Image1.Source = ImageSource.FromStream(() => file.GetStream());

            var result = await MakePredictionRequest(file);

            foreach(var item in result.Predictions)
            {
                if (isHotdog(item))
                {
                    HotdogStr = "HOTDOG!!!!";
                    StatusColor = Color.Green;
                }
            }

            HotDogStatus.Text = HotdogStr;
            HotDogStatus.BackgroundColor = StatusColor;

            PredictionView.ItemsSource = result.Predictions;
        }

		static byte[] GetImageAsByteArray(MediaFile file)
		{
			var stream = file.GetStream();
			BinaryReader binaryReader = new BinaryReader(stream);
			return binaryReader.ReadBytes((int)stream.Length);
		}


		public async Task<CVResults> MakePredictionRequest(MediaFile file)
		{
			var client = new HttpClient();

			// Request headers - Send in the header your valid subscription key.
			client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);

			// Request body. Loads Image from Disk.
			byte[] byteData = GetImageAsByteArray(file);

			using (var content = new ByteArrayContent(byteData))
			{
				// Set Content Type to Stream
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

				// Call the Prediction API
				HttpResponseMessage response = await client.PostAsync(predictionURL, content);

				var responseString = await response.Content.ReadAsStringAsync();

				// Convert into CVResults Model in hotdog.model for easier manipulation
				return JsonConvert.DeserializeObject<CVResults>(responseString);
			}
		}

		public bool isHotdog(Prediction prediction)
		{
			bool isHotdogImage = false;

			if ((prediction.Tag == "Hotdog") && (prediction.Probability >= 0.5))
                isHotdogImage = true;

            return isHotdogImage;
		}
    }
}
