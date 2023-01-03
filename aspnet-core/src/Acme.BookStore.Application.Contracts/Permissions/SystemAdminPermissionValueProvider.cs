//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp.Authorization.Permissions;

//namespace Acme.BookStore.Permissions
//{
//    public class SystemAdminPermissionValueProvider : PermissionValueProvider
//    {
//        public SystemAdminPermissionValueProvider(IPermissionStore permissionStore) : base(permissionStore)
//        {
//        }

//        public override string Name => "SystemAdmin";

//        public async override Task<PermissionGrantResult>
//          CheckAsync(PermissionValueCheckContext context)
//        {
//            if (context.Principal?.FindFirst("User_Type")?.Value == "SystemAdmin")
//            {
//                return PermissionGrantResult.Granted;
//            }

//            return PermissionGrantResult.Undefined;
//        }

//        public override Task<MultiplePermissionGrantResult> CheckAsync(PermissionValuesCheckContext context)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
