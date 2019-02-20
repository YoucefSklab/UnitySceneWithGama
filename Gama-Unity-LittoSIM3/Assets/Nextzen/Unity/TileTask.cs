using Nextzen;
using Nextzen.Unity;
using Nextzen.VectorData;
using System.Collections.Generic;
using UnityEngine;

public class TileTask
{
    // The tile address this task is working on
    private TileAddress address;

    // The transform applied to the geometry built the tile task builders
    private Matrix4x4 transform;

    // The generation of this tile task
    private int generation;

    // The resulting data of the tile task is stored in this container
    private List<FeatureMesh> data;

    // The map styling this tile task is working on
    private MapStyle featureStyling;

    public int Generation
    {
        get { return generation; }
    }

    public List<FeatureMesh> Data
    {
        get { return data; }
    }

    public Matrix4x4 Transform
    {
        get
        {
            return transform;
        }

        set
        {
            transform = value;
        }
    }

    public TileAddress Address
    {
        get
        {
            return address;
        }

        set
        {
            address = value;
        }
    }


    public TileTask(MapStyle featureStyling, TileAddress address, Matrix4x4 transform, int generation)
    {
        this.data = new List<FeatureMesh>();
        this.address = address;
        this.Transform = transform;
        this.generation = generation;
        this.featureStyling = featureStyling;
    }

    /// <summary>
    /// Runs the tile task, resulting data will be stored in Data.
    /// </summary>
    /// <param name="featureCollections">The feature collections this tile task will be building.</param>
    public void Start(IEnumerable<FeatureCollection> featureCollections)
    {
        float inverseTileScale = 1.0f / (float)address.GetSizeMercatorMeters();

        foreach (var styleLayer in featureStyling.Layers)
        {
            foreach (var collection in featureCollections)
            {
                foreach (var feature in styleLayer.GetFilter().Filter(collection))
                {
                    var layerStyle = styleLayer.Style;
                    string featureName = "";
                    object identifier;

                    if (feature.TryGetProperty("id", out identifier))
                    {
                        featureName += identifier.ToString();
                    }

                    // Resulting data for this feature.
                    FeatureMesh featureMesh = new FeatureMesh(address.ToString(), collection.Name, styleLayer.Name, featureName);
                    Debug.Log("^^^^^^^^^^^^^^^^^^--> 1 Tile:  "+address.ToString());
                    Debug.Log("^^^^^^^^^^^^^^^^^^--> 2 Collection: "+collection.Name);
                    Debug.Log("^^^^^^^^^^^^^^^^^^--> 3 Layer: "+ styleLayer.Name);
                    Debug.Log("^^^^^^^^^^^^^^^^^^--> 4 Identifier "+featureName);

                    IGeometryHandler handler = null;

                    if (feature.Type == GeometryType.Polygon || feature.Type == GeometryType.MultiPolygon)
                    {
                        var polygonOptions = layerStyle.GetPolygonOptions(feature, inverseTileScale);

                        if (polygonOptions.Enabled)
                        {
                            handler = new PolygonBuilder(featureMesh.Mesh, polygonOptions, Transform);
                        }
                    }

                    if (feature.Type == GeometryType.LineString || feature.Type == GeometryType.MultiLineString)
                    {
                        var polylineOptions = layerStyle.GetPolylineOptions(feature, inverseTileScale);

                        if (polylineOptions.Enabled)
                        {
                            handler = new PolylineBuilder(featureMesh.Mesh, polylineOptions, Transform);
                        }
                    }

                    if (handler != null)
                    {
                        feature.HandleGeometry(handler);
                        data.Add(featureMesh);
                    }
                }
            }
        }
    }
}