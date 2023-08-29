using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIndieFarm
{
    public class Global
    {
        /// <summary>
        /// 默认从第一天开始
        /// </summary>
        public static BindableProperty<int> Days = new BindableProperty<int>(1);

    }
}
