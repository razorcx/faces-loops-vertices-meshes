using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Solid;

namespace FacesAndMeshes
{
	public class FacesManager
	{
		public GraphicsDrawer GraphicsDrawer = new GraphicsDrawer();
	
		public Dictionary<Plane, Face> GetGeometricPlanes(Solid solid)
		{
			var faces = solid.GetFaceEnumerator().ToList();
			var planes = new Dictionary<Plane, Face>();
			faces.ForEach(face =>
			{
				var planeVertexes = new List<Point>();

				var loops = face.GetLoopEnumerator().ToList();
				loops.ForEach(loop =>
				{
					var vertexes = loop.GetVertexEnumerator().ToList();
					if (vertexes.Count > 4) return;

					var count = 1;

					vertexes.ForEach(v =>
					{
						GraphicsDrawer.DrawText(v, count++.ToString(), new Color(0, 0, 0));

						if (planeVertexes.Contains(v)) return;

						//Three points form a plane and they cannot be aligned.
						if (planeVertexes.Count != 3 ||
							planeVertexes.Count == 3 && !ArePointAligned(planeVertexes[0], planeVertexes[1], v))
							planeVertexes.Add(v);

						if (planeVertexes.Count != 3) return;

						var vector1 = new Vector(
							planeVertexes[1].X - planeVertexes[0].X,
							planeVertexes[1].Y - planeVertexes[0].Y,
							planeVertexes[1].Z - planeVertexes[0].Z);
						var vector2 = new Vector(
							planeVertexes[2].X - planeVertexes[0].X,
							planeVertexes[2].Y - planeVertexes[0].Y,
							planeVertexes[2].Z - planeVertexes[0].Z);

						var plane = new Plane
						{
							Origin = planeVertexes[0],
							AxisX = vector1,
							AxisY = vector2
						};
						planes.Add(plane, face);
					});
				});
			});
			return planes;
		}

		private bool ArePointAligned(Point point1, Point point2, Point point3)
		{
			var vector1 = new Vector(point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z);
			var vector2 = new Vector(point3.X - point1.X, point3.Y - point1.Y, point3.Z - point1.Z);

			return Parallel.VectorToVector(vector1, vector2);
		}

		public void DrawFaces(Dictionary<Plane, Face> partPlanes)
		{
			foreach (var plane in partPlanes.Keys)
			{
				DrawMesh(partPlanes[plane]);
			}
		}

		private void DrawMesh(Face face)
		{
			var loops = face.GetLoopEnumerator().ToList();
			var loop = loops.FirstOrDefault();
			var vertexes = loop?.GetVertexEnumerator().ToList();

			if (vertexes?.Count > 4) return;

			var mesh = new Mesh();

			vertexes?.ForEach(v =>
			{
				mesh.AddPoint(v);
			});

			mesh.AddTriangle(0, 1, 2);
			mesh.AddTriangle(2, 3, 0);

			GraphicsDrawer.DrawMeshSurface(mesh, new Color(1, 1, 0));
		}
	}
}

