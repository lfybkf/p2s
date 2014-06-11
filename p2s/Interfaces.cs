using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	public interface IContainerOfSceneItems
	{
		IEnumerable<SceneItem> getChilds();
	}//interface

	public static class ContainerOfSceneItemsDefaultMethods
	{
		public static bool hasChilds(this IContainerOfSceneItems cont) { return cont.getChilds().Any(); }

		public static bool contains(this IContainerOfSceneItems cont, SceneItem obj)
		{
			foreach (var item in cont.getChilds())
			{
				if (item == obj)
					return true;
				if (item is IContainerOfSceneItems && (item as IContainerOfSceneItems).contains(obj))
					return true;
			}//for
			return false;
		}//function
	}//class
}
