using System;
using System.Collections.Generic;
using System.Text;

namespace AssetCloud.AssetCloudSDK
{
    public class AssetCloudResponse<T>
    {

        public int Code { get; set; }

        public bool Success { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }


        public override string ToString()
        {
            return $"AssetCloudResponse {{ Code = {Code}, Success = {Success}, Msg = {Msg}, Data = {Data} }}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
