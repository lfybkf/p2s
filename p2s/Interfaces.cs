using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
		public static IEnumerable<SceneItem> getChildsAll(this IContainerOfSceneItems cont) 
		{
			return cont.getChilds().SelectMany(ch => ch.getChildsAll()).Concat(cont.getChilds());
		}//function

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

		public static XElement toXmlComponent(this IContainerOfSceneItems cont)
		{
			Func<object, string> getId = (obj => (obj is Scene) ? "main" : (obj as SceneItem).id);
			IEnumerable<SceneItem> childs = cont.getChilds();
			XElement Ret = new XElement(Air.COMPONENT
					, new XAttribute(Air.CLASS, Air.getComp(cont))
					, new XAttribute(Air.ID, getId(cont))
			);
			if (childs.Any())
				Ret.Add(
					new XElement(Air.CONSTANTS, childs.Select(sitem => sitem.toXmlConstant))
					, new XElement(Air.CHILDS, childs.Select(sitem => sitem.toXmlChild))
					, new XElement(Air.LAYOUT, childs.Select(sitem => sitem.toXmlLayout))
				);
			return Ret;			
		}//function
	}//class
}
