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
 
