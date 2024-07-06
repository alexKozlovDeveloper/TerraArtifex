// See https://aka.ms/new-console-template for more information
using TerraArtifex.PerlinNoise;
using TerraArtifex.Exporting;
using TerraArtifex.Generating;
using TerraArtifex.Generating.Layers.Altitude;

Console.WriteLine("Hello, World!");

var workFolder = $"{DateTime.Now:yyyy-MM-dd_HH_mm_ss}";
Directory.CreateDirectory(workFolder);
Directory.SetCurrentDirectory(workFolder);

//var noiseGen = new PerlinNoiseGenerator(42);

//var noiseMatrix = noiseGen.GetPerlinNoiseMatrix(64, 2);

//var bitmap = noiseMatrix.ToBitmap();

//bitmap.Save("test_noise.png");


var builder = new MapBuilder();

var altitudeStep = new AltitudeLayerBuilder();

var result = builder.BuildLayer(altitudeStep);

