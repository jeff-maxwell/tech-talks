using System;
using System.Collections.Generic;

namespace hotdog.Model
{
    public class CVResults
    {
        public CVResults()
        {
            Predictions = new List<Prediction>();
        }

        public string Id { get; set; }
        public string Project { get; set; }
        public string Iteration { get; set; }
		public DateTime Created { get; set; }
        public List<Prediction> Predictions { get; set; }
    }
}