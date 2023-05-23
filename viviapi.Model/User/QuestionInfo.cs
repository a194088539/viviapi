using System;

namespace viviapi.Model.User
{
    [Serializable]
    public class QuestionInfo
    {
        private int _id;
        private string _question;
        private bool _release;
        private int _sort;

        public int id
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

        public string question
        {
            get
            {
                return this._question;
            }
            set
            {
                this._question = value;
            }
        }

        public bool release
        {
            get
            {
                return this._release;
            }
            set
            {
                this._release = value;
            }
        }

        public int sort
        {
            get
            {
                return this._sort;
            }
            set
            {
                this._sort = value;
            }
        }
    }
}
