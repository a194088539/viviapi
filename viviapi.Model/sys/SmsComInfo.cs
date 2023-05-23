namespace viviapi.Model.sys
{
    public class SmsComInfo
    {
        private string _destnumber = string.Empty;
        private string _cmd = string.Empty;
        private int _fee = 0;

        public string destnumber
        {
            get
            {
                return this._destnumber;
            }
            set
            {
                this._destnumber = value;
            }
        }

        public string cmd
        {
            get
            {
                return this._cmd;
            }
            set
            {
                this._cmd = value;
            }
        }

        public int fee
        {
            get
            {
                return this._fee;
            }
            set
            {
                this._fee = value;
            }
        }
    }
}
