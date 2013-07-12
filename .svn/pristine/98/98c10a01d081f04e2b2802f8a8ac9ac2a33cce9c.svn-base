/**  版本信息模板在安装目录下，可自行修改。
* dicom.cs
*
* 功 能： N/A
* 类 名： dicom
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/28 23:07:24   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// dicom:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class dicom
	{
		public dicom()
		{}
		#region Model
		private int _id;
		private int? _pid=0;
		private int? _period=0;
		private string _heterogeneity;
		private string _lobesplit;
		private string _treatlobevolumn;
		private string _untreatlobevolumn;
		private DateTime? _checkdate;
		private string _file;
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
		public int? PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Period
		{
			set{ _period=value;}
			get{return _period;}
		}
		/// <summary>
		/// 异质性评分
		/// </summary>
		public string Heterogeneity
		{
			set{ _heterogeneity=value;}
			get{return _heterogeneity;}
		}
		/// <summary>
		/// 叶间裂完整性选项（完整、不完整）
		/// </summary>
		public string LobeSplit
		{
			set{ _lobesplit=value;}
			get{return _lobesplit;}
		}
		/// <summary>
		/// 治疗靶肺叶容积
		/// </summary>
		public string TreatLobeVolumn
		{
			set{ _treatlobevolumn=value;}
			get{return _treatlobevolumn;}
		}
		/// <summary>
		/// 同侧未治疗肺叶容积    ml
		/// </summary>
		public string UnTreatLobeVolumn
		{
			set{ _untreatlobevolumn=value;}
			get{return _untreatlobevolumn;}
		}
		/// <summary>
		/// 检查时间
		/// </summary>
		public DateTime? CheckDate
		{
			set{ _checkdate=value;}
			get{return _checkdate;}
		}
		/// <summary>
		/// 影像学资料（dicom文件）
		/// </summary>
		public string File
		{
			set{ _file=value;}
			get{return _file;}
		}
		#endregion Model

	}
}

