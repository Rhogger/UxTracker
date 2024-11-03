using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines;

namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class ElbowCalculator
{
    public List<ElbowChartData> CalculateElbowChartData(List<double[]> data, int maxK)
    {
        var elbowData = new List<ElbowChartData>();

        for (int k = 1; k <= maxK; k++)
        {
            var kmeans = new KMeans(k);
            var clusters = kmeans.Learn(data.ToArray());

            double wcss = 0;
            for (int i = 0; i < k; i++)
            {
                var clusterPoints = data.Where((point, index) => clusters.Decide(point) == i);
                var centroid = clusters.Centroids[i];

                wcss += clusterPoints.Sum(point => point.Zip(centroid, (p, c) => (p - c) * (p - c)).Sum());
            }

            elbowData.Add(new ElbowChartData(k, (decimal)wcss));
        }

        return elbowData;
    }
}