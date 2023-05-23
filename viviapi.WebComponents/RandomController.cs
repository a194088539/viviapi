using System;
using System.Collections.Generic;

namespace viviapi.WebComponents
{
    public class RandomController
    {
        public List<char> datas = new List<char>((IEnumerable<char>)new char[26]
        {
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'I',
      'J',
      'K',
      'L',
      'M',
      'N',
      'O',
      'P',
      'Q',
      'R',
      'S',
      'T',
      'U',
      'V',
      'W',
      'X',
      'Y',
      'Z'
        });
        public List<ushort> weights = new List<ushort>((IEnumerable<ushort>)new ushort[26]
        {
      (ushort) 1,
      (ushort) 2,
      (ushort) 3,
      (ushort) 4,
      (ushort) 5,
      (ushort) 6,
      (ushort) 7,
      (ushort) 8,
      (ushort) 9,
      (ushort) 0,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1,
      (ushort) 1
        });
        private int _Count;

        public int Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        public RandomController(ushort count)
        {
            if ((int)count > 26)
                throw new Exception("抽取个数不能超过数据集合大小！！");
            this._Count = (int)count;
        }

        private List<KeyValuePair<char, int>> SortByValue(Dictionary<char, int> dict)
        {
            List<KeyValuePair<char, int>> list = new List<KeyValuePair<char, int>>();
            if (dict != null)
            {
                list.AddRange((IEnumerable<KeyValuePair<char, int>>)dict);
                list.Sort((Comparison<KeyValuePair<char, int>>)((kvp1, kvp2) => kvp2.Value - kvp1.Value));
            }
            return list;
        }

        public char[] ControllerRandomExtract(Random rand)
        {
            List<char> list = new List<char>();
            if (rand != null)
            {
                Dictionary<char, int> dict = new Dictionary<char, int>(26);
                for (int index = this.datas.Count - 1; index >= 0; --index)
                    dict.Add(this.datas[index], rand.Next(100) * (int)this.weights[index]);
                foreach (KeyValuePair<char, int> keyValuePair in this.SortByValue(dict).GetRange(0, this.Count))
                    list.Add(keyValuePair.Key);
            }
            return list.ToArray();
        }

        public char[] RandomExtract(Random rand)
        {
            List<char> list = new List<char>();
            if (rand != null)
            {
                int count = this.Count;
                while (count > 0)
                {
                    char ch = this.datas[rand.Next(25)];
                    if (!list.Contains(ch))
                    {
                        list.Add(ch);
                        --count;
                    }
                }
            }
            return list.ToArray();
        }
    }
}
