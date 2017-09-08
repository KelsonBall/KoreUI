using Processing.OpenTk.Core.Math;

namespace Processing.OpenTk.Core.Rendering
{
    public class Model
    {
        public struct TriangleMap
        {
            public readonly ushort A;
            public readonly ushort B;
            public readonly ushort C;
            public readonly ushort Normal;

            public TriangleMap(ushort a, ushort b, ushort c, ushort normal)
            {
                A = a; B = b; C = c; Normal = normal;
            }
        }

        public readonly PVector3[] Verticies;
        public readonly PVector3[] Normals;
        public readonly TriangleMap[] Triangles;

        public Model(
            PVector3[] verticies,            
            PVector3[] normals,            
            TriangleMap[] triangles)
        {
            Verticies = verticies;
            Normals = normals;
            Triangles = triangles;
        }
    }
}
