namespace viviapi.Model
{
    public class phoneValid
    {
        private int _id;
        private string _phone;
        private int _count;

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                this._phone = value;
            }
        }

        public int count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }
    }
}
