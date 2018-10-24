using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mas.Domain.Common
{
    public class BaseEntity
    {

        public BaseEntity()
        {

        }

        public BaseEntity(BaseEntity entity)
        {
            Id = entity.Id;
            RowVersion = entity.RowVersion;
            Created = entity.Created;
            CreatorId = entity.CreatorId;
            CreatorName = entity.CreatorName;
            ModifierId = entity.ModifierId;
            Modified = entity.Modified;
            ModifierName = entity.ModifierName;
            State = entity.State;
        }
       

        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [MaxLength(30)]
        [Column(Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column(Order = 990)]
        [DefaultValue(CommonState.Enable)]
        public CommonState State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Order = 991)]
        public DateTime Created { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        [Column(Order = 992)]
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建人Name
        /// </summary>
        [Column(Order = 993)]
        public string CreatorName{ get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column(Order = 994)]
        public DateTime Modified { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        [Column(Order = 995)]
        public string ModifierId { get; set; }

        /// <summary>
        /// 修改人Name
        /// </summary>
        [Column(Order = 996)]
        public string ModifierName { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [Timestamp]
        [Column(Order = 997)]
        public byte[] RowVersion { get; set; }
    }
}
