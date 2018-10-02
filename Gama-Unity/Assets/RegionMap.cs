using Nextzen.Unity;
using Nextzen.VectorData;
using Nextzen.VectorData.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using ummisco.gama.unity.utils;
using UnityEngine;

namespace Nextzen
{
    public class RegionMap : MonoBehaviour
    {
        // Version information
        // This allows us to check whether an asset was serialized with a different version than this code.
        // If a serialized field of this class is changed or renamed, currentAssetVersion should be incremented.

        private const int currentAssetVersion = 1;
        [SerializeField] private int serializedAssetVersion = currentAssetVersion;

        // Public fields
        // These are serialized, so renaming them will break asset compatibility.

        public string ApiKey = "NO9atv-JQf289NztiKv45g";

        /* 
                public TileArea Area = new TileArea(
                    new LngLat(-74.014892578125, 40.70562793820589),
                    new LngLat(-74.00390625, 40.713955826286046),
                    16);
        */
        public TileArea Area = new TileArea(
           new LngLat(-74.009463, 40.711446),
           new LngLat(-73.999306, 40.706939),
           16);

        public float UnitsPerMeter = 1.0f;

        public string RegionName = "";

        public SceneGroupType GroupOptions;

        public GameObjectOptions GameObjectOptions;

        public MapStyle Style;

        // Private fields

        private IO tileIO = new IO();

        private List<TileTask> tasks = new List<TileTask>();

        private int nTasksForArea = 0;

        private int generation = 0;

        private AsyncWorker worker = new AsyncWorker(2);

        private GameObject regionMap = null;

        private TileCache tileCache = new TileCache(50);

        public Matrix4x4 tempTransform;
        public TileAddress tempTileAddress;

        public float elevation;

        public static Material buildingMaterial;



        public void DownloadTilesAsync()
        {
            TileBounds bounds = new TileBounds(Area);

            // Abort currently running tasks and increase generation
            worker.ClearTasks();
            tasks.Clear();
            nTasksForArea = 0;
            generation++;

            foreach (var tileAddress in bounds.TileAddressRange)
            {
                nTasksForArea++;
                tempTileAddress = tileAddress;
            }



            foreach (var tileAddress in bounds.TileAddressRange)
            {
                float offsetX = (tileAddress.x - bounds.min.x);
                float offsetY = (-tileAddress.y + bounds.min.y);

                float scaleRatio = (float)tileAddress.GetSizeMercatorMeters() * UnitsPerMeter;
                Matrix4x4 scale = Matrix4x4.Scale(new Vector3(scaleRatio, scaleRatio, scaleRatio));
                Matrix4x4 translate = Matrix4x4.Translate(new Vector3(offsetX * scaleRatio, 0.0f, offsetY * scaleRatio));
                Matrix4x4 transform = translate * scale;
                tempTransform = transform;

                IEnumerable<FeatureCollection> featureCollections = tileCache.Get(tileAddress);

                if (featureCollections != null)
                {
                    var task = new TileTask(Style, tileAddress, transform, generation);

                    worker.RunAsync(() =>
                    {
                        if (generation == task.Generation)
                        {
                            task.Start(featureCollections);
                            tasks.Add(task);
                            Debug.Log("New Task to add is (1) " + task.Address.ToString());
                        }
                    });
                }
                else
                {
                    // Use a local generation variable to be used in IORequestCallback coroutine
                    int requestGeneration = generation;

                    var wrappedTileAddress = tileAddress.Wrapped();

                    var uri = new Uri(string.Format("https://tile.nextzen.org/tilezen/vector/v1/all/{0}/{1}/{2}.mvt?api_key={3}",
                        wrappedTileAddress.z,
                        wrappedTileAddress.x,
                        wrappedTileAddress.y,
                        ApiKey));

                    IO.IORequestCallback onTileFetched = (response) =>
                    {
                        if (requestGeneration != generation)
                        {
                            // Another request has been made before the coroutine was triggered
                            return;
                        }

                        if (response.hasError())
                        {
                            Debug.Log("TileIO Error: " + response.error);
                            return;
                        }

                        if (response.data.Length == 0)
                        {
                            Debug.Log("Empty Response");
                            return;
                        }

                        var task = new TileTask(Style, tileAddress, transform, generation);

                        worker.RunAsync(() =>
                        {
                            // Skip any tasks that have been generated for a different generation
                            if (generation == task.Generation)
                            {
                                // var tileData = new GeoJsonTile(address, response);
                                var mvtTile = new MvtTile(tileAddress, response.data);

                                // Save the tile feature collections in the cache for later use
                                tileCache.Add(tileAddress, mvtTile.FeatureCollections);

                                task.Start(mvtTile.FeatureCollections);

                                tasks.Add(task);
                                // Debug.Log("New Task (from else) to add is " + task.Address.ToString());
                                // Debug.Log("Data is " + task.Data.Count);
                                // string result = System.Text.Encoding.UTF8.GetString(response.data);
                                // Debug.Log("---->>>   Response is " + result);
                            }
                        });
                    };

                    // Starts the HTTP request
                    StartCoroutine(tileIO.FetchNetworkData(uri, onTileFetched));
                }
            }

            //--------------------------------


            var myTask = new TileTask(Style, tempTileAddress, tempTransform, generation);

            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            var vect3 = new List<Vector3> {   new Vector3 (0, 0, 0),
                                                     new Vector3 (100, 0, 0),
                                                     new Vector3 (100, 100, 0),
                                                     new Vector3 (0, 100, 0),
                                                     new Vector3 (0, 100, 100),
                                                     new Vector3 (100, 100, 100),
                                                     new Vector3 (100, 0, 100),
                                                     new Vector3 (0, 0, 100)
                              };
            //  binFormatter.Serialize(mStream, vect3);

            //  var myMvtTile = new MvtTile(tempTileAddress, mStream.ToArray());
            //  myTask.Start(myMvtTile.FeatureCollections);
            //  tasks.Add(myTask);

        }

