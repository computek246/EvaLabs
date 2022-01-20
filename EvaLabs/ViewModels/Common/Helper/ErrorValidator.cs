using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EvaLabs.ViewModels.Common.Helper
{
    public abstract class ErrorValidator
    {
        [JsonIgnore] public bool HasErrors => Validate();

        [JsonIgnore] public List<string> Errors { get; protected set; }

        protected abstract bool Validate();
    }
}