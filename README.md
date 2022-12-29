# ABP-Framework
## ABP- DI: 
* ABP tự động đăng kí dịch vụ nếu không muốn tự động đăng kí dịch vụ có thể 
`
 public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SkipAutoServiceRegistration = true;
    }
`