        public bool HasPendingTasks()
        {
            return nTasksForArea > 0;
        }

        public bool FinishedRunningTasks()
        {
            // Number of tasks ready for the current generation
            int nTasksReady = 0;

            foreach (var task in tasks)
            {
                if (task.Generation == generation)
                {
                    nTasksReady++;
                }
            }

            return nTasksReady == nTasksForArea;
        }




        public void GenerateSceneGraph()
        {
            // Add tasks here! 
            //------------------------------------------------------------------
            // Debug.Log("Calling method - > GenerateSceneGraph ");

            List<FeatureMesh> meshList = new List<FeatureMesh>();
            FeatureMesh featureMesh = new FeatureMesh("NameGama1", "GamaBlocks", "NameGama3", "Blok1"); meshList.Add(featureMesh);
            featureMesh = new FeatureMesh("NameGama1", "GamaBlocks", "GamaBlocks", "Block2"); meshList.Add(featureMesh);
            featureMesh = new FeatureMesh("NameGama1", "GamaBlocks", "Na6", "Block3"); meshList.Add(featureMesh);
            featureMesh = new FeatureMesh("NameGama1", "GamaRoads", "NameGama9", "Road1"); meshList.Add(featureMesh);




            featureMesh = new FeatureMesh("NameGama11", "GamaRoads", "NameGama13", "Road2");
            // featureMesh.Mesh = new MeshData();

            MeshData meshData = featureMesh.Mesh;
            // List<Submesh> Submeshes = new List<Submesh>();
            List<Vector3> Vertices = new List<Vector3>();
            List<Vector2> UVs = new List<Vector2>();
            List<int> Indices = new List<int>();

            List<MeshData.Submesh> Submeshes = new List<MeshData.Submesh>();
            MeshData.Submesh submesh = new MeshData.Submesh();


            //Material Material = new Material(contents: "UVGrid");
            submesh.Indices = Indices;
            Submeshes.Add(submesh);





            meshData.addGamaMeshData(Vertices, UVs, Submeshes);
            featureMesh.Mesh = meshData;

            //meshData.Meshes

            meshList.Add(featureMesh);


            featureMesh = new FeatureMesh("NameGama11", "GamaRoads", "NameGama13", "Cube3");
            // featureMesh.Mesh = new MeshData();

            meshData = featureMesh.Mesh;

            Vector2[] vertices2D = new Vector2[] {
                        new Vector2(0,0),
                        new Vector2(10,0),
                        new Vector2(10,10),
                        new Vector2(0,10),
                };
            Triangulator triangulator = new Triangulator(vertices2D);
            Vertices = triangulator.get3dVerticesList(2);
            Indices = triangulator.getTriangulesList();

            UVs = new List<Vector2>();

            UVs.AddRange(new List<Vector2> {
               new Vector2(0.0f,0.2f),new Vector2(0.0f,0.2f),new Vector2(0.0f,0.0f),new Vector2(0.0f,0.0f),new Vector2(0.1f,0.2f),new Vector2(0.0f,0.2f),new Vector2(0.1f,0f),new Vector2(0.0f,0.0f)
             });
            Submeshes = new List<MeshData.Submesh>();
            submesh = new MeshData.Submesh();

            submesh.Indices = Indices;
            Submeshes.Add(submesh);

            meshData.addGamaMeshData(Vertices, UVs, Submeshes);
            featureMesh.Mesh = meshData;
            meshList.Add(featureMesh);


            if (regionMap != null)
            {
                DestroyImmediate(regionMap); Debug.Log("regionMap is Null");
            }

            // Merge all feature meshes
            List<FeatureMesh> features = new List<FeatureMesh>();
            foreach (var task in tasks)
            {
                if (task.Generation == generation)
                {
                    features.AddRange(task.Data);
                }
            }

            tasks.Clear();
            nTasksForArea = 0;

            features.AddRange(meshList);
            regionMap = new GameObject(RegionName);
            var sceneGraph = new SceneGraph(regionMap, GroupOptions, GameObjectOptions, features);

            sceneGraph.Generate();
        }


