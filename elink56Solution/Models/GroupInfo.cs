using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Serializable]
    public class GroupInfo : IComparable<GroupInfo>
    {
        #region 成员变量和公共属性
        private int _group_id;
        public int Group_id
        {
            set { _group_id = value; }
            get { return _group_id; }
        }

        private string _group_name;
        public string Group_name
        {
            set { _group_name = value; }
            get { return _group_name; }
        }

        private int _group_subjectionId;
        public int Group_subjectionId
        {
            set { _group_subjectionId = value; }
            get { return _group_subjectionId; }
        }

        private string _group_subjectionName;
        public string Group_subjectionName
        {
            set { _group_subjectionName = value; }
            get { return _group_subjectionName; }
        }

        private string _group_subjectionIds;
        public string Group_subjectionIds
        {
            set { _group_subjectionIds = value; }
            get { return _group_subjectionIds; }
        }

        private int _group_display;
        public int Group_display
        {
            set { _group_display = value; }
            get { return _group_display; }
        }


        private int _group_level;
        public int Group_level
        {
            set { _group_level = value; }
            get { return _group_level; }
        }


        private string _group_subjectionNames;
        public string Group_subjectionNames
        {
            set { _group_subjectionNames = value; }
            get { return _group_subjectionNames; }
        }



        private string _group_lower;
        public string Group_lower
        {
            set { _group_lower = value; }
            get { return _group_lower; }
        }


        private string _group_recommend;
        public string Group_recommend
        {
            set { _group_recommend = value; }
            get { return _group_recommend; }
        }

        private string _group_createdate;
        public string Group_createdate
        {
            set { _group_createdate = value; }
            get { return _group_createdate; }
        }


        #endregion

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据Group_id字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(GroupInfo other)
        {
            return Group_id.CompareTo(other.Group_id);
        }
        #endregion
    }
}

