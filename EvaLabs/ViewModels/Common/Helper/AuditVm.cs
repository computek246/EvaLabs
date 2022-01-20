using System;
using System.Text.Json.Serialization;

namespace EvaLabs.ViewModels.Common.Helper
{
    public class BaseEntityVm
    {
        public int Id { get; set; }
    }

    public class AuditVm : AuditVm<int>
    {
    }

    public class AuditVm<TForeignKey> : BaseEntityVm
    {
        public bool IsActive { get; set; }

        [JsonIgnore] public TForeignKey CreatorId { get; set; }
        [JsonIgnore] public DateTime CreationDate { get; set; }
        [JsonIgnore] public TForeignKey ModifierId { get; set; }
        [JsonIgnore] public DateTime LastModifiedDate { get; set; }
    }
}