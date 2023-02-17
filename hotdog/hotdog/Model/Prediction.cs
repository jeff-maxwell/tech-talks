namespace hotdog.Model
{
    public class Prediction
    {
        public Prediction()
        {

        }
		public string TagId { get; set; }
		public string Tag { get; set; }
		public double Probability { get; set; }
    }
}