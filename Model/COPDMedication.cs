using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// COPDMedication:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class COPDMedication
	{
		public COPDMedication()
		{}
		#region Model
		private int _id;
		private int? _pid=0;
		private int? _copdtypeid=0;
		private string _copdtypename;
		private string _drugname;
		private string _dose;
		private string _usage;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// patient ID
		/// </summary>
		public int? PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// COPD Drug ID
		/// </summary>
		public int? COPDTypeID
		{
			set{ _copdtypeid=value;}
			get{return _copdtypeid;}
		}
		/// <summary>
		/// COPD Drug Type Name
		/// </summary>
		public string COPDTypeName
		{
			set{ _copdtypename=value;}
			get{return _copdtypename;}
		}
		/// <summary>
		/// 药物名称
		/// </summary>
		public string DrugName
		{
			set{ _drugname=value;}
			get{return _drugname;}
		}
		/// <summary>
		/// 剂量
		/// </summary>
		public string Dose
		{
			set{ _dose=value;}
			get{return _dose;}
		}
		/// <summary>
		/// 用法
		/// </summary>
		public string Usage
		{
			set{ _usage=value;}
			get{return _usage;}
		}
		#endregion Model

	}
}

