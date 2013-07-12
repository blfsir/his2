﻿/*
 * COPD用药：
 * 口服药物：茶碱、白三烯受体拮抗剂、选择性磷酸二酯酶4抑制剂、激素、化痰药、镇咳药、其它，每类前面设打钩，后面手动填写药物名称，剂量，用法
 * 吸入药物：短效β受体激动剂、长效β受体激动剂、吸入激素、长效β受体激动剂/激素、长效抗胆碱能药物、其它，每类前面设打钩，后面手动填写药物名称，剂量，用法
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.common
{
    public class COPD
    {
        public enum Oral {茶碱=100,白三烯受体拮抗剂,选择性磷酸二酯酶4抑制剂,激素,化痰药,镇咳药,其它 }
        public enum Suck {短效β受体激动剂=200,长效β受体激动剂,吸入激素,长效β受体激动剂或激素,长效抗胆碱能药物,其它 }

        
        //static void Main()
        //{



        //    foreach (Oral foo in Enum.GetValues(typeof(Oral)))
        //    {
        //        Console.WriteLine(foo.ToString());
        //    }
        //}
    }

}
