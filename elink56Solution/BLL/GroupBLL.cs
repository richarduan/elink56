using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;

namespace BLL  //请先添加引用
{
    public class GroupBLL
    {
        private readonly DAL.GroupDAL dal = new DAL.GroupDAL();
        public GroupBLL()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int InsertData(Models.GroupInfo model)
        {
            return dal.InsertData(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpDate(Models.GroupInfo model)
        {
            dal.UpDate(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int group_id)
        {
            dal.Delete(group_id);
        }
        /// <summary>
        ///  得到一个对象实体
        /// </summary>
        public Models.GroupInfo GetModel(int group_id)
        {
            return dal.GetModel(group_id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<GroupInfo> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public IList<GroupInfo> GetList(int group_subjectionId)
        {
            if (group_subjectionId < 1) group_subjectionId = 0;
            string where = string.Format("group_subjectionId={0}", Convert.ToString(group_subjectionId));
            return dal.GetList(where);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<GroupInfo> GetList()
        {
            return dal.GetList("");
        }
        #endregion  成员方法
    }
}
