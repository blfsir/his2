using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class Patient
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string idcode;

        public string Idcode
        {
            get { return idcode; }
            set { idcode = value; }
        }
        private DateTime signdate;

        public DateTime Signdate
        {
            get { return signdate; }
            set { signdate = value; }
        }
        private DateTime modifydate;

        public DateTime Modifydate
        {
            get { return modifydate; }
            set { modifydate = value; }
        }
    }
}
