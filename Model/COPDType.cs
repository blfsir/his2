using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// COPDType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class COPDType
	{
		public COPDType()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _type;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// COPD用药类型:口服药物oral,吸入药物suck
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion Model

	}
}

