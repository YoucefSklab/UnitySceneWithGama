using System;
using System.Collections.Generic;
using System.Linq;
using ummisco.gama.unity.GamaAgent;
using UnityEngine;

namespace Nextzen.Unity
{
    public class MeshData
    {
        public class Submesh
        {
            public List<int> Indices;
            public Material Material;
        }

        public class MeshBucket
        {
            public List<Submesh> Submeshes;
            public List<Vector3> Vertices;
            public List<Vector2> UVs;
            public Agent gamaAgent;

            public MeshBucket()
            {
                Vertices = new List<Vector3>();
                UVs = new List<Vector2>();
                Submeshes = new List<Submesh>();
                gamaAgent = null;

            }

            public MeshBucket(Agent gamaAgent)
            {
                Vertices = new List<Vector3>();
                UVs = new List<Vector2>();
                Submeshes = new List<Submesh>();
                this.gamaAgent = gamaAgent; 
            }

           

            public void setGamaAgent(Agent gamaAgent)
            {
                this.gamaAgent = gamaAgent;
            }
            public void setUvs()
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    UVs[i] = new Vector2(Vertices[i].x, Vertices[i].z);
                }
            }
        }

        public List<MeshBucket> Meshes;



        private static readonly int MaxVertexCount = 65535;

        public MeshData()
        {
            Meshes = new List<MeshBucket>();
        }

        public string MeshDataVerticesToString()
        {
            string meshDataString = "Total Meshes is: " + Meshes.Count + " - ";
            int nbr = 0;
            int nbr2 = 0;
            foreach (var mData in Meshes)
            {
                meshDataString += "--> The " + nbr2 + " Vertices number is: " + mData.Vertices.Count + " [";
                nbr2++;
                foreach (var vert in mData.Vertices)
                {
                    meshDataString += vert.ToString();
                    nbr++;
                }
                meshDataString += "] \n";
            }
            meshDataString += " Total Vertices is -> " + nbr;

            return meshDataString;
        }

        public string MeshDataUVsToString()
        {
            string meshDataString = " [";
            int nbr = 0;
            foreach (var mData in Meshes)
            {
                foreach (var vert in mData.UVs)
                {
                    meshDataString += vert.ToString();
                    nbr++;
                }
            }
            meshDataString += "]  Total UV is -> " + nbr;
            return meshDataString;
        }

        public string MeshDataSubmeshesToString()
        {
            string meshDataString = "Total Meshes is " + Meshes.Count + " ";
            int nbrEl = 0;
            int nbrEl2 = 0;
            foreach (var mData in Meshes)
            {
                meshDataString += nbrEl2 + " ->  Its Indices number is " + mData.Submeshes.Count + " [ ";

                nbrEl2++;

                foreach (var vert in mData.Submeshes)
                {
                    meshDataString += " Indices [ ";
                    foreach (var nbr in vert.Indices)
                    {
                        meshDataString += nbr + ", ";
                        nbrEl++;
                    }
                    //  meshDataString += " ] Material [ " +vert.Material.ToString()+" ] " ;
                    meshDataString += " ] " + " Total Indices number is -> " + vert.Indices.Count;
                }
            }
            meshDataString += "]";
            return meshDataString;
        }

        public void Merge(MeshData other)
        {
            foreach (var bucket in other.Meshes)
            {
                foreach (var submesh in bucket.Submeshes)
                {
                    if (submesh.Indices.Count == 0)
                    {
                        continue;
                    }

                    int minIndex = int.MaxValue;
                    int maxIndex = int.MinValue;

                    foreach (var index in submesh.Indices)
                    {
                        minIndex = Math.Min(minIndex, index);
                        maxIndex = Math.Max(maxIndex, index);
                    }

                    int nIndices = maxIndex - minIndex + 1;
                    var uvs = bucket.UVs.GetRange(minIndex, nIndices);
                    var vertices = bucket.Vertices.GetRange(minIndex, nIndices);
                    var indices = submesh.Indices as IEnumerable<int>;

                    if (minIndex > 0)
                    {
                        indices = indices.Select(i => i - minIndex);
                    }

                    AddElements(vertices, uvs, indices, submesh.Material);

                }
            }
        }

        public void Merge(MeshData other, bool withGamaAgent)
        {
            foreach (var bucket in other.Meshes)
            {
                foreach (var submesh in bucket.Submeshes)
                {
                    if (submesh.Indices.Count == 0)
                    {
                        continue;
                    }

                    int minIndex = int.MaxValue;
                    int maxIndex = int.MinValue;

                    foreach (var index in submesh.Indices)
                    {
                        minIndex = Math.Min(minIndex, index);
                        maxIndex = Math.Max(maxIndex, index);
                    }

                    int nIndices = maxIndex - minIndex + 1;
                    var uvs = bucket.UVs.GetRange(minIndex, nIndices);
                    var vertices = bucket.Vertices.GetRange(minIndex, nIndices);
                    var indices = submesh.Indices as IEnumerable<int>;

                    if (minIndex > 0)
                    {
                        indices = indices.Select(i => i - minIndex);
                    }
                    if (withGamaAgent)
                    {
                        AddElements(vertices, uvs, indices, submesh.Material, bucket.gamaAgent);
                    }
                    else
                    {
                        AddElements(vertices, uvs, indices, submesh.Material);
                    }


                }
            }
        }
        public void AddElements(IEnumerable<Vector3> vertices, IEnumerable<Vector2> uvs, IEnumerable<int> indices, Material material, Agent gamaAgent)
        {
            var vertexList = new List<Vector3>(vertices);
            int vertexCount = vertexList.Count;

            MeshBucket bucket = null;

            // Check whether the last available bucket is valid for use given the maximum vertex count
            if (Meshes.Count > 0)
            {
                var last = Meshes[Meshes.Count - 1];
                if (last.Vertices.Count + vertexCount < MaxVertexCount)
                {
                    bucket = last;
                }
            }

            // No bucket were found, instantiate a new one
            if (bucket == null)
            {
                bucket = new MeshBucket();
                Meshes.Add(bucket);
            }

            bucket.gamaAgent = gamaAgent;
            int offset = bucket.Vertices.Count;
            bucket.Vertices.AddRange(vertexList);
            bucket.UVs.AddRange(uvs);

            // Find a submesh with this material, or create a new one.
            Submesh submesh = null;
            foreach (var s in bucket.Submeshes)
            {
                if (s.Material == material)
                {
                    submesh = s;
                    break;
                }
            }

            if (submesh == null)
            {
                submesh = new Submesh { Indices = new List<int>(), Material = material };
                bucket.Submeshes.Add(submesh);
            }

            foreach (var index in indices)
            {
                submesh.Indices.Add(index + offset);
            }
        }



        public void AddElements(IEnumerable<Vector3> vertices, IEnumerable<Vector2> uvs, IEnumerable<int> indices, Material material)
        {
            var vertexList = new List<Vector3>(vertices);
            int vertexCount = vertexList.Count;

            MeshBucket bucket = null;

            // Check whether the last available bucket is valid for use given the maximum vertex count
            if (Meshes.Count > 0)
            {
                var last = Meshes[Meshes.Count - 1];
                if (last.Vertices.Count + vertexCount < MaxVertexCount)
                {
                    bucket = last;
                }
            }

            // No bucket were found, instantiate a new one
            if (bucket == null)
            {
                bucket = new MeshBucket();
                Meshes.Add(bucket);
            }


            int offset = bucket.Vertices.Count;
            bucket.Vertices.AddRange(vertexList);
            bucket.UVs.AddRange(uvs);

            // Find a submesh with this material, or create a new one.
            Submesh submesh = null;
            foreach (var s in bucket.Submeshes)
            {
                if (s.Material == material)
                {
                    submesh = s;
                    break;
                }
            }

            if (submesh == null)
            {
                submesh = new Submesh { Indices = new List<int>(), Material = material };
                bucket.Submeshes.Add(submesh);
            }

            foreach (var index in indices)
            {
                submesh.Indices.Add(index + offset);
            }
        }

        //-----------------------------------------------
        public void addGamaMeshData(List<Vector3> vertices, List<Vector2> uVs, List<Submesh> Submeshes, Agent gamaAgent)
        {

            MeshBucket meshBucket = new MeshBucket();
            meshBucket.Vertices = vertices;
            meshBucket.UVs = uVs;
            meshBucket.Submeshes = Submeshes;
            meshBucket.gamaAgent = gamaAgent;
            Debug.Log("-----------------------------------========================++++++++++++++++++++++++>>>>>>>>>>>> " + meshBucket.gamaAgent.geometry);
            this.Meshes.Add(meshBucket);
        }

        public void addGamaMeshData(List<Vector3> vertices, List<Vector2> uVs, List<Submesh> Submeshes)
        {
            MeshBucket meshBucket = new MeshBucket();
            meshBucket.Vertices = vertices;
            meshBucket.UVs = uVs;
            meshBucket.Submeshes = Submeshes;
            meshBucket.gamaAgent = null;

            this.Meshes.Add(meshBucket);
        }
        //-----------------------------------------------


    }
}