using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HIS.common
{
    public class ValidateData
    {
        /// <summary>
        /// 验证类型
        /// </summary>
        public enum ValidateType
        {
            /// <summary>
            /// 纯数字
            /// </summary>
            Numbers,
            /// <summary>
            /// 正整数(不包括0)
            /// </summary>
            PositiveInteger,
            /// <summary>
            /// 正数,(不包括0)
            /// </summary>
            PositiveDecimal,
            /// <summary>
            /// 正数,(最多两位小数)
            /// </summary>
            PositiveDecimal2,
            /// <summary>
            /// 手机号码,13位数字
            /// </summary>
            Cellphone,
            /// <summary>
            /// 固定电话,格式如0591-88888888或88888888
            /// </summary>
            FixedTelephone,
            /// <summary>
            /// 身份证
            /// </summary>
            IDCARD,
            /// <summary>
            /// Email
            /// </summary>
            Email,
            /// <summary>
            /// 字母(包括大小写)
            /// </summary>
            Chars,
            /// <summary>
            /// 字母(包括大小写)和数字
            /// </summary>
            CharsAndNumbers,
            /// <summary>
            /// IP地址
            /// </summary>
            IPAddress,
            /// <summary>
            /// 邮编
            /// </summary>
            Postcode,
            /// <summary>
            /// 任意,不验证.
            /// </summary>
            Any

        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="control">要验证的控件</param>
        /// <param name="controlName">控件对应的中文名</param>
        /// <param name="nullable">是否为空,true表示可空,false不是不可空</param>
        /// <param name="minLength">最小长度,-1表示不验证最小长度</param>
        /// <param name="maxLength">最大长度,-1表示不验证最小长度</param>
        /// <param name="validateType">验证类型</param>
        /// <returns></returns>
        public static bool Validate(Control control, string ChineseName, bool nullable, int minLength, int maxLength)
        {
            return Validate(control, ChineseName, nullable, minLength, maxLength, ValidateType.Any);
        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="control">要验证的控件</param>
        /// <param name="controlName">控件对应的中文名</param>
        /// <param name="nullable">是否为空,true表示可空,false不是不可空</param>
        /// <param name="minLength">最小长度,-1表示不验证最小长度</param>
        /// <param name="maxLength">最大长度,-1表示不验证最小长度</param>
        /// <param name="validateType">验证类型</param>
        /// <returns></returns>
        public static bool Validate(Control control, string ChineseName, bool nullable, int minLength, int maxLength, ValidateType validateType)
        {
            return Validate(control, ChineseName, nullable, minLength, maxLength, validateType, null);
        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="control">要验证的控件</param>
        /// <param name="controlName">控件对应的中文名</param>
        /// <param name="nullable">是否为空,true表示可空,false不是不可空</param>
        /// <param name="minLength">最小长度,-1表示不验证最小长度</param>
        /// <param name="maxLength">最大长度,-1表示不验证最大长度</param>
        /// <param name="validateType">验证类型</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public static bool Validate(Control control, string ChineseName, bool nullable, int minLength, int maxLength, ValidateType validateType, double? maxValue)
        {
            //验证这几种类型
            if (!(control is TextBox || control is ComboBox || control is Infragistics.Win.UltraWinEditors.UltraNumericEditor))
            {
                return false;
            }
            string messageContent = string.Empty;
            string text = "";
            if (control is Infragistics.Win.UltraWinEditors.UltraNumericEditor)
            {
                //取控件中的文本的值
                object controlValue = ((Infragistics.Win.UltraWinEditors.UltraNumericEditor)control).Value;
                text = controlValue == null ? "" : controlValue.ToString();
            }
            else
            {
                text = control.Text;
            }
            //验证失败,提示失败内容
            if (GetValidateMessageContent(text, ChineseName, nullable, minLength, maxLength, validateType, maxValue, ref messageContent) == false)
            {
                MessageBox.Show(messageContent, "提示");
                control.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="control">UltraNumericEditor 控件</param>
        /// <param name="ChineseName">是否为空,true表示可空,false不是不可空</param>
        /// <param name="nullable">是否为0,true表示可为0,false不是不可0</param>
        /// <param name="zeroable">是否可为0</param>
        /// <returns></returns>
        public static bool Validate(Infragistics.Win.UltraWinEditors.UltraNumericEditor control, string ChineseName, bool nullable, bool zeroable)
        {
            if (nullable == false && control.Value == null)
            {
                MessageBox.Show("请填写" + ChineseName, "提示");
                control.Focus();
                return false;
            }
            else if (control.Value != null && zeroable == false && control.Value.ToString() == "0")
            {
                MessageBox.Show(ChineseName + "不能为0", "提示");
                control.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="control">验证文本内容</param>
        /// <param name="controlName">控件对应的中文名</param>
        /// <param name="nullable">是否为空,true表示可空,false不是不可空</param>
        /// <param name="minLength">最小长度,-1表示不验证最小长度</param>
        /// <param name="maxLength">最大长度,-1表示不验证最小长度</param>
        /// <param name="validateType">验证类型</param>
        /// <returns></returns>
        public static bool Validate(string text, string ChineseName, bool nullable, int minLength, int maxLength)
        {
            return Validate(text, ChineseName, nullable, minLength, maxLength, ValidateType.Any);
        }



        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="control">验证文本内容</param>
        /// <param name="controlName">控件对应的中文名</param>
        /// <param name="nullable">是否为空,true表示可空,false不是不可空</param>
        /// <param name="minLength">最小长度,-1表示不验证最小长度</param>
        /// <param name="maxLength">最大长度,-1表示不验证最小长度</param>
        /// <param name="validateType">验证类型</param>
        /// <returns></returns>
        public static bool Validate(string text, string ChineseName, bool nullable, int minLength, int maxLength, ValidateType validateType)
        {
            return Validate(text, ChineseName, nullable, minLength, maxLength, validateType, null);
        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="control">验证文本内容</param>
        /// <param name="controlName">控件对应的中文名</param>
        /// <param name="nullable">是否为空,true表示可空,false不是不可空</param>
        /// <param name="minLength">最小长度,-1表示不验证最小长度</param>
        /// <param name="maxLength">最大长度,-1表示不验证最大长度</param>
        /// <param name="validateType">验证类型</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public static bool Validate(string text, string ChineseName, bool nullable, int minLength, int maxLength, ValidateType validateType, double? maxValue)
        {
            string messageContent = string.Empty;
            //验证失败,提示失败内容
            if (GetValidateMessageContent(text, ChineseName, nullable, minLength, maxLength, validateType, maxValue, ref messageContent) == false)
            {
                MessageBox.Show(messageContent, "提示");
                return false;
            }
            else
            {
                return true;
            }
        }



        /// <summary>
        /// 进行验证,返回验证消息.
        /// </summary>
        /// <param name="text">要验证的文件</param>
        /// <param name="controlName">控件对应的中文名</param>
        /// <param name="nullable">是否为空,true表示可空,false不是不可空</param>
        /// <param name="minLength">最小长度,-1表示不验证最小长度</param>
        /// <param name="maxLength">最大长度,-1表示不验证最大长度</param>
        /// <param name="validateType">验证类型</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="messageContent">提示内容</param>
        /// <returns></returns>
        private static bool GetValidateMessageContent(string text, string ChineseName, bool nullable, int minLength, int maxLength, ValidateType validateType, double? maxValue, ref string messageContent)
        {
            //不能为空,但控件中的长度为空.
            if (nullable == false && text.Length == 0)
            {
                messageContent = ChineseName + "不能为空,请重新输入!";
                return false;
            }
            //可空时,不验证长度-1表示不验证长度,
            else if (nullable == false && minLength != -1 && text.Length < minLength)
            {
                messageContent = ChineseName + "长度应不少于" + minLength + "位,请重新输入!";
                return false;
            }
            //-1表示不验证长度
            else if (maxLength != -1 && text.Length > maxLength)
            {
                messageContent = ChineseName + "长度应不多于" + maxLength + "位,请重新输入!";
                return false;
            }
            //如果文本内容为空,不进行验证.若验证类型不为空,且验证失败,返回验证结果.
            else if (text != string.Empty && validateType != ValidateType.Any && getRegexValidateMessageContent(text, ChineseName, validateType, ref messageContent) == false)
            {
                return false;
            }
            //如果最大值不为空,且验证的类型为数字类型,且大于最大值.
            else if (maxValue != null
                && (validateType == ValidateType.Numbers || validateType == ValidateType.PositiveInteger ||
                validateType == ValidateType.PositiveDecimal || validateType == ValidateType.PositiveDecimal2)
                && (Convert.ToDouble(text) > maxValue))
            {
                messageContent = ChineseName + "大于" + maxValue + ",请重新输入!";
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 正则表达式验证,匹配返回true,不匹配返回false
        /// </summary>
        /// <param name="text">要验证的文本</param>
        /// <param name="ChineseName">文本的中文名称</param>
        /// <param name="validateType">验证类型</param>
        /// <param name="messageContent">提示内容</param>
        /// <returns>匹配返回true,不匹配返回false</returns>
        private static bool getRegexValidateMessageContent(string text, string ChineseName, ValidateType validateType, ref string messageContent)
        {
            string regexString = string.Empty;
            switch (validateType)
            {
                //纯数字
                case ValidateType.Numbers:
                    regexString = @"^\d+$";
                    messageContent = ChineseName + "必须为数字,请重新输入!";
                    break;
                //正整数(不包括0)    
                case ValidateType.PositiveInteger:
                    regexString = @"^[1-9]\d*$";
                    messageContent = ChineseName + "必须为正整数,请重新输入!";
                    break;
                //正数
                case ValidateType.PositiveDecimal:
                    regexString = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
                    messageContent = ChineseName + "必须为正数,请重新输入!";
                    break;
                //正数,最多两位小数
                case ValidateType.PositiveDecimal2:
                    regexString = @"^[1-9]\d{0,8}(\.\d{1,2})?$";
                    messageContent = ChineseName + "必须为正数,请重新输入!";
                    break;
                //手机号码,11位数字
                case ValidateType.Cellphone:
                    regexString = @"^(\d{11})$";
                    messageContent = "移动电话号码必须为11位数字,请重新输入!";
                    break;
                //固定电话
                case ValidateType.FixedTelephone:
                    regexString = @"^(\d{3,4}-)?\d{6,11}$";
                    messageContent = ChineseName + "格式错误,格式为010-88888888或88888888,请重新输入!";
                    break;
                //身份证号
                case ValidateType.IDCARD:
                    regexString = @"^(\d{18}|\d{15}|\d{17}[Xx])$";
                    messageContent = "身份证格式错误,请重新输入!";
                    break;
                //Email
                case ValidateType.Email:
                    regexString = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                    messageContent = "Email格式错误,请重新输入!";
                    break;
                //验证字符
                case ValidateType.Chars:
                    regexString = @"^[A-Za-z]+$";
                    messageContent = ChineseName + "只能为字母,请重新输入!";
                    break;
                //字母(包括大小写)和数字
                case ValidateType.CharsAndNumbers:
                    regexString = @"^[A-Za-z0-9]+$";
                    messageContent = ChineseName + "只能字母和数字,请重新输入!";
                    break;
                //IP地址
                case ValidateType.IPAddress:
                    regexString = @"^(([1-9]?\d|1\d\d|2[0-4]\d|25[0-5])\.){3}([1-9]?\d|1\d\d|2[0-4]\d|25[0-5])$";
                    messageContent = "IP地址格式错误,请重新输入!";
                    break;
                //邮编
                case ValidateType.Postcode:
                    regexString = @"^(\d{6})$";
                    messageContent = "邮编必须为6位数字,请重新输入!";
                    break;
            }
            return Regex.IsMatch(text.Trim(), regexString);
        }

    }
}
