using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace FacesAndMeshes
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var model = new Model();

			try
			{
				var savedPlane = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();
				model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());

				var picker = new Picker();
				var part = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART, "Pick Part") as Part;
				if (part == null) return;

				var solid = part.GetSolid();

				var facesManager = new FacesManager();
				var partPlanes = facesManager.GetGeometricPlanes(solid);
				facesManager.DrawFaces(partPlanes);

				model.GetWorkPlaneHandler().SetCurrentTransformationPlane(savedPlane);
			}
			catch
			{
				// ignored
			}
		}
	}
}
