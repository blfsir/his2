using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// GeneralInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GeneralInfo
    {
        public GeneralInfo()
        { }
        #region Model
        private int _id;
        private int? _pid = 0;
        private decimal? _height = 0;
        private decimal? _weight = 0;
        private decimal? _waist = 0;
        private decimal? _bmi = 0;
        private decimal? _smokeyear = 0;
        private decimal? _smokeperday = 0;
        private decimal? _smokeindex = 0;
        private bool _quitsmoke;
        private string _quityear;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// Patient ID fk
        /// </summary>
        public int? PID
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 身高：单位cm，整数3位，小数点后1位
        /// </summary>
        public decimal? Height
        {
            set { _height = value; }
            get { return _height; }
        }
        /// <summary>
        /// 体重：单位千克，整数3位，小数点后1位
        /// </summary>
        public decimal? Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 单位cm，整数3位，小数点后1位
        /// </summary>
        public decimal? Waist
        {
            set { _waist = value; }
            get { return _waist; }
        }
        /// <summary>
        /// 体重指数：根据上述数据，按公式自动计算 体重/身高的平方
        /// </summary>
        public decimal? BMI
        {
            set { _bmi = value; }
            get { return _bmi; }
        }
        /// <summary>
        /// 吸烟年
        /// </summary>
        public decimal? SmokeYear
        {
            set { _smokeyear = value; }
            get { return _smokeyear; }
        }
        /// <summary>
        /// 平均支/日
        /// </summary>
        public decimal? SmokePerDay
        {
            set { _smokeperday = value; }
            get { return _smokeperday; }
        }
        /// <summary>
        /// 自动计算吸烟指数（支年） smokeYear*smokePerDay
        /// </summary>
        public decimal? SmokeIndex
        {
            set { _smokeindex = value; }
            get { return _smokeindex; }
        }
        /// <summary>
        /// 是否戒烟
        /// </summary>
        public bool QuitSmoke
        {
            set { _quitsmoke = value; }
            get { return _quitsmoke; }
        }
        /// <summary>
        /// 戒烟时间（四位数年）
        /// </summary>
        public string QuitYear
        {
            set { _quityear = value; }
            get { return _quityear; }
        }
        #endregion Model

    }
}

