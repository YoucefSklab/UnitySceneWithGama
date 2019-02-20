
namespace Nextzen.VectorData
{
    public struct Point
    {
        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
        }

    public Point(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public float X;
        public float Y;

        public float Z;

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y,Z);
        }

        public UnityEngine.Vector3 toVector3D (){
            return new UnityEngine.Vector3(X,Y,Z);
        }
    }
}