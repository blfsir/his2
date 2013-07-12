using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.common
{
   public  class Constants
    {
        public string Name { get; set; }

        public Constants() { }
        public Constants(string _name)
        {
            Name = _name;
        }
        public List<Constants> races() {
            return new List<Constants> { 
                                new Constants("汉族"), 
                                new Constants("壮族"),
                                new Constants("满族"),
                                new Constants("回族"),
                                new Constants("苗族"),
                                new Constants("维吾尔族"),
                                new Constants("土家族"),
                                new Constants("彝族"),
                                new Constants("蒙古族"),
                                new Constants("藏族"),
                                new Constants("布依族"),
                                new Constants("侗族"),
                                new Constants("瑶族"), 
                                new Constants("朝鲜族"),
                                new Constants("白族"),
                                new Constants("哈尼族"),
                                new Constants("哈萨克族"),
                                new Constants("黎族"),
                                new Constants("傣族"),
                                new Constants("畲族"),
                                new Constants("傈僳族"),
                                new Constants("仡佬族"),
                                new Constants("东乡族"),
                                new Constants("高山族"),
                                new Constants("拉祜族"),
                                new Constants("水族"),
                                new Constants("佤族"),
                                new Constants("纳西族"),
                                new Constants("羌族"),
                                new Constants("土族"),
                                new Constants("仫佬族"),
                                new Constants("锡伯族"),
                                new Constants("柯尔克孜族"),
                                new Constants("达斡尔族"),
                                new Constants("景颇族"),
                                new Constants("毛南族"),
                                new Constants("撒拉族"),
                                new Constants("布朗族"),
                                new Constants("塔吉克族"),
                                new Constants("阿昌族"),
                                new Constants("普米族"),
                                new Constants("鄂温克族"),
                                new Constants("怒族"),
                                new Constants("京族"),
                                new Constants("基诺族"),
                                new Constants("德昂族"),
                                new Constants("保安族"),
                                new Constants("俄罗斯族"),
                                new Constants("裕固族"),
                                new Constants("乌兹别克族"),
                                new Constants("门巴族"),
                                new Constants("鄂伦春族"),
                                new Constants("独龙族"),
                                new Constants("塔塔尔族"),
                                new Constants("赫哲族"),
                                new Constants("珞巴族") 
 
        };
        }

        public List<Constants> regions() {
            return new List<Constants> { 
                new Constants("北京市"),
                new Constants("天津市"),
                new Constants("河北省"),
                new Constants("山西省"),
                new Constants("内蒙古"),
                new Constants("辽宁省"),
                new Constants("吉林省"),
                new Constants("黑龙江"),
                new Constants("上海市"),
                new Constants("江苏省"),
                new Constants("浙江省"),
                new Constants("安徽省"),
                new Constants("福建省"),
                new Constants("江西省"),
                new Constants("山东省"),
                new Constants("河南省"),
                new Constants("湖北省"),
                new Constants("湖南省"),
                new Constants("广东省"),
                new Constants("广西省"),
                new Constants("海南省"),
                new Constants("重庆市"),
                new Constants("四川省"),
                new Constants("贵州省"),
                new Constants("云南省"),
                new Constants("西藏"),
                new Constants("陕西省"),
                new Constants("甘肃省"),
                new Constants("青海省"),
                new Constants("宁夏"),
                new Constants("新疆"),
                new Constants("台湾省"),
                new Constants("香港"),
                new Constants("澳门")
            };
        }

        public List<Constants> TreatResults()
        {
            return new List<Constants> { 
                new Constants("治愈"),
                new Constants("好转"),
                new Constants("维持"),
                new Constants("进展")  
            };
        }
    }
}
