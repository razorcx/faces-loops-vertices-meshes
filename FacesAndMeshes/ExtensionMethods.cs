using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;

namespace FacesAndMeshes
{
	public static class ExtensionMethods
	{
		public static List<Face> ToList(this FaceEnumerator faceEnumerator)
		{
			var faces = new List<Face>();
			while (faceEnumerator.MoveNext())
			{
				var loop = faceEnumerator.Current;
				faces.Add(loop);
			}
			return faces;
		}

		public static List<Loop> ToList(this LoopEnumerator loopEnumerator)
		{
			var loops = new List<Loop>();
			while (loopEnumerator.MoveNext())
			{
				var loop = loopEnumerator.Current;
				loops.Add(loop);
			}
			return loops;
		}

		public static List<Point> ToList(this VertexEnumerator vertexEnumerator)
		{
			var vertexes = new List<Point>();
			while (vertexEnumerator.MoveNext())
			{
				var vertex = vertexEnumerator.Current;
				vertexes.Add(vertex);
			}
			return vertexes;
		}
	}
}