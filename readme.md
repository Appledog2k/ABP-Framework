# ABP-Framework
# Dependency-Injection
## LifeTime DI
* Singleton: Service được tạo chỉ một lần duy nhất.
* Scoped: Tạo một thể hiện mới cho tất cả các scope (Mỗi request là một scope). Trong scope thì service được dùng lại
* Transient: Một thể hiện mới luôn được tạo, mỗi khi được yêu cầu.
## ABP- DI: 
* ABP tự động đăng kí dịch vụ nếu không muốn tự động đăng kí dịch vụ có thể 
```
 public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SkipAutoServiceRegistration = true;
    }
```
* Khi bỏ qua tự động đăng kí phương thức cần phải đăng kí thủ công, phương thức AddAssemblyOf là phương thức mở rộng giúp đăng kí tất cả dịch vụ theo quy ước 
```
 public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAssemblyOf<NameProject_Module>();
    }
 ```
 ## Các dạng đăng kí mặc định DI
 * Các lớp mô-đun được đăng kí dưới dạng singleton 
 * MVC controllers (inherit Controller or AbpController) đăng kí dưới dạng transient
 * MVC page model đăng kí dưới dạng transient
 * MVC view component đăng kí dưới dạng transient
 * Application services (inherit ApplicationService class or các lớp con của nó) đăng kí dưới dạng transient
 * Repositories (implement BasicRepository class hoặc các lớp con của nó) đăng kí dưới dạng transient
 * Domain services (implement IDomainService interface or inherit DomainService class) are registered as transient.
 
 # Dependency Interface
 * Nếu lớp được triển khai từ các interface ITransientDepency , IScopeDependency, ISingletonDependency lớp sẽ được tự động đăng kí
 # Dependency Attribute
 * Có thể đăng kí dependency injection bằng cách sử dụng dependency attribute với các thuộc tính:
 ```
 Lifetime: Life đăng kí các dịch vụ: Singleton, Scope, Transient
 TryRegister: Đặt trueđể chỉ đăng ký dịch vụ nếu nó chưa được đăng ký trước đó. Sử dụng các phương thức mở rộng TryAdd... của IServiceCollection.
 ReplaceServices: Đặt trueđể thay thế các dịch vụ nếu chúng đã được đăng ký trước đó. Sử dụng Thay thế phương thức mở rộng của IServiceCollection.
 ```
 * Dependency có mức độ ưu tiên cao hơn nếu chúng xác định thuộc tính lifetime
 # Thuộc tính ExposeServices
 * Thuộc tính này được sử dụng để kiểm soát dịch vụ nào được cung cấp bởi các bên liên quan giả sử một class được triển khai từ n interface nếu dùng thuộc tính này sẽ khẳng định được triển khai từ Interface nào
 ```
 [ExposeServices(typeof(ITaxCalculator))]
public class TaxCalculator: ICalculator, ITaxCalculator, ICanCalculate, ITransientDependency
{

}
class TaxCalculator chỉ được tiêm bới ITaxCalculator
 ```
 # Options
 * Lấy giá trị options bằng cách DI dịch vụ IOptions vào lớp và sử dụng thuộc tính .Value
 ```
 public class MyService : ITransientDependency
{
    private readonly MyOptions _options;
    
    public MyService(IOptions<MyOptions> options)
    {
        _options = options.Value; //Notice the options.Value usage!
    }

    public void DoIt()
    {
        var v1 = _options.Value1;
        var v2 = _options.Value2;
    }
}
```
* Các property options chỉ được sử dụng khi đã đươc định nghĩa và DI trong class, nếu muốn sử dụng chúng trong việc cấu hình trong giai đoạn DI, để giải quyết vấn đề này  ABP đã đưa ra các phương thức mở rộng PreConfigure<TOptions> và ExecutePreConfigureActions<TOptions> của IServiceCollection
* Cách sử dụng:
``` 
Định nghĩa một lớp tùy chọn trước cho module của project:
public class MyPreOptions
{
    public bool MyValue { get; set; }
}
Bất kì module nào phụ thuộc vào module của bạn đều có thể sử dụng phương thức PreConfigure<TOptions> trong PreConfigureServices
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<MyPreOptions>(options =>
    {
        options.MyValue = true;
    });
}
Các module khác nhau có thể ghi đè giá trị dựa trên thứ tự sử dụng của chúng
public override void ConfigureServices(ServiceConfigurationContext context)
{
    var options = context.Services.ExecutePreConfiguredActions<MyPreOptions>();
    if (options.MyValue)
    {
        //...
    }
}

```
# Authorizatio
* Xác thực được dùng để kiểm tra xem người dùng có được phép thực hiện một số thao tác cụ thể trong ứng dụng hay không
 ## Authorize Attribute 
 ## Permission system
 * quyền là một chính sách đơn giản được cấp hoặc cấm đối với một người dùng, vai trò và khách hàng cụ thể
 ## Defining permisstions
 * Để xác định quyền, tạo một lớp kế thừa từ PermisstionDefinitionProvider
 ```
 using Volo.Abp.Authorization.Permissions;

namespace Acme.BookStore.Permissions
{
    public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup("BookStore");

            myGroup.AddPermission("BookStore_Author_Create");
        }
    }

Notes: ABP tự động nhận dạng được lớp này không cần cấu hình thêm, thường cấu hình quyền trong module Application.Contracts
```
 ## Multi-Tenancy
 * ABP hỗ trợ nhiều kiểu đăng kí quyền, có thể xác định quyền cho các bên thuê dịch vụ khi xác định quyền mới.
 ```
 Host: quyền chỉ khả dụng cho phía máy chủ
 Tenant: quyền chỉ khả dụng cho đối tượng thuê.
 Both (mặc định): quyền có sẵn cho cả bên thuê và bên lưu trữ
 Notes: Nếu ứng dụng không phải nhiều bên thuê có thể bỏ qua mục này
 Để đặt tùy chọn bên cho nhiều người thuê trong phương thức thêm params thứ 3
 myGroup.AddPermission(
    "BookStore_Author_Create",
    LocalizableString.Create<BookStoreResource>("Permission:BookStore_Author_Create"),
    multiTenancySide: MultiTenancySides.Tenant //set multi-tenancy side!
);
```
 ## Enable/ Disable Permisstions
 * Quyền được bật mặc định, có thể vô hiệu hóa một quyền, quyền bị vô hiệu hóa sẽ bị cấm đối với mọi người, có thể kiểm tra quyên nhưng nó sẽ luôn bị cấm
 ```
 myGroup.AddPermission("Author_Management", isEnabled: false);
```
 ## Changing Permisstion Definitions of a depended module 
 ```context
    .GetPermissionOrNull(IdentityPermissions.Roles.Delete)
    .IsEnabled = false;
 Khi thêm mã này vào mục định nghĩa quyền nó sẽ tìm thấy quyền xóa roles của module nhận dạng và vô hiệu quá quyền, nên trong ứng dụng không ai có thế xóa roles
```
 # Sử dụng quyền tùy thuộc vào điều kiện
 * Tùy thuộc vào một tính năng
 ```
 myGroup.AddPermission("Book_Creation")
    .RequireFeatures("BookManagement");
NOTES: Sử dụng phương thức mở rộng RequreFeatures để sử dụng khi đó, quyền chỉ được sử dụng khí tính năng BookManagement được bật, tương tự như vậy có thể sử dụng phương thức RequireGlobalFeatures
```
 # Permission Management
 * Nếu muốn quản lí quyền theo mã, inject  thêm IPermisstionManager
 ```
 public class MyService : ITransientDependency
{
    private readonly IPermissionManager _permissionManager;

    public MyService(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public async Task GrantPermissionForUserAsync(Guid userId, string permissionName)
    {
        await _permissionManager.SetForUserAsync(userId, permissionName, true);
    }

    public async Task ProhibitPermissionForUserAsync(Guid userId, string permissionName)
    {
        await _permissionManager.SetForUserAsync(userId, permissionName, false);
    }
}
 Notes: SetForUserAsync đặt giá trị true/false cho quyền của người dùng, có các phương thức mở rộng hơn như SetForRoleAsync và SetForClientAsync
        IPermission được xác định bởi module quản lý quyền
```
 # Avanced Topics Permisstions
 ## Permission value providers
 * UserPermissions Value Provider kiểm tra xem người dùng được cấp quyền hay chưa sử dụng, lấy Id của người dùng từ các xác nhận quyền sở hữu hiện tại, UserClaims name được định nghĩa bởi AbpClaimTypes.UserID
 * RolePermissionValueProvider......AbpClaimTypesTypes.Role
 * ClientPermisstionValueProvider kiểm tra xem máy khách hiện tại có được cấp quyền hay không... AbpClaimTypes.ClientId
 