        public bool IsValid()
        {
            bool hasStyle = Style != null;
            bool hasApiKey = ApiKey.Length > 0;
            return RegionName.Length > 0 && hasStyle && hasApiKey;
        }

        public void LogWarnings()
        {
            if (ApiKey.Length == 0)
            {
                Debug.LogWarning("Make sure to set an API key in the RegionMap");
            }

            if (Style != null && Style.Layers.Count == 0)
            {
                Debug.LogWarning("The current MapStyle has no layers, no output will be produced");
            }
        }

        public void LogErrors()
        {
            if (RegionName.Length == 0)
            {
                Debug.LogError("Make sure to give a region name");
            }

            if (Style == null)
            {
                Debug.LogError("Make sure to set a MapStyle");
            }
        }

        public void OnValidate()
        {
            if (serializedAssetVersion != currentAssetVersion)
            {
                Debug.LogWarningFormat("The RegionMap \"{0}\" was created with a different version of this tool. " +
                    "Some properties may be missing or have unexpected values.", this.name);
                serializedAssetVersion = currentAssetVersion;
            }
        }







        void Start()
        {
            Debug.Log("This is the map builder Agent");
            this.elevation = 10f;
            /* 
            ApiKey = "NO9atv-JQf289NztiKv45g";
            UnitsPerMeter = 1.0f;
            RegionName = "Test";

            Area = new TileArea(
            new LngLat(-74.009463, 40.711446),
            new LngLat(-73.999306, 40.706939),
            16);
            DownloadTilesAsync();
          */
        }

        void Awak()
        {
            //  this.elevation = 10f;
        }

        public void DrawNewAgents()
        {
            bool isNewAgentCreated = false;
            List<FeatureMesh> meshList = new List<FeatureMesh>();
            foreach (var agent in GamaManager.gamaAgentList)
            {
                if (!agent.isDrawed)
                {
                    isNewAgentCreated = true;
                    agent.isDrawed = true;

                    FeatureMesh featureMesh = new FeatureMesh("NameGama1", agent.geometry, "NameGama13", agent.agentName);
                    List<Vector3> Vertices = new List<Vector3>();
                    List<Vector2> UVs = new List<Vector2>();
                    List<int> Indices = new List<int>();
                    MeshData meshData = featureMesh.Mesh;
                    List<MeshData.Submesh> Submeshes = new List<MeshData.Submesh>();
                    MeshData.Submesh submesh = new MeshData.Submesh();

                    Vector2[] vertices2D = agent.agentCoordinate.getVector2Coordinates();

                    List<Vector2> vect = new List<Vector2>();
                    vect = vertices2D.ToList();
                    if (agent.geometry.Equals("Polygon"))
                    {
                        vect.RemoveAt(vect.Count - 1);
                    }
                    vertices2D = vect.ToArray();


                    Triangulator triangulator = new Triangulator(vertices2D);
                    triangulator.setAllPoints(triangulator.get2dVertices());
                    float elevation = this.elevation;
                    if (agent.geometry.Equals("LineString")) elevation = 0.0f;
                    Vertices = triangulator.get3dVerticesList(elevation);
                    Indices = triangulator.getTriangulesList();
                    UVs = new List<Vector2>();

                    Vector3[] VerticesArray = Vertices.ToArray();

                    Vector2[] UvArray = UvCalculator.CalculateUVs(VerticesArray, 100);

                    UVs = UvArray.ToList();

                    submesh.Indices = Indices;

                    submesh.Material = buildingMaterial;

                    Submeshes.Add(submesh);

                    Debug.Log("addGamaMeshData ------> " + agent.geometry + " Agent name -> " + agent.agentName);

                    meshData.addGamaMeshData(Vertices, UVs, Submeshes, agent.geometry);


                    featureMesh.Mesh = meshData;
                    meshList.Add(featureMesh);


                }
            }



            if (isNewAgentCreated)
            {

                if (regionMap != null)
                {
                    //DestroyImmediate(regionMap); Debug.Log("regionMap is Null");
                }

                // Merge all feature meshes
                List<FeatureMesh> features = new List<FeatureMesh>();
                features.AddRange(meshList);
                regionMap = GameObject.Find(RegionName);


                if (regionMap == null)
                {
                    regionMap = new GameObject(RegionName);
                }
                regionMap = new GameObject(RegionName);
                var sceneGraph = new SceneGraph(regionMap, GroupOptions, GameObjectOptions, features);
                //sceneGraph
                sceneGraph.Generate();

            }
        }




    }
}