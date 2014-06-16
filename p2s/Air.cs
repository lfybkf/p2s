using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synesis
{
	public class Air
	{
		#region components
		static IDictionary<Type, string> components = new Dictionary<Type, string>() 
		{ 
			{ typeof(Scene), "com.synesis.components.common.view.BaseGame" } 
		, { typeof(Panel), "com.synesis.framework.layout.component.BaseLayoutComponent" } 
		, { typeof(Button), "feathers.controls.Button" }
		, { typeof(Checkbox), "feathers.controls.Check" }
		, { typeof(Sprite), "com.synesis.components.common.base.image.ImageFix" }
		, { typeof(Text), "feathers.controls.TextArea" }
		};
		#endregion

		#region static_const
		public static readonly string ASSIGN = "assign";
		public static readonly string ACTION = "action";
		public static readonly string CHILD = "child";
		public static readonly string CHILDS = "childs";
		public static readonly string CID = "cid";
		public static readonly string CLASS = "class";
		public static readonly string COMPONENT = "component";
		public static readonly string CONSTANT = "constant";
		public static readonly string CONSTANTS = "constants";
		public static readonly string DP = "dp";
		public static readonly string ID = "id";
		public static readonly string LAYOUT = "layout";
		public static readonly string NAME = "name";
		public static readonly string PROPERTY = "property";
		public static readonly string TARGET_COMPONENT_CID = "target-component-cid";
		public static readonly string TARGET_PROPERTY = "target-property";
		public static readonly string TYPE = "type";
		public static readonly string VALUE = "value";
		#endregion

		public static string getComp(Type type) { return components[type]; }
		public static string getComp(Object obj) { return getComp(obj.GetType()); }
	}//class
}//nc
